using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will be attached to any of our projectiles
public class BlowUpMissile : StateMachineBehaviour {
    public AudioClip blowUpSound;
    public GameObject explosiveParticles;
    public bool hasHappendOnce = false;

    private ParticleSystem missileExhaust;
    ParticleSystem.MainModule mainMan;
    //public GameObject missileModel;
   
    
    //private ParticleSystem p;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //mainMan.loop = false;

        if (!hasHappendOnce)
        {
            Instantiate<GameObject>(explosiveParticles, animator.gameObject.transform.position, Quaternion.identity);
            animator.gameObject.transform.Find("Model").GetComponent<MeshRenderer>().enabled = false;
            missileExhaust = animator.gameObject.transform.Find("ParticleStuff").GetComponent<ParticleSystem>();
            missileExhaust.transform.SetParent(null);
            mainMan = missileExhaust.main;
            mainMan.loop = false;
            //missileExhaust.main.loop = mainMan.loop;

            if (animator.GetComponent<ParticleSystem>() != null)
            {
                animator.GetComponent<ParticleSystem>().Play();
            }
            hasHappendOnce = true;
        }
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
    }

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
