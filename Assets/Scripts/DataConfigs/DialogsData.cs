using Sirenix.OdinInspector;
using UnityEngine;

namespace DataConfigs
{
    [CreateAssetMenu(fileName = "DialogsData", menuName = "Configs/DialogsData", order = 0)]
    public class DialogsData : ScriptableObject
    {
        [HorizontalGroup("Split", Width = 150), HideLabel, PreviewField(150)]
        public Texture2D characterImage;

        [VerticalGroup("Split/Properties"),TextArea(2, 8)] 
        public string titleLabel;
        [VerticalGroup("Split/Properties"),TextArea(6, 8)] 
        public string descriptionLabel;
    }
}