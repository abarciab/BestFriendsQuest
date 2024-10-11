using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxZoom;
    [SerializeField] private float _zoomSpeed;
    
    [SerializeField]
    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        var z = transform.position.z;
        z += delta * _zoomSpeed * Time.deltaTime * 10;
        z = Mathf.Clamp(z, _minMaxZoom.x, _minMaxZoom.y);
        var pos = transform.position;
        pos.z = z;
        transform.position = pos;
    }
}
