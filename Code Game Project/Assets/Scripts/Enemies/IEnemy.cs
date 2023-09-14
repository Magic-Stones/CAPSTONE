using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public bool GetIsDefeated { get; }
    public SpriteRenderer GetSpriteRenderer { get; }
    void Death();
}
