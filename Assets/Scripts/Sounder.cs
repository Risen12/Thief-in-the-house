using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]
public class Sounder : MonoBehaviour
{
    private const string ThiefInHouseState = "IsThiefInHouse";

    [SerializeField] private AudioClip _sound;
    [SerializeField] private float _changeVolumeSpeed;

    private AudioSource _audioSource;
    private Coroutine _changeVolumeCoroutine;
    private Animator _animator;
    private float _activateVolume;
    private float _deactivateVolume;

    public event Action<bool> OnChangeState;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _sound;
        _audioSource.volume = 0f;
        _animator = GetComponent<Animator>();

        _activateVolume = 1f;
        _deactivateVolume = 0f;

        _audioSource.Play();
    }

    public void ChangeState(bool state)
    {
        _animator.SetBool(ThiefInHouseState, state);
        OnChangeState?.Invoke(state);

        Stop(_changeVolumeCoroutine);

        if (state)
        {
            _changeVolumeCoroutine = StartCoroutine(ChangeVolume(_activateVolume));
        }
        else
        {
            _changeVolumeCoroutine = StartCoroutine(ChangeVolume(_deactivateVolume));
        }
    }

    private IEnumerator ChangeVolume(float volume)
    {
        while (_audioSource.volume != volume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _changeVolumeSpeed * Time.deltaTime);

            yield return null;
        }

        yield break;
    }

    private void Stop(Coroutine coroutine)
    { 
        if(coroutine != null)
            StopCoroutine(coroutine);
    }
}
