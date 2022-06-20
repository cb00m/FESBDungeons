using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    //target of the camera
    public Transform lookAt;

    public float boundX = 0.3f;
    public float boundY = 0.15f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;    
    }

    // Called after Update and FixedUpdate
    private void LateUpdate()
    {
        if (GameManager.instance.player.isAlive())
        {
            Vector3 delta = Vector3.zero;

            float deltaX = lookAt.position.x - transform.position.x;

            //X axis bounds
            if (deltaX > boundX || deltaX < -boundX)
            {
                if (transform.position.x < lookAt.position.x)
                {
                    delta.x = deltaX - boundX;
                }
                else
                {
                    delta.x = deltaX + boundX;
                }
            }


            float deltaY = lookAt.position.y - transform.position.y;

            //Y axis bounds
            if (deltaY > boundY || deltaY < -boundY)
            {
                if (transform.position.y < lookAt.position.y)
                {
                    delta.y = deltaY - boundY;
                }
                else
                {
                    delta.y = deltaY + boundY;
                }
            }

            transform.position += new Vector3(delta.x, delta.y, 0);
        }
    }
}
