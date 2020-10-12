using System;
using System.Collections.Generic;
using Assets.Scripts.GameMechanic;
using Assets.Scripts.GameMechanic.Models;
using UnityEngine;

public class ButtonArray : MonoBehaviour
{
    [SerializeField]
    public int[,] buttonStatusArray = new int[3,3];
    private StepSystem stepSystem;
    private UIManager UIManager;
    private GameManager gameManager;

    void Awake()
    {
        stepSystem = FindObjectOfType<StepSystem>();
        UIManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetValue(int x, int y)
    {
        bool turn = stepSystem.GetTurn();
        
        if(turn)
            buttonStatusArray[x, y] = 1;
        else
            buttonStatusArray[x, y] = 2;


        var result = GameMap.CalculateGameResult(buttonStatusArray);
        Debug.Log($"Winner = {result.Winner} | IsToe = {result.IsToe} | "
                  + $" Sequence = {result.VictorySequenceDebugInfo}");

        var isMapFull = GameMap.IsMapFull(buttonStatusArray);
        Debug.Log($"Is map full: {isMapFull}");

        if (!result.IsToe || isMapFull)
            Debug.LogWarning("Game over!");

        if (!result.IsToe || isMapFull)
        {
            UIManager.TurnOnGameResultPopUp(result.Winner);
            gameManager.RestartGame();
        }
    }


    [Obsolete]
    GameResult CalculateGameResult()
    {
        GameResult GetVictoryResult (int player, List<(int, int)> victorySequence) => new GameResult(false, (Player) player, victorySequence);
        var map = buttonStatusArray;
        int mapWidth = map.GetLength(0);
        bool columnVictory = true;
        bool rowVictory = true;

        for (int x = 0; x < mapWidth; x++)
        {
            int firstColumnElement = map[0, x];
            int firstRowElement = map[x, 0];

            if (firstColumnElement == (int) Player.None && firstRowElement == (int) Player.None)
             continue;

            for (int y = 0; y < mapWidth; y++)
            {
                if (map[y, x]  != firstColumnElement || firstColumnElement == (int) Player.None)
                    columnVictory = false;

                if (map[x, y] != firstRowElement || firstRowElement == (int) Player.None)
                    rowVictory = false;
            }

            if (columnVictory)
                return GetVictoryResult(firstColumnElement, CalculateVictorySequence(VictorySequenceType.Column, x, mapWidth));

            if (rowVictory)
                return GetVictoryResult(firstRowElement, CalculateVictorySequence(VictorySequenceType.Row, x, mapWidth));

            columnVictory = true;
            rowVictory = true;
        }

        bool mainDiagonalVictory = true;
        bool reverseDiagonalVictory = true;
        var firstMainDiagonalElement = map[0, 0];
        var firstReverseDiagonalElement = map[0, mapWidth - 1];

        if (firstMainDiagonalElement == (int) Player.None && firstReverseDiagonalElement == (int) Player.None)
            return new GameResult(true);

        for (int i = 1; i < mapWidth; i++)
        {
            if (firstMainDiagonalElement != map[i, i] || firstMainDiagonalElement == (int) Player.None)
                mainDiagonalVictory = false;

            if (firstReverseDiagonalElement != map[i, mapWidth - i - 1] || firstReverseDiagonalElement == (int) Player.None)
                reverseDiagonalVictory = false;
        }

        if (mainDiagonalVictory)
            return GetVictoryResult(firstMainDiagonalElement, 
            CalculateVictorySequence(VictorySequenceType.MainDiagonal, 0, mapWidth));

        if (reverseDiagonalVictory)
            return GetVictoryResult(firstReverseDiagonalElement, 
            CalculateVictorySequence(VictorySequenceType.ReverseDiagonal, 0, mapWidth));

        return new GameResult(true);
    }

    [Obsolete]
    private List<(int, int)> CalculateVictorySequence(VictorySequenceType victorySequenceType, int index, int length)
    {
        var victorySequence = new List<(int, int)>();
        switch (victorySequenceType)
        {
            case VictorySequenceType.Row:
            {
                for (int x = 0; x < length; x++)
                    victorySequence.Add((index, x));
                
                return victorySequence;
            }

            case VictorySequenceType.Column: 
            {
                for (int y = 0; y < length; y++)
                    victorySequence.Add((y, index));
                
                return victorySequence;
            }

            case VictorySequenceType.MainDiagonal:
            {
                for (int y = 0; y < length; y++)
                    victorySequence.Add((y, y));
                
                return victorySequence;
            }
            
            case VictorySequenceType.ReverseDiagonal:
            {
                for (int y = 0; y < length; y++)
                    victorySequence.Add((y, length - y - 1));
                
                return victorySequence;
            }

            default: return null;
        }
    }
}
