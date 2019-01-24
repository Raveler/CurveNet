using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombardel.CurveNet.Client.Instantiate
{
	public delegate T InstantiatorConstructorDelegate<T>() where T : IInstantiable;

    public class Instantiator<T> : IInstantiator where T : IInstantiable
	{
		private InstantiatorConstructorDelegate<T> _constructor;


		public Instantiator(InstantiatorConstructorDelegate<T> constructor)
		{
			_constructor = constructor;
		}

		public void Spawn(Id objectId, Id ownerId, List<CurveConfig> curveConfigs)
		{
			// spawn the object from the provided constructor
			T obj = _constructor();

			// get all the curves in the object and apply the respective configs
			List<ICurve> curves = obj.GetSynchedCurves().ToList();

			// these do not match
			if (curves.Count != curveConfigs.Count) throw new InvalidObjectConfigException("Amount of curves in object (" + curves.Count + ") does not match amount of provided curve configs (" + curveConfigs.Count + ")");

			// apply all the configs to the curves
			for (int i = 0; i < curves.Count; ++i)
			{
				curves[i].ApplyConfig(curveConfigs[i]);
			}

		}
	}
}
