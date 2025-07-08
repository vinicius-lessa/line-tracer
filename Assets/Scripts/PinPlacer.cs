using UnityEngine;

public class PinPlacer : MonoBehaviour
{
    public GameObject pinPrefab;
    public GameObject wallObject;

    public int maxPins = 5;
    private int placedPins = 0;

    void Update()
    {
        if (placedPins >= maxPins)
            return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            PlacePin(Input.mousePosition);
        }
#else
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        PlacePin(Input.GetTouch(0).position);
    }
#endif
    }

    void PlacePin(Vector3 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            // Verifica se o objeto atingido é a parede/quadro
            if (hit.transform.gameObject == wallObject)
            {
                // Calcule a rotação correta
                Quaternion pinRotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(90, 0, 0);

                // Obtenha a altura do pino (eixo Y é a altura)
                float pinHeight = pinPrefab.GetComponent<Renderer>().bounds.extents.y;

                // Ajuste a posição para que a base do pino fique na superfície
                Vector3 pinPosition = hit.point + (hit.normal) * pinHeight;

                Instantiate(pinPrefab, pinPosition, pinRotation);
                placedPins++;
            }
        }
    }
}
