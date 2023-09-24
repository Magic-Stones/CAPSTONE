using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public bool GetIsDefeated { get; }
    public Sprite GetChallengePose { get; }
    void Death();
}
