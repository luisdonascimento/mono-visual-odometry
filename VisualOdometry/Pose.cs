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
using System.Windows;

namespace VisualOdometry
{
	public class Pose
	{
		public Pose()
		{
			this.Location = new Point();
			this.Heading = Angle.FromRads(0);
		}

		public Pose(Point location, Angle heading)
		{
			this.Location = location;
			this.Heading = heading;
		}

		public Pose(double x, double y, Angle heading)
		{
			this.Location = new Point(x, y);
			this.Heading = heading;
		}

		public Pose(Pose pose)
		{
			this.Location = pose.Location;
			this.Heading = pose.Heading; ;
		}

		public Point Location { get; set; }

		public double X
		{
			get { return this.Location.X; }
			set { this.Location = new Point(value, this.Y); }
		}

		public double Y
		{
			get { return this.Location.Y; }
			set { this.Location = new Point(this.X, value); }
		}

		private Angle m_Heading;
		public Angle Heading
		{
			get { return m_Heading; }
			set { m_Heading = value; }
		}

		public override string ToString()
		{
			return String.Format("x={0}, y={1}, heading={2}", this.X, this.Y, this.Heading.Degrees);
		}
	}
}
