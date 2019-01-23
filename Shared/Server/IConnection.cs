using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Server
{

	public interface IConnection
	{
		void Send(byte[] bytes);
	}
}
