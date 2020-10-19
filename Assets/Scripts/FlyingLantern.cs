using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingLantern : MonoBehaviour
{
    public Vector3 targetPos;

    float timeToMove = 1f;

    void Awake()
    {
        targetPos = CollectionManager.Instance.flyingLanternRP;
        StartCoroutine(MoveToPosition(this.gameObject.transform, targetPos, 1f));        
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, targetPos, t);
            yield return null;
        }
    }
}

