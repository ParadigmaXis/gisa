using System;
using System.Windows.Forms;

namespace TestUI
{
	/// <summary>
	/// Summary description for MainX.
	/// </summary>
	public class MainX
	{
		public MainX()
		{			
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
		//	Application.Run(new Form1());
			Application.Run(new MainForm());
		}
	}
}
