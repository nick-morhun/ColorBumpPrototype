using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class UIController : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Player _player;

		[SerializeField, CanBeNull]
		private GameObject _lobbyUI;

		[SerializeField, CanBeNull]
		private GameObject _startHintUI;

		[SerializeField, CanBeNull]
		private GameObject _currentUI;

		private void Start()
		{
			Assert.IsNotNull(_player);

			_player.PlayerStateChanged += OnPlayerStateChanged;
		}

		private void OnDestroy()
		{
			if (_player is null)
			{
				return;
			}

			_player.PlayerStateChanged -= OnPlayerStateChanged;
		}

		public void OnPlay()
		{
			if (_player == null)
			{
				return;
			}

			_player.PlayerState = PlayerState.AtStartLine;
		}

		private void OnPlayerStateChanged(Player player, PlayerState playerState)
		{
			if (_currentUI != null)
			{
				_currentUI.SetActive(false);
				_currentUI = null;
			}

			switch (playerState)
			{
				case PlayerState.InLobby:
					_lobbyUI.SetActive(true);
					_currentUI = _lobbyUI;
					break;
				case PlayerState.AtStartLine:
					_startHintUI.SetActive(true);
					_currentUI = _startHintUI;
					break;
				case PlayerState.PlayingLevel:
					break;
				case PlayerState.Observing:
					break;
				default:
					break;
			}
		}
	}
}