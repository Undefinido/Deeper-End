using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeReference] ConstsScriptableObject consts;
    [SerializeReference] Animator animator;
    [SerializeReference] float RotationSpeed, movementSpeed;
    public bool RotatetowardsMouse = true;
    [SerializeReference] Camera camera;
    public float health;

    private void Start()
    {
        health = consts.MaxHealth;
    }


    Vector3 inputMovement { get; set; }
    void Update()
    {
        //check if has died
        if (health <= 0)
        {
            //animator.SetTrigger(consts.Die);
        }
        Vector3 movement = MoveTowardsTarget(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        if (!RotatetowardsMouse)
            RotateTowardsMovementVector(movement);
        else if (Physics.Raycast(camera.ScreenPointToRay(inputMovement = Input.mousePosition), out RaycastHit hitinfo, maxDistance: 300f))
        {
            transform.LookAt(new Vector3(hitinfo.point.x, transform.position.y, hitinfo.point.z));
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger(consts.Attack1Name);
                
                CombatSystem.ChooseAttack(consts.Attack1Type, consts.Damage, transform.position + (transform.forward * 1), this.transform.rotation, consts.AttackRange1, LayerMask.GetMask("Enemy"));
            }
                
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger(consts.Attack2Name);
                CombatSystem.ChooseAttack(consts.Attack2Type, consts.Damage, transform.position, this.transform.rotation, consts.AttackRange2, LayerMask.GetMask("Enemy"));
            }
                
        }
    }
    void RotateTowardsMovementVector(Vector3 movementVector) {
        if (movementVector.magnitude == 0) return;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movementVector), RotationSpeed);
    }
    Vector3 MoveTowardsTarget(Vector3 targetVector) {
        transform.position += (targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector) * (movementSpeed * Time.deltaTime);
        return targetVector;
    }
}
