using System;

namespace Bombardel.CurveNet.Shared.ServerMessages
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
