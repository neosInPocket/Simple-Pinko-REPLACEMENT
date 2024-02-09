using System.Collections.Generic;
using UnityEngine;

public class SquareObstaclesSpawner : MonoBehaviour
{
	[SerializeField] private List<ObstacleSquare> squares;
	[SerializeField] private Vector2 yRange;
	[SerializeField] private float targetSphereRadius;
	[SerializeField] private float normalSpeed;
	[SerializeField] private float slowedSpeed;
	private float moveSpeed;

	public void Initialize()
	{
		moveSpeed = SaveBehaviour.DataFile.StoreValues[1] ? slowedSpeed : normalSpeed;

		var screenSize = MainComponent.SizeOfScreen;
		var topEdge = 2 * screenSize.y * yRange.y - screenSize.y - targetSphereRadius * 2;
		var bottomEdge = 2 * screenSize.y * yRange.x - screenSize.y + targetSphereRadius * 2;
		var avaliableScreen = topEdge - bottomEdge;
		var squareWidth = squares[0].Width;

		var allCount = squares.Count;
		var avaliableSpace = avaliableScreen - allCount * squareWidth;
		var spacing = avaliableSpace / (allCount + 1);

		Debug.Log(spacing);
		Vector2 position = Vector2.zero;
		position.y = topEdge - (spacing + squareWidth / 2);

		for (int i = 0; i < allCount; i++)
		{
			if (i % 2 == 0)
			{
				position.x = -screenSize.x + squareWidth / 2;
			}
			else
			{
				position.x = screenSize.x - squareWidth / 2;
			}

			squares[i].transform.position = position;
			position.y = position.y - (spacing + squareWidth);
		}
	}

	public void StartMove()
	{
		foreach (var square in squares)
		{
			square.StartMoving(moveSpeed);
		}
	}

	public void StopMove()
	{
		foreach (var square in squares)
		{
			square.Stop();
		}
	}
}
