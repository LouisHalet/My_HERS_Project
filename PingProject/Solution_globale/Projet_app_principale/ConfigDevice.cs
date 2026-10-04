using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_app_principale {
    public class ConfigDevice {
        string NamePrivate;
        public string Name {
            get { return this.NamePrivate; }
            set { NamePrivate = value; }
        }
        string IPAddressPrivate;
        public string IPAddress {
            get { return this.IPAddressPrivate; }
            set { IPAddressPrivate = value; }
        }
        int SecondIntervalPrivate;
        public int SecondInterval {
            get { return this.SecondIntervalPrivate; }
            set { SecondIntervalPrivate = value; }
        }
        public event EventHandler onAttributChange;
        string DeviceStatusPrivate;
        public string DeviceStatus {
            get { return this.DeviceStatusPrivate; }
            set { 
                DeviceStatusPrivate = value;
                onAttributChange?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfigDevice() {

        }
        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="name">The name of device</param>
        /// <param name="IPAddress">The ip address of device</param>
        /// <param name="SecondInterval">The interval of seconde between two ping</param>
        public ConfigDevice(string name, string IPAddress, int SecondInterval) {
            this.Name = name;
            this.IPAddress = IPAddress;
            this.SecondInterval = SecondInterval;
        }
        public override bool Equals(object obj) {
            if(obj is ConfigDevice d) {
                return this.Name.Equals(d.Name) && this.IPAddress.Equals(d.IPAddress) && this.SecondInterval == d.SecondInterval;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// Returns a hash code for this ConfigDevice, based on its Name, IPAddress and SecondInterval.
        /// </summary>
        /// <returns>a hash code consistent with Equals</returns>
        public override int GetHashCode() {
            return HashCode.Combine(Name, IPAddress, SecondInterval);
        }

    }
}
