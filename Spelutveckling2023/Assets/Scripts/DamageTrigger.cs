using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public int damage = 3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IDamage o = other.gameObject.GetComponent<IDamage>();

            if (o != null)
                o.OnDamage(damage);
        }
    }
}
