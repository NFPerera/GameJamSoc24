using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TDHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scrapAmountText;
    [SerializeField] private TextMeshProUGUI _waveAmountText;
    // Start is called before the first frame update
    void Start()
    {
        if (_scrapAmountText != null)
        {
            _scrapAmountText.text = "0";
        }

        if (_waveAmountText != null)
        {
            _waveAmountText.text = "0";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScrapAmount(int amount)
    {
        _scrapAmountText.text = amount.ToString();
    }

    public void UpdateWaveAmount(int amount)
    {
        _waveAmountText.text = amount.ToString();
    }

}
