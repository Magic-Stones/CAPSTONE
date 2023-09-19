using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private Light2D _light2D;

    void Awake()
    {
        _light2D = GetComponentInChildren<Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _light2D.pointLightInnerRadius = (Random.Range(0.1f, 1f)); // ayusin ang flicker rate
    }
}
