﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Room component used for procedural generation of the game's rooms. Not used in the current version.
/// </summary>
public class Room : MonoBehaviour
{
    [Tooltip("Respectively: North, South, East, West")]
    [SerializeField] public GameObject[] doors = new GameObject[4];
    [SerializeField] public float[] distances = new float[4];
    [SerializeField] GameObject bottomL, topR;

    Vector2 bufferVector;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            if (doors[i])
                distances[i] = Vector2.Distance(transform.position, doors[i].transform.position);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Closing");
            Application.Quit();
        }
    }

    public GameObject GetRoom(RoomEntrances whichRoom)
    {
        return doors[(int)whichRoom];
    }

    public Vector2 GetBottomL() { return bottomL.transform.position; }
    public Vector2 GetTopR() { return topR.transform.position; }

    public void SetPosition(RoomEntrances entrance)
    {
        if (entrance == RoomEntrances.NORTH)
        {
            bufferVector.Set(transform.position.x, transform.position.y + distances[1]);
            Destroy(doors[1]);
        }
        else if (entrance == RoomEntrances.SOUTH)
        {
            bufferVector.Set(transform.position.x, transform.position.y - distances[0]);
            Destroy(doors[0]);
        }
        else if (entrance == RoomEntrances.EAST)
        {
            bufferVector.Set(transform.position.x + distances[2], transform.position.y);
            Destroy(doors[3]);
        }
        else if (entrance == RoomEntrances.WEST)
        {
            bufferVector.Set(transform.position.x - distances[3], transform.position.y);
            Destroy(doors[2]);
        }
        transform.position = bufferVector;
    }
}
