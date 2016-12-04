using UnityEngine;
using System.Collections;

public class PersistantData : MonoBehaviour
{
	public PersistantData m_instance = null;
	private int m_winner = -1;
	//private int m_numPlayers = 0;
	private eWeaponType[,] m_SelectedWeapons = new eWeaponType[2, 2];
	
	// Use this for initialization
	void Start()
	{
		if (m_instance == null)
			m_instance = this;
		else
			Destroy(this);

		DontDestroyOnLoad(this);

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
		if (player >= 0 && player < 2)
		{
			m_SelectedWeapons[player, 0] = weapon1;
			m_SelectedWeapons[player, 1] = weapon2;
		}
	}

	public eWeaponType GetWeapons(int player, int weaponType)
	{
		return m_SelectedWeapons[player, weaponType];
	}
}
