using UnityEngine;

[RequireComponent (typeof(Dialogue))]
public class NPCEntity : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;

    private void Start()
    {
        dialogue = GetComponent<Dialogue>();
    }
    public void Interact(Player player)
    {
        dialogue.StartDialogue();
    }
}