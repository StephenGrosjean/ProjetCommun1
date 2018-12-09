using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textScore, textLifes;
    [SerializeField] private Image[] healthSprites;
    [SerializeField] private Sprite goodSprite, badSprite;

    [SerializeField] private int health;
    public int Health
    {
        set { health = value; }
    }

    private int score;
    public int Score
    {
        set { score = value; }
    }

    private int lifes;
    public int Lifes
    {
        set { lifes = value; }
    }

    private const int maxScore = 999999;
    private const int minScore = 0;

    private const int maxLifes = 99;
    private const int minLifes = 0;

	void Start () {
	}
	
	void Update () {
        if(score < minScore){
            score = minScore;
        }
        else if(score > maxScore){
            score = maxScore;
        }

        if (lifes < minLifes){
            lifes = minLifes;
        }
        else if (lifes > maxLifes){
            lifes = maxLifes;
        }
    }


    //Update the score and the life
    public void UpdateUI(){
        int[] processedScore = { 0, 0, 0, 0, 0, 0 };
        int[] processedLifes = { 0, 0 };

        string processedScoreString = "";
        string processedLifesString = "";

        //Turn the int score into arcade type
        processedScore[5] = score % 10;
        processedScore[4] = ((score % 100) - processedScore[5]) / 10;
        processedScore[3] = ((score % 1000) - processedScore[4]) / 100;
        processedScore[2] = ((score % 10000) - processedScore[3]) / 1000;
        processedScore[1] = ((score % 100000) - processedScore[2]) / 10000;
        processedScore[0] = ((score % 1000000) - processedScore[1]) / 100000;
        
        foreach(int intScore in processedScore){
            processedScoreString = processedScoreString + intScore.ToString();
        }

        //Turn the int Lifes into arcade type
        processedLifes[1] = lifes % 10;
        processedLifes[0] = ((lifes % 100) - processedLifes[1]) / 10;

        foreach (int intLife in processedLifes){
            processedLifesString = processedLifesString + intLife.ToString();
        }

        //Change Text
        textScore.text = processedScoreString;
        textLifes.text = "x" + processedLifesString;
    }

    //Update the Health Bar
    public void UpdateHealthUI()
    {
        switch (health)
        {
            case 6:
                changeSprite(5, goodSprite);
                changeSprite(4, goodSprite);
                changeSprite(3, goodSprite);
                changeSprite(2, goodSprite);
                changeSprite(1, goodSprite);
                changeSprite(0, goodSprite);
                break;

            case 5:
                changeSprite(5, badSprite);
                changeSprite(4, goodSprite);
                changeSprite(3, goodSprite);
                changeSprite(2, goodSprite);
                changeSprite(1, goodSprite);
                changeSprite(0, goodSprite);
                break;

            case 4:
                changeSprite(5, badSprite);
                changeSprite(4, badSprite);
                changeSprite(3, goodSprite);
                changeSprite(2, goodSprite);
                changeSprite(1, goodSprite);
                changeSprite(0, goodSprite);
                break;

            case 3:
                changeSprite(5, badSprite);
                changeSprite(4, badSprite);
                changeSprite(3, badSprite);
                changeSprite(2, goodSprite);
                changeSprite(1, goodSprite);
                changeSprite(0, goodSprite);
                break;

            case 2:
                changeSprite(5, badSprite);
                changeSprite(4, badSprite);
                changeSprite(3, badSprite);
                changeSprite(2, badSprite);
                changeSprite(1, goodSprite);
                changeSprite(0, goodSprite);
                break;

            case 1:
                changeSprite(5, badSprite);
                changeSprite(4, badSprite);
                changeSprite(3, badSprite);
                changeSprite(2, badSprite);
                changeSprite(1, badSprite);
                changeSprite(0, goodSprite);
                break;
            case 0:
                changeSprite(5, badSprite);
                changeSprite(4, badSprite);
                changeSprite(3, badSprite);
                changeSprite(2, badSprite);
                changeSprite(1, badSprite);
                changeSprite(0, badSprite);
                break;
        }
    }

    void changeSprite(int ID, Sprite sprite)
    {
        healthSprites[ID].sprite = sprite;
    }
}
