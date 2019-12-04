using System;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class ObstaclesGenerator : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Obstacle[] _obstaclePrefabs;

		[SerializeField, CanBeNull]
		private Transform _leftBound;

		[SerializeField, CanBeNull]
		private Transform _rightBound;

		[SerializeField, Range(2, 50)]
		private int _spawnPointsPerRow = 5;

		[SerializeField, Range(2, 50)]
		private int _spawnPointRows = 2;

		private List<Vector3> _cachedSpawnPoints;

		public IEnumerable<Obstacle> Generate()
		{
			if (_obstaclePrefabs == null || _obstaclePrefabs.Length == 0)
			{
				return Array.Empty<Obstacle>();
			}

			_cachedSpawnPoints = _cachedSpawnPoints ?? GetAllSpawnPoints();
			var obstacles = new List<Obstacle>();

			foreach (var point in _cachedSpawnPoints)
			{
				int prefabIndex = UnityEngine.Random.Range(0, _obstaclePrefabs.Length);
				var prefab = _obstaclePrefabs[prefabIndex];
				var obstacle = Instantiate(prefab);
				obstacle.transform.position = point;
				bool isHazard = UnityEngine.Random.Range(0, 2) == 1;
				bool isInited = isHazard
					? obstacle.TryMakeHazard()
					: obstacle.TryMakeNeutral();

				if (!isInited)
				{
					Destroy(obstacle);
					continue;
				}

				obstacles.Add(obstacle);
			}

			return obstacles;
		}

		private List<Vector3> GetAllSpawnPoints()
		{
			var spawnPoints = new List<Vector3>();

			if (_leftBound == null || _rightBound == null)
			{
				return spawnPoints;
			}

			var spawnRowWidthVector = _rightBound.position - _leftBound.position;
			var spawnRowLengthVector = Vector3.Cross(spawnRowWidthVector, Vector3.up);
			float spawnCellWidth = spawnRowWidthVector.magnitude / (_spawnPointsPerRow - 1);
			var spawnCellWidthVector = spawnRowWidthVector.normalized * spawnCellWidth;
			var spawnCellLengthVector = spawnRowLengthVector.normalized * spawnCellWidth;

			for (int i = 0; i < _spawnPointRows; i++)
			{
				for (int j = 0; j < _spawnPointsPerRow; j++)
				{
					var spawnPoint = _leftBound.position + spawnCellWidthVector * j + spawnCellLengthVector * i;
					spawnPoints.Add(spawnPoint);
				}
			}

			return spawnPoints;
		}
	}
}