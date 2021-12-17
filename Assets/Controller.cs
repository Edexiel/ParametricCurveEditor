using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private ParametricCurveDrawer _PCD;

    private void Update()
    {
        transform.position = _PCD.GetCurvePoint((Mathf.Sin(Time.time)/Mathf.PI) * 10 );
    }
}