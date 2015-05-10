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

namespace VisualOdometry
{
	public class PoseEventArgs : EventArgs
	{
		public PoseEventArgs(Pose pose)
		{
			this.Pose = pose;
		}

		public Pose Pose { get; private set; }
	}
}
