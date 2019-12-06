using JetBrains.Annotations;
using NickMorhun.ColorBump.Tools;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class FinishLine : MonoBehaviour
	{
		[SerializeField, Range(0f, 50f)]
		private float _distanceFromObstaclesToFinish = 5f;

		[SerializeField, CanBeNull]
		private TriggerEnterReporter _triggerEnterReporter;

		public event Action<FinishLine> WasCrossed;

		private void Awake()
		{
			Assert.IsNotNull(_triggerEnterReporter);
		}

		private void OnEnable()
		{
			if (_triggerEnterReporter != null)
			{
				_triggerEnterReporter.Collided += OnTriggerEnterReporterCollided;
			}
		}

		private void OnDisable()
		{
			if (_triggerEnterReporter != null)
			{
				_triggerEnterReporter.Collided -= OnTriggerEnterReporterCollided;
			}
		}

		public bool TryPrepare(Vector3 obstaclesFieldStart, Vector3 obstaclesFieldFinish)
		{
			var direction = obstaclesFieldFinish - obstaclesFieldStart;
			transform.position = obstaclesFieldFinish + direction.normalized * _distanceFromObstaclesToFinish;
			transform.LookAt(transform.position + direction);
			return true;
		}

		private void OnTriggerEnterReporterCollided(GameObject otherObject)
		{
			WasCrossed?.Invoke(this);
		}
	}
}