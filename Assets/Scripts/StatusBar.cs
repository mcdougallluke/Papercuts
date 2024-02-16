using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
	public enum StatusBarType { HEALTH, STAMINA }

	[SerializeField] public StatusBarType type;
	private Slider slider;

	public void Awake() 
	{
		slider = GetComponentInChildren<Slider>();
	}

	public void UpdateStatusBar(float currentVal, float maxVal)
	{
		slider.value = currentVal / maxVal;
	}
}
