using UnityEngine;
using System.Collections;

// this is used to carry the weapon data between the weapon selection screen and the main game
public class PersistantData : MonoBehaviour
{
	public PersistantData m_instance = null;
	private int m_winner = -1;
	//private int m_numPlayers = 0;
	private eWeaponType[,] m_SelectedWeapons = new eWeaponType[2, 2];
	
	void Start()
	{
		// makes it so there can only be one instance of persistant data running at a time
		if (m_instance == null)
			m_instance = this;
		else
			Destroy(this);

		// ensures this object will not be deleted by unity when changing scenes or reloading the current scene
		DontDestroyOnLoad(this);

		// sets all the selected weapons to none
		for (int z = 0; z < 2; z++)
		{
			for (int x = 0; x < 2; x++)
			{
				m_SelectedWeapons[z, x] = eWeaponType.NONE;
			}
		}
	}

	void SetWinner(int winner)
	{
		m_winner = winner;
	}

	int GetWinner()
	{
		return m_winner;
	}

	public void SetWeapons(int player, eWeaponType weapon1, eWeaponType weapon2)
	{
		// stores the selected weapons to be retreved later in a diffrent scene
		if (player >= 0 && player < 2)
		{
			m_SelectedWeapons[player, 0] = weapon1;
			m_SelectedWeapons[player, 1] = weapon2;
		}
	}

	public eWeaponType GetWeapons(int player, int weaponType)
	{
		// retreves the required weapon
		return m_SelectedWeapons[player, weaponType];
	}
}
