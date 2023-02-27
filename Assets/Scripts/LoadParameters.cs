using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadParameters", menuName = "AGS_NorthCross/LoadParameters", order = 0)]
public class LoadParameters : ScriptableObject
{
    [SerializeField] public int nextLevel;
    [SerializeField] public string currentCharacterName;
    [SerializeField] public bool howl;


    [SerializeField] public int currentLevel;

    [SerializeField] public List<int> existDialog; 
    [SerializeField] public List<int> cutscene;
}