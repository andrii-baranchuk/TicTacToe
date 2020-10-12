using Assets.Scripts.GameMechanic.Models;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;
    [SerializeField]
    private Image Tic;
    [SerializeField]
    private Image Tac;

    private ButtonArray buttonArray;
    private UnityEngine.UI.Button button;

    void Awake()
    {
        buttonArray = FindObjectOfType<ButtonArray>();
        button = GetComponent<UnityEngine.UI.Button>();
    }

    public (int X, int Y) GetCoordinates => (x, y);

    public void TurnOnImage()
    {
        
        Tic.enabled = buttonArray.buttonStatusArray[x, y] == (int)Player.Tic;
        Tac.enabled = buttonArray.buttonStatusArray[x, y] == (int)Player.Tac;
    }

    public void Block()
    {
        button.enabled = false;
    }
}
