using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    InputSystem inputSystem;
    bool isRotating;

    private void OnEnable()
    {
        if (inputSystem == null)
        {
            inputSystem = new InputSystem();
            inputSystem.Camera.Get().actionTriggered += OnActionTriggered;
        }

        inputSystem.Enable();
    }

    private void OnDisable()
    {
        isRotating = false;
        inputSystem?.Disable();
    }

    private void OnDestroy()
    {
        inputSystem?.Dispose();
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action)
        {
            case var action when action == inputSystem.Camera.Move:
                Move(context);
                break;
            case var action when action == inputSystem.Camera.Select:
                Select(context);
                break;
            case var action when action == inputSystem.Camera.Sprint:
                Sprint(context);
                break;
            case var action when action == inputSystem.Camera.Zoom:
                Zoom(context);
                break;
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        ReferenceStorer.Instance.cameraController.Move(context.ReadValue<Vector2>());
    }

    private void Select(InputAction.CallbackContext context)
    {
        if (context.performed && Mouse.current != null)
            ReferenceStorer.Instance.cameraController.BeginRotate(Mouse.current.position.ReadValue());
        else if (context.canceled)
            ReferenceStorer.Instance.cameraController.EndRotate();
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        ReferenceStorer.Instance.cameraController.Sprint(context);
    }

    private void Zoom(InputAction.CallbackContext context)
    {
        ReferenceStorer.Instance.cameraController.Zoom(context.ReadValue<Vector2>());
    }
}