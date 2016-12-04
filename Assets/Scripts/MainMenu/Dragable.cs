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

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Vector3 m_positionOffset;
	private Transform m_baseParent = null;
	private Transform m_placeholderParent = null;
	private CanvasGroup m_canvasGroup = null;
	//private GameObject m_placeholder = null;

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
		//m_placeholder = new GameObject();
		//m_placeholder.transform.SetParent(this.transform.parent);
		//LayoutElement layout = m_placeholder.AddComponent<LayoutElement>();
		//layout.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		//layout.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		//layout.flexibleHeight = 0;
		//layout.flexibleWidth = 0;

		//m_placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
		
		m_positionOffset = this.transform.position - (Vector3)data.position;

		m_baseParent = this.transform.parent;
		m_placeholderParent = m_baseParent;
		this.transform.SetParent(this.transform.parent.parent);
		m_canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData data)
	{
		this.transform.position = (Vector3)data.position + m_positionOffset;

		//if (m_placeholder.transform.parent != m_placeholderParent)
			//m_placeholder.transform.SetParent(m_placeholderParent);

		//int placeholderIndex = m_placeholderParent.childCount;
		//
		//for (int z = 0; z < m_placeholderParent.childCount; z++)
		//{
		//	if (this.transform.position.x < m_placeholderParent.GetChild(z).position.x)
		//	{
		//		//m_placeholder.transform.SetSiblingIndex(z);
		//		placeholderIndex = z;
		//
		//		//if (m_placeholder.transform.GetSiblingIndex() < placeholderIndex)
		//			placeholderIndex--;
		//
		//		break;
		//	}
		//}

		//m_placeholder.transform.SetSiblingIndex(placeholderIndex);
	}

	public void OnEndDrag(PointerEventData data)
	{
		this.transform.SetParent(m_baseParent);
		m_canvasGroup.blocksRaycasts = true;

		//this.transform.SetSiblingIndex(m_placeholder.transform.GetSiblingIndex());

		//Destroy(m_placeholder);
		//m_placeholder = null;
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
