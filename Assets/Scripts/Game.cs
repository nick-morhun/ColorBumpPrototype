using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Assertions;
using System.Collections;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public sealed class Game : MonoBehaviour
	{
		[SerializeField, CanBeNull]
		private Ball _ball;

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
			Assert.IsNotNull(_level);
			Assert.IsNotNull(_obstaclesGenerator);
			Assert.IsNotNull(_player);

			_ball.HitHazard += OnHitHazard;
			_player.PlayerInputReceived += OnPlayerInputReceived;
			_player.PlayerStateChanged += OnPlayerStateChanged;
		}

		private void OnDestroy()
		{
			if (_ball is object)
			{
				_ball.HitHazard -= OnHitHazard;
			}

			if (_player is object)
			{
				_player.PlayerInputReceived -= OnPlayerInputReceived;
				_player.PlayerStateChanged -= OnPlayerStateChanged;
			}
		}

		public void PlayNextLevel()
		{
			bool isPrepared = _obstaclesGenerator != null && _level.TryPrepare(_obstaclesGenerator.Generate());

			if (!isPrepared)
			{
				Debug.LogError("Failed to prepare a level.");
				Application.Quit();
				return;
			}

			_player.PlayerState = PlayerState.AtStartLine;
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

		private void OnPlayerStateChanged(Player player, PlayerState playerState)
		{
			switch (playerState)
			{
				case PlayerState.Observing:
					Time.timeScale = _observerStateTimeScale;

					if (_autoSkipObserverState)
					{
						StartCoroutine(GoToLobbyCoroutine());
					}
					break;
				default:
					break;
			}
		}
	}
}