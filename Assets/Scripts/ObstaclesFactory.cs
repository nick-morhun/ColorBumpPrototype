using System;
using UnityEngine;
using JetBrains.Annotations;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class ObstaclesFactory : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Obstacle[] _obstaclePrefabs;

		public (Obstacle obstacle, bool isSuccess) TryGetObstacle(ObstacleData obstacleData)
		{
			if (_obstaclePrefabs == null || _obstaclePrefabs.Length == 0)
			{
				return (null, false);
			}

			var prefab = _obstaclePrefabs[obstacleData.Type];
			var obstacle = Instantiate(prefab);
			obstacle.transform.position = obstacleData.WorldPostion;
			bool isInited = obstacleData.IsHazard
				? obstacle.TryMakeHazard()
				: obstacle.TryMakeNeutral();

			if (!isInited)
			{
				Destroy(obstacle);
				return (null, false);
			}

			return (obstacle, true);
		}
	}
}