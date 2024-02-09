using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BottomTimerPanel : MonoBehaviour
{
	[SerializeField] private Image fillImage;
	[SerializeField] private TMP_Text timeText;
	private float currentTimerTime;
	private float initialTime;
	private bool timerEnabled;
	private Action onTimeExpired;

	private void Update()
	{
		if (!timerEnabled) return;

		currentTimerTime -= Time.deltaTime;
		if (currentTimerTime <= 0)
		{
			onTimeExpired();
			timerEnabled = false;
		}

		RefreshControls();
	}

	public void SetTimerValues(int time, Action timeExpiredHandler)
	{
		currentTimerTime = (float)time;
		initialTime = (float)time;
		onTimeExpired = timeExpiredHandler;

		RefreshControls();
	}

	private void RefreshControls()
	{
		fillImage.fillAmount = currentTimerTime / initialTime;
		timeText.text = ((int)currentTimerTime).ToString();
	}

	public void StartTimer()
	{
		timerEnabled = true;
	}

	public void StopTimer()
	{
		timerEnabled = false;
	}
}
