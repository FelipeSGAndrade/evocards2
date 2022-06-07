using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float movementSpeed = 5;
    float zoomSpeed = 500;
    new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0) * movementSpeed * Time.deltaTime);

        var zoomMovement = Input.GetAxis("Mouse ScrollWheel");

        camera.orthographicSize -= zoomMovement * zoomSpeed * Time.deltaTime;
    }
}
