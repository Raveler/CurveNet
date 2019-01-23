using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{
	public class MessageTypeNotSupportedException : Exception { };

	public interface IClient
	{
		void SendRoomData(RoomData roomData);

		void SendNewClient(RoomEventData newClient);

		void SendRemoveClient(RoomEventData deadClient);

		void SendProtocolError(ProtocolError error);
	}
}
