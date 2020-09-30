using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BaseCharacterPresenter : MonoBehaviour, IUnit
{
    // Start is called before the first frame update
    [SerializeField] private GameObject selectionProjector;
    [SerializeField] private GameObject highlightProjector;
    [SerializeField] private GameObject shadowProjector;
    [SerializeField] private SkinnedMeshRenderer characterMeshRenderer;
    [SerializeField] private Material materialToSetup;

    public void Setup(int material)
    {
        UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Material> buf;
        switch (material)
        {
            case 1:
                buf = Addressables.LoadAssetAsync<Material>("GenericChar");
                buf.Completed += Buf_Completed;
                break;
            case 2:
                buf = Addressables.LoadAssetAsync<Material>("AllyChar");
                buf.Completed += Buf_Completed;
                break;
            case 3:
                buf = Addressables.LoadAssetAsync<Material>("EnemyChar");
                buf.Completed += Buf_Completed;
                break;
        }


    }

    private void Buf_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Material> obj)
    {
        characterMeshRenderer.material = obj.Result;
    }

    public void SetSelect(bool value)
    {
        selectionProjector.gameObject.SetActive(value);
    }

    public void SetHighlight(bool value)
    {
        highlightProjector.gameObject.SetActive(value);
        shadowProjector.gameObject.SetActive(!value);
    }
}
