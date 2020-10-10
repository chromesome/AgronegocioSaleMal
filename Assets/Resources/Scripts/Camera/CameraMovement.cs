using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 2f;
    public float dragSpeed = 40f;
    public float scrollSpeed = 200f;
    public float maxZoom = 1;
    public float minZoom = 4;

    bool dragging = false;

    void Start()
    {
        int currentLevel = GameManager.instance.level;

        if (currentLevel == 3)
        {
            SetMoodMid();
        }

        if (currentLevel == 4)
        {
            SetMoodFulero();
        }
    }

    private void SetMoodMid()
    {
        Camera.main.backgroundColor = new Color32(71, 93, 128, 0);
    }

    private void SetMoodFulero()
    {
        Camera.main.backgroundColor = new Color32(41, 51, 65, 0);
    }

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
        Camera.main.orthographicSize = Mathf.Clamp(newZoom, maxZoom, minZoom);
        
    }
}
