using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    #region Serialize Variables
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] Vector2 deathKick = new Vector2(20.0f, 20.0f);
    #endregion

    #region Component Referances
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    #endregion

    #region Variables
    private float gravityScaleAtStart;
    private bool isAlive = true;
    #endregion

    #region Start
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }
    #endregion

    #region Update
    private void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    #endregion

    #region Character Move Player Input, Event System
    private void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    #endregion

    #region  Character Jump Player Input, Event System
    private void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        // when we hit the ground.
        // IsTouchingLayers -> Bu çarpıştırıcının belirtilen layerMask üzerindeki herhangi bir çarpıştırıcıya temas edip etmediği.
        // LayerMask.GetMask -> Verilen input değerine göre, layer int adresinin döner.
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            // do stuff

            myRigidbody.velocity += new Vector2(0.0f, jumpSpeed);
        }
    }
    #endregion

    #region Character Run Main Code
    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunnig", playerHasHorizontalSpeed);
    }
    #endregion

    #region  Character Flip Sprite Main Code
    private void FlipSprite()
    {
        // Mathf.Abs        -> Bir değerin, mutlak değerini döndürür.
        // Mathf.Epsilon    -> Bir değerin sıfırdan farklı alabileceği en küçük değer.
        // Mathf.Sign       -> bir değeri ki bu değer f olsun, f pozitif veya sıfır olduğunda 1, f negatif olduğunda -1'dir.

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1.0f);
        }
    }
    #endregion

    #region  Character Climbing Main Code
    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0.0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }
    #endregion

    #region Death Hero
    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
        }
    }
    #endregion
}
