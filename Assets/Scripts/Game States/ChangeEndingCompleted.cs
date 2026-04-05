using UnityEngine;

public class ChangeEndingCompleted : SceneLoadState, IShutdownHandler
{
    protected override string TargetScene => "Main Scene";
    protected override float DelayAfterLoad => 1f;

    public void OnShutdownClicked(GameStateManager manager)
    {
        manager.SetState(manager.day3);
    }

    protected override void OnSceneReady(GameStateManager manager)
    {
        MainSceneManager.ins.canvasTable.SetActive(true);
        MainSceneManager.ins.shutdownScreen.SetActive(false);
        MainSceneManager.ins.screenShutdown.SetActive(false);
        MainSceneManager.ins.screenLocked.SetActive(false);
        MainSceneManager.ins.lockScreen.SetActive(false);
        MainSceneManager.ins.mxWriter.SetActive(true);
        MainSceneManager.ins.MxWriter.SetActive(true);
        MainSceneManager.ins.fileManager.SetActive(false);
        MainSceneManager.ins.FileManager.SetActive(false);

        var obj = GameObject.Find("You3");
        if (obj == null)
        {
            Debug.LogError("[ChangeEndingCompleted] 'You3' tidak ditemukan!");
            return;
        }
        obj.GetComponent<Dialogue>()?.TriggerDialogue();
    }
}
