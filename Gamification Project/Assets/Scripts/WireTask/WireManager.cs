using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class WireManager : MonoBehaviour
{
    [Header("Game References")]
    public List<GameObject> spriteRenderers = new List<GameObject>();
    public List<GameObject> spritePositions = new List<GameObject>();
    public List<Text> texts = new List<Text>();

    private List<int> wireColors = new List<int>();
    public List<Wire> leftWires = new List<Wire>();
    public List<Wire> rightWires = new List<Wire>();

    public Wire CurrentDraggedWire;
    public Wire CurrentHoveredWire;

    private List<GameObject> availableSpriteRenderers = new List<GameObject>();
    private List<int> _availableColors;
    private List<int> _availableLeftWireIndex;
    private List<int> _availableRightWireIndex;

    [HideInInspector] public int successCount = 0;

    [Header("UI References")]
    public GameObject win;
    public GameObject winWindow;
    public RawImage winBackground;

    private void Start()
    {
        win.SetActive(false);
        for (int i = 0; i < 5; i++) wireColors.Add(i);

        List<int> rng = new List<int>() { 0, 1, 2, 3, 4 };
        while(rng.Count > 0)
        {
            int pickedNumber = rng[Random.Range(0, rng.Count)];
            availableSpriteRenderers.Add(spriteRenderers[pickedNumber]);
            rng.Remove(pickedNumber);
        }

        _availableColors = new List<int>(wireColors);
        _availableLeftWireIndex = new List<int>();
        _availableRightWireIndex = new List<int>();

        for(int i = 0; i < leftWires.Count; i++) { _availableLeftWireIndex.Add(i); }
        for (int i = 0; i < rightWires.Count; i++) { _availableRightWireIndex.Add(i); }

        while(_availableColors.Count > 0 && _availableLeftWireIndex.Count > 0 && _availableRightWireIndex.Count > 0)
        {
            int pickedColor = _availableColors[Random.Range(0, _availableColors.Count)];
            int pickedLeftWireIndex = Random.Range(0, _availableLeftWireIndex.Count);
            int pickedRightWireIndex = Random.Range(0, _availableRightWireIndex.Count);
            
            leftWires[_availableLeftWireIndex[pickedLeftWireIndex]].SetColor(pickedColor);
            rightWires[_availableRightWireIndex[pickedRightWireIndex]].SetColor(pickedColor);

            availableSpriteRenderers[_availableLeftWireIndex[pickedLeftWireIndex]].transform.position = spritePositions[_availableLeftWireIndex[pickedLeftWireIndex]].transform.position;

            texts[_availableRightWireIndex[pickedRightWireIndex]].text = availableSpriteRenderers[_availableLeftWireIndex[pickedLeftWireIndex]].name;

            _availableColors.Remove(pickedColor);
            _availableLeftWireIndex.RemoveAt(pickedLeftWireIndex);
            _availableRightWireIndex.RemoveAt(pickedRightWireIndex);
        }
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
