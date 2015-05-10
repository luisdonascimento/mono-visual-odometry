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
using System.Drawing;

namespace VisualOdometry
{
	public class RotationAnalyzer
	{
		private VisualOdometer m_VisualOdometer;
		private double m_FocalLengthX;
		private double m_CenterX;

		private Angle m_HeadingChange;
		private List<double> m_RotationIncrements;

		internal RotationAnalyzer(VisualOdometer visualOdometer)
		{
			m_VisualOdometer = visualOdometer;
			m_FocalLengthX = visualOdometer.CameraParameters.Intrinsic.Fx;
			m_CenterX = visualOdometer.CameraParameters.Intrinsic.Cx;

			m_RotationIncrements = new List<double>();
		}

		public Angle HeadingChange
		{
			get { return m_HeadingChange; }
		}

		public List<double> MeasuredRotationIncrements
		{
			get { return m_RotationIncrements; }
		}

		internal void CalculateRotation()
		{
			m_RotationIncrements.Clear();

			List<TrackedFeature> trackedFeatures = m_VisualOdometer.TrackedFeatures;
			for (int i = 0; i < trackedFeatures.Count; i++)
			{
				TrackedFeature trackedFeature = trackedFeatures[i];
				if (trackedFeature.Count < 2)
				{
					continue;
				}
				PointF previousFeatureLocation = trackedFeature[-1];
				PointF currentFeatureLocation = trackedFeature[0];

				if (currentFeatureLocation.Y <= m_VisualOdometer.SkyRegionBottom)
				{
					double previousAngularPlacement = Math.Atan2(previousFeatureLocation.X - m_CenterX, m_FocalLengthX);
					double currentAngularPlacement = Math.Atan2(currentFeatureLocation.X - m_CenterX, m_FocalLengthX);
					double rotationIncrement = currentAngularPlacement - previousAngularPlacement;
				
					m_RotationIncrements.Add(rotationIncrement);
				}
			}

	
			if (m_RotationIncrements.Count > 0)
			{
				double meanRotationIncrement = DetermineBestRotationIncrement();
				m_HeadingChange = Angle.FromRads(meanRotationIncrement);
			}
		}

		private double DetermineBestRotationIncrement()
		{
			m_RotationIncrements.Sort();
			double meanRotationIncrement = m_RotationIncrements[m_RotationIncrements.Count / 2];
			return meanRotationIncrement;
		}
	}
}
