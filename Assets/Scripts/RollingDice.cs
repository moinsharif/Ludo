﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    [SerializeField] int numberGot;
    [SerializeField] GameObject rollingDiceAnimation = null;
    [SerializeField] SpriteRenderer numberedSpriteHolder = null;
    //[SerializeField] Sprite[] numberedSprites = null;

    public static bool canDiceRoll = true;
    Coroutine generateRandomNumOnDice_Coroutine;

    private void OnMouseDown()
    {
        generateRandomNumOnDice_Coroutine = StartCoroutine(GenerateRandomNumberOnDice_Enum());
    }

    IEnumerator GenerateRandomNumberOnDice_Enum()
    {
        //yield return new WaitForEndOfFrame();
        if (canDiceRoll)
        {
            SoundManager.diceAudioSource.Play();
            canDiceRoll = false;
            numberedSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnimation.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            if(GameManager.repeatedSix == 2)
            {
                Debug.Log("Entered in repeated Six");
                GameManager.repeatedSix = 0;
                numberGot = Random.Range(0, 5);
            }
            else
            {
                numberGot = Random.Range(0, 6);
            }

            //numberedSpriteHolder.sprite = numberedSprites[numberGot];
            GameManager.gm.changeDiceSprite(numberGot);
            numberGot += 1;
            if(numberGot == 6)
            {
                GameManager.repeatedSix += 1;
                Debug.Log("repeated Six " + GameManager.repeatedSix.ToString());
            }
            GameManager.gm.numOfStepsToMove = numberGot;
            GameManager.gm.rolledDice = this;
            numberedSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.SetActive(false);

            GameManager.gm.managePlayerMovement();

            yield return new WaitForEndOfFrame();

            if (generateRandomNumOnDice_Coroutine != null)
            {
                StopCoroutine(generateRandomNumOnDice_Coroutine);
            }

        }
    }
}
