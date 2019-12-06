using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class SpeedLimiter : MonoBehaviour
	{
		[SerializeField, CanBeNull, Tooltip("If not set, will get component.")]
		private Rigidbody _rigidbody;

		[SerializeField, Range(0f, 100f)]
		private float _topSpeed = 1f;

		private void Start()
		{
			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
			}

			Assert.IsNotNull(_rigidbody);
			StartCoroutine(LimitSpeedCoroutine());
		}

		private IEnumerator LimitSpeedCoroutine()
		{
			while (this)
			{
				yield return new WaitForFixedUpdate();

				float sqrMagnitude = _rigidbody.velocity.sqrMagnitude;

				if (!Mathf.Approximately(sqrMagnitude, 0f) && sqrMagnitude > _topSpeed * _topSpeed)
				{
					_rigidbody.velocity *= _topSpeed / _rigidbody.velocity.magnitude;
				}
			}
		}
	}
}