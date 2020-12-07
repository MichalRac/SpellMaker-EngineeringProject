using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MatchPreparationPresenter : MonoBehaviour
{
    [SerializeField] private Transform playerSlotRoot;
    [SerializeField] private Transform opponentSlotRoot;
    [SerializeField] private Button matchupConfirmButton;

    public void Setup(UnityAction onMatchupConfirm)
    {
        matchupConfirmButton.onClick.AddListener(onMatchupConfirm);
    }

    private void OnDisable()
    {
        matchupConfirmButton.onClick.RemoveAllListeners();
    }

    public void CleanupCharacterSlots()
    {
        playerSlotRoot.DestroyAllChildren();
        opponentSlotRoot.DestroyAllChildren();
    }

    public CharacterSlotMaster CreateSlot(CharacterSlotMaster characterSlot, UnitOwner owner, bool isInitialSlot = false)
    {
        switch (owner)
        {
            case UnitOwner.Player:
                {
                    var slot = Instantiate(characterSlot, playerSlotRoot);
                    slot.Setup(owner, isInitialSlot);
                    return slot;
                }
            case UnitOwner.Opponent:
                {
                    var slot = Instantiate(characterSlot, opponentSlotRoot);
                    slot.Setup(owner, isInitialSlot);
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

