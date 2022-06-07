using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedInScreen : MonoBehaviour
{
    [SerializeField]
    Vector2 padding = new Vector2(2, 2);
    [SerializeField]
    float zPosition = 5;
    [SerializeField]
    float baseCameraOrthographicSize = 6;
    [SerializeField]
    bool horizontalCenter;
    [SerializeField]
    bool verticalCenter;

    new Camera camera;

    void Awake() {
        camera = Camera.main;
    }

    void Update() {
        var verticalExtent = camera.orthographicSize;
        var horizontalExtent = verticalExtent * ((float)Screen.width / (float)Screen.height);
        var scale = verticalExtent / baseCameraOrthographicSize;

        var position = new Vector3(0, 0, zPosition);

        if (horizontalCenter) {
            position.x = - (padding.x * scale);
        } else {
            position.x = (padding.x * scale) - horizontalExtent;
        }

        if (verticalCenter) {
            position.y = - (padding.y * scale);
        } else {
            position.y = (padding.y * scale) - verticalExtent;
        }

        transform.localPosition = position;
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
