using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private GameObject obj;
    [SerializeField] private bool isFollowing;
    [SerializeField] private float followSpeed;
    [SerializeField] private float maxRange = 10;

    [SerializeField] private Camera cam;


    private void Start() {
        if (obj == null) {
            Debug.LogError("Game object not defined!", this);
            return;
        }
        ResetCameraPosition();
    }
    private void Update() {
        if (obj == null) return;
        if (cam == null) return;
        if (!isFollowing) return;
        LerpToPos();
    }


    private Vector3 GetMousePosInGame() {
        return cam.ScreenToWorldPoint(GetMousePosition());
    }

    private Vector2 GetMousePosition() {
        return Mouse.current.position.ReadValue();
    }

    private void OnDrawGizmos() {

        //TODO: Draw gizmos for camera location after camera lerping pos is fixed

        // Gizmos.DrawLine(obj.transform.position, ToFromVector(obj.transform.position.z));
        // Gizmos.color = Color.red;
        // Gizmos.DrawSphere(ToFromVector(obj.transform.position.z), .25f);
    }

    private void ResetCameraPosition() {
        Vector3 vec = new Vector3();
        vec.x = obj.transform.position.x;
        vec.y = obj.transform.position.y;
        vec.z = transform.position.z;
        transform.position = vec;
    }

    private void LerpToPos() {

        transform.position = Vector3.Lerp(transform.position, ToFromVector(transform.position.z), followSpeed * Time.fixedDeltaTime);
    }

    private Vector3 ToFromVector(float zPosition) {
        Vector3 vec = new Vector3();
        //TODO: Add camera lerping between object position and mouse position ex. 1/3 of length from obj to mouse
        //Vector3 mouse = GetMousePosInGame();
        vec.x = obj.transform.position.x;
        vec.y = obj.transform.position.y;
        vec.z = zPosition;

        return vec;
    }
}