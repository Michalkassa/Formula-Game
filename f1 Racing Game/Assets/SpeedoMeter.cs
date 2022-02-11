using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SpeedMeter : MonoBehaviour
{
    public TextMeshProUGUI kphDisplay;
    public Rigidbody rb;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update() { 

        //double kph = rb.velocity.magnitude * 3.6;
        double kph = rb.velocity.magnitude * 3.6;
        kphDisplay.text = Mathf.RoundToInt((float)kph).ToString() + " KPH";
    }
}
