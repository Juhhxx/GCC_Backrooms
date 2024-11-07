using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundsScript : MonoBehaviour
{
    [field:SerializeField] public AudioClip[] SoundToPlay { get; set; }
    private int _soundIndex = 0;

    public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(SoundToPlay[_soundIndex], transform.position,1f);
        _soundIndex++;

        if (_soundIndex >= SoundToPlay.Count()) _soundIndex = 0;
    }
}
