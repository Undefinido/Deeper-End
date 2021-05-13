using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeReference] GameObject UI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            UI.active = true;
    }
}
