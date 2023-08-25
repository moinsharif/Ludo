using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerPiece : PlayerPiece
{
    RollingDice greenHomeRollingDice;

    private void Start()
    {
        greenHomeRollingDice = GetComponentInParent<GreenHome>().rollingDice;
        whatIsPlayerPathPoints = pathsParent.greenPathPoints;
        playerName = "green";
    }

    private void OnMouseDown()
    {
        if (GameManager.gm.rolledDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rolledDice == greenHomeRollingDice && GameManager.gm.numOfStepsToMove == 6)
                {
                    MakePlayerReadyToMove(pathsParent.greenPathPoints);
                    GameManager.gm.numOfStepsToMove = 0;
                    return;
                }
                return;
            }

            if (GameManager.gm.rolledDice == greenHomeRollingDice && isReady)
            {
                MoveSteps(pathsParent.greenPathPoints);
            }
        }
    }
}
