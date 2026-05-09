using System;
using Unity.VisualScripting;
using UnityEngine;

public class Golf : MonoBehaviour
{
    public int playerCount;
    public int gameplayers = 4;
    public int curPlayer = 0;
    public GameObject[] golf;
    public GameObject[] archive;
    public GameObject start;
    public GameObject end;
    public GameObject curplay;
    public GameObject off;
    [SerializeField] GameObject[] tem;
    public int stroke;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        golf[0].GetComponent<Ball>().turn = true;
        for (int i = 0; i < golf.Length; i++)
        {
            golf[i].GetComponent<Ball>().distances = Vector2.Distance(golf[i].transform.position, end.transform.position);
        }
        curplay.GetComponent<SpriteRenderer>().color = golf[0].GetComponent<SpriteRenderer>().color;
        stroke = 1;
        golf[0].transform.position = start.transform.position;
        playerCount = gameplayers;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (golf[curPlayer].GetComponent<Ball>().turn == false)
        {


            calculatedist();
            curPlayer++;
            
            if (curPlayer > playerCount - 1)
            {


                curPlayer = 0;
                WhoOut();
                stroke++;
                for (int i = 0; i < playerCount; i++)
                {
                    golf[i].GetComponent<CircleCollider2D>().enabled = false;
                }
                golf[0].GetComponent<Ball>().turn = true;
            }

            if (stroke == 1)
            {
                golf[curPlayer].transform.position = start.transform.position;
            }

            curplay.GetComponent<SpriteRenderer>().color = golf[curPlayer].GetComponent<SpriteRenderer>().color;
            golf[curPlayer].GetComponent<Ball>().turn = true;
        }

    }
    public void calculatedist()
    {
        int k = playerCount;
        for (int i = 0;i < playerCount; i++)
        {
            
            golf[i].GetComponent<Ball>().distances = Vector2.Distance(golf[i].transform.position, end.transform.position);
            if (golf[i].GetComponent<Ball>().inHole)
            {
                k--;
                golf[i].GetComponent<Ball>().distances = 0;
                    }

        }
        if (k == 1)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (golf[i].GetComponent<Ball>().inHole == false)
                {
                    golf[i].GetComponent<Ball>().stroke++;
                }
            }
            calculatedist();
            WhoOut();
                EndLevel();
        }
    }
    public void WhoOut()
    {
        int roundOut = 0;
        int remain = playerCount;
        int m = playerCount - 1;

        for (int i = 0; i < playerCount; i++)
        {
            
            if (golf[i].GetComponent<Ball>().distances == 0)
            {
                roundOut++;
                playerCount--;
            }
            

        }
        if (roundOut == 0)
        {
            playerCount--;
        }
        
        tem = new GameObject[gameplayers];
        int j = playerCount;
        for (int i = 0; i < remain; i++)
        {
            j = playerCount;
            for (int j2 = 0; j2 < remain; j2++)
            {

                if (golf[i].GetComponent<Ball>().distances < golf[j2].GetComponent<Ball>().distances && i != j2)
                {
                    j--;
                }

            }
            if (j > 0 && roundOut > 0)
            {

                tem[j-1] = golf[i];
            }
            else if (roundOut > 0)
            {
                tem[m] = golf[i];
                m--;
                golf[i].SetActive(false);
            }

            if (j >= 0 && j != playerCount && roundOut < 1)
            {

                tem[j] = golf[i];

            }
            else if (roundOut < 1)
            {
                tem[m] = golf[i];
                m--;
                golf[i].SetActive(false);
                
            }
        }
        for (int i = 0;i < gameplayers; i++)
        {
            if (i < playerCount)
            {
                tem[i].GetComponent<Ball>().stroke++;
                golf[i] = tem[playerCount - i - 1];
            }
            else
            {
                if (tem[i] != null)
                {
                    golf[i] = tem[i];
                }
            }
            
            
        }
        if (playerCount == 1)
        {
            EndLevel();
        }
    }
    public void EndLevel()
    {
        Debug.Log("Round Over");
        playerCount = gameplayers;

        golf[0].GetComponent<Ball>().points++;

        for (int i = 0; i < golf.Length; i++)
        {
            golf[i].GetComponent<Ball>().stroke = 1;
            golf[i].GetComponent<Ball>().distances = Vector2.Distance(golf[i].transform.position, end.transform.position);
            golf[0].GetComponent<Ball>().turn = true;
            golf[i].SetActive(true);
            golf[i].transform.position = off.transform.position;
            golf[i].GetComponent<CircleCollider2D>().enabled = false;
            if (i < playerCount)
            {
                golf[i].GetComponent<Ball>().inHole = false;
            }
        }

        curplay.GetComponent<SpriteRenderer>().color = golf[0].GetComponent<SpriteRenderer>().color;
        stroke = 0;
        golf[0].transform.position = start.transform.position;
    }

}
