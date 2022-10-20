using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class Shop : MonoBehaviour
    {
        public GameObject shopUI;

        void Start()
        {
            shopUI.SetActive(false);
        }

        public void OpenShopUI()
        {
            shopUI.SetActive(true);
        }

        public void CloseShopUI()
        {
            shopUI.SetActive(false);
        }

        public void ExitShop()
        {
            this.gameObject.SetActive(false);
            shopUI.SetActive(false);
        }

        public void SellCalcium()
        {

        }

        public void SellIron()
        {

        }

        public void SellAluminium()
        {

        }

        public void SellSilicon()
        {

        }

        public void SellIgneous()
        {

        }

        public void SellSedimentary()
        {

        }

        public void SellMetamorphic()
        {

        }

        public void BuyPickaxe()
        {

        }

        public void BuyHelmet()
        {

        }

        public void BuyHealth()
        {

        }
    }
}
