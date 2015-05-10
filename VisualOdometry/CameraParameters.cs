/* Projecto 2014
 * Faculdade Ciencias Universidade do Porto
 * 
 * Developed By Luis Do Nascimento
 * featuring codes from openslam.org and Edgard Quirino and Mário Almeida 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using System.IO;
using System.Globalization;

namespace VisualOdometry
{
	public class CameraParameters
	{
	

		internal static void Write(string name, double value, TextWriter writer)
		{
			writer.WriteLine(name + "\t" + value.ToString(CultureInfo.InvariantCulture));
		}

		public static CameraParameters Load(string filePath)
		{
			Dictionary<string, double> valuesByName = new Dictionary<string, double>(StringComparer.InvariantCultureIgnoreCase);
			using (TextReader reader = new StreamReader(filePath))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					line = line.Trim();
					if (line.Length == 0 || line.StartsWith("#"))
					{
						continue;
					}

					string[] lineParts = line.Split('\t');
					string name = lineParts[0];
					double value = double.Parse(lineParts[1], CultureInfo.InvariantCulture);
					valuesByName.Add(name, value);
				}
			}

			IntrinsicCameraParameters intrinsicCameraParameters = new IntrinsicCameraParameters();

			// Intrinsic parameters
			intrinsicCameraParameters.IntrinsicMatrix[0, 0] = valuesByName["fx:"];
			intrinsicCameraParameters.IntrinsicMatrix[1, 1] = valuesByName["fy:"];
			intrinsicCameraParameters.IntrinsicMatrix[0, 2] = valuesByName["cx:"];
			intrinsicCameraParameters.IntrinsicMatrix[1, 2] = valuesByName["cy:"];
			intrinsicCameraParameters.IntrinsicMatrix[2, 2] = 1.0;

			// Distortion coefficients
			intrinsicCameraParameters.DistortionCoeffs[0, 0] = valuesByName["k1:"];
			intrinsicCameraParameters.DistortionCoeffs[1, 0] = valuesByName["k2:"];
			intrinsicCameraParameters.DistortionCoeffs[2, 0] = valuesByName["p1:"];
			intrinsicCameraParameters.DistortionCoeffs[3, 0] = valuesByName["p2:"];
			intrinsicCameraParameters.DistortionCoeffs[4, 0] = valuesByName["k3:"];

			return new CameraParameters(intrinsicCameraParameters);
		}


		public CameraParameters(IntrinsicCameraParameters intrinsicCameraParameters)
		{
			this.IntrinsicCameraParameters = intrinsicCameraParameters;
			this.Intrinsic = new Intrinsic(intrinsicCameraParameters);
			this.Distortion = new Distortion(intrinsicCameraParameters);
		}

		public IntrinsicCameraParameters IntrinsicCameraParameters { get; private set; }

		public Intrinsic Intrinsic { get; private set; }

		public Distortion Distortion { get; private set; }

		public void Save(string filePath)
		{
			using (TextWriter writer = new StreamWriter(filePath))
			{
				this.Intrinsic.Write(writer);
				writer.WriteLine();
				this.Distortion.Write(writer);
			}
		}

	}

	

	public class Intrinsic
	{
		private IntrinsicCameraParameters m_IntrinsicCameraParameters;
		internal Intrinsic(IntrinsicCameraParameters intrinsicCameraParameters)
		{
			m_IntrinsicCameraParameters = intrinsicCameraParameters;
		}

		public double Fx
		{
			get { return m_IntrinsicCameraParameters.IntrinsicMatrix[0, 0]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[0, 0] = value; }
		}

		public double Fy
		{
			get { return m_IntrinsicCameraParameters.IntrinsicMatrix[1, 1]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[1, 1] = value; }
		}

		public double Cx
		{
			get { return m_IntrinsicCameraParameters.IntrinsicMatrix[0, 2]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[0, 2] = value; }
		}

		public double Cy
		{
			get { return m_IntrinsicCameraParameters.IntrinsicMatrix[1, 2]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[1, 2] = value; }
		}

		internal void Write(TextWriter writer)
		{
			writer.WriteLine("# Intrinsic parameters:");
			CameraParameters.Write("fx:", this.Fx, writer);
			CameraParameters.Write("fy:", this.Fy, writer);
			CameraParameters.Write("cx:", this.Cx, writer);
			CameraParameters.Write("cy:", this.Cy, writer);
		}
	}

	public class Distortion
	{
		private IntrinsicCameraParameters m_IntrinsicCameraParameters;
		internal Distortion(IntrinsicCameraParameters intrinsicCameraParameters)
		{
			m_IntrinsicCameraParameters = intrinsicCameraParameters;
		}

		public double K1
		{
			get { return m_IntrinsicCameraParameters.DistortionCoeffs[0, 0]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[0, 0] = value; }
		}

		public double K2
		{
			get { return m_IntrinsicCameraParameters.DistortionCoeffs[1, 0]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[1, 0] = value; }
		}

		public double K3
		{
			get { return m_IntrinsicCameraParameters.DistortionCoeffs[4, 0]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[4, 0] = value; }
		}

		public double P1
		{
			get { return m_IntrinsicCameraParameters.DistortionCoeffs[2, 0]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[2, 0] = value; }
		}

		public double P2
		{
			get { return m_IntrinsicCameraParameters.DistortionCoeffs[3, 0]; }
			set { m_IntrinsicCameraParameters.IntrinsicMatrix[3, 0] = value; }
		}

		internal void Write(TextWriter writer)
		{
			writer.WriteLine("# Distortion parameters:");
			CameraParameters.Write("k1:", this.K1, writer);
			CameraParameters.Write("k2:", this.K2, writer);
			CameraParameters.Write("k3:", this.K3, writer);
			CameraParameters.Write("P1:", this.P1, writer);
			CameraParameters.Write("P2:", this.P2, writer);
		}
	}



}
