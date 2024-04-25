using UnityEngine;

public class HandTrackingManager : MonoBehaviour
{
    public OVRHand rightHand;

    private OVRHand.TrackingConfidence _confidence;
    private bool _isIndexFingerPinching;
    private bool _hasPinched;
    private bool _wasPinchingLastFrame = false;

    void Update()
    {
        CheckPinch();
    }
        
    public bool IsPinching()
    {
        return _hasPinched;
    }

    private void CheckPinch()
    {
        bool isCurrentlyPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        _confidence = rightHand.GetFingerConfidence(OVRHand.HandFinger.Index);

        if (!_wasPinchingLastFrame && isCurrentlyPinching && _confidence == OVRHand.TrackingConfidence.High)
        {
            _hasPinched = true;
        }
        else
        {
            _hasPinched = false;
        }
        _wasPinchingLastFrame = isCurrentlyPinching;
    }

    public OVRHand GetDominateHand()
    {
        return rightHand;
    }
}