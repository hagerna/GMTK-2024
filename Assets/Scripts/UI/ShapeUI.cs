using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShapeUI : MonoBehaviour
{
    public static UnityEvent<ShapeUI> Select = new UnityEvent<ShapeUI>();
    public ShapeSO Shape { get; private set; }
    RectTransform _rectTransform;
    Image _image;
    Scaler _activeShape;
    [SerializeField]
    GameObject _selectionBorder, Directional, _placementIndicator;
    [SerializeField]
    Sprite Grow, Shrink;
    [SerializeField]
    Image Vertical, Horizontal;

    public void Start()
    {
        Select.AddListener(Deselect);
        _selectionBorder.SetActive(false);
        _placementIndicator.SetActive(false);
    }


    public void SetShape(ShapeSO shapeSO)
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        Shape = shapeSO;
        SetScale();
        SetBackgroundColor();
        SetUI();
    }

    void SetScale()
    {
        _rectTransform.sizeDelta = new Vector2(Mathf.Min(50f + (20f * Shape.width), 160f), Mathf.Min(50f + (20f * Shape.height)));
        _selectionBorder.GetComponent<RectTransform>().sizeDelta = _rectTransform.sizeDelta + new Vector2(20f + (5f * Shape.width), 20f + (5f * Shape.width));
    }

    void SetBackgroundColor()
    {
        _image.color = Constants.GetBackgroundColor(new Vector2(Shape.horizontalGrowth, Shape.verticalGrowth));
        if (Shape.prodType == ProductionType.Currency)
        {
            _image.color = Constants.GREEN;
        }
    }

    void SetUI()
    {
        Vertical.GetComponent<RectTransform>().sizeDelta = new Vector2(_rectTransform.sizeDelta.x * 0.25f, _rectTransform.sizeDelta.y * 0.75f);
        Horizontal.GetComponent<RectTransform>().sizeDelta = new Vector2(_rectTransform.sizeDelta.x * 0.25f, _rectTransform.sizeDelta.y * 0.75f);
        Vertical.gameObject.SetActive(true);
        Horizontal.gameObject.SetActive(true);

        // Set Directional Arrow
        if (Shape.scalerType == ScalerType.Directional)
        {
            Directional.SetActive(true);
            Directional.transform.rotation = Quaternion.Euler(0, 0, Shape.rotation);
            Directional.GetComponent<RectTransform>().sizeDelta = new Vector2(_rectTransform.sizeDelta.x * 0.175f, _rectTransform.sizeDelta.y * 0.5f);
            Vertical.gameObject.SetActive(false);
            Horizontal.gameObject.SetActive(false);
        }

        // Set Vertical Arrows
        if (Shape.verticalGrowth > 0)
        {
            Vertical.sprite = Grow;
        }
        else if (Shape.verticalGrowth < 0)
        {
            Vertical.sprite = Shrink;
        }
        else
        {
            Vertical.sprite = null;
            Vertical.gameObject.SetActive(false);
        }

        // Set Horizontal Arrows
        if (Shape.horizontalGrowth > 0)
        {
            Horizontal.sprite = Grow;
        }
        else if (Shape.horizontalGrowth < 0)
        {
            Horizontal.sprite = Shrink;
        }
        else
        {
            Horizontal.sprite = null;
            Horizontal.gameObject.SetActive(false);
        }
    }

    public void Deselect(ShapeUI selected)
    {
        if (selected != this)
        {
            _selectionBorder.SetActive(false);
            if (_activeShape)
            {
                _activeShape.Deselect();
            }
        }
    }

    public void SelectShape()
    {
        Select.Invoke(this);
        _selectionBorder.SetActive(true);
        if (_activeShape != null)
        {
            _activeShape.ShapeSelected();
        }
    }

    public bool ShapeWillFit(Vector2 pos)
    {
        float width = Shape.width;
        float height = Shape.height;
        Vector2 topRight = new Vector2(pos.x + width / 2, pos.y + height / 2);
        Vector2 botLeft = new Vector2(pos.x - width / 2, pos.y - height / 2);
        Collider2D overlap = Physics2D.OverlapArea(topRight, botLeft, LayerMask.GetMask("Obstacles"));
        if (overlap != null)
        {
            Debug.Log("Shape Cannot Fit There");
            return false;
        }
        return true;
    }

    public void PlaceShape(Vector2 clickPosition)
    {
        if (ShapeWillFit(clickPosition) && _activeShape == null)
        {
            _activeShape = Instantiate(Shape.scalerPrefab, clickPosition, Quaternion.identity);
            _activeShape.Setup(Shape);
            Vertical.gameObject.SetActive(false);
            Horizontal.gameObject.SetActive(false);
            Directional.gameObject.SetActive(false);
            _placementIndicator.SetActive(true);
        }
    }

    public void RemoveShape()
    {
        Destroy(_activeShape.gameObject);
        _activeShape = null;
        _placementIndicator.SetActive(false);
        SetUI();
    }

    public bool IsShapeActive()
    {
        return _activeShape != null;
    }

    public bool IsShapeLocked()
    {
        return !_activeShape.IsScaling();
    }
}
