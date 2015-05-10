/*
 * Desenvolvido Por Luís do Nascimento Adães
 * Códigos Adaptados de openslam.org
 * Conexão com LEGO por Edgard Quirino e Mário Almeida
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisualOdometry.UI
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
