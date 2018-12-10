using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour {
    [SerializeField] private float speed;
    private SpriteRenderer spriteComponent;

	// Use this for initialization
	void Start () {
        spriteComponent = GetComponent<SpriteRenderer>();
        StartCoroutine("FadeIn");
	}

    public IEnumerator FadeOut(string sceneToGo)
    {
        for (float i = 0.0f; i < 1.1f; i += 0.05f)
        {
            spriteComponent.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(speed);
        }
        SceneManager.LoadScene(sceneToGo);
    }

    public IEnumerator FadeIn()
    {
        for (float i = 1.0f; i > -0.1f; i -= 0.05f)
        {
            spriteComponent.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(speed);
        }
    }
}
