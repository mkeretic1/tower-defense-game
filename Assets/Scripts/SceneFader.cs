using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public Image img;

    void Start()
    {
        StartCoroutine(FadeIn());
    }  

    IEnumerator FadeIn()
    {
        float time = 1f;

        while(time > 0f)
        {
            time -= Time.deltaTime * 0.5f;
            img.color = new Color(0f, 0f, 0f, time);
            yield return 0; // wait a frame then continue
        }
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeOut(string scene)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * 1f;
            img.color = new Color(0f, 0f, 0f, time);
            yield return 0; // wait a frame then continue
        }

        SceneManager.LoadScene(scene);
    }
}
