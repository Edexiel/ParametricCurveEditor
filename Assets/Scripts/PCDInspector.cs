using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;


[CustomEditor(typeof(ParametricCurveDrawer))]
public class SplineMenu : Editor
{
    private ParametricCurveDrawer _parametricCurveDrawer;


    public override void OnInspectorGUI()
    {
        _parametricCurveDrawer = target as ParametricCurveDrawer;


        DrawDefaultInspector();

        EditorGUI.BeginChangeCheck();


        _parametricCurveDrawer.OnInspectorGUI();

        if (EditorGUI.EndChangeCheck())
        {
            _parametricCurveDrawer.Generate();
        }


        if (GUILayout.Button("Refresh"))
        {
            _parametricCurveDrawer.ChangeCurve();
            _parametricCurveDrawer.Generate();
        }
    }
}