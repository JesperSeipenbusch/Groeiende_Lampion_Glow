using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public int groupID;

    public bool isDynamicGI;
    private Renderer rend;
    private Color color;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        color = rend.material.GetColor("_EmissionColor");

        if (isDynamicGI)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void SetIntensity(float val)
    {
        if (isDynamicGI)
        {
            DynamicGI.SetEmissive(rend, color * val);
        }
        else
        {
            rend.material.SetColor("_EmissionColor", color * val);
            Debug.Log(color * val);
        }
    }
}
