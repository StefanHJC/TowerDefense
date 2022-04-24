using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateHealthBar : Bar
{
    [SerializeField] private Obstacle _gate;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _count;

    private void Start()
    {
        _slider.maxValue = _gate.MaxHealth;
        _slider.value = _gate.MaxHealth;
        _count.text = $"{_gate.MaxHealth}/{_gate.MaxHealth}";
    }

    private void OnEnable()
    {
        _gate.HealthChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _gate.HealthChanged-= OnValueChanged;
    }

    private void OnValueChanged()
    {
        _count.text = $"{_gate.Health}/{_gate.MaxHealth}";
        _slider.value = _gate.Health;
    }
}
