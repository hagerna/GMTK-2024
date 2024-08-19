using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Generator: MonoBehaviour
{
    [SerializeField]
    ShapeSO[] StarterShapes;
    [SerializeField]
    ShapeSO[] CurrencyShapes;
    [SerializeField]
    LevelSO[] Levels;
    [SerializeField]
    LevelSO Tutorial;

    public static Generator instance;

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

    public ShapeSO[] GetStartingShapes()
    {
        return new ShapeSO[] { StarterShapes[0], StarterShapes[1], StarterShapes[2], StarterShapes[3], CurrencyShapes[0] };
    }

    public ShapeSO[] GetTutorialShapes()
    {
        return new ShapeSO[] { StarterShapes[0], StarterShapes[3], CurrencyShapes[0] };
    }

    public LevelSO[] GetRunLevels()
    {
        return Levels;
    }

    public LevelSO GetTutorial()
    {
        return Tutorial;
    }
}
