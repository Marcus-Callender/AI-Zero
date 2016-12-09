using UnityEngine;
using System.Collections;

public class StateHitAir : BaseState
{
	private float m_gravity = -25.0f;

	public override void Enter(eStates m_priviousState)
	{
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
		// returns to normal after coliding with ground or KO'D if player has no health remaining
		if (m_me.IsKOd())
			m_currentState = eStates.KO_D;
		else
			m_currentState = eStates.STANDING;
	}
}
