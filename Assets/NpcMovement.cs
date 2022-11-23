using System;
using System.Collections;
using System.Collections.Generic;
using GameAI.PathFinding;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public GridEditor GridEditor;
    public Transform Destination;
    public float speed = 1f;
    public Queue<Vector2> wayPoints = new Queue<Vector2>();

    private PathFinder<Vector2Int> mPathFinder = new AStarPathFinder<Vector2Int>();
    private void Start()
    {
        mPathFinder.onSuccess = OnSuccessPathFinding;
        mPathFinder.onFailure = OnFailurePathFinding;
        mPathFinder.HeuristicCost = GridEditor.GetManhattanCost;
        mPathFinder.NodeTraversalCost = GridEditor.GetEuclideanCost;
        StartCoroutine(CoroutineMoveTo());
    }

    public void AddWayPoint(Vector2 point)
    {
        wayPoints.Enqueue(point);
    }

    public void SetDestination(
        GridEditor map, 
        GridCell destination)
    {
       
        if (mPathFinder.Status == PathFinderStatus.RUNNING)
        {
            Debug.Log("Pathfinder already running. Cannot set destination now");
            return;
        }
        // remove all waypoints from the queue.
        wayPoints.Clear();
        // new start location is previous destination.
        GridCell start = map.GetRectGridCell(
            (int)transform.position.x, 
            (int)transform.position.y);
        if (start == null) return;
        mPathFinder.Initialize(start, destination);
        StartCoroutine(Coroutine_FindPathSteps());
    }

    IEnumerator CoroutineMoveTo()
    {
        while (true)
        {
            while (wayPoints.Count > 0)
            {
                yield return StartCoroutine(CoroutineMoveToPoint(wayPoints.Dequeue(), speed));
            }

            yield return null;


        }
    }

    IEnumerator CoruTineMoveOverSeconds(GameObject objectToMove,Vector3 end,float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
    }

    IEnumerator CoroutineMoveToPoint(Vector2 p, float speed)
    {
        Vector3 endP = new Vector3(p.x, p.y, transform.position.z);
        float duration = (transform.position - endP).magnitude / speed;
        yield return StartCoroutine(CoruTineMoveOverSeconds(transform.gameObject, endP, duration));
    }
    
    IEnumerator Coroutine_FindPathSteps()
    {
        while(mPathFinder.Status == PathFinderStatus.RUNNING)
        {
            mPathFinder.Step();
            yield return null;
        }
    }
    
    
    
    void OnSuccessPathFinding()
    {
        PathFinder<Vector2Int>.PathFinderNode node = mPathFinder.CurrentNode;
        List<Vector2Int> reverse_indices = new List<Vector2Int>();
        while(node != null)
        {
            reverse_indices.Add(node.Location.Value);
            node = node.Parent;
        }
        for(int i = reverse_indices.Count -1; i >= 0; i--)
        {
            AddWayPoint(new Vector2(reverse_indices[i].x, reverse_indices[i].y));
        }
    }

    void OnFailurePathFinding()
    {
        Debug.Log("Error: Cannot find path");
    }

    public void RayCastAndSetDestination()//ınput manager da cagır
    {
        Vector2 rayPos = new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        if (hit)
        {
            GameObject obj = hit.transform.gameObject;
            GridProperties sc = obj.GetComponent<GridProperties>();
            if (sc == null) return;

            Vector3 pos = Destination.position;
            pos.x = sc.GridCell.Value.x;
            pos.y = sc.GridCell.Value.y;
            Destination.position = pos;

            // Set the destination to the NPC.
            SetDestination(GridEditor, sc.GridCell);
        }
    }
    
    
}
