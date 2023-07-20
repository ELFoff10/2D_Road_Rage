using UnityEngine;

public class MobileDataPlatform : AbstractDataPlatform
{
    private JsonSerialization<SubData> _jsonSerializationSubData = new((PRENAME + nameof(SubDataModel) + ".json"));

    #region Save

    public override void SaveSubData() => _jsonSerializationSubData.Serialization(SubDataModel.GetSubData());

    #endregion

    #region Load

    public override void LoadData()
    {
        LoadSubDataModel(_jsonSerializationSubData.DeSerialization());
    }

    private void LoadSubDataModel((bool result, SubData subData) result)
    {
        if (result.result)
            SubDataModel.SetSubData(result.subData);
        else
            SubDataModel.SetAndInitEmptySubData(result.subData);

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log((PRENAME + nameof(SubData)) + " EndLoad");
#endif

        IsSubDataModelLoaded = true;
    }

    #endregion
}