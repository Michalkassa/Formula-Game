using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SpeedoMeter : MonoBehaviour
{
    public TextMeshProUGUI kphDisplay;
    public TextMeshProUGUI rpmDisplay;
    public TextMeshProUGUI gearDisplay;
    public Rigidbody rb;
    public CarController cc;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() { 

        //double kph = rb.velocity.magnitude * 3.6;
        double kph = rb.velocity.magnitude * 3.6;
        double rpm = cc.engineRPM;
        kphDisplay.text = Mathf.RoundToInt((float)kph).ToString() + " KPH";
        rpmDisplay.text = Mathf.RoundToInt((float)rpm).ToString() + " RPM";
    }


    public void changeGear()
	{
        if(cc.gearNum == 0)
		{
            gearDisplay.text = "N";

		}
		else
		{
            gearDisplay.text = cc.gearNum.ToString();
        }
    }
}
