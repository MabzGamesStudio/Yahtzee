using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Die))]
public class DieEditor : Editor
{
	/// <summary>
	/// The die object in the inspector
	/// </summary>
	Die die;

	/// <summary>
	/// This sets the inspector to edit the die object
	/// </summary>
	private void OnEnable()
	{
		die = (Die)target;
	}

	/// <summary>
	/// This updates the inspector window with function buttons
	/// </summary>
	public override void OnInspectorGUI()
	{
		// This makes the beginning of the inspector as it usually is
		base.OnInspectorGUI();
		EditorGUILayout.Space();

		// Roll Die
		EditorGUILayout.LabelField("Roll Die", EditorStyles.boldLabel);
		if (GUILayout.Button("Roll Die"))
		{
			die.RollDie();
		}
		EditorGUILayout.Space();

		// Increment Die
		EditorGUILayout.LabelField("Increment Die", EditorStyles.boldLabel);
		if (GUILayout.Button("Increment Die"))
		{
			die.IncrementDie();
		}
		EditorGUILayout.Space();

		// Update
		EditorGUILayout.LabelField("Update Die", EditorStyles.boldLabel);
		if (GUILayout.Button("Update Die"))
		{
			die.UpdateDie();
		}
	}
}
