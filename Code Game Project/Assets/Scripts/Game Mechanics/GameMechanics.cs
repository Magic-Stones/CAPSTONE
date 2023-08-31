using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMechanics : MonoBehaviour
{
    public event EventHandler OnMainBattle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerMainBattle() 
    {
        OnMainBattle?.Invoke(this, EventArgs.Empty);
    }
}
