using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	[SerializeField]
	AudioSource[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one AudioManager in the scene.");
		}
		else
		{
			instance = this;
		}
	}

	void Start()
	{
		sounds = GetComponentsInChildren<AudioSource>();
	}

	public void PlaySound(string _name)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				sounds[i].Play();
				return;
			}
		}

		// no sound with _name
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
	}

}
