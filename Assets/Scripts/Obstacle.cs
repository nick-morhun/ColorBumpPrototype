using JetBrains.Annotations;
using NickMorhun.ColorBump.Configuration;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class Obstacle : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Rigidbody _rigidbody;

		[SerializeField, CanBeNull]
		private Renderer _renderer;

		[SerializeField, CanBeNull]
		private Tags _tags;

		[SerializeField, CanBeNull]
		private Materials _materials;

		private void Start()
		{
			Assert.IsNotNull(_rigidbody);
			Assert.IsNotNull(_renderer);
			Assert.IsNotNull(_tags);
			Assert.IsNotNull(_materials);
		}

		public bool TryMakeHazard()
		{
			if (!IsSetupValid())
			{
				return false;
			}

			if (_materials.HazardMaterial == null)
			{
				return false;
			}

			_rigidbody.gameObject.tag = _tags.HazardTag;
			_renderer.material = _materials.HazardMaterial;
			return true;
		}

		public void Destroy()
		{
			if (this)
			{
				Destroy(gameObject);
			}
		}

		public bool TryMakeNeutral()
		{
			if (!IsSetupValid())
			{
				return false;
			}

			if (_materials.NeutralMaterial == null)
			{
				return false;
			}

			_rigidbody.gameObject.tag = _tags.NeutralTag;
			_renderer.material = _materials.NeutralMaterial;
			return true;
		}

		private bool IsSetupValid()
		{
			return _tags != null && _materials != null && _rigidbody != null && _renderer != null;
		}
	}
}