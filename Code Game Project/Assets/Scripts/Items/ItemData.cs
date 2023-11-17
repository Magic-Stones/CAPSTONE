using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Create Item Data", fileName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;

    [TextArea]
    public string itemDescription;

    [Space(10)]
    public string quizAnswer;
}
