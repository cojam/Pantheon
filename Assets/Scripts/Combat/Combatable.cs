using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Combatable : MonoBehaviour 
{
    public int partyId;
    public int perception;
    public int brawn;
    public int dex;
    public int hp = 100;

    public Weapon equippedWeapon;
    public Combatable attackTarget;

    public TextMeshProUGUI debugLog;
    public string weaponKey;
    public string weaponName;
    public Projectile projectilePrefab;

    public float lastAction = 0.0f;
    public float actionCoolDown = 1.0f;
    public ActionState actionState = ActionState.NORMAL;
    public float reach = 10.0f;
    public bool retaliate = true;
    public enum ActionState
    {
        NORMAL,
        ATTACKING
    }

    private void Start()
    {
        equippedWeapon = WeaponContainer.getWeapon(weaponKey);
        weaponName = equippedWeapon.name;

    }

    private void Update()
    {
        debugLog.text = actionState.ToString();


        if (attackTarget)
        {
            Debug.Log(gameObject.name + " has an attackTarget");
            if(actionState == Combatable.ActionState.NORMAL){
                if(equippedWeapon != null){
                    Debug.Log("Has a weapon");
                    Debug.Log("Distance to target: " + (attackTarget.transform.position - transform.position).sqrMagnitude);
                    Debug.Log("Weapon range: " + equippedWeapon.range);
                    if((attackTarget.transform.position - transform.position).sqrMagnitude < (equippedWeapon.range * equippedWeapon.range)){
                        if(Time.time > actionCoolDown + lastAction){
                            Debug.Log("Doing attack");

                            Attack attack = equippedWeapon.GetAttack();
                            StartCoroutine(DoAttack(attackTarget, attack));
                        }
                    }
                }
                else{
                    if((attackTarget.transform.position - transform.position).sqrMagnitude < reach){
                        if(Time.time > actionCoolDown + lastAction){
                            Attack attack = GetBaseAttack();
                            StartCoroutine(DoAttack(attackTarget, attack));
                        }
                    }
                }
            }
        }
    }

    public float GetAttackRange()
    {
        if (equippedWeapon != null)
        {
            return equippedWeapon.range;
        }
        return reach;
    }



    public void GetAttacked(int damage)
    {

        hp -= damage;
        if (hp < 0)
        {
            Destroy(this.gameObject);
        }


    }
    public void GetAttacked(int damage, Combatable attacker)
    {
        hp -= damage;
        if (hp < 0)
        {
            Destroy(this.gameObject);
        }
        if(retaliate && attackTarget == null){
            attackTarget = attacker;
        }
    }


    public IEnumerator DoAttack(Combatable target, Attack attack)
    {
        if (actionState == ActionState.NORMAL) {
            actionState = ActionState.ATTACKING;
            yield return new WaitForSeconds(attack.attackSpeed);

            lastAction = Time.time;
            int attackRoll = Random.Range(0,100);
            int damageRoll = Random.Range(1,brawn);
            if(attackRoll > 50){
                if(equippedWeapon != null){
                    damageRoll += Random.Range(1,equippedWeapon.baseDamage);

                   if( equippedWeapon.type == Weapon.WeaponType.RANGED){
                        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
                        projectile.endLocation = target.transform.position;
                    } 
                }
                Debug.Log( attackRoll + " attack for " + damageRoll + " damage");
                target.GetAttacked(damageRoll , this);
            }
            else{
                if( equippedWeapon.type == Weapon.WeaponType.RANGED){
                    Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
                    projectile.endLocation = target.transform.position;
                } 
            }

            actionState = ActionState.NORMAL;
        }
    }

    public Attack GetBaseAttack()
    {
        return new Attack(brawn, 1.0f, Attack.DamageType.BLUNT);
    }
}


// {

//     public int partyId;

//     public int perception;
//     public int brawn;
//     public int dex;


//     public Weapon equippedWeapon;


//     public Combatable attackTarget;
//     public Vector3 moveLocation;

//     public enum ActionState { 
//         NORMAL,
//         ATTACKING
//     }
//     public ActionState actionState;

//     public float lastLook = 0.0f;
//     public float lookTime = 1.0f;

//     public float lastAction = 0.0f;
//     public float actionCoolDown = 1.0f;
//     public float moveSpeed = 1.0f; 

//     public float reach = 10.0f;

//     public int hp = 100;
//     public TextMeshProUGUI debugLog;
//     public string weaponKey;
//     public string weaponName;
//     public Projectile projectilePrefab;



//     // Start is called before the first frame update
//     void Start()
//     {
//         actionState = ActionState.NORMAL; 
//         equippedWeapon = WeaponContainer.getWeapon(weaponKey);
//         weaponName =  equippedWeapon.name;
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//         debugLog.text = actionState.ToString();

//         if(Time.time > lastLook + lookTime){
//             look();
//         }
//         if(attackTarget){
//             moveLocation = attackTarget.transform.position;

//             if(equippedWeapon != null){
//                 if((attackTarget.transform.position - transform.position).sqrMagnitude < equippedWeapon.range){
//                     if(Time.time > actionCoolDown + lastAction){
//                         Attack attack = equippedWeapon.GetAttack();
//                         StartCoroutine(DoAttack(attackTarget, attack));
//                     }
//                 }
//             }
//             else{
//                 if((attackTarget.transform.position - transform.position).sqrMagnitude < reach){
//                     if(Time.time > actionCoolDown + lastAction){
//                         Attack attack = GetBaseAttack();
//                         StartCoroutine(DoAttack(attackTarget, attack));
//                     }
//                 }
//             }
//         }
//         if(moveLocation != null){
//             if((moveLocation - transform.position).sqrMagnitude >= GetAttackRange() ) {
//                 Vector3 moveVector = moveLocation - transform.position;
//                 moveVector = moveVector.normalized;
//                 transform.position += moveVector * Time.deltaTime;
//             }          
//         }
//     }

//     private float GetAttackRange(){
//         if(equippedWeapon != null){
//             return equippedWeapon.range;
//         }
//         return reach;
//     }

//     private void look(){
//         lastLook = Time.time;
//         Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 10.0f);
//         foreach(Collider2D col in objects){
//             if(col.GetComponent<Combatable>()){
//                 Combatable pawn = col.GetComponent<Combatable>();
//                 if(pawn.partyId != partyId){
//                     attackTarget = pawn;
//                     break;
//                 }
//             }
//         }
//     }

//     public void GetAttacked(int damage){
//         hp -= damage;
//         if(hp < 0){
//             Destroy(this.gameObject);
//         }
//     }


//     IEnumerator DoAttack(Combatable target, Attack attack)
//     {
//         if (actionState == ActionState.NORMAL) {
//             actionState = ActionState.ATTACKING;
//             yield return new WaitForSeconds(attack.attackSpeed);

//             lastAction = Time.time;
//             int attackRoll = Random.Range(0,100);
//             int damageRoll = Random.Range(1,brawn);
//             if(attackRoll > 50){
//                 if(equippedWeapon != null){
//                     damageRoll += Random.Range(1,equippedWeapon.baseDamage);

//                    if( equippedWeapon.type == Weapon.WeaponType.RANGED){
//                         Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
//                         projectile.endLocation = target.transform.position;
//                     } 
//                 }
//                 Debug.Log( attackRoll + " attack for " + damageRoll + " damage");
//                 target.GetAttacked(damageRoll);
//             }
//             else{
//                 if( equippedWeapon.type == Weapon.WeaponType.RANGED){
//                     Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
//                     projectile.endLocation = target.transform.position;
//                 } 
//             }

//             actionState = ActionState.NORMAL;
//         }
//     }
//     Attack GetBaseAttack () {
//         return new Attack(brawn,1.0f,Attack.DamageType.BLUNT);
//     }
// }
