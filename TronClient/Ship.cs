using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombardel.CurveNet.TronClient
{
	class Ship : ILocatable
	{
		public float Radius => _radius;
		public PointF Loc => _loc;

		private World _world;

		private PointF _loc;
		private PointF _dir;
		private float _speed = 180.0f;

		private Brush _brush;

		private float _radius = 25.0f;

		private bool _dead = false;


		public Ship(World world, PointF loc, Brush brush)
		{
			_world = world;
			_brush = brush;

			_loc = loc;;
			_dir = new PointF(0, 0);
		}

		public void SetDir(Point dir)
		{
			_dir = new PointF(dir.X, dir.Y);
		}

		public void Grow()
		{
			_radius += 5.0f;
		}

		public void Revert()
		{
			_dir = new PointF(-_dir.X, -_dir.Y);
		}

		public bool Update(float dt)
		{
			_loc += new SizeF(_dir.X * _speed * dt, _dir.Y * _speed * dt);

			if (_loc.X < 0) _loc.X = 0;
			if (_loc.Y < 0) _loc.Y = 0;
			if (_loc.X > _world.Width) _loc.X = _world.Width;
			if (_loc.Y > _world.Height) _loc.Y= _world.Height;

			return _dead;
		}

		public void Kill()
		{
			_dead = true;
		}

		public void Render(Graphics graphics)
		{
			float r = Radius;
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			graphics.FillEllipse(_brush, new RectangleF(_loc.X-r, _loc.Y-r, r*2, r*2));
		}
	}
}
