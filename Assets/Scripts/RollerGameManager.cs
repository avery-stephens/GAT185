using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;

public class RollerGameManager : Singleton<RollerGameManager> 
{
	[Header("Events")]

	[SerializeField] EventRouter startGameEvent;
	[SerializeField] EventRouter stopGameEvent;
	[SerializeField] EventRouter winGameEvent;

	[SerializeField] Slider healthMeter;
	[SerializeField] TMP_Text scoreUI;
	[SerializeField] GameObject gameOverUI;	
	[SerializeField] GameObject gameWonUI;	
	[SerializeField] GameObject titleUI;

	[SerializeField] AudioSource gameMusic;

	[SerializeField] GameObject playerPrefab;
	[SerializeField] Transform playerStart;

	public enum State
	{
		TITLE,
		START_GAME,
		PLAY_GAME,
		GAME_OVER,
		GAME_WON
	}

	State state = State.TITLE;
	float stateTimer = 0;

	private void Start()
	{
		winGameEvent.onEvent += SetGameWin;
	}

	private void Update()
	{
		switch (state) 
		{
			case State.TITLE:
				titleUI.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				startGameEvent.Notify();
				titleUI.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
				Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
				state = State.PLAY_GAME;
				gameMusic.Play();
				break;
			case State.PLAY_GAME:
				break;
			case State.GAME_OVER:
				stateTimer -= Time.deltaTime;

				if (stateTimer <= 0)
				{
					gameOverUI.SetActive(false);
					state = State.TITLE;
				}
				break;
			case State.GAME_WON:
				stateTimer -= Time.deltaTime;

				if (stateTimer <= 0)
				{
					gameWonUI.SetActive(false);
					state = State.TITLE;
				}
				break;

			default:
				break;
		}
	}

	public void SetHealth(int health)
	{
		healthMeter.value = Mathf.Clamp(health, 0, 100);
	}

	public void SetScore(int score) 
	{ 
		scoreUI.text = score.ToString();
	}

	public void SetGameOver()
	{
		stopGameEvent.Notify();
		gameOverUI.SetActive(true);
		gameMusic.Stop();
		state = State.GAME_OVER;
		stateTimer = 5;
	}

	public void SetGameWin()
	{
		stopGameEvent.Notify();
		gameWonUI.SetActive(true);
		state = State.GAME_WON;
		stateTimer = 5;
		Debug.Log("Win!!!");
	}

	public void OnStartGame()
	{
		state = State.START_GAME;
	}
}
