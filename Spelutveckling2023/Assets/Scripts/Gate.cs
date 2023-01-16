using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    public UnityEvent onOpen = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Key.keysHeld > 0)
        {
            Key.keysHeld--;
            onOpen.Invoke();
            gameObject.SetActive(false);
        }
    }
}
