using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class Shop : MonoBehaviour
    {
        public GameObject shopUI;

        private PlayerController player;
        public GetInventoryValues backpack;

        void Start()
        {
            // Get Player
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            shopUI.SetActive(false);
        }

        #region Shop Buttons

        #region Open and Close

        public void OpenShopUI()
        {
            shopUI.SetActive(true);
            backpack.UpdateBackpack();
        }

        public void CloseShopUI()
        {
            shopUI.SetActive(false);
        }

        public void ExitShop()
        {
            this.gameObject.SetActive(false);
            shopUI.SetActive(false);
            backpack.UpdateBackpack();
            player.agent.isStopped = false;
        }

        #endregion

        #region Sell

        public void SellCalcium()
        {
            if (player.calcium > 0)
            {

                // Update Money
                player.money += 10;

                for (int i = 0; i < player.moneyText.Length; i++)
                {
                    player.moneyText[i].text = player.money.ToString();
                }

                // Update Backpack
                player.calcium --;

                backpack.UpdateBackpack();
            }
        }

        public void SellIron()
        {
            if (player.iron > 0)
            {
                // Update Money
                player.money += 10;

                for (int i = 0; i < player.moneyText.Length; i++)
                {
                    player.moneyText[i].text = player.money.ToString();
                }

                // Update Backpack
                player.iron--;

                backpack.UpdateBackpack();
            }
        }

        public void SellAluminium()
        {
            if (player.aluminium > 0)
            {
                // Update Money
                player.money += 10;

                for (int i = 0; i < player.moneyText.Length; i++)
                {
                    player.moneyText[i].text = player.money.ToString();
                }

                // Update Backpack
                player.aluminium--;

                backpack.UpdateBackpack();
            }
        }

        public void SellSilicon()
        {
            if (player.silicon > 0)
            {
                // Update Money
                player.money += 10;

                for (int i = 0; i < player.moneyText.Length; i++)
                {
                    player.moneyText[i].text = player.money.ToString();
                }

                // Update Backpack
                player.silicon--;

                backpack.UpdateBackpack();
            }
        }

        public void SellIgneous()
        {
            if (player.igneous > 0)
            {
                // Update Money
                player.money += 20;

                for (int i = 0; i < player.moneyText.Length; i++)
                {
                    player.moneyText[i].text = player.money.ToString();
                }

                // Update Backpack
                player.igneous--;

                backpack.UpdateBackpack();
            }
        }

        public void SellSedimentary()
        {
            if (player.sedimentary > 0)
            {
                // Update Money
                player.money += 20;

                for (int i = 0; i < player.moneyText.Length; i++)
                {
                    player.moneyText[i].text = player.money.ToString();
                }

                // Update Backpack
                player.sedimentary--;

                backpack.UpdateBackpack();
            }
        }

        public void SellMetamorphic()
        {
            if (player.metamorphic > 0)
            {
                // Update Money
                player.money += 20;

                for (int i = 0; i < player.moneyText.Length; i++)
                {
                    player.moneyText[i].text = player.money.ToString();
                }

                // Update Backpack
                player.metamorphic--;

                backpack.UpdateBackpack();
            }
        }

        #endregion

        #region Buy

        public void BuyPickaxe()
        {
            if (player.pickaxeLvl < 2) // Max Level
            {
                if (player.money >= 50)
                {
                    player.pickaxeLvl ++;

                    // Update Money
                    player.money -= 50;

                    for (int i = 0; i < player.moneyText.Length; i++)
                    {
                        player.moneyText[i].text = player.money.ToString();
                    }

                    // Update Backpack
                    backpack.UpdateBackpack();
                }             
            }
        }

        public void BuyHelmet()
        {
            if (player.lanternLvl < 3) // Max Level
            {
                if (player.money >= 50)
                {
                    player.lanternLvl++;
                    player.helmetLight.pointLightOuterRadius = player.lanternLvl * 2;

                    // Update Money
                    player.money -= 50;

                    for (int i = 0; i < player.moneyText.Length; i++)
                    {
                        player.moneyText[i].text = player.money.ToString();
                    }

                    // Update Backpack
                    backpack.UpdateBackpack();
                }
            }
        }

        public void BuyHealth()
        {
            if (player.armourLvl < 3) // Max Level
            {
                if (player.money >= 50)
                {
                    player.armourLvl ++;
                    player.UpdateMaxHealth();

                    // Update Money
                    player.money -= 50;

                    for (int i = 0; i < player.moneyText.Length; i++)
                    {
                        player.moneyText[i].text = player.money.ToString();
                    }

                    // Update Backpack

                    backpack.UpdateBackpack();
                }
            }         
        }

        #endregion

        #endregion
    }
}
