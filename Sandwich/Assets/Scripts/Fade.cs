using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void StartFade(bool isFadeAway, CanvasGroup canvas)
    {
        StartCoroutine(FadeScreen(isFadeAway,canvas));
    }

    IEnumerator FadeScreen(bool isFadeAway, CanvasGroup canvas)
    {
        if (isFadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                canvas.alpha = i;
                yield return null;
            }
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
        else
        {
            for (float i = 0.8f; i <= 1; i += Time.deltaTime)
            {
                canvas.alpha = i;
                yield return null;
            }
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }


    }
}
