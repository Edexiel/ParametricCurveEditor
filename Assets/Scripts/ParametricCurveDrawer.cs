using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class ParametricCurveDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer = null;

    public enum CurveType
    {
        line,
        circle,
        cycloid,
        epicycloid,
        cardioid,
        lissajous
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
            CurveType.cardioid => new Cardioid(),
            CurveType.lissajous => new Lissajous(),
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

    public Vector3 GetCurvePoint(float t)
    {
        return curve.GetPoint(t);
    }
}