using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider _Coll)
    {
        CollectionManager.AddScore();
        Destroy(this.gameObject);
    }


    //debug method (for mouse)
    private void OnMouseOver()
    {
        CollectionManager.AddScore();
        CollectionManager.RemoveCollectableCount();
        Destroy(this.gameObject);
    }


    
    private void Update()
    {
        transform.Rotate(0, 0, 50f * Time.deltaTime);
    }
}