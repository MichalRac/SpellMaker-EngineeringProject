using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MatchPreparationPresenter : MonoBehaviour
{
    [SerializeField] private Transform playerSlotRoot;
    [SerializeField] private Transform opponentSlotRoot;
    [SerializeField] private Button matchupConfirmButton;

    [SerializeField] private Button addPlayerCharacterButton;
    [SerializeField] private Button addOpponentCharacterButton;

    public void Setup(UnityAction onMatchupConfirm, UnityAction onPlayerAdded, UnityAction onOpponentAdded)
    {
        matchupConfirmButton.onClick.AddListener(onMatchupConfirm);
        addPlayerCharacterButton.onClick.AddListener(onPlayerAdded);
        addOpponentCharacterButton.onClick.AddListener(onOpponentAdded);
    }

    private void OnDisable()
    {
        matchupConfirmButton.onClick.RemoveAllListeners();
        addPlayerCharacterButton.onClick.RemoveAllListeners();
        addOpponentCharacterButton.onClick.RemoveAllListeners();
    }

    public void CleanupCharacterSlots()
    {
        playerSlotRoot.DestroyAllChildren();
        opponentSlotRoot.DestroyAllChildren();
    }

    public void SetAddCharacterButton(UnitRelativeOwner owner, bool value)
    {
        if (owner == UnitRelativeOwner.Self)
            addPlayerCharacterButton.gameObject.SetActive(value);

        else if (owner == UnitRelativeOwner.Opponent)
            addOpponentCharacterButton.gameObject.SetActive(value);

        else
            Debug.LogError($"[MatchPreparationPresenter] Unexpected unit owner {owner}");
    }

    public CharacterSlotMaster CreateSlot(int slotID, CharacterSlotMaster characterSlot, UnitRelativeOwner owner, UnityAction<UnitRelativeOwner, int> onSlotRemovedCallback)
    {
        switch (owner)
        {
            case UnitRelativeOwner.Self:
                {
                    var slot = Instantiate(characterSlot, playerSlotRoot);
                    slot.Setup(slotID, owner, onSlotRemovedCallback);
                    return slot;
                }
            case UnitRelativeOwner.Opponent:
                {
                    var slot = Instantiate(characterSlot, opponentSlotRoot);
                    slot.Setup(slotID, owner, onSlotRemovedCallback);
                    return slot;
                }
            case UnitRelativeOwner.None:
            case UnitRelativeOwner.Ally:
                Debug.LogError($"[MatchPreparationPresenter] Trying to create character slot for unsupported owner {owner}");
                break;
        }

        return null;
    }
}

