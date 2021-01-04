using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void Save()
    {
        SaveSystem.SavePlayer(this);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 pos;

        pos.x = data.playerPos[0];
        pos.y = data.playerPos[1];
        pos.z = data.playerPos[2];

        transform.position = pos;

        Quaternion rot;

        rot.x = data.playerRot[0];
        rot.y = data.playerRot[1];
        rot.z = data.playerRot[2];
        rot.w = data.playerRot[3];

        transform.rotation = rot;
    }
}
