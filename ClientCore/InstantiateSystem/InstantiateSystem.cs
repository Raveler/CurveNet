using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Objects;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombardel.CurveNet.Client.Instantiate
{
	public class InvalidObjectConfigException : Exception
	{
		public InvalidObjectConfigException(string msg) : base(msg) { }
	}

    public class InstantiateSystem
	{
		private CurveStore _curveStore;
		private ObjectStore _objectStore;

		private Dictionary<int, IInstantiator> _instantiators = new Dictionary<int, IInstantiator>();


		public void RegisterClass<T>() where T : IInstantiable
		{

		}

		public void Spawn(ObjectData data)
		{
			// not enough curves
			if (data.curves.Count == 0) throw new InvalidObjectConfigException("You must define at least the class curve to use the InstantiateSystem");
			
			// the first curve determines the class of the object
			CurveConfig genericClassCurve = data.curves[0];

			// make sure it is the right type
			if (!(genericClassCurve is IntCurveConfig)) throw new InvalidObjectConfigException("The first curve that defines the class must be an IntCurve");
			IntCurveConfig classCurve = genericClassCurve as IntCurveConfig;

			// get the class index
			int classIndex = classCurve.defaultValue;

			// get the proper instantiator for this class
			if (!_instantiators.ContainsKey(classIndex)) throw new InvalidObjectConfigException("No class instantiator found for class index " + classIndex);
			IInstantiator instantiator = _instantiators[classIndex];

			// spawn the object
			instantiator.Spawn(data.id, data.owner, data.curves.Skip(1).ToList());
		}
    }
}
