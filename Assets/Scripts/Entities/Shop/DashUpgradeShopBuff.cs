using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Interfaces.Shop;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Services;
using System;

namespace Assets.Scripts.Entities.Shop
{
    public class DashUpgradeShopBuff : ShopBuff, IShopItem, ILanguageUI
    {

        public override Func<bool> HasReachedItemMax()
        {
            return () => PlayerStatusService.LoadPlayerStatus().CanUpgradeDash();
        }

        public override Action BuyItem()
        {
            return () => PlayerStatusService.LoadPlayerStatus().IncreaseDashUpgrade();
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
