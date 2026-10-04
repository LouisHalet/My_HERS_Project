
using PanelManager = Projet_app_configuration.PanelManager;
using GeneralConfigurationForm = Projet_app_configuration.GeneralConfigurationForm;
using ConfigSetting = Projet_app_configuration.ConfigSetting;
using ConfigDevice = Projet_app_configuration.ConfigDevice;
using System.ComponentModel;

namespace Projet_tests_unitaires;

[TestClass]
[DoNotParallelize]// for Bug File Acces simultaneity
public class PanelManagerConfigurationTests
{
    [TestMethod]
    public void DummyTest() {
        Assert.AreEqual(1, 1);
    }
    [TestMethod]

    public void CheckConfig_WrongConfig_False() {
        PanelManager panelManager = new PanelManager(new GeneralConfigurationForm());
        ConfigSetting configSetting = new ConfigSetting();
        panelManager.config = configSetting;
        ConfigDevice device1 = new ConfigDevice("", "1.1.1.1", 2);
        ConfigDevice device2 = new ConfigDevice("", "1.1.1.2", 2);
        ConfigDevice device3 = new ConfigDevice("", "1.1.1.3", 2);
        BindingList<ConfigDevice> list = new BindingList<ConfigDevice>();
        list.Add(device1);
        list.Add(device2);
        list.Add(device3);
        panelManager.config.listDevice = list;
        panelManager.config.EmailSenderUsers = "test";
        panelManager.config.SMTPEmail = "test";
        panelManager.config.SMTPHost = "test";
        panelManager.config.SMTPPassword = "test";
        panelManager.config.SMTPPort = 1;
        panelManager.CheckConfig();
        Assert.IsFalse(panelManager.configOk);
        
    }
    [TestMethod]

    public void CheckConfig_RightConfig_True() {
        PanelManager panelManager = new PanelManager(new GeneralConfigurationForm());
        ConfigSetting configSetting = new ConfigSetting();
        panelManager.config = configSetting;
        ConfigDevice device1 = new ConfigDevice("DeviceTest", "1.1.1.1", 2);
        ConfigDevice device2 = new ConfigDevice("DeviceTest2", "1.1.1.2", 2);
        ConfigDevice device3 = new ConfigDevice("", "1.1.1.2", 2);
        BindingList<ConfigDevice> list = new BindingList<ConfigDevice>();
        list.Add(device1);
        list.Add(device2);
        list.Add(device3);
        panelManager.config.listDevice = list;
        panelManager.config.EmailSenderUsers = "test";
        panelManager.config.SMTPEmail = "test";
        panelManager.config.SMTPHost = "test";
        panelManager.config.SMTPPassword = "test";
        panelManager.config.SMTPPort = 1;
        panelManager.CheckConfig();
        Assert.IsTrue(panelManager.configOk);

    }

}
