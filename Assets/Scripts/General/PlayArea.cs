using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    GameObject _indicatorPrefab;
    GameObject _indicatorInstance;
    ShapeUI _selectedShape;

    private void Start()
    {
        ShapeUI.Select.AddListener(UpdateSelected);
    }

    private void Update()
    {
        if (_indicatorInstance != null && Input.GetMouseButton(0))
        {
            _indicatorInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10f;
        }
    }

    private void UpdateSelected(ShapeUI selected)
    {
        _selectedShape = selected;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_selectedShape == null)
        {
            return;
        }
        _indicatorInstance = Instantiate(_indicatorPrefab, Camera.main.ScreenToWorldPoint(eventData.position) + Vector3.forward * 10f, Quaternion.identity);
        _indicatorInstance.transform.localScale = new Vector2(_selectedShape.Shape.width, _selectedShape.Shape.height);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_selectedShape == null)
        {
            return;
        }
        Destroy(_indicatorInstance);
        _indicatorInstance = null;
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        if (GetComponent<Collider2D>().OverlapPoint(clickPosition))
        {
            _selectedShape.PlaceShape(clickPosition);
        }
        else
        {
            Debug.Log("Point is outside Play Area.");
        }
    }
}
