using Bombardel.CurveNet.Server.Sessions.Outgoing;
using Bombardel.CurveNet.Server.WebSockets;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Server.Sessions
{

	public class Room
	{
		private List<Player> _players = new List<Player>();


		public void AddPlayer(string id)
		{
			if (_players.Any(p => p.Id == id)) throw new ProtocolErrorException(ProtocolError.AlreadyInRoom);
			Player player = new Player(id);
			_players.Add(player);
		}

		public void RemovePlayer(string id)
		{
			if (!_players.Any(p => p.Id == id)) throw new ProtocolErrorException(ProtocolError.NotInRoom);
			_players.RemoveAll(p => p.Id == id);
			// TODO transfer ownership???
		}
	}
}
