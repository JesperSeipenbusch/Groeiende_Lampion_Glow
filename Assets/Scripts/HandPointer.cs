using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointer : MonoBehaviour
{
    public GameObject visualHand;
    private Rigidbody hand;
    public Vector3 visualPos;
    public float depth;

    void Start()
    {
        visualPos = new Vector3(transform.position.x, transform.position.y, depth);
        hand = Instantiate(visualHand, visualPos, Quaternion.identity).GetComponent<Rigidbody>();
    }

    void Update()
    {
        visualPos = new Vector3(transform.position.x, transform.position.y, depth);
        hand.MovePosition(visualPos);
    }

    private void OnDestroy()
    {
        Destroy(hand.gameObject);
    }
}
