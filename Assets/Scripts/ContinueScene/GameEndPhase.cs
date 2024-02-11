using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndPhase : MonoBehaviour
{
	public string winString;
	public string nextString;
	public string retryString;
	public string timeOverString;
	public string crashedString;
	public string menuSceneString;
	public string gameSceneString;
	public TMP_Text coinsRewardValue;
	public TMP_Text resultText;
	public TMP_Text buttonCaptionValue;
	public Animator animatorController;
	private Action onWindowClosed { get; set; }

	public void ShowResult(bool timeOver, int coinsAdded)
	{
		gameObject.SetActive(true);
		bool isWonGame = coinsAdded > 0;

		if (isWonGame)
		{
			buttonCaptionValue.text = "NEXT";
			resultText.text = "WIN!";
		}
		else
		{
			buttonCaptionValue.text = "RETRY";

			if (timeOver)
			{
				resultText.text = "TIME IS OVER";
			}
			else
			{
				resultText.text = "CRASHED!";
			}
		}

		coinsRewardValue.text = coinsAdded.ToString();
	}

	public void GetMenuAnimator()
	{
		animatorController.SetTrigger("CloseWindow");
		onWindowClosed = PreloadMainMenu;
	}

	public void PreloadMainMenu()
	{
		SceneManager.LoadScene(menuSceneString);
	}

	public void PreloadGameScene()
	{
		SceneManager.LoadScene(gameSceneString);
	}

	public void PreloadGameAnimation()
	{
		animatorController.SetBool("CloseWindow", true);
		onWindowClosed = PreloadGameScene;
	}

	public void OnWindowClosed()
	{
		gameObject.SetActive(false);
		onWindowClosed();
	}
}
