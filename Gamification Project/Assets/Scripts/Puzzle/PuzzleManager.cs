using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PuzzleManager : MonoBehaviour
{
    [Header("Stuff References")]
    public Text progress;
    public Text caption;
    public GameObject[] puzzles;

    private int correctPieces = -1;

    private int rng;

    [Header("UI References")]
    public GameObject win;
    public GameObject winWindow;
    public RawImage winBackground;

    public void OnEnable()
    {
        PuzzlePiece.addScore += increaseScore;
    }

    public void OnDisable()
    {
        PuzzlePiece.addScore -= increaseScore;
    }

    void Start()
    {
        increaseScore();
        rng = Random.Range(0, 3);
        for(int i = 0; i < puzzles.Length; i++)
        {
            if (i == rng) puzzles[i].SetActive(true);
            else puzzles[i].SetActive(false);
        }
        switch (rng)
        {
            case 0: caption.text = "KOKI"; break;
            case 1: caption.text = "POLISI"; break;
            case 2: caption.text = "GURU"; break;
        }

        List<GameObject> puzzlePieces = new List<GameObject>();
        for(int i=0;i<6;i++)
        {
            int index = i + 6;
            puzzlePieces.Add(puzzles[rng].transform.GetChild(index).gameObject);
        }
        for(int i = 0; i < 6; i++)
        {
            int rand = Random.Range(0, puzzlePieces.Count);
            puzzlePieces[rand].transform.SetSiblingIndex(i + 6);
            puzzlePieces.RemoveAt(rand);
        }
    }

    public void increaseScore()
    {
        progress.text = (correctPieces + 1) + " / 6";
        correctPieces++;
        if (correctPieces >= 6) Win();
    }

    public void Win()
    {
        win.SetActive(true);

        winBackground.color = Color.clear;
        winBackground.DOColor(new Color(0, 0, 0, 0.5f), 0.5f);

        winWindow.transform.localScale = Vector3.zero;
        winWindow.transform.DOScale(Vector3.one, 0.5f);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
