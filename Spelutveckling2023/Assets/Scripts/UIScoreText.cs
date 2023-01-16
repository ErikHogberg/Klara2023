using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreText : MonoBehaviour
{
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.scoreIncreased.AddListener(UpdateText);
    }

    void UpdateText()
    {
        scoreText.text = ScoreManager.score.ToString();
    }
}
