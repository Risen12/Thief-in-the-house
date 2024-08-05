using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Mover : MonoBehaviour
{
    private const string Horizontal = "Mouse X";
    private const string Vertical = nameof(Vertical);

    private readonly int _moveTrigger = Animator.StringToHash("Move");
    private readonly int _standTrigger = Animator.StringToHash("Stand");

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    { 
        float direction = Input.GetAxis(Vertical);

        float distance = direction * _moveSpeed * Time.deltaTime;

        if (distance == 0)
        {
            _animator.ResetTrigger(_moveTrigger);
            _animator.SetTrigger(_standTrigger);
        }
        else
        {
            _animator.ResetTrigger(_standTrigger);
            _animator.SetTrigger(_moveTrigger);
        }

        transform.Translate(distance * Vector3.forward);
    }

    private void Rotate()
    {
        float rotation = Input.GetAxis(Horizontal);

        transform.Rotate(rotation * _rotationSpeed * Time.deltaTime * Vector3.up);
    }
}
