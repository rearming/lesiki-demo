using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ChestOpener : MonoBehaviour
	{
		[SerializeField]
		private Image closedChest;

		[SerializeField]
		private Image openedChest;

		private const string ChestOpenedKey = "Chest Opened";
		
		private void Start()
		{
			OpenChestIfWasOpened();
		}

		private void OpenChestIfWasOpened()
		{
			if (PlayerPrefs.HasKey(ChestOpenedKey))
				OpenChest();
		}

		public void Open()
		{
			if (PlayerPrefs.HasKey(ChestOpenedKey))
				return;
			PlayerPrefs.SetInt(ChestOpenedKey, 42);
			OpenChest();
		}

		private void OpenChest()
		{
			closedChest.enabled = false;
			openedChest.enabled = true;
		}

		[Button]
		private void ClearPlayerPref()
		{
			PlayerPrefs.DeleteKey(ChestOpenedKey);
		}
	}
}