using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Server
{
	public enum ServerCall
	{
		JoinRoom,
		LeaveRoom,
		ListRooms,
		CreateObject,
		AddObjectToRoom,
		RemoveObjectFromRoom,

	}
	public interface IServer
	{
		void JoinRoom(string roomName);
		void LeaveRoom(string roomName);

		void ListRooms();

		void CreateObject(NewObjectConfig objectConfig);
		void RemoveObject(string objectId);

		void AddObjectToRoom(string objectId, string roomName);

		void AddCurve(string objectId);

		void UpdateCurve(string objectId);
		void UpdateCurves(string objectId);
	}
}
