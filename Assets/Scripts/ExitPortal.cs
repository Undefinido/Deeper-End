using UnityEngine;

public class ExitPortal : Portal
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            sceneManager.Return();
            
    }
}