using UnityEngine;
using System.Collections;

// this class is the basses for all the states the zero can move,
// it defines all the functions that the derived states need acsess to
public class BaseState : MonoBehaviour
{
	protected Charicter m_me;
    
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
	}
}
