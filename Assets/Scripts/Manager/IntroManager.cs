using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float speed;
    [SerializeField] private FadeScript fadeScript;
	// Use this for initialization
	void Start () {
        StartCoroutine("fadeInText");	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator fadeInText()
    {
        yield return new WaitForSeconds(.5f);
        for(float i = 0.0f; i < 1.1f; i += 0.02f)
        {
            text.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(1.5f);
        fadeScript.StartCoroutine("FadeOut", sceneToLoad);
    }
}
