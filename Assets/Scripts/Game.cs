using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class Game : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Player _player;

		private void Awake()
		{
			Assert.IsNotNull(_player);

			_player.PlayerInputReceived += OnPlayerInputReceived;
		}

		private void OnDestroy()
		{
			if (_player is null)
			{
				return;
			}

			_player.PlayerInputReceived -= OnPlayerInputReceived;
		}

		private void OnPlayerInputReceived(Player player)
		{
			player.PlayerState = PlayerState.PlayingLevel;
		}
	}
}