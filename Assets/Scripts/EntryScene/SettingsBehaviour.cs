using UnityEngine;
using UnityEngine.UI;

public class SettingsBehaviour : MonoBehaviour
{
	[SerializeField] private Image musicSlider;
	[SerializeField] private Image vfxSlider;
	[SerializeField] private float increaseValue;
	private MusicData currentManager;

	private void Start()
	{
		currentManager = FindFirstObjectByType<MusicData>();
		Refresh();
	}

	private void Refresh()
	{
		float musicVolume = SaveBehaviour.DataFile.BackgroundEnabled;
		float effectsVolume = SaveBehaviour.DataFile.EfxEnabled;

		musicSlider.fillAmount = musicVolume;
		vfxSlider.fillAmount = effectsVolume;
	}

	public void IncreaseMusic()
	{
		currentManager.IncreaseMusicValue(increaseValue);
		Refresh();
	}

	public void DecreaseMusic()
	{
		currentManager.IncreaseMusicValue(-increaseValue);
		Refresh();
	}

	public void IncreaseEffects()
	{
		SaveBehaviour.DataFile.EfxEnabled += increaseValue;
		if (SaveBehaviour.DataFile.EfxEnabled > 1f)
		{
			SaveBehaviour.DataFile.EfxEnabled = 1f;
		}

		SaveBehaviour.SetValue();
		Refresh();
	}

	public void DecreaseEffects()
	{
		SaveBehaviour.DataFile.EfxEnabled -= increaseValue;
		if (SaveBehaviour.DataFile.EfxEnabled < 0f)
		{
			SaveBehaviour.DataFile.EfxEnabled = 1f;
		}

		SaveBehaviour.SetValue();
		Refresh();
	}
}
