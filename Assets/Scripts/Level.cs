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

		[CanBeNull]
		private IEnumerable<Obstacle> _obstacles;

		private void Start()
		{
			Assert.IsNotNull(_ball);
		}

		public bool TryPrepare([NotNull] ILevelDataSource levelDataSource)
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

			_obstacles = levelDataSource.GetObstacles();

			foreach (var item in _obstacles)
			{
				item.transform.SetParent(transform);
			}

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