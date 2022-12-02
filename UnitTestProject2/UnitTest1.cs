using System;
using System.Web.Mvc;
using Airline.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
{
	[TestClass]
	public class UnitTest1
	{
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            HomeController controller = new HomeController();

            ViewResult res = controller.Index() as ViewResult;

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            Assert.AreEqual("About", result.ViewName);
        }

		[TestMethod]
		public void IndexStringInViewbag()
		{
			HomeController controller = new HomeController();

			ViewResult res = controller.Contact() as ViewResult;

			Assert.IsTrue(res.ViewName.ToString() == "Contact");
		}
	}
}
