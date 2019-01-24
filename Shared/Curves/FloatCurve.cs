using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class FloatCurve : ValueCurve<float, FloatKeyframeValue>
	{

		public FloatCurve(float initialValue, InterpolationType interpolationType = InterpolationType.Linear) : base(new FloatKeyframeValue(initialValue), interpolationType)
		{
		}

		protected override float GetValue(FloatKeyframeValue value)
		{
			return value.Value;
		}
	}
}
