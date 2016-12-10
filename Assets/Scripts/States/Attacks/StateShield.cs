using UnityEngine;
using System.Collections;

public class StateShield : BaseState
{
	Projectile m_shield;

	private float m_stateTime = 0.45f;
	private float m_throwTime = 0.32f;
	private float m_timer = 0.0f;
	private bool m_thrown = false;
	private float m_shieldXSpeed = 0.0f;
	private float m_shieldYSpeed = 0.0f;

	public override void Initialize(Charicter me)
	{
		m_me = me;

		// adds the shield object to the player datas list of projectiles
		m_me.AddProjectile(m_shield);
	}

	// gives the state a refrence to the shield object used
	public void giveShield(Projectile shield)
	{
		m_shield = shield;
		m_shield.Initialize();
	}

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_stateTime;
	}

	public override void Exit()
	{
		m_thrown = false;
	}
	
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_timer -= deltaTime;

		m_me.setVelocity(0.0f, -75.0f * deltaTime);

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;
		else if (m_timer < m_throwTime && !m_thrown)
		{
			float xVal = 0.0f;
			float yVal = 0.0f;

			// if the time in the state is past the fireing time and the shield hasen't be fired yet, fires the shield
			m_shield.Fire(m_me, m_shieldXSpeed, m_shieldYSpeed, m_me.GetHitboxManager(), 2.0f);
			m_thrown = true;
		}
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		if ((inputs[(int)eInputs.LEFT] || inputs[(int)eInputs.RIGHT]) && inputs[(int)eInputs.UP])
		{
			m_shieldXSpeed = 6.364f * m_me.getIntFacingLeft();
			m_shieldYSpeed = 6.364f;
		}
		else if (inputs[(int)eInputs.UP])
		{
			m_shieldXSpeed = 0.0f;
			m_shieldYSpeed = 9.0f;
		}
		else
		{
			m_shieldXSpeed = 9.0f * m_me.getIntFacingLeft();
			m_shieldYSpeed = 0.0f;
		}
	}
}
