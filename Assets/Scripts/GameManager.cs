using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Scaler[] ActiveShapes;
    public static GameManager instance;
    public float score, currency, lockInTimer, lockInDuration;
    public UnityEvent Begin, Stop;

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
        StartCoroutine(LockIn());
    }

    IEnumerator LockIn()
    {
        Debug.Log("All Shapes Locked");
        lockInTimer = lockInDuration;
        while (lockInTimer > 0)
        {
            lockInTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Round Complete");

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
                currency += val;
                break;
            default:
                break;
        }
        score += val;
    }
}
