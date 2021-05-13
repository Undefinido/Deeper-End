using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem
{
    public static void ChooseAttack(int attackType, float damage, Vector3 attackPoint, Quaternion rotation, float attackRange, LayerMask enemyLayers) {
        switch (attackType) {
            case 0:
                SlashAttack(damage, attackPoint, attackRange, enemyLayers);
                break;
            case 1:
                SpinAttack(damage, attackPoint, attackRange, enemyLayers);
                break;
            case 2:
                ConeAttack(damage, attackPoint, rotation, attackRange, enemyLayers);
                break;
            case 3:
                AOEAttack(damage, attackPoint, attackRange, enemyLayers);
                break;
            case 4:
                BulletAttack(damage, attackPoint, rotation, attackRange, enemyLayers);
                break;
            case 5:
                SpreadAttack(damage, attackPoint, rotation, attackRange, enemyLayers);
                break;
        }
    }


    // Meele attacks
    public static void SlashAttack(float damage, Vector3 attackPoint, float attackRange, LayerMask enemyLayers)
    {
        // Play an attack animation, might need the reference of the animator as parameter if we want it to be interactable with enemies
        // animator.SetTrigger("Slash");

        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapBox(attackPoint, new Vector3(attackRange, attackRange, attackRange), Quaternion.identity, enemyLayers); //OverlapSphere(attackPoint.position, attackRange, el);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            if(LayerMask.LayerToName(enemy.gameObject.layer) == "Player")
            {
                enemy.gameObject.GetComponent<PlayerController>().health -= damage;
            }
            else if (LayerMask.LayerToName(enemy.gameObject.layer) == "Enemy")
            {
                enemy.gameObject.GetComponent<enemyIA>().Damage(damage);
            }
        }
    }

    public static void SpinAttack(float damage, Vector3 attackPoint, float attackRange, LayerMask enemyLayers)
    {
        //same using custom hitbox (capsule? cilinder?), omit self object

        // Play an attack animation, might need the reference of the animator as parameter if we want it to be interactable with enemies
        // animator.SetTrigger("Spin");

        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint, attackRange, enemyLayers); //OverlapSphere(attackPoint.position, attackRange, el);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            if (enemy.tag == "Player")
            {
                enemy.gameObject.GetComponent<PlayerController>().health -= damage;
            }
            else if (enemy.tag == "Enemy")
            {
                enemy.gameObject.GetComponent<enemyIA>().Damage(damage);
            }
        }
    }

    // Mage Attacks
    // Uses a box instead of a cone-like form
    public static void ConeAttack(float damage, Vector3 attackPoint, Quaternion rotation, float attackRange, LayerMask enemyLayers)
    {
        // Play an attack animation, might need the reference of the animator as parameter if we want it to be interactable with enemies
        // animator.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapBox(attackPoint, new Vector3(attackRange, 1.0f, 1.0f), rotation, enemyLayers); //OverlapSphere(attackPoint.position, attackRange, el);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            if (enemy.tag == "Player")
            {
                enemy.gameObject.GetComponent<PlayerController>().health -= damage;
            }
            else  if(enemy.tag == "Enemy")
            {
                enemy.gameObject.GetComponent<enemyIA>().Damage(damage);
            }
        }
    }

    public static void AOEAttack(float damage, Vector3 attackPoint, float attackRange, LayerMask enemyLayers)
    {
        // must think about it, not defined yet actually
    }

    // Ranger Attacks
    public static void BulletAttack(float damage, Vector3 attackPoint, Quaternion rotation, float attackRange, LayerMask enemyLayers)
    {
        // create projectile, shoot it, the projectile on its own (should) has a collider which will deal the dmg
    }

    public static void SpreadAttack(float damage, Vector3 attackPoint, Quaternion rotation, float attackRange, LayerMask enemyLayers)
    {
        // Play an attack animation, might need the reference of the animator as parameter if we want it to be interactable with enemies
        // animator.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapBox(attackPoint, new Vector3(1.0f, 1.0f, attackRange), rotation, enemyLayers); //OverlapSphere(attackPoint.position, attackRange, el);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            if (enemy.tag == "Player")
            {
                enemy.gameObject.GetComponent<PlayerController>().health -= damage;
            }
            else if(enemy.tag == "Enemy")
            {
                enemy.gameObject.GetComponent<enemyIA>().Damage(damage);
            }
        }
    }
}
