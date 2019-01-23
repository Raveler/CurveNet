using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{
	public class MessageTypeNotSupportedException : Exception { };

	public interface IClient
	{
		void OnRoomData(RoomData roomData);

		void OnNewClient(RoomEventData newClient);

		void OnClientRemoved(RoomEventData deadClient);

		void OnProtocolError(ProtocolError error);

		void OnObjectCreated(ObjectData data);
	}
}
