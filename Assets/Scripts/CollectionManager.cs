using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField]
    static int score = 0;

    private int maxCollectables;
    static int currentCollectables;

    public GameObject collectable;

    public float timer;


    #region Screen
    Camera cam;
    float scWidth;
    float scHeight;
    public float margin;
    
    [SerializeField]
    Vector3 upperRight;
    [SerializeField]
    Vector3 lowerLeft;        

    #endregion

    void Awake()
    {
        cam = Camera.main;
        
        margin = 0.3f;

        maxCollectables = 4;
    }
    private void FixedUpdate()
    {
        Timer();
        Findborders();
    }
    void Findborders()
    {
        scWidth = 1/(cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f);
        scHeight = 1/(cam.WorldToViewportPoint(new Vector3(1, 1, 0)).y - .5f);

        upperRight = new Vector3(scWidth / 2 - margin, scHeight / 2 - margin, 0);
        lowerLeft = new Vector3(-scWidth / 2 + margin, -scHeight / 2 + margin, 0);
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
            return;
        }
    }
    void SpawnCollectable()
    {
        Vector3 rp;
        rp = new Vector3(Random.Range(lowerLeft.x, upperRight.x), Random.Range(lowerLeft.y, upperRight.y), 0);

        //checks if there is another collectable nearby, if so returns and picks a new rp (random position).
        Collider[] hitColliders = Physics.OverlapSphere(rp, 1f);
        Debug.Log(hitColliders.Length);
        if(hitColliders.Length > 0)
        {
            Debug.Log("Found something");
            return;
        }
        else
        {
            Instantiate(collectable, rp, Quaternion.identity);
            currentCollectables++;
        }

    }
    
    #region Static Methods
        
    public static void AddScore()
    {
        score++;
        Debug.Log(score);
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