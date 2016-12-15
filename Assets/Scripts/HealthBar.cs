using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
	public Texture m_texture;
	public Sprite m_sprite;
	public Font m_font;

	static private int m_index = 0;
	private float m_myIndex;
	private int m_hp = 28;

	private float m_xOffset = 10;
	private float m_yOffset = 10;
	private float m_xSpacing = 350;

	private float m_width = 1366;
	private float m_height = 768;

	private static GUIStyle m_style = null;
	
	public void Initialize()
	{
		m_myIndex = m_index;

		m_index++;

		InitializeStyle();
	}

	public void DeInitialize()
	{
		// resets the index count for the next game
		m_index--;
	}

	public void Cycle(int hp)
	{
		m_hp = hp;
	}

	void OnGUI()
	{
		float width = Screen.width / m_width;
		float height = Screen.height / m_height;

		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(width, height, 1));

		float xTotal = m_xOffset + (m_xSpacing * m_myIndex);

		GUI.Box(new Rect(xTotal, m_yOffset, 300, 100), "Health: " + m_hp, m_style);
	}

	void InitializeStyle()
	{
		if (m_style == null)
		{
			// if m_style dosen't exist creates a style
			m_style = new GUIStyle();
			m_style.fontSize = 28;
		}
	}
}
