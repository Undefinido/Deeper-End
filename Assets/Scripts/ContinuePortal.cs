using UnityEngine;

public class ContinuePortal : Portal
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            sceneManager.NextFloor();
    }
}