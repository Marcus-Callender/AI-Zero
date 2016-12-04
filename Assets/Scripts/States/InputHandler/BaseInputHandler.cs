using UnityEngine;
using System.Collections;

public class BaseInputHandler : MonoBehaviour
{

	// Use this for initialization
	public virtual void Initialize()
	{

	}

	// Update is called once per frame
	public virtual void Cycle(float deltaTime, eStates myState)
	{

	}

	public virtual void ReceveStatus(Charicter opponent, Charicter me, float deltaTime)
	{

	}

	public virtual void Inputs(ref bool[] inputs)
	{
		//if (Input.GetAxisRaw("Horizontal") < -0.5f)
		//	inputs[(int)eInputs.LEFT] = true;

		//if (Input.GetAxisRaw("Horizontal") > 0.5f)
		//	inputs[(int)eInputs.RIGHT] = true;
	}

	public virtual void GiveWeapons(eWeaponType weapon1, eWeaponType weapoon2)
	{

	}
}
