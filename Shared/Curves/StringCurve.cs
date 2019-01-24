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
		
		public override void ApplyConfig(CurveConfig config)
		{
			SetNewValue(new StringKeyframeValue(((StringCurveConfig)config).defaultValue));
		}

		public override CurveConfig GetConfig()
		{
			return new StringCurveConfig(Value);
		}

		protected override StringKeyframeValue GetKeyframeValue(string value)
		{
			return new StringKeyframeValue(value);
		}
	}
}
