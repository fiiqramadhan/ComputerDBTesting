using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace ComputerDBTesting
{
    class testCase1
    {
        //declare variabel using IWebDriver Interface
        IWebDriver driver;

        //initiate to use ChromeDriver Class
        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
        }


        [Test]
        public void OpenAppTest()
        {
            //launch Website
            driver.Url= "http://computer-database.gatling.io/computers";

            //get website title
            String Title = driver.Title;

            //Print Title name on Console
            Console.WriteLine("Title of the page is : " + Title);
            Console.WriteLine("");

            //Write keyword in filterBox
            IWebElement filterBox = driver.FindElement(By.Id("searchbox"));
            filterBox.SendKeys("Apple");
            Console.WriteLine("Filter with keyword : Apple");
            Console.WriteLine("");

            //Click filter button
            IWebElement filterButton = driver.FindElement(By.Id("searchsubmit"));
            filterButton.Submit();

            // xpath of html table
            var elemTable = driver.FindElement(By.XPath("//section[@id='main']//table[1]"));

            // Fetch all Row of the table
            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));
            String strRowData = "";

            // Traverse each row
            foreach (var elemTr in lstTrElem)
            {
                // Fetch the columns from a particuler row
                List<IWebElement> lstTdElem = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));
                if (lstTdElem.Count > 0)
                {
                    // Traverse each column
                    foreach (var elemTd in lstTdElem)
                    {
                        
                        strRowData = strRowData + elemTd.Text + "\t";
                    }
                }
                else
                {
                    // To print the data into the console
                    
                    Console.WriteLine(lstTrElem[0].Text.Replace(" ", "\t"));
                }
                Console.WriteLine(strRowData);
                
                strRowData = String.Empty;
            }  

            Console.WriteLine("");
        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
