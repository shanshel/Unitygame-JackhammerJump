﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    Platform platformPrefab;

    [SerializeField]
    float startHeight = 200f, stepHeight = 5f, endHeight = 0f;

    [SerializeField]
    int minDistance = 10, maxDistance = 15;

    [SerializeField]
    int platformerCount,
        minPlatformsInDirection = 3, maxPlatformsInDirection = 7;


    [SerializeField]
    Material[] targetSurfacesMaterials;
    List<Platform> platforms = new List<Platform>();
    void Start()
    {
        bool negtiveDirection = false;
        bool isFlipped = false;
        int currentPlatInDirection = 0;
        int ranomChangeDirection = Random.Range(minPlatformsInDirection, maxPlatformsInDirection);
        int matIndex = 0;
        for (int x = 0; x < platformerCount; x++)
        {
            var position = new Vector3(0f, startHeight, 0f);
            if (x != 0)
            {
                position = platforms[x - 1].transform.position;
                position.y -= stepHeight;

                int xRand = Random.Range(minDistance, maxDistance);
                int zRand = Random.Range(minDistance, maxDistance);

                if (currentPlatInDirection == ranomChangeDirection)
                {
                    negtiveDirection = !negtiveDirection;
                    currentPlatInDirection = 0;
                    ranomChangeDirection = Random.Range(minPlatformsInDirection, maxPlatformsInDirection);
                    if (Random.Range(0f, 1f) > .6f)
                    {
                        isFlipped = !isFlipped;
                    }
                }

                if (negtiveDirection)
                {
                    if (isFlipped)
                    {
                        xRand *= -1;
                    }
                    zRand *= -1;
                }

                position.x += xRand;
                position.z += zRand;
            }
            var plat = Instantiate(platformPrefab, position, Quaternion.identity);
            if (matIndex >= targetSurfacesMaterials.Length)
                matIndex = 0;

            plat.setTargetMaterial(targetSurfacesMaterials[matIndex]);

            platforms.Add(plat);
            currentPlatInDirection++;
            matIndex++;
        }
    }

}
