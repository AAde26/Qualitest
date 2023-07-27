using DryIoc.FastExpressionCompiler.LightExpression;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAssessment.Hooks;

namespace TestAssessment.PageObjects
{
    public class SearchsPage
    {
        public IWebDriver driver;

        public SearchsPage()
        {
            driver = BaseTest.driver;
        }

        //locators
        private By addToCartButtonLocator = By.CssSelector(".single_add_to_cart_button");
        private By cartIconLocator = By.CssSelector(".cart-contents");
        private By cartItemsLocator = By.CssSelector(".cart_item");
        private By itemNameLocator = By.CssSelector(".product-title");
        private By itemPriceLocator = By.CssSelector(".product-price");

        private By firstItem = By.XPath("//*[@id=\"main\"]/div[2]/ul/li[4]/div/a[2]");
        private By secondItem = By.XPath("//*[@id=\"main\"]/div[2]/ul/li[6]/div/a[2]");
        private By thirdItem = By.XPath("//*[@id=\"main\"]/div[2]/ul/li[4]/div/a[7]");
        private By fourItem = By.XPath("");
        private By viewCart = By.XPath("//a[normal-space()='cart']");

        //Methods
        public void AddRandomItemsToCart(int numberOfItems)
        {
            List<IWebElement> addToCartButtons = driver.FindElements(addToCartButtonLocator).ToList();
            Random random = new Random();

            for (int i = 0; i < numberOfItems; i++)
            {
                //Generate a random integer between a specified range 
                int randomIndex = random.Next(addToCartButtons.Count);
                addToCartButtons[randomIndex].Click();
                System.Threading.Thread.Sleep(1000);
                
            }
        }
        public void NavigateToUrl()
        {
            driver.Navigate().GoToUrl("https://cms.demo.katalon.com/");
        }
        public void Viewcart()
        {
            driver.FindElement(cartIconLocator).Click();
        }
        public int GetTotalCartItems()
        {
            return driver.FindElements(cartItemsLocator).Count;
        }

        public void SearchForLowestPricedItem()
        {

        }
        public void FindLowestPricedItem()
        {
            //Get all items in the cart
            List<IWebElement> cartItems = driver.FindElements(cartItemsLocator).ToList();
        }

        //initialise variables to store the lowest price and the corresponding item name
        public double GetItemPrice(IWebElement itemElement)
        {
            //Get the price text of the item
            string priceText = itemElement.FindElement(itemPriceLocator).Text;

            //Remove currency symbol and any other nonnumeric character
            string numericPrice = new string(priceText.Where(char.IsDigit).ToArray()); 
            //Convert the price to a double value
            double itemPrice = double.Parse(numericPrice)/100.0;
            return itemPrice;
        }

        public string GetItemName(IWebElement itemElement)
        {
            return itemElement.FindElement(itemNameLocator).Text;
        }

        public string FindLowestPriceItem()
        {
            //Get all items in the cart 
            List<IWebElement> cartItems = driver.FindElements(cartItemsLocator).ToList();

            double lowestPrice = double.MaxValue;
            string lowestPricedItemName = "";

            //Loop through each item in the cart
            foreach (var item in cartItems)
            {

                //Get the price of the current item
                double itemPrice = GetItemPrice(item);
                if (itemPrice < lowestPrice)
                {
                    lowestPrice = itemPrice;
                    lowestPricedItemName = GetItemName(item);
                }
            }
            return lowestPricedItemName;
        }
        public void RemoveItemFromCart(string itemName)
        {
            //Find the item in the cart and click on the Remove Button 
            IWebElement removeButton = driver.FindElement(By.XPath($"//h3[contains(text(), '{itemName}' )]/following-sibling::a[@class='remove']"));
            removeButton.Click();
            
        }
      public  bool VerifyThreeItemsInCart(string item1, string item2, string item3)
        {
            List<string> itemsInCart = driver.FindElements(itemNameLocator).Select(e=> e.Text).ToList(); 
            return itemsInCart.Contains(item1) && itemsInCart.Contains(item2) && itemsInCart.Contains(item3);
        }
}
}





