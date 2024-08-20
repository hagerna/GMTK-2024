using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShapesManager : MonoBehaviour
{
    public static UnityEvent AllShapesLocked = new UnityEvent();
    public List<ShapeSO> Shapes { get; private set; }
    public List<ShapeUI> ShapesUI { get; private set; }
    [SerializeField]
    ShapeUI prefab;

    public static ShapesManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Scaler.LockedIn.AddListener(CheckAllLocked);
        if (RunManager.instance.isTutorial)
        {
            TutorialRun();
        }
        else
        {
            NewRun();
        }
    }

    public void TutorialRun()
    {
        Shapes = new List<ShapeSO>();
        ShapeSO[] shapes = Generator.instance.GetTutorialShapes();
        foreach (ShapeSO baseSO in shapes)
        {
            ShapeSO copy = ShapeSO.Copy(baseSO);
            Shapes.Add(copy);
        }
        LoadUI();
    }

    public void NewRun()
    {
        Shapes = new List<ShapeSO>();
        ShapeSO[] shapes = Generator.instance.GetStartingShapes();
        foreach (ShapeSO baseSO in shapes)
        {
            Shapes.Add(ShapeSO.Copy(baseSO));
        }
        LoadUI();
    }

    public void LoadUI()
    {
        ShapesUI = new List<ShapeUI>();
        int xOffset = 1920/2 - (100 * Shapes.Count);
        foreach (ShapeSO shape in Shapes)
        {
            ShapeUI shapeUI = Instantiate(prefab, new Vector2(xOffset, 100), Quaternion.identity, transform);
            shapeUI.SetShape(shape);
            ShapesUI.Add(shapeUI);
            xOffset += 250;
        }
    }

    private void CheckAllLocked()
    {
        foreach (ShapeUI obj in ShapesUI)
        {
            if (!obj.IsShapeLocked())
            {
                return;
            }
        }
        AllShapesLocked.Invoke();
    }

    public bool CheckAllPlaced()
    {
        foreach (ShapeUI obj in ShapesUI)
        {
            if (!obj.IsShapeActive())
            {
                return false;
            }
        }
        return true;
    }

    public void ClearAllScalers()
    {
        foreach (ShapeUI obj in ShapesUI)
        {
            obj.RemoveShape();
        }
    }

    public void AddShape(ShapeSO shape)
    {
        Shapes.Add(ShapeSO.Copy(shape));
        LoadUI();
    }
}
