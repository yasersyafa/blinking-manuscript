using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingGame : MonoBehaviour
{
    public GameObject failure;
    public Dialogue winDialogue;
    [Space]
    public TextMeshProUGUI timerText, textToType;


    // Teks target yang harus diketik oleh pemain
    private string targetText = "The shadows of the past lingered, casting doubts on everything he thought he knew. In the silence that followed, memories began to resurface, fragments of a truth he had long tried to forget. As the clock struck midnight, he realized that the line between reality and delusion had already started to blur.".ToLower();
    private bool isCorrect = true;
    private string currentTyped = ""; // Teks yang sudah diketik oleh pemain
    private bool gameStarted = false; // Menandakan apakah permainan sudah dimulai
    private float timeRemaining = 100f; // Waktu yang tersisa untuk mengetik
    // private bool isCorrect = true; // Penanda untuk memeriksa apakah input terakhir benar

    void Start()
    {
        
        // Menampilkan teks target yang harus diketik dengan format warna awal
        textToType.text = GetColoredText(targetText, 0);
        timerText.text = timeRemaining.ToString(); // Menampilkan waktu yang tersisa
        StartCoroutine(HandleInput()); // Memulai coroutine untuk menangani input
        StartCoroutine(UpdateTimer()); // Memulai coroutine untuk memperbarui timer
    }

    // Coroutine untuk menangani input dari pemain
    private IEnumerator HandleInput()
    {
        while (true)
        {
            if (Input.anyKeyDown && !gameStarted)
            {
                // Memulai permainan hanya jika tombol keyboard ditekan
                if (Input.inputString.Length > 0)
                {
                    gameStarted = true; // Permainan dimulai
                }
            }

            if (gameStarted)
            {
                foreach (char c in Input.inputString)
                {
                    if (currentTyped.Length < targetText.Length)
                    {
                        if (c == '\b') // Memeriksa jika tombol yang ditekan adalah backspace
                        {
                            if (currentTyped.Length > 0)
                            {
                                // Menghapus karakter terakhir yang diketik
                                currentTyped = currentTyped.Substring(0, currentTyped.Length - 1);
                                 // Reset penanda kesalahan
                                isCorrect = true;
                            }
                        }
                        else if(isCorrect)// Hanya menerima input baru jika karakter sebelumnya benar
                        {
                            if (c == targetText[currentTyped.Length])
                            {
                                // Menambahkan karakter yang benar ke teks yang sudah diketik
                                currentTyped += c;
                                 // Tetap benar
                                isCorrect = true;
                            }
                            else
                            {
                                // Menambahkan karakter yang salah dan menandai sebagai salah
                                currentTyped += c;
                                // Tandai sebagai salah
                                isCorrect = false;
                            }
                        }

                        // Perbarui teks yang ditampilkan sesuai dengan karakter yang sudah diketik
                        textToType.text = GetColoredText(targetText, currentTyped.Length);
                        if (currentTyped == targetText)
                        {
                            winDialogue.TriggerDialogue();
                            // GameStateManager.Ins.mindblink3.isWin = true;
                            StopAllCoroutines(); // Hentikan timer dan input handling
                            break;
                        }
                    }
                }
            }
            yield return null; // Menunggu frame berikutnya
        }
    }

    // Coroutine untuk memperbarui timer
    private IEnumerator UpdateTimer()
    {
        while (timeRemaining > 0)
        {
            if (gameStarted)
            {
                // Kurangi waktu yang tersisa
                timeRemaining -= Time.deltaTime;
                timerText.text = $"{Mathf.Ceil(timeRemaining)}"; // Tampilkan waktu yang tersisa

                if (timeRemaining <= 0)
                {
                    // Jika waktu habis dan teks belum selesai diketik
                    if (currentTyped.Length < targetText.Length)
                    {
                        failure.SetActive(true);
                    }
                    StopAllCoroutines(); // Hentikan input handling dan timer
                }
            }
            yield return null; // Menunggu frame berikutnya
        }
    }

    // Fungsi untuk memberikan warna pada teks yang sudah dan belum diketik
    private string GetColoredText(string text, int correctCharsCount)
    {
        string coloredText = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (i < correctCharsCount)
            {
                if (text[i] == currentTyped[i])
                {
                    // Jika karakter benar, beri warna putih
                    coloredText += $"<color=white>{text[i]}</color>";
                }
                else
                {
                    // Jika karakter salah, beri warna merah
                    coloredText += $"<color=red>{text[i]}</color>";
                }
            }
            else
            {
                // Untuk karakter yang belum diketik, beri warna ungu muda
                coloredText += $"<color=#936EBC>{text[i]}</color>";
            }
        }
        return coloredText;
    }

    public void RestartGame()
    {
        GameStateManager.Ins.SetState(GameStateManager.Ins.mindblink3);
    }

    // version 2
    
    // [Space]
    // public TextMeshProUGUI timerText, textToType;

    // private string targetText = "The shadows of the past lingered, casting doubts on everything he thought he knew. In the silence that followed, memories began to resurface, fragments of a truth he had long tried to forget. As the clock struck midnight, he realized that the line between reality and delusion had already started to blur.".ToLower(); // Contoh teks target
    // private string currentTyped = ""; // Teks yang sudah diketik oleh pemain
    // private bool gameStarted = false; // Menandakan apakah permainan sudah dimulai
    // private float timeRemaining = 30f; // Waktu yang tersisa untuk mengetik
    // private bool isCorrect = true; // Penanda untuk memeriksa apakah input terakhir benar

    // void Start()
    // {
    //     // Menampilkan teks target yang harus diketik dengan format warna awal
    //     textToType.text = GetColoredText(targetText, 0);
    //     timerText.text = timeRemaining.ToString(); // Menampilkan waktu yang tersisa
    //     StartCoroutine(HandleInput()); // Memulai coroutine untuk menangani input
    //     StartCoroutine(UpdateTimer()); // Memulai coroutine untuk memperbarui timer
    // }

    // // Coroutine untuk menangani input dari pemain
    // private IEnumerator HandleInput()
    // {
    //     while (true)
    //     {
    //         if (Input.anyKeyDown && !gameStarted)
    //         {
    //             // Memulai permainan hanya jika tombol keyboard ditekan
    //             if (Input.inputString.Length > 0)
    //             {
    //                 gameStarted = true; // Permainan dimulai
    //             }
    //         }

    //         if (gameStarted)
    //         {
    //             foreach (char c in Input.inputString)
    //             {
    //                 if (currentTyped.Length < targetText.Length)
    //                 {
    //                     if (c == '\b') // Memeriksa jika tombol yang ditekan adalah backspace
    //                     {
    //                         if (currentTyped.Length > 0)
    //                         {
    //                             // Menghapus karakter terakhir yang diketik
    //                             currentTyped = currentTyped.Substring(0, currentTyped.Length - 1);
    //                             isCorrect = true; // Reset penanda kesalahan
    //                         }
    //                     }
    //                     else if (isCorrect) // Hanya menerima input baru jika karakter sebelumnya benar
    //                     {
    //                         if (c == targetText[currentTyped.Length])
    //                         {
    //                             // Menambahkan karakter yang benar ke teks yang sudah diketik
    //                             currentTyped += c;
    //                         }
    //                         else
    //                         {
    //                             // Menandai kesalahan dan menghentikan input lebih lanjut
    //                             currentTyped += c;
    //                             isCorrect = false; // Tandai sebagai salah
    //                         }
    //                     }

    //                     // Perbarui teks yang ditampilkan sesuai dengan karakter yang sudah diketik
    //                     textToType.text = GetColoredText(targetText, currentTyped.Length);
    //                     if (currentTyped.Length == targetText.Length && isCorrect)
    //                     {
    //                         winDialogue.TriggerDialogue();
    // //                         if(DialogueManager.ins.isDone)
    // //                         {
    // //                             GameStateManager.Ins.mindblink3.isWin = true;
    // //                         }
    //                         StopAllCoroutines(); // Hentikan timer dan input handling
    //                         break;
    //                     }
    //                 }
    //                 else if (isCorrect)
    //                 {
    //                     // Jika pemain mengetik lebih dari yang diperlukan
    //                     currentTyped += c;
    //                     isCorrect = false; // Tandai kesalahan
    //                 }

    //                 // Perbarui teks yang ditampilkan dengan memperhitungkan kesalahan
    //                 textToType.text = GetColoredText(targetText, currentTyped.Length);
    //             }
    //         }
    //         yield return null; // Menunggu frame berikutnya
    //     }
    // }

    // // Coroutine untuk memperbarui timer
    // private IEnumerator UpdateTimer()
    // {
    //     while (timeRemaining > 0)
    //     {
    //         if (gameStarted)
    //         {
    //             // Kurangi waktu yang tersisa
    //             timeRemaining -= Time.deltaTime;
    //             timerText.text = $"{Mathf.Ceil(timeRemaining)}"; // Tampilkan waktu yang tersisa

    //             if (timeRemaining <= 0)
    //             {
    //                 // Jika waktu habis dan teks belum selesai diketik
    //                 if (currentTyped.Length < targetText.Length)
    //                 {
    //                     failure.SetActive(true);
    //                 }
    //                 StopAllCoroutines(); // Hentikan input handling dan timer
    //             }
    //         }
    //         yield return null; // Menunggu frame berikutnya
    //     }
    // }

    // // Fungsi untuk memberikan warna pada teks yang sudah dan belum diketik
    // private string GetColoredText(string text, int correctCharsCount)
    // {
    //     string coloredText = "";
    //     for (int i = 0; i < text.Length; i++)
    //     {
    //         if (i < currentTyped.Length)
    //         {
    //             if (currentTyped[i] == text[i])
    //             {
    //                 // Jika karakter benar, beri warna putih
    //                 coloredText += $"<color=white>{text[i]}</color>";
    //             }
    //             else
    //             {
    //                 // Jika karakter salah, beri warna merah
    //                 coloredText += $"<color=red>{currentTyped[i]}</color>";
    //             }
    //         }
    //         else
    //         {
    //             // Untuk karakter yang belum diketik, beri warna ungu muda
    //             coloredText += $"<color=#936EBC>{text[i]}</color>";
    //         }
    //     }

    //     // Menambahkan sisa karakter yang diketik oleh pemain (jika lebih dari target text) dengan warna merah
    //     if (currentTyped.Length > text.Length)
    //     {
    //         coloredText += $"<color=red>{currentTyped.Substring(text.Length)}</color>";
    //     }

    //     return coloredText;
    // }

    // version 3
    
}