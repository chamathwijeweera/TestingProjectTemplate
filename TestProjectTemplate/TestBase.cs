using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Era.UnityServiceInitiator;

namespace TestProjectTemplate
{
    [TestClass]
    public abstract class TestBase
    {
        public TService GetService<TService>()
        {
            return new ServiceInitiator().GetService<TService>();
        }

        public XDocument ReadXml(string path)
        {
            return XDocument.Load(path);
        }

        public void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine($"\n{ message}");
        }

        public bool CommonResponseValidator(string resultNode)
        {
            var expectedResults = ReadXml(@"ResponseData\Common.xml").ToString();
            return CompareXml(expectedResults, resultNode);
        }

        public bool CompareXml(string expectedXml, string resultXml)
        {
            Log("EXPECTED RESULT");
            Log(expectedXml);
            Log("RESPONSE RESULT");
            Log(resultXml);
            var orginalXmlHash = ConvertStringToHash(expectedXml);
            var resultXmlHash = ConvertStringToHash(resultXml);
            return (orginalXmlHash == resultXmlHash);
        }

        protected XmlDocument XDocumentToXmlDocument(string response)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            XmlNode node = xmlDoc.SelectSingleNode("Response/Errors");
            if (node != null)
            {
                throw new System.Exception("Exception in connector");
            }
            else
            {
                XmlNode executionStatusNode = xmlDoc.SelectSingleNode("Response/Result/TotalTimeElapsed");
                //Reset request process time
                if (executionStatusNode != null)
                    xmlDoc.SelectSingleNode("Response/Result/TotalTimeElapsed").InnerText = string.Empty;

                return xmlDoc;
            }
        }

        //Use this attribute [TestInitialize]
        public abstract void TestInitialization();

        //Use this attribute [TestCleanup]
        public abstract void TestCleanup();

        private string ConvertStringToHash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
