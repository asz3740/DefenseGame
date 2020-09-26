using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    private TowerBlueprint TowerToBuild;
    private Tile selectedTile;

    public TowerUI towerUI;

    public bool CanBuild
    {
        get { return TowerToBuild != null; }
    }
    
    public bool HasMoney
    {
        get { return PlayerStats.Money >= TowerToBuild.cost; }
    }
    
    public void SelectTile(Tile tile)
    {
        if (selectedTile == tile)
        {
            DeselectTower();
            return;
        }
        selectedTile = tile;
        TowerToBuild = null;

        towerUI.SetTarget(tile);
    }

    public void DeselectTower()
    {
        selectedTile = null;
        towerUI.Hide();
    }
    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        TowerToBuild = tower;
        DeselectTower();
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return TowerToBuild;
    }
}
