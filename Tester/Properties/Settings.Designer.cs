﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tester.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Tester_BCPrintWebServiceFunctions_BCPrintWebService {
            get {
                return ((string)(this["Tester_BCPrintWebServiceFunctions_BCPrintWebService"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://businesscentral.dynamics.com/f968cbe4-b4a1-48ff-9920-5224b47e606a/V15_Tes" +
            "t/WS/CRONUS%20UK%20Ltd./")]
        public string BaseUrl {
            get {
                return ((string)(this["BaseUrl"]));
            }
            set {
                this["BaseUrl"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SZABOLCS.GYORKE")]
        public string UserName {
            get {
                return ((string)(this["UserName"]));
            }
            set {
                this["UserName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MGJOHYH4SJaBj2JYLyq0yVJ4Ws59XkxFoWE/VeYNvUU=")]
        public string ApiKey {
            get {
                return ((string)(this["ApiKey"]));
            }
            set {
                this["ApiKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SZABOLCSG-PC2")]
        public string Instance {
            get {
                return ((string)(this["Instance"]));
            }
            set {
                this["Instance"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\temp\\UploadTest1")]
        public string UploadFolder1 {
            get {
                return ((string)(this["UploadFolder1"]));
            }
            set {
                this["UploadFolder1"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\temp\\UploadTest2")]
        public string UploadFolder2 {
            get {
                return ((string)(this["UploadFolder2"]));
            }
            set {
                this["UploadFolder2"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\temp\\DownloadTest1")]
        public string DownloadFolder1 {
            get {
                return ((string)(this["DownloadFolder1"]));
            }
            set {
                this["DownloadFolder1"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\temp\\DownloadTest2")]
        public string DownloadFolder2 {
            get {
                return ((string)(this["DownloadFolder2"]));
            }
            set {
                this["DownloadFolder2"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Basic")]
        public string AuthType {
            get {
                return ((string)(this["AuthType"]));
            }
            set {
                this["AuthType"] = value;
            }
        }
    }
}
