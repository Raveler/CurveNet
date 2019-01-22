using Bombardel.CurveNet.Server.Sessions.Incoming;
using Bombardel.CurveNet.Server.WebSockets;
using System;
using System.Collections.Generic;

namespace Bombardel.CurveNet.Server.Sessions
{

	public class GameManager : IConnectionHandlerFactory
	{
		private Dictionary<string, ConnectionHandler> _connections = new Dictionary<string, ConnectionHandler>();

		private Dictionary<string, Room> _rooms = new Dictionary<string, Room>();


		public IConnectionHandler CreateHandler(Connection connection)
		{
			string guid = CreateGUID();
			ConnectionHandler conn = new ConnectionHandler(this, connection, guid);
			_connections.Add(guid, conn);
			return conn;
		}

		private string CreateGUID() {
			string guid = Guid.NewGuid().ToString();
			while (_connections.ContainsKey(guid)) guid = Guid.NewGuid().ToString();
			return guid;
		}

		public void CloseConnection(string connection)
		{

		}

		public void JoinRoom(string connection, JoinRoomEvent evt)
		{
			string roomName = evt.name;
			Room room = GetRoom(roomName);
			room.AddPlayer(connection);
		}

		private Room GetRoom(string roomName)
		{
			if (!_rooms.ContainsKey(roomName))
			{
				_rooms.Add(roomName, new Room());
			}

			Room room = _rooms[roomName];
			return room;
		}

		public void LeaveRoom(string connection, LeaveRoomEvent evt)
		{

		}


	}
}
