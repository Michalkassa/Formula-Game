using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    private CarController cc;
    public float pitchcoeficient = 0.0004f;
    public float audioPitch = 1;
    public float minpitch = 1f;
    public float maxpitch = 1.8f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cc = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = cc.engineRPM  * pitchcoeficient;
        audioSource.pitch = Mathf.Clamp(audioSource.pitch, minpitch, maxpitch);
    }
}
