using UnityEngine;
using System.Collections;

public class PlayerInput : BaseInputHandler
{
	private float[] m_inputBuffers = new float[(int)eInputs.SIZE_OF_E_INPUTS];
	private bool[] m_bufferInUse = new bool[(int)eInputs.SIZE_OF_E_INPUTS];
	private float m_bufferTime = 6.0f / 60.0f;
	
	public override void Initialize()
	{
		for (int z = 5; z < (int)eInputs.SIZE_OF_E_INPUTS; z++)
		{
			m_inputBuffers[z] = 0.0f;
			m_bufferInUse[z] = false;
		}
	}
	
	public override void Cycle(float deltaTime, eStates myState)
	{
		for (int z = 5; z < (int)eInputs.SIZE_OF_E_INPUTS; z++)
		{
			m_inputBuffers[z] -= deltaTime;
		}
	}

	public override void ReceveStatus(Charicter opponent, Charicter me, float deltaTime)
	{

	}

	public override void Inputs(ref bool[] inputs)
	{
		if (Input.GetAxisRaw("Horizontal") < -0.5f)
			inputs[(int)eInputs.LEFT] = true;

		if (Input.GetAxisRaw("Horizontal") > 0.5f)
			inputs[(int)eInputs.RIGHT] = true;

		if (Input.GetAxisRaw("Vertical") < -0.5f)
			inputs[(int)eInputs.DOWN] = true;

		if (Input.GetAxisRaw("Vertical") > 0.5f)
			inputs[(int)eInputs.UP] = true;

		if (Input.GetButton("Block"))
			inputs[(int)eInputs.BLOCK] = true;

		/*if (Input.GetButton("Jump"))
			inputs[(int)eInputs.JUMP] = true;

		if (Input.GetButton("Attack1"))
			inputs[(int)eInputs.ATTACK_1] = true;

		if (Input.GetButton("Attack2"))
			inputs[(int)eInputs.ATTACK_2] = true;*/

		//BufferInput((int)eInputs.LEFT, Input.GetAxisRaw("Horizontal") < -0.5f);
		//BufferInput((int)eInputs.RIGHT, Input.GetAxisRaw("Horizontal") > -0.5f);
		//BufferInput((int)eInputs.DOWN, Input.GetAxisRaw("Vertical") < -0.5f);
		//BufferInput((int)eInputs.UP, Input.GetAxisRaw("Vertical") > -0.5f);

		//BufferInput((int)eInputs.BLOCK, Input.GetButton("Block"));
		BufferInput((int)eInputs.JUMP, Input.GetButton("Jump"));
		BufferInput((int)eInputs.ATTACK_1, Input.GetButton("Attack1"));
		BufferInput((int)eInputs.ATTACK_2, Input.GetButton("Attack2"));

		SetInputs(ref inputs);
	}

	private void BufferInput(int id, bool receved)
	{
		if (!m_bufferInUse[id])
		{
			m_bufferInUse[id] = true;
			m_inputBuffers[id] = m_bufferTime;
		}

		if (!receved)
			m_bufferInUse[id] = false;
	}

	private void SetInputs(ref bool[] inputs)
	{
		for (int z = 5; z < (int)eInputs.SIZE_OF_E_INPUTS; z++)
		{
			if (m_bufferInUse[z] && (m_inputBuffers[z] > 0.0f))
				inputs[z] = true;
			else
				inputs[z] = false;
		}
	}
}
