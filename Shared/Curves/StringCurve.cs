using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class StringCurve : ValueCurve<string, StringKeyframeValue>
	{

		public StringCurve(string initialValue) : base(new StringKeyframeValue(initialValue), InterpolationType.Step)
		{
		}

		protected override string GetValue(StringKeyframeValue value)
		{
			return value.Value;
		}
	}
}
