using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayerPiece : PlayerPiece
{
    RollingDice yellowHomeRollingDice;

    private void Start()
    {
        yellowHomeRollingDice = GetComponentInParent<YellowHome>().rollingDice;
        whatIsPlayerPathPoints = pathsParent.yellowPathPoints;
        playerName = "yellow";
    }

    private void OnMouseDown()
    {
        if (GameManager.gm.rolledDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rolledDice == yellowHomeRollingDice && GameManager.gm.numOfStepsToMove == 6)
                {
                    MakePlayerReadyToMove(pathsParent.yellowPathPoints);
                    GameManager.gm.numOfStepsToMove = 0;
                    return;
                }
                return;
            }

            if (GameManager.gm.rolledDice == yellowHomeRollingDice && isReady)
            {
                MoveSteps(pathsParent.yellowPathPoints);
            }
        }
    }
}
