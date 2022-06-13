using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // profesi, puzzle, seragam, sukukata, wiretask
    public GameObject[] ticks;
    
    void Start()
    {
        for(int i = 0; i < ticks.Length; i++)
        {
            if (PlayerPrefs.GetInt("CompleteMinigame" + i, 0) == 1) ticks[i].SetActive(true);
            else ticks[i].SetActive(false);
        }
    }

    public void LoadMinigame(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
