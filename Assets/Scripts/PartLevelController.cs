using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartLevelController : MonoBehaviour
{
    [SerializeField] private Transform _worldAxis;
    [SerializeField] private GameObject _ball;
    //[SerializeField] private Rigidbody rigidbody = null;
    [SerializeField] private Camera _camera = null;
    private bool canMove = false;
    private Vector2 _mousePosition => GetMousePosition();
    private Vector2 _initialMousePos;
    private Vector3 _deltaRotation;

    [Header("Параметры движения уровня")]
    [SerializeField] private float _maxDeltaRotation = 10f;
    [SerializeField] private float _speedRotation = 0.5f;
    [SerializeField] private float _maxOffsetAngleX = 20f;
    [SerializeField] private float _maxOffsetAngleZ = 20f;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        //rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //ChangeRotation();
        ChangeRotation2();
    }
    private void ChangeRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canMove = true;
            _initialMousePos = _mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (canMove)
            {
                Vector2 newMousePosition = _mousePosition;
                Vector2 deltaMousePosition = newMousePosition - _initialMousePos;
                //Debug.Log(newMousePosition);
                float distanceMousePositionX = newMousePosition.x - _initialMousePos.x;
                distanceMousePositionX = Mathf.Clamp(distanceMousePositionX = (distanceMousePositionX > 0) ? distanceMousePositionX : -distanceMousePositionX,
                                                           0f,
                                                           _maxDeltaRotation);
                float distanceMousePositionY = newMousePosition.y - _initialMousePos.y;
                distanceMousePositionY = Mathf.Clamp(distanceMousePositionY = (distanceMousePositionY > 0) ? distanceMousePositionY : -distanceMousePositionY,
                                                           0f,
                                                           _maxDeltaRotation);
                deltaMousePosition.x = Mathf.Clamp(deltaMousePosition.x, -_maxDeltaRotation, _maxDeltaRotation);
                deltaMousePosition.y = Mathf.Clamp(deltaMousePosition.y, -_maxDeltaRotation, _maxDeltaRotation);
                
                //Получение оси и угла поворота
                float angle = 0f;
                Vector3 axis;
                transform.rotation.ToAngleAxis(out angle, out axis);
                if (deltaMousePosition.x < 0)
                {
                    if (angle > _maxOffsetAngleX && axis.z > 0f)
                    {
                        deltaMousePosition.x = 0;
                        distanceMousePositionX = 0;
                    }
                }
                if (deltaMousePosition.x > 0)
                {
                    if (angle > _maxOffsetAngleX && axis.z < 0f)
                    {
                        deltaMousePosition.x = 0;
                        distanceMousePositionX = 0;
                    }
                }
                if (deltaMousePosition.y > 0)
                {
                    if (angle > _maxOffsetAngleX && axis.x > 0f)
                    {
                        deltaMousePosition.y = 0;
                        distanceMousePositionY = 0;
                    }
                }
                if (deltaMousePosition.y < 0)
                {
                    if (angle > _maxOffsetAngleX && axis.x < 0f)
                    {
                        deltaMousePosition.y = 0;
                        distanceMousePositionY = 0;
                    }
                }
                _deltaRotation = new Vector3(deltaMousePosition.y, 0f, -deltaMousePosition.x);
                Debug.Log(1+distanceMousePositionX);
                transform.RotateAround(_ball.transform.position, _deltaRotation, _speedRotation * (1+distanceMousePositionX) * (1+ distanceMousePositionY) * Time.deltaTime);
                //Debug.Log(_speedRotation * distanceMousePositionX * distanceMousePositionY * Time.deltaTime);
            }
        }
    }

    private void ChangeRotation2()
    {
        if (Input.GetMouseButton(0))
        {

            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            float angle = 0f;
            Vector3 axis;
            transform.rotation.ToAngleAxis(out angle, out axis);
            /*if (mouseX < 0 && angle > _maxOffsetAngleX && axis.z > 0f)
            {
                mouseX = 0;
            }
            if (mouseX >= 0 && angle > _maxOffsetAngleX && axis.z < 0f)
            {
                mouseX = 0;
            }
            if (mouseY >= 0 && angle > _maxOffsetAngleZ && axis.x > 0f)
            {
                mouseY = 0;
            }
            if (mouseY < 0 && angle > _maxOffsetAngleZ && axis.x < 0f)
            {
                mouseY = 0;
            }*/
            //transform.eulerAngles.z % 360
            /*float angleX = transform.eulerAngles.x;
            angle = (angle > 180) ? angle - 360 : angle;*/
            //Debug.Log(transform.eulerAngles.x - 360);
            float angleZ = transform.eulerAngles.z;
            angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;
            if (mouseX < 0 && angleZ >= _maxOffsetAngleX)
            {
                Debug.Log("Влево нельзя");
                mouseX = 0;
            }
            if (mouseX > 0 && angleZ <= -_maxOffsetAngleX)
            {
                Debug.Log("Вправо нельзя");
                mouseX = 0;
            }
            float angleX = transform.eulerAngles.x;
            angleX = (angleX > 180) ? angleX - 360 : angleX;
            if (mouseY > 0 && angleX >= _maxOffsetAngleZ)
            {
                Debug.Log("Вверх нельзя");
                mouseY = 0;
            }
            if (mouseY < 0 && angleX <= -_maxOffsetAngleZ)
            {
                Debug.Log("Вниз нельзя");
                mouseY = 0;
            }
            //Debug.Log(new Vector2(mouseX, mouseY));
            var input = new Vector2(mouseY, -mouseX);
            input = input.Rotate(-transform.eulerAngles.y);
            // transform.RotateAround(_ball.transform.position, new Vector3(mouseY, 0, -mouseX), _speedRotation * (new Vector2(mouseX, mouseY)).magnitude * Time.deltaTime * 1000);
            transform.RotateAround(_ball.transform.position, new Vector3(input.x, 0, input.y), _speedRotation * (new Vector2(input.x, input.y)).magnitude * Time.deltaTime * 1000);
            // float angleY = (transform.eulerAngles.y >= 90) ? transform.eulerAngles.y % 90 : transform.eulerAngles.y;
            // transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if ((Input.touchCount > 0) || (Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                Touch Touch0 = Input.GetTouch(0);
                float DeltaTime = (Touch0.deltaTime > 0.0f) ? (Time.deltaTime / Touch0.deltaTime) : 0.0f;
                float mouseX = Touch0.deltaPosition.x * DeltaTime;
                float mouseY = Touch0.deltaPosition.y * DeltaTime;
                float angle = 0f;
                Vector3 axis;
                transform.rotation.ToAngleAxis(out angle, out axis);
                if (mouseX < 0 && angle > _maxOffsetAngleX && axis.z > 0f)
                {
                    mouseX = 0;
                }
                if (mouseX >= 0 && angle > _maxOffsetAngleX && axis.z < 0f)
                {
                    mouseX = 0;
                }
                if (mouseY >= 0 && angle > _maxOffsetAngleZ && axis.x > 0f)
                {
                    mouseY = 0;
                }
                if (mouseY < 0 && angle > _maxOffsetAngleZ && axis.x < 0f)
                {
                    mouseY = 0;
                }
                transform.RotateAround(_ball.transform.position, new Vector3(mouseY, 0, -mouseX), _speedRotation * (new Vector2(mouseX, mouseY)).magnitude);
            }
        }

        
        //Debug.Log(transform.eulerAngles);
        //transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, -30.0f, 30.0f), 0, Mathf.Clamp(transform.eulerAngles.y, -30.0f, 30.0f));
    }



    private Vector2 GetMousePosition()
    {
        var z = 0;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
        float distance;
        xy.Raycast(ray, out distance);
        var res = ray.GetPoint(distance);
        return new Vector2(res.x, res.y);
    }
    
    
}

public static class Vector2Extension {
    public static Vector2 Rotate(this Vector2 v, float degrees) {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
         
        float tx = v.x;
        float ty = v.y;
 
        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }
}
