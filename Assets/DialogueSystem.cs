using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    // The text including syntax
    public string text;
    // The text not including any syntax (what is shown)
    private string draftText;

    // The text that is being used 
    public GameObject letterPrefab;

    // Delay in between the letters
    public float typeSpeed;

    // Values in order to use the proper placement with singular letters
    private string visable = "<alpha=#FF>";
    private string invisable = "<alpha=#00>";

    // List of characters instantiated
    private List<GameObject> chars = new List<GameObject>();
    // List of the text effect being applied to the letters
    private List<TextEffects> textFx = new List<TextEffects>();

    // If the text is still being added too
    private bool typing;
    // If an effect will be added to the current character
    private bool setFx;

    // The canvas in the scene
    private GameObject canvas;

    // Different audio clips that can be played
    public AudioClip[] audioClips;

    // The amount of audio loops 
    private int audioCount;

    // The random amounts of loops for the current cycle
    private int randAudioLoop;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Typing());
        }
    }

    public IEnumerator Typing()
    {
        typing = true;
        // Clear all Text effects
        textFx.Clear();
        textFx.Add(new TextEffects());
        // Get text length
        for (int i = 0; i < text.Length; i++)
        {
            // * = Shake effect in syntax
            if (text[i].ToString() == "*")
            {
                AddTextFX(i, 1);
            }
            // @ = Color hue effect
            if(text[i].ToString() == "@")
            {
                AddTextFX(i, 2);
            }
        }
        // Remove all syntax from text and set to draft text
        draftText = Regex.Replace(text, "[^\\w\\._,!.'? ]", "");
        int x = 0;
        // For all char in draft text
        for (int i = 0; i < draftText.Length; i++)
        {
            // If there are text effects
            if (textFx.Count > 1)
            {
                // If the text effect isn't blank
                if (textFx[x].effect != 0)
                {
                    if (i == textFx[x].start)
                    {
                        setFx = true;
                    }
                    if (i == textFx[x].end)
                    {
                        setFx = false;
                        x += 1;
                    }
                }
            }
            // Set default to invisable
            string charText = draftText.Insert(0, invisable);
            // Set where the current char is + amount taken by previous text to visable
            charText = charText.Insert(i + 11, visable);
            // Set after current char + amount taken by previous text to invisable
            charText = charText.Insert(i + 23, invisable);
            GameObject curChar = Instantiate(letterPrefab, canvas.transform);
            curChar.GetComponent<TextMeshProUGUI>().text = charText;
            // *TEMP* Set startup to desired start
            curChar.GetComponent<TextEffect>().startup = 1;
            chars.Add(curChar);
            if (text[i].ToString() != " ")
            {
                if(audioCount == 0)
                {
                    int rand = Random.Range(1, 101);
                    if(rand < 21)
                    {
                        randAudioLoop = 0;
                    }
                    if(20 < rand && rand < 41)
                    {
                        randAudioLoop = 2;
                    }
                    if(40 < rand)
                    {
                        randAudioLoop = 1;
                    }
                }
                if (audioCount < randAudioLoop)
                {
                    audioCount += 1;
                }
                else
                {
                    // Play random audio clip
                    canvas.GetComponent<AudioSource>().PlayOneShot(audioClips[Random.Range(0, audioClips.Length)], 1);
                    audioCount = 0;
                }
            }
            if (setFx)
            {
                curChar.GetComponent<TextEffect>().effect = textFx[x].effect;
            }
            yield return new WaitForSeconds(typeSpeed);
        }
        typing = false;
    }

    public void AddTextFX(int i, int fx)
    {
        // If latest effect isn't enabled to anything
        if (textFx[textFx.Count - 1].effect == 0)
        {
            // The start of the text effect is equal to the current text char amount minus removing sytax from all of the effects
            textFx[textFx.Count - 1].start = i - ((textFx.Count - 1) * 2);
            // 1 = Shake effect in code
            textFx[textFx.Count - 1].effect = fx;
        }
        // If latest effect already enabled
        else
        {
            // End of the text is equal to current text char minus the amount of syntax plus one
            textFx[textFx.Count - 1].end = i - (textFx.Count * 2) + 1;
            // Add an disabled text effect
            textFx.Add(new TextEffects());
        }
    }
}

[System.Serializable]
public class TextEffects
{
    public int effect;
    public int start;
    public int end;
}