using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator my_animator;
    GameObject mainCamera;
    CannonController cannon;
    Rigidbody cannonBall;
    Image crossHair;
    float rotateSpeed = 20;
   public float shootPower = 125;
    int mode = 0; // 0 for moving around, 1 for aiming cannon, 2 for shot follow mode
    void Start()
    {
        crossHair = GameObject.Find("CrossHair").GetComponent<Image>();
        my_animator = GetComponent<Animator>();
        mainCamera = GameObject.Find("Main Camera");
        cannon = null;
        cannonBall = null;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        my_animator.SetFloat("HAXIS", 0);
        my_animator.SetFloat("VAXIS", 0);

        if (mode == 0)
        {
            my_animator.SetFloat("HAXIS", h);
            my_animator.SetFloat("VAXIS", v);

            mainCamera.transform.position = transform.position - 5 * transform.forward + 2 * Vector3.up;
            mainCamera.transform.forward = transform.forward;
        }
        else if (mode == 1) //cannon mode
        {
            cannon.transform.Rotate(Vector3.up, h * rotateSpeed * Time.deltaTime, Space.World);
            cannon.transform.Rotate(cannon.transform.right, -v * rotateSpeed * Time.deltaTime, Space.World);

            mainCamera.transform.position = cannon.spawnPoint.transform.position;
            mainCamera.transform.forward = cannon.spawnPoint.transform.forward;
            if (Input.GetButtonDown("Fire1"))
            {
              cannonBall = cannon.shoot(shootPower);
                mode = -1;
                Invoke("setShotFollowMode", 0.25f);
            }
            if (Input.GetButtonDown("Fire2"))
            {
                setMovingMode();
            }
        }
        else if (mode == 2) //SHOT follow mode
        {
            //once complete go back to aim mode
            if (cannonBall != null)
            {
                mainCamera.transform.position = cannonBall.transform.position - cannonBall.velocity * 0.25f;
                mainCamera.transform.LookAt(cannonBall.transform.position);
            }
            else
            { // once complete go back to aim mode
                mode = -1;
                Invoke("setAimingMode", 2);
             
            }

        }

        else if (mode == 3) 
        {
            //this is null mode that does nothing
        }

    }



    void OnTriggerStay(Collider other)
    {
        if (other.tag == "FriendlyCannon" && mode == 0 && Input.GetButton("Jump"))
        {
            cannon = other.transform.Find("Cannon").GetComponent<CannonController>();
            setAimingMode();
        }
    }

    void setMovingMode()
    {
        crossHair.enabled = false;
        mode = 0;
    }

    void setAimingMode()
    {
        crossHair.enabled = true;
        mode = 1;
    }

    void setShotFollowMode()
    {
        crossHair.enabled = false;
        mode = 2;
    }
}