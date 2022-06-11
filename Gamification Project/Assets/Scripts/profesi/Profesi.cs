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

    // Start is called before the first frame update
    void Start()
    {
        checkmark = transform.Find("checkmark").gameObject;
        cross = transform.Find("cross").gameObject;

        clicked = false;

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnProfesiClick() {
        if(IsAnswer) {
            checkmark.SetActive(true);
        } else {
            cross.SetActive(true);
        }

        clicked = true;
    }

    public void Reset() {
        checkmark.SetActive(false);
        cross.SetActive(false);

        clicked = false;
    }
}
