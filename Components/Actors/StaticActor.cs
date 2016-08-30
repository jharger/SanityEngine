using UnityEngine;
using System.Collections;

[AddComponentMenu("Sanity Engine/Actors/Static Actor")]
public class StaticActor : GameObjectActor {
	
	public override Vector3 Velocity
    {
        get { return Vector3.zero; }
    }

	public override Vector3 AngularVelocity
    {
        get { return Vector3.zero; }
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
