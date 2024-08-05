using UnityEngine;

public class HomeZoneHandler : MonoBehaviour
{
    [SerializeField] private Sounder _sounder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Thief thief))
            ChangeSounderState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Thief thief))
            ChangeSounderState(false);
    }

    private void ChangeSounderState(bool state) => _sounder.ChangeState(state);
}
