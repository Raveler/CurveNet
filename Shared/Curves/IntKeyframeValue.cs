using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class IntKeyframeValue : IKeyframeValue<IntKeyframeValue>
	{
		private int _value;

		public IntKeyframeValue(int value)
		{
			_value = value;
		}

		public IKeyframeValue InterpolateTo(IKeyframeValue next, float t)
		{
			return _value + (next
		}
	}
}
