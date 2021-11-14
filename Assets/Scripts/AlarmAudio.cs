using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmAudio : MonoBehaviour
{
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _changeDuration = 3f;

    private AudioSource _audioSource;
    private Coroutine _activeCoroutine;

    private void OnValidate()
    {
        _maxVolume = Mathf.Clamp(_maxVolume, 0.1f, 1f);
        _minVolume = Mathf.Clamp(_minVolume, 0f, _maxVolume);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    public void Play()
    {
        StopActiveCoroutine();
        _activeCoroutine = StartCoroutine(FadeInAudioVolume());
    }

    public void Stop()
    {
        StopActiveCoroutine();
        _activeCoroutine = StartCoroutine(FadeOutAudioVolume());
    }

    private IEnumerator FadeInAudioVolume()
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        yield return ChangeAudioVolume(_minVolume, _maxVolume);
        Debug.Log("Alarm reached full power.");
    }

    private IEnumerator FadeOutAudioVolume()
    {
        yield return ChangeAudioVolume(_maxVolume, _minVolume);
        _audioSource.Stop();
        Debug.Log("Alarm stopped.");
    }

    private IEnumerator ChangeAudioVolume(float from, float to)
    {
        float totalVolumeChange = Mathf.Abs(to - from);
        float changeProgress = 0f;
        float volumeChangePerSecond = (totalVolumeChange / _changeDuration);

        while (changeProgress < 1)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, to, volumeChangePerSecond * Time.deltaTime);
            changeProgress = Mathf.Abs(_audioSource.volume - from) / totalVolumeChange;

            yield return null;
        }
    }

    private void StopActiveCoroutine()
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }
    }
}