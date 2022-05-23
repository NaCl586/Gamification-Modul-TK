using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class QuizManager : MonoBehaviour
{

    [Header("Questions")]
    public List<QuestionsAndAnswers> QnA;

    [Header("References")]
    public GameObject[] options;
    private int currentQuestion;
    private int lastQuestion = -1;

    public Image image;

    public Text QuestionTxt;
    public Text CurrentScoreText;

    private int TotalQuestions = 0;
    public int score;

    [Header("UI References")]
    public GameObject win;
    public GameObject winWindow;
    public RawImage winBackground;

    private List<int> correctAnswerIndex = new List<int>();

    private void Start()
    {
        TotalQuestions = 5;
        generateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Win()
    {
        win.SetActive(true);

        winBackground.color = Color.clear;
        winBackground.DOColor(new Color(0, 0, 0, 0.5f), 0.5f);

        winWindow.transform.localScale = Vector3.zero;
        winWindow.transform.DOScale(Vector3.one, 0.5f);

    }

    public void answerQuestion(bool correct)
    {
        if (correct)
        {
            score++;
            correctAnswerIndex.Add(currentQuestion);
        }
        generateQuestion();
    }

    public List<int> randomizedList(int start, int finish, int take)
    {
        List<int> orderedSequence = new List<int>();
        for (int i = start; i <= finish; i++) orderedSequence.Add(i);
        
        List<int> finalSequence = new List<int>();
        for (int i = 0; i < take; i++)
        {
            int rng = Random.Range(0, orderedSequence.Count);
            finalSequence.Add(orderedSequence[rng]);
            orderedSequence.RemoveAt(rng);
        }

        return finalSequence;
    }

    void SetAnswers()
    {
        List<int> answerPos = randomizedList(1, 4, 2);
        answerPos.Add(0);
        answerPos.Reverse();

        List<int> sequencePos = randomizedList(0, 2, 3);


        for(int i = 0; i < 3; i++)
        {
            options[sequencePos[i]].GetComponent<AnswerScript>().isCorrect = false;
            options[sequencePos[i]].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[answerPos[i]];
             
            if(answerPos[i] == 0)
                options[sequencePos[i]].GetComponent<AnswerScript>().isCorrect = true;
        }
    }
    void generateQuestion()
    {

        CurrentScoreText.text = score + "/" + TotalQuestions;

        if (score < TotalQuestions)
        {
            do
            {
                currentQuestion = Random.Range(0, QnA.Count);
            }
            while (correctAnswerIndex.Contains(currentQuestion) || lastQuestion == currentQuestion);
            lastQuestion = currentQuestion;

            image.sprite = QnA[currentQuestion].image;
            QuestionTxt.text = QnA[currentQuestion].Questions;
            SetAnswers();
        }
    }

    public void OnEnable()
    {
        AnswerScript.increaseCorrectAnswers += increaseCorrectAnswers;
    }

    public void OnDisable()
    {
        AnswerScript.increaseCorrectAnswers -= increaseCorrectAnswers;
    }

    public void increaseCorrectAnswers()
    {
        answerQuestion(true);
        if (score == TotalQuestions) Win();
    }
}
