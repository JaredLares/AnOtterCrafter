using UnityEngine;

public class UI : MonoBehaviour
{
    #region variables

        // public
    
        // private 
        private bool isHotbarActive = true;
        [SerializeField] private GameObject hotbarButton;
        [SerializeField] private GameObject hotbar;
        [SerializeField] private GameObject upButton, downButton, leftButton, rightButton;
        
    #endregion
    
    #region ui funtions
        public void UpButton()
        {
            InventoryScene();
        }
        public void DownButton()
        {
            CraftScene();   
        }
        public void LeftButton()
        {
            InventoryScene();
        }
        public void RightButton()
        {
            MainScene();
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

        private void CraftScene()
        {
        
        }
        private void TradeScene()
        {
        
        }
        private void InventoryScene()
        {
        
        }
        private void MainScene()
        {
        
        }
    #endregion
}
