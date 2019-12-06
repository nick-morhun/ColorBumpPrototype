using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace NickMorhun.ColorBump
{
	public interface ILevelDataSource
	{
		[NotNull]
		IEnumerable<ObstacleData> GetObstacles();

		(Vector3 startCenter, Vector3 endCenter, bool isSuccess) TryGetObstaclesFieldCoordinates();
	}
}