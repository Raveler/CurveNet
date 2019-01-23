using Bombardel.CurveNet.Server.WebSockets;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.ClientMessages;
using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Objects;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using System;
using System.Collections.Generic;

namespace Bombardel.CurveNet.Server.Sessions
{

	public class GameManager : IConnectionHandlerFactory
	{
		private Dictionary<Id, ConnectionHandler> _connections = new Dictionary<Id, ConnectionHandler>();

		private Dictionary<string, Room> _rooms = new Dictionary<string, Room>();

		private IdStore _clientIdStore = new IdStore();

		private CurveStore _curveStore;
		private ObjectStore _objectStore;


		public GameManager()
		{
			_curveStore = new CurveStore();
			_objectStore = new ObjectStore(_curveStore);
		}

		public IConnectionHandler CreateHandler(Connection connection)
		{
			ConnectionHandler conn = new ConnectionHandler(this, connection, _clientIdStore.GenerateId());
			_connections.Add(conn.Id, conn);
			return conn;
		}
		
		public void CloseConnection(string connection)
		{

		}

		public void JoinRoom(ConnectionHandler client, string roomName)
		{
			Room room = GetRoom(roomName);
			room.AddClient(client);
		}

		private Room GetRoom(string roomName)
		{
			if (!_rooms.ContainsKey(roomName))
			{
				_rooms.Add(roomName, new Room(roomName));
			}

			Room room = _rooms[roomName];
			return room;
		}

		public void LeaveRoom(ConnectionHandler client, string roomName)
		{
			Room room = GetRoom(roomName);
			room.RemoveClient(client);
		}

		public void CreateObject(ConnectionHandler client, NewObjectConfig config)
		{
			ObjectData data = _objectStore.CreateObject(config, client.Id);
			foreach (ConnectionHandler otherClient in _connections.Values)
			{
				otherClient.Connection.OnObjectCreated(data);
			}
		}

		public void ListRooms(ConnectionHandler client)
		{
			throw new NotImplementedException();
		}
	}
}
