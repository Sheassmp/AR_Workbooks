using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOverlap : MonoBehaviour
{
    public static List<Collider> overlaps = new List<Collider>();

    private const string TAG = "Interactive";

    private void Awake()
    {
        foreach (Collider c in GetComponentsInChildren<Collider>()) {
            c.isTrigger = true;
            c.gameObject.tag = TAG;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TAG) {
            overlaps.Add(other);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag == TAG) {
            overlaps.Remove(other);
            
        }
    }
}
