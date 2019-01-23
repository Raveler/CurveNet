using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Server
{

	public interface IServer
	{
		void JoinRoom(string roomName);
		void LeaveRoom(string roomName);

		void ListRooms();

		void CreateObject(string objectId);
		void RemoveObject(string objectId);

		void AddObjectToRoom(string objectId, string roomName);

		void AddCurve(string objectId);

		void UpdateCurve(string objectId);
		void UpdateCurves(string objectId);
	}
}
