using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private static LightManager _instance;

    public List<LightScript> AllLights;
    public List<LightGroup> Groups = new List<LightGroup>();

    //LightGroup GetOrCreate(LightGroup group);

    #region Singleton
    public static LightManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if(_instance == null)
            {
                _instance = value;
                DontDestroyOnLoad(_instance);
            }
            else
            {
                Destroy(value.gameObject);
            }
        }
    }
    

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject light in GameObject.FindGameObjectsWithTag("Light"))
        {
            AllLights.Add(light.GetComponent<LightScript>());
        }
        //Adds Lights to appropriate LightGroup
        foreach (LightScript l in AllLights)
        {
            LightGroup lGroep = GetOrCreate(l.groupID);
            lGroep.Add(l);
        }
    }

    LightGroup GetOrCreate(int groupID)
    {
        //Gets Lightgroup
        for (int i = 0; i < Groups.Count; i++)
        {
            if (Groups[i].groupID == groupID) 
            {
                return Groups[i];
            }
        }

        //Creates a new LightGroup if no Lightgroup is found
        LightGroup lg = new LightGroup();
        lg.groupID = groupID;
        Groups.Add(lg);

        return lg;
    }

    private void Update()
    {
        //          DEBUG
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Blackout();
        //}
    }

    //Turns all lights off
    void Blackout()
    {
        for (int i = 0; i < AllLights.Count; i++)
        {
            //AllLights[i].gameObject.SetActive(false);

            LightScript l = AllLights[i].GetComponent<LightScript>();
            l.SetIntensity(0);
        }
    }
}
