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
    public class Instantiator<T> : IInstantiator where T : IInstantiable
	{
		public int Id => _id;


		private Constructor<T> _constructor;

		private int _id;


		public Instantiator(Constructor<T> constructor, int id)
		{
			_constructor = constructor;
			_id = id;
		}

		public T Instantiate()
		{
			T obj = _constructor();
			return obj;
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
				curves[i].RegisterRemote(_curveStore, 
			}

			// register all the curves at the curve store

			// let the object know that we are fully ready
			obj.Init();
		}
	}
}
