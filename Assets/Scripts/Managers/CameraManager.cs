using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace CAMERA
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        // [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        // private float shakeTimer;
        // private float startingIntensity;

        [SerializeField] private int currentVirtualCamera = 0;
        [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }
        // private void Update() {
        //     UpdateShakeTimer();
        // }

        // public void ShakeCamera(float intensity, float time) {
        //     CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
        //         cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //     cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        //     startingIntensity = intensity;
        //     shakeTimer = time;
        // }
        // private void UpdateShakeTimer() {
        //     shakeTimer -= Time.deltaTime;

        //     if (shakeTimer <= 0) {
        //         CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
        //             cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //         cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

        //         Mathf.Lerp(startingIntensity, 0f, shakeTimer);
        //     }
        // }
        public void ChangeCurrentCamera(bool playerHasPassed)
        {
            if (!playerHasPassed)
            {
                virtualCameras[currentVirtualCamera].Priority--;
                currentVirtualCamera++;
                virtualCameras[currentVirtualCamera].Priority++;
                return;
            }
            virtualCameras[currentVirtualCamera].Priority--;
            currentVirtualCamera--;
            virtualCameras[currentVirtualCamera].Priority++;
        }
    }
}