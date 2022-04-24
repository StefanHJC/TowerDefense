using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MissedMonstersBar : Bar
{
    [SerializeField] private CityEnter _cityEnter;
    [SerializeField] private LevelSettings _levelSettings;

    private TMP_Text _count;

    private void Start()
    {
        _count = GetComponent<TMP_Text>();
        _count.text = $"0/{_levelSettings.NeedMissForLose}";
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
        _count.text = $"{_cityEnter.MissedMonstersCount}/{_levelSettings.NeedMissForLose}";
    }
}
