using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControlPointController : MonoBehaviour
{
    private const float DISTANCE_THRESHOLD = .01f;

    [SerializeField] private Transform _leadingPoint;
    [SerializeField] private Transform _controllingTransform;
    private float _distanceToLeadingPoint;
    private Matrix4x4 _controllingTransformDelta;

    // Start is called before the first frame update
    void Start()
    {
        _distanceToLeadingPoint = (_leadingPoint.position - transform.position).magnitude;
        if (_controllingTransform)
        {
            _controllingTransformDelta = transform.worldToLocalMatrix * _controllingTransform.localToWorldMatrix;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistance = (_leadingPoint.position - transform.position).magnitude;
        float deltaDistance = currentDistance - _distanceToLeadingPoint;
        if (Mathf.Abs(deltaDistance) >= DISTANCE_THRESHOLD)
        {
            Vector3 toLeadingPoint = (_leadingPoint.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(toLeadingPoint, Vector3.up);
            transform.position += toLeadingPoint * deltaDistance;
        }
        if (_controllingTransform)
        {
            Matrix4x4 controllingPosition = transform.localToWorldMatrix * _controllingTransformDelta;
            _controllingTransform.position = controllingPosition.MultiplyPoint(Vector3.zero);
            _controllingTransform.rotation = controllingPosition.rotation;
        }
    }
}
