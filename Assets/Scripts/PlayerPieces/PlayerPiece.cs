using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public bool isReady;
    public bool canMove;
    public bool moveNow;
    public int numberOfStepsAlreadyMoved;

    public GameObject canMoveAnim;

    public Animator animator;

    public PathObjectsParent pathsParent;
    public PathPoint previousPathPoint;
    public PathPoint currentPathPoint;

    Coroutine moveSteps_Coroutine;
    Coroutine backToHomeWhenCut_Coroutine;

    PlayerPiece cuttedPlayer;
    public PathPoint[] whatIsPlayerPathPoints;
    public string playerName;

    Vector3 playerPosition;

    private void Awake()
    {
        pathsParent = FindObjectOfType<PathObjectsParent>();
        playerPosition = transform.position;
        canMoveAnim.SetActive(false);
    }

    public void MoveSteps(PathPoint[] pathPointsToMoveOn_)
    {
        moveSteps_Coroutine = StartCoroutine(MoveSteps_Enum(pathPointsToMoveOn_));
    }

    public void MakePlayerReadyToMove(PathPoint[] pathPointsToMoveOn_)
    {
        isReady = true;
        RollingDice.canDiceRoll = true;
        animator.enabled = false;

        GameObject[] canMoveAnimDisable = GameObject.FindGameObjectsWithTag("canMoveAnim");
        foreach (GameObject letsdisable in canMoveAnimDisable)
        {
            letsdisable.SetActive(false);
        }

        transform.position = pathPointsToMoveOn_[0].transform.position;
        //transform.position = Vector3.Lerp(transform.position, pathPointsToMoveOn_[0].transform.position, Time.deltaTime * 50f);
        //transform.position = Vector3.MoveTowards(transform.position, pathPointsToMoveOn_[0].transform.position, Time.deltaTime * 2.0f);

        numberOfStepsAlreadyMoved = 1;
        SoundManager.playerAudioSource.Play();

        previousPathPoint = pathPointsToMoveOn_[0];
        currentPathPoint = pathPointsToMoveOn_[0];

        GameManager.gm.AddPathPoint(currentPathPoint);
        currentPathPoint.AddPlayerPiece(this);
    }

    IEnumerator MoveSteps_Enum(PathPoint[] pathPointsToMoveOn_)
    {
        yield return new WaitForSeconds(0.05f);
        int numOfStepsToMove = GameManager.gm.numOfStepsToMove;

        if (canMove)
        {
            GameManager.gm.playerMoved();

            GameManager.gm.numOfStepsToMove = 0;
            canMove = false;

            GameObject[] canMoveAnimDisable = GameObject.FindGameObjectsWithTag("canMoveAnim");
            foreach (GameObject letsdisable in canMoveAnimDisable)
            {
                letsdisable.SetActive(false);
            }

            for (int i = numberOfStepsAlreadyMoved; i < (numberOfStepsAlreadyMoved + numOfStepsToMove); i++)
            {
                if (isPathPointsAvailableToMoveOn(numOfStepsToMove, numberOfStepsAlreadyMoved, pathPointsToMoveOn_))
                {
                    yield return new WaitForSeconds(0.19f);
                    animator.enabled = true;
                    animator.SetTrigger("playerAnim");
                    SoundManager.playerAudioSource.Play();
                    transform.position = pathPointsToMoveOn_[i].transform.position;
                }
            }

            if (isPathPointsAvailableToMoveOn(numOfStepsToMove, numberOfStepsAlreadyMoved, pathPointsToMoveOn_))
            {
                numberOfStepsAlreadyMoved += numOfStepsToMove;
                previousPathPoint.RescaleAndRepositionAllPlayerPieces(); // i made this change because i added it on PathPoint when pathpoint removes.
                GameManager.gm.RemovePathPoint(previousPathPoint);
                previousPathPoint.RemovePlayerPiece(this);
                currentPathPoint = pathPointsToMoveOn_[numberOfStepsAlreadyMoved - 1];
                currentPathPoint.AddPlayerPiece(this);
                GameManager.gm.AddPathPoint(currentPathPoint);
                previousPathPoint = currentPathPoint;

                if(currentPathPoint.gameObject.tag == "safeHouse")
                {
                    SoundManager.safeHouseAudioSource.Play();
                }
                if(numberOfStepsAlreadyMoved == 57)
                {
                    GameManager.gm.addPlayerToWin();
                    SoundManager.winAudioSource.Play();
                }
                if(currentPathPoint.playerPiecesList.Count == 2 && currentPathPoint.playerPiecesList[0].playerName != playerName && currentPathPoint.gameObject.tag != "safeHouse")
                {
                    cuttedPlayer = currentPathPoint.playerPiecesList[0];
                    SoundManager.dismissalAudioSource.Play();
                    GameManager.isCutted = true;
                    Debug.Log(cuttedPlayer.ToString() + cuttedPlayer.numberOfStepsAlreadyMoved.ToString());
                    cuttedPlayer.isReady = false;
                    cuttedPlayer.canMove = false;
                    cuttedPlayer.moveNow = false;
                    backToHomeWhenCut_Coroutine = StartCoroutine(letsBackToHome(cuttedPlayer.whatIsPlayerPathPoints, cuttedPlayer.numberOfStepsAlreadyMoved));
                    Debug.Log("whatIsPlayerPathPoints " + cuttedPlayer.whatIsPlayerPathPoints.ToString());
                    Debug.Log("numberOfStepsAlreadyMoved " + cuttedPlayer.numberOfStepsAlreadyMoved.ToString());
                }
            }

            if (GameManager.isCutted)
            {
                RollingDice.canDiceRoll = true;
            }
            else if(numberOfStepsAlreadyMoved == 57)
            {
                RollingDice.canDiceRoll = true;
            }
            else
            {
                if (numOfStepsToMove == 6)
                {
                    RollingDice.canDiceRoll = true;
                }
                else
                {
                    StartCoroutine(GameManager.gm.manageDiceCoroutine());
                }
            }
        }

        animator.enabled = false;

        yield return new WaitForEndOfFrame();

        if (moveSteps_Coroutine != null)
        {
            StopCoroutine(moveSteps_Coroutine);
        }

        //RollingDice.canDiceRoll = true;
    }

    IEnumerator letsBackToHome(PathPoint[] pathPointsToMoveOn_, int numberOfStepsMoved)
    {
        for (int i = numberOfStepsMoved-1; i >= 1; i--)
        {
            cuttedPlayer.transform.position = pathPointsToMoveOn_[i].transform.position;
            yield return new WaitForSeconds(0.035f);
        }

        GameManager.isCutted = false;
        cuttedPlayer.numberOfStepsAlreadyMoved = 0;
        cuttedPlayer.transform.position = cuttedPlayer.playerPosition;
        currentPathPoint.RemovePlayerPiece(currentPathPoint.playerPiecesList[0]);
        cuttedPlayer.transform.localScale = new Vector3(1f, 1f, 1f);

        yield return new WaitForEndOfFrame();

        if (backToHomeWhenCut_Coroutine != null)
        {
            StopCoroutine(backToHomeWhenCut_Coroutine);
        }
    }

    public bool isPathPointsAvailableToMoveOn(int numOfStepsToMove_, int numberOfStepsAlreadyMoved_, PathPoint[] pathPointsToMoveOn_)
    {
        int leftNumberOfPathPoints = pathPointsToMoveOn_.Length - numberOfStepsAlreadyMoved_;
        if(leftNumberOfPathPoints >= numOfStepsToMove_)
        {
            return true;
        }

        return false;
    }
}
