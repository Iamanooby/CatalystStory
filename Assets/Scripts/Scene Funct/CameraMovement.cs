using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public VectorValue camMinPos;
    public VectorValue camMaxPos;

    void Start()
    {
        minPosition = camMinPos.initialValue;
        maxPosition = camMaxPos.initialValue;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            // Set variable for target.transform.position
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            
            // Get camera to chase (Transform) target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }

    }
}
