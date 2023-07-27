using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TestAssessment.Hooks;
using TestAssessment.PageObjects;

namespace TestAssessment.StepDefinitions
{
    [Binding]
    public class SearchsStepDefinitions
    {

        public IWebDriver driver = BaseTest.driver;

        SearchsPage searchsPage = new SearchsPage();
        
        [Given(@"I add four random items to my cart")]
        public void GivenIAddFourRandomItemsToMyCart()
        {
            searchsPage.AddRandomItemsToCart(4);
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            searchsPage.Viewcart();
        }

        [Then(@"I find total four items listed in my cart")]
        public void ThenIFindTotalFourItemsListedInMyCart()
        {
            int totalItems = searchsPage.GetTotalCartItems();
            Assert.AreEqual(4, totalItems);
        }

        [When(@"I search  for lowest price item")]
        public void WhenISearchForLowestPriceItem()
        {
            searchsPage.SearchForLowestPricedItem();
        }

        [When(@"I am able to remove the lowest price item from my cart")]
        public void WhenIAmAbleToRemoveTheLowestPriceItemFromMyCart()
        {
            searchsPage.RemoveItemFromCart("lowestPricedItemName");
        }

        [Then(@"I am able to verify three items in my cart")]
        public void ThenIAmAbleToVerifyThreeItemsInMyCart()
        {
            bool areThreeItemsDisplayed = searchsPage.VerifyThreeItemsInCart("Item 1", "Item 2", "Item 3");
            Assert.IsTrue(areThreeItemsDisplayed);
        }
    }
}
        
    

