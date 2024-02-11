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
	[SerializeField] private float topEdge;
	[SerializeField] private float bottomEdge;
	[SerializeField] private float radius;
	public TargetSphere currentStartSphere { get; set; }
	public Rigidbody2D Rigidbody2D => Rigidbody;
	private bool oneMoreChance;
	private bool invincible;
	private Action onScoreAdded;
	private Action onDead;
	private float topEdgeValue;
	private float bottomEdgeValue;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		var screenSize = MainComponent.SizeOfScreen;

		topEdgeValue = 2 * screenSize.y * topEdge - screenSize.y;
		bottomEdgeValue = 2 * screenSize.y * bottomEdge - screenSize.y;
		oneMoreChance = SaveBehaviour.DataFile.StoreValues[0];
	}

	public void GetEventObservers(Action scoreAddedHandler, Action deadHandler)
	{
		onScoreAdded = scoreAddedHandler;
		onDead = deadHandler;
	}

	private void Update()
	{
		if (transform.position.y >= topEdgeValue - radius || transform.position.y <= bottomEdgeValue + radius)
		{
			DestroySphere();
		}
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
			Rigidbody.velocity = Vector2.zero;
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
				if (targetSphere == currentStartSphere)
				{
					Rigidbody.velocity *= -1;
					return;
				}

				currentStartSphere = targetSphere;
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

	private IEnumerator Invincibility()
	{
		Renderer.color = invincibilityColor;
		yield return new WaitForSeconds(invincibilityTime);
		Renderer.color = Color.white;
		invincible = false;
	}

	private void DestroySphere()
	{
		DestroyAction();
		onDead();
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
