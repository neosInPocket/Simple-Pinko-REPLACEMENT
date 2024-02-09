using System.Collections;
using UnityEngine;

public class TargetSphere : MonoBehaviour
{
	[SerializeField] private GameObject onTouchMatchEffect;
	[SerializeField] private ObstacleYPosition yPosition;
	[SerializeField] Vector2 yPositionRange;
	[SerializeField] Vector2 xPositionRange;
	[SerializeField] private float radius;
	[SerializeField] private TargetSpheresController targetSpheresController;
	private Vector2 screen;
	private float yPositionValue;

	private void Start()
	{
		screen = MainComponent.SizeOfScreen;
		if (yPosition == ObstacleYPosition.Top)
		{
			yPositionValue = 2 * screen.y * yPositionRange.y - screen.y - radius;
		}
		else
		{
			yPositionValue = 2 * screen.y * yPositionRange.x - screen.y + radius;
		}
	}

	public void OnTouchCompleted()
	{
		StopAllCoroutines();
		onTouchMatchEffect.SetActive(false);
		StartCoroutine(OnMatch());

		targetSpheresController.ChangePositions(this);
	}

	public void ChangePosition()
	{
		transform.position = new Vector2(
			Random.Range(2 * screen.x * xPositionRange.x - screen.x, 2 * screen.x * xPositionRange.y - screen.x), yPositionValue
			);
	}

	private IEnumerator OnMatch()
	{
		onTouchMatchEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
		onTouchMatchEffect.SetActive(false);
	}
}

public enum ObstacleYPosition
{
	Bottom,
	Top
}
