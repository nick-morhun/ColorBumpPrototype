using UnityEngine;

namespace NickMorhun.ColorBump.Tools
{
	public sealed class TransformWorldConstantSpeedMover : ConstantSpeedMover
	{
		[SerializeField]
		private UpdateMethod _updateMethod;

		private void Update()
		{
			if (_updateMethod == UpdateMethod.Update)
			{
				UpdatePosition();
			}
		}

		private void FixedUpdate()
		{
			if (_updateMethod == UpdateMethod.FixedUpdate)
			{
				UpdatePosition();
			}
		}

		private void LateUpdate()
		{
			if (_updateMethod == UpdateMethod.LateUpdate)
			{
				UpdatePosition();
			}
		}

		private void UpdatePosition()
		{
			transform.position += Time.fixedDeltaTime * Speed * Direction;
		}

		public enum UpdateMethod
		{
			Update,
			FixedUpdate,
			LateUpdate,
		}
	}
}