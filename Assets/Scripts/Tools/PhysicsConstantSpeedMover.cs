using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump.Tools
{
	[DisallowMultipleComponent]
	public sealed class PhysicsConstantSpeedMover : ConstantSpeedMover
	{
		[SerializeField, CanBeNull]
		private Rigidbody _rigidbody;

		protected override void Awake()
		{
			base.Awake();

			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
			}

			Assert.IsNotNull(_rigidbody);
		}

		private void FixedUpdate()
		{
			_rigidbody.velocity = Speed * Direction;
		}
	}
}