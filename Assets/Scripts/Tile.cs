using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public GameObject tower;
    public TowerBlueprint towerBlueprint;
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    private BuildManager buildManager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }
    void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if (tower != null)
        {
            buildManager.SelectTile(this);
            return;
        }
        
        if (!buildManager.CanBuild)
            return;

        BuildTower(buildManager.GetTowerToBuild());
    }

    void BuildTower(TowerBlueprint buleprint)
    {
        if (PlayerStats.Money < buleprint.cost)
            return;
        PlayerStats.Money -= buleprint.cost;
        GameObject _tower = (GameObject)Instantiate(buleprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        towerBlueprint = buleprint;
    }
    
    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerBlueprint.upgradeCost)
            return;

        PlayerStats.Money -= towerBlueprint.upgradeCost;
        
        Destroy(tower);
        GameObject _tower = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;
        isUpgraded = true;
    }

    void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
