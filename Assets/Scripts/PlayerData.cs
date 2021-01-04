using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int level;
    public float[] playerPos;
    public float[] playerRot;

    public PlayerData(Player player)
    {
        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
        playerPos[2] = player.transform.position.z;

        playerRot = new float[4];
        playerRot[0] = player.transform.rotation.x;
        playerRot[1] = player.transform.rotation.y;
        playerRot[2] = player.transform.rotation.z;
        playerRot[3] = player.transform.rotation.w;

    }
}
