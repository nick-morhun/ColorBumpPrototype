using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace NickMorhun.ColorBump.Tools
{
	[DisallowMultipleComponent]
	public sealed class DisposedByGravity : MonoBehaviour
	{
		[SerializeField, Range(0f, 100f)]
		private float _disposalDepth = 20f;

		[SerializeField, Range(0.05f, 2f), Tooltip("In seconds.")]
		private float _checkPeriod = 1f;

		[SerializeField, CanBeNull]
		private Transform _positionSource;

		private void Start()
		{
			if (_positionSource == null)
			{
				_positionSource = transform;
			}

			StartCoroutine(DisposeCoroutine());
		}

		private IEnumerator DisposeCoroutine()
		{
			while (this)
			{
				if (_positionSource == null)
				{
					Destroy(gameObject);
					yield break;
				}

				var gravityDirection = Physics.gravity.normalized;
				float objectDepth = Vector3.Project(_positionSource.position, gravityDirection).magnitude;

				if (_disposalDepth < objectDepth)
				{
					Destroy(gameObject);
					yield break;
				}

				yield return new WaitForSecondsRealtime(_checkPeriod);
			}
		}
	}
}