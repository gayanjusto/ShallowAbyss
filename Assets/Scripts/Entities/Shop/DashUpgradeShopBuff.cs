using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Interfaces.Shop;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Services;
using System;

namespace Assets.Scripts.Entities.Shop
{
    public class DashUpgradeShopBuff : ShopBuff, IShopBuff, ILanguageUI
    {

        public override Func<bool> CanIncreaseBuff()
        {
            return () => PlayerStatusManager.PlayerDataInstance.CanUpgradeDash();
        }

        public override Action BuyBuff()
        {
            return () => PlayerStatusManager.PlayerDataInstance.IncreaseDashUpgrade();
        }

        public override void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                this.buffName = ld.dashUpgradeItem;
            }
        }
    }
}
