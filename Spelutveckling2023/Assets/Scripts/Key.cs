using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    public static int keysHeld = 0;
    public UnityEvent pickedUp = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pickedUp.Invoke();
            keysHeld++;
            gameObject.SetActive(false);
        }
    }
}
