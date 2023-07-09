using DataModels.Models;

namespace DataModels.PlatformModels
{
    public abstract class AbstractDataPlatform
    {
        protected const string PRENAME = "Road_Rage_MotorikaStorage";
        public bool IsSubDataModelLoaded = false;

        protected SubDataModel SubDataModel;
        
        public void Init(SubDataModel subDataModel)
        {
            SubDataModel = subDataModel;
        }
        
        public void SaveFullData()
        {
            SaveSubData();
        }
        public virtual void SaveSubData()
        {
        }
        
        public virtual void LoadData()
        {
        }
    }
}