using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;

public class ParametricCurveDrawer : MonoBehaviour
{
    public bool GameObjectHandle = false;

    private LineRenderer _lineRenderer = null;

    public enum CurveType
    {
        line,
        circle,
        cycloid,
        epicycloid,
    }


    [SerializeField] private CurveType _curveType;
    [CanBeNull] private Curve curve;

    private CurveType _oldCurveType; //to check if we changed line type so we can regen point list for line render
    
    private void Start()
    {
        _oldCurveType = _curveType;

        _lineRenderer = gameObject.GetComponent<LineRenderer>();

        Debug.Assert(_lineRenderer != null, "There is no line renderer in this object");

        ChangeCurve();
        Generate();
    }

    void Update()
    {
        if (_oldCurveType != _curveType)
        {
            ChangeCurve();
            Generate();
        }

        //todo: draw thingy that moves on the spline
    }

    public void ChangeCurve()
    {
        curve = _curveType switch
        {
            CurveType.line => new Line(),
            CurveType.circle => new Circle(),
            CurveType.cycloid => new Cycloid(),
            CurveType.epicycloid => new Epiycloid(),
            _ => throw new ArgumentOutOfRangeException()
        };

        _oldCurveType = _curveType;
    }

    public void Generate()
    {
        if (_lineRenderer)
        {
            List<Vector3> temp = curve.GetPoints();
            _lineRenderer.positionCount = temp.Count;
            _lineRenderer.SetPositions(temp.ToArray());
        }
    }

    public void OnInspectorGUI()
    {
        curve?.OnInspectorGUI();
    }

    // private void OnGUI()
    // {
    //     curve.OnInspectorGUI();
    // }

    // private void GenerateBezier()
    // {
    //     _lineRenderer.positionCount = (int) _lineSamples * points.Count;
    //
    //     int nb = 0;
    //     for (int i = 3; i < points.Count; i += 3)
    //     {
    //         Vector3 p0 = points[i - 3];
    //         Vector3 p1 = points[i - 2]; //Point 1
    //         Vector3 p2 = points[i - 1]; //Point 2
    //         Vector3 p3 = points[i]; //End point
    //
    //         float step = 1f / _lineSamples;
    //         for (float t = 0f; t <= 1f; t += step)
    //         {
    //             _lineRenderer.SetPosition(nb++, GetBezierPoint(p0, p1, p2, p3, t));
    //         }
    //     }
    //
    //     _lineRenderer.positionCount = nb;
    // }
    //
    // private Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    // {
    //     float t2 = t * t;
    //     float t3 = t * t * t;
    //     float invt = 1f - t;
    //
    //     return Mathf.Pow(invt, 3) * p0
    //            + 3 * t * Mathf.Pow(invt, 2) * p1
    //            + 3 * t2 * (1f - t) * p2
    //            + t3 * p3;
    // }


    //
    // public void AddCurve()
    // {
    //     Vector3 point = points[points.Count - 1];
    //     Vector3 startPoint = points[points.Count - 4];
    //     Vector3 direction = Vector3.Normalize(point - startPoint) * 10;
    //     point += direction;
    //     points.Add(point);
    //     point += direction;
    //     points.Add(point);
    //     point += direction;
    //     points.Add(point);
    // }
    //
    //
    // public void DeleteLastCurve()
    // {
    //     points.RemoveRange(points.Count - 4, 3);
    // }

    // public void SmoothCurve(int index)
    // {
    //     int maxIndex = points.Count - 1;
    //
    //     if ((index + 1) % 3 == 0 && (index + 2) < maxIndex)
    //     {
    //         points[index + 2] = points[index + 1] - (points[index] - points[index + 1]);
    //     }
    //     else if ((index - 1) % 3 == 0 && (index - 2) >= 0)
    //     {
    //         points[index - 2] = points[index - 1] - (points[index] - points[index - 1]);
    //     }
    // }
}