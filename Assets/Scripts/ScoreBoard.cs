using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
   //May be on enemy?
    int score;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }
    public void ScoreHit(int scorePer)
    {
        score += scorePer;
        scoreText.text = score.ToString();
    }
}
