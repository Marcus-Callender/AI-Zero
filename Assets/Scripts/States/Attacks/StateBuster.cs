using UnityEngine;
using System.Collections;

public class StateBuster : BaseState
{
	Projectile[] m_buletts = new Projectile[3];

	private float m_stateTime = 0.45f;
	private float m_fireTime = 0.32f;
	private float m_timer = 0.0f;
	private bool m_fired = false;

	public override void Initialize(Charicter me)
	{
		m_me = me;

		// gives a refrence to the players data about the bulett objects
		for (int z = 0; z < 3; z++)
		{
			m_me.AddProjectile(m_buletts[z]);
		}
	}

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_stateTime;
	}

	public void GiveProjectile(Projectile shield)
	{
		for (int z = 0; z < 3; z++)
		{
			//finds an avalable place in the m_buletts array and places the new buett in it, then initializes it
			if (m_buletts[z] == null)
			{
				m_buletts[z] = shield;
				m_buletts[z].Initialize();
				break;
			}
		}
	}

	public override void Exit()
	{
		m_fired = false;
	}
	
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_timer -= deltaTime;

		m_me.setVelocity(0.0f, -75.0f * deltaTime);

		// if the states timer has ended, leaves the state
		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;
		else if (m_timer < m_fireTime && !m_fired)
		{
			// the m_fired makes sure this code only runs once per time in state
			for (int z = 0; z < 3; z++)
			{
				// when the bulett needs to be fired it looks for a inactive bulett to fire, fires it then breaks
				if (!m_buletts[z].GetActive())
				{
					m_buletts[z].Fire(m_me, 9.0f * m_me.getIntFacingLeft(), 0.0f, m_me.GetHitboxManager(), 2.0f);
					m_fired = true;
					break;
				}
			}
		}
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{

	}
}
