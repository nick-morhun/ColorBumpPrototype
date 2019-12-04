﻿using System;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class Level : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Ball _ball;

		private IEnumerable<Obstacle> _obstacles;

		public bool TryPrepare([NotNull] IEnumerable<Obstacle> obstacles)
		{
			if (_ball == null || !obstacles.Any())
			{
				return false;
			}

			_ball.Stop();
			Transform ballTransform = _ball.transform;
			ballTransform.localPosition = Vector3.zero;
			_ball.transform.localRotation = Quaternion.identity;
			CleanUp();

			_obstacles = obstacles;

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