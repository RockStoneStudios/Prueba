using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSense = 100f;
    [SerializeField] private Transform player, playerArms;

    private float xAxisClamp = 0f;

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        float rotateX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        xAxisClamp -= rotateY;
        xAxisClamp = Mathf.Clamp(xAxisClamp, -90f, 90f);

        Vector3 rotPlayerArms = new Vector3(xAxisClamp, 0f, 0f);
        playerArms.localRotation = Quaternion.Euler(rotPlayerArms);

        player.Rotate(Vector3.up * rotateX);
    }
}
