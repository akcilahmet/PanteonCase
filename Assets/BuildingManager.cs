using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public BuildingSO buildingSo;
    public BuildingSO oldBuildingSO;
    public delegate void BuildGhost();
    public event BuildGhost ghostEvent;

    public List<ProduceSoldiers> BarrackProduceSoldiersList = new List<ProduceSoldiers>();
  
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
    public BuildingSO GetActiveOldBuildingSo()
    {
        return oldBuildingSO;
    }
    public BuildingSO SetGetActiveBuildingSo(BuildingSO buildingSo)
    {
        this.buildingSo= buildingSo;
        this.oldBuildingSO = buildingSo;
        return this.buildingSo;
    }

    public void ClearBuildingSO()
    {
        buildingSo = null;
    } 
    public void ClearOldBuildingSO()
    {
        oldBuildingSO = null;
    }
    
    public void DeselectGhostObj() {
        RefreshSelectedGhost();
    }
    private void RefreshSelectedGhost() {
        ghostEvent?.Invoke();
    }
    
    
    public int GetBarrackListCount()
    {
        return BarrackProduceSoldiersList.Count;
    }

    public void AddBarrackProduceSoldiersList(ProduceSoldiers produceSoldiers,BuildingSO buildingSo)
    {
        if (buildingSo.type == BuildingSO.Type.barrack)
        {
            BarrackProduceSoldiersList.Add(produceSoldiers);
        }
       
    } 
    public void RemoveBarrackProduceSoldiersList(ProduceSoldiers produceSoldiers)
    {
        BarrackProduceSoldiersList.Remove(produceSoldiers);
    }

    public List<ProduceSoldiers> GetBarrackProduceSoldiersList()
    {
        return BarrackProduceSoldiersList;
    }
}
