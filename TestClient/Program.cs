﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			WebSocket socket = null;
			bool connected = false;
			while (!connected)
			{
				Console.Write("Try to connect to websocket...");
				connected = TryConnect(out socket);
			}

			ServerConnection connection = new ServerConnection(socket);

			Game game = new Game(connection);

			while (!connection.IsClosed())
			{
				connection.Update();
				Thread.Sleep(1);
			}

			Console.ReadKey(true);
		}

		private static bool TryConnect(out WebSocket socket)
		{
			try
			{
				WebSocket ws = new WebSocket("ws://localhost:54008/ws");

				ws.OnError += Ws_OnError;
				ws.Connect();

				if (!ws.IsAlive) throw new Exception("Failed to connect to socket");
				Console.WriteLine("Connected to websocket!");
				socket = ws;
				return true;


			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to connect!");
				Console.WriteLine(e.Message);
				socket = null;
				return false;
			}
		}

		private static void Ws_OnError(object sender, ErrorEventArgs e)
		{
			Console.WriteLine("ERROR FOUND!!!!!!!");
			throw e.Exception;
		}
	}
}
