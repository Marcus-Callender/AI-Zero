using UnityEngine;
using System.Collections;

public class StateWalk : BaseState
{
	private float m_movementSpeed = 6.0f;

	public override void Enter(eStates m_priviousState)
	{
	}

	public override void Exit()
	{
	}
	
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		// sets the charicters x velocity relative to the charicters orientation
		m_me.setVelocity(m_movementSpeed * m_me.getIntFacingLeft(), -75.0f * deltaTime);
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
		else if (inputs[(int)eInputs.JUMP])
		{
			m_currentState = eStates.PRE_POST_JUMP;
		}
		else if (inputs[(int)eInputs.LEFT])
		{
			m_me.setLeft(true);
		}
		else if (inputs[(int)eInputs.RIGHT])
		{
			m_me.setLeft(false);
		}
		else
		{
			m_currentState = eStates.STANDING;
		}
	}

	// if there is no floor under the charicter, moves to falling state
	public override void NotCollideVertical(ref eStates m_currentState)
	{
		m_currentState = eStates.FALLING;
	}
}
