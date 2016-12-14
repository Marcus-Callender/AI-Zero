using UnityEngine;
using System.Collections;

// a list of states the charicter can be in
public enum eStates
{
	STANDING,
	WALKING,
	JUMPING,
	PRE_POST_JUMP,
	FALLING,

	SLIDING,
	BLOCK,

	ATTACK_1,
	ATTACK_2,
	THROW,

	HIT_STUN,
	AIR_HIT_STUN,

	SPAWNING,
	KO_D,

	SIZE_OF_E_STATES
}

// a list of posible inputs
public enum eInputs
{
	UP,
	DOWN,
	LEFT,
	RIGHT,

	BLOCK,
	JUMP,
	ATTACK_1,
	ATTACK_2,

	SIZE_OF_E_INPUTS
}

// a list of the diffrent types of attack
public enum eAttackType
{
	STRIKE,
	PROJECTILE,
	THROW
}

public class State_Machine : MonoBehaviour
{
	public Sprite[] m_sprites;
	private BaseState[] m_stateClass = new BaseState[(int)(eStates.SIZE_OF_E_STATES)];

	public AnimClass m_animations;
	private SpriteRenderer m_sprite;
	Charicter m_me;

	public eStates m_newState;
	public eStates m_currentState;

	private BaseAttack m_attack1;
	private BaseAttack m_attack2;

	private bool[] m_inputs = new bool[(int)eInputs.SIZE_OF_E_INPUTS];
	public BaseInputHandler m_inputHandler;

	private eWeaponType m_weaponType1 = eWeaponType.NONE;
	private eWeaponType m_weaponType2 = eWeaponType.NONE;
	
	public void Initialize()
	{
		m_me = GetComponent<Charicter>();
		m_sprite = GetComponent<SpriteRenderer>();
		m_inputHandler = GetComponent<BaseInputHandler>();
		m_inputHandler.Initialize();

		// tells the input handler what weapons it will be using
		m_inputHandler.GiveWeapons(m_weaponType1, m_weaponType2);

		// retreves the data for the weapons given to the chariceter
		BuildWeapons();

		// retreves refrences to the scripts for teh diffrent states
		m_stateClass[(int)eStates.STANDING] = GetComponent<StateStand>();
		m_stateClass[(int)eStates.WALKING] = GetComponent<StateWalk>();
		m_stateClass[(int)eStates.JUMPING] = GetComponent<StateJump>();
		m_stateClass[(int)eStates.PRE_POST_JUMP] = GetComponent<StatePrePostJump>();
		m_stateClass[(int)eStates.FALLING] = GetComponent<StateFall>();
		m_stateClass[(int)eStates.SLIDING] = GetComponent<StateSlide>();
		m_stateClass[(int)eStates.BLOCK] = GetComponent<StateGuard>();

		// retreves the scripts for the given weapons from a diffrent scripts retreved from BuildWeapons()
		m_stateClass[(int)eStates.ATTACK_1] = m_attack1.InitializeState();
		m_stateClass[(int)eStates.ATTACK_2] = m_attack2.InitializeState();

		m_stateClass[(int)eStates.THROW] = GetComponent<StateThrow>();
		m_stateClass[(int)eStates.HIT_STUN] = GetComponent<StateHitGround>();
		m_stateClass[(int)eStates.AIR_HIT_STUN] = GetComponent<StateHitAir>();
		m_stateClass[(int)eStates.SPAWNING] = GetComponent<StateSpawn>();
		m_stateClass[(int)eStates.KO_D] = GetComponent<StateKO>();

		m_newState = eStates.SPAWNING;
		m_currentState = eStates.STANDING;

		// initializes the first state as spawning
		m_stateClass[(int)m_newState].Enter(m_currentState);

		// initializes all the states and gives them a refrence to this charicters data
		for (int z = 0; z < (int)eStates.SIZE_OF_E_STATES; z++)
		{
			m_stateClass[z].Initialize(m_me);
		}

		m_animations = GetComponent<AnimClass>();
		m_animations.F_initialize(m_sprite);

		// creates the animations needed for the diffrent states
		m_animations.addAnim(); // STAND
		m_animations.addKeyFrame((int)eStates.STANDING, m_sprites[1], 0.01f);
		m_animations.animRepeat((int)eStates.STANDING);

		m_animations.addAnim(); // WALKING
		m_animations.addKeyFrame((int)eStates.WALKING, m_sprites[4], 0.15f);
		m_animations.addKeyFrame((int)eStates.WALKING, m_sprites[5], 0.15f);
		m_animations.addKeyFrame((int)eStates.WALKING, m_sprites[6], 0.15f);
		m_animations.addKeyFrame((int)eStates.WALKING, m_sprites[5], 0.15f);
		m_animations.animRepeat((int)eStates.WALKING);

		m_animations.addAnim(); // JUMPING
		m_animations.addKeyFrame((int)eStates.JUMPING, m_sprites[7], 0.01f);
		m_animations.animRepeat((int)eStates.JUMPING);

		m_animations.addAnim(); // PRE_POST_JUMP
		m_animations.addKeyFrame((int)eStates.PRE_POST_JUMP, m_sprites[1], 0.01f);
		m_animations.animRepeat((int)eStates.PRE_POST_JUMP);

		m_animations.addAnim(); // FALLING
		m_animations.addKeyFrame((int)eStates.FALLING, m_sprites[7], 0.01f);
		m_animations.animRepeat((int)eStates.FALLING);

		m_animations.addAnim(); // SLIDING
		m_animations.addKeyFrame((int)eStates.SLIDING, m_sprites[3], 0.16f);
		m_animations.addKeyFrame((int)eStates.SLIDING, m_sprites[26], 0.5f);
		m_animations.addKeyFrame((int)eStates.SLIDING, m_sprites[3], 0.16f);

		m_animations.addAnim(); // BLOCK
		m_animations.addKeyFrame((int)eStates.BLOCK, m_sprites[51], 0.01f);
		m_animations.animRepeat((int)eStates.BLOCK);

		// gets the animaton from the selected weapons
		m_attack1.InitializeAnimation(m_animations, m_sprites, (int)eStates.ATTACK_1);
		m_attack2.InitializeAnimation(m_animations, m_sprites, (int)eStates.ATTACK_2);

		m_animations.addAnim(); // THROW
		m_animations.addKeyFrame((int)eStates.THROW, m_sprites[3], 5.0f / 60.0f);
		m_animations.addKeyFrame((int)eStates.THROW, m_sprites[27], 8.0f / 60.0f);
		m_animations.addKeyFrame((int)eStates.THROW, m_sprites[18], 12.0f / 60.0f);

		m_animations.addAnim(); // HIT_STUN
		m_animations.addKeyFrame((int)eStates.HIT_STUN, m_sprites[20], 0.01f);
		m_animations.animRepeat((int)eStates.HIT_STUN);

		m_animations.addAnim(); // AIR_HIT_STUN
		m_animations.addKeyFrame((int)eStates.AIR_HIT_STUN, m_sprites[21], 0.01f);
		m_animations.animRepeat((int)eStates.AIR_HIT_STUN);

		m_animations.addAnim(); // SPAWNING
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[0], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[0], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[10], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[11], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[1], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[2], 0.25f);

		m_animations.addAnim(); // KO_D
		m_animations.addKeyFrame((int)eStates.KO_D, m_sprites[28], 0.01f);
		m_animations.animRepeat((int)eStates.KO_D);

		m_animations.F_play();

		ResetInputs();
		m_me.Initialize(m_sprite);
	}
	
	public void Cycle(float deltaTime)
	{
		// sets all the inuts to null
		ResetInputs();
		StateCheck();

		// gets inputs from the user
		m_inputHandler.Cycle(deltaTime, m_newState);
		m_inputHandler.Inputs(ref m_inputs);

		m_stateClass[(int)m_newState].Input(m_inputs, ref m_newState);
		StateCheck();

		m_stateClass[(int)m_newState].Cycle(deltaTime, ref m_newState);
		StateCheck();

		// checks and updates the inputs
		m_animations.setAnim((int)m_currentState);
		m_animations.F_update(deltaTime);

		m_me.F_Update(deltaTime);
	}

	public void GiveInputHandlerStatus(Charicter opponent, Charicter me, float deltaTime)
	{
		// gives the input handler the status of the battle
		m_inputHandler.ReceveStatus(opponent, me, deltaTime);
	}

	public void AssignWeapons(eWeaponType weapon1, eWeaponType weapon2)
	{
		m_weaponType1 = weapon1;
		m_weaponType2 = weapon2;
	}

	private void BuildWeapons()
	{
		bool weapon1NotAssigned = (m_weaponType1 == eWeaponType.NONE);
		bool weapon2NotAssigned = (m_weaponType2 == eWeaponType.NONE);

		do
		{
			// if the weapons aren't assigned, give random weapons
			if (weapon1NotAssigned)
			{
				m_weaponType1 = (eWeaponType)Random.Range(0, 4);
			}

			if (weapon2NotAssigned)
			{
				m_weaponType2 = (eWeaponType)Random.Range(0, 4);
			}
		}
		while (m_weaponType1 == m_weaponType2);

		// gets the componats needed for the weapons
		if (m_weaponType1 == eWeaponType.BUSTER)
			m_attack1 = GetComponent<BusterAttack>();

		else if (m_weaponType1 == eWeaponType.SABER)
			m_attack1 = GetComponent<SaberAttack>();

		else if (m_weaponType1 == eWeaponType.SHIELD)
			m_attack1 = GetComponent<ShieldAttack>();

		else if (m_weaponType1 == eWeaponType.TONFA)
			m_attack1 = GetComponent<TonfaAttack>();

		if (m_weaponType2 == eWeaponType.BUSTER)
			m_attack2 = GetComponent<BusterAttack>();

		else if (m_weaponType2 == eWeaponType.SABER)
			m_attack2 = GetComponent<SaberAttack>();

		else if (m_weaponType2 == eWeaponType.SHIELD)
			m_attack2 = GetComponent<ShieldAttack>();

		else if (m_weaponType2 == eWeaponType.TONFA)
			m_attack2 = GetComponent<TonfaAttack>();
	}

	public void Physics(float deltaTime)
	{
		m_me.Physics(deltaTime);
	}

	void StateCheck()
	{
		if (m_me.WasHit())
		{
			// if the charicter was hit

			Debug.Log("Hit detected");

			m_me.ResetWasHit();

			// changes the state to being hit
			m_newState = eStates.AIR_HIT_STUN;

			m_stateClass[(int)m_currentState].Exit();
			m_stateClass[(int)m_newState].Enter(m_currentState);

			m_currentState = m_newState;
		}
		else
		{
			if (m_newState != m_currentState)
			{
				// if the state has been changed

				m_stateClass[(int)m_currentState].Exit();
				m_stateClass[(int)m_newState].Enter(m_currentState);

				m_currentState = m_newState;
			}
		}
	}

	void ResetInputs()
	{
		// sets all the inputs to false
		for (int z = 0; z < (int)eInputs.SIZE_OF_E_INPUTS; z++)
		{
			m_inputs[z] = false;
		}
	}

	public void colideHorizontal()
	{
		m_stateClass[(int)m_newState].CollideHorizontal(ref m_newState);
		m_me.colideHorizontal();
	}

	public void colideHorizontal(float velocity)
	{
		m_stateClass[(int)m_newState].CollideHorizontal(ref m_newState);
		m_me.colideHorizontal(velocity);
	}

	public void collideVertical(float top)
	{
		m_stateClass[(int)m_newState].CollideVertical(ref m_newState);
		m_me.collideVertical(top);
	}

	public void notColideHorizontal()
	{
		m_stateClass[(int)m_newState].NotCollideHorizontal(ref m_newState);
	}

	public void notCollideVertical()
	{
		m_stateClass[(int)m_newState].NotCollideVertical(ref m_newState);
	}

	public bool isInAir()
	{
		return (m_newState == eStates.FALLING);
	}

	public bool IsFalling()
	{
		if ((m_newState == eStates.FALLING) && (m_me.GetYVelocity() < 0.0f))
			return true;

		return false;
	}
}
