using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsável por criar a linha entre dois pinos
 */
public class RopeBuilder : MonoBehaviour
{
    public GameObject segmentPrefab;
    public int segmentCount = 20;
    public float segmentLength = 0.1f;
    public float stiffness = 100f;

    public LineRenderer lineRendererPrefab; // arraste um prefab ou instância configurada

    public Vector3 startPos = new Vector3(-4, -4, 4.3f);

    //public void BuildRope(Vector3 start, Vector3 end)
    //{
    //    List<GameObject> segments = new List<GameObject>();

    //    Vector3 direction = (end - start).normalized;
    //    float totalLength = Vector3.Distance(start, end);
    //    int count = Mathf.CeilToInt(totalLength / segmentLength);
    //    Vector3 delta = (end - start) / count;

    //    GameObject previous = null;

    //    for (int i = 0; i <= count; i++)
    //    {
    //        Vector3 pos = start + delta * i;
    //        GameObject segment = Instantiate(segmentPrefab, pos, Quaternion.identity);
    //        var segmentRb = segment.GetComponent<Rigidbody>();

    //        if (previous == null)
    //        {
    //            segmentRb.isKinematic = true;
    //        }
    //        else
    //        {
    //            segment.GetComponent<RopeSegment>().Init(previous.GetComponent<Rigidbody>(), segmentLength, stiffness);
    //        }

    //        segments.Add(segment);
    //        previous = segment;
    //    }

    //    previous.GetComponent<Rigidbody>().isKinematic = true;

    //    // Cria visual da linha
    //    LineRenderer lr = Instantiate(lineRendererPrefab);
    //    lr.positionCount = segments.Count;

    //    // Atualiza visual da linha em tempo real
    //    StartCoroutine(UpdateLineRenderer(lr, segments));
    //}

    IEnumerator UpdateLineRenderer(LineRenderer lr, List<GameObject> segments)
    {
        while (true)
        {
            for (int i = 0; i < segments.Count; i++)
            {
                lr.SetPosition(i, segments[i].transform.position);
            }

            yield return null;
        }
    }

    public GameObject CreateFreeRope()
    {
        

        List<GameObject> segments = new List<GameObject>();
        Vector3 delta = Vector3.right * segmentLength; // linha horizontal no chão

        GameObject previous = null;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 pos = startPos + delta * i;
            GameObject segment = Instantiate(segmentPrefab, pos, Quaternion.identity);
            var rb = segment.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = false;

            //if (previous != null)
            //{
            //    segment.GetComponent<RopeSegment>().Init(previous.GetComponent<Rigidbody>(), segmentLength, stiffness);
            //}

            segments.Add(segment);
            previous = segment;
        }

        // Criar linha visual
        // LineRenderer lr = Instantiate(lineRendererPrefab);
        // lr.positionCount = segments.Count;
        // StartCoroutine(UpdateLineRenderer(lr, segments));

        segments[0].tag = "RopeTip"; // Marcar a PONTA para interação

        return segments[0]; // retorna a ponta (interativa)
    }
}
