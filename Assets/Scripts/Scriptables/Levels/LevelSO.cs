using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptables/Level", order = 2)]
public class LevelSO : ScriptableObject
{
    public string LevelName;
    public Color BackgroundColor;
    public GameObject PlayAreaPrefab;
}
