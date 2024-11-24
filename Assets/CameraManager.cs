using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Security.Permissions;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cmVirtualCamera;
    public Camera mainCamera;

    private bool usingVirtualCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            usingVirtualCamera = !usingVirtualCamera;

            if (usingVirtualCamera)
            {
                cmVirtualCamera.gameObject.SetActive(true);
            }
            else
            {
                cmVirtualCamera.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
            }

            if (!usingVirtualCamera)
            {
                float x = Input.mousePosition.x;
                float y = Input.mousePosition.y;

                if (x < 10)
                {
                    mainCamera.transform.position -= Vector3.left * Time.deltaTime * 10;
                }
                else if (x > Screen.width - 10)
                {
                    mainCamera.transform.position -= Vector3.right * Time.deltaTime * 10;
                }

                if (y < 10)
                {
                    mainCamera.transform.position -= Vector3.back * Time.deltaTime * 10;
                }
                else if (y > Screen.height - 10)
                {
                    mainCamera.transform.position -= Vector3.forward * Time.deltaTime * 10;
                }
            }
        }
    }
}
