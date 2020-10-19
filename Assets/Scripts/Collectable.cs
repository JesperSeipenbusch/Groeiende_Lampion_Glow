using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    float t;
    Vector3 startPosition;
    Vector3 endPos;
    float timeToReachTarget;


    [Header("Targets")]
    public GameObject[] targets = new GameObject[0];
    int currTarget;
    float minDistance;
    public Vector3 target;
    public Vector3 dir;
    public float speed;
    public float angularSpeed;


    private void Awake()
    {
        CollectionManager.Instance.FindTarget();

        speed = 0.03f;
        angularSpeed = 0.05f;

        startPosition = transform.position;
        endPos = new Vector3(0,0,10);

        dir = new Vector3(Random.value, Random.value, 0);
        currTarget = Random.Range(0, 6);
        minDistance = 0.03f;
    }
    private void OnTriggerEnter(Collider _Coll)
    {
        CollectionManager.Instance.RemoveCollectableCount();
        StartCoroutine(MoveToPosition(this.gameObject.transform, endPos, 1f));
    }


    //debug method (for mouse)
    private void OnMouseEnter()
    {
        CollectionManager.Instance.RemoveCollectableCount();
        StartCoroutine(MoveToPosition(this.gameObject.transform, endPos, 1f));
    }


    
    private void Update()
    {
        if(transform.position == endPos)
        {
            DestroyAndAdd();
        }
        StartCoroutine(MoveAbout());
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
        CollectionManager.Instance.AddScore();
    }


    public IEnumerator MoveAbout()
    {
        target = CollectionManager.Instance.targetRP; 
        Vector3 v = target - transform.position;
        v.Normalize();
        dir = Vector3.Slerp(dir, v, angularSpeed);
        dir.Normalize();
        transform.position += dir * speed;
        CheckIfClose();
        yield return new WaitForSeconds(0);
    }

    void CheckIfClose()
    {
        if(Vector3.Distance(transform.position, target) < minDistance)
        {
            CollectionManager.Instance.FindTarget();
        }
    }

}