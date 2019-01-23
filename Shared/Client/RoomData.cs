using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{

	public class RoomData
	{
		public string roomName;

		public List<ClientData> clients = new List<ClientData>();
	}
}
