using UnityEngine;

public class SendDraft : SceneLoadState
{
    protected override string TargetScene => "Main Scene";
    protected override float DelayAfterLoad => 0.5f;
    public bool isCorrectDraft = false;
    private bool _isDone = false;

    protected override void OnSceneReady(GameStateManager manager)
    {
        MainSceneManager.ins.canvasFocused.SetActive(false);
        MainSceneManager.ins.canvasTable.SetActive(false);
        MainSceneManager.ins.lockScreen.SetActive(false);
        MainSceneManager.ins.screenLocked.SetActive(false);
        MainSceneManager.ins.mxWriter.SetActive(false);
        MainSceneManager.ins.MxWriter.SetActive(false);
        MainSceneManager.ins.FileManager.SetActive(true);
        MainSceneManager.ins.fileManager.SetActive(true);
    }

    public override void OnExecute(GameStateManager manager)
    {
        base.OnExecute(manager); // handle scene load + delay

        if (isCorrectDraft && !_isDone)
        {
            _isDone = true;
            var obj = GameObject.Find("DraftSendComplete");
            if (obj == null)
            {
                Debug.LogError("[SendDraft] 'DraftSendComplete' tidak ditemukan!");
                return;
            }
            obj.GetComponent<Dialogue>()?.TriggerDialogue();
        }

        if (_isDone && DialogueManager.ins.isDone)
            manager.SetState(manager.day2);
    }

    public override void OnExit(GameStateManager manager)
    {
        base.OnExit(manager); // wajib panggil base untuk unsubscribe
        _isDone = false;
        isCorrectDraft = false;
    }
}