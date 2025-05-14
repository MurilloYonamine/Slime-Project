using CAMERA;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private CameraZone[] cameras;

    private void Start() {
        CameraZone[] cams = GetComponentsInChildren<CameraZone>();
        cameras = cams;
    }
}
