using UnityEngine;

public class TapManager : MonoBehaviour
{
    private ButtonArray buttonArray;
    private StepSystem stepSystem;
    private UIManager UIManager;

    void Awake()
    {
        buttonArray = FindObjectOfType<ButtonArray>();
        stepSystem = FindObjectOfType<StepSystem>();
        UIManager = FindObjectOfType<UIManager>();
    }

    public void OnClicked(Button button)
    { 
        var (x, y) = button.GetCoordinates; 
        buttonArray.SetValue(x, y); 
        button.TurnOnImage();
        button.Block();
        stepSystem.NextTurn();
        UIManager.TurnOnStepText();
    }
}
