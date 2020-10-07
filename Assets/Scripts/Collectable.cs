using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;

    private void Awake()
    {
        startPosition = transform.position;
        target = new Vector3(0,0,10);
    }
    private void OnTriggerEnter(Collider _Coll)
    {
        CollectionManager.RemoveCollectableCount();
        StartCoroutine(MoveToPosition(this.gameObject.transform, target, 1f));
    }


    //debug method (for mouse)
    private void OnMouseEnter()
    {
        CollectionManager.RemoveCollectableCount();
        StartCoroutine(MoveToPosition(this.gameObject.transform, target, 1f));

    }


    
    private void Update()
    {
        transform.Rotate(0, 0, 50f * Time.deltaTime);
        if(transform.position == target)
        {
            DestroyAndAdd();
        }
    }


    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
   {
        var currentPos = transform.position;
        var t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
    void DestroyAndAdd()
    {
        Destroy(this.gameObject);
        CollectionManager.AddScore();
    }
}