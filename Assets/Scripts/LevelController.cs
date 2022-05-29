using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    FullPipe pipePrefab;

    int nPipes = 7;
    FullPipe[] pipes;

    float pipeDistance = 5.5f;

    int firstPipeIndex = 0;

    void Start()
    {
        pipes = new FullPipe[nPipes];
        for(int i = 0; i < nPipes; i++)
        {
            InstantiateFullPipe(new Vector3(i * pipeDistance, 0, 0), ref pipes[i]);
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for(int i = 0; i < nPipes; i++)
        {
            pipes[i].transform.Translate(new Vector3(-3.5f * deltaTime, 0, 0));
        }

        // check if first pipe is off screen
        Vector3 firstPipeScreenPos = Camera.main.WorldToScreenPoint(pipes[firstPipeIndex].transform.position);
        if (firstPipeScreenPos.x < -20.0f)
        {
            pipes[firstPipeIndex].gameObject.SetActive(false);
            Destroy(pipes[firstPipeIndex]);
            int lastPipeIndex = firstPipeIndex - 1;
            if (lastPipeIndex == -1)
                lastPipeIndex = nPipes - 1;
            InstantiateFullPipe(new Vector3(pipes[lastPipeIndex].transform.position.x + pipeDistance, 0, 0), ref pipes[firstPipeIndex]);
            firstPipeIndex = (firstPipeIndex + 1) % nPipes;
        }
    }

    void InstantiateFullPipe(Vector3 position, ref FullPipe instantiatedPipe)
    {
        instantiatedPipe = Instantiate(pipePrefab, position, Quaternion.identity);
        instantiatedPipe.CreateFullPipe(position);
    }
}
