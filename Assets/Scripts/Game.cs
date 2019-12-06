using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Assertions;
using System.Collections;
using NickMorhun.ColorBump.Tools;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class Game : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Ball _ball;

		[SerializeField, CanBeNull]
		private ConstantSpeedMover _ballMover;

		[SerializeField, CanBeNull]
		private ConstantSpeedMover _cameraMover;

		[SerializeField, CanBeNull]
		private Level _level;

		[SerializeField, CanBeNull]
		private ObstaclesGenerator _obstaclesGenerator;

		[SerializeField, CanBeNull]
		private Player _player;

		[SerializeField, Range(0f, 1f)]
		private float _observerStateTimeScale = .5f;

		[SerializeField]
		private bool _autoSkipObserverState;

		[SerializeField, Range(0f, 10f), Tooltip("If 'Auto Skip Observer State is true'.")]
		private float _observerStateDuration = 5f;

		private void Awake()
		{
			Assert.IsNotNull(_ball);
			Assert.IsNotNull(_ballMover);
			Assert.IsNotNull(_cameraMover);
			Assert.IsNotNull(_level);
			Assert.IsNotNull(_obstaclesGenerator);
			Assert.IsNotNull(_player);

			_ball.HitHazard += OnHitHazard;
			_player.PlayerInputReceived += OnPlayerInputReceived;
			_player.PlayerStateChanged += OnPlayerStateChanged;
			_player.PointerDownReceived += OnPlayerPointerDownReceived;
			_player.PointerUpReceived += OnPlayerPointerUpReceived;
			_level.FinishLineWasCrossed += OnFinishLineWasCrossed;
		}

		private void Start()
		{
			ToggleGameSpeed(false);
		}

		private void OnDestroy()
		{
			ToggleGameSpeed(false);

			if (_ball is object)
			{
				_ball.HitHazard -= OnHitHazard;
			}

			if (_level is object)
			{
				_level.FinishLineWasCrossed -= OnFinishLineWasCrossed;
			}

			if (_player is object)
			{
				_player.PlayerInputReceived -= OnPlayerInputReceived;
				_player.PlayerStateChanged -= OnPlayerStateChanged;
				_player.PointerDownReceived -= OnPlayerPointerDownReceived;
				_player.PointerUpReceived -= OnPlayerPointerUpReceived;
			}
		}

		public void PlayNextLevel()
		{
			_ballMover.Direction = _level.Direction;
			_cameraMover.Direction = _level.Direction;
			bool isPrepared = _obstaclesGenerator != null && _level.TryPrepare(_obstaclesGenerator);

			if (isPrepared)
			{
				isPrepared = _cameraMover != null;	
			}

			if (isPrepared)
			{
				_cameraMover.transform.localPosition = Vector3.zero;
			}

			if (!isPrepared)
			{
				Debug.LogError("Failed to prepare a level.");
				Application.Quit();
				return;
			}

			_player.PlayerState = PlayerState.AtStartLine;
		}

		private void ToggleGameSpeed(bool isOn)
		{
			if (_ballMover != null)
			{
				_ballMover.enabled = isOn;
			}

			if (_cameraMover != null)
			{
				_cameraMover.enabled = isOn;
			}
		}

		private IEnumerator GoToLobbyCoroutine()
		{
			yield return new WaitForSecondsRealtime(_observerStateDuration);

			if (_player)
			{
				_level.CleanUp();
				Time.timeScale = 1f;
				_player.PlayerState = PlayerState.InLobby;
			}
		}

		private void OnHitHazard(Ball ball)
		{
			Debug.Log("The ball hit a hazard.");

			if (_player)
			{
				_player.PlayerState = PlayerState.Observing;
			}
		}

		private void OnPlayerInputReceived(Player player)
		{
			player.PlayerState = PlayerState.PlayingLevel;
		}

		private void OnPlayerPointerUpReceived(Player player)
		{
			_ballMover.enabled = true;
		}

		private void OnPlayerPointerDownReceived(Player player)
		{
			_ballMover.enabled = false;
		}

		private void OnFinishLineWasCrossed(Level level)
		{
			Debug.Log("The ball crossed the finish line.");

			if (_player)
			{
				_player.PlayerState = PlayerState.ObservingWin;
			}
		}

		private void OnPlayerStateChanged(Player player, PlayerState playerState)
		{
			switch (playerState)
			{
				case PlayerState.PlayingLevel:
					ToggleGameSpeed(true);
					break;
				case PlayerState.Observing:
				case PlayerState.ObservingWin:
					ToggleGameSpeed(false);
					Time.timeScale = _observerStateTimeScale;

					if (_autoSkipObserverState)
					{
						StartCoroutine(GoToLobbyCoroutine());
					}
					break;
				default:
					ToggleGameSpeed(false);
					break;
			}
		}
	}
}