using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private Vector3 offset;

    public float moveSpeed = 15f;

    void Start()
    {
        AssignTarget();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
         transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.deltaTime);

    }

    private void AssignTarget()
    {
        target = FindObjectOfType<PlayerController>().transform;
        offset = transform.position;
    }

}
