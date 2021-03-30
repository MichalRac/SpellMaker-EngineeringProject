using System.Collections;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] UnitManager unitManager;
    [SerializeField] PlayerActionManager playerActionManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameCompletedPopup gameCompletedPopup;

    public void Initialize(BaseBattleSceneArgs sceneArguments)
    {
        new WorldModelService();
        
        foreach(var unitIdentifier in sceneArguments.PlayerCharactersIdentifiers)
        {
            unitManager.SpawnUnit(unitIdentifier);
        }

        foreach (var unitIdentifier in sceneArguments.OpponentCharactersIdentifiers)
        {
            unitManager.SpawnUnit(unitIdentifier);
        }

        turnManager.PrepareQueue(unitManager.GetAllActiveCharacters());
        
        WorldModelService.Instance.Initialize( unitManager.PrepareUnitCopiesForWorldModel(), turnManager.queue, turnManager.active );

        StartCoroutine(BeginTurn());
    }



    private IEnumerator BeginTurn()
    {
        unitManager.UpdateStatus(out var removedUnits);
        turnManager.UpdateStatus(removedUnits);

        if(!unitManager.HasAnyCharacterLeft(0))
        {
            gameCompletedPopup.Show(false);
        }
        else if(!unitManager.HasAnyCharacterLeft(1))
        {
            gameCompletedPopup.Show(true);
        }
        else
        {
            var nextInQueue = turnManager.GetNextInQueue();
            var activeCharacter = unitManager.GetActiveCharacter(nextInQueue);
        
            cameraController.SwapSide(activeCharacter.Unit.UnitIdentifier.TeamId);

            if (activeCharacter.Unit.UnitIdentifier.TeamId == 0)
            {
                playerActionManager.BeginPlayerActionPhase(activeCharacter, () => 
                {
                    activeCharacter.ActivateActionEffects();
                    activeCharacter.RefreshLabel();
                    StartCoroutine(BeginTurn());
                });

            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                WorldModelService.Instance.Initialize(unitManager.PrepareUnitCopiesForWorldModel(), turnManager.queue, nextInQueue);
                
                new AbilitySequenceHandler(WorldModelService.Instance.CalculateGOBAction(activeCharacter.Unit, WorldModelService.Instance.GetCurrentWorldModelLayer()), () =>
                {
                    activeCharacter.ActivateActionEffects();
                    activeCharacter.RefreshLabel();

                    StartCoroutine(BeginTurn());
                }).Begin(); 

                /*
                 // GOAP approach
                 new AbilitySequenceHandler(WorldModelService.Instance.WIP_CalculateGOAPAction(activeCharacter.Unit), () =>
                {
                    activeCharacter.ActivateActionEffects();
                    activeCharacter.RefreshLabel();

                    StartCoroutine(BeginTurn());
                }).Begin(); 
                */
            };
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}
