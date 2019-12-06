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
		private Game _game;

		[SerializeField, CanBeNull]
		private Player _player;

		[SerializeField, CanBeNull]
		private GameObject _lobbyUI;

		[SerializeField, CanBeNull]
		private GameObject _startHintUI;

		[SerializeField, CanBeNull]
		private GameObject _victoryUI;

		[SerializeField, CanBeNull]
		private GameObject _settingsUI;

		private GameObject _currentUI;

		private void Start()
		{
			Assert.IsNotNull(_game);
			Assert.IsNotNull(_player);
			Assert.IsNotNull(_lobbyUI);
			Assert.IsNotNull(_startHintUI);

			SetUiScreen(_lobbyUI);
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

		[UsedImplicitly]
		public void OnPlay()
		{
			if (_game == null)
			{
				return;
			}

			_game.PlayNextLevel();
		}

		[UsedImplicitly]
		public void OnSettings()
		{
			if (_settingsUI != null)
			{
				SetUiScreen(_settingsUI);
			}
		}

		[UsedImplicitly]
		public void OnSettingsBack()
		{
			if (_lobbyUI != null)
			{
				SetUiScreen(_lobbyUI);
			}
		}

		private void SetUiScreen([CanBeNull] GameObject ui)
		{
			if (_currentUI != null)
			{
				_currentUI.SetActive(false);
			}

			_currentUI = ui;

			if (_currentUI != null)
			{
				_currentUI.SetActive(true);
			}
		}

		private void OnPlayerStateChanged(Player player, PlayerState playerState)
		{
			switch (playerState)
			{
				case PlayerState.InLobby:
					SetUiScreen(_lobbyUI);
					break;
				case PlayerState.AtStartLine:
					SetUiScreen(_startHintUI);
					break;
				case PlayerState.PlayingLevel:
					SetUiScreen(null);
					break;
				case PlayerState.Observing:
					break;
				case PlayerState.ObservingWin:
					SetUiScreen(_victoryUI);
					break;
				default:
					break;
			}
		}
	}
}