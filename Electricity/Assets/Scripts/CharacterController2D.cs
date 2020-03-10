using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float jumpForce = 350f;
    public Transform groundCheck;
    const float groundCheckRadius=0.1f;
    private bool _isGrounded;
    private bool facingRight=true;
    const float groundCheckBreak = 0.1f;
    private float nextGroundCheckTime;
    private Rigidbody2D rigidBody2D;
    public bool isGrounded { get; private set; }
    public AudioClip jumpAudio;
    private AudioSource audioSource;
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        bool wasGrounded = _isGrounded;
        _isGrounded = false;
        if (Time.time > nextGroundCheckTime)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    _isGrounded = true;
                }
            }
        }
    }
    private void Update()
    {
        isGrounded = _isGrounded;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }
    public void Move(float move,bool jump)
    {
            rigidBody2D.velocity = new Vector2(move, rigidBody2D.velocity.y);
            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        if (_isGrounded && jump)
        {
            audioSource.PlayOneShot(jumpAudio,1);
            _isGrounded = false;
            rigidBody2D.AddForce(new Vector2(0f, jumpForce));
            nextGroundCheckTime = Time.time + groundCheckBreak;
        }
    }
}
