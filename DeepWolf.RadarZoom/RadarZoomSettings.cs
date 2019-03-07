namespace DeepWolf.RadarZoom
{
    using System.IO;
    using System.Windows.Forms;
    using GTA;

    /// <summary>
    /// Represents the settings for the <see cref="RadarZoom"/> mod.
    /// Handles the saving and loading settings.
    /// </summary>
    internal static class RadarZoomSettings
    {
        #region Constants
        /// <summary>
        /// The name of the INI file with the extension.
        /// </summary>
        private const string INIFileName = "RadarZoom.ini";

        /// <summary>
        /// The relative path to the INI file.
        /// </summary>
        private const string INIFilePath = "scripts/" + INIFileName;

        /// <summary>
        /// The name of the general section inside the INI file.
        /// </summary>
        private const string GeneralSection = "GENERAL";

        /// <summary>
        /// The name of the zoom section inside the INI file.
        /// </summary>
        private const string ZoomSection = "ZOOM";

        /// <summary>
        /// The name of the setting that holds the <see cref="OpenMenuKey"/> value inside the INI file.
        /// </summary>
        private const string OpenMenuKeySettingName = "OPENMENUKEY";

        /// <summary>
        /// The name of the setting that holds the <see cref="ZoomLevelOnFoot"/> value inside the INI file.
        /// </summary>
        private const string ZoomLevelOnFootSettingName = "ONFOOT";

        /// <summary>
        /// The name of the setting that holds the <see cref="ZoomLevelInVehicle"/> value inside the INI file.
        /// </summary>
        private const string ZoomLevelInVehicleSettingName = "INVEHICLE";

        /// <summary>
        /// The name of the setting that holds the <see cref="ZoomLevelInBuilding"/> value inside the INI file.
        /// </summary>
        private const string ZoomLevelInBuildingSettingName = "INBUILDING";
        #endregion Constants

        #region Fields
        private static ScriptSettings settings;

        /// <summary>
        /// The key to press to open the menu.
        /// </summary>
        private static Keys openMenuKey;

        /// <summary>
        /// The radar zoom level when on foot.
        /// </summary>
        private static int zoomLevelOnFoot;

        /// <summary>
        /// The radar zoom level when inside a vehicle.
        /// </summary>
        private static int zoomLevelInVehicle;

        /// <summary>
        /// The radar zoom level when inside a building.
        /// </summary>
        private static int zoomLevelInBuilding;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets the key to open the settings menu.
        /// </summary>
        public static Keys OpenMenuKey
        {
            get => openMenuKey;
            private set => openMenuKey = value;
        }

        /// <summary>
        /// Gets or sets the zoom level on foot.
        /// </summary>
        public static int ZoomLevelOnFoot
        {
            get => zoomLevelOnFoot;
            internal set
            {
                if (ZoomLevelOnFoot == value)
                {
                    return;
                }

                if (value < 25)
                {
                    zoomLevelOnFoot = 25;
                }
                else
                {
                    zoomLevelOnFoot = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the zoom level in vehicle.
        /// </summary>
        public static int ZoomLevelInVehicle
        {
            get => zoomLevelInVehicle;
            internal set
            {
                if (ZoomLevelInVehicle == value)
                {
                    return;
                }

                if (value < 25)
                {
                    zoomLevelInVehicle = 25;
                }
                else
                {
                    zoomLevelInVehicle = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the zoom level in building.
        /// </summary>
        public static int ZoomLevelInBuilding
        {
            get => zoomLevelInBuilding;
            internal set
            {
                if (ZoomLevelInBuilding == value)
                {
                    return;
                }

                if (value < 0)
                {
                    zoomLevelInBuilding = 0;
                }
                else
                {
                    zoomLevelInBuilding = value;
                }
            }
        }
        #endregion Properties

        /// <summary>
        /// Saves the settings to the INI file.
        /// </summary>
        internal static void SaveSettings()
        {
            // Check whether the INI file has been loaded.
            if (settings == null)
            {
                UI.Notify("Failed to save settings. Reason: No settings is loaded.");
            }
            else
            {
                // Save all values to the INI file.
                settings.SetValue(ZoomSection, ZoomLevelOnFootSettingName, zoomLevelOnFoot);
                settings.SetValue(ZoomSection, ZoomLevelInVehicleSettingName, zoomLevelInVehicle);
                settings.SetValue(ZoomSection, ZoomLevelInBuildingSettingName, zoomLevelInBuilding);
                settings.Save();
            }
        }

        /// <summary>
        /// Loads the settings from the INI file.
        /// </summary>
        internal static void LoadSettings()
        {
            string filePath = Path.Combine(Application.StartupPath, INIFilePath);

            if (!File.Exists(filePath))
            {
                UI.Notify($"Failed to load settings. Reason: File at path {filePath} not found.");
                return;
            }

            // Load the INI file.
            settings = ScriptSettings.Load(INIFilePath);

            // Check whether the INI file was successfully loaded.
            if (settings == null)
            {
                UI.Notify("Failed to load settings.");
            }
            else
            {
                // Load all values from the INI file.
                OpenMenuKey = settings.GetValue(GeneralSection, OpenMenuKeySettingName, Keys.Z);

                ZoomLevelOnFoot = settings.GetValue(ZoomSection, ZoomLevelOnFootSettingName, 840);
                ZoomLevelInVehicle = settings.GetValue(ZoomSection, ZoomLevelInVehicleSettingName, 1100);
                ZoomLevelInBuilding = settings.GetValue(ZoomSection, ZoomLevelInBuildingSettingName, 0);
            }
        }
    }
}
