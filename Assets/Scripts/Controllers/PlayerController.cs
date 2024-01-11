using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float panSpeed = 20f; // Speed at which the camera moves
    public float panBorderThickness = 10f; // Thickness of screen border for panning
    public Vector2 panLimit; // Limits for how far the camera can move
    public bool mouseScroll = false;
    public int partyId; 
    public Camera camera;
    public LayerMask layerMask; // Layer to interact with

    
    public float zoomSpeed = 2.0f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    public List<PawnController> teamPawnControllers = new List<PawnController>();
    public List<PawnController> selectedPawnControllers = new List<PawnController>();

    void Start (){

    }

    //public Pawn[] pawns;
    void Update()
    {
        Vector3 pos =camera. transform.position;

        // Camera panning controls
        if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - panBorderThickness  && mouseScroll))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || (Input.mousePosition.y <= Screen.height && mouseScroll))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - panBorderThickness  && mouseScroll))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || (Input.mousePosition.x <= panBorderThickness  && mouseScroll))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        // Clamping camera position
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        camera.transform.position = pos;

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newSize = Camera.main.orthographicSize;
        newSize -= scrollInput * zoomSpeed;
        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        Camera.main.orthographicSize = newSize;


        foreach(PawnController teamMember in teamPawnControllers){
            teamMember.selected = false;
        }
        foreach(PawnController pc in selectedPawnControllers){
            pc.selected = true;
        }

        if (Input.GetMouseButtonDown(1))
        {

            // Convert the mouse position to world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Cast a ray from the mouse position into the scene
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the ray hits something in the scene
            if (hit.collider != null)
            {
                Combatable target = hit.collider.GetComponent<Combatable>();
                Debug.Log("CLICKED OBJRCT " + hit.collider.gameObject.name);
                Debug.Log("CLICKED OBJRCT " + hit.collider.gameObject.transform.position);

                if(target && target.partyId != partyId){
                    Debug.Log("ENEMY NAme " + hit.collider.gameObject.name);

                    foreach(PawnController pc in selectedPawnControllers){
                        Debug.Log("Setting at for " + pc.gameObject.name);
                        pc.combatScript.attackTarget = target;
                    }
                }

                // Set the GameObject's position to the hit point
                foreach(PawnController pc in selectedPawnControllers){
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
                        pc.movementScript.AddMoveLocation(hit.point);
                    }
                    else{
                        pc.movementScript.ClearMoveList();
                        pc.movementScript.AddMoveLocation(hit.point);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
           // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the ray hits something in the scene
            if (hit.collider != null)
            {
                // Check if the hit GameObject is selectable (you can customize this condition)
                PawnController pc = hit.collider.GetComponent<PawnController>();

                if (pc != null)
                {
                    if (selectedPawnControllers.Contains(pc))
                    {
                        if(selectedPawnControllers.Count > 1){
                            selectedPawnControllers.Clear();
                            selectedPawnControllers.Add(pc);
                        }
                        else{
                            selectedPawnControllers.Remove(pc);
                        }
                    }
                    else
                    {
                        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                        {
                            selectedPawnControllers.Clear();
                        }

                        selectedPawnControllers.Add(pc);
                    }
                }
            }
        }
    }


}
