using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using OnFingerTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class FlyingTarget : MonoBehaviour
{
	[SerializeField] private SpriteRenderer Renderer;
	[SerializeField] private Rigidbody2D Rigidbody;
	[SerializeField] private GameObject destroyParticles;
	[SerializeField] private GameObject oneMoreChanceParticles;
	[SerializeField] private VisibleTextPopup visibleTextPopup;
	[SerializeField] private float invincibilityTime;
	[SerializeField] private Color invincibilityColor;
	private bool oneMoreChance;
	private bool invincible;
	private Action onScoreAdded;
	private Action onDead;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

	}

	private void Start()
	{
		oneMoreChance = SaveBehaviour.DataFile.StoreValues[0];
	}

	public void GetEventObservers(Action scoreAddedHandler, Action deadHandler)
	{
		onScoreAdded = scoreAddedHandler;
		onDead = deadHandler;
	}

	public void Enabled(bool enabled)
	{
		if (enabled)
		{
			OnFingerTouch.onFingerDown += OnFingerScreenTouch;
		}
		else
		{
			OnFingerTouch.onFingerDown -= OnFingerScreenTouch;
		}
	}

	private void OnFingerScreenTouch(Finger finger)
	{
		var raycast = Physics2D.RaycastAll(transform.position, Vector3.forward);
		var target = raycast.FirstOrDefault(x => x.collider.GetComponent<TargetSphere>() != null);

		if (target.collider != null)
		{
			if (target.collider.TryGetComponent<TargetSphere>(out TargetSphere targetSphere))
			{
				onScoreAdded();
				targetSphere.OnTouchCompleted();
			}
		}
		else
		{
			Rigidbody.velocity *= -1;
		}
	}

	public void SetVelocity(Vector2 velocity)
	{
		Rigidbody.velocity = velocity;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.GetComponent<ObstacleSquare>() != null)
		{
			if (invincible) return;

			if (oneMoreChance)
			{
				invincible = true;
				oneMoreChance = false;
				oneMoreChanceParticles.gameObject.SetActive(true);
				visibleTextPopup.PopupText("ONE MORE CHANCE");
				StartCoroutine(Invincibility());
			}
			else
			{
				DestroyAction();
				onDead();
			}
		}
	}

	public void DestroyTarget()
	{
		Renderer.enabled = false;
		Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		destroyParticles.SetActive(true);
	}

	private IEnumerator Invincibility()
	{
		Renderer.color = invincibilityColor;
		yield return new WaitForSeconds(invincibilityTime);
		invincible = false;
	}

	private void DestroyAction()
	{
		Renderer.enabled = false;
		Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		destroyParticles.SetActive(true);

	}

	private void OnDestroy()
	{
		OnFingerTouch.onFingerDown -= OnFingerScreenTouch;
	}
}
