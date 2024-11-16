using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWallMotion : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _limitDist;

    private void Update()
    {
        foreach (Transform child in transform) {
            child.position += _speed * Time.deltaTime * child.right;
            if (child.localPosition.x > _limitDist) child.localPosition -= Vector3.right * (_limitDist*2);
        }
    }
}
