using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class KeyframeData
	{
		public byte[] bytes;

		public KeyframeData(IKeyframe keyframe)
		{
			bytes = keyframe.Serialize();
		}
	}
}
