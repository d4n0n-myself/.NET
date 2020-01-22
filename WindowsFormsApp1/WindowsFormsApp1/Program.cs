using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	static class Program
	{
		private static EventHandler eh;

		static void HandleUnhandledException(Exception e)
		{
			// show report sender and close the app or whatever
		}
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			AppDomain.CurrentDomain.UnhandledException +=
				(sender, args) => HandleUnhandledException(args.ExceptionObject as Exception);
			Application.ThreadException +=
				(sender, args) => HandleUnhandledException(args.Exception);

			//eh.Invoke(null, new EventArgs());

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}