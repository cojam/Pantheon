using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public int queueLength = 0;
    public float moveSpeed = 5.0f;
    public Queue<Vector3> moveLocations = new Queue<Vector3>();

    public float closeness = 1.0f;
    private LineRenderer lineRenderer;   
    public Color lineColor = Color.red;

    public void AddMoveLocation(Vector3 newLocation)
    {
        moveLocations.Enqueue(new Vector3(newLocation.x,newLocation.y,transform.position.z));
    }
    public void ClearMoveList(){
        moveLocations.Clear();
    }

    private void Start(){
        lineRenderer = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        queueLength = moveLocations.Count;

        // Inside Update method
        if (moveLocations.Count > 0)
        {
            Vector3 nextLocation = moveLocations.Peek();

            // Calculate the direction vector to the next point
            Vector3 moveVector = nextLocation - transform.position;

            // Normalize the direction vector
            moveVector = moveVector.normalized;

            // Move the object towards the next point with a fixed speed
            transform.position += moveVector * moveSpeed * Time.deltaTime;

            // Check if the object has reached the next point
            if (Vector3.Distance(transform.position, nextLocation) < closeness)
            {
                // Remove the reached location from the queue
                moveLocations.Dequeue();

                if (moveLocations.Count == 0)
                {
                    // Clear the lineRenderer if no more locations in the queue
                    lineRenderer.positionCount = 0;
                }
            }
        }

        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        Vector3[] positions = new Vector3[moveLocations.Count + 1];

        positions[0] = transform.position; // Start point

        int index = 1;
        foreach (Vector3 location in moveLocations)
        {
            positions[index] = location;
            index++;
        }

        if (lineRenderer)
        {
            lineRenderer.materials[0].color = lineColor;
            lineRenderer.positionCount = positions.Length;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
            lineRenderer.SetPositions(positions);

            if (moveLocations.Count == 0)
            {
                // Clear the lineRenderer if no more locations in the queue
                lineRenderer.positionCount = 0;
            }
        }
    }
}