using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShapesManager : MonoBehaviour
{
    public static UnityEvent<List<ShapeSO>> ShapesUpdated = new UnityEvent<List<ShapeSO>>();
    public List<ShapeSO> Shapes { get; private set; }
    [SerializeField]
    ShapeUI prefab;
    Generator generator;

    // Start is called before the first frame update
    void Start()
    {
        generator = FindAnyObjectByType<Generator>();
        NewRun();
    }

    public void NewRun()
    {
        int xOffset = 1920/2 - 500;
        Shapes = new List<ShapeSO>();
        foreach (ShapeSO baseSO in generator.GetStartingShapes())
        {
            ShapeSO copy = ShapeSO.Copy(baseSO);
            Shapes.Add(copy);
            Instantiate(prefab, new Vector2(xOffset, 100), Quaternion.identity, transform).SetShape(copy);
            xOffset += 250;
        }
        ShapesUpdated.Invoke(Shapes);
    }
}
