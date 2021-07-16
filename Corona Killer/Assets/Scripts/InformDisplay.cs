using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformDisplay : MonoBehaviour {

    Text informText;
    GameSession gameSession;

    bool showText = false;

	// Use this for initialization
	void Start () {
        gameSession = FindObjectOfType<GameSession>();
        informText = GetComponent<Text>();
        informText.text = "Level " + gameSession.GetCurrentLevel().ToString();
        StartCoroutine(waiter());
	}

    public void SetInformText(string text){
        informText.text = text;
        StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
        waiter();
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
