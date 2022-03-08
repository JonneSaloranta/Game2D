using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent onInteract;

    private GameObject player;

    public void InteracAction() {
        onInteract.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) return;

        player = other.gameObject;
    }
}
