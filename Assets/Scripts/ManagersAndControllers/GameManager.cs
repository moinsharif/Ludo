using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public static int currentPlayer;
    public static bool isCutted;
    public static int repeatedSix = 0;

    public int blueInWinHouse = 0;
    public int redInWinHouse = 0;
    public int greenInWinHouse = 0;
    public int yellowInWinHouse = 0;

    public int numOfStepsToMove;
    public RollingDice rolledDice;

    public GameObject DicepadBlue, DicePadRed, DicePadGreen, DicePadYellow;
    public GameObject RollingDiceBlue, RollingDiceRed, RollingDiceGreen, RollingDiceYellow;
    public GameObject BlueHighLight, RedHighLight, GreenHighLight, YellowHighLight;
    public GameObject BlueDiceArrow, RedDiceArrow, GreenDiceArrow, YellowDiceArrow;
    public GameObject blueHome, redHome, greenHome, yellowHome;
    public GameObject Ludoboard0, Ludoboard1;
    public GameObject winner1, winner2;

    public GameObject winWindow;

    public PlayerPiece[] BluePlayerPieces;
    public PlayerPiece[] GreenPlayerPieces;
    public PlayerPiece[] RedPlayerPieces;
    public PlayerPiece[] YellowPlayerPieces;

    public UnityEngine.Vector3 player1ScaleHolder;
    public UnityEngine.Vector3 player2ScaleHolder;
    public UnityEngine.Vector3 player3ScaleHolder;
    public UnityEngine.Vector3 player4ScaleHolder;

    List<PathPoint> playerOnPathPointsList = new List<PathPoint>();

    [SerializeField] Sprite[] numberedSprites = null;
    [SerializeField] SpriteRenderer numberedSpriteHolderBlue = null;
    [SerializeField] SpriteRenderer numberedSpriteHolderRed = null;
    [SerializeField] SpriteRenderer numberedSpriteHolderGreen = null;
    [SerializeField] SpriteRenderer numberedSpriteHolderYellow = null;

    private void Awake()
    {
        gm = this;
        currentPlayer = 1;
    }

    private void Start()
    {
        if(panelControl.howManyPlayers < 1 || panelControl.howManyPlayers > 4)
        {
            panelControl.howManyPlayers = 4;
        }
        if (MainMenuScript.boardType == 0)
        {
            Ludoboard0.SetActive(true);
            Ludoboard1.SetActive(false);
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("guti3");
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            GameObject[] gameObjectArray1 = GameObject.FindGameObjectsWithTag("guti4");
            foreach (GameObject go1 in gameObjectArray1)
            {
                go1.SetActive(false);
            }
        }
        else
        {
            Ludoboard0.SetActive(false);
            Ludoboard1.SetActive(true);
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("guti1");
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            GameObject[] gameObjectArray1 = GameObject.FindGameObjectsWithTag("guti2");
            foreach (GameObject go1 in gameObjectArray1)
            {
                go1.SetActive(false);
            }
        }

        if (MainMenuScript.gutiType == 0)
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("guti2");
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            GameObject[] gameObjectArray1 = GameObject.FindGameObjectsWithTag("guti4");
            foreach (GameObject go1 in gameObjectArray1)
            {
                go1.SetActive(false);
            }
        }
        else
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("guti1");
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            GameObject[] gameObjectArray1 = GameObject.FindGameObjectsWithTag("guti3");
            foreach (GameObject go1 in gameObjectArray1)
            {
                go1.SetActive(false);
            }
        }

        switch(panelControl.howManyPlayers)
        {
            case 2:
                DicePadRed.SetActive(false);
                DicePadYellow.SetActive(false);
                RollingDiceRed.SetActive(false);
                RollingDiceYellow.SetActive(false);
                redHome.SetActive(false);
                yellowHome.SetActive(false);

                GreenHighLight.SetActive(false);
                GreenDiceArrow.SetActive(false);
                RollingDiceGreen.SetActive(false);
                break;
            case 3:
                DicePadYellow.SetActive(false);
                RollingDiceYellow.SetActive(false);
                yellowHome.SetActive(false);

                GreenHighLight.SetActive(false);
                GreenDiceArrow.SetActive(false);
                RollingDiceGreen.SetActive(false);
                RedHighLight.SetActive(false);
                RedDiceArrow.SetActive(false);
                RollingDiceRed.SetActive(false);
                break;
            case 4:
                Debug.Log("No DicePad to Hide");
                GreenHighLight.SetActive(false);
                GreenDiceArrow.SetActive(false);
                RollingDiceGreen.SetActive(false);
                RedHighLight.SetActive(false);
                RedDiceArrow.SetActive(false);
                RollingDiceRed.SetActive(false);
                YellowHighLight.SetActive(false);
                YellowDiceArrow.SetActive(false);
                RollingDiceYellow.SetActive(false);
                break;
            default:
                Debug.Log("Something is wrong. i can't get how many players id");
                break;
        }
    }

    public IEnumerator manageDiceCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        manageDice();
        RollingDice.canDiceRoll = true;
    }

    public void addPlayerToWin()
    {
        Debug.Log("Added Player to Win House");
        switch (currentPlayer)
        {
            case 1:
                blueInWinHouse += 1;
                if (blueInWinHouse == 4)
                {
                    declearWinner(1);
                }
                break;
            case 2:
                redInWinHouse += 1;
                if (redInWinHouse == 4)
                {
                    declearWinner(2);
                }
                break;
            case 3:
                greenInWinHouse += 1;
                if (greenInWinHouse == 4)
                {
                    declearWinner(2);
                }
                break;
            case 4:
                yellowInWinHouse += 1;
                if (yellowInWinHouse == 4)
                {
                    declearWinner(2);
                }
                break;
            default:
                Debug.Log("Something is wrong. i can't get current Player id");
                break;
        }
    }

    public void declearWinner(int winner)
    {
        SoundManager.winAudioSource.Play();
        winWindow.SetActive(true);

        if (winner == 1)
        {
            winner1.SetActive(true);
        }
        else if(winner == 2)
        {
            winner2.SetActive(true);
        }
    }

    public void changeDiceSprite(int diceSprite)
    {
        numberedSpriteHolderBlue.sprite = numberedSprites[diceSprite];
        numberedSpriteHolderRed.sprite = numberedSprites[diceSprite];
        numberedSpriteHolderGreen.sprite = numberedSprites[diceSprite];
        numberedSpriteHolderYellow.sprite = numberedSprites[diceSprite];
    } 

    public void manageDice()
    {
        switch(panelControl.howManyPlayers)
        {
            case 2:
                switch (currentPlayer)
                {
                    case 1:
                        currentPlayer = 3;
                        RollingDiceBlue.gameObject.SetActive(false);
                        RollingDiceGreen.gameObject.SetActive(true);
                        BlueHighLight.SetActive(false);
                        BlueDiceArrow.SetActive(false);
                        GreenHighLight.SetActive(true);
                        GreenDiceArrow.SetActive(true);
                        break;
                    case 3:
                        currentPlayer = 1;
                        RollingDiceBlue.gameObject.SetActive(true);
                        RollingDiceGreen.gameObject.SetActive(false);
                        BlueHighLight.SetActive(true);
                        BlueDiceArrow.SetActive(true);
                        GreenHighLight.SetActive(false);
                        GreenDiceArrow.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (currentPlayer)
                {
                    case 1:
                        currentPlayer = 2;
                        RollingDiceBlue.SetActive(false);
                        RollingDiceGreen.SetActive(false);
                        RollingDiceRed.SetActive(true);
                        RedHighLight.SetActive(true);
                        RedDiceArrow.SetActive(true);
                        GreenHighLight.SetActive(false);
                        GreenDiceArrow.SetActive(false);
                        BlueHighLight.SetActive(false);
                        BlueDiceArrow.SetActive(false);
                        break;
                    case 2:
                        currentPlayer = 3;
                        RollingDiceBlue.SetActive(false);
                        RollingDiceGreen.SetActive(true);
                        RollingDiceRed.SetActive(false);
                        RedHighLight.SetActive(false);
                        RedDiceArrow.SetActive(false);
                        GreenHighLight.SetActive(true);
                        GreenDiceArrow.SetActive(true);
                        BlueHighLight.SetActive(false);
                        BlueDiceArrow.SetActive(false);
                        break;
                    case 3:
                        currentPlayer = 1;
                        RollingDiceBlue.SetActive(true);
                        RollingDiceGreen.SetActive(false);
                        RollingDiceRed.SetActive(false);
                        RedHighLight.SetActive(false);
                        RedDiceArrow.SetActive(false);
                        GreenHighLight.SetActive(false);
                        GreenDiceArrow.SetActive(false);
                        BlueHighLight.SetActive(true);
                        BlueDiceArrow.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                switch (currentPlayer)
                {
                    case 1:
                        currentPlayer = 2;
                        RollingDiceBlue.SetActive(false);
                        RollingDiceGreen.SetActive(false);
                        RollingDiceYellow.SetActive(false);
                        RollingDiceRed.SetActive(true);
                        RedHighLight.SetActive(true);
                        RedDiceArrow.SetActive(true);
                        GreenHighLight.SetActive(false);
                        GreenDiceArrow.SetActive(false);
                        BlueHighLight.SetActive(false);
                        BlueDiceArrow.SetActive(false);
                        YellowHighLight.SetActive(false);
                        YellowDiceArrow.SetActive(false);
                        break;
                    case 2:
                        currentPlayer = 3;
                        RollingDiceBlue.SetActive(false);
                        RollingDiceGreen.SetActive(true);
                        RollingDiceYellow.SetActive(false);
                        RollingDiceRed.SetActive(false);
                        RedHighLight.SetActive(false);
                        RedDiceArrow.SetActive(false);
                        GreenHighLight.SetActive(true);
                        GreenDiceArrow.SetActive(true);
                        BlueHighLight.SetActive(false);
                        BlueDiceArrow.SetActive(false);
                        YellowHighLight.SetActive(false);
                        YellowDiceArrow.SetActive(false);
                        break;
                    case 3:
                        currentPlayer = 4;
                        RollingDiceBlue.SetActive(false);
                        RollingDiceGreen.SetActive(false);
                        RollingDiceYellow.SetActive(true);
                        RollingDiceRed.SetActive(false);
                        RedHighLight.SetActive(false);
                        RedDiceArrow.SetActive(false);
                        GreenHighLight.SetActive(false);
                        GreenDiceArrow.SetActive(false);
                        BlueHighLight.SetActive(false);
                        BlueDiceArrow.SetActive(false);
                        YellowHighLight.SetActive(true);
                        YellowDiceArrow.SetActive(true);
                        break;
                    case 4:
                        currentPlayer = 1;
                        RollingDiceBlue.SetActive(true);
                        RollingDiceGreen.SetActive(false);
                        RollingDiceYellow.SetActive(false);
                        RollingDiceRed.SetActive(false);
                        RedHighLight.SetActive(false);
                        RedDiceArrow.SetActive(false);
                        GreenHighLight.SetActive(false);
                        GreenDiceArrow.SetActive(false);
                        BlueHighLight.SetActive(true);
                        BlueDiceArrow.SetActive(true);
                        YellowHighLight.SetActive(false);
                        YellowDiceArrow.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            default:
                Debug.Log("Something is wrong. i can't get how many players id in Manage Dice");
                break;
        }
    }

    public void AddPathPoint(PathPoint pathPoint_)
    {
        playerOnPathPointsList.Add(pathPoint_);
    }
    public void RemovePathPoint(PathPoint pathPoint_)
    {
        if (playerOnPathPointsList.Contains(pathPoint_))
        {
            playerOnPathPointsList.Remove(pathPoint_);
        }
        else
        {
            Debug.Log("Path point not found to be removed.");
        }
    }

    public void managePlayerMovement()
    {
        Debug.Log("Entered into if player can move method");
        PlayerPiece[] currentPlayerPieces;
        PlayerPiece player1;
        PlayerPiece player2;
        PlayerPiece player3;
        PlayerPiece player4;
        PathPoint[] player1_PathPoint;
        PathPoint[] player2_PathPoint;
        PathPoint[] player3_PathPoint;
        PathPoint[] player4_PathPoint;

        switch (currentPlayer)
        {
            case 1:
                Debug.Log("Blue Player currently moving");
                currentPlayerPieces = BluePlayerPieces;
                player1 = BluePlayerPieces[0];
                player2 = BluePlayerPieces[1];
                player3 = BluePlayerPieces[2];
                player4 = BluePlayerPieces[3];
                player1_PathPoint = player1.pathsParent.bluePathPoints;
                player2_PathPoint = player2.pathsParent.bluePathPoints;
                player3_PathPoint = player3.pathsParent.bluePathPoints;
                player4_PathPoint = player4.pathsParent.bluePathPoints;
                break;
            case 2:
                Debug.Log("Red Player currently moving");
                currentPlayerPieces = RedPlayerPieces;
                player1 = RedPlayerPieces[0];
                player2 = RedPlayerPieces[1];
                player3 = RedPlayerPieces[2];
                player4 = RedPlayerPieces[3];
                player1_PathPoint = player1.pathsParent.redPathPoints;
                player2_PathPoint = player2.pathsParent.redPathPoints;
                player3_PathPoint = player3.pathsParent.redPathPoints;
                player4_PathPoint = player4.pathsParent.redPathPoints;
                break;
            case 3:
                Debug.Log("Green Player currently moving");
                currentPlayerPieces = GreenPlayerPieces;
                player1 = GreenPlayerPieces[0];
                player2 = GreenPlayerPieces[1];
                player3 = GreenPlayerPieces[2];
                player4 = GreenPlayerPieces[3];
                player1_PathPoint = player1.pathsParent.greenPathPoints;
                player2_PathPoint = player2.pathsParent.greenPathPoints;
                player3_PathPoint = player3.pathsParent.greenPathPoints;
                player4_PathPoint = player4.pathsParent.greenPathPoints;
                break;
            case 4:
                Debug.Log("Yellow Player currently moving");
                currentPlayerPieces = YellowPlayerPieces;
                player1 = YellowPlayerPieces[0];
                player2 = YellowPlayerPieces[1];
                player3 = YellowPlayerPieces[2];
                player4 = YellowPlayerPieces[3];
                player1_PathPoint = player1.pathsParent.yellowPathPoints;
                player2_PathPoint = player2.pathsParent.yellowPathPoints;
                player3_PathPoint = player3.pathsParent.yellowPathPoints;
                player4_PathPoint = player4.pathsParent.yellowPathPoints;
                break;
            default:
                Debug.Log("Blue Player currently moving");
                currentPlayerPieces = BluePlayerPieces;
                player1 = BluePlayerPieces[0];
                player2 = BluePlayerPieces[1];
                player3 = BluePlayerPieces[2];
                player4 = BluePlayerPieces[3];
                player1_PathPoint = player1.pathsParent.bluePathPoints;
                player2_PathPoint = player2.pathsParent.bluePathPoints;
                player3_PathPoint = player3.pathsParent.bluePathPoints;
                player4_PathPoint = player4.pathsParent.bluePathPoints;
                break;
        }

        playerRequiredToMove(player1, player2, player3, player4);

        if (numOfStepsToMove != 6
            && player1.numberOfStepsAlreadyMoved == 0
            && player2.numberOfStepsAlreadyMoved == 0
            && player3.numberOfStepsAlreadyMoved == 0
            && player4.numberOfStepsAlreadyMoved == 0)
        {
            StartCoroutine(manageDiceCoroutine());
        }
        else if ((player1.numberOfStepsAlreadyMoved == 1 | (player1.numberOfStepsAlreadyMoved != 0 && player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player1_PathPoint)))
            && ((numOfStepsToMove != 6 && player2.numberOfStepsAlreadyMoved == 0) | (player2.numberOfStepsAlreadyMoved != 0 && !player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint)))
            && ((numOfStepsToMove != 6 && player3.numberOfStepsAlreadyMoved == 0) | (player3.numberOfStepsAlreadyMoved != 0 && !player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player3_PathPoint)))
            && ((numOfStepsToMove != 6 && player4.numberOfStepsAlreadyMoved == 0) | (player4.numberOfStepsAlreadyMoved != 0 && !player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player4_PathPoint))))
        {
            player1.canMove = true;
            player1.MoveSteps(player1_PathPoint);
        }
        else if (((numOfStepsToMove != 6 && player1.numberOfStepsAlreadyMoved == 0) | (player1.numberOfStepsAlreadyMoved != 0 && !player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player1_PathPoint)))
            && (player2.numberOfStepsAlreadyMoved == 1 | (player2.numberOfStepsAlreadyMoved != 0 && player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint)))
            && ((numOfStepsToMove != 6 && player3.numberOfStepsAlreadyMoved == 0) | (player3.numberOfStepsAlreadyMoved != 0 && !player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player3_PathPoint)))
            && ((numOfStepsToMove != 6 && player4.numberOfStepsAlreadyMoved == 0) | (player4.numberOfStepsAlreadyMoved != 0 && !player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player4_PathPoint))))
        {
            player2.canMove = true;
            player2.MoveSteps(player2_PathPoint);
        }
        else if (((numOfStepsToMove != 6 && player1.numberOfStepsAlreadyMoved == 0) | (player1.numberOfStepsAlreadyMoved != 0 && !player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player1_PathPoint)))
            && ((numOfStepsToMove != 6 && player2.numberOfStepsAlreadyMoved == 0) | (player2.numberOfStepsAlreadyMoved != 0 && !player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint)))
            && (player3.numberOfStepsAlreadyMoved == 1 | (player3.numberOfStepsAlreadyMoved != 0 && player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player3_PathPoint)))
            && ((numOfStepsToMove != 6 && player4.numberOfStepsAlreadyMoved == 0) | (player4.numberOfStepsAlreadyMoved != 0 && !player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player4_PathPoint))))
        {
            player3.canMove = true;
            player3.MoveSteps(player3_PathPoint);
        }
        else if (((numOfStepsToMove != 6 && player1.numberOfStepsAlreadyMoved == 0) | (player1.numberOfStepsAlreadyMoved != 0 && !player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player1_PathPoint)))
            && ((numOfStepsToMove != 6 && player2.numberOfStepsAlreadyMoved == 0) | (player2.numberOfStepsAlreadyMoved != 0 && !player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint)))
            && ((numOfStepsToMove != 6 && player3.numberOfStepsAlreadyMoved == 0) | (player3.numberOfStepsAlreadyMoved != 0 && !player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player3_PathPoint)))
            && (player4.numberOfStepsAlreadyMoved == 1 | (player4.numberOfStepsAlreadyMoved != 0 && player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player4_PathPoint))))
        {
            player4.canMove = true;
            player4.MoveSteps(player4_PathPoint);
        }
        else if (!player1.isPathPointsAvailableToMoveOn(numOfStepsToMove, player1.numberOfStepsAlreadyMoved, player2_PathPoint)
            && !player2.isPathPointsAvailableToMoveOn(numOfStepsToMove, player2.numberOfStepsAlreadyMoved, player2_PathPoint)
            && !player3.isPathPointsAvailableToMoveOn(numOfStepsToMove, player3.numberOfStepsAlreadyMoved, player2_PathPoint)
            && !player4.isPathPointsAvailableToMoveOn(numOfStepsToMove, player4.numberOfStepsAlreadyMoved, player2_PathPoint))
        {
            StartCoroutine(manageDiceCoroutine());
        }
        else if (numOfStepsToMove == 6)
        {
            int howManyInDesigHouse = 0;
            for (int i = 0; i <= 3; i++)
            {
                if (currentPlayerPieces[i].isPathPointsAvailableToMoveOn(numOfStepsToMove, currentPlayerPieces[i].numberOfStepsAlreadyMoved, currentPlayerPieces[i].pathsParent.bluePathPoints)
                    | currentPlayerPieces[i].numberOfStepsAlreadyMoved == 0)
                {
                    currentPlayerPieces[i].canMove = true;
                    currentPlayerPieces[i].canMoveAnim.SetActive(true);
                }
                else
                {
                    howManyInDesigHouse += 1;
                }
            }
            if (howManyInDesigHouse == 4)
            {
                StartCoroutine(manageDiceCoroutine());
            }
        }
        else
        {
            for (int i = 0; i <= 3; i++)
            {
                if (currentPlayerPieces[i].numberOfStepsAlreadyMoved != 0
                    && currentPlayerPieces[i].isPathPointsAvailableToMoveOn(numOfStepsToMove, currentPlayerPieces[i].numberOfStepsAlreadyMoved, currentPlayerPieces[i].pathsParent.bluePathPoints))
                {
                    currentPlayerPieces[i].canMove = true;
                    currentPlayerPieces[i].canMoveAnim.SetActive(true);
                }
            }

            if(!player1.canMove && !player2.canMove && !player3.canMove && !player4.canMove)
            {
                StartCoroutine(manageDiceCoroutine());
            }
        }
    }

    void playerRequiredToMove(PlayerPiece player1, PlayerPiece player2, PlayerPiece player3, PlayerPiece player4)
    {
        player1ScaleHolder = player1.transform.localScale;
        player2ScaleHolder = player2.transform.localScale;
        player3ScaleHolder = player3.transform.localScale;
        player4ScaleHolder = player4.transform.localScale;
        player1.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
        player2.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
        player3.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
        player4.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
    }

    public void playerMoved()
    {
        PlayerPiece player1;
        PlayerPiece player2;
        PlayerPiece player3;
        PlayerPiece player4;

        switch (currentPlayer)
        {
            case 1:
                player1 = BluePlayerPieces[0];
                player2 = BluePlayerPieces[1];
                player3 = BluePlayerPieces[2];
                player4 = BluePlayerPieces[3];
                break;
            case 2:
                player1 = RedPlayerPieces[0];
                player2 = RedPlayerPieces[1];
                player3 = RedPlayerPieces[2];
                player4 = RedPlayerPieces[3];
                break;
            case 3:
                player1 = GreenPlayerPieces[0];
                player2 = GreenPlayerPieces[1];
                player3 = GreenPlayerPieces[2];
                player4 = GreenPlayerPieces[3];
                break;
            case 4:
                player1 = YellowPlayerPieces[0];
                player2 = YellowPlayerPieces[1];
                player3 = YellowPlayerPieces[2];
                player4 = YellowPlayerPieces[3];
                break;
            default:
                player1 = BluePlayerPieces[0];
                player2 = BluePlayerPieces[1];
                player3 = BluePlayerPieces[2];
                player4 = BluePlayerPieces[3];
                break;
        }

        player1.transform.localScale = player1ScaleHolder;
        player2.transform.localScale = player2ScaleHolder;
        player3.transform.localScale = player3ScaleHolder;
        player4.transform.localScale = player4ScaleHolder;
    }
}
