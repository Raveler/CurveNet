using Bombardel.CurveNet.Server.WebSockets;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Server.Sessions
{

	public class RoomStore
	{
		private Dictionary<string, Room> _rooms = new Dictionary<string, Room>();
	}
}
