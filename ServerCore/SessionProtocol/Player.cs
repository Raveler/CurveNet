using Bombardel.CurveNet.Server.WebSockets;

namespace Bombardel.CurveNet.Server.Sessions
{

	public class Player
	{
		public string Id => _id;


		private string _id;


		public Player(string id)
		{
			_id = id;
		}
	}
}
