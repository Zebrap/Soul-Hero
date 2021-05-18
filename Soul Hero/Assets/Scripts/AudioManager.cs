using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private bool musicMuted;
    public AudioSource musicSource;

 public void ToggleMusic()
     {
         if( musicMuted )
             DisableMusic();
         else
             EnableMusic();
     }
    public void DisableMusic()
     {
         musicSource.mute = false;
         musicMuted = false;
     }
     
     public void EnableMusic()
     {
         musicSource.mute = true;
         musicMuted = true;
     }
}
