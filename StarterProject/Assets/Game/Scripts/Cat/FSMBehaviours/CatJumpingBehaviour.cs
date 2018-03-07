using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatJumpingBehaviour : StateMachineBehaviour {

    private GameObject platform;

    public float velocity = 2.0f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        platform = CatController.Instance.standOn;

        ChooseAPlatformToJump();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // Determine which platform to jump to
    void ChooseAPlatformToJump()
    {
        Vector2 toFish = CatController.Instance.fish.transform.position - CatController.Instance.gameObject.transform.position;

        float smallestAngle = 359;
        GameObject bestP = null;

        foreach (GameObject p in CatController.Instance.platforms)
        {
            if (p != null && p != CatController.Instance.standOn && p.gameObject.tag != "Ground")
            {
                Vector2 toP = p.transform.position - CatController.Instance.gameObject.transform.position;
                float angle = Vector2.Angle(toFish.normalized, toP.normalized);

                if (angle < smallestAngle)
                {
                    smallestAngle = angle;
                    bestP = p;
                }
            }
        }

        JumpTo(bestP);
    }

    // Calculate tragectory path
    void JumpTo(GameObject target)
    {
        if (target != null)
        {
            //float x = Mathf.Clamp(CatController.Instance.gameObject.transform.position.x, target.transform.position.x - platform.GetComponent<SpriteRenderer>().size.x / 2f, target.transform.position.x + platform.GetComponent<SpriteRenderer>().size.x / 2f) - CatController.Instance.gameObject.transform.position.x;
            //float y = target.transform.position.y - CatController.Instance.gameObject.transform.position.y;

            ////float jumpAngle = Mathf.Atan(((jumpVelo * jumpVelo + Mathf.Sqrt(Mathf.Pow(jumpVelo, 4) - Physics2D.gravity.y * (Physics2D.gravity.y * x * Physics2D.gravity.y * x + 2 * y * jumpVelo * jumpVelo))) / (Physics2D.gravity.y * x)));
            
            //float verticalIniVelo = Mathf.Sqrt(2 * -Physics2D.gravity.y * (y + 1f));
            //float flightTime = verticalIniVelo / (-Physics2D.gravity.y);
            //float horizontalIniVelo = x / flightTime;


            
            float x = target.transform.position.x - CatController.Instance.gameObject.transform.position.x;
            float y = target.transform.position.y - CatController.Instance.gameObject.transform.position.y;

            float gravity = Physics2D.gravity.y;

            float rootedVal = Mathf.Sqrt(
                Mathf.Pow(velocity, 4)
                - gravity
                    * (gravity * Mathf.Pow(x, 2)
                        + 2.0f * y * Mathf.Pow(velocity, 2))
            );

            float angle = Mathf.Rad2Deg * Mathf.Atan(
                (Mathf.Pow(velocity, 2) + rootedVal) / gravity * x
            );

            angle = Mathf.Abs(angle);

            Debug.Log(angle);

            Vector2 trajectory = new Vector2();
            trajectory.x = CatController.Instance.transform.right.x;
            trajectory.y = CatController.Instance.transform.right.y;

            CatController.Instance.transform.Rotate(Vector3.forward, angle);

            CatController.Instance.gameObject.GetComponent<Rigidbody2D>().velocity = CatController.Instance.transform.right * velocity;


            CatController.Instance.transform.right = Vector3.right;

            //CatController.Instance.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalIniVelo, verticalIniVelo);

            CatController.Instance.gameObject.GetComponent<Animator>().SetTrigger("falling");
        }
        else
        {
            CatController.Instance.gameObject.GetComponent<Animator>().SetTrigger("walk");
        }
    }
}
