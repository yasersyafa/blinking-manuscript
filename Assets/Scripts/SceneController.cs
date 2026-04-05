using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Ins;
    private Animator animator;

    void Awake()
    {
        Ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(FadeTransition(scene));
    }

    private IEnumerator FadeTransition(string nameScene)
    {
        animator.SetTrigger("load");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nameScene);
    }
}

public abstract class SceneLoadState : IState
{
    private bool _sceneReady;
    private bool _initialized;

    protected abstract string TargetScene { get; }
    protected virtual float DelayAfterLoad => 1f;
    private float _countdown;

    public virtual void OnEnter(GameStateManager manager)
    {
        _sceneReady = false;
        _initialized = false;
        _countdown = DelayAfterLoad;

        // Subscribe ke event — jauh lebih aman dari polling nama scene
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneController.Ins.LoadScene(TargetScene);
    }

    public virtual void OnExecute(GameStateManager manager)
    {
        if (!_sceneReady) return;

        _countdown -= Time.deltaTime;
        if (_countdown <= 0 && !_initialized)
        {
            _initialized = true;
            OnSceneReady(manager);
        }
    }

    public virtual void OnExit(GameStateManager manager)
    {
        // Wajib unsubscribe — kalau lupa ini bisa jadi memory leak
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Dipanggil saat scene + semua Awake() sudah selesai
    protected abstract void OnSceneReady(GameStateManager manager);

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == TargetScene)
        {
            _sceneReady = true;
            // Unsubscribe setelah trigger — tidak perlu dengar lagi
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
