using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    Scaler[] ShapePrefabs;
    [SerializeField]
    ShapeUI selectedShape;
    ShapesManager shapesManager;

    private void Start()
    {
        shapesManager = FindObjectOfType<ShapesManager>();
        ShapeUI.Select.AddListener(UpdateSelected);
    }

    private void UpdateSelected(ShapeUI selected)
    {
        selectedShape = selected;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 clickPosition = FindAnyObjectByType<Camera>().ScreenToWorldPoint(eventData.position);
        selectedShape.PlaceShape(clickPosition);
    }
}
