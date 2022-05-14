using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{

    public int startup;
    public int effect;

    private TextMeshProUGUI tmp;

    private float startupTime = 0.04f;
    private float currentTime = 0.0f;
    private bool startupAnim;
    private Color32 startColor;

    private float irritationIntensity = 5;
    private float irritationSpeed = 0.02f;
    private bool irritate;
    private Vector2 startPos;
    private Vector2 randOffset;
    private Vector3 velocity = new Vector3();

    private float colorHueTime = 0.1f;
    private bool reset;
    private Color32[] colors = new Color32[6];
    private int colorInt = -1;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        if (startup == 1)
        {
            startupAnim = true;
            startColor = tmp.color;
            tmp.color = new Color32(startColor.r, startColor.g, startColor.b, 0);
            startColor = tmp.color;
        }
        if(effect == 2)
        {
            colors[0] = new Color32(255, 0, 0, 255);
            colors[1] = new Color32(255, 0, 255, 255);
            colors[2] = new Color32(0, 0, 255, 255);
            colors[3] = new Color32(0, 255, 255, 255);
            colors[4] = new Color32(0, 255, 0, 255);
            colors[5] = new Color32(255, 255, 0, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startup == 1 && startupAnim)
        {
            if (currentTime <= startupTime)
            {
                currentTime += Time.deltaTime;
                tmp.color = Color32.Lerp(startColor, new Color32(startColor.r, startColor.g, startColor.b, 255), currentTime / startupTime);
            }
            else
            {
                tmp.color = new Color32(startColor.r, startColor.g, startColor.b, 255);
                currentTime = 0f;
                startupAnim = false;
            }
        }
        if (!startupAnim)
        {
            if (effect == 1)
            {
                if (!irritate)
                {
                    startPos = transform.position;
                    StartCoroutine(Irritation());
                }
            }
            if(effect == 2)
            {
                if (reset)
                {
                    if (currentTime <= colorHueTime)
                    {
                        currentTime += Time.deltaTime;
                        tmp.color = Color32.Lerp(startColor, colors[colorInt], currentTime / colorHueTime);
                    }
                    else
                    {
                        tmp.color = colors[colorInt];
                        currentTime = 0f;
                        reset = false;
                    }
                }
                else
                {
                    startColor = tmp.color;
                    if(colorInt == colors.Length - 1)
                    {
                        colorInt = 0;
                    }
                    else
                    {
                        colorInt += 1;
                    }
                    reset = true;
                }
            }
        }
        if (irritate)
        {
            Vector3 desiredPosition = startPos + randOffset;
            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, irritationSpeed);
            transform.position = smoothPosition;
        }
        
    }

    public IEnumerator Irritation()
    {
        irritate = true;
        randOffset = new Vector2(Random.Range(-irritationIntensity, irritationIntensity), Random.Range(-irritationIntensity, irritationIntensity));
        yield return new WaitForSeconds(Random.Range(0.02f, 0.1f));
        StartCoroutine(Irritation());
    }
}
