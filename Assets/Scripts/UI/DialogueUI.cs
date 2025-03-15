using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance { get; private set; }
    public bool IsOpen { get; private set; }
    public UIManager uiManager;

    public Dialogue dialogue;
    public int dialogueIndex;

    [Header("UI Elements")]
    public Button nextButton;
    public GameObject dialogueGameObject;
    public TMP_Text speakerText;
    public TMP_Text chatText;
    public GameObject dialogueOptions;
    public GameObject optionPrefab;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        dialogueGameObject.SetActive(false);
        uiManager = GetComponent<UIManager>();
    }

    void Update()
    {
        if (!IsOpen) return;
        if (dialogue == null) return;

        speakerText.text = dialogue.lines[dialogueIndex].speaker;
        chatText.text = dialogue.lines[dialogueIndex].text;

    }
    public void OpenDialogueIU(Dialogue dialogue)
    {
        if (IsOpen) HideUI();
        IsOpen = true;
        uiManager.HideHUD();
        uiManager.player.actionsBlocked = true;
        this.dialogue = dialogue;
        dialogueIndex = -1;
        NextLine();

        dialogueGameObject.SetActive(true);

        nextButton.onClick.AddListener(NextLine);
    }
    public void HideUI()
    {
        nextButton.onClick.RemoveAllListeners();
        foreach (Transform child in dialogueOptions.transform)
        {
            child.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(child.gameObject);
        }
        IsOpen = false;
        dialogueGameObject.SetActive(false);

        if (!uiManager.playerInventoryVisible)
        {
            uiManager.player.actionsBlocked = false;
            uiManager.ShowHUD();
        }
    }
    public void TriggerOption(int index)
    {
        Debug.Log(index);
        dialogue.options[index].action.Invoke();

        if (dialogue.options[index].dialogues != null)
        {
            OpenDialogueIU(dialogue.options[index].dialogues);
        }
        else HideUI();
    }
    private void NextLine()
    {
        if (dialogueIndex < dialogue.lines.Count - 1)
        {
            dialogueIndex++;
        }
        if (dialogueIndex == dialogue.lines.Count - 1)
        {
            if (dialogueOptions.transform.childCount == 0)
            {
                for (int i = 0; i < dialogue.options.Count; i++)
                {
                    GameObject option = Instantiate(optionPrefab, dialogueOptions.transform);
                    int i2 = i;
                    option.GetComponent<Button>().onClick.AddListener(delegate { TriggerOption(i2); });
                    option.GetComponentInChildren<TMP_Text>().text = dialogue.options[i].optionText;
                }
            }
        }
    }
}
