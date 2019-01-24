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
		private IInterpolator<T> _interpolator;
		private T _initialValue;


		public ValueCurveBinder(CurveStore curveStore, T initialValue, Action<T> ValueSetter, IInterpolator<T> interpolator)
		{
			_curveStore = curveStore;
			_valueSetter = ValueSetter;
			_interpolator = interpolator;
			_initialValue = initialValue;
		}

		public void RegisterLocal()
		{
			_id = _curveStore.RegisterLocalCurve(this, _initialValue, _interpolator);
		}

		public void RegisterRemote(Id id)
		{
			_id = id;
			_curveStore.RegisterRemoteCurve(id, this, _initialValue, _interpolator);
		}

		public void MoveTo(float time, T value)
		{
			_curveStore.SubmitKeyframeToServer(_id, new KeyframeData(new Keyframe<T>(time, value)));
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
