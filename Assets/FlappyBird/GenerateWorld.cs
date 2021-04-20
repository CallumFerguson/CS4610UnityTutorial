using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public GameObject pipePrefab;
    public GameObject groundPrefab;
    public Transform birdTransform;

    private float _lastPipePosition = 0;
    private const float PipeSpacing = 8f;
    private const float TotalHeight = 20f;
    private const float PipeGapSize = 3f;

    private int _lastSegment = -1;

    private Queue<GameObject> _worldObjects = new Queue<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GenerateNewPipe(Random.Range(1f, 8f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        var segment = Mathf.FloorToInt(birdTransform.position.x / PipeSpacing);
        if (segment != _lastSegment)
        {
            GenerateNewPipe(Random.Range(1f, 8f));
            if (_worldObjects.Count > (3 * 8))
            {
                Destroy(_worldObjects.Dequeue());
                Destroy(_worldObjects.Dequeue());
                Destroy(_worldObjects.Dequeue());
            }
        }
        _lastSegment = segment;
    }

    private void GenerateNewPipe(float height)
    {
        var offset = _lastPipePosition + PipeSpacing;
        _lastPipePosition = offset;
        
        var bottomPipe = Instantiate(pipePrefab);
        bottomPipe.transform.localScale = new Vector3(1, height, 1);
        bottomPipe.transform.position = new Vector3(offset, height / 2f, 0);

        var topPipeHeight = TotalHeight - height - PipeGapSize;
        var topPipe = Instantiate(pipePrefab);
        topPipe.transform.localScale = new Vector3(1, topPipeHeight, 1);
        topPipe.transform.position = new Vector3(offset, TotalHeight - topPipeHeight / 2f, 0);

        var ground = Instantiate(groundPrefab);
        ground.transform.localScale = new Vector3(PipeSpacing, 1, 1);
        ground.transform.position = new Vector3(offset, -0.5f, 0);
        
        _worldObjects.Enqueue(bottomPipe);
        _worldObjects.Enqueue(topPipe);
        _worldObjects.Enqueue(ground);
    }
}