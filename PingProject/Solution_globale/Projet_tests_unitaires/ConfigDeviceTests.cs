using ConfigDevice = Projet_app_configuration.ConfigDevice;
namespace Projet_tests_unitaires {
    [TestClass]
    public class ConfigDeviceTests {
        [TestMethod]
        public void DummyTest() {
            Assert.AreEqual(1,1);
        }
        [TestMethod]
        public void Equals_NominalCase_True() {
            //Arrange
            ConfigDevice config1 = new ConfigDevice("DeviceTest","1.1.1.1",2);
            ConfigDevice config2 = new ConfigDevice("DeviceTest", "1.1.1.1", 2);
            //Act
            bool result = config1.Equals(config2);
            ///Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Equals_NominalCase_False() {
            //Arrange
            ConfigDevice config1 = new ConfigDevice("DeviceTest", "1.1.1.1", 2);
            ConfigDevice config2 = new ConfigDevice("DeviceTest", "1.1.1.2", 2);
            //Act
            bool result = config1.Equals(config2);
            ///Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Equals_OneArgNull_False() {
            //Arrange
            ConfigDevice config1 = new ConfigDevice("DeviceTest", "1.1.1.1", 2);
            ConfigDevice config2 = null;
            //Act
            bool result = config1.Equals(config2);
            ///Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Equals_OneArgEmpty_False() {
            //Arrange
            ConfigDevice config1 = new ConfigDevice("DeviceTest", "1.1.1.1", 2);
            ConfigDevice config2 = new ConfigDevice();
            //Act
            bool result = config1.Equals(config2);
            ///Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Equals_SameReference_True() {
            //Arrange
            ConfigDevice config1 = new ConfigDevice("DeviceTest", "1.1.1.1", 2);
            ConfigDevice config2 = config1;
            //Act
            bool result = config1.Equals(config2);
            ///Assert
            Assert.IsTrue(result);
        }
    }
}
