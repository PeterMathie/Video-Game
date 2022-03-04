using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    private string currentState;

    public bool isIdle;
    
    public bool isJumping;

    public bool isRunning;

    public bool isForward;
    public bool isBackward;
    public bool isLeft;
    public bool isRight;

    const string IDLE               = "BasicMotions@Idle01";

    const string JUMP               = "BasicMotions@Jump01";


    const string RUNFORWARD         = "BasicMotions@Run01 - Forwards";
    const string RUNFORWARDLEFT     = "BasicMotions@Run01 - ForwardsLeft";
    const string RUNFORWARDRIGHT    = "BasicMotions@Run01 - ForwardsRight";

    const string RUNBACKWARD        = "BasicMotions@Run01 - Backwards";
    const string RUNBACKWARDLEFT    = "BasicMotions@Run01 - BackwardsLeft";
    const string RUNBACKWARDRIGHT   = "BasicMotions@Run01 - BackwardsRight";

    const string RUNLEFT            = "BasicMotions@Run01 - Left";
    const string RUNRIGHT           = "BasicMotions@Run01 - Right";
    

    const string FORWARD            = "BasicMotions@Walk01 - Forwards";
    const string FORWARDLEFT        = "BasicMotions@Walk01 - ForwardsLeft";
    const string FORWARDRIGHT       = "BasicMotions@Walk01 - ForwardsRight";

    const string BACKWARD           = "BasicMotions@Walk01 - Forwards";//"BasicMotions@Walk01 - Backwards";
    const string BACKWARDLEFT       = "BasicMotions@Walk01 - BackwardsLeft";
    const string BACKWARDRIGHT      = "BasicMotions@Walk01 - BackwardsRight";

    const string LEFT               = "BasicMotions@Walk01 - Left";
    const string RIGHT              = "BasicMotions@Walk01 - Right";


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void ChangeAnimationState(string newState)
    {   
        //stop same animation from interupting itself
        if(currentState == newState) 
        {
            return;
        }

        //plays the animations
        animator.Play(newState);
        
        currentState = newState;
        
        
    }
    void endOfJump(){
        animator.SetBool("isJumping", false);
    }
    // Update is called once per frame
    void Update()
    {   
        
        bool isForward = animator.GetBool("isForward");
        bool wPress = Input.GetKey("w");

        bool isLeft = animator.GetBool("isLeft");
        bool aPress = Input.GetKey("a");

        bool isBackward = animator.GetBool("isBackward");
        bool sPress = Input.GetKey("s");

        bool isRight = animator.GetBool("isRight");
        bool dPress = Input.GetKey("d");

        bool isRunning = animator.GetBool("isRunning");
        bool shiftPress = Input.GetKey("left shift");

        bool isJumping = animator.GetBool("isJumping");
        bool jumpPress = Input.GetKey("space");

        if(!isForward && !isLeft && !isBackward && !isRight && !isJumping)
        {
            animator.SetBool("isIdle", true);
        }

        if(isForward || isLeft || isBackward || isRight || isJumping)
        {
            animator.SetBool("isIdle", false);
        }
        if(jumpPress)
        {
            animator.SetBool("isJumping", true);

            animator.SetBool("isIdle", false);
        }

        if(shiftPress)
        {
            animator.SetBool("isRunning", true);
        }

        if(!shiftPress)
        {
            animator.SetBool("isRunning", false);
        }

        

        if(!isForward && wPress)
        {   
            animator.SetBool("isForward", true);
        }

        if(isForward && !wPress)
        {
            animator.SetBool("isForward", false);
        }

        if(!isBackward && sPress)
        {
            animator.SetBool("isBackward", true);
        }

        if(isBackward && !sPress)
        {
            animator.SetBool("isBackward", false);
        }

        if(!isLeft && aPress)
        {
            animator.SetBool("isLeft", true);
        }

        if(isLeft && !aPress)
        {
            animator.SetBool("isLeft", false);
        }

        if(!isRight && dPress)
        {
            animator.SetBool("isRight", true);
        }

        if(isRight && !dPress)
        {
            animator.SetBool("isRight", false);
        }

        if(isForward && isBackward)
        {
            animator.SetBool("isForward", false);
            animator.SetBool("isBackward", false);
        }

        if(isLeft && isRight)
        {
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1){
            Debug.Log("not playing");
        }
            if(!isRight && !isLeft && !isForward && !isBackward && !isJumping)
            {
                ChangeAnimationState(IDLE);
            }

            if(isJumping)
            {
                ChangeAnimationState(JUMP);

            }    

        if(isForward && !isJumping){
            if(!isBackward && !isLeft && !isRight)
            {   
                if(shiftPress){
                    ChangeAnimationState(RUNFORWARD);
                }
                else{
                    ChangeAnimationState(FORWARD);
                }
            }
            if(isLeft && !isRight)
            {
                if(shiftPress){
                    ChangeAnimationState(RUNFORWARDLEFT);
                }
                else{
                    ChangeAnimationState(FORWARDLEFT);
                }
            }
            if(isRight && !isLeft)
            {
                if(shiftPress){
                    ChangeAnimationState(RUNFORWARDRIGHT);
                }
                else{
                    ChangeAnimationState(FORWARDRIGHT);
                }
            } 
        }

        if(isBackward && !isJumping){
            if(!isForward && !isLeft && !isRight)
            {
                if(shiftPress){
                    ChangeAnimationState(RUNBACKWARD);
                }
                else{
                    ChangeAnimationState(BACKWARD);
                }
            }
            if(isLeft && !isRight)
            {
                if(shiftPress){
                    ChangeAnimationState(RUNBACKWARDLEFT);

                }
                else{
                    ChangeAnimationState(BACKWARDLEFT);
                }
            }
            if(isRight && !isLeft)
            {
                if(shiftPress){
                    ChangeAnimationState(RUNBACKWARDRIGHT);
                }
                else{
                    ChangeAnimationState(BACKWARDRIGHT);
                }
            } 
        }

        if(isLeft && !isRight && !isForward && !isBackward && !isJumping)
        {
            if(shiftPress){
                ChangeAnimationState(RUNLEFT);
            }
            else{
                ChangeAnimationState(LEFT);
            }
        }

        if(isRight && !isLeft && !isForward && !isBackward && !isJumping)
        {
            if(shiftPress){
                ChangeAnimationState(RUNRIGHT);
            }
            else{
                ChangeAnimationState(RIGHT);
            }
        }
    }
}

