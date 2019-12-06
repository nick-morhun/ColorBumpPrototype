using System;
using UnityEngine;
using UnityEngine.EventSystems;
using NickMorhun.ColorBump.Tools;
using JetBrains.Annotations;

namespace NickMorhun.ColorBump
{
	public class Player : InputListener
	{
		[SerializeField, CanBeNull]
		private Ball _ball;

		[SerializeField, Range(0f, 10f)]
		private float _inputScale = 1f;

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
		public event Action<Player> PointerDownReceived;
		public event Action<Player> PointerUpReceived;

		public override void OnDrag(PointerEventData eventData)
		{
			base.OnDrag(eventData);

			if (_ball == null || (PlayerState != PlayerState.AtStartLine && PlayerState != PlayerState.PlayingLevel))
			{
				return;
			}

			Vector3 input = eventData.delta.ConvertToVector3XZ();
			_ball.Push(_inputScale * input);
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			PlayerInputReceived?.Invoke(this);
			PointerDownReceived?.Invoke(this);
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			PointerUpReceived?.Invoke(this);
		}
	}

	public enum PlayerState
	{
		InLobby,
		AtStartLine,
		PlayingLevel,
		Observing,
		ObservingWin,
	}
}