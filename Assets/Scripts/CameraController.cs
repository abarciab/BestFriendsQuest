using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxZoom;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private Transform _character;
    
    [SerializeField]
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) Scroll();
    }

    private void Scroll()
    {
        var dist = Vector3.Distance(transform.position, _character.position);
        var dir = (_character.position - transform.position).normalized;

        float scrollDelta = Input.mouseScrollDelta.y;
        var dirMod = scrollDelta * _zoomSpeed * Time.deltaTime * 10;
        if ( (dist + dirMod > _minMaxZoom.y && scrollDelta < 0) || (dist + dirMod < _minMaxZoom.x && scrollDelta > 0)) return;
        
        var posDelta = dir * dirMod;
        transform.position += posDelta;

    }
}
