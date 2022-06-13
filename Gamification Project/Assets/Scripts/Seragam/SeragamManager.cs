using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SeragamManager : MonoBehaviour
{
    public static int anakCowo;

    [Header("Gambar Anak")]
    public Sprite tara;
    public Sprite kanaya;

    [Header("Gambar Seragam")]
    public Sprite[] seragamCowo;
    public Sprite[] seragamCewe;

    [Header("References")]
    public Text caption;
    public Image gambarAnak;
    public Button[] buttons;
    public Image[] buttonImage;

    [Header("UI References")]
    public GameObject win;
    public GameObject winWindow;
    public RawImage winBackground;

    private AudioManager am;

    private int day = 0;
    private string[] days = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" };
    private string[] namaAnak = { "Tara", "Kanaya" };

    private List<int> sequence;

    void Awake()
    {
        anakCowo = Random.Range(0, 100) % 2 == 0 ? 0 : 1;
        sequence = Utils.randomizedList(0, 3, 4);
        am = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        gambarAnak.sprite = (anakCowo == 0) ? tara : kanaya;
        setTextGuru();
        for(int i = 0; i < buttons.Length; i++)
        {
            buttonImage[sequence[i]].sprite = anakCowo == 0 ? seragamCowo[i] : seragamCewe[i];
        }
    }

    public void checkAnswer(Image selectedImage)
    {
        if (checkCorrectAnswer(selectedImage.sprite.name))
        {
            am.puCorrect();

            if (day == 4) Win();
            else day++;
            
            setTextGuru();
        }
        else
        {
            am.puWrong();
        }
    }

    bool checkCorrectAnswer(string name)
    {
        if(day == 0 || day == 2)
        {
            if (name == "seragam_cowo" || name == "seragam_cewe") return true;
            else return false;
        }
        else if(day == 1)
        {
            if (name == "baju_bebas" || name == "gaun") return true;
            else return false;
        }
        else if (day == 3)
        {
            if (name == "seragam_olahraga") return true;
            else return false;
        }
        else
        {
            if (name == "batik_cowo" || name == "batik_cewe") return true;
            else return false;
        }
    }

    public void Win()
    {
        win.SetActive(true);

        winBackground.color = Color.clear;
        winBackground.DOColor(new Color(0, 0, 0, 0.5f), 0.5f);

        winWindow.transform.localScale = Vector3.zero;
        winWindow.transform.DOScale(Vector3.one, 0.5f);

        PlayerPrefs.SetInt("CompleteMinigame2", 1);
    }

    public void retry()
    {
        SceneManager.LoadScene(0);
    }

    void setTextGuru()
    {
        caption.text = "Apa yang harus dikenakan " + namaAnak[anakCowo] + " di hari " + days[day] + "?";
    }
}
