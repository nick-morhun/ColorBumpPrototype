using UnityEngine;

namespace NickMorhun.ColorBump.Tools
{
	[DisallowMultipleComponent]
	public abstract class ConstantSpeedMover : MonoBehaviour
	{
		private const float MinSpeed = 0f;
		private const float MaxSpeed = 100f;

		[SerializeField, Range(MinSpeed, MaxSpeed)]
		private float _speed = 2;

		private Vector3? _direction;

		public float Speed
		{
			get => _speed;
			set => _speed = Mathf.Clamp(value, MinSpeed, MaxSpeed);
		}

		public Vector3 Direction
		{
			get => _direction ?? Vector3.zero;
			set => _direction = value;
		}

		protected virtual void Awake()
		{
			if (!_direction.HasValue)
			{
				_direction = transform.forward;
			}
		}
	}
}