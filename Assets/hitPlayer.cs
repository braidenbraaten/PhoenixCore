using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//what will happen if the plane hits the player (before death)
public class hitPlayer : StateMachineBehaviour {
	public Player player;
	public MotherShip motherShip;
    //the plane that we are going to switch to when it hits a player
    public GameObject planeSwap;
    private GameObject swapedPlane;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        swapedPlane = Instantiate(planeSwap, animator.gameObject.transform.position, animator.gameObject.transform.rotation);
        player = GameObject.FindObjectOfType<Player>();
        player.m_health -= 10;
        //find a way to make the plane turn it's opacity down so it fades out
        Destroy(swapedPlane, 6.0f);



	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
