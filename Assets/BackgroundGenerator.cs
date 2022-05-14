using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject bgObj;

    public int colRowAmount;
    public int dist;

    private float x;
    private float y;

    private int z;

    private int fi;

    public Vector2 posCorrection;
    public int rot;

    public float posInAnim;

    private float maxPosInAnim = 5 / 0.1f;

    void Start()
    {
        GenerateBG();
    }

    public void GenerateBG()
    {
        fi = 0;
        for(int i = 0; i < colRowAmount * colRowAmount; i++)
        {
            x = (dist * (colRowAmount / 2)) + (dist * fi) + posCorrection.x;
            y = (dist * (colRowAmount / 2) - (dist * z)) + posCorrection.y;
            fi += 1;
            if(fi == colRowAmount)
            {
                z += 1;
                fi = 0;
            }
            GameObject obj = Instantiate(bgObj, new Vector2(x, y), Quaternion.identity);
            obj.transform.parent = gameObject.transform;
            obj.AddComponent<BackgroundOptimization>();
        }
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    void Update()
    {
        if(posInAnim >= maxPosInAnim)
        {
            posInAnim = 0;
        }
        posInAnim += Time.deltaTime;
    }
}
