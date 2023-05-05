using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHearts : MonoBehaviour
{
    public GameObject heartPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Drop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Drop() {
        Instantiate(heartPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);

    }
}
