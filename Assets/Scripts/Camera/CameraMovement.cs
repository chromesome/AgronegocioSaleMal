using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 2f;
    public float dragSpeed = 40f;
    public float scrollSpeed = 200f;

    bool dragging = false;

    void Update()
    {
        Vector3 pos = transform.position;

        #region camera movement
        if (Input.GetMouseButtonDown(1))
            dragging = true;

        if (Input.GetMouseButtonUp(1))
            dragging = false;

        if(dragging)
        {
            pos.x -= Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime;
            pos.y -= Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime;
        }
        else
        {
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
        }

        // TODO: diego - limitar X e Y para no irse al carajo
        // necesitamos calcular segun tamaño del mapa cargado
        transform.position = pos;
        #endregion

        // Camera zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = Camera.main.orthographicSize - scroll * scrollSpeed * Time.deltaTime;
        Camera.main.orthographicSize = Mathf.Clamp(newZoom, 1, 3);
        
    }
}
