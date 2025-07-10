using UnityEngine;

public class RopeTipDrag : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Drag(Input.mousePosition);
        }
#else
        if (Input.touchCount > 0)
        {
            Drag(Input.GetTouch(0).position);
        }
#endif
    }

    void Drag(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("RopeTip"))
            {
                Vector3 targetPos = hit.point;
                rb.MovePosition(Vector3.Lerp(rb.position, targetPos, 0.5f));
            }
        }
    }
}
