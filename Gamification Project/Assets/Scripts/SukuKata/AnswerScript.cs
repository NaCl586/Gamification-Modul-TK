using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    private AudioManager am;

    //Observer Pattern - Subject
    public delegate void updateCorrectAnswers();
    public static updateCorrectAnswers increaseCorrectAnswers;

    public void Awake()
    {
        am = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();
    }

    public bool isCorrect = false;
    public QuizManager quizManager;
    public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("Correct Answer");
            am.puCorrect();
            increaseCorrectAnswers?.Invoke();

        }
        else
        {
            Debug.Log("Wrong Answer");
            am.puWrong();
            quizManager.answerQuestion(false);
        }
    }    
}
