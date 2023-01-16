using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onTriggered = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onTriggered.Invoke();
            gameObject.SetActive(false);
        }
    }
}
