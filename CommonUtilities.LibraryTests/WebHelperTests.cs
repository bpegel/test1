using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonUtilities.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.Library.Tests
{
    [TestClass()]
    public class WebHelperTests
    {
        [TestMethod()]
        public void GetWebDataTest()
        {
            string baseUrl = @"http://www.molinahealthcare.com";
            string reqUri = @"/members/tx/en-us/Pages/home.aspx";
            WebResponseInfo resInfo = new WebResponseInfo();
            try
            {
                resInfo = WebHelper.GetWebData(baseUrl, reqUri, "", "", WebTool.HTTP_CLIENT);
            }
            catch { }
            
            Assert.AreEqual(resInfo.HttpStatus, System.Net.HttpStatusCode.OK);
        }
    }
}