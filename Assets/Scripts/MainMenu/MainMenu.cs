using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public SelectZone m_startButton;

	private PlayerZone[] m_playerZones;

	// Use this for initialization
	void Start()
	{
		m_playerZones = GetComponentsInChildren<PlayerZone>();
	}

	// Update is called once per frame
	void Update()
	{
		if (m_startButton.GetClicked())
		{
			for (int z = 0; z < m_playerZones.Length; z++)
			{
				m_playerZones[z].OnPlay();
			}

			SceneManager.LoadScene("TestMM", LoadSceneMode.Single);
		}
	}
}
