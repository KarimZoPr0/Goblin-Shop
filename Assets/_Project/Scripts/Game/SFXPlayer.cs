using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSS.Audio
{
    
    [Serializable]
    public struct Sounds
    {
        public string name;
        public AudioClip clip;
    }
    public class SFXPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        
        public Sounds[] sounds;
        
        public void PlaySound(string soundName)
        {
            for (var i = 0; i < sounds.Length; i++)
            {
                var sound = sounds[i];
                if (sound.name == soundName)
                {
                    source.PlayOneShot(sound.clip);
                }
            }
        }
    }

}
