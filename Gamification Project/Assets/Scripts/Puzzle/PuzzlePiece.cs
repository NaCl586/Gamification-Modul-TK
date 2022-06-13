using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public RectTransform correctTransform;
    public bool isPlacedCorrectly = false;

    private AudioManager am;

    public delegate void scoreCounter();
    public static scoreCounter addScore;

    public void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("PuzzleCanvas").GetComponent<Canvas>();
        am = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform.anchoredPosition = new Vector2(Random.Range(-200, 200), Random.Range(-100, 200));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlacedCorrectly) return;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlacedCorrectly) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlacedCorrectly) return;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(Vector2.Distance(rectTransform.anchoredPosition, correctTransform.anchoredPosition));
        if(Vector2.Distance(rectTransform.anchoredPosition, correctTransform.anchoredPosition) < 50f &&
            !isPlacedCorrectly)
        {
            gameObject.SetActive(false);
            correctTransform.gameObject.GetComponent<Image>().color = Color.white;
            isPlacedCorrectly = true;
            am.puCorrect();
            addScore?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
