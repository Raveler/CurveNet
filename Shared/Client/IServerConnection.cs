using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{

	public interface IServerConnection
	{
		void Send(byte[] bytes);
	}
}
