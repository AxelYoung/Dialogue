/*using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public string desiredText;
    public GameObject letterPrefab;

    private string visable = "<alpha=#FF>";
    private string invisable = "<alpha=#00>";

    public float typeSpeed;

    public List<GameObject> characters = new List<GameObject>();

    public bool typing;

    private bool setFx;

    private string draftText;

    public List<TextEffects> textFx = new List<TextEffects>();

    public GameObject canvas;

    public GameObject player;
    public Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Typing(desiredText, letterPrefab, true, 0));
    }

    public IEnumerator Typing(string txt, GameObject prefab, bool ifQuestion, int dir)
    {
        typing = true;
        textFx.Clear();
        textFx.Add(new TextEffects());
        for (int i = 0; i < txt.Length; i++)
        {
            if (txt[i].ToString() == "*")
            {
                if (textFx[textFx.Count - 1].effect == null)
                {
                    textFx[textFx.Count - 1].start = i - ((textFx.Count - 1) * 2);
                    textFx[textFx.Count - 1].effect = "Shake";
                }
                else
                {
                    textFx[textFx.Count - 1].end = i - (textFx.Count * 2) + 1;
                    textFx.Add(new TextEffects());
                }
            }
            if (txt[i].ToString() == "@")
            {
                if (textFx[textFx.Count - 1].effect == null)
                {
                    textFx[textFx.Count - 1].start = i - ((textFx.Count - 1) * 2);
                    textFx[textFx.Count - 1].effect = "Fade";
                }
                else
                {
                    textFx[textFx.Count - 1].end = i - (textFx.Count * 2) + 1;
                    textFx.Add(new TextEffects());
                }
            }
        }
        draftText = Regex.Replace(txt, "[^\\w\\._,!.'? ]", "");
        int x = 0;
        for (int i = 0; i < draftText.Length; i++)
        {
            if(textFx.Count != 0)
            {
                if (textFx[x].effect != null)
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
            string charText = draftText.Insert(0, invisable);
            charText = charText.Insert(i + 11, visable);
            charText = charText.Insert(i + 23, invisable);
            GameObject curChar = Instantiate(prefab, canvas.transform.GetChild(0)).transform.GetChild(0).gameObject;
            curChar.GetComponent<TextMeshProUGUI>().text = charText;
            curChar.GetComponent<Animator>().SetInteger("Fallin", 0);
            characters.Add(curChar);
            if (setFx)
            {
                curChar.GetComponent<Animator>().SetBool(textFx[x].effect, true);
            }
            if (txt[i].ToString() != " ")
            {
                yield return new WaitForSeconds(typeSpeed);
            }
        }
        typing = false;
        if (ifQuestion)
        {
            canvas.transform.GetChild(0).GetComponent<Animator>().SetBool("Open", true);
            StartCoroutine(PlayerSpawn());
        }
    }

    public IEnumerator PlayerSpawn()
    {
        yield return new WaitForSeconds(1 + (1/6));
        if(player != null)
        {
            Instantiate(player, spawnPoint, transform.rotation);
        }
    }

    public IEnumerator RemoveText()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].GetComponent<Animator>().SetBool("Remove", true);
            if (desiredText[i].ToString() != " ")
            {
                yield return new WaitForSeconds(typeSpeed);
            }
        }
    }
}*/