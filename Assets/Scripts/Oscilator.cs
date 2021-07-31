using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Oscilator : MonoBehaviour
{
    Vector3 StartingPosition;
    [SerializeField] Vector3 Movementvector;
    [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        StartingPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(period == 0) { return; }

        float Cycles = Time.time / period;
        const float tau = Mathf.PI * 2;

        float rawSinWave = Mathf.Sin(Cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = Movementvector * movementFactor;
        transform.position = StartingPosition + offset;
        
    }
}
