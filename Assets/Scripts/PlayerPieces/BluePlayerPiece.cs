using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerPiece : PlayerPiece
{

    RollingDice blueHomeRollingDice;

    private void Start()
    {
        blueHomeRollingDice = GetComponentInParent<BlueHome>().rollingDice;
        whatIsPlayerPathPoints = pathsParent.bluePathPoints;
        playerName = "blue";
    }

    /*private void Update()
    {
        int numOfStepsToMove = GameManager.gm.numOfStepsToMove;

        if ((GameManager.gm.rolledDice == blueHomeRollingDice && numOfStepsToMove != 0 && isReady) | (GameManager.gm.rolledDice == blueHomeRollingDice && numOfStepsToMove == 6))
        {
            if (isPathPointsAvailableToMoveOn(numOfStepsToMove, numberOfStepsAlreadyMoved, pathsParent.bluePathPoints))
            {
                canMoveAnim.SetActive(true);
            }
        }
        else
        {
            canMoveAnim.SetActive(false);
        } 
    }*/

    private void OnMouseDown()
    {
        if (GameManager.gm.rolledDice != null)
        {
            if (!isReady)
            {
                Debug.Log("Player Not Ready");
                if (GameManager.gm.rolledDice == blueHomeRollingDice && GameManager.gm.numOfStepsToMove == 6)
                {
                    MakePlayerReadyToMove(pathsParent.bluePathPoints);
                    GameManager.gm.numOfStepsToMove = 0;
                    return;
                }
                return;
            }

            if (GameManager.gm.rolledDice == blueHomeRollingDice && isReady)
            {
                MoveSteps(pathsParent.bluePathPoints);
            }
        }
    }

}
