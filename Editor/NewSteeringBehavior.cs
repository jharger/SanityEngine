using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using SanityEngine.Actors;
using SanityEngine.Movement.SteeringBehaviors;
using SanityEngine.Movement.SteeringBehaviors.Flocking;

public class NewSteeringBehavior : EditorWindow {
	public SteeringBehaviorAsset asset;
	
	List<System.Type> classes;
	List<string> names;
	int chosenType = -1;
	float weight = 1.0f;
	bool enabled = true;
		
	public NewSteeringBehavior()
	{
		classes = new List<System.Type>();
		names = new List<string>();
		
		System.Type behaviorType = typeof(SteeringBehavior);
		Assembly asm = behaviorType.Assembly;
    	System.Type[] types = asm.GetTypes();
	    foreach(System.Type type in types) {
    	    if(!type.IsSubclassOf(behaviorType) || type.IsAbstract) {
        	  	continue;
	        }
	        
	        classes.Add(type);
    	    if(type.IsSubclassOf(typeof(FlockingBehavior))) {
		        names.Add("Flock/" + type.Name);
    	    } else {
		        names.Add(type.Name);
			}
    	}
	}
	
	void OnGUI()
	{
		chosenType = EditorGUILayout.Popup("Type", chosenType, names.ToArray());
		if(chosenType >= 0) {
			object temp = System.Activator.CreateInstance(classes[chosenType]);
			string description = (string)classes[chosenType].GetMethod(
				"GetDescription").Invoke(temp, null);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Description");
			GUILayout.Label(description, EditorStyles.wordWrappedLabel);
			EditorGUILayout.EndHorizontal();
		}
		weight = EditorGUILayout.FloatField("Weight", weight);
		enabled = EditorGUILayout.Toggle("Initially Enabled", enabled);
		EditorGUILayout.Space();
		if(GUILayout.Button("Create")) {
			CreateBehavior();
		}
	}
	
	void CreateBehavior()
	{
		System.Type type = classes[chosenType];

		SteeringBehaviorAsset.BehaviorDef behavior =
			new SteeringBehaviorAsset.BehaviorDef();
		behavior.type = type.FullName;	
		behavior.name = type.Name;
		behavior.weight = weight;
		behavior.enabled = enabled;	 
        
        List<SteeringBehaviorAsset.LinkedProperty> props =
            new List<SteeringBehaviorAsset.LinkedProperty>();
        object impl = System.Activator.CreateInstance(type);
	    PropertyInfo[] properties = type.GetProperties(
	    	BindingFlags.Instance | BindingFlags.Public);
    	foreach(PropertyInfo prop in properties) {
    		if(prop.Name == "Weight" || prop.Name == "Enabled") {
    			continue;
    		}
    		
    	    if(!prop.CanWrite) {
    	    	continue;
            }
            
            string def = "";
			SteeringBehaviorAsset.LinkedProperty linkedProp
				= new SteeringBehaviorAsset.LinkedProperty();
			linkedProp.name = prop.Name;
			linkedProp.type = GetPropertyType(prop.PropertyType, out def);
          	if(prop.CanRead) {
          		object v = prop.GetValue(impl, null);
          		if(v != null) { 
	               	def = v.ToString();
          		}
	        }
			linkedProp.defaultValue = def;
			props.Add(linkedProp);
   		}
   		behavior.properties = props.ToArray();
   		asset.AddBehavior(behavior);
   		Close();
	}
	
	SteeringBehaviorAsset.PropertyType GetPropertyType(System.Type type,
		out string def)
	{
		if(typeof(Actor).IsAssignableFrom(type)) {
			def = null;
			return SteeringBehaviorAsset.PropertyType.ACTOR;
		}
		if(typeof(Flock).IsAssignableFrom(type)) {
			def = null;
			return SteeringBehaviorAsset.PropertyType.FLOCK;
		}
		if(type == typeof(bool)) {
			def = "false";
			return SteeringBehaviorAsset.PropertyType.BOOL;
		}
		if(type == typeof(int)) {
			def = "0";
			return SteeringBehaviorAsset.PropertyType.INT;
		}
		if(type == typeof(float)) {
			def = "0.0";
			return SteeringBehaviorAsset.PropertyType.FLOAT;
		}
		if(type == typeof(Vector2)) {
			def = "(0.0,0.0)";
			return SteeringBehaviorAsset.PropertyType.VECTOR2;
		}
		if(type == typeof(Vector3)) {
			def = "(0.0,0.0,0.0)";
			return SteeringBehaviorAsset.PropertyType.VECTOR3;
		}
		if(type == typeof(Vector4)) {
			def = "(0.0,0.0,0.0,1.0)";
			return SteeringBehaviorAsset.PropertyType.VECTOR4;
		}
		if(type == typeof(string)) {
			def = "";
			return SteeringBehaviorAsset.PropertyType.STRING;
		}
		throw new System.ArgumentException("Unsupported type: " + type.Name);
	}
}
