using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hello : MonoBehaviour
{
    public int nummer;
    public float flyttal;
    public string text;
    public UnityEngine.Events.UnityEvent OnHello;

    void Start()
    {
        Hello();
    }

    public void Hello()
    {
        Debug.Log("Hello, World!");
        OnHello.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Hello();
        }
    }
}
