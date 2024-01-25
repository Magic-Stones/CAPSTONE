using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Create Stage Data", fileName = "StageData")]
public class StageData : ScriptableObject
{
    public bool isStage2Unlocked = false;
    public bool isStage3Unlocked = false;
    public bool isStage4Unlocked = false;
    public bool isStage5Unlocked = false;
    public bool isStage6Unlocked = false;
    public bool isStage7Unlocked = false;

    public void ResetAllStages()
    {
        isStage2Unlocked = false;
        isStage3Unlocked = false;
        isStage4Unlocked = false;
        isStage5Unlocked = false;
        isStage6Unlocked = false;
        isStage7Unlocked = false;
    }
}
