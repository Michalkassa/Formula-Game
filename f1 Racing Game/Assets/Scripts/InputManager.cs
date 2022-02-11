using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float THROTTLE;
    public float HORIZONTAL;
    public bool HANDBRAKE;
    public bool UPSHIFT;
    public bool DOWNSHIFT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        THROTTLE = Input.GetAxis("Throttle");
        HORIZONTAL = Input.GetAxis("Horizontal");
        HANDBRAKE = Input.GetButton("Handbrake");
        
        UPSHIFT = Input.GetButtonUp("UpShift");
        DOWNSHIFT = Input.GetButtonUp("DownShift");
    }
}