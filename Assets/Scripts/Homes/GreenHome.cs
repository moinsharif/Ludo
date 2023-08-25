using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHome : LudoHome
{
    private void Start()
    {
        
    }
    /*public void checkIfCanMove()
    {
        Debug.Log("Entered into if player can move method");
        PlayerPiece player1 = playerPieces[0];
        PlayerPiece player2 = playerPieces[1];
        PlayerPiece player3 = playerPieces[2];
        PlayerPiece player4 = playerPieces[3];
        PathPoint[] player1_PathPoint = player1.pathsParent.greenPathPoints;
        PathPoint[] player2_PathPoint = player2.pathsParent.greenPathPoints;
        PathPoint[] player3_PathPoint = player3.pathsParent.greenPathPoints;
        PathPoint[] player4_PathPoint = player4.pathsParent.greenPathPoints;

        int numOfStepsToMove = GameManager.gm.numOfStepsToMove;

        if (numOfStepsToMove == 6)
        {
            if (!player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player2_PathPoint)
                && !player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint)
                && !player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player2_PathPoint)
                && !player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player2_PathPoint))
            {
                StartCoroutine(GameManager.gm.manageDiceCoroutine());
            }
            else
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (playerPieces[i].isPathPointsAvailableToMoveOn(numOfStepsToMove, playerPieces[i].numberOfStepsAlreadyMoved, playerPieces[i].pathsParent.greenPathPoints)
                        | playerPieces[i].numberOfStepsAlreadyMoved == 0)
                    {
                        playerPieces[i].canMoveAnim.SetActive(true);
                    }
                }
            }
        }
        else if ((player1.numberOfStepsAlreadyMoved == 1 | (player1.numberOfStepsAlreadyMoved != 0 && player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player1_PathPoint)))
            && (player2.numberOfStepsAlreadyMoved == 0 | !player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint))
            && (player3.numberOfStepsAlreadyMoved == 0 | !player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player3_PathPoint))
            && (player4.numberOfStepsAlreadyMoved == 0 | !player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player4_PathPoint)))
        {
            Debug.Log("Entered into 1");
            player1.canMove = true;
            player1.MoveSteps(player1_PathPoint);
        }
        else if ((player1.numberOfStepsAlreadyMoved == 0 | !player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player1_PathPoint))
            && (player2.numberOfStepsAlreadyMoved == 1 | (player2.numberOfStepsAlreadyMoved != 0 && player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint)))
            && (player3.numberOfStepsAlreadyMoved == 0 | !player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player3_PathPoint))
            && (player4.numberOfStepsAlreadyMoved == 0 | !player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player4_PathPoint)))
        {
            Debug.Log("Entered into 2");
            player2.canMove = true;
            player2.MoveSteps(player2_PathPoint);
        }
        else if ((player1.numberOfStepsAlreadyMoved == 0 | !player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player1_PathPoint))
            && (player2.numberOfStepsAlreadyMoved == 0 | !player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint))
            && (player3.numberOfStepsAlreadyMoved == 1 | (player3.numberOfStepsAlreadyMoved != 0 && player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player3_PathPoint)))
            && (player4.numberOfStepsAlreadyMoved == 0 | !player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player4_PathPoint)))
        {
            Debug.Log("Entered into 3");
            player3.canMove = true;
            player3.MoveSteps(player3_PathPoint);
        }
        else if ((player1.numberOfStepsAlreadyMoved == 0 | !player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player1_PathPoint))
            && (player2.numberOfStepsAlreadyMoved == 0 | !player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint))
            && (player3.numberOfStepsAlreadyMoved == 0 | !player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player3_PathPoint))
            && (player4.numberOfStepsAlreadyMoved == 1 | (player4.numberOfStepsAlreadyMoved != 0 && player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player4_PathPoint))))
        {
            Debug.Log("Entered into 4");
            player4.canMove = true;
            player4.MoveSteps(player4_PathPoint);
        }
        else
        {
            if (numOfStepsToMove != 6
                && player1.numberOfStepsAlreadyMoved == 0
                && player2.numberOfStepsAlreadyMoved == 0
                && player3.numberOfStepsAlreadyMoved == 0
                && player4.numberOfStepsAlreadyMoved == 0)
            {
                StartCoroutine(GameManager.gm.manageDiceCoroutine());
            }
            else
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (playerPieces[i].numberOfStepsAlreadyMoved != 0
                        && playerPieces[i].isPathPointsAvailableToMoveOn(numOfStepsToMove, playerPieces[i].numberOfStepsAlreadyMoved, playerPieces[i].pathsParent.greenPathPoints))
                    {
                        playerPieces[i].canMoveAnim.SetActive(true);
                    }
                }
            }
        }
    }*/
}
