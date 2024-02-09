using TMPro;
using UnityEngine;

public class VisibleTextPopup : MonoBehaviour
{
	[SerializeField] private TMP_Text popupCaption;

	public void PopupText(string caption)
	{
		gameObject.SetActive(false);
		popupCaption.text = caption;
		gameObject.SetActive(true);
	}

	public void OnAnimationEnd()
	{
		gameObject.SetActive(false);
	}
}
