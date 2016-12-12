using UnityEngine;
using System.Collections;

public class StateSlide : BaseState
{
	private float m_slideTime = 0.82f;
	private float m_slideStart = 0.66f;
	private float m_slideEnd = 0.16f;

	private float m_timer;
	private float m_slideSpeed = 8.0f;
	private int m_hitboxID = -1;

	private float m_gravity = -120.0f;

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_slideTime;
		
		// sets the players hurt to be lower than usual
		m_me.setHeight(1.2f);

		m_hitboxID = -1;
	}

	public override void Exit()
	{
		// ensures the hitbox is removed
		m_me.RemoveHitbox(m_hitboxID);
	}
	
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		// sets the players velocity to match the players orientation
		if ((m_timer > m_slideStart) || (m_timer < m_slideEnd))
		{
			m_me.setXVelocity(0.0f);
			m_me.DisableHitbox(m_hitboxID);
		}
		else
		{
			// creates the hitbox if it dosen't exist
			if (m_hitboxID == -1)
				m_hitboxID = m_me.AddHitbox(m_me.getX(), m_me.getY(), 2.75f, 1.75f, 3, 6.0f, 12.0f, 0.0f, 0.2f, eAttackType.STRIKE);

			// allignes the players velocity with there charicters orientation
			if (m_me.getFacingLeft())
				m_me.setXVelocity(-m_slideSpeed);
			else
				m_me.setXVelocity(m_slideSpeed);
		}

		// updates the position of the slides hitbox
		m_me.SetHitboxPos(m_hitboxID, m_me.getX(), m_me.getY());

		m_timer -= deltaTime;

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;

		m_me.addToVelocity(0.0f, m_gravity * deltaTime);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		// allows the player to change there direction mid slie
		if (inputs[(int)eInputs.LEFT])
			m_me.setLeft(true);

		if (inputs[(int)eInputs.RIGHT])
			m_me.setLeft(false);
	}

	public override void CollideVertical(ref eStates m_currentState)
	{
		// stops the player from sinking into the ground
		m_me.setVelocity(m_me.GetXVelocity(), 0.0f);
	}
}
