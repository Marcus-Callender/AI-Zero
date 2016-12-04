using UnityEngine;
using System.Collections;

public class StateGuard : BaseState
{
	public override void Enter(eStates m_priviousState)
	{
		m_me.SetGuarding(true);
	}

	public override void Exit()
	{
		m_me.SetGuarding(false);
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.setVelocity(0.0f, 0.0f);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		if (!m_me.IsInStun())
		{
			if (!inputs[(int)eInputs.BLOCK])
				m_currentState = eStates.STANDING;

			else if (inputs[(int)eInputs.ATTACK_1] || inputs[(int)eInputs.ATTACK_2])
				m_currentState = eStates.THROW;

			else if (inputs[(int)eInputs.LEFT])
				m_me.setLeft(true);

			else if (inputs[(int)eInputs.RIGHT])
				m_me.setLeft(false);
		}
	}
}
