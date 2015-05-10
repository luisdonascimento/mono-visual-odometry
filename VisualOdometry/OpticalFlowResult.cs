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
	public class OpticalFlowResult
	{
		public PointF[] TrackedFeaturePoints { get; set; }
		public byte[] TrackingStatusIndicators { get; set; }
		public float[] TrackingErrors { get; set; }
	}
}
