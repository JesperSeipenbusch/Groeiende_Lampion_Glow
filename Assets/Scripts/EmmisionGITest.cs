using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmisionGITest : MonoBehaviour
{
    Color color;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        color = rend.material.GetColor("_EmissionColor");
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float value = Mathf.Sin(Time.realtimeSinceStartup * 3.0f) * 0.5f + 0.5f;
        Debug.Log(value);
        DynamicGI.SetEmissive(rend, color * value);
    }
}
