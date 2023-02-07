using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector3 targetPosition;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        targetPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        transform.position = targetPosition;
    }
}
