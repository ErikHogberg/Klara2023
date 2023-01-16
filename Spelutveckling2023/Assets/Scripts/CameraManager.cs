using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Singleton variable
    private static float highestY = 0f;

    public GameObject player;
    public Vector3 cameraMoveThreshold = new Vector3(4,3,4);

    Vector3 initialOffset;
    float yThresholdBias;

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - player.transform.position;
        yThresholdBias = cameraMoveThreshold.y * -.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 refPosition = transform.position - initialOffset;
        Vector3 offset = new Vector3();

        highestY = Mathf.Max(highestY, player.transform.position.y);

        offset.x = CalcOffset(refPosition.x, player.transform.position.x, cameraMoveThreshold.x);
        offset.y = CalcOffset(refPosition.y + yThresholdBias, player.transform.position.y, cameraMoveThreshold.y);
        offset.z = CalcOffset(refPosition.z, player.transform.position.z, cameraMoveThreshold.z);


        transform.position = transform.position + offset;
    }

    float CalcOffset(float source, float target, float threshold)
    {
        return Mathf.Max(0, Mathf.Abs(source - target) - threshold) * Mathf.Sign(target - source);
    }

    public static float GetHighestY()
    {
        return highestY;
    }
}
