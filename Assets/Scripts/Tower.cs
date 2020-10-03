using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타워 스크립트
public class Tower : MonoBehaviour
{
    
    [Header("일반적인 타워 속성")]
    public Transform Target; // 목표 타깃
    private Enemy TargetEnemy; // 
    [SerializeField] 
    private float range = 3f; // 공격 범위
    [SerializeField] 
    private string enemyTag = "Enemy"; // 적 태그
    [SerializeField] 
    private float fireRate = 1f; // 공격 속도
    private float fireCountdown = 0f; // 
    [SerializeField] 
    private GameObject BulletPrefab; // 총알 프리팹
    [SerializeField]
    private Transform FirePos; // 공격 위치
    [SerializeField]
    private float slowAmount= .5f; // 타깃 속도 저하
    
    [Header("레이저 타워 추가 속성")]
    [SerializeField]
    private bool useLaser; // 레이저 타워인지 체크
    [SerializeField]
    private bool useIce; 
    
    [SerializeField]
    private LineRenderer LineRenderer;
    [SerializeField]
    private int damageOverTime=30;
    
    
    
    void Start()
    {
        // UpdateTarget 메소드 0.5초마다 실행
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() 
    {
        // Enemy태그를 지정한 게임오브젝트들의 리스트를 반환해 배열에 저장
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); 
        
        // 가장 짧은 거리에 무한대로 할당
        float shortestDistance = Mathf.Infinity;
        
        // 가장 가까운 적에 null값 할당
        GameObject NearestEnemy = null;
        
        // enemies의 인덱스를 끝까지 순환
        foreach (GameObject enemy in enemies)
        { 
            // 타워의 위치와 적의 위치 사이 거리 측정
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            
            // 현재 가장 짧은 거리보다 더 짧은 거리가 측정되면 실행 
            if (distanceToEnemy < shortestDistance)
            {
                // 가장 짧은 거리 갱신
                shortestDistance = distanceToEnemy;
                
                // 가장 가까운 적 갱신
                NearestEnemy = enemy;
            }
        }
        
        // 가장 가까운 적이 존재하고 공격 범위 안에 적이 들어왔으면 실행
        if (NearestEnemy != null && shortestDistance <= range)
        {
            // 가장 가까운 적을 타깃으로 할당
            Target = NearestEnemy.transform;

            TargetEnemy = NearestEnemy.GetComponent<Enemy>();
        }
        // 위 조건을 만족하지 않으면 실행
        else
        {
            // null값 할당 
            Target = null;
        }
    }
  
    void Update()
    {
        // 타깃이 null이면 실행
        if (Target == null)
        {
            // 레이저타워이면 실행 
            if (useLaser)
            {
                // LineRenderer.enabled이 true이면 실행
                if (LineRenderer.enabled)
                    // false로 변경
                    LineRenderer.enabled = false;
            }

            return;
        }
            
        // useLaser이 true이면 실행
        if (useLaser)
        {
            // Laser 메소드 호출
            Laser();
        }
        // 위 조건을 만족하지 않으면 실행
        else
        {
            // fireCountdown이 0초 보다 작거나 같으면 실행
            if (fireCountdown <= 0f)
            {
                // Shoot 메소드 호출
                Shoot();
                
                // 
                fireCountdown = 1f / fireRate;
            }
            
            fireCountdown -= Time.deltaTime;
        }
        
        if(useIce)
            TargetEnemy.Slow(slowAmount);
    }

    // 레이저 타워 메소드
    void Laser()
    {
        //
        TargetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        if (!LineRenderer.enabled)
            LineRenderer.enabled = true;
        LineRenderer.SetPosition(0,FirePos.position);
        LineRenderer.SetPosition(1, Target.position);
        
    }

    // 공격 메소드
    private GameObject bullet;
    void Shoot()
    {
        switch (BulletPrefab.tag)
        {
            case "SpeedBullet" :
                SoundManager.Instance.Attack1();
                bullet = ObjectPool.SharedInstance.GetPooledObject("SpeedBullet");
                break;
            case "SpeedBullet_Upgraded" :
                SoundManager.Instance.Attack1();
                bullet = ObjectPool.SharedInstance.GetPooledObject("SpeedBullet_Upgraded") ;
                break;
            case "IceBullet" :
                SoundManager.Instance.Attack2();
                bullet = ObjectPool.SharedInstance.GetPooledObject("IceBullet");
                break;
            case "IceBullet_Upgraded":
                SoundManager.Instance.Attack2();
                bullet = ObjectPool.SharedInstance.GetPooledObject("IceBullet_Upgraded");
                break;
            case "ExplosionBullet" :
                print("1241421");
                SoundManager.Instance.Attack3();
                bullet = ObjectPool.SharedInstance.GetPooledObject("ExplosionBullet");
                break;
            case "ExplosionBullet_Upgraded" :
                SoundManager.Instance.Attack3();
                bullet = ObjectPool.SharedInstance.GetPooledObject("ExplosionBullet_Upgraded");
                break;
        }
        
        bullet.transform.position = FirePos.position;
        bullet.transform.rotation = FirePos.rotation;
        
        Bullet Bullet = bullet.GetComponent<Bullet>();
        if (Bullet != null)
        {
            // Seek 메소드 호출
            Bullet.Seek(Target);
            bullet.SetActive(true);
        }
        
    }

    
    //
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
    