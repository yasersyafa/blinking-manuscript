/// <summary>
/// TypingGame.cs
/// A Unity typing minigame replicating Monkeytype's input mechanics,
/// extended with narrative-game strict rules per game designer feedback:
///
/// Input rules:
///   - Correct characters shown in white, incorrect in red (target char displayed)
///   - Extra characters (typed beyond word length) accepted and shown in smaller red
///   - Missing characters (word skipped before completion) shown with underline
///   - Backspace removes last character in active word; if word is empty,
///     moves BACK to the previous word (restores it for re-editing)
///   - Space advances to next word ONLY if the current word is 100% correct
///     (no incorrect chars, no extra chars, no missing chars)
///   - Timer starts on first keystroke
///   - Win condition: space pressed after last word with no errors
///   - Lose condition: timer runs out before all words are completed
///
/// Visual:
///   - A blinking caret "|" is injected into the rich-text string at the
///     current typing position — no extra UI objects required
/// </summary>

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingGame : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Inspector References
    // -------------------------------------------------------------------------

    [Header("UI References")]
    public GameObject failure;         // Failure screen activated on timeout
    public Dialogue winDialogue;       // Dialogue triggered on win
    public RectTransform caret;
    public GameObject backButton;

    [Space]
    public TextMeshProUGUI timerText;  // Displays remaining time
    public TextMeshProUGUI textToType; // Displays colored + caret text

    // -------------------------------------------------------------------------
    // Game Settings
    // -------------------------------------------------------------------------

    [Header("Game Settings")]
    [SerializeField] private float timeRemaining = 100f; // Total allowed time in seconds

    [Header("Caret Settings")]
    [SerializeField] private float caretBlinkInterval = 0.53f; // Seconds per blink half-cycle

    // -------------------------------------------------------------------------
    // Color Constants (TMP rich text)
    // -------------------------------------------------------------------------

    private const string COLOR_CORRECT   = "white";   // Correctly typed character
    private const string COLOR_INCORRECT = "red";     // Incorrectly typed (shows target char)
    private const string COLOR_UNTYPED   = "#936EBC"; // Not yet reached by the player
    private const string COLOR_EXTRA     = "red";     // Typed beyond word length
    private const string COLOR_MISSING   = "#FF6B6B"; // Skipped before typing (underlined)
    private const string COLOR_CARET     = "white";   // Blinking caret "|"

    // -------------------------------------------------------------------------
    // Private State
    // -------------------------------------------------------------------------

    /// <summary>Target sentence split into individual words.</summary>
    private List<string> words = new List<string>();

    /// <summary>
    /// Per-word typed character history.
    /// typedWords[i] contains every character the player typed for words[i].
    /// </summary>
    private List<List<char>> typedWords = new List<List<char>>();

    /// <summary>Index of the word currently being typed.</summary>
    private int currentWordIndex = 0;

    /// <summary>
    /// Character position within the current word.
    /// Always equals typedWords[currentWordIndex].Count after any input event.
    /// </summary>
    private int currentCharIndex = 0;

    /// <summary>Whether the player has pressed any key yet (starts the timer).</summary>
    private bool gameStarted = false;

    /// <summary>Controls caret visibility for the blink animation.</summary>
    private bool caretVisible = true;

    // -------------------------------------------------------------------------
    // Unity Lifecycle
    // -------------------------------------------------------------------------

    void Start()
    {
        // Build the target word list from the narrative sentence
        string targetText =
            "The shadows of the past lingered, casting doubts on everything he thought he knew. " +
            "In the silence that followed, memories began to resurface, fragments of a truth he had long tried to forget. " +
            "As the clock struck midnight, he realized that the line between reality and delusion had already started to blur.";

        targetText = targetText.ToLower();

        foreach (string w in targetText.Split(' '))
        {
            if (w.Length > 0)
            {
                words.Add(w);
                typedWords.Add(new List<char>());
            }
        }

        // Initial render — all characters untyped, caret at word 0 position 0
        textToType.text = BuildColoredText();
        timerText.text  = Mathf.Ceil(timeRemaining).ToString();

        StartCoroutine(HandleInput());
        StartCoroutine(UpdateTimer());
        StartCoroutine(BlinkCaret());
    }

    // -------------------------------------------------------------------------
    // Input Coroutine
    // -------------------------------------------------------------------------

    /// <summary>
    /// Reads player keystrokes every frame.
    /// Routes each character to the appropriate handler:
    ///   '\b' → backspace (can go back to previous word if current is empty)
    ///   ' '  → space (advance only if current word is 100% correct)
    ///   else → regular character (always accepted, never blocked)
    /// </summary>
    private IEnumerator HandleInput()
    {
        while (true)
        {
            // Start game clock on first keystroke
            if (!gameStarted && Input.anyKeyDown && Input.inputString.Length > 0)
                gameStarted = true;

            if (gameStarted)
            {
                foreach (char c in Input.inputString)
                {
                    // All words already submitted — nothing left to process
                    if (currentWordIndex >= words.Count) break;

                    if (c == '\b')
                        HandleBackspace();
                    else if (c == ' ')
                        HandleSpace();
                    else if (c >= ' ') // Printable characters only (exclude control chars)
                        HandleCharacter(c);

                    // Refresh display after every keystroke and reset caret to visible
                    caretVisible        = true;
                    caret.gameObject.SetActive(true);  // reset ke visible saat mengetik
                    textToType.text = BuildColoredText();
                    UpdateCaretPosition();   
                    textToType.text     = BuildColoredText();
                }
            }

            yield return null;
        }
    }

    // -------------------------------------------------------------------------
    // Input Handlers
    // -------------------------------------------------------------------------

    /// <summary>
    /// Backspace logic (strict / narrative mode):
    ///   - If the active word has typed characters → remove the last one.
    ///   - If the active word is empty AND we are not on the first word →
    ///     move back to the previous word, placing the caret at its end
    ///     so the player can re-edit it.
    ///   - If at word 0 with an empty word → do nothing.
    /// </summary>
    private void HandleBackspace()
    {
        List<char> activeWord = typedWords[currentWordIndex];

        if (activeWord.Count > 0)
        {
            // Remove the last character from the current word
            activeWord.RemoveAt(activeWord.Count - 1);
            currentCharIndex = activeWord.Count;
        }
        else if (currentWordIndex > 0)
        {
            // Current word is empty — step back to the previous word
            currentWordIndex--;
            // Set caret to the end of whatever was typed in that word
            currentCharIndex = typedWords[currentWordIndex].Count;
        }
        // currentWordIndex == 0 and word empty: silently ignore
    }

    /// <summary>
    /// Space logic (strict / narrative mode):
    ///   - Ignored if the active word has no typed characters yet.
    ///   - Blocked if the active word has ANY error (incorrect char, extra char,
    ///     or missing/incomplete char). The player must fix all errors first.
    ///   - If the word is 100% correct → lock it and advance to the next word.
    ///   - If the last word is now completed → trigger win.
    /// </summary>
    private void HandleSpace()
    {
        // Ignore leading space — player must type at least one character first
        if (typedWords[currentWordIndex].Count == 0) return;

        // Block advancement until the current word is perfect
        if (ActiveWordHasError()) return;

        // Word is correct — advance to next word
        currentWordIndex++;
        currentCharIndex = 0;

        // Win condition: all words submitted and each was correct
        if (currentWordIndex >= words.Count)
            TriggerWin();
    }

    /// <summary>
    /// Appends a typed printable character to the current word.
    /// All characters are accepted unconditionally — no input is ever blocked:
    ///   - Within word bounds → will be compared visually in BuildColoredText()
    ///   - Beyond word bounds → marked as "extra" in BuildColoredText()
    /// </summary>
    private void HandleCharacter(char c)
    {
        typedWords[currentWordIndex].Add(c);
        currentCharIndex = typedWords[currentWordIndex].Count;
    }

    // -------------------------------------------------------------------------
    // Error Checking
    // -------------------------------------------------------------------------

    /// <summary>
    /// Returns true if the active word cannot be submitted via space.
    /// Conditions that count as an error:
    ///   1. Extra characters present (typed more than the word's length)
    ///   2. Word is incomplete (typed fewer characters than the word's length)
    ///   3. Any typed character does not match the corresponding target character
    /// </summary>
    private bool ActiveWordHasError()
    {
        string     targetWord = words[currentWordIndex];
        List<char> typed      = typedWords[currentWordIndex];

        // Extra characters
        if (typed.Count > targetWord.Length) return true;

        // Incomplete word (missing characters)
        if (typed.Count < targetWord.Length) return true;

        // Any character mismatch
        for (int i = 0; i < typed.Count; i++)
            if (typed[i] != targetWord[i]) return true;

        return false;
    }

    // -------------------------------------------------------------------------
    // Caret Blink Coroutine
    // -------------------------------------------------------------------------

    /// <summary>
    /// Toggles caretVisible at a fixed interval to produce a blinking cursor effect.
    /// Triggers a display refresh each half-cycle.
    /// Note: HandleInput() always resets caretVisible to true on each keystroke,
    /// so typing resets the blink phase and the caret stays solid while actively typing.
    /// </summary>
    private IEnumerator BlinkCaret()
    {
        while (true)
        {
            yield return new WaitForSeconds(caretBlinkInterval);
            caretVisible = !caretVisible;
            caret.gameObject.SetActive(caretVisible);
        }
    }

    private string CaretTag(string inner)
    {
        // Saat visible: normal | Saat invisible: size=0% → lebar 0, layout tidak berubah
        if (caretVisible)
            return $"<color={COLOR_CARET}>{inner}</color>";
        else
            return $"<color={COLOR_CARET}><size=0%>{inner}</size></color>";
    }

    // -------------------------------------------------------------------------
    // Timer Coroutine
    // -------------------------------------------------------------------------

    /// <summary>
    /// Counts down from timeRemaining once the game starts.
    /// Activates the failure screen if time reaches zero before all words
    /// are completed. Stops all coroutines on timeout.
    /// </summary>
    private IEnumerator UpdateTimer()
    {
        while (timeRemaining > 0)
        {
            if (gameStarted)
            {
                timeRemaining -= Time.deltaTime;

                // Ceiling so display reads "1" for the full last second, then "0"
                timerText.text = Mathf.Ceil(timeRemaining).ToString();

                if (timeRemaining <= 0)
                {
                    if (currentWordIndex < words.Count)
                        failure.SetActive(true);

                    StopAllCoroutines();
                    yield break;
                }
            }

            yield return null;
        }
    }

    // -------------------------------------------------------------------------
    // Text Rendering
    // -------------------------------------------------------------------------

    /// <summary>
    /// Builds the complete TMP rich-text string for the current game state.
    ///
    /// For each character slot across all words, applies the appropriate color tag:
    ///   Correct   → white        (typed[ci] == target[ci])
    ///   Incorrect → red          (typed[ci] != target[ci], shows target char)
    ///   Extra     → small red    (ci >= word.Length, shows typed char)
    ///   Missing   → dim red + underline  (word was advanced past without typing this)
    ///   Untyped   → purple       (not yet reached)
    ///
    /// Injects a blinking "|" caret at position (currentWordIndex, currentCharIndex).
    /// The caret is placed BEFORE the character at that index, or after the last
    /// character if the caret is at the end of all typed/extra slots.
    /// </summary>
    private string BuildColoredText()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int wi = 0; wi < words.Count; wi++)
        {
            string     targetWord = words[wi];
            List<char> typed      = typedWords[wi];

            // Slot count covers both normal chars and any extra chars typed
            int slotCount = Mathf.Max(targetWord.Length, typed.Count);

            for (int ci = 0; ci < slotCount; ci++)
            {
                // // Inject caret BEFORE the character at the active caret position
                // if (wi == currentWordIndex && ci == currentCharIndex)
                //     sb.Append(CaretTag("|"));

                bool hasTarget = ci < targetWord.Length;
                bool hasTyped  = ci < typed.Count;

                if (hasTyped && hasTarget)
                {
                    // Within word bounds — color by correctness, always show target char
                    if (typed[ci] == targetWord[ci])
                        sb.Append($"<color={COLOR_CORRECT}>{targetWord[ci]}</color>");
                    else
                        sb.Append($"<color={COLOR_INCORRECT}>{targetWord[ci]}</color>");
                }
                else if (hasTyped && !hasTarget)
                {
                    // Extra character beyond word length — show actual typed char, smaller
                    sb.Append($"<color={COLOR_EXTRA}><size=80%>{typed[ci]}</size></color>");
                }
                else if (!hasTyped && hasTarget)
                {
                    if (wi < currentWordIndex)
                    {
                        // Missing: word was advanced past before this char was typed
                        sb.Append($"<color={COLOR_MISSING}><u>{targetWord[ci]}</u></color>");
                    }
                    else
                    {
                        // Untyped: player hasn't reached this character yet
                        sb.Append($"<color={COLOR_UNTYPED}>{targetWord[ci]}</color>");
                    }
                }
            }

            // Caret at end of word — when caret position equals or exceeds all slots
            // (e.g. player typed the full word and is waiting to press space)
            // if (wi == currentWordIndex && currentCharIndex >= slotCount)
            //     sb.Append(CaretTag("|"));

            // Space separator between words
            if (wi < words.Count - 1)
            {
                // Completed words use white space; future words use untyped color
                string spaceColor = wi < currentWordIndex ? COLOR_CORRECT : COLOR_UNTYPED;
                sb.Append($"<color={spaceColor}> </color>");
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Hitung karakter index global (dari awal seluruh teks) untuk posisi caret saat ini,
    /// lalu pakai TMP_TextInfo untuk dapat koordinat pixel-nya.
    /// </summary>
    private void UpdateCaretPosition()
    {
        textToType.ForceMeshUpdate();
        TMP_TextInfo textInfo = textToType.textInfo;

        // Hitung global char index
        int globalIndex = 0;
        for (int wi = 0; wi < currentWordIndex; wi++)
            globalIndex += Mathf.Max(words[wi].Length, typedWords[wi].Count) + 1;
        globalIndex += currentCharIndex;

        // Clamp ke karakter valid terdekat
        int clampedIndex = Mathf.Clamp(globalIndex, 0, textInfo.characterCount - 1);

        if (textInfo.characterCount == 0) return;

        TMP_CharacterInfo charInfo = textInfo.characterInfo[clampedIndex];

        float xPos;
        if (globalIndex >= textInfo.characterCount)
            // Caret di akhir — pakai sisi kanan karakter terakhir
            xPos = charInfo.bottomRight.x;
        else
            xPos = charInfo.bottomLeft.x;

        // Gunakan ascender dan descender dari charInfo untuk tinggi dan posisi Y
        float yBottom = charInfo.descender;
        float yTop    = charInfo.ascender;

        // Posisikan caret: X dari karakter, Y dari bottom karakter tersebut
        caret.localPosition = new Vector3(xPos, yBottom + (yTop - yBottom), 0f);

        // Sesuaikan tinggi caret dengan tinggi baris karakter ini
        caret.sizeDelta = new Vector2(caret.sizeDelta.x, yTop - yBottom);
    }

    // -------------------------------------------------------------------------
    // Win / Restart
    // -------------------------------------------------------------------------

    /// <summary>
    /// Stops all coroutines and fires the win dialogue.
    /// Called when the player has correctly submitted every word.
    /// </summary>
    private void TriggerWin()
    {
        StopAllCoroutines();
        winDialogue.TriggerDialogue();
        backButton.SetActive(true);
    }

    /// <summary>
    /// Reloads the game state via GameStateManager.
    /// Wired to the UI restart button in the Inspector.
    /// </summary>
    public void RestartGame()
    {
        GameStateManager.Ins.SetState(GameStateManager.Ins.mindblink3);
    }

    public void OnBackButtonClicked()
    {
        var currentState = GameStateManager.Ins.currentState;
        if (currentState == GameStateManager.Ins.mindblink3)
        {
            var endingCompleted = GameStateManager.Ins.endingCompleted;
            GameStateManager.Ins.SetState(endingCompleted);
        }
    }
}