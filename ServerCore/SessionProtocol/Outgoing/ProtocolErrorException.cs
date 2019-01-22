using Bombardel.CurveNet.Server.WebSockets;
using System;

namespace Bombardel.CurveNet.Server.Sessions.Outgoing
{
	public class ProtocolErrorException : Exception
	{
		public ProtocolError error;

		public ProtocolErrorException(ProtocolError error)
		{
			this.error = error;
		}
	}
}
