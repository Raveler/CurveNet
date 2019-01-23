using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombardel.CurveNet.TronClient
{
	class World
	{
		public int Width => _width;
		public int Height => _height;



		private readonly Brush[] shipBrushes = new Brush[] { Brushes.IndianRed, Brushes.DarkSeaGreen, Brushes.HotPink, Brushes.CornflowerBlue };

		private int _width, _height;

		private WorldForm _form;

		private List<Ship> _ships = new List<Ship>();
		private List<Player> _players = new List<Player>();
		private List<Powerup> _powerups = new List<Powerup>();

		private Random _rand;

		private Dictionary<Keys, Action> _keyBindings = new Dictionary<Keys, Action>();

		private float _powerupTimer = 0.0f;
		private float _powerupInterval = 5.0f;


		public World(int width, int height)
		{
			_width = width;
			_height = height;
			_rand = new Random(5);

			_form = new WorldForm(_width, _height);

			int nShips = 4;
			for (int i = 0; i < nShips; ++i)
			{
				Ship ship = new Ship(this, new System.Drawing.PointF(_rand.Next(0, _width), _rand.Next(0, _height)), shipBrushes[i]);
				_ships.Add(ship);
			}

			int nLocalPlayers = 2;
			for (int i = 0; i < nLocalPlayers; ++i)
			{
				Player player = new Player(this, _ships[i], i);
				_players.Add(player);
			}

			Powerup powerup = new Powerup(this, new PointF(50, 50));
			_powerups.Add(powerup);

			_form.KeyDown += OnKeyDown;
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			Keys key = e.KeyCode;
			Console.WriteLine("Received key " + key);
			if (_keyBindings.ContainsKey(key)) _keyBindings[key]();
		}

		public void BindKey(Keys key, Action action)
		{
			Console.WriteLine("Bind key " + key);
			_keyBindings[key] = action;
		}

		public void Update(float dt)
		{
			_powerupTimer -= dt;
			if (_powerupTimer <= 0.0f)
			{
				_powerupTimer += _powerupInterval;
				bool done = false;
				int attempt = 0;
				while (!done && attempt++ < 50)
				{
					PointF loc = new PointF(_rand.Next(0, _width), _rand.Next(0, _height));
					bool okLoc = true;
					for (int i = 0; i < _ships.Count; ++i)
					{
						if (_ships[i].Distance(loc) < _ships[i].Radius + 30.0f) okLoc = false;
					}

					if (okLoc)
					{
						Powerup powerup = new Powerup(this, loc);
						_powerups.Add(powerup);
						done = true;
					}
				}
			}

			for (int i = 0; i < _ships.Count; ++i)
			{
				if (_ships[i].Update(dt))
				{
					_ships.RemoveAt(i);
					--i;
				}
			}

			for (int i = 0; i < _powerups.Count; ++i)
			{
				if (_powerups[i].Update(dt, _ships))
				{
					_powerups.RemoveAt(i);
					--i;
				}
			}

			// see if there is a collision between ships
			for (int i = 0; i < _ships.Count; ++i)
			{
				for (int j = 0; j < i; ++j)
				{
					Ship ship1 = _ships[i];
					Ship ship2 = _ships[j];

					if (ship1.Distance(ship2) < ship1.Radius + ship2.Radius)
					{
						if (ship1.Radius != ship2.Radius)
						{
							if (ship1.Radius < ship2.Radius) ship1.Kill();
							else ship2.Kill();
						}
						else
						{
							ship1.Revert();
							ship2.Revert();
						}
					}
				}
			}
		}

		public void Render()
		{
			_form.Graphics.Clear(Color.FromArgb(255, 15, 15, 20));

			foreach (Ship ship in _ships)
			{
				ship.Render(_form.Graphics);
			}
			foreach (Powerup powerup in _powerups)
			{
				powerup.Render(_form.Graphics);
			}

			_form.Invalidate();
		}
	}
}
