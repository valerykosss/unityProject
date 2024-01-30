using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMove : MonoBehaviour
{
    public GameObject player;
    public int speedRotation = 1;
    public int speed = 5;
    public bool walkstatus;
    public bool speedup;
    public AnimationClip walk;
    public AnimationClip run;
    public AnimationClip jump;
    public int jumpSpeed = 50;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = (GameObject)this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            walkstatus = true;
            anim.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedup = true;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedup = false;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            walkstatus = false;
            anim.enabled = false;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && walkstatus==true)
        {
            anim=GetComponent<Animator>();
            anim.speed = 1.0f;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.UpArrow) && walkstatus == true && speedup==true)
        {
            anim = GetComponent<Animator>();
            anim.speed = 2.0f;

        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.position -= player.transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.Rotate(Vector3.down * speedRotation);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.Rotate(Vector3.up * speedRotation);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.position += player.transform.up * jumpSpeed * Time.deltaTime;
        }
    }
}
