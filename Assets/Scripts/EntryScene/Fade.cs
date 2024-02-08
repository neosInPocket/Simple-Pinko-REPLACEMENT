using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
	[SerializeField] private List<GameObject> switchables;
	[SerializeField] private Material mat;
	[SerializeField] private string materialKeyValue;
	[SerializeField] private string materialDirectionKeyValue;
	[SerializeField] private float fadeSpeed;
	[SerializeField] private float topEdgeValue;
	[SerializeField] private float bottomEdgeValue;
	[SerializeField] private float speedThreshold;
	private GameObject switchable;

	private void Start()
	{
		mat.SetFloat(materialKeyValue, bottomEdgeValue);

		foreach (var switchable in switchables)
		{
			switchable.SetActive(false);
		}

		switchable = switchables[0];
		switchable.SetActive(true);
	}

	public void Switch(GameObject toSwitchable)
	{
		StopAllCoroutines();
		StartCoroutine(SwitchRoutine(toSwitchable));
	}

	private IEnumerator SwitchRoutine(GameObject toSwitchable)
	{
		var currentValue = 0f;
		var currentDirection = 1f;
		var currentDistance = 1f;
		mat.SetFloat(materialDirectionKeyValue, currentDirection);
		mat.SetFloat(materialKeyValue, currentValue);

		while (currentValue < topEdgeValue)
		{
			currentValue += fadeSpeed * Time.deltaTime * (currentDistance + speedThreshold);
			mat.SetFloat(materialKeyValue, currentValue);
			currentDistance = topEdgeValue - currentValue;
			yield return null;
		}

		mat.SetFloat(materialKeyValue, topEdgeValue);
		currentValue = topEdgeValue;

		toSwitchable.SetActive(true);
		switchable.SetActive(false);
		switchable = toSwitchable;
		currentDistance = currentValue - bottomEdgeValue;

		while (currentValue > bottomEdgeValue)
		{
			currentValue -= fadeSpeed * Time.deltaTime * (currentDistance + speedThreshold);
			mat.SetFloat(materialKeyValue, currentValue);
			currentDistance = currentValue - bottomEdgeValue;
			yield return null;
		}

		mat.SetFloat(materialKeyValue, bottomEdgeValue);
	}
}
