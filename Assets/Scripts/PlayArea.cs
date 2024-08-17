using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Scaler[] ShapePrefabs;
    [SerializeField]
    ShapeSO selectedShape;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Vector2 clickPosition = FindAnyObjectByType<Camera>().ScreenToWorldPoint(eventData.position);
        if (ShapeWillFit(clickPosition))
        {
            Instantiate(ShapePrefabs[(int)selectedShape.scalerType], clickPosition, Quaternion.identity).Setup(selectedShape);
        }
    }

    bool ShapeWillFit(Vector2 pos)
    {
        float width = selectedShape.width;
        float height = selectedShape.height;
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
}
