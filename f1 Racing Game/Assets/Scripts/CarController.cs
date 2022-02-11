using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	//ENUMS
	internal enum driveType
	{
		frontWheelDrive,
		rearWheelDrive,
		allWheelDrive
	}

	internal enum gearBox
	{
		automatic,
		manual
	}
	[SerializeField] private driveType drive;
	[SerializeField] private gearBox gearChange;


	// Variables
	public float totalPower;
	public static int numberOfGears = 8;
	public int gearNum = 0;
	public float maxRPM, minRPM;
	public float[] gears = new float[numberOfGears];
	float smoothTime = 0.01f;
	public float radius = 6f;
	public float maxSteerAngle= 40f;
	public float brakePower;
	public float KPH = 0f;
	public float engineRPM;
	public float DownForceValue = 50f;
	public float steeringMax = 25f;
	public bool isReversing = false;
	private float wheelRPMs;
	public float defaultFOV;



	//SETTERS
	public AnimationCurve enginePower;
	public SpeedoMeter speedMeter;
	public GameObject[] wheelMesh = new GameObject[4];
	private InputManager IM;
	private Camera Cam;
	public WheelCollider[] wheels = new WheelCollider[4];
	private Rigidbody rb;
	public Transform CenterOfMass;

	private void Start()
	{

		GetObjects();
	}
	private void Update()
	{
		rb.centerOfMass = CenterOfMass.localPosition;
		moveVehicle();
		if (IM.THROTTLE < 0f)
		{
			isReversing = true;
		}
		else
		{
			isReversing = false;
		}

		CangeFovOnSpeed(rb.velocity.magnitude);


	}
	private void FixedUpdate()
	{
	
		addDownForce();
		animateWheels();
		steerVehicle();
		calculateEnginePower();
		moveVehicle();
		shifter();
		KPH = rb.velocity.magnitude * 3.6f;
	}
	void calculateEnginePower()
	{
		
		wheelRPM();
		totalPower = enginePower.Evaluate(engineRPM) * Mathf.Abs(IM.THROTTLE) * (gears[gearNum]); 
		float velocity = 0.0f;
		engineRPM = Mathf.SmoothDamp(engineRPM, minRPM + (Mathf.Abs(wheelRPMs) * (gears[gearNum])), ref velocity, smoothTime);
		engineRPM = Mathf.Clamp(engineRPM, minRPM, maxRPM);
	}

	private void wheelRPM()
	{
		float sum = 0f;
		int R = 0;
		for (int i = 0; i < 3; i++)
		{
			sum += wheels[i].rpm;
			R++;
		}
		wheelRPMs = (R != 0) ? sum / R : 0;
	}

	public void shifter()
	{
		if(gearChange == gearBox.automatic){
			if (gearNum == 0f)
			{
				gearNum++;
				speedMeter.changeGear();
			}
			if(engineRPM > (maxRPM - 400) && gearNum < gears.Length - 1)
			{
				gearNum++;
				speedMeter.changeGear();
			}
			if (engineRPM < maxRPM && gearNum > 1)
			{
				gearNum--;
			}
		}

		if (gearChange == gearBox.manual)
		{
			if (IM.UPSHIFT && gearNum < gears.Length - 1)
			{
				gearNum++;
				speedMeter.changeGear();
			}
			if (IM.DOWNSHIFT && gearNum > 0)
			{
				gearNum--;
				speedMeter.changeGear();
			}
		}
	}
	void moveVehicle()
	{
		

		if(drive == driveType.allWheelDrive){
			for (int i = 0; i < wheels.Length; i++)
			{	
				if (isReversing)
				{
					wheels[i].motorTorque = IM.THROTTLE * (totalPower * 1/16);
				}
				else
				{
					wheels[i].motorTorque = IM.THROTTLE * (totalPower * 1/4);
				}
				
			}
		}else if (drive == driveType.rearWheelDrive){
			for (int i = 0; i < wheels.Length; i++)
			{
				
			
				wheels[i].motorTorque = IM.THROTTLE * (totalPower * 1/8);
				
				wheels[i].motorTorque = IM.THROTTLE * (totalPower * 1/2);
			}
		}
		else{
			for (int i = 0; i < wheels.Length - 2; i++){
				if (isReversing)
				{
					wheels[i].motorTorque = IM.THROTTLE * (totalPower * 1/8);
				}
				else
				{
					wheels[i].motorTorque = IM.THROTTLE * (totalPower * 1/2);
				}
			}
		}

		if (IM.HANDBRAKE){
			wheels[0].brakeTorque = brakePower;
			wheels[1].brakeTorque = brakePower;
			wheels[3].brakeTorque = brakePower;
			wheels[2].brakeTorque = brakePower;
		}
		else
		{
			wheels[0].brakeTorque = 0f;
			wheels[1].brakeTorque = 0f;
			wheels[3].brakeTorque = 0f;
			wheels[2].brakeTorque = 0f;
		}
	}
	private void steerVehicle()
	{
		
		float currentSteerAngle;
		currentSteerAngle = maxSteerAngle * IM.HORIZONTAL;
		wheels[0].steerAngle = currentSteerAngle; 
		wheels[1].steerAngle = currentSteerAngle;


	}
	void animateWheels()
	{
		Vector3 wheelPosition = Vector3.zero;
		Quaternion wheelRotation = Quaternion.identity;

		for (int i = 0; i < 4; i++)
		{
			wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
			wheelMesh[i].transform.position = wheelPosition;
			wheelMesh[i].transform.rotation = wheelRotation;
		}
	}
	void GetObjects()
	{
		IM = GetComponent<InputManager>();
		rb = GetComponent<Rigidbody>();
		Cam = GetComponentInChildren<Camera>();
	}
	private void addDownForce()
	{
		rb.AddForce(-transform.up * DownForceValue);
	}

	private void CangeFovOnSpeed(float speed)
	{
		float desiredFov = defaultFOV + speed / 10;


		Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, desiredFov, Time.deltaTime * 1000);
	
	}

}
