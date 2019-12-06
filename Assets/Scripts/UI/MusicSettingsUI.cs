using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NickMorhun.ColorBump.UI
{
	[DisallowMultipleComponent]
	public sealed class MusicSettingsUI : UIBehaviour
	{
		[SerializeField, CanBeNull]
		private Music _music;

		[SerializeField, CanBeNull]
		private Toggle _musicOnToggle;

		[SerializeField, CanBeNull]
		private Toggle _musicOffToggle;

		protected override void Start()
		{
			base.Start();

			Assert.IsNotNull(_music);
			Assert.IsNotNull(_musicOnToggle);
			Assert.IsNotNull(_musicOffToggle);

			_musicOnToggle.isOn = _music.IsOn;
			_musicOffToggle.isOn = !_music.IsOn;
		}

		[UsedImplicitly]
		public void OnMusicOnToggle(bool value)
		{
			if (_music != null)
			{
				_music.IsOn = value;
			}

			if (_musicOffToggle != null)
			{
				_musicOffToggle.isOn = !value;
			}
		}

		[UsedImplicitly]
		public void OnMusicOffToggle(bool value)
		{
			if (_music != null)
			{
				_music.IsOn = !value;
			}

			if (_musicOffToggle != null)
			{
				_musicOnToggle.isOn = !value;
			}
		}
	}
}