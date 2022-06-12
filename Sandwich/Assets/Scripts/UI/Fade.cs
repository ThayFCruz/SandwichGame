using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{

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
            canvas.transform.gameObject.SetActive(false);
        }
        else
        {
            canvas.transform.gameObject.SetActive(true);
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
