using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAtPlayer : StateMachineBehaviour {
    //public GameObject PlaneObject;
    private Light flashingLight;
    private bool lowLight = false;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        flashingLight = animator.GetComponentInChildren<Light>();

        //flashingLight = PlaneObject.GetComponent<Light>();
        flashingLight.enabled = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {


        if (lowLight)
        {
            flashingLight.intensity += 8f * Time.deltaTime;

            if (flashingLight.intensity >= 8) lowLight = false;
        }
        else
        {
            flashingLight.intensity -= 8f * Time.deltaTime;

            if (flashingLight.intensity <= 2) lowLight = true;
        }

        //flashingLight.intensity = Mathf.Lerp(4,10, 2f * Time.deltaTime );
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        flashingLight.enabled = false;
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
