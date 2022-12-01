using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public BuildingSO buildingSo;
    public delegate void BuildGhost();
    public event BuildGhost ghostEvent;

  
    #region Singleton

    public static BuildingManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("EXTRA : " + this + "  SCRIPT DETECTED RELATED GAME OBJ DESTROYED");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
      
        
    }

    #endregion


    public BuildingSO GetActiveBuildingSo()
    {
        return buildingSo;
    }
    
    
    public void DeselectGhostObj() {
        RefreshSelectedGhost();
    }
    private void RefreshSelectedGhost() {
        ghostEvent?.Invoke();
    }
}
