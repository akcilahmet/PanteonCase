using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GridEditor gridEditor;
    public NpcMovement NpcMovement;
    
    
    
    private void Update()
    {
      if(Input.GetMouseButtonDown(0))
      {
        gridEditor.RayCastAndToggleWalkable();
      }
      if (Input.GetMouseButtonDown(1))
      {
         NpcMovement.RayCastAndSetDestination();
      }
    }
}
