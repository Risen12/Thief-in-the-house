using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Liver : MonoBehaviour
{
    private const string DogBarkingParameterName = "IsDogBarking";

    [SerializeField] private Sounder _sounder;

    private Animator _animator;

    private void OnEnable()
    {
        _sounder.OnChangeState += OnSounderChangeState;
    }

    private void OnDisable()
    {
        _sounder.OnChangeState -= OnSounderChangeState;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnSounderChangeState(bool state)
    {
        _animator.SetBool(DogBarkingParameterName, state);
    }
}
