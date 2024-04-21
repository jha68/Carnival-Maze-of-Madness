using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    private Vector3 initialPosition;
    public PlayerControllerAppleGame scriptapple;
    private Vector3 movement;

    void Start()
    {
        initialPosition = transform.position;
        scriptapple = FindObjectOfType<PlayerControllerAppleGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptapple != null){
            scriptapple.move.z *= (float) 0.9;
            transform.position += scriptapple.move;
        }
        
    }
}
