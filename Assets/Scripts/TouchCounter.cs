using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TouchCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    private int count = 0;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            count++;
            counterText.text = count.ToString();
        }

        if (Input.GetMouseButtonDown(0))
        {
            count++;
            counterText.text = count.ToString();
        }
    }
}
