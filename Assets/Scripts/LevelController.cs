using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    FullPipe pipePrefab;

    [SerializeField]
    BirdController bird;

    [SerializeField]
    GameObject GameOverUI;

    [SerializeField]
    TMP_Text scoreText;

    int nPipes = 7;
    FullPipe[] pipes;

    float pipeDistance = 5.5f;

    int firstPipeIndex = 0;

    int score = 0;
    bool gameOver = false;

    void Start()
    {
        pipes = new FullPipe[nPipes];
        InitPipes();
    }

    void Update()
    {
        if (!gameOver)
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < nPipes; i++)
            {
                pipes[i].transform.Translate(new Vector3(-3.5f * deltaTime, 0, 0));

                // Check if need to update score
                if (pipes[i].UpdateScore)
                {
                    pipes[i].UpdateScore = false;
                    score++;

                    scoreText.text = "Score: " + score.ToString();
                }
            }

            // check if first pipe is off screen
            Vector3 firstPipeScreenPos = Camera.main.WorldToScreenPoint(pipes[firstPipeIndex].transform.position);
            if (firstPipeScreenPos.x < -30.0f)
            {
                int lastPipeIndex = firstPipeIndex - 1;
                if (lastPipeIndex == -1)
                    lastPipeIndex = nPipes - 1;
                OffsetFullPipe(new Vector3(pipes[lastPipeIndex].transform.position.x + pipeDistance, 0, 0), ref pipes[firstPipeIndex]);
                firstPipeIndex = (firstPipeIndex + 1) % nPipes;
            }
        }

        gameOver = bird.GameOver;
        if (gameOver)
        {
            GameOverUI.SetActive(true);
        }
    }

    void InitPipes()
    {
        for (int i = 0; i < nPipes; i++)
        {
            pipes[i] = Instantiate(pipePrefab, Vector3.zero, Quaternion.identity);
            OffsetFullPipe(new Vector3(i * pipeDistance, 0, 0), ref pipes[i]);
        }
    }

    void OffsetFullPipe(Vector3 position, ref FullPipe instantiatedPipe)
    {
        instantiatedPipe.transform.position = position;
        instantiatedPipe.OffsetOrDisableFullPipe();
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = "Score: 0";
        GameOverUI.SetActive(false);
        for (int i = 0; i < nPipes; i++)
        {
            OffsetFullPipe(new Vector3(i * pipeDistance, 0, 0), ref pipes[i]);
        }
        gameOver = false;
        bird.ResetBird();
    }
}
