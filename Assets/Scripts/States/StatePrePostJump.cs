using UnityEngine;
using System.Collections;

public class StatePrePostJump : BaseState
{
	private float m_launch = 14.0f;
	private bool m_preJump = false;

	private float m_timeInState = 0.05f;
	private float m_timer = 0.0f;

	private float m_direction;
	private float m_movementSpeed = 6.0f;

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_timeInState;
		m_me.setVelocity(0.0f, 0.0f);

		// determans when the player is contacting or leaving the ground
		if (m_priviousState == eStates.FALLING)
			m_preJump = false;
		else
			m_preJump = true;
	}

	public override void Exit()
	{

	}

	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_timer -= deltaTime;

		if (m_timer <= 0.0f)
		{
			// if the time in the state has gone by, transitions to the next state
			if (m_preJump)
			{
				m_me.setVelocity(m_movementSpeed * m_direction, m_launch);
				m_currentState = eStates.FALLING;
			}
			else
				m_currentState = eStates.STANDING;
		}
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		// gets the desired x velocity from user input
		if (inputs[(int)eInputs.LEFT])
			m_direction = -1.0f;
		else if (inputs[(int)eInputs.RIGHT])
			m_direction = 1.0f;
		else
			m_direction = 0.0f;
	}

	public override void CollideVertical(ref eStates m_currentState)
	{
	}
}
