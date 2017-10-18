/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEditor;

namespace extOSC.Editor
{
    public static class OSCEditorSettings
    {
        #region Static Public Vars

        public static string Settings
        {
            get { return _settingRoot; }
        }

        public static string Console
        {
            get { return _consoleRoot; }
        }

        public static string Debug
        {
            get { return _debugRoot; }
        }

        public static string Mapping
        {
            get { return _mappingRoot; }
        }

        public static string ControlCreator
        {
            get { return _controlCreatorRoot; }
        }

        #endregion

        #region Static Private Vars

        private const string _settingRoot = "extOSC.";

        private const string _consoleRoot = _settingRoot + "console.";

        private const string _debugRoot = _settingRoot + "debug.";

        private const string _mappingRoot = _settingRoot + "mapping.";

        private const string _controlCreatorRoot = _settingRoot + "controlcreator.";

        #endregion

        #region Static Public Methods

        // FLOAT
        public static void SetFloat(string settingPath, float value)
        {
            EditorPrefs.SetFloat(settingPath + ".float", value);
        }

        public static float GetFloat(string settingPath, float defaultSetting)
        {
            return EditorPrefs.GetFloat(settingPath + ".float", defaultSetting);
        }

        // BOOL
        public static void SetBool(string settingPath, bool value)
        {
            EditorPrefs.SetBool(settingPath + ".bool", value);
        }

        public static bool GetBool(string settingPath, bool defaultSetting)
        {
            return EditorPrefs.GetBool(settingPath + ".bool", defaultSetting);
        }

        // INT
        public static void SetInt(string settingPath, int value)
        {
            EditorPrefs.SetInt(settingPath + ".int", value);
        }

        public static int GetInt(string settingPath, int defaultSetting)
        {
            return EditorPrefs.GetInt(settingPath + ".int", defaultSetting);
        }

        // STRING
        public static void SetString(string settingPath, string value)
        {
            EditorPrefs.SetString(settingPath + ".string", value);
        }

        public static string GetString(string settingPath, string defaultSetting)
        {
            return EditorPrefs.GetString(settingPath + ".string", defaultSetting);
        }

        // STRING ARRAY
        public static void SetStringArray(string settingPath, string[] value)
        {
            var stringValue = string.Empty;

            foreach (var element in value)
            {
                stringValue += string.Format("{0}, ", element);
            }

            if (stringValue.Length > 2)
                stringValue = stringValue.Remove(stringValue.Length - 2);

            EditorPrefs.SetString(settingPath + ".stringarray", stringValue);
        }

        public static string[] GetStringArray(string settingPath, string[] defaultValue)
        {
            var stringValue = EditorPrefs.GetString(settingPath + ".stringarray", string.Empty);
            if (string.IsNullOrEmpty(stringValue)) return defaultValue;
            var value = stringValue.Split(',');

            for (var i = 0; i < value.Length; i++)
            {
                value[i] = value[i].Trim();
            }

            return value;
        }

        // COLOR
        public static void SetColor(string settingPath, Color color)
        {
            EditorPrefs.SetFloat(settingPath + ".r", color.r);
            EditorPrefs.SetFloat(settingPath + ".g", color.g);
            EditorPrefs.SetFloat(settingPath + ".b", color.b);
            EditorPrefs.SetFloat(settingPath + ".a", color.a);
        }

        public static Color GetColor(string settingPath)
        {
            var color = new Color();

            color.r = EditorPrefs.GetFloat(settingPath + ".r", 1);
            color.g = EditorPrefs.GetFloat(settingPath + ".g", 1);
            color.b = EditorPrefs.GetFloat(settingPath + ".b", 1);
            color.a = EditorPrefs.GetFloat(settingPath + ".a", 1);

            return color;
        }

        // TRANSMITTER
        public static void SetTransmitter(string settingPath, OSCTransmitter transmitter)
        {
            EditorPrefs.SetString(settingPath + ".remotehost", transmitter != null ? transmitter.RemoteHost : "");
            EditorPrefs.SetInt(settingPath + ".remoteport", transmitter != null ? transmitter.RemotePort : -1);
        }

        public static OSCTransmitter GetTransmitter(string settingPath)
        {
            var remoteHost = EditorPrefs.GetString(settingPath + ".remotehost", "");
            var remotePort = EditorPrefs.GetInt(settingPath + ".remoteport", -1);

            return OSCEditorUtils.FindTransmitter(remoteHost, remotePort); ;
        }

        // RECEIVER
        public static void SetReceiver(string settingPath, OSCReceiver receiver)
        {
            EditorPrefs.SetInt(settingPath + ".localport", receiver != null ? receiver.LocalPort : -1);
        }

        public static OSCReceiver GetReceiver(string settingPath)
        {
            var localPort = EditorPrefs.GetInt(settingPath + ".localport", -1);

            return OSCEditorUtils.FindReceiver(localPort);
        }

        #endregion
    }
}