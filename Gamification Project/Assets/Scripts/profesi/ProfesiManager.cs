using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfesiManager : MonoBehaviour
{

    /**
    1. Perlengkapan dan kendaraan polisi
    2. Alat - alat dokter
    3. Alat - alat koki
    4. Perlengkapan dan alat - alat guru
    5. Alat - Alat Penata Rambut
    6. Alat - Alat Pelukis
    */
    /**
     * variable to store all the possible choices
     * and then removes the choice from it once the player has completed it
     * so that the game doesn't have duplicate problem
     */
    List<int> boxes;

    [Header("Game text")]
    public Text text;

    [Header("Image gameobject")]
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    public GameObject Image5;
    public GameObject Image6;

    private Image image1Image;
    private Image image2Image;
    private Image image3Image;
    private Image image4Image;
    private Image image5Image;
    private Image image6Image;

    private Profesi image1Profesi; 
    private Profesi image2Profesi;
    private Profesi image3Profesi;
    private Profesi image4Profesi;
    private Profesi image5Profesi;
    private Profesi image6Profesi;

    [Header("I am gay")]
    public Sprite polisi1;  
    public Sprite polisi2;
    public Sprite polisi3;

    public Sprite artist1;
    public Sprite artist2;    
    public Sprite artist3;

    public Sprite koki1;
    public Sprite koki2;
    public Sprite koki3;

    public Sprite dokter1;
    public Sprite dokter2;
    public Sprite dokter3;

    public Sprite guru1;
    public Sprite guru2;
    public Sprite guru3;

    public Sprite penata1;
    public Sprite penata2;
    public Sprite penata3;



    // Start is called before the first frame update
    void Start()
    {
        boxes = new List<int>();
        for(int i = 0; i < 6; ++i) {
            boxes.Add(i+1);
        }

        image1Image = Image1.GetComponent<Image>();
        image2Image = Image2.GetComponent<Image>();
        image3Image = Image3.GetComponent<Image>();
        image4Image = Image4.GetComponent<Image>();
        image5Image = Image5.GetComponent<Image>();
        image6Image = Image6.GetComponent<Image>();
        
        image1Profesi = Image1.GetComponent<Profesi>();
        image2Profesi = Image2.GetComponent<Profesi>();
        image3Profesi = Image3.GetComponent<Profesi>();
        image4Profesi = Image4.GetComponent<Profesi>();
        image5Profesi = Image5.GetComponent<Profesi>();
        image6Profesi = Image6.GetComponent<Profesi>();

        PickProfesi(); 
    }

    // Update is called once per frame
    void Update()
    {
        //variable ini berfungsi untuk menghitung banyak jawaban yang benar
        //appabila jawaban 3, reset
        int correctCounter = 0;
        if(image1Profesi.IsAnswer && image1Profesi.clicked) {
            correctCounter++;
        }
        if(image2Profesi.IsAnswer && image2Profesi.clicked) {
            correctCounter++;
        }
        if(image3Profesi.IsAnswer && image3Profesi.clicked) {
            correctCounter++;
        }
        if(image4Profesi.IsAnswer && image4Profesi.clicked) {
            correctCounter++;
        }
        if(image5Profesi.IsAnswer && image5Profesi.clicked) {
            correctCounter++;
        }
        if(image6Profesi.IsAnswer && image6Profesi.clicked) {
            correctCounter++;
        }

        if(correctCounter == 3) {
            image1Profesi.Reset();
            image2Profesi.Reset();
            image3Profesi.Reset();
            image4Profesi.Reset();
            image5Profesi.Reset();
            image6Profesi.Reset();
            PickProfesi();
        }
    }

    void PickProfesi() {
        
        int chooser = Random.Range(0, boxes.Count);
        int chosen = boxes[chooser];
        boxes.RemoveAt(chooser);

        //polisi
        if(chosen == 1) {
            text.text = "Pilihlah alat yang digunakan seorang polisi";

        }
        //dokter
        else if(chosen == 2) {
            text.text = "Pilihlah alat yang digunakan oleh seorang dokter";
        }
        //koki
        else if(chosen == 3) {
            text.text = "Pilihlah alat yang digunakan oleh seorang koki";
        }
        //guru
        else if(chosen == 4) {
            text.text = "Pilihlah alat yang digunakan oleh seorang guru";
        }
        //penata rambut
        else if(chosen == 5) {
            text.text = "Pilihlah alat yang digunakan oleh seorang penata rambut";
        }
        //pelukis
        else if(chosen == 6) {
            text.text = "Pilihlah alat yang digunakan oleh seorang pelukis";
        }

        bool[] answers = {true, true, true, false, false, false};

        Sprite[] imageSprite = returnSpriteRandom(chosen);

        /**
         * randomize image order
         */
        for (int i = 0; i < imageSprite.Length; i++) {
             int rnd = Random.Range(0, imageSprite.Length);
             Sprite tempGO = imageSprite[rnd];
             imageSprite[rnd] = imageSprite[i];
             imageSprite[i] = tempGO;

             bool temp = answers[rnd];
             answers[rnd] = answers[i];
             answers[i] = temp;
         }

         image1Image.sprite = imageSprite[0];
         image2Image.sprite = imageSprite[1];
         image3Image.sprite = imageSprite[2];
         image4Image.sprite = imageSprite[3];
         image5Image.sprite = imageSprite[4];
         image6Image.sprite = imageSprite[5];

         image1Profesi.IsAnswer = answers[0];
         image2Profesi.IsAnswer = answers[1];
         image3Profesi.IsAnswer = answers[2];
         image4Profesi.IsAnswer = answers[3];
         image5Profesi.IsAnswer = answers[4];
         image6Profesi.IsAnswer = answers[5];
    }

    /**
    1. Perlengkapan dan kendaraan polisi
    2. Alat - alat dokter
    3. Alat - alat koki
    4. Perlengkapan dan alat - alat guru
    5. Alat - Alat Penata Rambut
    6. Alat - Alat Pelukis
    */
    Sprite[] returnSpriteRandom(int profesi) {
        int resultIndex = 0;

        Sprite[] result = new Sprite[6];
        
        /**
         * this variable is used in order to shuffle through
         * the sprites, very ineffciient
         */
        List<Sprite> spriteBox = new List<Sprite>();

        spriteBox.Add(artist1); //0
        spriteBox.Add(artist2); //1
        spriteBox.Add(artist3); //2

        spriteBox.Add(koki1); //3
        spriteBox.Add(koki2); //4
        spriteBox.Add(koki3); //5

        spriteBox.Add(dokter1); //6
        spriteBox.Add(dokter2); //7
        spriteBox.Add(dokter3); //8

        spriteBox.Add(guru1); //9
        spriteBox.Add(guru2); //10
        spriteBox.Add(guru3); //11

        spriteBox.Add(penata1); //12
        spriteBox.Add(penata2); //13
        spriteBox.Add(penata3); //14

        spriteBox.Add(polisi1); //15
        spriteBox.Add(polisi2); //16
        spriteBox.Add(polisi3); //17

        if(profesi == 1) {
            spriteBox.RemoveAt(15);
            spriteBox.RemoveAt(15);
            spriteBox.RemoveAt(15);

            result[resultIndex++] = polisi1;
            result[resultIndex++] = polisi2;
            result[resultIndex++] = polisi3;

            for(int i = 0; i < 3; ++i) {
                int rng = Random.Range(0, spriteBox.Count);
                result[resultIndex++] = spriteBox[rng];
                spriteBox.RemoveAt(rng);
            }
        } else if(profesi == 2) {
            spriteBox.RemoveAt(6);
            spriteBox.RemoveAt(6);
            spriteBox.RemoveAt(6);

            result[resultIndex++] = dokter1;
            result[resultIndex++] = dokter2;
            result[resultIndex++] = dokter3;

            for(int i = 0; i < 3; ++i) {
                int rng = Random.Range(0, spriteBox.Count);
                result[resultIndex++] = spriteBox[rng];
                spriteBox.RemoveAt(rng);
            }
        } else if(profesi == 3) {
            spriteBox.RemoveAt(3);
            spriteBox.RemoveAt(3);
            spriteBox.RemoveAt(3);

            result[resultIndex++] = koki1;
            result[resultIndex++] = koki2;
            result[resultIndex++] = koki3;

            for(int i = 0; i < 3; ++i) {
                int rng = Random.Range(0, spriteBox.Count);
                result[resultIndex++] = spriteBox[rng];
                spriteBox.RemoveAt(rng);
            }
        } else if(profesi == 4) {
            spriteBox.RemoveAt(9);
            spriteBox.RemoveAt(9);
            spriteBox.RemoveAt(9);

            result[resultIndex++] = guru1;
            result[resultIndex++] = guru2;
            result[resultIndex++] = guru3;

            for(int i = 0; i < 3; ++i) {
                int rng = Random.Range(0, spriteBox.Count);
                result[resultIndex++] = spriteBox[rng];
                spriteBox.RemoveAt(rng);
            }
        }else if(profesi == 5) {
            spriteBox.RemoveAt(12);
            spriteBox.RemoveAt(12);
            spriteBox.RemoveAt(12);

            result[resultIndex++] = penata1;
            result[resultIndex++] = penata2;
            result[resultIndex++] = penata3;

            for(int i = 0; i < 3; ++i) {
                int rng = Random.Range(0, spriteBox.Count);
                result[resultIndex++] = spriteBox[rng];
                spriteBox.RemoveAt(rng);
            }
        }else if(profesi == 6) {
            spriteBox.RemoveAt(0);
            spriteBox.RemoveAt(0);
            spriteBox.RemoveAt(0);

            result[resultIndex++] = artist1;
            result[resultIndex++] = artist2;
            result[resultIndex++] = artist3;

            for(int i = 0; i < 3; ++i) {
                int rng = Random.Range(0, spriteBox.Count);
                result[resultIndex++] = spriteBox[rng];
                spriteBox.RemoveAt(rng);
            }
        }


        return result;
    }
}
