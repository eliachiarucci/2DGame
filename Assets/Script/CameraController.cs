using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    public static Transform target;

    public Tilemap tileMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
        bottomLeftLimit = tileMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = tileMap.localBounds.max - new Vector3(halfWidth, halfHeight, 0f);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        if (target)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
        }
    }
}
