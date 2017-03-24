using UnityEngine;
using System.Collections;

public class PlayerInput : BaseInputHandler
{
    // this is what keeps track of how much longer a button will be counted as just being pressed
	private float[] m_inputBuffers = new float[(int)eInputs.UP];

    // this is weather the game is reading a button as just being pressed
    private bool[] m_bufferInUse = new bool[(int)eInputs.UP];
    
    // this is how long the game will read a button as just having been pressed
	private float m_bufferTime = 6.0f / 60.0f;
	
	public override void Initialize()
	{
		// initializes the values used
		for (int z = 0; z < (int)eInputs.UP; z++)
		{
			m_inputBuffers[z] = 0.0f;
			m_bufferInUse[z] = false;
		}
	}
	
	public override void Cycle(float deltaTime, eStates myState)
	{
		// reduces the indvidual timers
		for (int z = 0; z < (int)eInputs.UP; z++)
		{
			m_inputBuffers[z] -= deltaTime;
		}
	}

	public override void ReceveStatus(Charicter opponent, Charicter me, float deltaTime)
	{
        // this function is only neccacery in AI input
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

		BufferInput((int)eInputs.JUMP, Input.GetButton("Jump"));
		BufferInput((int)eInputs.ATTACK_1, Input.GetButton("Attack1"));
		BufferInput((int)eInputs.ATTACK_2, Input.GetButton("Attack2"));

		SetInputs(ref inputs);
	}

	private void BufferInput(int id, bool receved)
	{
		if (!m_bufferInUse[id])
		{
			// if ther buffer isn't already in use set it to in use and start the input countdown
			m_bufferInUse[id] = true;
			m_inputBuffers[id] = m_bufferTime;
		}

		if (!receved)
			m_bufferInUse[id] = false;
	}

	private void SetInputs(ref bool[] inputs)
	{
		for (int z = 0; z < (int)eInputs.UP; z++)
		{
			// while the countown from pressing the button is still above 0 the button counts as just being pressed

			if (m_bufferInUse[z] && (m_inputBuffers[z] > 0.0f))
				inputs[z] = true;
			else
				inputs[z] = false;
		}
	}
}
