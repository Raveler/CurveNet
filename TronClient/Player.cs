using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombardel.CurveNet.TronClient
{
	class Player
	{
		private readonly KeyProfile[] keyProfiles = new KeyProfile[] {
			new KeyProfile(Keys.Left, Keys.Right, Keys.Up, Keys.Down),
			new KeyProfile(Keys.Q, Keys.D, Keys.Z, Keys.S),
			new KeyProfile(Keys.NumPad4, Keys.NumPad6, Keys.NumPad8, Keys.NumPad5),
			new KeyProfile(Keys.K, Keys.M, Keys.O, Keys.L),
		};


		private Ship _ship;


		public Player(World world, Ship ship, int playerNumber)
		{
			keyProfiles[playerNumber].Register(world, this);

			_ship = ship;
		}

		public void Move(Point dir)
		{
			_ship.SetDir(dir);
		}
	}
}
