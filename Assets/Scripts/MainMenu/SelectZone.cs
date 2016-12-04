using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectZone : MonoBehaviour, IPointerClickHandler
{
	private bool m_clicked = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	/*void Update()
	{

	}*/

	void OnMouseDown()
	{
		m_clicked = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		m_clicked = true;
	}

	public bool GetClicked()
	{
		return m_clicked;
	}
}
