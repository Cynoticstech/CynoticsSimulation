using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RefractionManager : MonoBehaviour
{
    [SerializeField]
    private float _incidenceAngle = 40f, _refrectionAngle, _emergenceAngle, _topRayMaxPos, _bottomRayMaxPos, _topRot, _bottomRot, _maxIncidenceAngle = 90,
        _ArcDist = 0.3f,angleFixOffset =1.2f;
    [SerializeField] Transform _topRay, _bottomRay, _topNorm, _bottomNorm;
    [SerializeField] private Vector3 _bottomArcOffsetFix;
    [SerializeField] LineRenderer _rayLine;
    [SerializeField] Slider _incidenceValue;
    [SerializeField] TMP_Text refractiveTxt,incidenceTxt,emergenceTxt;
    [SerializeField] LineRenderer _topTrail, _bottomTrail;
    [SerializeField] int _pointsInArc = 6;

    float _originalTop, _originalBottom;

    const float _firstRefrectiveIndex = 1.0003f, _secondRefrectiveIndex = 1.52f;

    private void Start()
    {
        _rayLine.positionCount = 2;
        _originalTop = _topRay.position.x;
        _originalBottom = _bottomRay.position.x;
        UpdateRay();
    }

    private void Update()
    {
        _rayLine.SetPositions(new Vector3[]
        {
            _topRay.position,
            _bottomRay.position,
        });
    }
    public void UpdateRay()
    {
        float _val = _incidenceValue.value;
        _incidenceAngle = _maxIncidenceAngle * _val;

        var _pos = _topRay.position;
        _pos.x = _topRayMaxPos * _val + _originalTop;
        _topRay.position = _pos;
        var _Rot = _topRay.eulerAngles;
        _Rot.z = _val * _topRot;
        _topRay.eulerAngles = _Rot;
        _topNorm.position = _pos;

        //draw arc
        _topTrail.positionCount = _pointsInArc;
        var _startPos = new GameObject("_tempRot").transform;
        _startPos.position = _pos;
        var _roteAngle = _Rot  / _pointsInArc;
        _roteAngle /= angleFixOffset;
        List<Vector3> _tempArcPos = new List<Vector3>();
        for (int i = 0; i < _pointsInArc; i++)
        {
            _tempArcPos.Add(-_startPos.up.normalized * _ArcDist+_startPos.position);
            var _angle = _startPos.transform.eulerAngles;
            _angle += _roteAngle;
            _startPos.eulerAngles = _angle;
        }
        _topTrail.SetPositions(_tempArcPos.ToArray());

        _pos = _bottomRay.position;
        _pos.x = _bottomRayMaxPos * _val + _originalBottom;
        _bottomRay.position = _pos;
        _Rot = _bottomRay.eulerAngles;
        _Rot.z = _val * _bottomRot;
        _bottomRay.eulerAngles = _Rot;
        _bottomNorm.position = _pos;

        //draw arc
        _bottomTrail.positionCount = _pointsInArc;
        _startPos.position = _pos;
        _tempArcPos = new List<Vector3>();
        for (int i = 0; i < _pointsInArc; i++)
        {
            _tempArcPos.Add(_startPos.up * _ArcDist+ _startPos.position+_bottomArcOffsetFix);
            var _angle = _startPos.transform.eulerAngles;
            _angle -= _roteAngle;
            _startPos.eulerAngles = _angle;
        }
        _bottomTrail.SetPositions(_tempArcPos.ToArray());
        Destroy(_startPos.gameObject);

        _refrectionAngle = CalculateRefraction();
        refractiveTxt.text = "Refrection Angle: " + _refrectionAngle;
        incidenceTxt.text = "Incidence Angle: " + _incidenceAngle;
        emergenceTxt.text = "Emergence Angle: " + _incidenceAngle;

    }

    private float CalculateRefraction()
    {
        var _tempAngle = Mathf.Asin(_firstRefrectiveIndex / _secondRefrectiveIndex) * Mathf.Sin(Mathf.Deg2Rad * _incidenceAngle);
        return _tempAngle * Mathf.Rad2Deg;
    }
}
