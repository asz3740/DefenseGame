using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Destroy());
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

}
