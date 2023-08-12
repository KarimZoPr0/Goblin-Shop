using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
   [SerializeField] private AudioSource source;

   public void Play(AudioClip clip)
   {
      source.clip = clip;
      source.Play();
   }

   public void Stop() => source.Stop();
}
