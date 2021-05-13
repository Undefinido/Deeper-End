using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CombatSystem.SlashAttack(5, attackPoint.position, attackRange, enemyLayers);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        //Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawCube(attackPoint.position, new Vector3(attackRange, attackRange, attackRange));
    }
}
