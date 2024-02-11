using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSquare : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Vector2 delays;
	[SerializeField] private float angular;
	public float Width => spriteRenderer.size.x;
	private Vector2 screenSize;
	private float speed;

	public void StartMoving(float currentSpeed)
	{
		screenSize = MainComponent.SizeOfScreen;
		speed = currentSpeed;
		StartCoroutine(MoveRoutine());
	}

	public void Stop()
	{
		StopAllCoroutines();
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0;
	}

	private IEnumerator MoveRoutine()
	{
		yield return new WaitForSeconds(Random.Range(delays.x, delays.y));
		if (transform.position.x < 0)
		{
			rb.velocity = new Vector2(speed, 0);
			rb.angularVelocity = angular;

			while (transform.position.x < screenSize.x - Width / 2)
			{
				yield return null;
			}

			rb.velocity = Vector2.zero;
			rb.angularVelocity = 0;
			transform.position = new Vector2(screenSize.x - Width / 2, transform.position.y);
			transform.rotation = Quaternion.identity;
			StartCoroutine(MoveRoutine());
			yield break;
		}

		if (transform.position.x > 0)
		{
			rb.velocity = new Vector2(-speed, 0);
			rb.angularVelocity = -angular;

			while (transform.position.x > -screenSize.x + Width / 2)
			{
				yield return null;
			}

			rb.velocity = Vector2.zero;
			rb.angularVelocity = 0;
			transform.position = new Vector2(-screenSize.x + Width / 2, transform.position.y);
			transform.rotation = Quaternion.identity;
			StartCoroutine(MoveRoutine());
			yield break;
		}


	}
}
