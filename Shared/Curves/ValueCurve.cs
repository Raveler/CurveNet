using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public abstract class ValueCurve<V, T> where T : IKeyframeValue<T>, new()
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
		
		public void Register(CurveStore curveStore, PulseCurveData data)
		{
			// get the id in the store that corresponds to this curve
			Id id = data.id;

			// create a binder that ties this curve to the corresponding data structure in the store
			_binder = new ValueCurveBinder<T>(curveStore, id, _value, SetNewValue, _interpolator);
		}

		private void SetNewValue(T value) {
			_value = value;
		}

		protected abstract V GetValue(T value);
	}
}
