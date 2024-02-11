using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TrainingPhase : MonoBehaviour
{
	[SerializeField] private TMP_Text textHolder;
	[SerializeField] private List<string> texts;
	private int currentTextIndex;
	private Action m_actionAfter;

	public void StartTraining(Action actionAfter)
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += OnFingerDownHandler;
		gameObject.SetActive(true);

		SetTextHolder();
		m_actionAfter = actionAfter;
	}

	private void SetTextHolder()
	{
		if (currentTextIndex == texts.Count)
		{
			EndLine();
			return;
		}

		textHolder.text = texts[currentTextIndex];
		currentTextIndex++;
	}

	private void EndLine()
	{
		Touch.onFingerDown -= OnFingerDownHandler;
		m_actionAfter();
		gameObject.SetActive(false);
	}

	private void OnFingerDownHandler(Finger finger)
	{
		SetTextHolder();
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDownHandler;
	}
}
