using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MissedMonstersBar : Bar
{
    [SerializeField] private CityEnter _cityEnter;

    private TMP_Text _count;

    private void Start()
    {
        _count = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _cityEnter.MonsterMissed += OnValueChanged;
    }

    private void OnDisable()
    {
        _cityEnter.MonsterMissed -= OnValueChanged;
    }

    private void OnValueChanged()
    {
        _count.text = _cityEnter.MissedMonstersCount.ToString();
    }
}
