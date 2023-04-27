using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class LaboratoryInputs : MonoBehaviour {

    bool triggerValue, primaryButton, primaryTouch,
    secondaryButton, secondaryTouch, gripButton, primary2DAxistouch, primary2DAxisclick,
    isTracked, isTrackedG, screenIsOn, tabletIsOn, userPresence;

    public TextMeshProUGUI infoTextMesh, consolaTextMesh, centerScreenTextMesh, leftScreenTextMesh, rightScreenTextMesh;
    private List<UnityEngine.XR.InputDevice> gameControllers;
    private List<UnityEngine.XR.InputDevice> detectedDevices;

    float gripsensi, triggersensi;

    Vector3 velocity, angularVelocity, aceleracion, angularAceleracion, velocityG, angularVelocityG, aceleracionG, angularAceleracionG, posicion, posicionG;
    private Vector2 primary2DAxis;
    Quaternion rotacion, rotacionG;
    Vector3 leftEyePosition, rightEyePosition, centerEyePos;

    public Toggle speedHeadsetToggler, buttonToggler, joystickToggler;
    public TextMeshProUGUI speedHeadset, contButtons, joystick, tabletTextMesh;


    void Start() {
        detectedDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(detectedDevices);
        screenIsOn = false;
    }

    void Update() {
        isOnDevices();
        GetControllerValues();
        GetControllerTransforms();
        GetHeadsetValues();
        GetHeadsetTransforms();

        if (screenIsOn) {
            WriteOnScreens();
        } else {
            rightScreenTextMesh.text = "";
            leftScreenTextMesh.text = "";
        }
    }

    

    public void ShowDevices() {
        var texto = "";
        foreach (var device in detectedDevices) {
            texto += (string.Format("Device name: '{0}' and rol: '{1}. '", device.name, device.characteristics.ToString()));
        }
        infoTextMesh.text = texto;
    }

    public void ShowCharController() {
        var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();
        var device = detectedDevices[2]; 

        if (device.TryGetFeatureUsages(inputFeatures))
        {
            var texto = "";
            foreach (var feature in inputFeatures)
            {
                texto += (string.Format("Features: {0}. ", feature.name));
            }
            infoTextMesh.text = texto;
        }
    }
    
    public void TurnOnMonitor() {
        screenIsOn = !screenIsOn;
    }


    public void isOnDevices() {
        if (speedHeadsetToggler.isOn)
            speedHeadset.text = "Speed: " + velocityG;
        else
            speedHeadset.text = "";

        if (buttonToggler.isOn) {
            contButtons.text = "Finger on button A? " + primaryTouch + ". Is button pressed: " + primaryButton;
            contButtons.text += "Finger on button B? " + secondaryTouch + ". Is button pressed: " + secondaryButton;
        } else {
            contButtons.text = "";
        }

        if (joystickToggler.isOn)
            joystick.text = "finger on bumper: " + primary2DAxistouch + " joystick pressed: " + primary2DAxisclick + " joystick movement " + primary2DAxis;
        else
            joystick.text = "";

        tabletTextMesh.text = " controller placement:" + posicion + " rotation " + rotacion +  " controller speed " + velocity;
    }

    public void GetControllerValues() {
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primary2DAxis);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButton);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out primaryTouch);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryTouch, out secondaryTouch);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButton);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripButton);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out gripsensi);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggersensi);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisclick);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisTouch, out primary2DAxistouch);
    }

    public void GetControllerTransforms() {
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.isTracked, out isTracked);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out posicion);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out rotacion);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out velocity);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularVelocity, out angularVelocity);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAcceleration, out aceleracion);
        detectedDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularAcceleration, out angularAceleracion);
    }

    public void GetHeadsetValues() {
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.userPresence, out userPresence);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.leftEyePosition, out leftEyePosition);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.rightEyePosition, out rightEyePosition);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.centerEyePosition, out centerEyePos);
    }

    public void GetHeadsetTransforms() {
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.isTracked, out isTrackedG);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out posicionG);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out rotacionG);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out velocityG);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularVelocity, out angularVelocityG);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAcceleration, out aceleracionG);
        detectedDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularAcceleration, out angularAceleracionG);
    }

    public void WriteOnScreens() {
        rightScreenTextMesh.text = "Input right controller is moving bumper" + primary2DAxis;
        rightScreenTextMesh.text += "\n trigger pressed " + triggerValue + triggersensi;
        rightScreenTextMesh.text += "\n a is pressed " + primaryButton;
        rightScreenTextMesh.text += "\n b is pressed " + secondaryButton;
        rightScreenTextMesh.text += "\n finger over a " + primaryTouch;
        rightScreenTextMesh.text += "\n finger over b " + secondaryTouch;
        rightScreenTextMesh.text += "\n grip button " + gripButton + gripsensi;
        rightScreenTextMesh.text += "\n finger over bumper " + primary2DAxistouch;
        rightScreenTextMesh.text += "\n bumper button pressed " + primary2DAxisclick;
        rightScreenTextMesh.text += "\n Tracked device " + isTracked;
        rightScreenTextMesh.text += "\n position " + posicion + " rotation " + rotacion;
        rightScreenTextMesh.text += "\n speed " + velocity + "a. speed " + angularVelocity;
        rightScreenTextMesh.text += "\n acel " + aceleracion + "a. aceleration " + angularAceleracion;

        leftScreenTextMesh.text = "Inputs headset. headset is on " + userPresence;
        leftScreenTextMesh.text += "\n Dispositivo trackeado " + isTrackedG;
        leftScreenTextMesh.text += "\n Posiocion " + posicionG + " Rotacion " + rotacionG;
        leftScreenTextMesh.text += "\n speed " + velocityG + "a speed " + angularVelocityG;
        leftScreenTextMesh.text += "\n acel " + aceleracionG + "a acel " + angularAceleracionG;
    }
}
