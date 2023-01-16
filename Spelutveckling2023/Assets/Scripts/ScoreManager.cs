using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager
{
    public static UnityEvent scoreIncreased = new UnityEvent();
    public static int score = 0;
    public static bool canIncreaseScore = true;
    public static void IncreaseScore()
    {
        if (!canIncreaseScore)
            return;

        score += 1;
        scoreIncreased.Invoke();
    }
}
