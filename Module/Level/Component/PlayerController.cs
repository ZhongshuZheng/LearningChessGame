using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// control the player
/// </summary>
public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10f;
        GameApp.CameraManager.SetPosition(transform.position);  // set the camera on player
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();        
    }

    public void Move() {
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0f) {
            animator.Play("idle");
        } else {
            // face up
            float faceto = transform.localScale.x;
            if (faceto * h < 0f) {
                Flip();
            }

            // animation
            animator.Play("move");

            // edge check and move
            Vector3 pos = transform.position + Vector3.right * h * moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -32f, 24f);

            transform.position = pos;

            // camera
            GameApp.CameraManager.SetPosition(transform.position);
        }
    }

    public void Flip() {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
