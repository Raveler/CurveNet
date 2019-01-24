using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public abstract class ValueCurve<V, T> : ICurve where T : IKeyframeValue<T>, new()
	{
		public V Value
		{
			get
			{
				return GetValue(_value);
			}
		}

		private T _value;

		private ValueCurveBinder<T> _binder;
		private IInterpolator<T> _interpolator;


		public ValueCurve(T initialValue, InterpolationType interpolationType = InterpolationType.Linear)
		{
			_value = initialValue;
			_interpolator = interpolationType.GetInterpolator<T>();
		}

		public void MoveTo(float targetTime, V value)
		{
			if (_binder == null) throw new InvalidOperationException("Object is not yet registered at the networking system, so curves are not available for use yet.");

			T keyframeValue = GetKeyframeValue(value);
			_binder.MoveTo(targetTime, keyframeValue);
		}
		
		public void RegisterLocal(CurveStore curveStore)
		{
			_binder = new ValueCurveBinder<T>(curveStore, _value, SetNewValue, _interpolator);
			_binder.RegisterLocal();
		}

		public void RegisterRemote(CurveStore curveStore, Id remoteId)
		{
			_binder = new ValueCurveBinder<T>(curveStore, _value, SetNewValue, _interpolator);
			_binder.RegisterRemote(remoteId);
		}

		protected void SetNewValue(T value) {
			_value = value;
		}

		protected abstract V GetValue(T value);

		protected abstract T GetKeyframeValue(V value);

		public abstract void ApplyConfig(CurveConfig config);

		public abstract CurveConfig GetConfig();
	}
}
