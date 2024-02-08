using System;

[Serializable]
public class SavesData
{
	public int LevelValue = 1;
	public int CurrentPlayerCoins = 60;
	public bool CanPassTutorial = false;
	public float BackgroundEnabled = 1.0f;
	public float EfxEnabled = 1.0f;
	public bool[] StoreValues = { false, false };

}
