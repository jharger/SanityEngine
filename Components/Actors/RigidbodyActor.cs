using UnityEngine;
using System.Collections;
using SanityEngine.Movement.SteeringBehaviors;

[AddComponentMenu("Sanity Engine/Actors/Rigid Body Actor")]
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyActor : GameObjectActor {
    Rigidbody body;

	void Awake()
	{
		body = GetComponent<Rigidbody>();
	}
	
	public override Vector3 Velocity
    {
        get { return body.velocity; }
    }

	public override Vector3 AngularVelocity
    {
        get { return body.angularVelocity; }
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
