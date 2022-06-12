using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profesi : MonoBehaviour
{

    private bool _isAnswer;

    /**
     * variable ini menentukan apakah
     * Imagenya merupakan jawaban atau bukan
     */
    public bool IsAnswer
    {
        get { return _isAnswer; }
        set { _isAnswer = value; }
    }

    public bool clicked;

    private GameObject checkmark;
    private GameObject cross;

    private AudioManager am;
    private ProfesiManager pm;


    public void Awake()
    {
        am = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();
        pm = GameObject.Find("SoalManager").GetComponent<ProfesiManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        checkmark = transform.GetChild(0).gameObject;
        cross = transform.GetChild(1).gameObject;

        clicked = false;

        Reset();
    }

    public void OnProfesiClick() {
        if(IsAnswer) {
            checkmark.SetActive(true);
            am.puCorrect();
        } else {
            cross.SetActive(true);
            am.puWrong();
        }

        clicked = true;
        pm.CheckAnswer();
    }

    public void Reset() {
        checkmark.SetActive(false);
        cross.SetActive(false);

        clicked = false;
    }
}
