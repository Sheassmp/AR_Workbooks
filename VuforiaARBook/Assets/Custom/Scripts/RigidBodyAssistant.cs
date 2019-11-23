using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RigidBodyAssistant : MonoBehaviour
{
    private bool wasKinematic;
    private Vector3 lastKnownVelocity;
    private Rigidbody rb;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Events.OnTrackingFound += OnTrackingFound;
        Events.OnTrackingLost += OnTrackingLost;    
    }

    private void OnDisable()
    {
        Events.OnTrackingFound -= OnTrackingFound;
        Events.OnTrackingLost -= OnTrackingLost;
    }
    
    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTrackingLost() {
        lastKnownVelocity = rb.velocity;
        wasKinematic = rb.isKinematic;

        rb.isKinematic = true;
    }

    private void OnTrackingFound() {
        rb.isKinematic = wasKinematic;
        rb.velocity = lastKnownVelocity;
    }
}

