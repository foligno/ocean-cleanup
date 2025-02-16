using OceanCleanup.SuckerCat.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace OceanCleanup.SuckerCat.UI
{
    [RequireComponent(typeof(Slider))]
    public class EnergyUpdater : MonoBehaviour
    {
        private Slider _slider;
    
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            SuckerCatControls.OnFuelChange += UpdateSlider;
        }

        private void OnDisable()
        {
            SuckerCatControls.OnFuelChange -= UpdateSlider;
        }

        private void UpdateSlider(float newValue)
        {
            _slider.value = newValue;
        }
    }
}
