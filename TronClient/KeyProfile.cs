using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombardel.CurveNet.TronClient
{
	class KeyProfile
	{
		private Keys _left, _right, _up, _down;


		public KeyProfile(Keys left, Keys right, Keys up, Keys down)
		{
			_left = left;
			_right = right;
			_up = up;
			_down = down;
		}

		public void Register(World world, Player player)
		{
			world.BindKey(_left, () => player.Move(new Point(-1, 0)));
			world.BindKey(_right, () => player.Move(new Point(1, 0)));
			world.BindKey(_up, () => player.Move(new Point(0, -1)));
			world.BindKey(_down, () => player.Move(new Point(0, 1)));
		}
	}
}
