using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductionMenuData", menuName = "ProductionMenuData")]
public class ProductionMenuSO : ScriptableObject
{
    public List<ProductionProperties> productionProperties;

}

[System.Serializable]
public class ProductionProperties
{
    public BuildingSO buildingSo;
}

