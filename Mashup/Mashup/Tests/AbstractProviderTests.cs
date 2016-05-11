//-----------------------------------------------------------------------
// <copyright file="AbstractProviderTests.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Tests
{
    using Mashup.Tests.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Util;

    /// <summary>
    /// Provider manager tests
    /// </summary>
    [TestClass]
    public class AbstractProviderTests
    {
        /// <summary>
        /// Testing if the instance is really instantiated once
        /// </summary>
        /*[TestMethod]
        public void DeserializeJSonTest()
        {
            string json = "{ \"employee\": {\"firstName\":\"Rick\", \"lastName\":\"Grimes\"}}";
            Example example = AbstractProvider.DeserializeJSon<Example>(json);
            Employee employee = example.Employee;
            Assert.AreEqual(employee.FirstName, "Rick");
            Assert.AreEqual(employee.LastName, "Grimes");
        }*/
    }
}