using UnityEngine;
using System.Collections;

// UNUSED

public class StateHitGround : BaseState
{
	public override void Enter(eStates m_priviousState)
	{
	}

	public override void Exit()
	{

	}

	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.setVelocity(0.0f, 0.0f);

		if (!m_me.IsInStun())
			m_currentState = eStates.STANDING;
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{

	}

	public override void CollideVertical(ref eStates m_currentState)
	{
	}
}
