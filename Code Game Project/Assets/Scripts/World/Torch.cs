using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    [SerializeField] private float _flickerRate = 1f;
    private float _nextFlickerTime;

    private Light2D _light2D;

    void Awake()
    {
        _light2D = GetComponentInChildren<Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _nextFlickerTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _nextFlickerTime)
        {
            _light2D.pointLightInnerRadius = Random.Range(0.5f, 1f);
            _nextFlickerTime = Time.time + (1f / _flickerRate);
        }
    }
}
