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

		public override void ApplyConfig(CurveConfig config)
		{
			SetNewValue(new FloatKeyframeValue(((FloatCurveConfig)config).defaultValue));
		}

		public override CurveConfig GetConfig()
		{
			return new FloatCurveConfig(Value);
		}

		protected override FloatKeyframeValue GetKeyframeValue(float value)
		{
			return new FloatKeyframeValue(value);
		}
	}
}
