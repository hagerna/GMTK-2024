using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShapeUI : MonoBehaviour
{
    public static UnityEvent<ShapeUI> Select = new UnityEvent<ShapeUI>();
    ShapeSO _shape;
    RectTransform _rectTransform;
    Image _image;
    Scaler _activeShape;
    [SerializeField]
    GameObject _selectionBorder;

    public void Start()
    {
        Select.AddListener(Deselect);
    }


    public void SetShape(ShapeSO shapeSO)
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _shape = shapeSO;
        SetScale();
        SetBackgroundColor();
    }

    void SetScale()
    {
       _rectTransform.sizeDelta = new Vector2(80f, 80f);
    }

    void SetBackgroundColor()
    {
        if (_shape.verticalGrowth >= 0 && _shape.horizontalGrowth >= 0)
        {
            _image.color = Constants.RED;
        }
        else if (_shape.verticalGrowth >= 0 || _shape.horizontalGrowth >= 0)
        {
            _image.color = Constants.PURPLE;
        }
        else
        {
            _image.color = Constants.BLUE;
        }
        if (_shape.prodType == ProductionType.Currency)
        {
            _image.color = Constants.GREEN;
        }
    }

    public void Deselect(ShapeUI selected)
    {
        //_selectionBorder.SetActive(false);
    }

    public void SelectShape()
    {
        //_selectionBorder.SetActive(true);
        Select.Invoke(this);
    }

    public bool ShapeWillFit(Vector2 pos)
    {
        float width = _shape.width;
        float height = _shape.height;
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
            _activeShape = Instantiate(_shape.scalerPrefab, clickPosition, Quaternion.identity);
            _activeShape.Setup(_shape);
        }
    }

}
