using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private GameObject obj;
    public bool isFollowing;
    [SerializeField] private float followSpeed;

    private Rigidbody2D rb;

    private void Start() {
        if (obj == null) {
            Debug.LogError("Game object not defined!", this);
            return;
        }
        rb = obj.GetComponent<Rigidbody2D>();
    }
    private void LateUpdate() {
        if (obj == null) return;
        if (!isFollowing) return;

        Vector3 vec = new Vector3();
        vec.x = rb.transform.position.x;
        vec.y = rb.transform.position.y;
        vec.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, vec, followSpeed * Time.fixedDeltaTime);
    }
}
