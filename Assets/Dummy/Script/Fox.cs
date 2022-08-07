using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dummy
{
    public class Fox : MonoBehaviour
    {
        private SpriteRenderer renderer;
        private Rigidbody2D body;
        private Animator animator;

        private float speed = 1.0f;
        private float runModifier = 1.75f;
        private float horValue;

        private bool isRunning = false;
        private bool facingRight = true;

        const float groundRadius = 0.2f;
        private bool isGrounded = false;

        private bool jump = false;
        private bool coyoteJump = false;
        private bool multipleJump = false;
        [SerializeField]
        private float jumpPower = 6.0f;
        [SerializeField]
        private int totalJump;
        private int availableJump;

        private LayerMask groundLayer;
        private Transform groundChecker;

        // Start is called before the first frame update
        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();

            groundLayer = LayerMask.GetMask("Ground");
            groundChecker = GameObject.Find("groundChecker").transform;

            availableJump = totalJump;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                facingRight = true;
                horValue = 1.0f;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                facingRight = false;
                horValue = -1.0f;
            }
            else horValue = 0.0f;

            if (Input.GetKey(KeyCode.LeftShift)) isRunning = true;
            else isRunning = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else jump = false;

            animator.SetFloat("yVelocity", body.velocity.y);
        }

        private void FixedUpdate()
        {
            GroundCheck();
            Move(horValue);

            if (facingRight) renderer.flipX = false;
            else renderer.flipX = true;
        }

        private void GroundCheck()
        {
            var wasGrouned = isGrounded;
            isGrounded = false;

            var colliders = Physics2D.OverlapCircleAll(groundChecker.position, groundRadius, groundLayer);

            if (colliders.Length > 0)
            {
                isGrounded = true;

                if (!wasGrouned)
                {
                    availableJump = totalJump;
                    multipleJump = false;
                }
            }
            else if(wasGrouned)
            {
                StartCoroutine(CoyoteJumpDelay());
            }

            animator.SetBool("Jump", !isGrounded);
        }

        IEnumerator CoyoteJumpDelay()
        {
            coyoteJump = true;
            yield return new WaitForSeconds(0.5f);
            coyoteJump = false;
        }

        private void Jump()
        {
            if (isGrounded)
            {
                multipleJump = true;
                availableJump--;

                body.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
            else if (coyoteJump)
            {
                multipleJump = true;
                availableJump--;

                body.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
            else if (multipleJump && availableJump > 0)
            {
                availableJump--;

                body.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }

        private void Move(float dir)
        {
            var xVal = dir * speed * Time.deltaTime * 100;
            if (isRunning) xVal *= runModifier;

            var targetVel = new Vector2(xVal, body.velocity.y);
            body.velocity = targetVel;

            animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        }


    }
}
