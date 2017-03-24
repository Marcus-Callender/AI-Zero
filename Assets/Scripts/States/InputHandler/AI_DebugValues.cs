using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DebugValues : MonoBehaviour
{
	public Texture m_texture;
	public Sprite m_sprite;
	public Font m_font;

	static private int m_index = 0;
	private float m_myIndex;

	private float m_xOffset = 10;
	private float m_yOffset = 100;
	private float m_xSpacing = 350;

	private float m_width = 1366;
	private float m_height = 768;

	private static GUIStyle m_style = null;

	private string m_text;
	
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
	
	public void Cycle(eAI_Actions act, int actFlag)
	{
		m_text = "";

		m_text += "Action: ";

		m_text += act;

		m_text += "\nFlags: ";

		m_text += actFlag;
	}

	void OnGUI()
	{
		float width = Screen.width / m_width;
		float height = Screen.height / m_height;

		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(width, height, 1));

		float xTotal = m_xOffset + (m_xSpacing * m_myIndex);

		GUI.Box(new Rect(xTotal, m_yOffset, 300, 100), m_text, m_style);
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
