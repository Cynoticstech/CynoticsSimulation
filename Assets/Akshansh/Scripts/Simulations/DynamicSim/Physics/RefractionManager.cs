using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionManager : MonoBehaviour
{
    [SerializeField] private float _incidenceAngle = 40f, _refrectionAngle, _emergenceAngle;
    [SerializeField] private GameObject _lineRendererPrefab, _trailRendererPrefab,_dottedLinePrefab;

    [SerializeField] float _orignToSlabDist = 1f,_refractiveLineDist =1f;
    [SerializeField] Vector3 _rayOriginPos, _slabTopEdgePos, _slabBottomEdgePos, _refractiveLineDir;

    [SerializeField] Color _rayColor = Color.yellow,_normalColor = Color.blue,_incidenceAngleColor = Color.red,
        _refrectionAngleColor = Color.green;

    private LineRenderer _rayOfLight,_normal1,_normal2;
    private TrailRenderer _i1AngleTrail, _r1AngleTrail, _r2AngleTrail, _e1AngleTrail;
   [SerializeField]  private Vector3 _rayStartingDir;

    const float _firstRefrectiveIndex =1.0003f , _secondRefrectiveIndex = 1.52f;

    private void Start()
    {
        InitializeRays();
    }

    private void InitializeRays()
    {
        //generate all rays
        _rayOfLight = Instantiate(_lineRendererPrefab).GetComponent<LineRenderer>();
        _normal1 = Instantiate(_lineRendererPrefab).GetComponent<LineRenderer>();
        _normal2 = Instantiate(_lineRendererPrefab).GetComponent<LineRenderer>();

        _i1AngleTrail = Instantiate(_trailRendererPrefab).GetComponent<TrailRenderer>();
        _r1AngleTrail = Instantiate(_trailRendererPrefab).GetComponent<TrailRenderer>();
        _r2AngleTrail = Instantiate(_trailRendererPrefab).GetComponent<TrailRenderer>();
        _e1AngleTrail = Instantiate(_trailRendererPrefab).GetComponent<TrailRenderer>();

        //initialize trails
        _rayOfLight.positionCount = 2;
        _normal2.positionCount = 2;
        _normal1.positionCount = 2;
        _rayOfLight.startColor = _rayColor;
        _rayOfLight.endColor = _rayColor;
        _normal1.startColor = _normalColor;
        _normal1.endColor = _normalColor;
        _normal2.startColor = _normalColor;
        _normal2.endColor = _normalColor;

        _i1AngleTrail.Clear();
        _r1AngleTrail.Clear();
        _r2AngleTrail.Clear();
        _e1AngleTrail.Clear();
        _i1AngleTrail.startColor = _incidenceAngleColor;
        _i1AngleTrail.endColor = _incidenceAngleColor;
        _e1AngleTrail.startColor = _incidenceAngleColor;
        _e1AngleTrail.endColor = _incidenceAngleColor;
        _r1AngleTrail.startColor = _refrectionAngleColor;
        _r1AngleTrail.endColor = _refrectionAngleColor;
        _r1AngleTrail.startColor = _refrectionAngleColor;
        _r1AngleTrail.endColor = _refrectionAngleColor;

    }

    private void Update()
    {
        DrawRay();
    }

    private void DrawRay()
    {
        _refrectionAngle = CalculateRefraction();
        _emergenceAngle = _incidenceAngle;
        _rayStartingDir = (_slabTopEdgePos - _rayOriginPos).normalized;
        _refractiveLineDir = -RotatePointAroundOrigin(_rayStartingDir, _orignToSlabDist * _rayStartingDir, _refrectionAngle);

        _rayOfLight.positionCount = 3;
        _refractiveLineDir = (_refractiveLineDir - (_orignToSlabDist * _rayStartingDir)).normalized * _refractiveLineDist;
        _rayOfLight.SetPositions(new Vector3[]
        {
            _rayOriginPos,
            _orignToSlabDist*_rayStartingDir,
            _refractiveLineDir
        }) ;
    }
    
    private float CalculateRefraction()
    {
        var _tempAngle = Mathf.Asin(_firstRefrectiveIndex/_secondRefrectiveIndex)*Mathf.Sin(Mathf.Deg2Rad*_incidenceAngle);
        return _tempAngle*Mathf.Rad2Deg;
    }

    private Vector3 RotatePointAroundOrigin(Vector3 point, Vector3 origin, float angle)
    {
        Vector3 dir = point - origin;
        Vector3 rotatedDir = Quaternion.Euler(0, 0, angle) * dir;
        Vector3 rotatedPoint = origin + rotatedDir;
        return rotatedPoint;
    }
}
