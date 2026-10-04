using PanelManager = Projet_app_principale.PanelManager;
using PingMonitoringForm = Projet_app_principale.PingMonitoringForm;
using ConfigSetting = Projet_app_principale.ConfigSetting;
using System.ComponentModel;
using Projet_app_principale;

namespace Projet_tests_unitaires;

[TestClass]
public class PanelManagerTests
{
    
    [TestMethod]
    public void DummyTest() {
        Assert.AreEqual(1, 1);
    }
    [TestMethod]
    // The DeviceOK method is not tested, as it is tested indirectly in this test.
    public void GetDevicesList_NominalCase_RightList() {
        //Arrange
        PanelManager panelManager = new PanelManager(new PingMonitoringForm());
        ConfigSetting configSetting = new ConfigSetting();
        panelManager.config = configSetting;
        ConfigDevice device1 = new ConfigDevice("DeviceTest", "1.1.1.1", 2);
        ConfigDevice device2 = new ConfigDevice("DeviceTest2", "1.1.1.2", 2);
        ConfigDevice device3 = new ConfigDevice("", "1.1.1.2", 2);
        BindingList<ConfigDevice> list = new BindingList<ConfigDevice>();
        list.Add(device1);
        list.Add(device2);
        list.Add(device3);
        BindingList<ConfigDevice> listReturn = new BindingList<ConfigDevice>();
        panelManager.config.listDevice = list;
        
        //Act
        listReturn = panelManager.GetDevicesList();
        foreach (ConfigDevice c in listReturn) {
            c.onAttributChange += OnAttributChangeHandler;
        }
        ///Assert
        Assert.IsTrue(listReturn[0].Equals(device1));
        Assert.IsTrue(listReturn[1].Equals(device2));

        Assert.AreEqual(listReturn[0].DeviceStatus, "UNITIATED");
        Assert.AreEqual(listReturn[1].DeviceStatus, "UNITIATED");
    }

    private void OnAttributChangeHandler(object sender, EventArgs e) {

    }
}
