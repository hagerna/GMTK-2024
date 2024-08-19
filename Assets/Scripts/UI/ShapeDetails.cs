using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapeDetails : MonoBehaviour
{
    ShapeUI shape;
    [SerializeField]
    GameObject DetailsPanel;
    [SerializeField]
    TextMeshProUGUI type, scoreMult, size, growth, modifiers;

    // Start is called before the first frame update
    void Start()
    {
        ShapeUI.Select.AddListener(UpdateDisplay);
    }

    void UpdateDisplay(ShapeUI shapeUI)
    {
        if (shapeUI == null)
        {
            return;
        }
        DetailsPanel.SetActive(true);
        shape = shapeUI;
        ShapeSO shapeData = shape.Shape;
        type.text = string.Format("Type: {0}", shapeData.scalerType.ToString());
        scoreMult.text = string.Format("Score Multiplier: {0}", shapeData.prodMultiplier.ToString("0.00"));
        size.text = string.Format("Size: {0} x {1}", shapeData.width, shapeData.height);
        growth.text = string.Format("Growth: {0}/sec x {1}/sec", shapeData.verticalGrowth * 20f, shapeData.horizontalGrowth * 20f);
        modifiers.text = "Modifiers: None";

    }

    public void CloseDetails()
    {
        DetailsPanel.SetActive(false);
        shape.Deselect(null);
    }

    public void Remove()
    {
        if (shape.IsShapeActive())
        {
            shape.RemoveShape();
        }
        else
        {
            Debug.Log("No Shape to Remove");
        }
    }
}
