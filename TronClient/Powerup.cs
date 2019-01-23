using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombardel.CurveNet.TronClient
{
	class Powerup : ILocatable
	{
		public PointF Loc => _loc;


		private float _radius = 10.0f;

		private World _world;

		private PointF _loc;


		public Powerup(World world, PointF loc)
		{
			_world = world;
			_loc = loc;
		}

		public bool Update(float dt, List<Ship> ships)
		{
			// pickup!
			foreach (Ship ship in ships)
			{
				float d = this.Distance(ship);
				if (d < _radius + ship.Radius)
				{
					ship.Grow();
					return true;
				}
			}

			return false;
		}

		public void Render(Graphics graphics)
		{
			float r = _radius;
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			
			RectangleF rect = new RectangleF(_loc.X-r, _loc.Y-r, r*2, r*2);
			graphics.FillEllipse(Brushes.Yellow, rect);
			graphics.DrawEllipse(new Pen(Color.Red, 4.0f), rect);
		}
	}
}
