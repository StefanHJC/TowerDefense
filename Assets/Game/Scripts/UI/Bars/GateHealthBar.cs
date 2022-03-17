using TMPro;
using UnityEngine;

public class GateHealthBar : Bar
{
    [SerializeField] private Gate _gate;

    private TMP_Text _count;

    private void Start()
    {
        _count = GetComponent<TMP_Text>();
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
        _count.text = _gate.Health.ToString();
    }
}
