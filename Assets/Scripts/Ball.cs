using System;
using UnityEngine;
using UnityEngine.EventSystems;
using NickMorhun.ColorBump.Tools;

namespace NickMorhun.ColorBump
{
	[RequireComponent(typeof(Rigidbody))]
	public class Ball : DragInputListener
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

		public override void OnDrag(PointerEventData eventData)
		{
			Vector3 input = eventData.delta.ConvertToVector3XZ();
			float magnitude = Mathf.Clamp(input.magnitude, _minAcceleration, _maxAcceleration);
			_horizontalForce = magnitude * input.normalized;
		}
	}
}