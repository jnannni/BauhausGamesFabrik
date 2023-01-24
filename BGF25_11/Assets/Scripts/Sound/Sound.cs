using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    public bool loop;

    [Range(0f, 1f)]
    public float pitch;
    [Range(.11f, 3f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
