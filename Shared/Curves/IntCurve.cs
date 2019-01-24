using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{

	public class IntCurve : ICurve
	{
		int Value
		{
			get
			{
				return GetValue();
			}
		}
		
	}
}
