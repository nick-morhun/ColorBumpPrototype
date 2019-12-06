using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class Music : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private AudioSource _audioSource;

		public bool IsOn
		{
			get
			{
				return _audioSource != null ? !_audioSource.mute : false;
			}
			set
			{
				if (_audioSource != null)
				{
					_audioSource.mute = !value;
				}
			}
		}

		private void Start()
		{
			Assert.IsNotNull(_audioSource);
		}
	}
}