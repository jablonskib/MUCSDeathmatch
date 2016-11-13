using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class PlayerAnimConNetwork : NetworkBehaviour
{

    FirstPersonController fpc;
    PlayerStatus ps; 
    public Animator animator;
    NetworkAnimator nwAnim;
    // Use this for initialization
    void Start()
    {
        ps = GetComponent<PlayerStatus>();
        fpc = GetComponent<FirstPersonController>();
        animator = GetComponent<Animator>();
        GetComponent<NetworkAnimator>();

    }

    // Update is called once per frame
    void Update()
    {
       /* if(fpc.m_Jump)
        {
           // animator.SetBool("Jumping", true);
            if (isServer)
            {
                RpcChangeAnimation("Jumping", true);
            }
            else
            {
                CmdChangeAnimation("Jumping", true);
            }
        }
        else
        {
            //animator.SetBool("Jumping", false);
            if (isServer)
            {
                RpcChangeAnimation("Jumping", false);
            }
            else
            {
                CmdChangeAnimation("Jumping", false);
            }
            
        }*/
			

		float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        GetComponentInChildren<NetworkAnimator>().SetParameterAutoSend(0, true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }

        

        if(Input.GetButton("Fire1"))
        {
            animator.SetBool("Shooting", true);
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
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


    [Command]
    void CmdChangeAnimation(string animName, bool animState)
    {
        NetworkServer.FindLocalObject(this.netId).GetComponent<Animator>().SetBool(animName, animState);
    }

	[Command]
	void CmdChangeAnimationInt(string animName, int animState)
	{
		NetworkServer.FindLocalObject(this.netId).GetComponent<Animator>().SetInteger(animName, animState);
	}

    [ClientRpc]
    void RpcChangeAnimation(string animName, bool animState)
    {

           animator.SetBool(animName, animState);
    }

	[ClientRpc]
	void RpcChangeAnimationInt(string animName, int animState)
	{
		animator.SetInteger(animName, animState);
	}
}

