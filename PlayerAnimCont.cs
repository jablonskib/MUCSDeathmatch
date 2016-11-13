using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
[RequireComponent(typeof(Animator))]
public class PlayerAnimCont : MonoBehaviour 
{
    FirstPersonController fpc;
    Animator animator;
	// Use this for initialization
	void Start () 
    {
        fpc = GetComponent<FirstPersonController>();
        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () 
    {
		float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		float vertical = CrossPlatformInputManager.GetAxis("Vertical");
        Debug.Log(animator.GetBool("Walking"));
        animator.SetBool("Jumping", fpc.m_Jump);


    
		if(vertical > 0)
		{
			animator.SetInteger("Walking", 1);
		}

		if(vertical < 0 )
		{
			animator.SetInteger("Walking", 3);
		}

		if(horizontal > 0)
		{
			animator.SetInteger("Walking", 4);
		}

		if(horizontal < 0)
		{
			animator.SetInteger("Walking" , 2);
		}

		if(horizontal == 0 && vertical == 0)
		{
			animator.SetInteger("Walking" , 0);
		}
	}


}
