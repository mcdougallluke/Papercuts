using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
	public enum StatusBarType { HEALTH, STAMINA }

	[SerializeField] private Slider slider;
	[SerializeField] public StatusBarType type;

	private void Awake() 
	{
		slider = GetComponentInChildren<Slider>();
	}

	public void UpdateStatusBar(float currentVal, float maxVal)
	{
		slider.value = currentVal / maxVal;
	}
}
