using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public Vector2 cameraMoveDir;

    public void Move(Vector2 movementInput)
    {
        cameraMoveDir = movementInput;
    }

    public void Update()
    {
        var normalizedMoveDir = cameraMoveDir.normalized;
        Vector3 moveDirTranslated = new Vector3(normalizedMoveDir.x, 0f, normalizedMoveDir.y);
        gameObject.transform.Translate(moveDirTranslated * cameraSpeed * Time.deltaTime);
    }
}
