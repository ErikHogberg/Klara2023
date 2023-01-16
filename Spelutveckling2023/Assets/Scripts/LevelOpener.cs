using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOpener : MonoBehaviour
{
    public static void OpenLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
