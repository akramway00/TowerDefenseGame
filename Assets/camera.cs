using UnityEngine;

public class camera : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = transform;
    }

    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        cameraTransform.Rotate(0, horizontal, 0);
    }
}