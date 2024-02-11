using TMPro;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainComponent : MonoBehaviour
{
	[SerializeField] private int addScore;
	[SerializeField] private TrainingPhase trainingPhase;
	[SerializeField] private WaitGamePhase waitGamePhase;
	[SerializeField] private GameEndPhase gameEndPhase;
	[SerializeField] private TMP_Text levelHolder;
	[SerializeField] private string menuSceneId;
	[SerializeField] private string gameSceneId;
	[SerializeField] private TargetSpheresController targetSpheresController;
	[SerializeField] private SquareObstaclesSpawner squareObstaclesSpawner;
	[SerializeField] private BottomTimerPanel timer;
	[SerializeField] private FlyingTarget flyingTarget;
	[SerializeField] private Image progressFill;
	[SerializeField] private TMP_Text progressText;
	public static Vector2 SizeOfScreen;
	private int timeAllowed;
	private int maxProgressValue;
	private int rewardGemns;
	private int currentProgress;

	private void Awake()
	{
		SizeOfScreen = ToWorldPosition(new Vector3(Screen.width, Screen.height));
	}

	private void Start()
	{
		timeAllowed = (int)(2 * Mathf.Log(SaveBehaviour.DataFile.LevelValue + 1) + 10);
		maxProgressValue = (int)(2 * Mathf.Log(SaveBehaviour.DataFile.LevelValue + 1) + 2);
		rewardGemns = (int)(3 * Mathf.Log(SaveBehaviour.DataFile.LevelValue + 1 + 1) + 3 + SaveBehaviour.DataFile.LevelValue + 1);

		levelHolder.text = "LEVEL " + SaveBehaviour.DataFile.LevelValue.ToString();
		flyingTarget.GetEventObservers(ScoreAddedHandler, OnDestroyedHandler);
		targetSpheresController.SetVerticalPositions();
		timer.SetTimerValues(timeAllowed, OnTimerTimeExpired);
		squareObstaclesSpawner.Initialize();
		RefreshProgress();

		bool canPassTutorial = SaveBehaviour.DataFile.CanPassTutorial;

		if (!canPassTutorial)
		{
			StartTraining();
		}
		else
		{
			OnTrainDone();
		}
	}

	private void OnTrainDone()
	{
		waitGamePhase.Enable(OnGame);
	}

	private void RefreshProgress()
	{
		progressFill.fillAmount = (float)currentProgress / (float)maxProgressValue;
		progressText.text = $"{currentProgress}/{maxProgressValue}";
	}

	private void OnGame()
	{
		squareObstaclesSpawner.StartMove();
		targetSpheresController.SetStartVelocity();
		timer.StartTimer();
	}

	private void OnTimerTimeExpired()
	{
		gameEndPhase.ShowResult(true, 0);
		squareObstaclesSpawner.StopMove();
		targetSpheresController.Stop();
		flyingTarget.Enabled(false);
		timer.StopTimer();
	}

	private void ScoreAddedHandler()
	{
		currentProgress += 1;
		if (currentProgress >= maxProgressValue)
		{
			gameEndPhase.ShowResult(false, rewardGemns);
			squareObstaclesSpawner.StopMove();
			targetSpheresController.Stop();
			timer.StopTimer();
			flyingTarget.Enabled(false);

			SaveBehaviour.DataFile.LevelValue += 1;
			SaveBehaviour.DataFile.CurrentPlayerCoins += rewardGemns;
			SaveBehaviour.SetValue();
		}

		RefreshProgress();
	}

	private void OnDestroyedHandler()
	{
		gameEndPhase.ShowResult(false, 0);
		squareObstaclesSpawner.StopMove();
		targetSpheresController.Stop();
		timer.StopTimer();
		flyingTarget.Enabled(false);
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

	private void StartTraining()
	{
		trainingPhase.StartTraining(OnTrainDone);

		SaveBehaviour.DataFile.CanPassTutorial = true;
		SaveBehaviour.SetValue();
	}
}
