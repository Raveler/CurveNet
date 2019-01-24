using Bombardel.CurveNet.Server.Sessions;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.ClientMessages;
using Bombardel.CurveNet.Shared.Curves;
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

			NewObjectConfig config = new NewObjectConfig();
			config.curves.Add(new StringCurveConfig(CurveType.Step, "Walter"));
			config.curves.Add(new IntCurveConfig(CurveType.Step, 20));
			config.curves.Add(new FloatCurveConfig(CurveType.Linear, 5.0f));
			connection.Server.CreateObject(config);
		}

		public void OnRoomData(RoomData roomData)
		{
			Console.WriteLine("Roomdata for " + roomData.roomName + ":");
			for (int i = 0; i < roomData.clients.Count; ++i)
			{
				Console.WriteLine("Client #" + i + ": " + roomData.clients[i].id);
			}
		}

		public void OnNewClient(RoomEventData newClient)
		{
			Console.WriteLine("Player " + newClient.client.id + " joined room " + newClient.roomName + "!");
		}

		public void OnClientRemoved(RoomEventData deadClient)
		{
			Console.WriteLine("Player " + deadClient.client.id + " left room " + deadClient.roomName + "!");
		}

		public void OnProtocolError(ProtocolError error)
		{
			Console.WriteLine("PROTOCOL ERROR: " + error + " ( " + error.GetDescription() + ")");
		}

		public void OnObjectCreated(ObjectData data)
		{
			Console.WriteLine("CREATED A NEW OBJECT!!!!!!!!!");
			Console.WriteLine("Id: " + data.id);
			Console.WriteLine("Owner: " + data.owner);
			Console.WriteLine("Curves: " + data.curves.Count);


		}
	}
}
