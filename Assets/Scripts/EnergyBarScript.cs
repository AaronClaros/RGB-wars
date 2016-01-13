using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour {

    Slider energiBar;
	// Use this for initialization
	void Start () {
        energiBar = GetComponent<Slider>();
        energiBar.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void IncreaseEnergy(float count) {
        if (energiBar.value + (count) <= 1)
        {
            energiBar.value += (count);
        }
        else {
            energiBar.value = 1;
        }
    }

    public void DecreaseEnergy(float count)
    {
        if (energiBar.value - (count / 100) >= 0)
        {
            energiBar.value -= (count / 100);
        }
        else {
            energiBar.value = 0;
        }
    }

    public float GetEnergyValue() {
        return energiBar.value;
    }

    public void SetEnergyValue(float value){
        energiBar.value = value;
    }
}
