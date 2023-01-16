using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectGravity : MonoBehaviour
{
    const int DAMAGE = 1;

    //Settings
    public float fallSpeed = 9.82f;
    public Vector3 halfSize = new Vector3(0.5f, 0.5f, 0.5f);  //The radius of the box in every axis
    public UnityEvent hitGround = new UnityEvent();

    float velocity = 0f;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float stepDistance;

        velocity += fallSpeed * Time.deltaTime;
        stepDistance = velocity * Time.deltaTime;

        //Trace for collisions
        if (Physics.BoxCast(transform.position, halfSize, Vector3.down, out hit, transform.rotation, stepDistance))
        {
            if (!hit.collider.isTrigger)
            {
                stepDistance = hit.distance;

                IDamage damageHit = hit.collider.gameObject.GetComponent<IDamage>();

                if (damageHit != null)
                {
                    damageHit.OnDamage(DAMAGE);
                    Destroy(gameObject);
                    return;
                }

                hitGround.Invoke();

                ScoreManager.IncreaseScore();
                Destroy(this);
            }
        }

        //Applies transform change (Move "stepDistance" down Y-axis)
        transform.position += Vector3.down * stepDistance;

        //Destroy objects falling under the level
        if (transform.position.y < -2)
            Destroy(gameObject);
    }

}
