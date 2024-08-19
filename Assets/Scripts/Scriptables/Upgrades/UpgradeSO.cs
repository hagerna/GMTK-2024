using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptables/Upgrade", order = 3)]
public class UpgradeSO : ScriptableObject
{
    public string UpgradeName;
    public string Description;
    public float Cost;
    public UpgradeClass Class;
    public UpgradeType Type;
    public float Modifier;
    public Sprite Sprite;
}

public enum UpgradeType
{
    Width,
    Height,
    Size,
    VerticalGrowth,
    HorizontalGrowth,
    Growth,
    ProductionMultiplier,
    ProductionDelay,
    LockInDuration
}

public enum UpgradeClass
{
    Basic,
    Advanced,
    NewShape,
}
