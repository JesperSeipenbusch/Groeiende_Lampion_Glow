using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGroup
{
    public int groupID;
    List<LightScript> Lights = new List<LightScript>();

    public void Add(LightScript _Lights)
    {
        //Adds Light to LightScript List
        Lights.Add(_Lights);
    }

    //Calls LightScript to set Intensity of Emission on object
     public void SetLightIntensity(float val)
    {
        foreach (LightScript light in Lights)
        {
            light.SetIntensity(val);
        }
    }
}