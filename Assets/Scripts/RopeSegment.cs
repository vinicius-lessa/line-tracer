using UnityEngine;

/*
 * Este script permite que esse segmento seja conectado ao anterior via SpringJoint com rigidez e elasticidade customizáveis.
 */
public class RopeSegment : MonoBehaviour
{
    public Rigidbody rb;
    public SpringJoint joint;

    public void Init(Rigidbody connectedBody, float length, float stiffness)
    {
        rb = GetComponent<Rigidbody>();
        joint = gameObject.AddComponent<SpringJoint>();

        joint.connectedBody = connectedBody;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = Vector3.zero;
        joint.spring = stiffness;
        joint.damper = 0.1f;
        joint.minDistance = length;
        joint.maxDistance = length;
    }
}
