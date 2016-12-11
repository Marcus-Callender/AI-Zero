using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public SelectZone m_startButton;

	private PlayerZone[] m_playerZones;
	
	void Start()
	{
		// retreves all the player zones through code
		m_playerZones = GetComponentsInChildren<PlayerZone>();
	}
	
	void Update()
	{
		// if the start button gets clicked
		if (m_startButton.GetClicked())
		{
			// tells the player zones to give persistant data the selcected weapons
			for (int z = 0; z < m_playerZones.Length; z++)
			{
				m_playerZones[z].OnPlay();
			}

			// loads the game
			SceneManager.LoadScene("TestMM", LoadSceneMode.Single);
		}
	}
}
