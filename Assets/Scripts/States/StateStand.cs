using UnityEngine;
using System.Collections;

public class StateStand : BaseState
{
	public override void Enter(eStates m_priviousState)
	{
		// resets the size of the charicters hitbox
		m_me.resetHeight();
	}

	public override void Exit()
	{
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.setVelocity(0.0f, -75.0f * deltaTime);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		if (inputs[(int)eInputs.DOWN] && inputs[(int)eInputs.ATTACK_1])
		{
			m_currentState = eStates.SLIDING;
		}
		else if (inputs[(int)eInputs.ATTACK_1])
		{
			m_currentState = eStates.ATTACK_1;
		}
		else if (inputs[(int)eInputs.ATTACK_2])
		{
			m_currentState = eStates.ATTACK_2;
		}
		else if (inputs[(int)eInputs.BLOCK])
		{
			m_currentState = eStates.BLOCK;
		}
		else if (inputs[(int)eInputs.LEFT] || inputs[(int)eInputs.RIGHT])
		{
			m_currentState = eStates.WALKING;
		}
		else if (inputs[(int)eInputs.JUMP])
		{
			//m_currentState = eStates.JUMPING;
			m_currentState = eStates.PRE_POST_JUMP;
		}
	}

	public override void NotCollideVertical(ref eStates m_currentState)
	{
		// if the charicter is pushed of an edge moves to the falling state
		m_currentState = eStates.FALLING;
	}
}
