using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBox : MonoBehaviour
{
    [SerializeField]
    float width;
    [SerializeField]
    float height;

    SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(transform.position.x - width / 2, transform.position.y - height / 2, 0), new Vector3(transform.position.x - width / 2, transform.position.y + height / 2, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - width / 2, transform.position.y + height / 2, 0), new Vector3(transform.position.x + width / 2, transform.position.y + height / 2, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + width / 2, transform.position.y + height / 2, 0), new Vector3(transform.position.x + width / 2, transform.position.y - height / 2, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + width / 2, transform.position.y - height / 2, 0), new Vector3(transform.position.x - width / 2, transform.position.y - height / 2, 0));
    }

    public void SetSprite(Sprite sprite) {
        var sizeX = sprite.bounds.size.x;
        var sizeY = sprite.bounds.size.y;

        float scale;
        if (sizeX/width > sizeY/height) {
            scale = width / sizeX;
        } else {
            scale = height / sizeY;
        }

        transform.localScale = new Vector3(scale, scale, 1);
        spriteRenderer.sprite = sprite;
    }
}
