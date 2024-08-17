using System.Collections;
using TMPro;
using UnityEngine;

public class Mindblink3Manager : MonoBehaviour
{
    public TextMeshProUGUI timerText, textToType;

    private string targetText = "senin selasa".ToLower(); // Teks target yang harus diketik oleh pemain
    private string currentTyped = ""; // Teks yang sudah diketik oleh pemain
    private bool gameStarted = false; // Menandakan apakah permainan sudah dimulai
    private float timeRemaining = 30f; // Waktu yang tersisa untuk mengetik
    private bool hasMistake = false; // Menandakan apakah ada kesalahan pada input pemain

    void Start()
    {
        textToType.text = GetColoredText(targetText, 0);
        timerText.text = timeRemaining.ToString();
        StartCoroutine(HandleInput());
        StartCoroutine(UpdateTimer());
    }

    // Coroutine untuk menangani input dari pemain
    private IEnumerator HandleInput()
    {
        while (true)
        {
            if (Input.anyKeyDown && !gameStarted)
            {
                if (Input.inputString.Length > 0)
                {
                    gameStarted = true;
                }
            }

            if (gameStarted)
            {
                foreach (char c in Input.inputString)
                {
                    if (currentTyped.Length < targetText.Length)
                    {
                        if (c == '\b') // Jika tombol backspace ditekan
                        {
                            if (currentTyped.Length > 0)
                            {
                                currentTyped = currentTyped.Substring(0, currentTyped.Length - 1);
                                hasMistake = false; // Reset kesalahan ketika pemain menghapus karakter
                            }
                        }
                        else
                        {
                            currentTyped += c;

                            // Periksa apakah input benar pada index saat ini
                            if (!hasMistake && currentTyped[currentTyped.Length - 1] != targetText[currentTyped.Length - 1])
                            {
                                hasMistake = true;
                            }
                        }

                        textToType.text = GetColoredText(targetText, currentTyped.Length);

                        if (currentTyped.Length == targetText.Length)
                        {
                            if (!hasMistake)
                            {
                                Debug.Log("WIN");
                            }
                            else
                            {
                                Debug.Log("Kesalahan terdeteksi, tapi belum kalah selama masih ada waktu.");
                            }
                            StopAllCoroutines();
                            break;
                        }
                    }
                }
            }
            yield return null;
        }
    }

    // Coroutine untuk memperbarui timer
    private IEnumerator UpdateTimer()
    {
        while (timeRemaining > 0)
        {
            if (gameStarted)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = $"{Mathf.Ceil(timeRemaining)}";

                if (timeRemaining <= 0)
                {
                    if (currentTyped.Length < targetText.Length || hasMistake)
                    {
                        Debug.Log("LOSE");
                    }
                    else
                    {
                        Debug.Log("WIN");
                    }
                    StopAllCoroutines();
                }
            }
            yield return null;
        }
    }

    // Fungsi untuk memberikan warna pada teks yang sudah dan belum diketik
    private string GetColoredText(string text, int correctCharsCount)
    {
        string coloredText = "";

        for (int i = 0; i < correctCharsCount; i++)
        {
            if (i < text.Length)
            {
                if (currentTyped[i] == text[i])
                {
                    // Jika karakter benar, beri warna putih
                    coloredText += $"<color=white>{text[i]}</color>";
                }
                else
                {
                    // Jika pemain menginputkan spasi tapi target bukan spasi
                    if (currentTyped[i] == ' ' && text[i] != ' ')
                    {
                        coloredText += $"<color=red>{text[i]}</color>";
                    }
                    // Jika target adalah spasi tapi pemain tidak mengetik spasi
                    else if (text[i] == ' ' && currentTyped[i] != ' ')
                    {
                        coloredText += $"<color=#FF00004D>{currentTyped[i]}</color>"; // Alpha 0.3
                    }
                    else
                    {
                        // Jika karakter salah, beri warna merah
                        coloredText += $"<color=red>{currentTyped[i]}</color>";
                    }
                }
            }
        }

        // Tambahkan sisa teks yang belum diketik dalam warna ungu muda
        if (correctCharsCount < text.Length)
        {
            coloredText += $"<color=#936EBC>{text.Substring(correctCharsCount)}</color>";
        }

        return coloredText;
    }
}
