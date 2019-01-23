using Bombardel.CurveNet.Server.WebSockets;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Server.Sessions
{

	public class Room
	{
		private List<ConnectionHandler> _members = new List<ConnectionHandler>();

		private string _name;


		public Room(string name) {
			_name = name;
		}

		public void AddClient(ConnectionHandler client)
		{
			if (_members.Any(p => p.Id == client.Id)) throw new ProtocolErrorException(ProtocolError.AlreadyInRoom);

			// send to all players in the room that we have a new member
			for (int i = 0; i < _members.Count; ++i)
			{
				_members[i].Connection.SendNewClient(GetRoomEventData(client));
			}

			// actually add the client
			_members.Add(client);

			// send the full room data to the new member
			client.Connection.SendRoomData(GetRoomData());
		}

		public void RemoveClient(ConnectionHandler client)
		{
			if (!_members.Any(p => p.Id == client.Id)) throw new ProtocolErrorException(ProtocolError.NotInRoom);

			// the client himself must receive this message as well, so we send it first
			for (int i = 0; i < _members.Count; ++i)
			{
				_members[i].Connection.SendRemoveClient(GetRoomEventData(client));
			}

			// actually remove the client
			_members.RemoveAll(p => p.Id == client.Id);
		}

		private RoomEventData GetRoomEventData(ConnectionHandler client)
		{
			return new RoomEventData()
			{
				roomName = _name,
				client = GetClientData(client),
			};
		}

		private ClientData GetClientData(ConnectionHandler client)
		{
			return new Shared.Client.ClientData()
			{
				id = client.Id,
			};
		}

		public RoomData GetRoomData()
		{
			RoomData roomData = new RoomData();
			roomData.roomName = _name;
			for (int i = 0; i < _members.Count; ++i)
			{
				roomData.clients.Add(GetClientData(_members[i]));
			}
			return roomData;
		}
	}
}
