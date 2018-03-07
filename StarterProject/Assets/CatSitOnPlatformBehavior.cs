using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSitOnPlatformBehavior : StateMachineBehaviour {

    float startTime;
    public float actionWaitTime;
    GameObject platform;
    public float retreatingSpd;
    public bool retreating;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime = Time.time;
        platform = CatController.Instance.standOn;
        retreating = false;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("grounded") == false)
        {
            animator.SetTrigger("falling");
        }

        else
        {
            if (Mathf.Abs(animator.gameObject.transform.position.x - platform.transform.position.x) / (platform.GetComponent<SpriteRenderer>().size.x / 2f) > 0.8f)
            {
                retreating = true;
            }
            if (retreating)
            {
                animator.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Sign(animator.gameObject.transform.position.x - platform.transform.position.x) * retreatingSpd, 0);
                if (Mathf.Abs(animator.gameObject.transform.position.x - platform.transform.position.x) / (platform.GetComponent<SpriteRenderer>().size.x / 2f) < 0.6f)
                {
                    animator.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    retreating = false;
                }
            }
            if (Time.time - startTime > actionWaitTime)
            {
                actionWaitTime += Random.Range(-2f, 2f);
                if (CatController.Instance.platforms.Count > 0)
                {
                    ChooseAPlatformToJump();
                    return;
                }
                else
                {
                    if (!retreating)
                    animator.SetTrigger("walk");
                }
            }
        }
    }
    void Retreat()
    {

    }
    void ChooseAPlatformToJump()
    {
        if (CatController.Instance.platforms.Count == 1)
        {
            JumpTo(CatController.Instance.platforms[0]);
        }
        else
        {
            Vector2 toF = CatController.Instance.fish.transform.position - CatController.Instance.gameObject.transform.position;

            float biggestCos = 0;
            GameObject bestP = null;
            foreach (GameObject p in CatController.Instance.platforms)
            {
                Vector2 toP = p.transform.position - CatController.Instance.gameObject.transform.position;
                float cos = Vector2.Dot(toF.normalized , toP.normalized );
                if (cos > biggestCos)
                {
                    biggestCos = cos;
                    bestP = p;                    
                }
            }
            JumpTo(bestP);
        }
    }
    void JumpTo(GameObject target)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
