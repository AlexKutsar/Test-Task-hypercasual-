using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _ball = null;
    [SerializeField] private Transform _partLevel = null;

    private void Update()
    {
        transform.parent.position = _ball.transform.position;
        transform.parent.rotation = Quaternion.Euler(0, _partLevel.eulerAngles.y, 0);
    }
}
