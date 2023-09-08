using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public SpriteRenderer GetSpriteRenderer { get; }
    void Death();
}
