using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    CannonController cannon;
    GameObject mortarBoy;
    Transform mortarBoyPos;
    float shootTime;
    float shootPower;
    float startTime;
    
    // Start is called before the first frame update
    void Start()
    {
      shootTime =  Random.Range(3, 15);
        shootPower = Random.Range(45, 60);
        startTime = Random.Range(3, 6);
        cannon = GetComponent<CannonController>();
        mortarBoy = GameObject.Find("UnityDude").GetComponent<GameObject>();
        mortarBoyPos = GameObject.Find("UnityDude").GetComponent<Transform>();

        InvokeRepeating("shootMortarBoy", 10, shootTime);
    }



    void shootMortarBoy()
    {
     transform.LookAt(mortarBoyPos.position);

        Vector3 temp = transform.forward;
        // temp.x = -38;
        temp.x = Random.Range(-34, -65);    
        cannon.transform.Rotate(temp.x, 0, 0);
        cannon.shoot(shootPower);
    }

}
