namespace DeepWolf.RadarZoom
{
    using System.Text.RegularExpressions;
    using GTA;
    using NativeUI;

    internal class SettingsMenu
    {
        #region Constants
        public const string SettingsMenuTitle = "Radar Zoom";
        public const string SettingsMenuSubTitle = "Settings";
        #endregion Constants

        #region Fields
        private UIMenu settingsMenu;
        private UIMenuItem onFootZoom;
        private UIMenuItem inVehicleZoom;
        private UIMenuItem inBuildingZoom;
        #endregion Fields

        #region Constructors
        internal SettingsMenu()
        {
            // Define the settings menu.
            settingsMenu = new UIMenu(SettingsMenuTitle, SettingsMenuSubTitle);

            // Define UI elements.
            onFootZoom = new UIMenuItem("On Foot Zoom");
            onFootZoom.Description = "The zoom level when on foot.";
            onFootZoom.SetRightLabel(RadarZoomSettings.ZoomLevelOnFoot.ToString());

            inVehicleZoom = new UIMenuItem("In Vehicle Zoom");
            inVehicleZoom.Description = "The zoom level when in a vehicle.";
            inVehicleZoom.SetRightLabel(RadarZoomSettings.ZoomLevelInVehicle.ToString());

            inBuildingZoom = new UIMenuItem("In Building Zoom");
            inBuildingZoom.Description = "The zoom level when in a building.";
            inBuildingZoom.SetRightLabel(RadarZoomSettings.ZoomLevelInBuilding.ToString());


            // Add UI elements to settings menu.
            settingsMenu.AddItem(onFootZoom);
            settingsMenu.AddItem(inVehicleZoom);
            settingsMenu.AddItem(inBuildingZoom);

            settingsMenu.RefreshIndex();

            // Subscribe to UI elements events.
            settingsMenu.OnMenuClose += SettingsMenu_OnMenuClose;
            onFootZoom.Activated += OnFootZoom_Activated;
            inVehicleZoom.Activated += InVehicleZoom_Activated;
            inBuildingZoom.Activated += InBuildingZoom_Activated;
        }
        #endregion Constructors

        #region Properties
        public UIMenu Menu => settingsMenu;
        #endregion Properties

        #region Methods
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
