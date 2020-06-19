using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint speedTower;
    public TowerBlueprint iceTower;
    public TowerBlueprint explosionTower;
    public TowerBlueprint LaserTower;

    private BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectSpeedTower()
    { 
        buildManager.SelectTowerToBuild(speedTower);
    }

    public void SelectIceTower()
    {
        buildManager.SelectTowerToBuild(iceTower);
    }
    
    public void SelectExplosionTower()
    {
        buildManager.SelectTowerToBuild(explosionTower);
    }
    
    public void SelectLaserTower()
    {
        buildManager.SelectTowerToBuild(LaserTower);
    }
    
}
