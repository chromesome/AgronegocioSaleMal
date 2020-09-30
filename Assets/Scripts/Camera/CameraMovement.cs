using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 2f;
    public float scrollSpeed = 200f;


    void Update()
    {
        Vector3 pos = transform.position;

        #region camera movement
        if (Input.GetKey("w"))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        transform.position = pos;
        #endregion

        // Camera zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * scrollSpeed * Time.deltaTime;
    }
}
