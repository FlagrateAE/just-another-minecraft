using UnityEngine;
using UnityEngine.UI;

namespace JustAnotherMinecraft.UI
{
    public class ToolBarSlot : MonoBehaviour
    {
        [Header("References")]
        public Image Image;
        public Color SelectedColor, notSelectedColor;

        private void Awake()
        {
            Deselect();
        }

        public void Select()
        {
            Image.color = SelectedColor;
        }
        
        public void Deselect()
        {
            Image.color = notSelectedColor;
        }
    }
}