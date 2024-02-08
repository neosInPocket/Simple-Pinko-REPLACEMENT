using TMPro;
using UnityEngine;

public class GemsUIController : MonoBehaviour
{
	[SerializeField] private TMP_Text gemsText;

	private void Start()
	{
		RestartGems();
	}

	public void RestartGems()
	{
		gemsText.text = SaveBehaviour.DataFile.CurrentPlayerCoins.ToString();
	}
}
