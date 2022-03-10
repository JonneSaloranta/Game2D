using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayDebugInfo : MonoBehaviour {

    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private TextMeshProUGUI playerLocationText;
    [SerializeField] private TextMeshProUGUI directionText;
    [SerializeField] private TextMeshProUGUI mouseScreenPosText;
    [SerializeField] private TextMeshProUGUI mouseGamePosText;
    [SerializeField] private TextMeshProUGUI isPlayerOnGroundText;

    [Header("Gameobject")]
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;

    private PlayerControls playerControls;


    private float pollingTime = .25f;
    private float time;
    private int frameCount;

    private bool showDebugInfo;


    private void Awake() {
        playerControls = new PlayerControls();
        playerControls.UI.ToggleDebug.performed += ctx => ToggleDebugInfo();
    }
    private void Start() {
        cam = Camera.main;

        HideDebug();

    }


    void Update() {
        if (showDebugInfo) {
            CurrentFps();
            MousePosOnScreen();
            MousePosInGame();
            PlayerLocation();
            UpdateDirection();
            PlayerOnGround();
        }
    }

    private void ToggleDebugInfo() {
        if (!showDebugInfo) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
        } else {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
        }
        showDebugInfo = !showDebugInfo;
    }

    private void HideDebug() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        showDebugInfo = false;
    }

    private void CurrentFps() {
        if (fpsText == null) return;
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime) {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate.ToString() + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }

    private void PlayerLocation() {
        if (playerLocationText == null) return;
        if (player == null) return;
        playerLocationText.text = Round(player.transform.position[0], 2).ToString() + ", " + Round(player.transform.position[1], 2).ToString() + " XYPL";

    }

    private void MousePosOnScreen() {
        if (mouseScreenPosText == null) return;
        Vector2 pos = GetMousePosition();
        mouseScreenPosText.text = Round(pos[0], 2).ToString() + ", " + Round(pos[1], 2).ToString() + " XYMSL";
    }

    private void MousePosInGame() {
        if (mouseGamePosText == null) return;
        if (cam == null) return;

        Vector3 pos = cam.ScreenToWorldPoint(GetMousePosition());
        mouseGamePosText.text = Round(pos[0], 2).ToString() + ", " + Round(pos[1], 2).ToString() + " XYMGL";

    }

    private void UpdateDirection() {
        Vector2 vel = player.GetComponent<Rigidbody2D>().velocity.normalized;
        directionText.text = Round(vel.x, 2) + ", " + Round(vel.y, 2) + " PMD";
    }

    private Vector2 GetMousePosition() {
        return Mouse.current.position.ReadValue();
    }

    private float Round(float number, int decimals) {
        float powof = Mathf.Pow(10f, decimals);
        return Mathf.Round(number * powof) / powof;
    }

    private void PlayerOnGround() {
        if (isPlayerOnGroundText == null) return;
        if (player == null) return;
        isPlayerOnGroundText.text = player.GetComponent<Controls>().IsGrounded().ToString() + " :IOG";
    }

    private void OnEnable() {
        playerControls.UI.Enable();
    }

    private void OnDisable() {
        playerControls.UI.Disable();
    }
}
