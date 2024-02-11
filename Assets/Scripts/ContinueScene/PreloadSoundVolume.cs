using UnityEngine;

public class PreloadSoundVolume : MonoBehaviour
{
	[SerializeField] private AudioSource[] sounds;

	private void Start()
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			sounds[i].volume = SaveBehaviour.DataFile.EfxEnabled;
		}
	}
}
