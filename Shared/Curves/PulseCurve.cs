using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public delegate void OnPulseDelegate();

	public class PulseCurve
	{
		private OnPulseDelegate _onPulse;

		private PulseCurveBinder _binder = null;


		public PulseCurve(OnPulseDelegate OnPulse)
		{
			_onPulse = OnPulse;
		}

		// we pulse ourself - we MUST be the owner of this object!
		public void Pulse()
		{
			_onPulse();
			if (_binder == null) throw new InvalidOperationException("Object is not yet registered at the networking system, so curves are not available for use yet");

			// TODO send to server
			_binder.SendPulseToServer();
		}

		public void Register(CurveStore curveStore, PulseCurveData data)
		{
			// get the id in the store that corresponds to this curve
			Id id = data.id;

			// create a binder that ties this curve to the corresponding data structure in the store
			_binder = new PulseCurveBinder(curveStore, id, OnNetworkPulseReceived);
		}

		// we received a pulse instruction from the server, so the owner previously triggered a pulse
		private void OnNetworkPulseReceived()
		{
			_onPulse();
		}
	}
}
