using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GoPanel;

    public Text QuestionTxt;

    private void Start()
    {
        GoPanel.SetActive(false);
        generateQuestion();
    }

    public void retry()
    {
        SceneManagement.LoadScene(SceneManagement.GetActiveScene().buildindex);
    }

    void GameOver()
    {
        Quispanel.SetActive(false);
        GoPanel.SetActive(true);
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for(int i=0; i<options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
             
            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
            options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }
    void generateQuestion()
    {
        if(QnA.Count > 0 )
        {
              currentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text = QnA[currentQuestion].Questions;
        SetAnswers();
        }
        else 
        {
            Debug.Log("Out of Question");
            GameOver();
        }
    }
}
