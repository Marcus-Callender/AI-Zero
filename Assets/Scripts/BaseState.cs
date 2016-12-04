using UnityEngine;
using System.Collections;

public class BaseState : MonoBehaviour
{
	protected Charicter m_me;

	// Use this for initialization
	public virtual void Initialize(Charicter me)
	{
		m_me = me;
	}

	public virtual void Enter(eStates m_priviousState)
	{
	}

	public virtual void Exit()
	{
	}

	// Update is called once per frame
	public virtual void Cycle(float deltaTime, ref eStates m_currentState)
	{
	}

	public virtual void Input(bool[] inputs, ref eStates m_currentState)
	{
	}

	public virtual void CollideHorizontal(ref eStates m_currentState)
	{
	}

	public virtual void CollideVertical(ref eStates m_currentState)
	{
	}

	public virtual void NotCollideHorizontal(ref eStates m_currentState)
	{
	}

	public virtual void NotCollideVertical(ref eStates m_currentState)
	{
		//m_currentState = eStates.FALLING;
	}
}
