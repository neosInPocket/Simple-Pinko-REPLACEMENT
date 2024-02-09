using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainComponent : MonoBehaviour
{
	[SerializeField] private int addScore;
	// [SerializeField] private Tutorial tutorial;
	// [SerializeField] private TapSource tapSource;
	// [SerializeField] private ResultSource resultSource;
	[SerializeField] private TMP_Text levelHolder;
	[SerializeField] private string menuSceneId;
	[SerializeField] private string gameSceneId;
	[SerializeField] private TargetSpheresController targetSpheresController;
	[SerializeField] private SquareObstaclesSpawner squareObstaclesSpawner;
	[SerializeField] private BottomTimerPanel timer;
	[SerializeField] private FlyingTarget flyingTarget;
	public static Vector2 SizeOfScreen;
	private int timeAllowed => (int)(2 * Mathf.Log(SaveBehaviour.DataFile.LevelValue + 1) + 2);
	private int rewardGemns => (int)(3 * Mathf.Log(SaveBehaviour.DataFile.LevelValue + 1 + 1) + 3 + SaveBehaviour.DataFile.LevelValue + 1);
	private float currentTime;

	private void Awake()
	{
		SizeOfScreen = ToWorldPosition(new Vector3(Screen.width, Screen.height));
	}

	private void Start()
	{
		levelHolder.text = "LEVEL " + SaveBehaviour.DataFile.LevelValue.ToString();
		flyingTarget.GetEventObservers(ScoreAddedHandler, OnDestroyedHandler);
		targetSpheresController.SetInitialPositions();
		timer.SetTimerValues(timeAllowed, OnTimerTimeExpired);
		squareObstaclesSpawner.Initialize();
		squareObstaclesSpawner.StartMove();

		if (!SaveBehaviour.DataFile.CanPassTutorial)
		{
			SaveBehaviour.DataFile.CanPassTutorial = true;
			SaveBehaviour.SetValue();

			// tutorial.StartAction(OnTutorialCompleted);
		}
		else
		{
			OnTutorialCompleted();
		}
	}

	private void OnTutorialCompleted()
	{

	}

	private void OnTimerTimeExpired()
	{

	}

	private void ScoreAddedHandler()
	{

	}

	private void OnDestroyedHandler()
	{

	}

	public void Menu()
	{
		SceneManager.LoadScene(menuSceneId);
	}

	public void Retry()
	{
		SceneManager.LoadScene(gameSceneId);
	}

	public Vector3 ToWorldPosition(Vector2 postionOfScreen)
	{
		var main = Camera.main.ScreenPointToRay(postionOfScreen);

		var directive = main.direction;
		var or = main.origin;

		Vector3 normalToPoint = new Vector3(0, 0, 1);
		Vector3 position = new Vector3(0, 0, 0);

		float resultProduct = Vector3.Dot(directive, normalToPoint);

		float distance = Vector3.Dot(position - or, normalToPoint) / resultProduct;

		Vector3 returnResult = or + distance * directive;
		return returnResult;
	}
}
