using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] [Range (0,1)] private float _soundVolume;
    [SerializeField] bool _isLoop;
    [SerializeField] bool _playOnAwake;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _soundVolume;
        _audioSource.loop = _isLoop;
        _audioSource.playOnAwake = _playOnAwake;
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
}
