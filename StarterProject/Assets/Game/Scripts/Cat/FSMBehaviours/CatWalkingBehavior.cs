using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWalkingBehavior : StateMachineBehaviour {

    public float walkingSpd;
    public float maxWalkTime = 3.0f, minWalkTime = 1.0f;
    float walkTime;
    float direction;

    GameObject platform;

    float startTime;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CatController.Instance.animMessanger.sendTriggerMessage("walking");

        startTime = Time.time;
        platform = CatController.Instance.standOn;

        walkTime = Random.Range(minWalkTime, maxWalkTime);
        direction = Mathf.Sign(Random.Range(1f, -1f));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("grounded"))
        {
            animator.SetTrigger("stopWalking");
        }
        else
        {
            animator.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(walkingSpd * direction, 0.0f);

            if (direction > 0.0f)
            {
                animator.gameObject.transform.right = Vector3.right;
            }
            else
            {
                animator.gameObject.transform.right = Vector3.left;
            }

            if (
                (Mathf.Abs(animator.gameObject.transform.position.x - platform.transform.position.x) / (platform.GetComponent<SpriteRenderer>().size.x / 2f) > 0.8f)
                || ((Time.time - startTime) > walkTime)
                )
            {
                animator.SetTrigger("stopWalking");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CatController.Instance.animMessanger.sendTriggerMessage("sitting");

        animator.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
