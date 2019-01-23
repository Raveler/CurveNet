using Bombardel.CurveNet.Server.Sessions;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.ClientMessages;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using static Bombardel.CurveNet.Server.Sessions.ObjectBasedSerializer;

namespace TestClient
{
	class ServerConnection : IServerConnection
	{
		public IServer Server => _serializer;

		public delegate void OnMessageDelegate(byte[] msg);

		public OnMessageDelegate OnMessage;


		private WebSocket _socket;

		private ServerSerializer _serializer;


		public ServerConnection(WebSocket socket)
		{
			_socket = socket;
			_serializer = new ServerSerializer(this);

			_socket.OnMessage += _socket_OnMessage;

		}

		private void _socket_OnMessage(object sender, MessageEventArgs e)
		{
			if (OnMessage != null) OnMessage(e.RawData);
		}

		private void OnError(ProtocolErrorMessage err)
		{
			Console.WriteLine("PROTOCOL ERROR: " + err.error + " ( " + err.errorDescription + ")");
		}

		private void OnPlayerJoined(PlayerJoinedEvent evt)
		{
			Console.WriteLine("Player " + evt.id + " joined!");
		}

		private void OnPlayerLeft(PlayerLeftEvent evt)
		{
			Console.WriteLine("Player " + evt.id + " left!");
		}

		public void Send(byte[] bytes)
		{
			_socket.Send(bytes);
		}
		
		public bool IsClosed()
		{
			return false;
		}

		public void Update()
		{

		}
	}
}
