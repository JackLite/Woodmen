using Woodman.Utils;

namespace Woodman.MetaTrees
{
    /// <summary>
    /// Данные по деревьям в мете
    /// </summary>
    public class MetaTreesRepository
    {
        private const string SAVE_KEY = "meta.trees";
        private readonly MetaTreeSaveData _saveData;
        public TreeMeta CurrentTree { get; set; }

        public MetaTreesRepository()
        {
            _saveData = RepositoryHelper.CreateSaveData<MetaTreeSaveData>(SAVE_KEY);
        }

        public bool IsFell(string id)
        {
            CheckTree(id);
            return _saveData.trees[id].isFell;
        }

        public void SetFell(string id)
        {
             CheckTree(id);
            _saveData.trees[id].isFell = true;
            Save();
        }

        private void CheckTree(string id)
        {
            if (!_saveData.trees.ContainsKey(id))
                _saveData.trees[id] = new MetaTree { id = id, isFell = false };
        }

        private void Save()
        {
            RepositoryHelper.Save(SAVE_KEY, _saveData);
        }
    }
}