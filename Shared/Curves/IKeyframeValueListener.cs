using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public interface IKeyframeValueListener<T> where T : IKeyframeValue<T>
	{
		void SetValue(IKeyframeValue<T> value);

		void SetKeyframePassed(IKeyframeValue<T> value);
	}
}
