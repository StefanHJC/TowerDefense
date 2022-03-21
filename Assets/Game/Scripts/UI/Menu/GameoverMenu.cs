using TMPro;
using UnityEngine;

public class GameoverMenu : Menu
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _finalMessage;
    [SerializeField] private string _onWinMessage;
    [SerializeField] private string _onLoseMessage;

    private string _finalStats;

    private void Start()
    {
        _finalStats = $"\n Всего золота накоплено: {_player.GoldCollected} \n Всего монстров убито: {_player.MonstersKilled}";
    }

    public void OnGameFinished(bool gameWon)
    {
        if (gameWon)
            _finalMessage.text = _onWinMessage + _finalStats;
        else
            _finalMessage.text = _onLoseMessage + _finalStats;
    }
}
