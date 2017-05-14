using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody ThisBody = null;
    private Transform ThisTransform = null;

    public bool MouseLook = true;
    public string HorzAxis = "Horizontal";
    public string VertAxis = "Vertical";
    public string FireAxis = "Fire1";
    public float MaxSpeed = 5f;

    // Use this for initialization
    public void Awake()
    {
        ThisBody = GetComponent<Rigidbody>();
        ThisTransform = GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        // Update movement
        float Horz = Input.GetAxis(HorzAxis);
        float Vert = Input.GetAxis(VertAxis);
        Vector3 MoveDirection = new Vector3(Horz, 0f, Vert);
        ThisBody.AddForce(MoveDirection.normalized * MaxSpeed);

        // Clamp speed
        ThisBody.velocity = new Vector3(
            Mathf.Clamp(ThisBody.velocity.x, -MaxSpeed, MaxSpeed),
            Mathf.Clamp(ThisBody.velocity.y, -MaxSpeed, MaxSpeed),
            Mathf.Clamp(ThisBody.velocity.z, -MaxSpeed, MaxSpeed));

        // Should we look with the mouse?
        if (MouseLook)
        {
            // Update rotation, turn to face mouse pointer
            Vector3 MousePosWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            MousePosWorld = new Vector3(MousePosWorld.x, 0f, MousePosWorld.z);

            // Get direction to cursor
            Vector3 LookDirection = MousePosWorld - ThisTransform.position;

            // FixedUpdate rotation
            ThisTransform.localRotation = Quaternion.LookRotation(LookDirection.normalized, Vector3.up);
        }
    }
}
