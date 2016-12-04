using UnityEngine;
using System.Collections;

public class StateFall : BaseState
{
	private float m_gravity = -25.0f;

	private float m_airAcceleration = 4.0f;
	private float m_maxAirSpeed = 6.0f;
	private float m_airSpeed = 0.0f;

	public override void Enter(eStates m_priviousState)
	{
		m_airSpeed = m_me.GetXVelocity();
	}

	public override void Exit()
	{

	}

	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.addToVelocity(0.0f, m_gravity * deltaTime);
		//m_me.setXVelocity(m_airSpeed);
		//m_me.addToVelocity(m_airSpeed, m_gravity * deltaTime);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		if (inputs[(int)eInputs.RIGHT])
		{
			m_airSpeed += m_airAcceleration;

			if (m_airSpeed < m_maxAirSpeed)
				m_airSpeed = m_maxAirSpeed;
		}
		else if (inputs[(int)eInputs.LEFT])
		{
			m_airSpeed -= m_airAcceleration;

			if (m_airSpeed > -m_maxAirSpeed)
				m_airSpeed = -m_maxAirSpeed;
		}
	}

	public override void CollideVertical(ref eStates m_currentState)
	{
		m_currentState = eStates.STANDING;
	}
}
