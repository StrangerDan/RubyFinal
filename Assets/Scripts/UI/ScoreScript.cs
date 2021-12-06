using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private int score =0;
    public Text scoreText;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    
    void Start()
    {
    scoreText = GetComponent<Text>();
    scoreText.text="Score : " + score;

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score ;
        if(Enemy1.tag == "Fixed"){
            score += 1;
            Enemy1.tag = "Counted";
        }
        if(Enemy2.tag == "Fixed"){
            score += 1;
                        Enemy2.tag = "Counted";

        }
        if(Enemy3.tag == "Fixed"){
            score += 1;
                        Enemy3.tag = "Counted";

        }
        if(Enemy4.tag == "Fixed"){
            score += 1;
                     Enemy4.tag = "Counted";

        }
    }
}
