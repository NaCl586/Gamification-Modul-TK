using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _as;
    public AudioClip correct;
    public AudioClip wrong;

    // Start is called before the first frame update
    void Start()
    {
        _as = this.GetComponent<AudioSource>();
    }

    public void puCorrect()
    {
        _as.PlayOneShot(correct);
    }

    public void puWrong()
    {
        _as.PlayOneShot(wrong);
    }
}
