using UnityEngine;

public class AIController : MonoBehaviour    
{
    public int partyId;
    private Moveable movementScript;
    private Combatable  combatScript;
    private float lookTime = 1.0f;
    private float lastLook = 0.0f;


    private void Start()
    {
        movementScript = GetComponent<Moveable>();
        combatScript = GetComponent<Combatable>();
        combatScript.partyId = partyId;
    }


    void Update()
    {
        

        if(Time.time > lastLook + lookTime){
            Look();
        }
        if(combatScript.attackTarget){
            movementScript.ClearMoveList();

            //WE want the AI to stop moving once it is in range of the target
            if((combatScript.attackTarget.transform.position - transform.position).sqrMagnitude > (combatScript.equippedWeapon.range * combatScript.equippedWeapon.range)){
                movementScript.AddMoveLocation(combatScript.attackTarget.transform.position);
            }
        }
        // if(movementScript.moveLocation != null){
        //     if((movementScript.moveLocation - transform.position).sqrMagnitude >= GetAttackRange() ) {
        //         Vector3 moveVector = movementScript.moveLocation - transform.position;
        //         moveVector = moveVector.normalized;
        //         transform.position += moveVector * Time.deltaTime;
        //     }          
        // }
    }

    private void Look()
    {
        lastLook = Time.time;
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 10.0f);
        foreach (Collider2D col in objects)
        {
            if (col.GetComponent<Combatable>())
            {
                Combatable pawn = col.GetComponent<Combatable>();
                if (pawn.partyId != partyId)
                {
                    combatScript.attackTarget = pawn;
                    break;
                }
            }
        }
    }
}
