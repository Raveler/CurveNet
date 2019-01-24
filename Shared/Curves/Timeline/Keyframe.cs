using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class Keyframe
	{
		public IKeyframeValue Value { get; set; }
		public float Time { get; set; }


		public Keyframe(float time, IKeyframeValue value)
		{
			if (time < 0.0f) throw new ArgumentOutOfRangeException("Time must be a positive value");
			Time = time;
			Value = value;
		}
	}
}
