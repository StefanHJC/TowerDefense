using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class GoldBar : Bar
{
    [SerializeField] private Player _player;

    private TMP_Text _count;

    private void Start()
    {
        _count = GetComponent<TMP_Text>();
        _count.text = _player.Gold.ToString();
    }

    private void OnEnable()
    {
        _player.GoldAmountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.GoldAmountChanged -= OnValueChanged;
    }

    private void OnValueChanged()
    {
        _count.text = _player.Gold.ToString();
    }
}
