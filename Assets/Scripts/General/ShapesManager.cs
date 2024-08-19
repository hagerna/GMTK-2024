using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShapesManager : MonoBehaviour
{
    public static UnityEvent<List<ShapeSO>> ShapesUpdated = new UnityEvent<List<ShapeSO>>();
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
        if (RunManager.instance.GetLevel() == 0)
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
        ShapeSO[] shapes = Generator.instance.GetTutorialShapes();
        LoadUI(shapes);
    }

    public void NewRun()
    {
        ShapeSO[] shapes = Generator.instance.GetStartingShapes();
        LoadUI(shapes);
    }

    public void LoadUI(ShapeSO[] shapes)
    {
        Shapes = new List<ShapeSO>();
        ShapesUI = new List<ShapeUI>();
        int xOffset = 1920/2 - (100 * shapes.Length);
        foreach (ShapeSO baseSO in shapes)
        {
            ShapeSO copy = ShapeSO.Copy(baseSO);
            Shapes.Add(copy);
            ShapeUI shapeUI = Instantiate(prefab, new Vector2(xOffset, 100), Quaternion.identity, transform);
            shapeUI.SetShape(copy);
            ShapesUI.Add(shapeUI);
            xOffset += 250;
        }
        ShapesUpdated.Invoke(Shapes);
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
}
