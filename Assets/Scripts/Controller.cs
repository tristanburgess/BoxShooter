using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

    public float translateMagnitude = 3.0f;
    public float gravityMagnitude = 9.81f;

    private CharacterController playerController;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Debug.Log("Horizontal Input = " + Input.GetAxis("Horizontal"));
        Debug.Log("Vertical Input = " + Input.GetAxis("Vertical"));

        Vector3 movementZ = Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * translateMagnitude;
        Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * translateMagnitude;

        Vector3 movement = transform.TransformDirection(movementZ + movementX);

        movement.y -= gravityMagnitude * Time.deltaTime;

        // Debug.Log("Movement Vector = " + movement);

        playerController.Move(movement);
    }
}
