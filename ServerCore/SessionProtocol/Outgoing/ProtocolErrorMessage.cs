using Bombardel.CurveNet.Server.WebSockets;

namespace Bombardel.CurveNet.Server.Sessions.Outgoing
{
	public class ProtocolErrorMessage
	{
		public ProtocolError error;

		public string errorDescription
		{
			get
			{
				return error.GetDescription();
			}
		}
	}
}
