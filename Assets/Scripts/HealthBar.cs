using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Slider slider;

	public void UpdateHealthBar(float currentVal, float maxVal)
	{
		slider.value = currentVal / maxVal;
	}
}
