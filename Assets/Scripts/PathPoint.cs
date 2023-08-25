using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PathObjectsParent pathObjectsParent;
    public List<PlayerPiece> playerPiecesList = new List<PlayerPiece>();

    public Animator animator;

    private void Start()
    {
        pathObjectsParent = GetComponentInParent<PathObjectsParent>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.isCutted)
        {
            if (collision.gameObject.tag == "rTail")
            {
                if(MainMenuScript.boardType == 0)
                {
                    animator.SetTrigger("tailRed");
                }
                else
                {
                    animator.SetTrigger("tailGreen");
                }
                
            }
            else if (collision.gameObject.tag == "bTail")
            {
                if (MainMenuScript.boardType == 0)
                {
                    animator.SetTrigger("tailBlue");
                }
                else
                {
                    animator.SetTrigger("tailRed");
                }
            }
            else if (collision.gameObject.tag == "gTail")
            {
                if (MainMenuScript.boardType == 0)
                {
                    animator.SetTrigger("tailGreen");
                }
                else
                {
                    animator.SetTrigger("tailYellow");
                }
            }
            else if (collision.gameObject.tag == "yTail")
            {
                if (MainMenuScript.boardType == 0)
                {
                    animator.SetTrigger("tailYellow");
                }
                else
                {
                    animator.SetTrigger("tailBlue");
                }
            }
            else
            {
                //Nothing to show
            }
        }
    }

    public void AddPlayerPiece(PlayerPiece playerPiece_)
    {
        playerPiecesList.Add(playerPiece_);
        RescaleAndRepositionAllPlayerPieces();
    }

    public void RemovePlayerPiece(PlayerPiece playerPiece_)
    {
        if (playerPiecesList.Contains(playerPiece_))
        {
            playerPiecesList.Remove(playerPiece_);
            RescaleAndRepositionAllPlayerPieces();  //i added this extra code.. .
        }
    }

    public void RescaleAndRepositionAllPlayerPieces()
    {
        int plsCount = playerPiecesList.Count;
        bool isOdd = (plsCount % 2) == 0 ? false : true;
        int spritelayers = 0;

        int extent = plsCount / 2;
        int counter = 0;

        if (isOdd)
        {
            for(int i = -extent; i <= extent; i++)
            {
                playerPiecesList[counter].transform.localScale = new Vector3(pathObjectsParent.scales[plsCount - 1], pathObjectsParent.scales[playerPiecesList.Count - 1], 1f);
                playerPiecesList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectsParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }
        }
        else
        {
            for (int i = -extent; i < extent; i++)
            {
                playerPiecesList[counter].transform.localScale = new Vector3(pathObjectsParent.scales[plsCount - 1], pathObjectsParent.scales[playerPiecesList.Count - 1], 1f);
                playerPiecesList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectsParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }
        }

        for(int i = 0; i < playerPiecesList.Count; i++)
        {
            playerPiecesList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spritelayers;
            spritelayers++;
        }
    }
    /*private void Update()
    {
        int plsCount = playerPiecesList.Count;
        if (!GameManager.isPlayerMoving && plsCount >= 1)
        {
            bool isOdd = (plsCount % 2) == 0 ? false : true;
            int spritelayers = 0;

            int extent = plsCount / 2;
            int counter = 0;

            if (isOdd)
            {
                for (int i = -extent; i <= extent; i++)
                {
                    playerPiecesList[counter].transform.localScale = new Vector3(pathObjectsParent.scales[plsCount - 1], pathObjectsParent.scales[playerPiecesList.Count - 1], 1f);
                    playerPiecesList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectsParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                    counter++;
                }
            }
            else
            {
                for (int i = -extent; i < extent; i++)
                {
                    playerPiecesList[counter].transform.localScale = new Vector3(pathObjectsParent.scales[plsCount - 1], pathObjectsParent.scales[playerPiecesList.Count - 1], 1f);
                    playerPiecesList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectsParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                    counter++;
                }
            }

            for (int i = 0; i < playerPiecesList.Count; i++)
            {
                if (!playerPiecesList[i].gameObject.tag.Contains(ReturnPlayerName()))
                {
                    playerPiecesList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spritelayers;
                    //playerPiecesList[i].transform.localScale = new Vector3(pathObjectsParent.scales[0], pathObjectsParent.scales[0], 1f);
                    spritelayers++;
                }
            }
            for (int i = 0; i < playerPiecesList.Count; i++)
            {
                if (playerPiecesList[i].gameObject.tag.Contains(ReturnPlayerName()))
                {
                    playerPiecesList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spritelayers;
                    spritelayers++;
                }
            }
        }
    }

    private string ReturnPlayerName()
    {
        string playerName;
        if (GameManager.currentPlayer == 1)
        {
            playerName = "Blue";
            return playerName;
        }
        else if (GameManager.currentPlayer == 2){
            playerName = "Red";
            return playerName;
        }
        else if (GameManager.currentPlayer == 3)
        {
            playerName = "Green";
            return playerName;
        }
        else
        {
            playerName = "Yellow";
            return playerName;
        }
    }*/
}
