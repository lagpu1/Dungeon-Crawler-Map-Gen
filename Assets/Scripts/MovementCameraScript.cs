using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MovementCameraScript : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float zoomSpeed = 100;

    float x, y, zoom;

    PixelPerfectCamera camera;

    void Start()
    {
        camera = this.GetComponent<PixelPerfectCamera>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        zoom = Input.GetAxisRaw("Mouse ScrollWheel");

        gameObject.transform.Translate(x * speed, y * speed, 0);

        camera.assetsPPU -= (int)(zoom * zoomSpeed);
    }
}
