﻿using UnityEngine;
using System.Collections;

public enum eAI_InputArray
{
	ENEMY_CLOSE = 1,
	ENEMY_MID,
	ENEMY_FAR,

	ENEMY_JUMPING,
	ENEMY_ON_LEFT,

	TOTAL_INPUTS
}

public enum eAI_Actions
{
	ZONE,
	POKE,
	PRESSURE,

	JUMP_IN,
	ANTI_AIR,
	DEFFEND,

	FREE,
	WAIT
}

public enum eAI_SubAction
{
	NONE,

	THROW,
	SLIDE,

	SABER_MOVE,
	ANTI_AIR_SHIELD,
	BUSTER_BARAGE
}

public class AI_Input : BaseInputHandler
{
	eWeaponType m_weapon1;
	eWeaponType m_weapon2;

	private float m_stateTime = 0.0f;
	private float m_waitingTime = 1.5f;
	private eAI_Actions m_currentAction;
	private eAI_Actions m_previousAction;
	private bool[] m_input = new bool[(int)eInputs.SIZE_OF_E_INPUTS];
	//private int m_StatusInputs = 0;
	private int m_statePosition = 0;
	private int m_subStatePosition = 0;

	//private float m_baseInputTime = 0.25f;

	//private float[] m_InputTimings = new float[(int)eAI_InputArray.TOTAL_INPUTS];
	//private bool[] m_InputResults = new bool[(int)eAI_InputArray.TOTAL_INPUTS];

	private float m_maxTimeInState = 3.0f;
	private eAI_SubAction m_subAction = eAI_SubAction.NONE;
	private float m_defendTime = 0.7f;

	//private int DEBUG_STATE = -1;

	private float m_timeBoundries = 0.5f;
	private float[] m_timers = new float[(int)eAI_InputArray.TOTAL_INPUTS];
	private int m_statusResult = 0;

	public override void Initialize()
	{
		m_currentAction = eAI_Actions.WAIT;
		m_previousAction = eAI_Actions.WAIT;

		for (int z = 0; z < (int)eAI_InputArray.TOTAL_INPUTS; z++)
		{
			//m_InputTimings[z] = m_baseInputTime;
			//m_InputResults[z] = false;
			m_timers[z] = -m_timeBoundries;
		}
	}

	public override void Cycle(float deltaTime, eStates myState)
	{
		for (int z = 0; z < (int)eInputs.SIZE_OF_E_INPUTS; z++)
		{
			m_input[z] = false;
		}

		bool stateChanged = false;

		if (m_previousAction != m_currentAction)
		{
			Debug.Log("Going to state: " + m_currentAction);

			/*for (int z = 0; z < (int)eAI_InputArray.TOTAL_INPUTS; z++)
			{
				m_InputTimings[z] = m_baseInputTime;
				m_InputResults[z] = false;
			}*/

			m_stateTime = 0.0f;
			m_previousAction = m_currentAction;
			m_statePosition = 0;

			stateChanged = true;
		}

		if (m_subAction == eAI_SubAction.NONE)
		{
			m_subStatePosition = 0;
			RunAction(deltaTime, myState, stateChanged);
		}
		else
		{
			RunSubAction(deltaTime, myState, stateChanged);
		}
	}

	void RunAction(float deltaTime, eStates myState, bool stateChanged)
	{
		m_stateTime += deltaTime;

		if (m_stateTime > m_maxTimeInState)
			m_currentAction = eAI_Actions.FREE;

		if (m_currentAction == eAI_Actions.ANTI_AIR)
			m_currentAction = AntiAir(deltaTime, myState, stateChanged);

		else if (m_currentAction == eAI_Actions.DEFFEND)
			m_currentAction = Defend(deltaTime, myState, stateChanged);

		else if (m_currentAction == eAI_Actions.JUMP_IN)
			m_currentAction = JumpIn(deltaTime, myState, stateChanged);

		else if (m_currentAction == eAI_Actions.POKE)
			m_currentAction = Poke(deltaTime, myState, stateChanged);

		else if (m_currentAction == eAI_Actions.PRESSURE)
			m_currentAction = Pressure(deltaTime, myState, stateChanged);

		else if (m_currentAction == eAI_Actions.ZONE)
			m_currentAction = Zone(deltaTime, myState, stateChanged);

		else if (m_currentAction == eAI_Actions.FREE)
			m_currentAction = FreeAction(deltaTime, myState, stateChanged);

		else if (m_currentAction == eAI_Actions.WAIT)
			m_currentAction = Wait(deltaTime, myState, stateChanged);
	}

	void RunSubAction(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_subAction == eAI_SubAction.THROW)
		{
			ThrowSubAction(deltaTime, myState, stateChanged);
		}
		else if (m_subAction == eAI_SubAction.SLIDE)
		{
			SlideSubAction(deltaTime, myState, stateChanged);
		}
		else
		{
			m_subAction = eAI_SubAction.NONE;
		}
	}

	public override void ReceveStatus(Charicter opponent, Charicter me, float deltaTime)
	{
		m_statusResult = 0;

		/*float distance = Mathf.Abs(opponent.getX() - me.getX());

		if (distance < 4.0f)
			m_StatusInputs += IntToFlag((int)eAI_InputArray.ENEMY_CLOSE);

		if (distance > 2.0f && distance < 16.0f)
			m_StatusInputs += IntToFlag((int)eAI_InputArray.ENEMY_MID);

		if (distance > 8.0f && distance < 32.0f)
			m_StatusInputs += IntToFlag((int)eAI_InputArray.ENEMY_FAR);

		if (opponent.getY() > me.getY())
			m_StatusInputs += IntToFlag((int)eAI_InputArray.ENEMY_JUMPING);

		if (opponent.getX() < me.getX())
			m_StatusInputs += IntToFlag((int)eAI_InputArray.ENEMY_ON_LEFT);*/

		SetInputStatus(opponent, me, deltaTime);
	}

	public void SetInputStatus(Charicter opponent, Charicter me, float deltaTime)
	{
		float distance = Mathf.Abs(opponent.getX() - me.getX());

		SetInput((int)eAI_InputArray.ENEMY_CLOSE, (distance < 4.0f), deltaTime);

		SetInput((int)eAI_InputArray.ENEMY_MID, (distance > 2.0f && distance < 16.0f), deltaTime);

		SetInput((int)eAI_InputArray.ENEMY_FAR, (distance > 8.0f && distance < 32.0f), deltaTime);

		SetInput((int)eAI_InputArray.ENEMY_JUMPING, (opponent.getY() > me.getY()), deltaTime);

		SetInput((int)eAI_InputArray.ENEMY_ON_LEFT, (opponent.getX() < me.getX()), deltaTime);
	}

	void SetInput(int z, bool active, float deltaTime)
	{
		bool activated = m_timers[z] > 0.0f;

		if (active)
		{
			m_timers[z] += deltaTime;

			if (m_timers[z] > m_timeBoundries)
				m_timers[z] = m_timeBoundries;
		}
		else
		{
			m_timers[z] -= deltaTime;

			if (m_timers[z] < -m_timeBoundries)
				m_timers[z] = -m_timeBoundries;
		}

		if (m_timers[z] > 0.0f)
			m_statusResult += IntToFlag(z);

		if (activated != (m_timers[z] > 0.0f))
		{
			if (activated)
				Debug.Log(z + " has been deactivated.");
			else
				Debug.Log(z + " has been activated.");
		}

		/*if (active)
		{
			if (m_InputTimings[z] <= 0.0f)
			{
				//m_InputResults[z] = true;
				m_StatusInputs += IntToFlag(z);
				//Debug.Log("Input " + z + " Receved.");
			}
			else
			{
				m_InputTimings[z] -= deltaTime;
				//Debug.Log("Input " + z + " Counting down.");

				if (m_InputTimings[z] < 0.0f)
					m_InputTimings[z] = 0.0f;
			}
		}
		else
		{
			m_InputTimings[z] += m_baseInputTime;
			//Debug.Log("Input " + z + " Counting up.");

			if (m_InputTimings[z] > m_baseInputTime)
				m_InputTimings[z] = m_baseInputTime;
		}*/
	}

	public override void Inputs(ref bool[] inputs)
	{
		for (int z = 0; z < (int)eInputs.SIZE_OF_E_INPUTS; z++)
			inputs[z] = m_input[z];

		//inputs[(int)eInputs.RIGHT] = true;
	}

	public override void GiveWeapons(eWeaponType weapon1, eWeaponType weapon2)
	{
		m_weapon1 = weapon1;
		m_weapon2 = weapon2;
	}

	public eAI_Actions Zone(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_statePosition == 0)
		{
			if ((m_statusResult & (IntToFlag((int)eAI_InputArray.ENEMY_FAR))) != IntToFlag((int)eAI_InputArray.ENEMY_FAR))
			{
				if ((m_statusResult & (IntToFlag((int)eAI_InputArray.ENEMY_JUMPING))) != IntToFlag((int)eAI_InputArray.ENEMY_JUMPING))
				{
					if ((m_statusResult & IntToFlag((int)eAI_InputArray.ENEMY_CLOSE)) == IntToFlag((int)eAI_InputArray.ENEMY_CLOSE))
					{
						MoveToward();
					}
					else
					{
						MoveAway();
					}
				}
			}
			else
				m_statePosition++;
		}
		else if (m_statePosition <= 5)
		{
			MoveToward();

			m_statePosition++;
		}
		else if (m_statePosition == 6)
		{
			UseRangedAttack();
			m_statePosition++;
		}
		else if (m_statePosition == 7)
		{
			if (myState == eStates.STANDING)
				return eAI_Actions.WAIT;
		}

		return eAI_Actions.ZONE;
	}

	public eAI_Actions Poke(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_statePosition == 0)
		{
			if ((m_statusResult & (IntToFlag((int)eAI_InputArray.ENEMY_MID) | IntToFlag((int)eAI_InputArray.ENEMY_CLOSE) | IntToFlag((int)eAI_InputArray.ENEMY_FAR))) != IntToFlag((int)eAI_InputArray.ENEMY_MID))
			{
				if ((m_statusResult & IntToFlag((int)eAI_InputArray.ENEMY_CLOSE)) != IntToFlag((int)eAI_InputArray.ENEMY_CLOSE))
				{
					MoveToward();
				}
				else
				{
					MoveAway();
				}
			}
			else
				m_statePosition++;
		}
		else if (m_statePosition <= 5)
		{
			MoveToward();

			m_statePosition++;
		}
		else if (m_statePosition == 6)
		{
			UseMidRangeAttack();
			m_statePosition++;
		}
		else if (m_statePosition == 7)
		{
			if (myState == eStates.STANDING)
				return eAI_Actions.WAIT;
		}

		return eAI_Actions.POKE;
	}

	public eAI_Actions Pressure(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_statePosition == 0)
		{
			if ((m_statusResult & IntToFlag((int)eAI_InputArray.ENEMY_CLOSE)) != IntToFlag((int)eAI_InputArray.ENEMY_CLOSE))
			{
				MoveToward();
			}
			else
				m_statePosition++;
		}
		else if (m_statePosition == 1)
		{
			UseFastAttack();
			m_statePosition++;
		}
		else if (m_statePosition == 2)
		{
			if (myState == eStates.STANDING)
				return eAI_Actions.WAIT;
		}

		return eAI_Actions.PRESSURE;
	}

	public eAI_Actions JumpIn(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_statePosition <= 5)
		{
			m_input[(int)eInputs.JUMP] = true;

			MoveToward();

			m_statePosition++;
		}
		else
		{
			if (myState == eStates.STANDING)
				return eAI_Actions.WAIT;
		}

		return eAI_Actions.JUMP_IN;
	}

	public eAI_Actions AntiAir(float deltaTime, eStates myState, bool stateChanged)
	{
		if (stateChanged)
		{
			m_input[(int)eInputs.JUMP] = true;
		}
		else if (myState == eStates.STANDING)
		{
			return eAI_Actions.WAIT;
		}

		return eAI_Actions.ANTI_AIR;
	}

	public eAI_Actions Defend(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_stateTime > m_defendTime)
		{
			return eAI_Actions.WAIT;
		}

		m_input[(int)eInputs.BLOCK] = true;

		MoveToward();

		return eAI_Actions.DEFFEND;
	}

	public eAI_Actions FreeAction(float deltaTime, eStates myState, bool stateChanged)
	{
		/*if ((m_StatusInputs & IntToFlag((int)eAI_InputArray.ENEMY_FAR)) != IntToFlag((int)eAI_InputArray.ENEMY_FAR))
			return eAI_Actions.ZONE;

		if ((m_StatusInputs & IntToFlag((int)eAI_InputArray.ENEMY_MID)) != IntToFlag((int)eAI_InputArray.ENEMY_MID))
			return eAI_Actions.POKE;

		if ((m_StatusInputs & IntToFlag((int)eAI_InputArray.ENEMY_CLOSE)) != IntToFlag((int)eAI_InputArray.ENEMY_CLOSE))
			return eAI_Actions.PRESSURE;*/

		//return (eAI_Actions)Random.Range(0, 3);

		//DEBUG_STATE++;

		//if (DEBUG_STATE >= 6)
		//	DEBUG_STATE = -1;

		//return (eAI_Actions)DEBUG_STATE;

		//return eAI_Actions.PRESSURE;
		return (eAI_Actions)Random.Range(0, 6);
	}

	public eAI_Actions Wait(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_stateTime > m_waitingTime)
		{
			return eAI_Actions.FREE;
		}

		return eAI_Actions.WAIT;
	}

	void MoveAway()
	{
		if ((m_statusResult & IntToFlag((int)eAI_InputArray.ENEMY_ON_LEFT)) == IntToFlag((int)eAI_InputArray.ENEMY_ON_LEFT))
			m_input[(int)eInputs.RIGHT] = true;
		else
			m_input[(int)eInputs.LEFT] = true;
	}

	void MoveToward()
	{
		if ((m_statusResult & IntToFlag((int)eAI_InputArray.ENEMY_ON_LEFT)) == IntToFlag((int)eAI_InputArray.ENEMY_ON_LEFT))
			m_input[(int)eInputs.LEFT] = true;
		else
			m_input[(int)eInputs.RIGHT] = true;
	}

	void UseFastAttack()
	{
		if (m_weapon1 == eWeaponType.SABER)
		{
			m_input[(int)eInputs.ATTACK_1] = true;
		}
		else if (m_weapon2 == eWeaponType.SABER)
		{
			m_input[(int)eInputs.ATTACK_2] = true;
		}
		else if (m_weapon1 == eWeaponType.TONFA)
		{
			m_input[(int)eInputs.ATTACK_1] = true;
		}
		else if (m_weapon2 == eWeaponType.TONFA)
		{
			m_input[(int)eInputs.ATTACK_2] = true;
		}
		else
		{
			m_subAction = eAI_SubAction.THROW;
		}
	}

	void UseMidRangeAttack()
	{
		if (m_weapon1 == eWeaponType.TONFA)
		{
			m_input[(int)eInputs.ATTACK_1] = true;
		}
		else if (m_weapon2 == eWeaponType.TONFA)
		{
			m_input[(int)eInputs.ATTACK_2] = true;
		}
		else if (m_weapon1 == eWeaponType.BUSTER)
		{
			m_input[(int)eInputs.ATTACK_1] = true;
		}
		else if (m_weapon2 == eWeaponType.BUSTER)
		{
			m_input[(int)eInputs.ATTACK_2] = true;
		}
		else
		{
			m_subAction = eAI_SubAction.SLIDE;
		}
	}

	void UseRangedAttack()
	{
		if (m_weapon1 == eWeaponType.BUSTER)
		{
			m_input[(int)eInputs.ATTACK_1] = true;
		}
		else if (m_weapon2 == eWeaponType.BUSTER)
		{
			m_input[(int)eInputs.ATTACK_2] = true;
		}
		else if (m_weapon1 == eWeaponType.SHIELD)
		{
			m_input[(int)eInputs.ATTACK_1] = true;
		}
		else if (m_weapon2 == eWeaponType.SHIELD)
		{
			m_input[(int)eInputs.ATTACK_2] = true;
		}
		else
		{
			m_subAction = eAI_SubAction.SLIDE;
		}
	}

	void UseAntiAirAttack()
	{
		if (m_weapon1 == eWeaponType.SHIELD)
		{
			m_input[(int)eInputs.UP] = true;
			m_input[(int)eInputs.ATTACK_1] = true;
		}
		else if (m_weapon2 == eWeaponType.SHIELD)
		{
			m_input[(int)eInputs.UP] = true;
			m_input[(int)eInputs.ATTACK_2] = true;
		}
		else if (m_weapon1 == eWeaponType.SABER)
		{
			m_input[(int)eInputs.ATTACK_1] = true;
		}
		else if (m_weapon2 == eWeaponType.SABER)
		{
			m_input[(int)eInputs.ATTACK_2] = true;
		}
		else
		{
			m_subAction = eAI_SubAction.SLIDE;
		}
	}

	int IntToFlag(int z)
	{
		if (z == 0)
			return 0;

		if (z == 1)
			return 1;

		if (z == 2)
			return 2;

		if (z == 3)
			return 4;

		if (z == 4)
			return 8;

		if (z == 5)
			return 16;

		if (z == 6)
			return 32;

		if (z == 7)
			return 64;

		if (z == 8)
			return 128;

		if (z == 9)
			return 256;

		return 0;
	}

	void ThrowSubAction(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_subStatePosition <= 4)
		{
			m_input[(int)eInputs.BLOCK] = true;

			m_subStatePosition++;
		}
		else if (m_subStatePosition <= 9)
		{
			m_input[(int)eInputs.BLOCK] = true;
			m_input[(int)eInputs.ATTACK_1] = true;

			if (m_subStatePosition == 9)
				m_subAction = eAI_SubAction.NONE;
			else
				m_subStatePosition++;
		}
	}
	
	void SlideSubAction(float deltaTime, eStates myState, bool stateChanged)
	{
		if (m_subStatePosition <= 4)
		{
			m_input[(int)eInputs.DOWN] = true;

			m_subStatePosition++;
		}
		else if (m_subStatePosition <= 9)
		{
			m_input[(int)eInputs.DOWN] = true;
			m_input[(int)eInputs.ATTACK_1] = true;

			if (m_subStatePosition == 9)
				m_subAction = eAI_SubAction.NONE;
			else
				m_subStatePosition++;
		}
	}
}