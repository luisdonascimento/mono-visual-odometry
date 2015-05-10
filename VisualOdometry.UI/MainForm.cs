/* Projecto 2014
 * Faculdade Ciencias Universidade do Porto
 * 
 * Developed By Luis Do Nascimento
 * featuring codes from openslam.org and Edgard Quirino and Mário Almeida 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualOdometry.UI.Properties;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
using CameraCalibrator.Support;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using System.Diagnostics;
using Lego.Ev3.Core;
using Lego.Ev3.Desktop;

using System.Threading.Tasks;

using Microsoft.VisualBasic;

using System.Runtime.InteropServices;


namespace VisualOdometry.UI
{
	public partial class MainForm : Form
	{
        //Odometry
        private VisualOdometer m_VisualOdometer;
        private Boolean[,] Obstacle;
		private Capture m_Capture;
		//private HomographyMatrix m_GroundProjectionTransformationForUI;
		private RobotPath m_RobotPath = new RobotPath();
		private MapForm m_MapForm;

        //Lego
        private Brick _brick;
        private int _foward;
        private int _backward;
        private int _turn;
        uint _time = 300;
        private string _bluetoothPort;
        private string _CamIP;

		public MainForm()
		{
			InitializeComponent();

			CameraParameters cameraParameters = null;
			HomographyMatrix groundProjectionTransformation = null;

            bool useCamera=false;


            _CamIP = Prompt.ShowDialog("OK para testes", "Conexão Camara");
            if (_CamIP != "")
            {
                useCamera = true;
            }

        	if (useCamera)
			{
				
               //Emgu.CV.CvInvoke.cvCreateFileCapture("http://username:pass@cam_address/axis-cgi/mjpg/video.cgi?resolution=1280x720&req_fps=30&.mjpg");
                m_Capture = new Capture(0);
				m_Capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1280);
				m_Capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 720);

				cameraParameters = CameraParameters.Load(@"..\..\CalibrationFiles\MicrosoftCinemaFocus12.txt");

				groundProjectionTransformation = HomographyMatrixSupport.Load(@"..\..\CalibrationFiles\BirdsEyeViewTransformationForCalculation.txt");
			}
            else
            {
                m_Capture = new Capture(@"..\..\CalibrationFiles\2010-07-18 11-10-22.853.wmv");
                m_Timer.Interval = 33;
                m_Timer.Enabled = true;

                cameraParameters = CameraParameters.Load(@"..\..\CalibrationFiles\MicrosoftCinemaFocus12.txt");

                groundProjectionTransformation = HomographyMatrixSupport.Load(@"..\..\CalibrationFiles\BirdsEyeViewTransformationForCalculation.txt");
              
            }			
          
			m_VisualOdometer = new VisualOdometer(m_Capture, cameraParameters, groundProjectionTransformation, new OpticalFlow());
     
			UpdateFromModel();

			m_VisualOdometer.Changed += new EventHandler(OnVisualOdometerChanged);
			Application.Idle += OnApplicationIdle;

            if (m_MapForm == null || m_MapForm.IsDisposed)
            {
                m_MapForm = new MapForm(m_RobotPath);
            }

            m_MapForm.Show();

		}

		private void OnTimerTick(object sender, EventArgs e)
		{
			ProcessFrame();
		}

		private void OnVisualOdometerChanged(object sender, EventArgs e)
		{
			UpdateFromModel();
		}

		private void OnApplicationIdle(object sender, EventArgs e)
		{
			ProcessFrame();
		}

        private DenseHistogram GetHistogramsHsv(Image<Hsv, Byte> src, int nBins, int type,float max)
        {
            DenseHistogram histogram;
            Image<Gray, Byte>[] channels = src.Split();
            histogram = new DenseHistogram(nBins, new RangeF(0, max));
            histogram.Calculate(new Image<Gray, Byte>[] { channels[type] }, false, null);
            histogram.Normalize(256);
            return histogram;
        }

        private void DrawObstacle() {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Obstacle[i, j])
                    {
                        m_VisualOdometer.CurrentImage.Draw(new Rectangle(i * (1280 / 4), j * (720 / 4), (1280 / 4), 720 / 4), new Bgr(255, 180, 23), -1);
                    }
                }
            }
        }


        private void GetObstacles(Image<Hsv, Byte> src,Image<Bgr, Byte> result)
        {
            Image<Hsv, Byte> reference;

            float[] Hist = new float[181];
            float[] Hist1 = new float[300];
            float[] Hist2 = new float[300];
            Obstacle= new Boolean[4,4];

            Rectangle roi = new Rectangle(500, 300, 400, 400);
            DenseHistogram ref_hue;
            DenseHistogram ref_sat;
            DenseHistogram ref_int;

            int value;
            double bin;
            int value1;
            double bin1;

            reference = src.Clone();
            reference.ROI = roi;

            result.Draw(roi, new Bgr(System.Drawing.Color.Green), 1);

            ref_hue = GetHistogramsHsv(reference, 180, 0,180);
            ref_sat = GetHistogramsHsv(reference, 256, 1,256);
            ref_int = GetHistogramsHsv(reference, 256, 2,256);
            ref_hue.MatND.ManagedArray.CopyTo(Hist, 0);
            ref_int.MatND.ManagedArray.CopyTo(Hist1, 0);
            ref_sat.MatND.ManagedArray.CopyTo(Hist2, 0);
            List<TrackedFeature> trackedFeatures = m_VisualOdometer.TrackedFeatures;

            for (int i = 0; i < m_VisualOdometer.TrackedFeatures.Count; i++)
            {
                    TrackedFeature t_trackedFeature = trackedFeatures[i];
                    int x = (int)t_trackedFeature[0].X;
                    int y = (int)t_trackedFeature[0].Y;


                    if ((t_trackedFeature.IsFull) && (t_trackedFeature.Count > 1) && (x < 1280) && (y < 720) && (x > 0) && (y > 0))
                    {

                        value = (int)src[new Point(x, y)].Hue;
                        bin = Hist[value];
                        value1 = (int)src[new Point(x, y)].Value;
                        bin1 = Hist1[value1];

                        if ((bin <= 0.8) && (bin1 <= 0.8) && (Hist2[(int)src[new Point(x, y)].Satuation] <= 0.5))
                        {
                            SortObstacle(x, y);
   
                        
                        }
                   }
            }
        }

        private void SortObstacle(int x, int y) {
            int xpos=0;
            int ypos=0;
            if ((x < 1280 / 4)&&(x >= 0)) {
                xpos = 0;
            }
            if (((x >= 1280 / 4) && (x < 1280 / 2))) {
                xpos = 1;
            }
            if ((x >= 1280 / 2) && (x < (1280 / 2)+(1280 / 4)))  {
                xpos = 2;
            }
            if ((x >= (1280 / 2)+(1280 / 4)) && (x <= (1280)))  {
                xpos = 3;
            }
            if ((y >= 0)&&(y < 720 / 4)) {
                ypos = 0;
            }
            if (((y >= 720/ 4) && (y <720 / 2))) {
                ypos = 1;
            }
            if ((y >= 720 / 2) && (y < (720 / 2) + (720 / 4))) {
                ypos = 2;
            }
            if ((y >= (720 / 2) + (720 / 4)) && (y <= (720))) {
                ypos = 3;
            }
            if ((ypos == 2) && ((xpos == 1) || (xpos == 2))) { Obstacle[xpos, ypos] = false; }
            else { Obstacle[xpos, ypos] = true; }
   

        }
        
        private void ObstacleDetector() {
            Image<Hsv, Byte> imgHSV = m_VisualOdometer.CurrentImage.Convert<Hsv, Byte>();
            imgHSV.SmoothBlur(5, 5);
            GetObstacles(imgHSV, m_VisualOdometer.CurrentImage);
        }

		private void ProcessFrame()
		{
			m_VisualOdometer.ProcessFrame();
           
			if (m_VisualOdometer.CurrentImage == null)
			{
				return;
			}
           
			m_FramesPerSecondTextBox.Text = String.Format("{0:0.0}", m_VisualOdometer.FramesCounter.FramesPerSecond);

			DrawRegionBounderies();
			m_ImageBox.Image = m_VisualOdometer.CurrentImage;
			
			Pose newPose = new Pose(m_VisualOdometer.RobotPose);
			m_RobotPath.Add(newPose);

            ObstacleDetector();

			if (m_MapForm != null && !m_MapForm.IsDisposed)
			{
				m_MapForm.UpdateMap();
			}

            if (m_ObstaclesCheckBox.Checked)
            {
                DrawObstacle();
            }

            if (m_DrawFeaturesCheckBox.Checked)
			{
				DrawAllFeatureLocationsPreviousAndCurrent();
			}
            
            if (m_SurfCheckbox.Checked)
            {
                Image<Gray, Byte> img1 = new Image<Gray, Byte>("../../img.jpg");
                Image<Gray, Byte> img2 = m_VisualOdometer.CurrentImage.Convert<Gray, Byte>();

                executeSURF(img1, img2, m_VisualOdometer.CurrentImage);
            }
            
   

            ArtificialInteligence();
           
		}
        
		private void DrawRegionBounderies()
		{
			DrawRegionBoundary(m_VisualOdometer.CurrentImage, m_VisualOdometer.SkyRegionBottom);
			DrawRegionBoundary(m_VisualOdometer.CurrentImage, m_VisualOdometer.GroundRegionTop);
		}

		private void DrawRegionBoundary(Image<Bgr, Byte> image, int yPos)
		{
			PointF start = new PointF(0, yPos);
			PointF end = new PointF(image.Width, yPos);
			LineSegment2DF lineSegment = new LineSegment2DF(start, end);
			image.Draw(lineSegment, new Bgr(System.Drawing.Color.Red), 1);
		}

        public void executeSURF(Image<Gray, Byte> modelImage, Image<Gray, byte> observedImage, Image<Bgr,byte> result)
        {
            HomographyMatrix homography;
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;
            Matrix<int> indixes;
            Matrix<byte> mask;

            SURFfeature(modelImage, observedImage, out modelKeyPoints, out observedKeyPoints, out indixes, out mask, out homography);

    
            if (homography != null)
            {  
                Rectangle rect = modelImage.ROI;
                PointF[] pts = new PointF[] { 
                new PointF(rect.Left, rect.Bottom),
                new PointF(rect.Right, rect.Bottom),
                new PointF(rect.Right, rect.Top),
                new PointF(rect.Left, rect.Top)};
                homography.ProjectPoints(pts);

                result.DrawPolyline(Array.ConvertAll<PointF, Point>(pts, Point.Round), true, new Bgr(System.Drawing.Color.Red), 5);
            }
 
        }
   
        private void SURFfeature(Image<Gray, Byte> modelImage, Image<Gray, byte> observedImage, out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints, out Matrix<int> indices, out Matrix<byte> mask, out HomographyMatrix homography)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            SURFDetector surfCPU = new SURFDetector(300, false);
        
            homography = null;

            //extract features from the object image
            modelKeyPoints = new VectorOfKeyPoint();
            Matrix<float> modelDescriptors = surfCPU.DetectAndCompute(modelImage, null, modelKeyPoints);

            // extract features from the observed image
            observedKeyPoints = new VectorOfKeyPoint();
            Matrix<float> observedDescriptors = surfCPU.DetectAndCompute(observedImage, null, observedKeyPoints);
            BruteForceMatcher<float> matcher = new BruteForceMatcher<float>(DistanceType.L2);
            matcher.Add(modelDescriptors);

            indices = new Matrix<int>(observedDescriptors.Rows, k);
            using (Matrix<float> dist = new Matrix<float>(observedDescriptors.Rows, k))
            {
                matcher.KnnMatch(observedDescriptors, indices, dist, k, null);
                mask = new Matrix<byte>(dist.Rows, 1);
                mask.SetValue(255);
                Features2DToolbox.VoteForUniqueness(dist, uniquenessThreshold, mask);
            }

            int nonZeroCount = CvInvoke.cvCountNonZero(mask);
            if (nonZeroCount >= 4)
            {
                nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, indices, mask, 1.5, 20);
                if (nonZeroCount >= 4)
                    homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, indices, mask, 2);
            }
        }
              
        private void DrawAllFeatureLocationsPreviousAndCurrent()
        {
            double[,] Sections = new double[10, 5];
            List<TrackedFeature> trackedFeatures = m_VisualOdometer.TrackedFeatures;
            List<LineSegment2DF> OpticFluxLines = new List<LineSegment2DF>();
            List<PointF> FOE = new List<PointF>();
           
            for (int i = 0; i < trackedFeatures.Count; i++)
            {
                TrackedFeature trackedFeature = trackedFeatures[i];
                if (trackedFeature.Count > 1)
                {
                    OpticFluxLines.Add(new LineSegment2DF(trackedFeature[-1], trackedFeature[0]));
                    DrawOpticFluxLines(trackedFeature[-1], trackedFeature[0], trackedFeature.IsFull, m_VisualOdometer.CurrentImage);
                }
            }
        }

        internal void DrawOpticFluxLines(PointF previousFeatureLocation, PointF CurrentFeatureLocation, bool hasFullHistory, Image<Bgr, Byte> image)
        {
            if (hasFullHistory)
            {
                LineSegment2DF linusSegmentus = new LineSegment2DF(previousFeatureLocation, CurrentFeatureLocation);
                image.Draw(linusSegmentus, new Bgr(System.Drawing.Color.Red), 1);
            }
       
        }

		private void UpdateFromModel()
		{
			m_MaxFeatureCountTextBox.Text = m_VisualOdometer.OpticalFlow.MaxFeatureCount.ToString();
			m_BlockSizeTextBox.Text = m_VisualOdometer.OpticalFlow.BlockSize.ToString();
			m_QualityLevelTextBox.Text = m_VisualOdometer.OpticalFlow.QualityLevel.ToString();
			m_MinDistanceTextBox.Text = m_VisualOdometer.OpticalFlow.MinDistance.ToString();

			m_SkyBottomTextBox.Text = m_VisualOdometer.SkyRegionBottom.ToString();
			m_GroundTopTextBox.Text = m_VisualOdometer.GroundRegionTop.ToString();
		}

		private void OnApplyButtonClicked(object sender, EventArgs e)
		{
			int maxFeatureCount = m_VisualOdometer.OpticalFlow.MaxFeatureCount;
			Int32.TryParse(m_MaxFeatureCountTextBox.Text, out maxFeatureCount);

			int blockSize = m_VisualOdometer.OpticalFlow.BlockSize;
			Int32.TryParse(m_BlockSizeTextBox.Text, out blockSize);

			double qualityLevel = m_VisualOdometer.OpticalFlow.QualityLevel;
			Double.TryParse(m_QualityLevelTextBox.Text, out qualityLevel);

			double minDistance = m_VisualOdometer.OpticalFlow.MinDistance;
			Double.TryParse(m_MinDistanceTextBox.Text, out minDistance);

			OpticalFlow opticalFlow = new OpticalFlow(maxFeatureCount, blockSize, qualityLevel, minDistance);
			m_VisualOdometer.OpticalFlow = opticalFlow;

			int skyBottom;
			if (Int32.TryParse(m_SkyBottomTextBox.Text, out skyBottom))
			{
				m_VisualOdometer.SkyRegionBottom = skyBottom;
			}

			int groundTop;
			if (Int32.TryParse(m_GroundTopTextBox.Text, out groundTop))
			{
				m_VisualOdometer.GroundRegionTop = groundTop;
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			if (m_Capture != null)
			{
				m_Capture.Dispose();
			}
			if (m_VisualOdometer != null)
			{
				m_VisualOdometer.Dispose();
			}
			if (this.WindowState != FormWindowState.Minimized)
			{
				Settings.Default.Size = this.Size;
				Settings.Default.Location = this.Location;
			}
			Settings.Default.Save();
		}

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form();
                prompt.Width = 500;
                prompt.Height = 150;
                prompt.Text = caption;
                prompt.StartPosition = FormStartPosition.CenterScreen;
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                prompt.ShowDialog();
                return textBox.Text;
            }
        }

        private void Connect_btn_Click(object sender, EventArgs e)
        {
            _bluetoothPort = Prompt.ShowDialog("Porta Bluetooth", "Conexão Bluetooth");
            if (_bluetoothPort != "")
            {
                createConnection(_bluetoothPort);
            }
        }

        private async void createConnection(string connectionString)
        {
            if (connectionString == "")
            {
                _brick = new Brick(new UsbCommunication());
            }
            else
            {
                _brick = new Brick(new BluetoothCommunication(connectionString));

            }

            _brick.BrickChanged += _brick_BrickChanged;
            await _brick.ConnectAsync();
            await _brick.DirectCommand.PlayToneAsync(100, 1000, 300);
            await _brick.DirectCommand.StopMotorAsync(OutputPort.All, false);
        }

        private void _brick_BrickChanged(object sender, BrickChangedEventArgs e)
        {
            Debug.WriteLine("Mudou!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ArtificialInteligence() {
            int right=0;
            int left=0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i >= 0 && i <= 1 && Obstacle[i, j] == true) { left++; }
                    if (i >= 2 && i <= 3 && Obstacle[i, j] == true) { right++; }  
                }
            }

            if (right > left) { label5.Text="GO LEFT";}
            if (right < left) {  label5.Text="GO RIGHT";}
            if (right == left && right!=0) { label5.Text = "STOP"; }
        
        }



	}
}
