using System;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class ObstaclesGenerator : MonoBehaviour, ILevelDataSource
	{
		private const int MinPassageWidth = 2;

		[SerializeField, CanBeNull]
		private Transform _leftBound;

		[SerializeField, CanBeNull]
		private Transform _rightBound;

		[SerializeField, Range(2, 50)]
		private int _spawnPointsPerRow = 5;

		[SerializeField, Range(2, 50)]
		private int _spawnPointRows = 2;


		[SerializeField, Range(1, 50)]
		private int _obstacleTypesCount = 1;

		[SerializeField, Range(MinPassageWidth, 50)]
		private int _passageWidth = MinPassageWidth;

		private List<Vector3> _cachedSpawnPoints;

		public (Vector3 startCenter, Vector3 endCenter, bool isSuccess) TryGetObstaclesFieldCoordinates()
		{
			if (_leftBound == null || _rightBound == null)
			{
				return (Vector3.zero, Vector3.zero, false);
			}

			var spawnRowWidthVector = _rightBound.position - _leftBound.position;
			var spawnRowLengthVector = Vector3.Cross(spawnRowWidthVector, Vector3.up);
			float spawnCellWidth = spawnRowWidthVector.magnitude / (_spawnPointsPerRow - 1);
			var spawnCellLengthVector = spawnRowLengthVector.normalized * spawnCellWidth;
			var fieldLengthVector = _spawnPointRows * spawnCellLengthVector;

			var fieldStart = .5f * (_leftBound.position + _rightBound.position);
			var fieldFinish = fieldStart + fieldLengthVector;
			return (fieldStart, fieldFinish, true);
		}

		public IEnumerable<ObstacleData> GetObstacles()
		{
			if (_obstacleTypesCount == 0)
			{
				return Array.Empty<ObstacleData>();
			}

			_cachedSpawnPoints = _cachedSpawnPoints ?? GetAllSpawnPoints();
			var obstacles = new List<ObstacleData>();
			_passageWidth = Mathf.Clamp(_passageWidth, MinPassageWidth, _spawnPointsPerRow);
			int startIndexUpperBound = _spawnPointsPerRow - _passageWidth;
			int passageStartIndex = UnityEngine.Random.Range(0, startIndexUpperBound);

			for (int i = 0; i < _spawnPointRows; i++)
			{
				passageStartIndex = Mathf.Clamp(passageStartIndex, 0, startIndexUpperBound);

				for (int j = 0; j < _spawnPointsPerRow; j++)
				{
					var point = _cachedSpawnPoints[i * _spawnPointsPerRow + j];
					int type = UnityEngine.Random.Range(0, _obstacleTypesCount);
					bool isHazard = j < passageStartIndex || j >= passageStartIndex + _passageWidth;
					var obstacleData = new ObstacleData(isHazard, point, type);
					obstacles.Add(obstacleData);
				}

				int halfPassageWidth = _passageWidth / 2;
				passageStartIndex += UnityEngine.Random.Range(-halfPassageWidth, halfPassageWidth + 1);
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