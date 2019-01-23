using Bombardel.CurveNet.Server.Sessions;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.ClientMessages;
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
	class Game : IClient
	{
		private ClientDeserializer _deserializer;


		public Game(ServerConnection connection)
		{
			// we attach ourself to the deserializer
			_deserializer = new ClientDeserializer(this);
			connection.OnMessage += (byte[] bytes) =>
			{
				_deserializer.Deserialize(bytes);
			};

			// we join a room
			connection.Server.JoinRoom("TEST");
			connection.Server.JoinRoom("TEST");
			connection.Server.LeaveRoom("TEST");
			connection.Server.LeaveRoom("TEST");
			connection.Server.JoinRoom("TEST2");
		}

		public void SendRoomData(RoomData roomData)
		{
			Console.WriteLine("Roomdata for " + roomData.roomName + ":");
			for (int i = 0; i < roomData.clients.Count; ++i)
			{
				Console.WriteLine("Client #" + i + ": " + roomData.clients[i].id);
			}
		}

		public void SendNewClient(RoomEventData newClient)
		{
			Console.WriteLine("Player " + newClient.client.id + " joined room " + newClient.roomName + "!");
		}

		public void SendRemoveClient(RoomEventData deadClient)
		{
			Console.WriteLine("Player " + deadClient.client.id + " left room " + deadClient.roomName + "!");
		}

		public void SendProtocolError(ProtocolError error)
		{
			Console.WriteLine("PROTOCOL ERROR: " + error + " ( " + error.GetDescription() + ")");
		}
	}
}
