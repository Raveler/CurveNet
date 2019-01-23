using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Objects
{

	public class ObjectStore
	{

		private IdStore _idStore = new IdStore();

		private Dictionary<Id, Object> _objects = new Dictionary<Id, Object>();




		public ObjectStore(CurveStore curveStore)
		{

		}

		public ObjectData CreateObject(NewObjectConfig config, Id owner)
		{
			Object obj = new Object(config, _idStore.GenerateId(), owner);
			return obj.GetData();
		}
	}
}
