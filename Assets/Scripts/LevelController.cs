using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    Pipe pipePrefab;

    int nPipes = 10;
    Pipe[] pipes;

    void Start()
    {
        pipes = new Pipe[nPipes];
        for(int i = 0; i < nPipes; i++)
        {
            Pipe pipe = Instantiate(pipePrefab, new Vector3(i * 10.0f, 0, 0), Quaternion.identity);
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
    }
}
