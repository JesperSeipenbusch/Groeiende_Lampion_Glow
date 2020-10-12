using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField]
    static int score = 0;

    private int maxCollectables;
    static int currentCollectables;

    [Header("Prefabs")]
    public GameObject collectable;

    public GameObject flyingLantern;
    
    [Header("Timer")]
    public float timer;


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

    Vector3 CollectableRP;
    public static Vector3 flyingLanternRP;
    
    [Header("Screen corner positions")]
    [SerializeField]
    Vector3 upperRight;
    [SerializeField]
    Vector3 lowerLeft;        

    #endregion

    void Awake()
    {
        cam = Camera.main;

        timer = 2f;
        
        topMargin = 0.3f;
        bottomMargin = 0.3f;
        sideMargin = 0.3f;

        isCollectable = true;
        maxCollectables = 4;
    }
    private void FixedUpdate()
    {
        if(isCollectable)
        {
            bottomMargin = 1f;
            depth = 0f;
        }
        else
        {
            bottomMargin = 26f;
            depth = 30;
        }
        Timer();
        Findborders(depth);
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
        if(currentCollectables < maxCollectables)
        {
            SpawnCollectable();
        }
        else
        {
            SpawnFlyingLantern();
            //return;
        }
    }
    void SpawnCollectable()
    {
        isCollectable = true;
        CollectableRP = FindRandomPoint();
        Instantiate(collectable, CollectableRP, Quaternion.identity);
        currentCollectables++;
    }

    void SpawnFlyingLantern()
    {
        isCollectable = false;
        flyingLanternRP = FindRandomPoint();
    
        Vector3 spawnPoint = new Vector3(0,-5,-10);
        Instantiate(flyingLantern, spawnPoint, Quaternion.identity);
    }

    Vector3 FindRandomPoint()
    {
        Debug.Log("point is being found");
        Vector3 rp = new Vector3(Random.Range(lowerLeft.x, upperRight.x), Random.Range(lowerLeft.y, upperRight.y),  depth);

        //checks if there is another collectable nearby, if so returns and picks a new rp (random position).
        Collider[] hitColliders = Physics.OverlapSphere(rp, 1f);
        if(hitColliders.Length > 0)
        {
            Debug.Log("Found something... Finding new Random point");
            return FindRandomPoint();
        }
        else
        {
            return rp;
        }
    }
    
    #region Static Methods
        
    public static void AddScore()
    {
        score++;
    }
    public static void RemoveCollectableCount()
    {
        if(currentCollectables > 0)
        {
            currentCollectables -= 1;
        }
    }

    #endregion


}