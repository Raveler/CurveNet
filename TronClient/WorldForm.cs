using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombardel.CurveNet.TronClient
{
	class WorldForm : Form
	{
		public Graphics Graphics => _graphics;


		private Bitmap _bitmap;

		private Graphics _graphics;


		public WorldForm(int width, int height) : base()
		{
			Text = "CurveNet Tron";
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			SetBounds(0, 0, width, height);
			ClientSize = new System.Drawing.Size(width, height);
			Location = new System.Drawing.Point(0, 0);
			Show();

			DoubleBuffered = true;

			_bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

			_graphics = Graphics.FromImage(_bitmap);
			_graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			_graphics.Clear(Color.Green);
			_graphics.FillEllipse(Brushes.Yellow, 0, 0, _bitmap.Width, _bitmap.Height);

			Console.WriteLine("HUMM...");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.DrawImage(_bitmap, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), GraphicsUnit.Pixel);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (_bitmap != null)
			{
				_graphics.Dispose();
				_bitmap.Dispose();
			}

			base.OnClosing(e);
		}
	}
}
