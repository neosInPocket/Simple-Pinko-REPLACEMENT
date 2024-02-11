using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.UI;

public class WaitGamePhase : MonoBehaviour
{
	[SerializeField] private TMP_Text textValue;
	private Action onContiuned { get; set; }

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void Enable(Action continued, int index = 0, GameObject value = null)
	{
		textValue.gameObject.SetActive(true);
		onContiuned = continued;

		Touch.onFingerDown += OnTapHandler;
	}

	private void OnTapHandler(Finger finger)
	{
		Touch.onFingerDown -= OnTapHandler;
		onContiuned();

		textValue.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnTapHandler;
	}
}
