using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Sources")] [SerializeField]
    private AudioSource mainSource;

    [SerializeField] private AudioSource soundAudioSource;

    [Header("Audio Clips")] [SerializeField]
    private List<Audio> audioList;

    [SerializeField] private List<Audio> sfxList;

    private Dictionary<string, AudioClip> audioDictionary;
    private Dictionary<string, AudioClip> sfxDictionary;

    public override void Awake()
    {
        base.Awake();

        audioDictionary = new Dictionary<string, AudioClip>();

        foreach (var audio in audioList.Where(audio => !audioDictionary.ContainsKey(audio.name)))
            audioDictionary.Add(audio.name, audio.audioClip);

        sfxDictionary = new Dictionary<string, AudioClip>();

        foreach (var audio in sfxList.Where(audio => !sfxDictionary.ContainsKey(audio.name)))
            sfxDictionary.Add(audio.name, audio.audioClip);
    }

    public static void PlayMain(string soundName)
    {
        if (!Instance.audioDictionary.TryGetValue(soundName, out var clip)) return;
        if (Instance.mainSource.isPlaying)
        {
            Instance.mainSource.Stop();
        }        
        Instance.mainSource.clip = clip;
        Instance.mainSource.Play();
    }

    public static void PlaySfx(string soundName, float pitch = 1f, float volume = 1f)
    {
        if (!Instance.sfxDictionary.TryGetValue(soundName, out var clip)) return;
        Instance.soundAudioSource.pitch = pitch;
        Instance.soundAudioSource.volume = volume;
        Instance.soundAudioSource.PlayOneShot(clip);
    }
}

[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip audioClip;
}