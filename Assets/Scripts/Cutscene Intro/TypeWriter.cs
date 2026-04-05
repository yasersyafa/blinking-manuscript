using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TypeWriter : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private AudioSource _audioSource;
    public AudioClip[] clips;

    [TextArea(3, 10)]
    public string fullText = "Humans often grapple with fleeting memories...";
    private const float LetterDelay = 0.05f;

    [Header("Scene Config")]
    public string sceneToLoadAfter = "";

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _textMesh = GetComponent<TextMeshProUGUI>();
        OverrideTextIfNeeded();
        StartCoroutine(TypeText());
    }

    private void OverrideTextIfNeeded()
    {
        if (GameStateManager.Ins == null) return;
        if (GameStateManager.Ins.currentState == GameStateManager.Ins.day3)
            fullText = "The next day...";
    }

    private IEnumerator TypeText()
    {
        // Set teks penuh dari awal, semua karakter disembunyikan dulu
        _textMesh.text = fullText;
        _textMesh.maxVisibleCharacters = 0;

        yield return new WaitForSeconds(1f);

        // Force rebuild supaya TMP tahu total karakter yang ada
        _textMesh.ForceMeshUpdate();
        int totalChars = _textMesh.textInfo.characterCount;

        for (int i = 0; i <= totalChars; i++)
        {
            _textMesh.maxVisibleCharacters = i;

            // Hanya play audio kalau karakter yang muncul bukan spasi
            if (i > 0)
            {
                char current = _textMesh.textInfo.characterInfo[i - 1].character;
                if (current != ' ')
                    _audioSource.PlayOneShot(GetRandomClip());
            }

            yield return new WaitForSeconds(LetterDelay);
        }

        yield return new WaitForSeconds(1f);
        OnTypeWriterFinished();
    }

    private void OnTypeWriterFinished()
    {
        if (GameStateManager.Ins != null &&
            GameStateManager.Ins.currentState is ITypeWriterHandler handler)
        {
            handler.OnTypeWriterFinished(GameStateManager.Ins);
            return;
        }

        if (!string.IsNullOrEmpty(sceneToLoadAfter))
        {
            SceneController.Ins.LoadScene(sceneToLoadAfter);
            return;
        }

        Debug.LogWarning("[TypeWriter] Tidak ada handler dan sceneToLoadAfter kosong!");
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}