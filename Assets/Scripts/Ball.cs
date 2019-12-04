using System;
using UnityEngine;

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

		private Rigidbody _rigidbody;
		private Vector3 _horizontalForce;

		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			_rigidbody.AddForce(_horizontalForce, ForceMode.Acceleration);
		}

		public void Push(Vector3 input)
		{
			float magnitude = Mathf.Clamp(input.magnitude, _minAcceleration, _maxAcceleration);
			_horizontalForce = magnitude * input.normalized;
		}
	}
}