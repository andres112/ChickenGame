
using UnityEngine;
[System.Serializable] // this clas is required for SerializeField

public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] // this allows to create a slide instead an input in unity
    public float volume = 0.5f;
    [Range(-0.5f, 1.5f)]
    public float pitch = 1f;
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();        
    }

    public void Stop()
    {
        source.Stop();
    }

}
public class AudioManager : MonoBehaviour
{
    public static AudioManager audio;
    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (audio != null)
        {
            Debug.LogError("More than AudioManager in the scene.");
        }
        else
        {
            audio = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            AudioSource _source = _go.AddComponent<AudioSource>();
            sounds[i].SetSource(_source);
        }
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

        // no sound with that name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        // no sound with that name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }
}
