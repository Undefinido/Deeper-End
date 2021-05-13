using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSelector : MonoBehaviour
{
    [SerializeReference] SceneManager sceneManager;
    [SerializeReference] int choice;
    private void Start() => gameObject.SetActive(sceneManager.unlocked >= choice);
    public void Select() => sceneManager.StartingFloor(choice);
}
