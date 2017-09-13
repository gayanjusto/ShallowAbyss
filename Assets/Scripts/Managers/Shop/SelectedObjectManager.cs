using Assets.Scripts.Interfaces.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Shop
{
    public class SelectedObjectManager : MonoBehaviour
    {
        public Text selectedObjNameTxt;

        IShopSelectedObject selectedItem;
        public void SetSelectedObject(IShopSelectedObject selectedItem)
        {
            //reset previous selected object
            if(this.selectedItem != null)
            {
                this.selectedItem.DeselectObject();
            }
            this.selectedItem = selectedItem;
            selectedItem.SelectObject();

            selectedObjNameTxt.text = selectedItem.GetName();
        }

        public void RemoveSelectedObject()
        {
            this.selectedItem = null;
        }

        public IShopSelectedObject GetSelectedObject()
        {
            return this.selectedItem;
        }
    }
}
