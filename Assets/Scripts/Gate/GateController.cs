using System;
using System.Text;
using UnityEngine;

public class GateController : MonoBehaviour
{
    #region serializefields
    [SerializeField] private GateInnerController leftGateInner;
    [SerializeField] private GateInnerController rightGateInner;
    [SerializeField] private Material goodMaterial;
    [SerializeField] private Material badMaterial;
    #endregion
    #region fields
    private StringBuilder stringBuilder = new StringBuilder();
    private int leftGateScore;
    private int rightGateScore;
    private int maximumGoodScore = 500;
    private int minimumGoodScore = -500;
    private string[] goodLabels = { "Pickpocket", "Dance", "Soliciting", "Black Market" };
    private string[] badLabels = { "Arrested", "Bad Dancing", "Items stolen", "Police Raid" };
    #endregion
    public static event Action<int> GateTriggeredEvent;
    private delegate int GateRandom(int minInclusive, int maxExclusive);
    private GateRandom gateRandom = UnityEngine.Random.Range;

    private void Update()
    {
        if (leftGateInner.IsTriggered)
            OnGateClosed(true);
        else if (rightGateInner.IsTriggered)
            OnGateClosed(false);
    }

    private void OnEnable() => SetInnerGates();

    private void OnDisable()
    {
        enabled = false;
    }

    private void OnGateClosed(bool isLeftTriggered)
    {
        var innerGates = GetComponentsInChildren<GateInnerController>();
        foreach (var innerGate in innerGates)
            innerGate.OnGateClosed();
        GateTriggeredEvent?.Invoke(isLeftTriggered ? leftGateScore : rightGateScore);
        OnDisable();
    }

    private void SetInnerGates()
    {
        bool isLeftGood = gateRandom(0, 2) == 1;
        leftGateScore = GenerateScore(isLeftGood);
        rightGateScore = GenerateScore(!isLeftGood);
        leftGateInner.SetInnerGate(GenerateLabel(isLeftGood, leftGateScore)
            , isLeftGood
            , isLeftGood ? goodMaterial : badMaterial);
        rightGateInner.SetInnerGate(GenerateLabel(!isLeftGood, rightGateScore)
            , !isLeftGood
            , !isLeftGood ? goodMaterial : badMaterial);
    }

    private int GenerateScore(bool isGood)
        => gateRandom(0, isGood ? maximumGoodScore : minimumGoodScore);

    private string GenerateLabel(bool isGood, int score)
    {
        return stringBuilder
            .Clear()
            .Append(
                isGood ?
                goodLabels[gateRandom(0, goodLabels.Length)] :
                badLabels[gateRandom(0, badLabels.Length)])
            .Append("\n")
            .Append(score)
            .ToString();
    }
}
