using System;
using System.Web.Mvc;
using Airline.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Airline.Tests.Controllers
{
        [TestClass]
        public class РейсControllerTest
        {
                [TestMethod]
                public void IndexViewResultNotNull()
                {
                    РейсController controller = new РейсController();

                    ViewResult result = controller.Index("","") as ViewResult;

                    Assert.IsNotNull(result);
                }

                [TestMethod]
                public void IndexViewEqualIndexCshtml()
                {
                    РейсController controller = new РейсController();

                    ViewResult result = controller.Index("","") as ViewResult;

                    Assert.AreEqual("Index", result.ViewName);
                }

                [TestMethod]
                public void IndexStringInViewbag()
                {
                    РейсController controller = new РейсController();

                    ViewResult result = controller.Index("","") as ViewResult;

                    Assert.AreEqual("Index", result.ViewBag.Message);
                }
        }

    
}
