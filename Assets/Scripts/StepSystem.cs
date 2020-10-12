using UnityEngine;

public class StepSystem : MonoBehaviour
{
    [SerializeField]
    private bool turn;

    public bool GetTurn()
    {
        return turn;
    }

    public void NextTurn()
    {
        turn = !turn;
    }
}
