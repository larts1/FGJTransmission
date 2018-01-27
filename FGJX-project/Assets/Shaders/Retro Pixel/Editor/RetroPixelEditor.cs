using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AlpacaSound
{
	[CustomEditor (typeof (RetroPixel))]
	public class RetroPixelEditor : Editor
	{
		SerializedObject serObj;

		SerializedProperty horizontalResolution;
		SerializedProperty verticalResolution;
		SerializedProperty numColors;

		SerializedProperty colors;

		void OnEnable ()
		{
			serObj = new SerializedObject (target);
			
			horizontalResolution = serObj.FindProperty ("horizontalResolution");
			verticalResolution = serObj.FindProperty ("verticalResolution");
			numColors = serObj.FindProperty ("numColors");
			colors = serObj.FindProperty ("colors");
		}

		override public void OnInspectorGUI ()
		{
			serObj.Update ();

			RetroPixel target = (RetroPixel) GameObject.FindGameObjectWithTag("MainCamera").GetComponent(typeof(RetroPixel));

			horizontalResolution.intValue = EditorGUILayout.IntField("Horizontal Resolution", horizontalResolution.intValue);
			verticalResolution.intValue = EditorGUILayout.IntField("Vertical Resolution", verticalResolution.intValue);
			numColors.intValue = EditorGUILayout.IntSlider("Number of colors", numColors.intValue, 2, RetroPixel.MAX_NUM_COLORS);

            for (int i = 0; i < numColors.intValue; i++)
            {
                colors.GetArrayElementAtIndex(i).colorValue = EditorGUILayout.ColorField("Color " + i, colors.GetArrayElementAtIndex(i).colorValue);
            }

            if (GUILayout.Button("Reset colors"))
            {
                target.ResetColors();
            }

			serObj.ApplyModifiedProperties ();
		}
	}
}
