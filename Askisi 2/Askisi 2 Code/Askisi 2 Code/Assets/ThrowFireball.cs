using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFire : StateMachineBehaviour
{
    private Animator anim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorStateInfo previousStateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        if (previousStateInfo.IsName("Charge_fireball")){
            // Get the FireballEnemy script and call the ThrowFireball function
            FireballEnemy fireballEnemy = animator.GetComponent<FireballEnemy>();
            if (fireballEnemy != null)
            {
                fireballEnemy.ThrowFireball(); 
            }
        } 
    }
}
