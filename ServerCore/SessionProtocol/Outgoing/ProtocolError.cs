using Bombardel.CurveNet.Server.WebSockets;

namespace Bombardel.CurveNet.Server.Sessions.Outgoing
{
	public enum ProtocolError
	{
		InvalidMessageType,
		AlreadyInRoom,
		NotInRoom,
	}

	public static class ProtocolErrorExtensions
	{
		public static string GetDescription(this ProtocolError error)
		{
			switch (error)
			{
				case ProtocolError.InvalidMessageType:
					return "That message type does not exist";


				case ProtocolError.AlreadyInRoom:
					return "You are already in that room";

				case ProtocolError.NotInRoom:
					return "You are not in that room";


				default:
					return "";
			}
		}
	}

}
