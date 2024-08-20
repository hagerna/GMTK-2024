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
    bool randomizeLoadout = false;

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
        if (!randomizeLoadout)
        {
            return new ShapeSO[] { StarterShapes[0], StarterShapes[1], StarterShapes[2], StarterShapes[3], CurrencyShapes[0] };
        }
        else
        {
            int maxIndex = StarterShapes.Length - 1;
            return new ShapeSO[] { StarterShapes[Random.Range(0,maxIndex)], StarterShapes[Random.Range(0, maxIndex)], StarterShapes[Random.Range(0, maxIndex)], StarterShapes[Random.Range(0, maxIndex)], CurrencyShapes[Random.Range(0, CurrencyShapes.Length-1)] };
        }
    }

    public ShapeSO[] GetTutorialShapes()
    {
        return new ShapeSO[] { StarterShapes[0], StarterShapes[3], CurrencyShapes[0] };
    }

    public LevelSO[] GetRunLevels()
    {
        RandomizeLevels(Levels);
        return Levels;
    }

    public LevelSO GetTutorial()
    {
        return Tutorial;
    }

    public static void RandomizeLevels(LevelSO[] ts)
    {
        var count = ts.Length;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    public void ToggleRandomLoadout(GameObject checkMark)
    {
        randomizeLoadout = !randomizeLoadout;
        checkMark.SetActive(randomizeLoadout);
    }
}
