using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Transform ground;
    public Transform groundParent;
    public Transform groundSpawnStartingPosition;
    public GameObject extraGround;
    public Transform runner;
    public List<Transform> groundPieces = new List<Transform>();
    public float runnerMaxDistance;
    public float rotatingSpeed = 0.5f;
    private SpriteRenderer groundRenderer;
    private float groundWidth;


    void Awake() {
        ground = GetComponent<Transform>();
        groundRenderer = extraGround.GetComponent<SpriteRenderer>();
        groundWidth = groundRenderer.bounds.extents.x;
        print("width: " + groundWidth);
    }

    void Update(){
        var rotation = 0f;
        print(RunnerCloseToEdge());
        if(RunnerCloseToEdge()) {
            SpawnGround(CalculatePositionOfNextGround());
        }
        //AdjustRunnerTransform();
    }

    bool RunnerCloseToEdge(){
        if(Vector3.Distance(runner.position, groundPieces[groundPieces.Count - 1].position) < runnerMaxDistance)
            return true;
        else
            return false;

    }

    void SpawnGround(Vector3 spawnPosition){
        var ground = Instantiate(extraGround, spawnPosition, Quaternion.identity, groundParent) as GameObject;
        groundPieces.Add(ground.GetComponent<Transform>());
    }

    Vector3 CalculatePositionOfNextGround(){
        var lastPiece = groundPieces[groundPieces.Count - 1];
        return new Vector3(lastPiece.position.x + groundWidth, lastPiece.position.y, lastPiece.position.y);
    }

    void AdjustRunnerTransform(){
        var pieceToAdjustTo = groundPieces[groundPieces.Count - 5];
        runner.rotation = pieceToAdjustTo.rotation;
        runner.position = new Vector3(runner.position.x, pieceToAdjustTo.position.y + 1.5f, runner.position.z);

    }
}
