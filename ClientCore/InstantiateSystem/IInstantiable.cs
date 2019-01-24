using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombardel.CurveNet.Client.Instantiate
{
    public interface IInstantiable
	{

		IEnumerable<ICurve> GetSynchedCurves();

		void Init();
    }
}
