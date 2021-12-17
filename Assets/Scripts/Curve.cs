using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.Animations;

public abstract class Curve
{
    public float min = -Mathf.PI;
    public float max = Mathf.PI;
    public int samples = 40;
    public Vector3 offset;

    protected List<Vector3> points = new List<Vector3>();

    public abstract Vector3 GetPoint(float t);

    public virtual List<Vector3> GetPoints()
    {
        points.Clear();

        float step = (max - min) / samples;

        for (float t = min; t < max; t += step)
        {
            points.Add(GetPoint(t) + offset);
        }

        points.Add(GetPoint(max) + offset);

        Debug.Log(points.Count);
        return points;
    }

    public virtual void OnInspectorGUI()
    {
        min = EditorGUILayout.FloatField("Min", min);
        max = EditorGUILayout.FloatField("Max", max);
        // samples = EditorGUILayout.IntField("Samples", samples);
        samples = EditorGUILayout.IntSlider("Samples", samples, 4, 10000);
        offset = EditorGUILayout.Vector3Field("Offset", offset);
    }
}

public class Line : Curve
{
    private Vector3 point;
    private Vector3 direction;

    public override Vector3 GetPoint(float t)
    {
        return new Vector3(direction.x * t + point.x, direction.y * t + point.y, direction.z * t + point.z);
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        point = EditorGUILayout.Vector3Field("Point", point);
        direction = EditorGUILayout.Vector3Field("Direction", direction);
    }
}

public class Circle : Curve
{
    private float radius = 30f;

    public Circle()
    {
        min = 0f;
        max = Mathf.PI * 2;
    }

    public override Vector3 GetPoint(float t)
    {
        return new Vector3(Mathf.Cos(t), Mathf.Sin(t), 0) * radius;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        radius = EditorGUILayout.FloatField("Radius", radius);
    }
}

public class Cycloid : Curve
{
    private float radius = 10f;

    public Cycloid()
    {
        min = -Mathf.PI * 2f;
        max = Mathf.PI * 2f;
    }

    public override Vector3 GetPoint(float t)
    {
        return new Vector3(radius * (t - Mathf.Sin(t)), radius * (1f - Mathf.Cos(t)), 0);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        radius = EditorGUILayout.FloatField("Radius", radius);
    }
}

public class Epiycloid : Curve
{
    private float radius = 10f;
    private float k = 4f;

    public override Vector3 GetPoint(float t)
    {
        return new Vector3(radius * (t - Mathf.Sin(t)), radius * (1f - Mathf.Cos(t)), 0);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        radius = EditorGUILayout.FloatField("Radius", radius);
        k = EditorGUILayout.FloatField("Repetitions", k);
    }
}