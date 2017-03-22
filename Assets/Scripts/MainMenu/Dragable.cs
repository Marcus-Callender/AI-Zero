using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum eWeaponType
{
	BUSTER,
	SABER,
	TONFA,
	SHIELD,
	NONE
}

// this script is attached to the weapon icons that represent the equipable weapons on the menu
public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Vector3 m_positionOffset;
	private Transform m_baseParent = null;
	private Transform m_placeholderParent = null;
	private CanvasGroup m_canvasGroup = null;

	public eWeaponType m_myType;

	void Start()
	{
		//m_transform = GetComponent<Transform>();
		m_baseParent = this.transform.parent;
		m_placeholderParent = m_baseParent;
		m_canvasGroup = GetComponent<CanvasGroup>();
	}

	public void OnBeginDrag(PointerEventData data)
	{
		m_positionOffset = this.transform.position - (Vector3)data.position;

		m_baseParent = this.transform.parent;
		m_placeholderParent = m_baseParent;
		this.transform.SetParent(this.transform.parent.parent);
		m_canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData data)
	{
		this.transform.position = (Vector3)data.position + m_positionOffset;
	}

	public void OnEndDrag(PointerEventData data)
	{
		this.transform.SetParent(m_baseParent);
		m_canvasGroup.blocksRaycasts = true;
	}

	public void SetBaseParent(Transform newParent)
	{
		m_baseParent = newParent;
	}

	public void SoftSetBaseParent(Transform newParent)
	{
		if (m_baseParent == null)
			m_baseParent = newParent;
	}

	public Transform GetBaseParent()
	{
		return m_baseParent;
	}

	public void SetPlaceholderParent(Transform newParent)
	{
		m_baseParent = newParent;
	}

	public Transform GetPlaceholderParent()
	{
		return m_placeholderParent;
	}
}
