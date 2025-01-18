using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;
    void OnTriggerStay() { grounded = true; }

    void OnTriggerExit(Collider other) { grounded = false; }
}
