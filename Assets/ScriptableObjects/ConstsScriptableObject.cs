using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ConstsScriptableObject", order = 1)]
public class ConstsScriptableObject : ScriptableObject
{
    // Enemy IA
    public float minRoamDistance = 2f;
    public float maxRoamDistance = 7f;
    public float lookRadius = 10f;
    public float StoppingTargetDistance = 2f;

    
    public string Attack1Name;
    public int Attack1Type;
    public float AttackRange1 = 0.5f;
    public string Attack2Name;
    public int Attack2Type;
    public float AttackRange2 = 0.5f;

    public float Damage;
    public float MaxHealth;
}