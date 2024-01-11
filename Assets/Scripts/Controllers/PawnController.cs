using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{

    public int partyId;
    public Moveable movementScript;
    public Combatable combatScript;
    public PawnRenderer renderer;
    public GameObject selectedCircle; 
    public bool selected = false;
    public SpriteRenderer weaponRenderer;
    public LineRenderer weaponRangeRenderer;

    public float lookTime = 1.0f;
    public float lastLook = 0.0f;
    public float lookDistance = 10.0f;

    private void Start()
    {
        movementScript = GetComponent<Moveable>();
        combatScript = GetComponent<Combatable>();
        renderer = GetComponent<PawnRenderer>();
        combatScript.partyId = partyId;

        GameObject weaponRange = new GameObject("weaponRange");
        weaponRange.transform.SetParent(this.transform);
        weaponRangeRenderer = weaponRange.AddComponent<LineRenderer>();

        // Set LineRenderer properties
        weaponRangeRenderer.positionCount = 12;
        weaponRangeRenderer.startWidth = 0.1f;
        weaponRangeRenderer.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastLook + lookTime){
            Look();
        }
        DrawWeaponRange();

        selectedCircle.SetActive(selected);
        weaponRangeRenderer.gameObject.SetActive(selected);

        transform.position = new Vector3(transform.position.x,transform.position.y,-1.0f);
        

    }

    public void DrawWeaponRange (){
        if (weaponRangeRenderer != null && combatScript.equippedWeapon != null )
        {
            float radius = combatScript.equippedWeapon.range;
            int segments = weaponRangeRenderer.positionCount;
            float angleIncrement = 360f / segments;
            Vector3[] positions = new Vector3[segments];

            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleIncrement;
                float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle) + transform.position.x;
                float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle) + transform.position.y;
                positions[i] = new Vector3(x, y, -1.0f);
            }

            weaponRangeRenderer.SetPositions(positions);
        }
    }

    private void Look()
    {

        lastLook = Time.time;

        if(combatScript.equippedWeapon != null && combatScript.equippedWeapon.range > lookDistance){
            lookDistance = combatScript.equippedWeapon.range;
        }
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, lookDistance);
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
