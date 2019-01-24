using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombardel.CurveNet.Client.Instantiate
{
    public interface IInstantiator
	{
		void Spawn(Id objectId, Id ownerId, IEnumerable<CurveConfig> curves);
	}
}
