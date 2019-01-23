using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombardel.CurveNet.TronClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new WorldForm(1024, 768));

			World world = new World(1024, 768);

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (true)
			{
				world.Update((float)stopwatch.Elapsed.TotalSeconds);
				stopwatch.Restart();
				world.Render();
				Application.DoEvents(); // default message pump
			}
		}
	}
}
