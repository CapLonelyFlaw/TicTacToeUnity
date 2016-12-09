using Assets.Scripts.Editor.Assets;
using UnityEditor;

namespace GameCore
{
    internal class GameAssetsCreator
    {
        [MenuItem("Assets/Create/Settings/GameConfig")]
        public static void CreateBuildSettingsAsset()
        {
            ScriptableObjectUtility.CreateAsset<GameConfig>();
        }
    }
}
