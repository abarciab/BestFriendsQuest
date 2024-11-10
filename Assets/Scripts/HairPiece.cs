using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HairPiece : MonoBehaviour
{
    [SerializeField] private Transform _modelParent;
    [SerializeField] private float _debugLength = 10;

    private void Update()
    {
        //UpdatePosition();
    }

    public void UpdatePosition()
    {
        _modelParent.localPosition = Vector3.forward * 2.5f;
        var dir = transform.forward * -1;
        bool hit = Physics.Raycast(_modelParent.position, dir, out var hitData, 100);
        //Debug.DrawLine(_modelParent.position, _modelParent.position + dir * -_debugLength, Color.red);
        if (!hit) {
            return;
        }

        //Debug.DrawLine(hitData.point, hitData.point + hitData.normal * _debugLength, Color.green);
        _modelParent.up = hitData.normal;
        _modelParent.position = hitData.point;
    }
}
