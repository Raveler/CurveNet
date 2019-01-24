using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class ValueCurveBinder<T> : IKeyframeValueListener<T> where T : IKeyframeValue<T>, new()
	{
		private CurveStore _curveStore;

		private Id _id;

		private Action<T> _valueSetter;


		public ValueCurveBinder(CurveStore curveStore, Id curveId, T initialValue, Action<T> ValueSetter, IInterpolator<T> interpolator)
		{
			_id = curveId;
			_curveStore = curveStore;
			_valueSetter = ValueSetter;
			curveStore.RegisterCurve(curveId, this, initialValue, interpolator);
		}

		public void SetKeyframePassed(T value)
		{
			// nothing happens here
		}

		public void SetValue(T value)
		{
			_valueSetter(value);
		}
	}
}
