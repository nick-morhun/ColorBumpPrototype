using System;
using UnityEngine;
using UnityEngine.EventSystems;
using NickMorhun.ColorBump.Tools;
using JetBrains.Annotations;

namespace NickMorhun.ColorBump
{
	public class Player : DragInputListener
	{
		[SerializeField, CanBeNull]
		private Ball _ball;
		private PlayerState _playerState = PlayerState.InLobby;

		public PlayerState PlayerState
		{
			get => _playerState;
			set
			{
				if (_playerState == value)
				{
					return;
				}

				_playerState = value;
				PlayerStateChanged?.Invoke(this, PlayerState);
			}
		}

		public event Action<Player, PlayerState> PlayerStateChanged;
		public event Action<Player> PlayerInputReceived;

		public override void OnDrag(PointerEventData eventData)
		{
			if (_ball == null || (PlayerState != PlayerState.AtStartLine && PlayerState != PlayerState.PlayingLevel))
			{
				return;
			}

			Vector3 input = eventData.delta.ConvertToVector3XZ();
			_ball.Push(input);
			PlayerInputReceived?.Invoke(this);
		}
	}

	public enum PlayerState
	{
		InLobby,
		AtStartLine,
		PlayingLevel,
		Observing,
	}
}