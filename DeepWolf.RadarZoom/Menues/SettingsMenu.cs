namespace DeepWolf.RadarZoom
{
    using System.Text.RegularExpressions;
    using GTA;
    using NativeUI;

    /// <summary>
    /// Represents the menu for the settings.
    /// </summary>
    internal class SettingsMenu
    {
        #region Constants
        /// <summary>
        /// The title of the menu.
        /// </summary>
        public const string SettingsMenuTitle = "Radar Zoom";

        /// <summary>
        /// The sub title of the menu.
        /// </summary>
        public const string SettingsMenuSubTitle = "Settings";
        #endregion Constants

        #region Fields
        /// <summary>
        /// The settings <see cref="UIMenu"/>.
        /// </summary>
        private UIMenu settingsMenu;

        /// <summary>
        /// The menu button that is used to change the on foot zoom level.
        /// </summary>
        private UIMenuItem onFootZoom;

        /// <summary>
        /// The menu button that is used to change the in vehicle zoom level.
        /// </summary>
        private UIMenuItem inVehicleZoom;

        /// <summary>
        /// The menu button that is used to change the in building zoom level.
        /// </summary>
        private UIMenuItem inBuildingZoom;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsMenu"/> class.
        /// </summary>
        internal SettingsMenu()
        {
            // Define the settings menu.
            settingsMenu = new UIMenu(SettingsMenuTitle, SettingsMenuSubTitle);

            // Set up the menu options.
            onFootZoom = new UIMenuItem("On Foot Zoom");
            onFootZoom.Description = "The zoom level when on foot.";
            onFootZoom.SetRightLabel(RadarZoomSettings.ZoomLevelOnFoot.ToString());

            inVehicleZoom = new UIMenuItem("In Vehicle Zoom");
            inVehicleZoom.Description = "The zoom level when in a vehicle.";
            inVehicleZoom.SetRightLabel(RadarZoomSettings.ZoomLevelInVehicle.ToString());

            inBuildingZoom = new UIMenuItem("In Building Zoom");
            inBuildingZoom.Description = "The zoom level when in a building.";
            inBuildingZoom.SetRightLabel(RadarZoomSettings.ZoomLevelInBuilding.ToString());

            // Add options to the settings menu.
            settingsMenu.AddItem(onFootZoom);
            settingsMenu.AddItem(inVehicleZoom);
            settingsMenu.AddItem(inBuildingZoom);

            settingsMenu.RefreshIndex();

            // Subscribe to UI events.
            settingsMenu.OnMenuClose += SettingsMenu_OnMenuClose;
            onFootZoom.Activated += OnFootZoom_Activated;
            inVehicleZoom.Activated += InVehicleZoom_Activated;
            inBuildingZoom.Activated += InBuildingZoom_Activated;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the settings <see cref="UIMenu"/>.
        /// </summary>
        public UIMenu Menu => settingsMenu;
        #endregion Properties

        #region Methods
        /// <summary>
        /// Shows the menu.
        /// </summary>
        public void Show()
        {
            Menu.Visible = true;
        }
        #endregion Methods

        #region Event listeners
        private void SettingsMenu_OnMenuClose(UIMenu sender)
        {
            RadarZoomSettings.SaveSettings();
        }

        private void OnFootZoom_Activated(UIMenu sender, UIMenuItem selectedItem)
        {
            string userInput = Game.GetUserInput(RadarZoomSettings.ZoomLevelOnFoot.ToString(), 4);
            if (Regex.IsMatch(userInput, "^[0-9]+$"))
            {
                RadarZoomSettings.ZoomLevelOnFoot = int.Parse(userInput);
                onFootZoom.SetRightLabel(RadarZoomSettings.ZoomLevelOnFoot.ToString());
            }
        }

        private void InVehicleZoom_Activated(UIMenu sender, UIMenuItem selectedItem)
        {
            string userInput = Game.GetUserInput(RadarZoomSettings.ZoomLevelInVehicle.ToString(), 4);
            if (Regex.IsMatch(userInput, "^[0-9]+$"))
            {
                RadarZoomSettings.ZoomLevelInVehicle = int.Parse(userInput);
                inVehicleZoom.SetRightLabel(RadarZoomSettings.ZoomLevelInVehicle.ToString());
            }
        }

        private void InBuildingZoom_Activated(UIMenu sender, UIMenuItem selectedItem)
        {
            string userInput = Game.GetUserInput(RadarZoomSettings.ZoomLevelInBuilding.ToString(), 4);
            if (Regex.IsMatch(userInput, "^[0-9]+$"))
            {
                RadarZoomSettings.ZoomLevelInBuilding = int.Parse(userInput);
                inBuildingZoom.SetRightLabel(RadarZoomSettings.ZoomLevelInBuilding.ToString());
            }
        }
        #endregion Event listeners
    }
}
