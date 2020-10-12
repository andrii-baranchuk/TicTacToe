using Assets.Scripts.GameMechanic.Models;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text ticWinText;

    [SerializeField] private Text tacWinText;

    [SerializeField] private Text tieText;

    [SerializeField] private GameObject map;

    [SerializeField] private Text ticTurnText;
    [SerializeField] private Text tacTurnText;

    private StepSystem stepSystem;

    private void Awake()
    {
        stepSystem = FindObjectOfType<StepSystem>();
        TurnOnStepText();
    }

    public void TurnOnGameResultPopUp(Player winner)
    {
        tieText.enabled = winner == Player.None;
        ticWinText.enabled = winner == Player.Tic;
        tacWinText.enabled = winner == Player.Tac;

        map.GetComponent<Animator>().SetTrigger("GameOverTrigger");
    }

    public void TurnOnStepText()
    {
        bool turn = stepSystem.GetTurn();

        if (turn)
        {
            ticTurnText.enabled = true;
            tacTurnText.enabled = false;
        }
        else
        {
            ticTurnText.enabled = false;
            tacTurnText.enabled = true;
        }
    }
}
