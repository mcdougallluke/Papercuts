using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PaperCount : MonoBehaviour
{
    public AutoProximityPickUp playerPickUpScript;
    public FaxMachine faxMachineScript;
    public Text paperCountText;

    private void Start()
    {
        UpdatePaperCountUI();
    }

    private void Update()
    {
        UpdatePaperCountUI();
    }

    void UpdatePaperCountUI()
    {
        if (playerPickUpScript != null && faxMachineScript != null && paperCountText != null)
        {
            paperCountText.text = string.Format("{0}/{1}", playerPickUpScript.paperCount, faxMachineScript.paperThreshold);
        }
    }
}
