using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public interface ICurve
	{
		void ApplyConfig(CurveConfig config);

		CurveConfig GetConfig();

		void RegisterLocal(CurveStore curveStore);

		void RegisterRemote(CurveStore curveStore, Id remoteId);
	}
}
