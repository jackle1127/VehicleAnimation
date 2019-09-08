using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireController : MonoBehaviour
{
    [SerializeField] private Transform _tireChild;
    [SerializeField] private bool _frontTire = true;
    [SerializeField] private Transform _targetRotation;
    [SerializeField] private float _radius;
    private Vector3 _prevPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetRotation)
        {
            transform.rotation = _targetRotation.rotation;
        }
        if (_tireChild)
        {
            Vector3 delta = transform.position - _prevPos;
            delta = Vector3.ProjectOnPlane(delta, transform.right);
            float distance = delta.magnitude;
            if (Vector3.Dot(delta, transform.forward) < 0)
            {
                distance = -distance;
            }
            float deltaAngle = 360f * distance / (2 * Mathf.PI * _radius);
            _tireChild.localRotation = Quaternion.AngleAxis(deltaAngle, Vector3.right) * _tireChild.localRotation;
        }
        _prevPos = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        const float segments = 60;
        const float deltaAngle = 360 / segments;
        for (float deg = 0; deg <= 360 - deltaAngle; deg += deltaAngle)
        {
            Vector3 point1 = Vector3.up * _radius;
            point1 = Quaternion.AngleAxis(deg, transform.right) * point1;
            Vector3 point2 = Quaternion.AngleAxis(deltaAngle, transform.right) * point1;
            point1 += transform.position;
            point2 += transform.position;
            Gizmos.DrawLine(point1, point2);
        }
    }
}
