using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [TextArea(2, 4)]
    public string[] dialogueLines =
    {
        "Gee, I do sure hope I win this game.",
        "What are we, some kind of Hunger Games?",
        "We have to keep moving forward",
        "Oh my god, there are so many traps!"
    };

    public KeyCode interactKey = KeyCode.E;
    public float dialogueDuration = 3.0f;

    private bool playerNearby;
    private string currentDialogue;
    private float dialogueTimer;

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(interactKey))
            SayRandomLine();

        if (dialogueTimer > 0.0f)
            dialogueTimer -= Time.deltaTime;
        else
            currentDialogue = string.Empty;
    }

    private void SayRandomLine()
    {
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogWarning($"NPC '{name}' has no dialogue lines.", this);
            return;
        }

        currentDialogue = dialogueLines[Random.Range(0, dialogueLines.Length)];
        dialogueTimer = dialogueDuration;
        Debug.Log($"{name}: {currentDialogue}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }

    private void OnGUI()
    {
        if (string.IsNullOrEmpty(currentDialogue))
            return;

        GUI.Box(new Rect(20.0f, Screen.height - 100.0f, Screen.width - 40.0f, 60.0f),
            $"{name}: {currentDialogue}");
    }
}
