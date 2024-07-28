using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Sounder : MonoBehaviour
{
    private const string ThiefInHouseState = "IsThiefInHouse";

    [SerializeField] private HomeZoneHandler _homeZone;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private float _changeVolumeSpeed;

    private AudioSource _audioSource;
    private bool _isThiefInHouse;
    private Coroutine _raiseSound;
    private Coroutine _decreaseSound;
    private Animator _animator;

    private void OnEnable()
    {
        _homeZone.OnThiefEntered += () => 
        { 
            _isThiefInHouse = true;
            _animator.SetBool(ThiefInHouseState, _isThiefInHouse);
            _raiseSound = StartCoroutine(RaiseSound());
            Stop(_decreaseSound);
        };
        _homeZone.OnThiefExited += () => 
        { 
            _isThiefInHouse = false;
            _animator.SetBool(ThiefInHouseState, _isThiefInHouse);
            _decreaseSound = StartCoroutine(DecreaseSound());
            Stop(_raiseSound);
        };
    }

    private void OnDisable()
    {
        _homeZone.OnThiefEntered -= () =>
        {
            _isThiefInHouse = true;
            _raiseSound = StartCoroutine(RaiseSound());
            Stop(_decreaseSound);
        };
        _homeZone.OnThiefExited -= () =>
        {
            _isThiefInHouse = false;
            _decreaseSound = StartCoroutine(DecreaseSound());
            Stop(_raiseSound);
        };

        Stop(_decreaseSound);
        Stop(_raiseSound);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _sound;
        _isThiefInHouse = false;
        _audioSource.volume = 0f;
        _audioSource.Play();
        _animator = GetComponent<Animator>();
    }

    private IEnumerator RaiseSound()
    {
        float maxVolume = 1f;

        while (_isThiefInHouse)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, maxVolume, _changeVolumeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator DecreaseSound()
    {
        float minVolume = 0f;

        while (_isThiefInHouse == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, minVolume, _changeVolumeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void Stop(Coroutine coroutine)
    { 
        if(coroutine != null)
            StopCoroutine(coroutine);
    }
}
