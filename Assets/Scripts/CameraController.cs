using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public float rotationSpeed = 0.2f;
    public Vector2 cameraMoveDir;

    private Vector2 rotationInput;
    private Vector3 rotationPivot;
    private bool isRotating;

    public void Move(Vector2 movementInput)
    {
        cameraMoveDir = movementInput;
    }

    private void OnMove()
    {
        var normalizedMoveDir = cameraMoveDir.normalized;
        Vector3 moveDirTranslated = new Vector3(normalizedMoveDir.x, 0f, normalizedMoveDir.y);
        transform.Translate(moveDirTranslated * cameraSpeed * Time.deltaTime);
    }

    public void BeginRotate(Vector2 mousePosition)
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            rotationPivot = ray.GetPoint(distance);
            rotationInput = Vector2.zero;
            isRotating = true;
        }
    }

    public void Rotate(Vector2 movementInput)
    {
        rotationInput = movementInput;
    }

    public void EndRotate()
    {
        isRotating = false;
        rotationInput = Vector2.zero;
    }

    private void OnRotate()
    {
        if (!isRotating || Mouse.current == null) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        transform.RotateAround(rotationPivot, Vector3.up, mouseDelta.x * rotationSpeed);
        transform.RotateAround(rotationPivot, transform.right, -mouseDelta.y * rotationSpeed);
    }

    private void LateUpdate()
    {
        OnMove();
        OnRotate();
    }
}