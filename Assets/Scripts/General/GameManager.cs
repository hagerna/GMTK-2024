using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<Scaler> ActiveShapes;

    public static GameManager instance;
    public float score, earnedCurrency, roundCurrency, lockInTimer, lockInDuration;
    public UnityEvent Begin, LockIn, Stop;

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

    private void Start()
    {
        Scaler.LockedIn.AddListener(CheckAllShapes);
        ActiveShapes = new List<Scaler>();
        lockInDuration = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckAllShapes()
    {
        foreach (Scaler shape in ActiveShapes)
        {
            if (shape.IsScaling())
            {
                return;
            }
        }
        StartCoroutine(TriggerLockIn());
    }

    IEnumerator TriggerLockIn()
    {
        LockIn.Invoke();
        Debug.Log("All Shapes Locked");
        lockInTimer = lockInDuration;
        yield return new WaitForSeconds(0.5f);
        while (lockInTimer > 0)
        {
            lockInTimer -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("Round Complete");
        lockInTimer = 0;
        Stop.Invoke();

    }

    public void ShapePlaced(Scaler scaler)
    {
        ActiveShapes.Add(scaler);
    }

    public void OnBeginPressed()
    {
        Begin.Invoke();
    }

    public void AddProduction(ProductionType type, float val)
    {
        switch (type)
        {
            case ProductionType.Score:
                score += val;
                break;
            case ProductionType.Currency:
                roundCurrency += val;
                break;
            default:
                break;
        }
        score += val;
    }
}
