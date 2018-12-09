using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject imageToBlink;
    [SerializeField] private float blinkFrequency;
    [SerializeField] private SpriteRenderer fadeSpriteRenderer;
    [SerializeField] private float fadeOutSpeed;

    private bool fadeCalled = false;

    private void Start()
    {
        StartCoroutine("Blink");
    }

    void Update () {
        if (Input.GetButtonDown("Jump") && !fadeCalled)
        {
            fadeCalled = true;
            StartCoroutine("FadeOut");
        }
	}

    IEnumerator Blink()
    {
        while (true)
        {
            imageToBlink.SetActive(false);
            yield return new WaitForSeconds(blinkFrequency);
            imageToBlink.SetActive(true);
            yield return new WaitForSeconds(blinkFrequency);
        }
    }

    IEnumerator FadeOut()
    {
        for (float i = 0; i < 1.1f; i += 0.05f)
        {
            fadeSpriteRenderer.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(fadeOutSpeed);
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
