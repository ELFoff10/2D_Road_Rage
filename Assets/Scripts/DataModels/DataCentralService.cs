// using Cysharp.Threading.Tasks;
//
// public interface IDataCentralService
// {
//     ISubDataModel SubData { get; }
//     void SaveFull();
//     void SaveSubData();
// }
//
// public class DataCentralService : IDataCentralService
// {
//     private SubDataModel _subDataModel = new SubDataModel();
//     public ISubDataModel SubData => _subDataModel;
//     private AbstractDataPlatform _dataPlatform;
//
//     #region PublicMethods
//
//     public DataCentralService()
//     {
// #if UNITY_EDITOR
//         _dataPlatform = new MobileDataPlatform();
// // #elif UNITY_WEBGL
// //                 _dataPlatform = new MobileDataPlatform();
// #elif UNITY_ANDROID || UNITY_IPHONE
//             _dataPlatform = new MobileDataPlatform();
// #endif
//         _dataPlatform.Init(_subDataModel);
//     }
//
//     public void SaveSubData()
//     {
//         _dataPlatform.SaveSubData();
//     }
//
//     public void SaveFull()
//     {
//         SaveSubData();
//     }
//
//     public void Restart()
//     {
//         _subDataModel.SetAndInitEmptySubData(new SubData());
//     }
//
//     public async UniTask LoadData()
//     {
//         _dataPlatform.LoadData();
//         await UniTask.WaitUntil(() => _dataPlatform.IsSubDataModelLoaded);
//     }
//
//     #endregion
// }