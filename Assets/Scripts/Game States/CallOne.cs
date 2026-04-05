using UnityEngine;

public abstract class BaseCallState : IState, IPhoneHandler
{
    private RectTransform _rectTransform;
    private Vector3 _originalPosition;
    private float _countdown;

    public bool isDone;
    public bool isShaking;

    protected abstract string DialogueObjectName { get; }
    protected abstract IState NextState(GameStateManager manager);

    public virtual void OnEnter(GameStateManager manager)
    {
        _rectTransform = MainSceneManager.ins.handphone.GetComponent<RectTransform>();
        _originalPosition = _rectTransform.localPosition;
        _countdown = 0.7f;
        isDone = false;
        isShaking = true;

        var dialogueObj = GameObject.Find(DialogueObjectName);
        if (dialogueObj == null)
        {
            Debug.LogError($"[BaseCallState] GameObject '{DialogueObjectName}' tidak ditemukan!");
            return;
        }
        _dialogue = dialogueObj.GetComponent<Dialogue>();
    }

    public virtual void OnExecute(GameStateManager manager)
    {
        _countdown -= Time.deltaTime;

        if (_countdown <= 0 && isShaking)
        {
            ShakePhone();
            if (_countdown <= -1.2f && !isDone)
            {
                isDone = true;
                _dialogue?.TriggerDialogue();
            }
        }

        if (!isShaking)
        {
            SoundEffect.Ins.audioSource.loop = false;
            manager.SetState(NextState(manager));
        }
    }

    public virtual void OnExit(GameStateManager manager) { }

    private Dialogue _dialogue;

    private void ShakePhone()
    {
        SoundEffect.Ins.audioSource.PlayOneShot(SoundEffect.Ins.phone);
        SoundEffect.Ins.audioSource.loop = true;
        MainSceneManager.ins.handphone.transform.GetChild(1).gameObject.SetActive(true);
        _rectTransform.localPosition = _originalPosition + Random.insideUnitSphere * 5f;
    }

    public void OnPhoneStopClicked(GameStateManager manager)
    {
        SoundEffect.Ins.audioSource.Stop();
        var effect = MainSceneManager.ins.handphone.transform.GetChild(MainSceneManager.ins.handphone.transform.childCount - 1).gameObject;
        
        effect.SetActive(false);
        isShaking = false;
    }
}

public class CallOne : BaseCallState
{
    protected override string DialogueObjectName => "CallOne";
    protected override IState NextState(GameStateManager manager) => manager.publisherOne;
}
