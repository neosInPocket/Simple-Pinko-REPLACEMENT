using System.Linq;
using UnityEngine;

public class MusicData : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Awake()
	{
		var others = FindObjectsOfType<MusicData>();
		var mine = others.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

		if (mine != null && mine != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		audioSource.volume = SaveBehaviour.DataFile.BackgroundEnabled;
	}

	public void IncreaseMusicValue(float value)
	{
		audioSource.volume += value;
		SaveBehaviour.DataFile.BackgroundEnabled = audioSource.volume;
		SaveBehaviour.SetValue();
	}
}
