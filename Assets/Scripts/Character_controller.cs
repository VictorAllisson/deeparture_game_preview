using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_controller : MonoBehaviour
{
    Animator anim;
    private TimeController timeController;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        timeController = FindObjectOfType(typeof(TimeController)) as TimeController;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //{
        //    float move = Input.GetAxis("Horizontal");
        //    anim.SetFloat("Speed", move);
        //}
    }

    void Movement()
    {
        if (!timeController.isReversing)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * 3f * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 0);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.right * 3f * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, -100);
            }
        }
    }
}
