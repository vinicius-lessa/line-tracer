using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public RopeBuilder ropeBuilder;
    private List<GameObject> selectedPins = new List<GameObject>();

    public int totalPinsRequired = 1;
    private bool ropeInteractionEnabled = false;
    private GameObject ropeTip;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch(Input.mousePosition);
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleTouch(Input.GetTouch(0).position);
        }
#endif
    }

    void HandleTouch(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Pin"))
            {
                GameObject pin = hit.collider.gameObject;

                selectedPins.Add(pin);

                // if (selectedPins.Count >= 2)
                // {
                //    var pinA = selectedPins[selectedPins.Count - 2];
                //    var pinB = selectedPins[selectedPins.Count - 1];

                //    ropeBuilder.BuildRope(pinA.transform.position, pinB.transform.position);
                // }
            }
        }

        if (selectedPins.Count >= totalPinsRequired && !ropeInteractionEnabled)
        {
            ropeInteractionEnabled = true;
            
            ropeTip = ropeBuilder.CreateFreeRope();

            // Habilita "Drag" no final da corda
            ropeTip.AddComponent<RopeTipDrag>();
        }
    }
}
