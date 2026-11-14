using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    InputSystem inputSystem;

    private void OnEnable()
    {
        if (inputSystem == null)
        {
            inputSystem = new InputSystem();

            inputSystem.Camera.Get().actionTriggered += OnActionTriggered;
        }

        inputSystem.Enable();
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
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        ReferenceStorer.Instance.cameraController.Move(context.ReadValue<Vector2>());
    }

    private void Select(InputAction.CallbackContext context)
    {
        ReferenceStorer.Instance.cameraController.Move(context.ReadValue<Vector2>());
    }
}


