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
using System.Windows;
using System.Diagnostics;

namespace VisualOdometry
{
	public class TranslationAnalyzer
	{
		private VisualOdometer m_VisualOdometer;
		private HomographyMatrix m_GroundProjectionTransformation;
		private List<TrackedFeature> m_GroundFeatures;
		private List<TrackedFeature> m_UsedGroundFeatures;
		private List<TrackedFeature> m_ScratchPadUsedGroundFeatures;
		private Random m_Random = new Random();
		private List<Point> m_TranslationIncrements;
		private Point m_CurrentLocationChange;
		private Angle m_AcceptedDirectionMisalignment;

		internal TranslationAnalyzer(VisualOdometer visualOdometer, HomographyMatrix groundProjectionTransformation)
		{
			m_VisualOdometer = visualOdometer;
			m_GroundProjectionTransformation = groundProjectionTransformation;
			m_GroundFeatures = new List<TrackedFeature>();
			m_UsedGroundFeatures = new List<TrackedFeature>();
			m_ScratchPadUsedGroundFeatures = new List<TrackedFeature>();
			m_TranslationIncrements = new List<Point>();
			m_AcceptedDirectionMisalignment = Angle.FromDegrees(45);
		}

		public Angle AcceptedDirectionMisalignment
		{
			get { return m_AcceptedDirectionMisalignment; }
			set { m_AcceptedDirectionMisalignment = value; }
		}

		public Point LocationChange
		{
			get { return m_CurrentLocationChange; }
		}

		internal void CalculateTranslation(Angle headingChange)
		{
			PopulateRotationCorrectedTranslationIncrements(headingChange);
			DeterminMostLikelyTranslationVector();
		}

		private void PopulateRotationCorrectedTranslationIncrements(Angle headingChange)
		{
			double s = Math.Sin(headingChange.Rads);
			double c = Math.Cos(headingChange.Rads);

			m_TranslationIncrements.Clear();
			System.Drawing.PointF[] featurePointPair = new System.Drawing.PointF[2];

			List<TrackedFeature> trackedFeatures = m_VisualOdometer.TrackedFeatures;
			m_GroundFeatures.Clear();
			for (int i = 0; i < trackedFeatures.Count; i++)
			{
				TrackedFeature trackedFeature = trackedFeatures[i];
				if (trackedFeature.Count < 2)
				{
					continue;
				}

				// previous and current feature points need to be in the ground region
				if (!(trackedFeature[-1].Y > m_VisualOdometer.GroundRegionTop && trackedFeature[0].Y > m_VisualOdometer.GroundRegionTop))
				{
					continue;
				}

				featurePointPair[0] = trackedFeature[-1]; // previous feature location
				featurePointPair[1] = trackedFeature[0];  // current featue location


				ProjectOnFloor(featurePointPair);
	

				Point rotationCorrectedEndPoint = new Point(
					c * featurePointPair[1].X - s * featurePointPair[1].Y,
					s * featurePointPair[1].X + c * featurePointPair[1].Y);

				Point translationIncrement = new Point(
					featurePointPair[0].X - rotationCorrectedEndPoint.X,
					featurePointPair[0].Y - rotationCorrectedEndPoint.Y);


				m_TranslationIncrements.Add(translationIncrement);
				m_GroundFeatures.Add(trackedFeature);
			}
		}

		private void DeterminMostLikelyTranslationVector()
		{
			Point mostLikelyTranslation = new Point();
			int maxVotes = 0;

	
			const int maxPicks = 40;
			int randomPicksCount = m_TranslationIncrements.Count < maxPicks ? m_TranslationIncrements.Count : maxPicks;
			for (int i = 0; i < randomPicksCount; i++)
			{
				m_ScratchPadUsedGroundFeatures.Clear();
				int index = m_Random.Next(m_TranslationIncrements.Count);
				Point translationVector = m_TranslationIncrements[index];

				double netX = 0, netY = 0;
				int votes = 0;

				for (int j = 0; j < m_TranslationIncrements.Count; j++)
				{
					if (i == j)
					{
						continue;
					}

					double dx = m_TranslationIncrements[j].X - translationVector.X;
					double dy = m_TranslationIncrements[j].Y - translationVector.Y;

					if ((dx * dx + dy * dy) < 0.5)
					{
						votes++;
						netX += dx;
						netY += dy;
						m_ScratchPadUsedGroundFeatures.Add(m_GroundFeatures[j]);
					}
				}

				if (votes > maxVotes)
				{
					maxVotes = votes;
					mostLikelyTranslation = new Point(
						translationVector.X + netX / votes,
						translationVector.Y + netY / votes);

					List<TrackedFeature> temp = m_UsedGroundFeatures;
					m_UsedGroundFeatures = m_ScratchPadUsedGroundFeatures;
					m_ScratchPadUsedGroundFeatures = temp;
				}
			}

			m_CurrentLocationChange = mostLikelyTranslation;
		}

		

		public System.Drawing.PointF RemoveRotationEffect(Angle headingChange, System.Drawing.PointF point)
		{
			float s = (float)Math.Sin(headingChange.Rads);
			float c = (float)Math.Cos(headingChange.Rads);

			return new System.Drawing.PointF(
				c * point.X - s * point.Y,
				s * point.X + c * point.Y);
		}

		private void ProjectOnFloor(System.Drawing.PointF[] featurePoints)
		{
			m_GroundProjectionTransformation.ProjectPoints(featurePoints);
		}

		public List<TrackedFeature> UsedGroundFeatures
		{
			get { return m_UsedGroundFeatures; }
		}
	}
}
