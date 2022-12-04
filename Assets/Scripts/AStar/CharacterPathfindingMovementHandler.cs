

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
using Utils;

public class CharacterPathfindingMovementHandler : MonoBehaviour {

    private const float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    [Header("animator")] [SerializeField] private Animator animator;

    
    private void Update() {
        HandleMovement();
     

        /*if (Input.GetMouseButtonDown(0)) {
            SetTargetPosition(UtilsMethod.GetMouseWorldPosition());
        }*/
    }
    
    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
               
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                    AnimatorState("Run", false);

                    CharacterPathfindingMove.Instance.ClearCharacterPathfinding();

                }
            }
          

        } 
    }

    private void StopMoving() {
        pathVectorList = null;
    }

  

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        Debug.Log("target pos "+targetPosition);
        Debug.Log("target pos not "+transform.position);
        float dir = targetPosition.x - transform.position.x;
        Debug.Log("target pos dir "+dir);
        Vector3 soldierDir = animator.gameObject.transform.localScale;

        if (dir < 0 && soldierDir.x>0)
        {
            soldierDir.x *= -1;
            animator.gameObject.transform.localScale = soldierDir;
        }
        if (dir > 0 && soldierDir.x<0)
        {
            soldierDir.x *= -1;
            animator.gameObject.transform.localScale = soldierDir;
        }
       
        AnimatorState("Run", true);
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }


    void AnimatorState(String tempString, bool state)
    {
        animator.SetBool(tempString,state);
    }
}