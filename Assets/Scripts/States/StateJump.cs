using UnityEngine;
using System.Collections;

// UNUSED

public class StateJump : BaseState
{
	private float m_gravity = -25.0f;
	private float m_launch = 14.0f;

	public override void Enter(eStates m_priviousState)
	{
		m_me.addToVelocity(0.0f, m_launch);
	}

	public override void Exit()
	{

	}
	
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.addToVelocity(0.0f, m_gravity * deltaTime);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{

	}

	public override void CollideVertical(ref eStates m_currentState)
	{
		m_currentState = eStates.STANDING;
	}
}
