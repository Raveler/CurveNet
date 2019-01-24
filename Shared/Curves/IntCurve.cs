using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class IntCurve : ValueCurve<int, IntKeyframeValue>
	{

		public IntCurve(int initialValue, InterpolationType interpolationType = InterpolationType.Linear) : base(new IntKeyframeValue(initialValue), interpolationType)
		{
		}

		protected override int GetValue(IntKeyframeValue value)
		{
			return value.Value;
		}
	}
}
