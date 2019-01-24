using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Objects;
using Bombardel.CurveNet.Shared.Serialization;
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

	public delegate T Constructor<T>();

    public class InstantiateSystem
	{
		private CurveStore _curveStore;
		private ObjectStore _objectStore;

		private IdStore _classIdStore;

		private Dictionary<int, IInstantiator> _instantiators = new Dictionary<int, IInstantiator>();
		private Dictionary<Type, int> _typeToInstantiator = new Dictionary<Type, int>();

		private int _classCount = 0;


		public InstantiateSystem(CurveStore curveStore)
		{
			_curveStore = curveStore;
		}

		public void RegisterClass<T>(Constructor<T> constructor) where T : IInstantiable
		{
			Instantiator<T> instantiator = new Instantiator<T>(constructor, ++_classCount);
			_instantiators.Add(instantiator.Id, instantiator);
			_typeToInstantiator.Add(typeof(T), instantiator.Id);
		}

		public void MakeNetworked(IInstantiable obj)
		{
			// get the type of this object - it MUST be registered before!
			Type type = obj.GetType();
			if (!_typeToInstantiator.ContainsKey(type)) throw new ArgumentException("Type " + type.Name + " was not registered before, so you cannot make an object of this type networked. Register it first using RegisterClass()");

			// we get all the curves and submit them to the server
			NewObjectConfig config = new NewObjectConfig();

			// create the curve that will define the id, which will in turn be used to respawn the object
			// on all other clients.
			int id = _typeToInstantiator[type];
			IntCurveConfig classCurve = new IntCurveConfig(id);
			config.curves.Add(classCurve);

			// Now create configs from all the other curves in the object, so that
			// we transmit the values we have locally.
			foreach (ICurve curve in obj.GetSynchedCurves())
			{
				config.curves.Add(curve.GetConfig());
				curve.RegisterLocal(_curveStore);
			}

			// since the object is now registered, we can allow it to use the curves
			obj.Init();
		}

		public void DestroyObject(IInstantiable obj)
		{
			// TODO also destroy over the network!!!
		}
		
		public void InstantiateRemoteObject(ObjectData data)
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
