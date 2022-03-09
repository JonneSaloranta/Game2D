using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayDebugInfo : MonoBehaviour {

    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private TextMeshProUGUI xLocationText;
    [SerializeField] private TextMeshProUGUI yLocationText;
    [SerializeField] private TextMeshProUGUI directionText;
    [SerializeField] private TextMeshProUGUI mouseXPositionText;
    [SerializeField] private TextMeshProUGUI mouseYPositionText;

    [Header("Gameobject")]
    private GameObject player;
    private Camera cam;


    private float pollingTime = .25f;
    private float time;
    private int frameCount;

    private void Start() {
        cam = Camera.main;
    }


    void Update() {
        UpdateFps();
        UpdateMouse();

    }

    private void UpdateFps() {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime) {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate.ToString() + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }

    private void UpdateLocation() { }

    private void UpdateMouse() {
        if (mouseXPositionText == null || mouseYPositionText == null) return;
        Vector2 pos = Mouse.current.position.ReadValue();
        mouseXPositionText.text = pos[0].ToString();
        mouseYPositionText.text = pos[1].ToString();
    }
    private void UpdateDirection() { }
}
