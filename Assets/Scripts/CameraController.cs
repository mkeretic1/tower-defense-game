using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool lockScreen = true;

    public float speed = 0.5f;
    public float borderThickness = 2f;
    public float scrollSpeed = 3f;
    public float minY = 10f;
    public float maxY = 80f;
    public float minX = -100;
    public float maxX = 100;
    public float minZ = -100;
    public float maxZ = 100;

    void Update()
    {
        if (GameManager.gameOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown("l"))
        {
            lockScreen = !lockScreen;
        }

        if (lockScreen)
        {
            return;
        }

        if (Input.mousePosition.y >= Screen.height - borderThickness)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.y <= borderThickness)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.x >= Screen.width - borderThickness)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.x <= borderThickness)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}
