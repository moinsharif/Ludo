using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerPiece : PlayerPiece
{
    RollingDice redHomeRollingDice;

    private void Start()
    {
        redHomeRollingDice = GetComponentInParent<RedHome>().rollingDice;
        whatIsPlayerPathPoints = pathsParent.redPathPoints;
        playerName = "red";
    }

    private void OnMouseDown()
    {
        if (GameManager.gm.rolledDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rolledDice == redHomeRollingDice && GameManager.gm.numOfStepsToMove == 6)
                {
                    MakePlayerReadyToMove(pathsParent.redPathPoints);
                    GameManager.gm.numOfStepsToMove = 0;
                    return;
                }
                return;
            }

            if (GameManager.gm.rolledDice == redHomeRollingDice && isReady)
            {
                MoveSteps(pathsParent.redPathPoints);
            }
        }
    }
}
