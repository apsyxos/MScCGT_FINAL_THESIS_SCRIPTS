using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//functions that control the variables of the enemy's animation behaviour
//links them with the tags that we use in the TagHolder script.
public class EnemyAnimator : MonoBehaviour {
    private Animator anim;
	void Awake () {
        anim = GetComponent<Animator>();	
	}	
    public void Walk(bool walk) {
        anim.SetBool(AnimationTags.WALK_PARAMETER, walk);
    }
    public void Run(bool run) {
        anim.SetBool(AnimationTags.RUN_PARAMETER, run);
    }
    public void Attack() {
        anim.SetTrigger(AnimationTags.ATTACK_TRIGGER);
    }
    public void Dead() {
        anim.SetTrigger(AnimationTags.DEAD_TRIGGER);
    }
}