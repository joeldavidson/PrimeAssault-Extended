using NUnit.Framework;
using Newtonsoft;

using Game.Helpers;
using Newtonsoft.Json.Linq;
using Game.Helpers;
using System;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class JsonHelperTests
    {
        string ExampleJson = @"
{
'msg': 'Ok',
'errorCode': 0,
'version': '1.1.1.1',
'Bool': 'True',
'Integer':1,
'Double':1.1,
'String':'string',
'StringNull':null,
'Long':1,
'ULong':2,
'List':[{'string':'first'},{'string':'second'},{'string':'third'}],
'msg':'Ok','errorCode':0,'version':'1.1.1.1','data':{'ItemList':[{'Value':10,'Attribute':14,'Location':20,'Name':'Strong Shield','Guid':'3a138793-7411-7c60-6b03-aee9423d3684','Description':'Enough to hide behind','ImageURI':'http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png','Range':0,'Damage':0,'Count':-1,'IsConsumable':false,'Category':10},{'Value':10,'Attribute':14,'Location':20,'Name':'Bow','Guid':'2e0ac680-1913-0854-de5e-294bb0e4d23a','Description':'Fast shooting bow','ImageURI':'http://cliparts.co/cliparts/di4/oAB/di4oABdbT.png','Range':10,'Damage':6,'Count':-1,'IsConsumable':false,'Category':10},{'Value':10,'Attribute':12,'Location':30,'Name':'Blue Ring of Power','Guid':'c3f4cece-b1d8-bb02-38c0-32c7a4e87160','Description':'The one to control them all','ImageURI':'http://www.clker.com/cliparts/A/E/4/t/L/1/diamond-ring-teal-hi.png','Range':0,'Damage':0,'Count':-1,'IsConsumable':false,'Category':10},{'Value':10,'Attribute':10,'Location':10,'Name':'Bunny Hat','Guid':'0e9f41b4-4be2-adc3-d39d-1c70ae814913','Description':'Pink hat with fluffy ears','ImageURI':'http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png','Range':0,'Damage':0,'Count':-1,'IsConsumable':false,'Category':10}]}}
";

        [Test]
        public void JsonHelper_GetJsonString_Valid_Should_Pass()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonString(json, "String");

            // Reset

            // Assert
            Assert.AreEqual("string", result);
        }

        [Test]
        public void JsonHelper_GetJsonString_InValid_Field_Null_Should_Fail()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonString(json, null);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void JsonHelper_GetJsonString_InValid_Field_Bogus_Should_Fail()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonString(json, "bogus");

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }


        [Test]
        public void JsonHelper_GetJsonString_InValid_Value_Empty_Should_Fail()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonString(json, "StringNull");

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void JsonHelper_GetJsonString_InValid_Field_Wrong_Type_Should_Fail()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonString(json, "TimeSpan");

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }


        [Test]
        public void JsonHelper_GetJsonInteger_Valid_Should_Pass()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonInteger(json, "Integer");

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void JsonHelper_GetJsonLong_Valid_Should_Pass()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonLong(json, "Long");

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void JsonHelper_GetJsonULong_Valid_Should_Pass()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonuLong(json, "ULong");

            // Reset

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void JsonHelper_GetJsonDouble_Valid_Should_Pass()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonDouble(json, "Double");

            // Reset

            // Assert
            Assert.AreEqual(1.1, result);
        }

        [Test]
        public void JsonHelper_GetJsonBool_Valid_Should_Pass()
        {
            // Arrange

            JObject json = JObject.Parse(ExampleJson);

            // Act
            var result = JsonHelper.GetJsonBool(json, "Bool");

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}