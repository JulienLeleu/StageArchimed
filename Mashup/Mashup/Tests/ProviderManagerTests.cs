//-----------------------------------------------------------------------
// <copyright file="ProviderManagerTests.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Tests
{
    using System;
    using System.Threading.Tasks;
    using IO;
    using Exception;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Entity;

    /// <summary>
    /// Provider manager tests
    /// </summary>
    [TestClass]
    public class ProviderManagerTests
    {
        /// <summary>
        /// Testing if the instance is really instantiated once
        /// </summary>
        [TestMethod]
        public void GetInstanceTest()
        {
            ProviderManager manager = ProviderManager.GetInstance();
            Assert.IsTrue(manager == ProviderManager.GetInstance());
        }

        /// <summary>
        /// Testing if Identifier from a string returned the right identifier value
        /// </summary>
        [TestMethod]
        public void GetIdentifierFromStringTest()
        {
            Assert.AreEqual(Identifier.Ean, ProviderManager.GetIdentifierFromString("Ean"));
            Assert.AreEqual(Identifier.Ean, ProviderManager.GetIdentifierFromString("EAN"));
            Assert.AreEqual(Identifier.Ean, ProviderManager.GetIdentifierFromString("ean"));
            Assert.AreEqual(Identifier.Id, ProviderManager.GetIdentifierFromString("Id"));
            Assert.AreEqual(Identifier.Isbn, ProviderManager.GetIdentifierFromString("Isbn"));
            Assert.AreEqual(Identifier.Mbid, ProviderManager.GetIdentifierFromString("Mbid"));
        }

        /// <summary>
        /// Testing if culture from a string returned the right object CultureInfo
        /// </summary>
        [TestMethod]
        public void GetCultureInfoFromStringTest()
        {
            Assert.AreEqual("fr", ProviderManager.GetCultureInfoFromString("FR").TwoLetterISOLanguageName);
            Assert.AreEqual("en", ProviderManager.GetCultureInfoFromString("En").TwoLetterISOLanguageName);
            Assert.AreEqual("es", ProviderManager.GetCultureInfoFromString("es").TwoLetterISOLanguageName);
        }

        /// <summary>
        /// Test method for $$(ProviderManager.SendAll())$$
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.AggregateException))]
        public void SendAllIdentifierTest()
        {
            ProviderManager manager = ProviderManager.GetInstance();

            try
            {
                Task<ResultSetObject> t = manager.GetObjectsDatasFromProviders(new SendObject("LOL", "9c9f1380-2516-4fc9-a3e6-f9f61941d090", "", ""));
                t.Wait();
                ResultSetObject b = t.Result;
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.InnerException.GetType(), typeof(IdentifierUnknownException));
                throw;
            }

            Assert.Fail("Expected IdentifierUnknownException was not thrown");
        }
    }
}