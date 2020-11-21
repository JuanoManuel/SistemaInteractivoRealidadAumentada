using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public Light currentLight;

    private void OnEnable()
    {
        arCameraManager.frameReceived += FrameUpdate;
    }

    private void OnDisable()
    {
        arCameraManager.frameReceived -= FrameUpdate;
    }

    private void FrameUpdate(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
            currentLight.intensity = args.lightEstimation.averageBrightness.Value;
        if (args.lightEstimation.averageColorTemperature.HasValue)
            currentLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        if (args.lightEstimation.colorCorrection.HasValue)
            currentLight.color = args.lightEstimation.colorCorrection.Value;
    }
}
