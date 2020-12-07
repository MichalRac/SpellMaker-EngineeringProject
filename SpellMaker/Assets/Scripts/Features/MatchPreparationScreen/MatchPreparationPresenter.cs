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

    public void SetAddCharacterButton(UnitOwner owner, bool value)
    {
        if (owner == UnitOwner.Player)
            addPlayerCharacterButton.gameObject.SetActive(value);

        else if (owner == UnitOwner.Opponent)
            addOpponentCharacterButton.gameObject.SetActive(value);

        else
            Debug.LogError($"[MatchPreparationPresenter] Unexpected unit owner {owner}");
    }

    public CharacterSlotMaster CreateSlot(int slotID, CharacterSlotMaster characterSlot, UnitOwner owner, UnityAction<UnitOwner, int> onSlotRemovedCallback)
    {
        switch (owner)
        {
            case UnitOwner.Player:
                {
                    var slot = Instantiate(characterSlot, playerSlotRoot);
                    slot.Setup(slotID, owner, onSlotRemovedCallback);
                    return slot;
                }
            case UnitOwner.Opponent:
                {
                    var slot = Instantiate(characterSlot, opponentSlotRoot);
                    slot.Setup(slotID, owner, onSlotRemovedCallback);
                    return slot;
                }
            case UnitOwner.None:
            case UnitOwner.Ally:
                Debug.LogError($"[MatchPreparationPresenter] Trying to create character slot for unsupported owner {owner}");
                break;
        }

        return null;
    }
}

