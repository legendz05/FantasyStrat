
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public float zoomSpeed = 100f;
    public float rotationSpeed = 0.2f;
    private float verticalRotation;

    public Vector2 cameraMoveDir;
    private Vector3 rotationPivot;
    private bool isRotating;

    public void Move(Vector2 movementInput)
    {
        cameraMoveDir = movementInput;
    }

    private void OnMove()
    {
        if (isRotating) return;

        var normalizedMoveDir = cameraMoveDir.normalized;
        Vector3 moveDirTranslated = new Vector3(normalizedMoveDir.x, 0f, normalizedMoveDir.y);
        transform.Translate(moveDirTranslated * cameraSpeed * Time.deltaTime);
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cameraSpeed *= 2f;
        }
        else if (context.canceled)
        {
            cameraSpeed /= 2f;
        }
    }

    public void Zoom(Vector2 zoomInput)
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        cam.transform.Translate(Vector3.forward * zoomInput.y * zoomSpeed * Time.deltaTime);
    }

    public void BeginRotate(Vector2 mousePosition)
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        isRotating = groundPlane.Raycast(ray, out float distance);

        if (isRotating)
            rotationPivot = ray.GetPoint(distance);
    }

    public void EndRotate()
    {
        isRotating = false;
    }

    private void OnRotate()
    {
        if (!isRotating || Mouse.current == null) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        transform.RotateAround(rotationPivot, Vector3.up, mouseDelta.x * rotationSpeed);

        float requestedRotation = -mouseDelta.y * rotationSpeed;
        float targetRotation = Mathf.Clamp(verticalRotation + requestedRotation, -45f, 45f);
        float allowedRotation = targetRotation - verticalRotation;

        transform.RotateAround(rotationPivot, transform.right, allowedRotation);

        verticalRotation = targetRotation;
    }

    private void LateUpdate()
    {
        OnMove();
        OnRotate();
    }
}
