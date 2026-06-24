using UnityEngine;

public class UI : MonoBehaviour
{
    #region variables

        // public
    
        // private 
        private bool isHotbarActive = false;
        [SerializeField] private GameObject hotbarButton;
        [SerializeField] private GameObject hotbar;
        [SerializeField] private GameObject upButton, downButton, leftButton, rightButton;
        
    #endregion
    
    #region ui funtions
        public void UpButton()
        {
            if (!GameManager.Instance.MainCameraActive())
            {
                GameManager.Instance.InventoryScene();
            }
        }
        public void DownButton()
        {
            GameManager.Instance.CraftingScene();
        }
        public void LeftButton()
        {
            if (!GameManager.Instance.CraftingCameraActive())
            {
                GameManager.Instance.InventoryScene();
            }
        }
        public void RightButton()
        {
            GameManager.Instance.MainScene();
        }
        public void ToggleHotbar()
        {
            if(isHotbarActive)
            {
                isHotbarActive = false;
                hotbarButton.SetActive(false);
                hotbar.SetActive(true);
            }
            else
            {
                isHotbarActive = true;
                hotbarButton.SetActive(true);
                hotbar.SetActive(false);
            }
        }

    #endregion
}
