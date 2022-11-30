using UnityEngine;
using TMPro;

public class GateInnerController : MonoBehaviour
{
    private bool isTriggered = false;
    public bool IsTriggered { get => isTriggered; }
    private MeshRenderer gateMaterial;
    private TextMeshPro gateLabel;

    private void Awake()
    {
        gateMaterial = GetComponent<MeshRenderer>();
        gateLabel = GetComponentInChildren<TextMeshPro>();
    }

    public void OnGateClosed() => enabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
            isTriggered = true;
    }

    public void SetInnerGate(string label, bool isGood, Material material)
    {
        gateLabel.text = label;
        gateMaterial.material = material;
    }
}
