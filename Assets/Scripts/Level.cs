using System;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class Level : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Ball _ball;

		[SerializeField, CanBeNull]
		private FinishLine _finishLine;

		[CanBeNull]
		private IEnumerable<Obstacle> _obstacles;

		public Vector3 Direction => this ? transform.forward : Vector3.zero;

		public event Action<Level> FinishLineWasCrossed;

		private void Start()
		{
			Assert.IsNotNull(_ball);
			Assert.IsNotNull(_finishLine);

			_finishLine.WasCrossed += OnFinishLineWasCrossed;
		}

		private void OnDestroy()
		{
			if (_finishLine is object)
			{
				_finishLine.WasCrossed -= OnFinishLineWasCrossed;
			}
		}

		private void OnFinishLineWasCrossed(FinishLine finishLine)
		{
			FinishLineWasCrossed?.Invoke(this);
		}

		public bool TryPrepare([NotNull] ILevelDataSource levelDataSource, [NotNull] ObstaclesFactory obstaclesFactory)
		{
			CleanUp();

			if (_ball == null)
			{
				return false;
			}

			_ball.Stop();
			Transform ballTransform = _ball.transform;
			ballTransform.localPosition = Vector3.zero;
			ballTransform.localRotation = Quaternion.identity;

			if (_finishLine == null)
			{
				return false;
			}

			var (startCenter, endCenter, isSuccess) = levelDataSource.TryGetObstaclesFieldCoordinates();

			if (!isSuccess)
			{
				return false;
			}

			bool isFinishPrepared = _finishLine.TryPrepare(startCenter, endCenter);

			if (!isFinishPrepared)
			{
				return false;
			}

			var obstaclesData = levelDataSource.GetObstacles();
			var obstacles = new List<Obstacle>();

			foreach (var obstacleData in obstaclesData)
			{
				Obstacle obstacle;
				(obstacle, isSuccess) = obstaclesFactory.TryGetObstacle(obstacleData);

				if (isSuccess)
				{
					obstacle.transform.SetParent(transform);
					obstacles.Add(obstacle);
				}
			}

			_obstacles = obstacles;
			return true;
		}

		public void CleanUp()
		{
			if (_obstacles is null)
			{
				return;
			}

			foreach (var item in _obstacles)
			{
				item.Destroy();
			}

			_obstacles = null;
		}
	}
}