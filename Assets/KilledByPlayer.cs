using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledByPlayer : StateMachineBehaviour {

	MotherShip motherShip;
	GameManager gm;
	GameObject plane;
    Player p;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		motherShip = GameObject.FindObjectOfType<MotherShip>();
		gm = GameObject.FindObjectOfType<GameManager>();
		plane = animator.gameObject;
        p = GameObject.FindObjectOfType<Player>();


		motherShip.m_shipsLeftToDestroy -= 1;
		if (plane.tag == "Bomber")
		{
			gm.AddScore(motherShip.b_stats.scoreValue);
            //p.m_health -= 10;
		}
		else if (plane.tag == "Fighter")
		{
			gm.AddScore(motherShip.f_stats.scoreValue);
		}
		else if (plane.tag == "Captain")
		{
			gm.AddScore(motherShip.c_stats.scoreValue);
		}
		else
		{
			Debug.Log("NO TAG ON THIS PLANE ");
			Debug.Break();
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		
		

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
