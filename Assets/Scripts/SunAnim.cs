using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAnim : MonoBehaviour
{
    public float scaleValue;
    private Vector3 startPos;
    public Vector3 midPos;
    public Vector3 endPos;
    private float checkpoint;

    public float timeToScale;
    public float timeToSunDown;
    private float timer;
    private float startTime;

    private Vector3 endScale;

    private void Start()
    {
        startTime = Time.time;
        endScale = new Vector3(scaleValue, scaleValue, scaleValue);
        startPos = transform.position;
        checkpoint = midPos.z;
        Destroy(gameObject, 6.0f);
    }

    private void Update()
    {
        timer = Time.time;

        if(transform.localScale.x < scaleValue)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, endScale, (timer - startTime) / timeToScale);
        }
        else
        {
            if(transform.position.z < checkpoint)
            {
                transform.position = Vector3.Slerp(startPos, midPos, ((timer - timeToScale) - startTime) / timeToSunDown);
            }
            else
            {
                transform.position = Vector3.Slerp(midPos, endPos, ((timer - timeToScale - timeToSunDown) - startTime) / timeToSunDown);
            }

            transform.localScale = Vector3.Lerp(transform.localScale, (endScale * 2), ((timer - timeToScale) - startTime) / (timeToSunDown * 2));
        }
    }
}
