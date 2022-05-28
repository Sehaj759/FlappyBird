using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Pipe pipePrefab;

    int nPipes = 7;
    Pipe[] pipes;

    float pipeDistance = 5.5f;

    int firstPipeIndex = 0;

    void Start()
    {
        pipes = new Pipe[nPipes];
        for(int i = 0; i < nPipes; i++)
        {
            Pipe pipe = Instantiate(pipePrefab, new Vector3(i * pipeDistance, 0, 0), Quaternion.identity);
            pipes[i] = pipe;
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for(int i = 0; i < nPipes; i++)
        {
            pipes[i].transform.Translate(new Vector3(-3.5f * deltaTime, 0, 0));
        }

        // check if first pipe is on screen
        Vector3 firstPipeScreenPos = mainCamera.WorldToScreenPoint(pipes[firstPipeIndex].transform.position);
        if (firstPipeScreenPos.x < -20.0f)
        {
            pipes[firstPipeIndex].gameObject.SetActive(false);
            Destroy(pipes[firstPipeIndex]);
            int lastPipeIndex = firstPipeIndex - 1;
            if (lastPipeIndex == -1)
                lastPipeIndex = nPipes - 1;
            pipes[firstPipeIndex] = Instantiate(pipePrefab, new Vector3(pipes[lastPipeIndex].transform.position.x + pipeDistance, 0, 0), Quaternion.identity);
            firstPipeIndex = (firstPipeIndex + 1) % nPipes;
        }
    }
}
