using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController2 : MonoBehaviour
{
    public Transform player;
    Animator anim;

    public float rotationSpeed = 2.0f;
    public float speed = 2.0f;
    public float visDist = 20.0f;
    public float visAngle = 30.0f;
    public float shootDist = 5.0f;

    string state = "IDLE";


    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            if (direction.magnitude > shootDist)
            {
                if (state != "RUNNING")
                {
                    state = "RUNNING";
                    anim.SetTrigger("isRunning");
                }
            }
            else
            {
                if (state != "SHOOTING")
                {
                    state = "SHOOTING";
                    anim.SetTrigger("isShooting");
                }
            }
        }
        else
        {
            if (state != "IDLE")
            {
                state = "IDLE";
                anim.SetTrigger("isIdle");
            }
        }

        if (state == "RUNNING")
        {
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }


    }


}
