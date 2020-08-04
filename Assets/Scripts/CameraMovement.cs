using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _ball = null;
    //[SerializeField] private GameObject _partLevel = null;
    private Rigidbody rigidbody = null;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        offset = transform.position - _ball.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody.MovePosition(_ball.transform.position + offset);
        //rigidbody.MoveRotation(Quaternion.Euler(new Vector3(transform.rotation.x, _partLevel.transform.rotation.y, transform.rotation.z)));
    }
}
