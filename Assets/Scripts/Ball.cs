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
		[SerializeField]
		private Tags _tags;

		private Rigidbody _rigidbody;

		public event Action<Ball> HitHazard;

		private void Start()
		{
			Assert.IsNotNull(_tags);
			_rigidbody = GetComponent<Rigidbody>();
			Assert.IsNotNull(_rigidbody);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag(_tags.HazardTag))
			{
				HitHazard?.Invoke(this);
			}
		}

		public void Push(Vector3 impulse)
		{
			_rigidbody.AddForce(impulse, ForceMode.Impulse);
		}

		public void Stop()
		{
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
			_rigidbody.rotation = Quaternion.identity;
		}
	}
}