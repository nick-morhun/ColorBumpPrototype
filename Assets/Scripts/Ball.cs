using JetBrains.Annotations;
using NickMorhun.ColorBump.Configuration;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Rigidbody))]
	public sealed class Ball : MonoBehaviour
	{
		[SerializeField, Range(0f, 10f)]
		private float _minAcceleration = 1f;

		[SerializeField, Range(0f, 10f)]
		private float _maxAcceleration = 1f;

		[SerializeField, CanBeNull]
		private Tags _tags;

		private Rigidbody _rigidbody;
		private Vector3 _horizontalForce;

		public event Action<Ball> HitHazard;

		private void Start()
		{
			Assert.IsNotNull(_tags);
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			_rigidbody.AddForce(_horizontalForce, ForceMode.Acceleration);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag(_tags.HazardTag))
			{
				HitHazard?.Invoke(this);
			}
		}

		public void Push(Vector3 input)
		{
			float magnitude = Mathf.Clamp(input.magnitude, _minAcceleration, _maxAcceleration);
			_horizontalForce = magnitude * input.normalized;
		}

		public void Stop()
		{
			_horizontalForce = Vector3.zero;
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.rotation = Quaternion.identity;
		}
	}
}