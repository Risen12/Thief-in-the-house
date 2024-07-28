using System;
using UnityEngine;

public class HomeZoneHandler : MonoBehaviour
{
    private const string ThiefTag = "Thief";

    public event Action OnThiefEntered;
    public event Action OnThiefExited;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == ThiefTag)
            OnThiefEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ThiefTag)
            OnThiefExited?.Invoke();
    }
}
