using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombardel.CurveNet.Client
{
    public interface IClientListener
	{
		void OnError(ProtocolError error);


    }
}
