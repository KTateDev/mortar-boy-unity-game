using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public Rigidbody shoot(float power)
    {
        GameObject g = Instantiate(cannonBallPrefab, spawnPoint.transform.position, transform.rotation) as GameObject;
        Rigidbody body = g.GetComponent<Rigidbody>();
        body.AddForce(transform.forward.normalized * power, ForceMode.Impulse);
        return body;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
