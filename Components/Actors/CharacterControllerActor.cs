using UnityEngine;
using System.Collections;
using SanityEngine.Movement.SteeringBehaviors;

[AddComponentMenu("Sanity Engine/Actors/Character Controller Actor"),
 RequireComponent(typeof(CharacterController))]
public class CharacterControllerActor : GameObjectActor {
	Vector3 oldPosition;
	Vector3 oldRotation;
	Vector3 velocity;
	Vector3 angularVelocity;
	CharacterControllerMotor motor;
	
	void Start ()
	{
		velocity = Vector3.zero;
		angularVelocity = Vector3.zero;
		motor = GetComponent<CharacterControllerMotor>();
		
		oldPosition = transform.position;
		oldRotation = transform.rotation.eulerAngles;
	}
	
	void FixedUpdate()
	{
		if(motor) {
			velocity = motor.Velocity;
			angularVelocity = motor.AngularVelocity;
		} else {
			float scale = 1f / Time.deltaTime;
			velocity = (transform.position - oldPosition) * scale;
			Vector3 rot = transform.rotation.eulerAngles;
			angularVelocity = (rot - oldRotation) * scale;
			oldPosition = transform.position;
			oldRotation = rot;
		}
	}
		
	public override Vector3 Velocity
    {
        get { return velocity; }
    }

	public override Vector3 AngularVelocity
    {
        get { return angularVelocity; }
    }
	
    public override Vector3 Position
    {
        get { return transform.position; }
    }

    public override Vector3 Forward
    {
        get { return transform.forward; }
    }

	public override Vector3 Right
    {
        get { return transform.right; }
    }
    
	public override Vector3 Up
    {
        get { return transform.up; }
    }
}
