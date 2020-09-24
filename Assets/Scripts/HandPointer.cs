using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Light"))
        {
            Debug.Log("Collected light");
            StartCoroutine(Lights(col.gameObject));
        }
    }

    private IEnumerator Lights(GameObject ob)
    {
        float i = Random.Range(2.0f, 4.5f);
        ob.SetActive(false);
        yield return new WaitForSeconds(i);
        ob.SetActive(true);
    }
}
