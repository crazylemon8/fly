using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float horizontalSpeed = 5f;
    public float flySpeed = 5f;

    private Rigidbody rb;
    private float horizontalInput;
    private float altitudeInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadKeyboardInput();
        ReadTouchInput();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            horizontalInput * horizontalSpeed,
            altitudeInput * flySpeed,
            0f
        );
    }

    private void ReadKeyboardInput()
    {
        horizontalInput = 0f;
        altitudeInput = 0f;

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            horizontalInput = -1f;
        }

        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            horizontalInput = 1f;
        }

        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            altitudeInput = -1f;
        }

        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed || Keyboard.current.spaceKey.isPressed)
        {
            altitudeInput = 1f;
        }
    }

    private void ReadTouchInput()
    {
        if (Touchscreen.current == null)
        {
            return;
        }

        foreach (TouchControl touch in Touchscreen.current.touches)
        {
            if (!touch.press.isPressed)
            {
                continue;
            }

            Vector2 position = touch.position.ReadValue();

            if (position.x < Screen.width * 0.5f)
            {
                horizontalInput = position.x < Screen.width * 0.25f ? -1f : 1f;
                continue;
            }

            float normalizedY = position.y / Screen.height;
            altitudeInput = Mathf.Clamp((normalizedY - 0.5f) * 2f, -1f, 1f);
        }
    }
}
