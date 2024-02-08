using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFrame : MonoBehaviour
{
	[SerializeField] private UpgradeFrameData data;
	[SerializeField] private TMP_Text nameText;
	[SerializeField] private TMP_Text descText;
	[SerializeField] private TMP_Text priceText;
	[SerializeField] private Button mainButton;
	[SerializeField] private TMP_Text buyStatus;
	[SerializeField] private Color avaliableColor;
	[SerializeField] private Color purchasedColor;
	[SerializeField] private Color noCoinsColor;
	[SerializeField] private Color notBoughtColor;
	[SerializeField] private GemsUIController[] controllers;
	[SerializeField] private Image medalImage;
	[SerializeField] private UpgradeFrame otherFrame;

	private void Start()
	{
		nameText.text = data.name;
		descText.text = data.description;
		priceText.text = data.price.ToString();

		RestartControls();
	}

	public void RestartControls()
	{
		bool avaliableCoins = SaveBehaviour.DataFile.CurrentPlayerCoins >= data.price;
		bool alreadyBought = SaveBehaviour.DataFile.StoreValues[data.index];

		if (avaliableCoins)
		{
			if (alreadyBought)
			{
				buyStatus.text = "PURCHASED";
				buyStatus.color = purchasedColor;
				mainButton.interactable = false;
			}
			else
			{
				buyStatus.text = "AVALIABLE";
				buyStatus.color = avaliableColor;
				mainButton.interactable = true;
			}
		}
		else
		{
			if (alreadyBought)
			{
				buyStatus.text = "PURCHASED";
				buyStatus.color = purchasedColor;
				mainButton.interactable = false;
			}
			else
			{
				buyStatus.text = "NO GEMS";
				buyStatus.color = noCoinsColor;
				mainButton.interactable = false;
			}
		}

		if (alreadyBought)
		{
			medalImage.color = Color.white;
		}
		else
		{
			medalImage.color = notBoughtColor;
		}

		for (int i = 0; i < controllers.Length; i++)
		{
			controllers[i].RestartGems();
		}
	}

	public void PurchaseStoreUpgrade()
	{
		SaveBehaviour.DataFile.StoreValues[data.index] = true;
		SaveBehaviour.DataFile.CurrentPlayerCoins -= data.price;
		SaveBehaviour.SetValue();

		RestartControls();
		otherFrame.RestartControls();
	}
}
