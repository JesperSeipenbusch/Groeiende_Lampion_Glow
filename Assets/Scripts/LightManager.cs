using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private static LightManager _instance;

    private float score;
    private float oldScore = 0;
    public float maxScore;

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
        Blackout();
    }

    public void SetScore(int _score)
    {
        score = _score;
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
        if(score > 0)
        {
            Groups[0].SetLightIntensity(score / maxScore);
            Debug.Log(score / maxScore);
        }

        if(score > 2 && score != oldScore)
        {
            CollectionManager.Instance.SpawnFlyingLantern();
            oldScore = score;
        }

        if(score > 9)
        {
            Groups[1].SetLightIntensity((score - 9) / (maxScore - 9));
            Debug.Log((score - 9) / (maxScore - 9));
        }


        //          DEBUG
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Blackout();
        //}


        //float value = Mathf.Sin(Time.realtimeSinceStartup * 3.0f) * 0.5f + 0.5f;
        //foreach (LightScript light in AllLights)
        //{
        //    light.SetIntensity(value);
        //}
    }

    //Turns all lights off
    public void Blackout()
    {
        for (int i = 0; i < AllLights.Count; i++)
        {
            //AllLights[i].gameObject.SetActive(false);

            LightScript l = AllLights[i].GetComponent<LightScript>();
            l.SetIntensity(0);
        }
    }
}
