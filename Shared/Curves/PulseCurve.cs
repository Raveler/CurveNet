using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public delegate void OnPulseDelegate();

	public class PulseCurve : ICurve
	{
		private OnPulseDelegate _onPulse;

		private PulseCurveBinder _binder = null;


		public PulseCurve(OnPulseDelegate OnPulse)
		{
			_onPulse = OnPulse;
		}

		public void ApplyConfig(CurveConfig config)
		{
			// does nothing
		}

		public CurveConfig GetConfig()
		{
			return new IntCurveConfig(0);
		}

		// we pulse ourself - we MUST be the owner of this object!
		public void Pulse()
		{
			if (_binder == null) throw new InvalidOperationException("Object is not yet registered at the networking system, so curves are not available for use yet.");

			// TODO send to server
			_binder.SendPulseToServer();
		}

		public void RegisterLocal(CurveStore curveStore)
		{
			_binder = new PulseCurveBinder(curveStore, OnNetworkPulseReceived);
			_binder.RegisterLocal();
		}

		public void RegisterRemote(CurveStore curveStore, Id remoteId)
		{
			_binder = new PulseCurveBinder(curveStore, OnNetworkPulseReceived);
			_binder.RegisterRemote(remoteId);
		}

		// we received a pulse instruction from the server, so the owner previously triggered a pulse
		private void OnNetworkPulseReceived()
		{
			_onPulse();
		}
	}
}
