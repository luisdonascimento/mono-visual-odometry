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
using VisualOdometry.Utilities;

namespace VisualOdometry
{
	public class FramesCounter
	{
		private CircularBuffer<DateTime> m_FrameTimesBuffer = new CircularBuffer<DateTime>(10); 
		private double m_TicksSecond = (double)TimeSpan.FromSeconds(1).Ticks;

		internal FramesCounter()
		{
			
			this.FramesPerSecond = 0;
		}

		internal void Update()
		{
			m_FrameTimesBuffer.Add(DateTime.UtcNow);


		
				this.FramesPerSecond = m_FrameTimesBuffer.Count * m_TicksSecond / (double)(m_FrameTimesBuffer[m_FrameTimesBuffer.Count - 1].Ticks - m_FrameTimesBuffer[0].Ticks);
		
		}


		public double FramesPerSecond { get; private set; }
	}
}
