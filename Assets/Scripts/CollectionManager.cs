﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance;

    [SerializeField]
    int score = 0;
    int Score
    {
        get { return score; }
        set
        {
            score = value;
            LightManager.Instance.SetScore(score);
        }
    }

    private int maxCollectables;
    static int currentCollectables;

    [Header("Prefabs")]
    public GameObject collectable;

    public GameObject flyingLantern;

    public GameObject sun;
    
    [Header("Timer")]
    public float timer;
    public float sunDown;
    [Header("Position")]
    public Vector3 sunPos;

    private bool isRestarting;

    #region Screen
    Camera cam;
    float scWidth;
    float scHeight;

    public bool isCollectable;
    [Header("Screen margins")]
    public float topMargin;
    public float bottomMargin;
    public float sideMargin;

    float depth;

    [HideInInspector]
    public Vector3 CollectableRP;
    [HideInInspector]
    public Vector3 flyingLanternRP;
    [HideInInspector]
    public Vector3 targetRP;
    
    [Header("Screen corner positions")]
    [SerializeField]
    Vector3 upperRight;
    [SerializeField]
    Vector3 lowerLeft;        

    #endregion


    public bool IsCollectable
    {
        get {return isCollectable; }
        set 
        {
            isCollectable = value; 
            if(isCollectable)
            {
                bottomMargin = 7.6f;
                depth = 0f;
            }
            else
            {
                bottomMargin = 26f;
                depth = 30;
            }
        }
    }
    void Awake()
    {
        Instance = this;

        cam = Camera.main;

        timer = 2f;
        
        topMargin = 0.3f;
        bottomMargin = 7.6f;
        sideMargin = 0.3f;

        IsCollectable = true;
        maxCollectables = 4;
    }
    private void FixedUpdate()
    {
        if(score < LightManager.Instance.maxScore)
        {
            Timer();
            Findborders(depth);
        }
        else if (!isRestarting)
        {
            StartCoroutine(Restart());
            isRestarting = true;
        }
    }
    void Findborders(float d)
    {
        scWidth = 1/(cam.WorldToViewportPoint(new Vector3(1, 1, d)).x - .5f);
        scHeight = 1/(cam.WorldToViewportPoint(new Vector3(1, 1, d)).y - .5f);

        upperRight = new Vector3(scWidth / 2 - sideMargin, scHeight / 2 - topMargin, 0);
        lowerLeft = new Vector3(-scWidth / 2 + sideMargin, -scHeight / 2 + bottomMargin, 0);
    }

    void Timer()
    {
        if(timer <= 0)
        {
            timer = Random.Range(0f, 5f);
            CheckMax();
        }
        else
        {
            timer -= 1f * Time.deltaTime;
        }
    }

    void CheckMax()
    {
        if(currentCollectables < maxCollectables && (currentCollectables + score) < LightManager.Instance.maxScore)
        {
            SpawnCollectable();
        }
    }
    void SpawnCollectable()
    {
        IsCollectable = true;
        CollectableRP = FindRandomPoint();
        Instantiate(collectable, CollectableRP, Quaternion.identity);
        currentCollectables++;
    }

    public void FindTarget()
    {
        IsCollectable = true;
        targetRP = FindRandomPoint();
    }

    public void SpawnFlyingLantern()
    {
        IsCollectable = false;
        flyingLanternRP = FindRandomPoint();
    
        Vector3 spawnPoint = new Vector3(0,-5,-10);
        Instantiate(flyingLantern, spawnPoint, Quaternion.identity);
    }

    Vector3 FindRandomPoint()
    {
        //Debug.Log("point is being found");
        Vector3 rp = new Vector3(Random.Range(lowerLeft.x, upperRight.x), Random.Range(lowerLeft.y, upperRight.y),  depth);

        //checks if there is another collectable nearby, if so returns and picks a new rp (random position).
        Collider[] hitColliders = Physics.OverlapSphere(rp, 1f, 0);
        if(hitColliders.Length > 0)
        {
            //Debug.Log("Found something... Finding new Random point");
            return FindRandomPoint();
        }
        else
        {
            return rp;
        }
    }
            
    public void AddScore()
    {
        Score++;
    }
    public void RemoveCollectableCount()
    {
        if(currentCollectables > 0)
        {
            currentCollectables -= 1;
        }
    }

    public IEnumerator Restart()
    {
        Instantiate(sun, sunPos, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Score = 0;
        LightManager.Instance.Blackout();
        yield return new WaitForSeconds(2.5f);
        isRestarting = false;
    }
}