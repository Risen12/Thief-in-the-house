using UnityEngine;

public class HomeZoneHandler : MonoBehaviour
{
    [SerializeField] private Sounder _sounder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Thief thief))
            _sounder.IncreaseSound();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Thief thief))
            _sounder.DecreaseSound();
    }
}
