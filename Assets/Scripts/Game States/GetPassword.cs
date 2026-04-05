using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetPassword : SceneLoadState
{
    protected override string TargetScene => "Main Scene";
    protected override float DelayAfterLoad => 1f;

    protected override void OnSceneReady(GameStateManager manager)
    {
        // Semua Awake() sudah jalan, aman akses MainSceneManager
        MainSceneManager.ins.bgBathroom.SetActive(true);

        var obj = GameObject.Find("GetPassword");
        if (obj == null)
        {
            Debug.LogError("[GetPassword] 'GetPassword' GameObject tidak ditemukan!");
            return;
        }
        obj.GetComponent<Dialogue>()?.TriggerDialogue();
    }
}
