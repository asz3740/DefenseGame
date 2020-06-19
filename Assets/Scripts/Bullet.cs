using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public int damage;
    public float explosionRadius = 0f;

    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            gameObject.SetActive ( false ); // false
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
            
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private GameObject effectIns;
    void HitTarget()
    {
        if (impactEffect.CompareTag("StandardEffect"))
        {
             effectIns = ObjectPool.SharedInstance.GetPooledObject("StandardEffect") ;
        }
        else
        {
            effectIns = ObjectPool.SharedInstance.GetPooledObject("ExplosionEffect") ;
        }
       
        effectIns.transform.position = transform.position;
        effectIns.transform.rotation = transform.rotation;
        effectIns.gameObject.SetActive(true);


        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        gameObject.SetActive (false);
        
    } 
    
    IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(0.5f);
        effectIns.gameObject.SetActive(false);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if(e != null)
        {
            e.TakeDamage(damage);
        }
        gameObject.SetActive ( false ); // false

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
