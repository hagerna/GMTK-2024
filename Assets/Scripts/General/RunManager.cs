using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunManager : MonoBehaviour
{
    public static UnityEvent<int, LevelSO> LoadLevel = new UnityEvent<int, LevelSO>();
    public static UnityEvent Begin = new UnityEvent();
    public static UnityEvent Stop = new UnityEvent();
    private float score, roundCurrency, earnedCurrency, lockInTimer, lockInDuration;
    private int _level, _attempts;
    private float[] _targetScores = new float[] { 1000f, 1500f, 2500f, 4000f, 6000f, 8000f, 12500f };
    private LevelSO[] _currentRunLevels;
    public bool isTutorial { get; private set; }

    public static RunManager instance;

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
        ShapesManager.AllShapesLocked.AddListener(TriggerLockIn);
    }


    public void ResetRound()
    {
        score = 0f;
        roundCurrency = 0f;
        lockInTimer = 0;
    }

    public void LoadTutorial()
    {
        _currentRunLevels = new LevelSO[] { Generator.instance.GetTutorial() };
        ResetRound();
        _level = 0;
        lockInDuration = 10f;
        LoadLevel.Invoke(_level, _currentRunLevels[_level]);
        isTutorial = true;
    }

    public void LoadNewRun()
    {
        _currentRunLevels = Generator.instance.GetRunLevels();
        ResetRound();
        _level = 0;
        _attempts = 3;
        lockInDuration = 10f;
        LoadLevel.Invoke(_level, _currentRunLevels[_level]);
        isTutorial = false;
    }

    public void LoadNextLevel()
    {
        ResetRound();
        LoadLevel.Invoke(_level, _currentRunLevels[_level]);
    }

    private void TriggerLockIn()
    {
        StartCoroutine(LockingIn());
    }

    IEnumerator LockingIn()
    {
        lockInTimer = lockInDuration;
        yield return new WaitForSeconds(0.5f);
        while (lockInTimer > 0)
        {
            lockInTimer -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        lockInTimer = 0;
        HandleRoundEnd();
    }

    void HandleRoundEnd()
    {
        if (score > GetTargetScore())
        {
            earnedCurrency += roundCurrency;
            Stop.Invoke();
            _level++;
        }
        else
        {
            _attempts--;
            Stop.Invoke();
        }
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
    }

    public bool TriggerBegin()
    {
        if (ShapesManager.instance.CheckAllPlaced())
        {
            Begin.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckPurchase(float cost)
    {
        if (earnedCurrency > cost)
        {
            return true;
        }
        return false;
    }

    public void MakePurchase(float cost)
    {
        if (CheckPurchase(cost))
        {
            Debug.Log("Made Purchase For " + cost);
            earnedCurrency -= cost;
        }
        else
        {
            Debug.LogError("Attempted to make purchase without checking cost.");
        }
    }

    public float GetTargetScore() { return _targetScores[_level]; }

    public float GetRoundScore() {  return score; }

    public float GetRoundCurrency() { return roundCurrency; }

    public float GetTotalCurrency() { return earnedCurrency; }

    public float GetLockInTime() { return lockInTimer; }

    public int GetLevel() { return _level; }

    public int GetAttempts() { return _attempts; }

    public LevelSO GetLevelData() { return _currentRunLevels[_level]; }
}
