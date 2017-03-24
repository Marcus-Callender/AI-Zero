using UnityEngine;
using System.Collections;

// this defines the methods needed for the AI and player input handlers
public class BaseInputHandler : MonoBehaviour
{
	public virtual void Initialize()
	{

	}
	
	public virtual void Cycle(float deltaTime, eStates myState)
	{

	}

	public virtual void ReceveStatus(Charicter opponent, Charicter me, float deltaTime)
	{

	}

	public virtual void Inputs(ref bool[] inputs)
	{

	}

	public virtual void GiveWeapons(eWeaponType weapon1, eWeaponType weapoon2)
	{

	}
}
