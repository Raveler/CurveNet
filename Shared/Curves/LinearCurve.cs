using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class LinearCurve<T>
	{
		public T Value
		{
			get
			{
				return GetValue();
			}
		}

		private PulseCurveBinder _binder = null;


		public LinearCurve(T initialValue)
		{
		}

		public void MoveTo(float duration, T targetValue)
		{

		}

		public void Register(CurveStore curveStore, PulseCurveData data)
		{
			// get the id in the store that corresponds to this curve
			Id id = data.id;

			// create a binder that ties this curve to the corresponding data structure in the store
			_binder = new PulseCurveBinder(curveStore, id, OnNetworkPulseReceived);
		}

		private T GetValue()
		{

		}
	}
}
