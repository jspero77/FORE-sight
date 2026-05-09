using System;
using Unity.VisualScripting;
using UnityEngine;

public class Golf : MonoBehaviour
{
    public int playerCount;
    public int gameplayers = 4;
    public int curPlayer = 0;
    public GameObject[] golf;
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
        if (golf[curPlayer].GetComponent<Ball>().turn == true && golf[curPlayer].GetComponent<Ball>().shot == true)
        {
            calculatedist();
        }
            if (golf[curPlayer].GetComponent<Ball>().turn == false)
        {


            
            curPlayer++;
            calculatedist();

            if (curPlayer > playerCount - 1)
            {


                curPlayer = 0;
                WhoOut();
                stroke++;
                for (int i = 0; i < playerCount; i++)
                {
                    golf[i].GetComponent<CircleCollider2D>().enabled = false;
                }
                if(playerCount == 1)
                {
                    EndLevel();
                }
                golf[0].GetComponent<Ball>().turn = true;
                curPlayer = 0;
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
        int p = playerCount;
        for (int i = 0;i < playerCount; i++)
        {
            
            golf[i].GetComponent<Ball>().distances = Vector2.Distance(golf[i].transform.position, end.transform.position);
            if (golf[i].GetComponent<Ball>().inHole)
            {
                p--;
                golf[i].GetComponent<Ball>().distances = 10000;
                    }

        }
        if (p == 1)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (golf[i].GetComponent<Ball>().inHole == false)
                {
                    golf[i].GetComponent<Ball>().stroke++;
                }
            }
            WhoOut();
            EndLevel();
            curPlayer = 0;

        }
    }
    public void WhoOut()
    {
        int roundOut = 0;
        int remain = playerCount;
        int m = playerCount - 1;

        for (int i = 0; i < remain; i++)
        {
            
            if (golf[i].GetComponent<Ball>().inHole == true)
            {
                roundOut++;
                golf[i].transform.position = off.transform.position;
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

                if (golf[i].GetComponent<Ball>().distances > golf[j2].GetComponent<Ball>().distances && i != j2)
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
                golf[i].transform.position = off.transform.position;
            }

            if (j > 0 && roundOut < 1)
            {

                tem[j-1] = golf[i];

            }
            else if (roundOut < 1)
            {
                tem[m] = golf[i];
                m--;
                golf[i].transform.position = off.transform.position;
                
            }
        }
        for (int i = 0;i < gameplayers; i++)
        {
            if (i < playerCount)
            {
                tem[i].GetComponent<Ball>().stroke++;

                    golf[i] = tem[i];
       
            }
            else
            {
                if (tem[i] != null)
                {
                    golf[i] = tem[i];
                    golf[i].GetComponent <Ball>().inHole = true;
                }
            }
            
            
        }

            
        
    }
    public void EndLevel()
    {
        Debug.Log("Round Over");
        playerCount = gameplayers;

        golf[0].GetComponent<Ball>().points++;

        for (int i = 0; i < golf.Length; i++)
        {
            golf[i].SetActive(true);
            golf[i].GetComponent<Ball>().stroke = 1;
            golf[i].GetComponent<Ball>().turn = false;
            golf[i].GetComponent<Ball>().shot = true;
            golf[i].GetComponent<Ball>().distances = 10;
            golf[0].GetComponent<Ball>().turn = true;
            golf[0].GetComponent<Ball>().shot = false;
            golf[i].GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            golf[i].transform.position = off.transform.position;
            golf[i].GetComponent<CircleCollider2D>().enabled = false;
            golf[i].GetComponent<Ball>().inHole = false;
            curPlayer = 0;
            
        }

        curplay.GetComponent<SpriteRenderer>().color = golf[0].GetComponent<SpriteRenderer>().color;
        stroke = 1;
        golf[0].transform.position = start.transform.position;
    }

}
