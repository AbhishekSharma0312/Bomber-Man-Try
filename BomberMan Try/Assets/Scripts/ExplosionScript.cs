using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() 
    {
        StartCoroutine(selfOffObject());
    }

    IEnumerator selfOffObject()
    {
        yield return new WaitForSecondsRealtime(1f);
        this.gameObject.SetActive(false);
    }
}
