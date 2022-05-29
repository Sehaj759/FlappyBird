using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPipe : MonoBehaviour
{
    [SerializeField]
    Pipe pipePrefab;

    float gapSize = 40.0f;

    public void CreateFullPipe(Vector3 position)
    {
        transform.position = position;

        Pipe top = Instantiate(pipePrefab, Vector3.zero, Quaternion.identity, transform);
        top.Initialize(1, true);

        Pipe bottom = Instantiate(pipePrefab, Vector3.zero, Quaternion.identity, transform);
        bottom.Initialize(3, false);

        top.transform.localPosition = new Vector3(0, 4.0f, 0);
        bottom.transform.localPosition = new Vector3(0, -4.0f, 0);
    }
}
