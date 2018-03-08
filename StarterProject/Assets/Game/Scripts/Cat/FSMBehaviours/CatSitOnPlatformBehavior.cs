using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSitOnPlatformBehavior : StateMachineBehaviour {

    private GameObject platform;

    private float timer;

    public float actionWaitTime;
    public float jumpVelo;
    public float retreatingSpd;
    public bool retreating;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        platform = CatController.Instance.standOn;
        retreating = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        
        if (animator.GetBool("fishInSight"))
        {
            animator.SetTrigger("jumping");
        }
        // Ground disapeared
        else if (animator.GetBool("grounded") == false || platform == null)
        {
            animator.SetTrigger("falling");
            CatController.Instance.animMessanger.sendTriggerMessage("falling");
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
                if (Mathf.Abs(animator.gameObject.transform.position.x - platform.transform.position.x) / (platform.GetComponent<SpriteRenderer>().size.x / 2f) < 0.4f)
                {
                    animator.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    retreating = false;
                }
            }
            else if (timer > actionWaitTime)
            {
                if (CatController.Instance.platforms.Count > 1)
                {
                    animator.SetTrigger("jumping");
                }
                else if (!retreating)
                {
                    animator.SetTrigger("walk");
                }

                actionWaitTime = Random.Range(1.0f, 3.0f);
                timer = 0;
            }
        }
    }
}
