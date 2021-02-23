using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionController : MonoBehaviour
{
    GameObject friendCan;
    GameObject enemyCan;
    Collider cannonCollider;
    GameObject mortarBoy;
    Text endGameText;
    Image crossHair;

    public List<GameObject> enemyCannons = new List<GameObject>();
    public List<GameObject> friendlyCannons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        mortarBoy = GameObject.Find("UnityDude").GetComponent<GameObject>();
        endGameText = GameObject.Find("endGameText").GetComponent<Text>();
        crossHair = GameObject.Find("CrossHair").GetComponent<Image>();

        // transform.position.range
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            mortarBoy = player;
            float playerDist = Vector3.Distance(transform.position, player.transform.position);
            if (playerDist < 10)
            {
                endGameText.text = "You Lose";
                Destroy(player);
            }
        }


        foreach (GameObject cannon in GameObject.FindGameObjectsWithTag("FriendlyCannon"))
        {
            friendlyCannons.Add(cannon);
            float dist = Vector3.Distance(transform.position, cannon.transform.position);
            if (dist < 10)
            {
                Destroy(cannon);
                friendlyCannons.Remove(cannon);
                crossHair.enabled = false;
            }
        }
        if (friendlyCannons.Count == 0)
        {
            endGameText.text = "You Lose";
            Destroy(mortarBoy);    
        }



        foreach (GameObject ecannon in GameObject.FindGameObjectsWithTag("EnemyCannon"))
        {
            enemyCannons.Add(ecannon);
            float dist = Vector3.Distance(transform.position, ecannon.transform.position);
            if (dist < 10)
            {
                Destroy(ecannon);
                enemyCannons.Remove(ecannon);
            }
        }

        if (enemyCannons.Count == 0)
        {
            endGameText.text = "You Win";
            Destroy(mortarBoy);
        }
    }
 
}
