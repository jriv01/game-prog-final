using UnityEngine;
using System.Collections;
 
 public class CircleSpawn : MonoBehaviour {
 
    int numObjects = 8;
    public GameObject enemyPrefab;
    void Start()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            int a = i * 30;
            Vector3 pos = RandomCircle(center, 8.0f , a);
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
    Vector3 RandomCircle(Vector3 center, float radius,int a)
    {
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
 
    
 }