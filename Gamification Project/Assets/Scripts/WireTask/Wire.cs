using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private AudioManager am;

    //Observer Pattern - Subject
    public delegate void updateSucessCount();
    public static updateSucessCount increaseSuccessCount;
    public static updateSucessCount decreaseSuccessCount;

    [HideInInspector] public Wire successor;

    [HideInInspector] public int wireColor;

    private Image _image;
    public LineRenderer lineRenderer;
    private Canvas _canvas;

    private bool _isDragStarted = false;
    [HideInInspector] public bool isSuccess = false;

    private WireManager _manager;

    private void Awake()
    {
        am = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();

        _image = this.GetComponent<Image>();
        lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WireManager>();
        lineRenderer.startColor = lineRenderer.endColor = Color.black;
    }
    
    public void SetColor(int c)
    {
        wireColor = c;
    }

    private void Update()
    {
        if (_isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                _canvas.worldCamera,
                out movePos
            );

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _canvas.transform.TransformPoint(movePos));
        }
        else
        {
            if (!isSuccess)
            {
                lineRenderer.SetPosition(0, Vector3.zero);
                lineRenderer.SetPosition(1, Vector3.zero);
            }
        }

        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, _canvas.worldCamera);

        if (isHovered)
            _manager.CurrentHoveredWire = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_manager.CurrentHoveredWire != null)
        {
            if(_manager.CurrentHoveredWire.wireColor == wireColor && 
                _manager.CurrentHoveredWire != _manager.CurrentDraggedWire)
            {
                isSuccess = true;
                _manager.CurrentDraggedWire.isSuccess = true;

                successor = _manager.CurrentDraggedWire;
                _manager.CurrentHoveredWire.successor = _manager.CurrentDraggedWire;

                am.puCorrect();
                increaseSuccessCount?.Invoke();
            }
            else
            {
                am.puWrong();
                isSuccess = false;
                _manager.CurrentDraggedWire.isSuccess = false;
            }
        }

        _isDragStarted = false;
        _manager.CurrentDraggedWire = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (successor != null && successor != this)
        {
            decreaseSuccessCount?.Invoke();
            successor.lineRenderer.SetPosition(0, Vector3.zero);
            successor.lineRenderer.SetPosition(1, Vector3.zero);
        }
        else if(successor == this)
        {
            decreaseSuccessCount?.Invoke();
        }
        _isDragStarted = true;
        _manager.CurrentDraggedWire = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //ga dipake
    }
}
