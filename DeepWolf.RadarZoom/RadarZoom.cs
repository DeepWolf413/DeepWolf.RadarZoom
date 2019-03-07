namespace DeepWolf.RadarZoom
{
    using System;
    using System.Windows.Forms;
    using GTA;
    using GTA.Native;
    using NativeUI;

    public class RadarZoom : Script
    {
        #region Fields
        private MenuPool menuPool;
        private SettingsMenu settingsMenu;

        /// <summary>
        /// The current zoom level.
        /// </summary>
        private int currentZoomLevel;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RadarZoom"/> class.
        /// </summary>
        public RadarZoom()
        {
            // Subscribe to events.
            Tick += OnTick;
            KeyDown += OnKeyDown;

            RadarZoomSettings.LoadSettings();

            // Initialize the settings menu
            settingsMenu = new SettingsMenu();

            // Initialize the menu pool.
            menuPool = new MenuPool();
            menuPool.Add(settingsMenu.Menu);

            // Let the user know that the mod has loaded.
            UI.Notify("Loaded RadarZoom.");
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Whether the player is in a building.
        /// </summary>
        /// <returns>A <see cref="bool"/> that determines whether the player is in a building.</returns>
        private bool IsPlayerInBuilding()
        {
            return Function.Call<bool>(Hash.IS_INTERIOR_SCENE);
        }

        /// <summary>
        /// Sets the radar's zoom level to the specified zoom level.
        /// </summary>
        /// <param name="newZoomLevel">The new zoom level.</param>
        private void SetZoomLevel(int newZoomLevel)
        {
            if (currentZoomLevel != newZoomLevel)
            {
                currentZoomLevel = newZoomLevel;
            }

            Game.RadarZoom = currentZoomLevel;
        }
        #endregion Methods

        #region Event Listeners
        /// <summary>
        /// Triggered every game tick.
        /// </summary>
        private void OnTick(object sender, EventArgs e)
        {
            menuPool.ProcessMenus();

            if (Game.Player.CanControlCharacter)
            {
                if (Game.Player.Character.IsInVehicle())
                {
                    SetZoomLevel(RadarZoomSettings.ZoomLevelInVehicle);
                }
                else if (IsPlayerInBuilding())
                {
                    SetZoomLevel(RadarZoomSettings.ZoomLevelInBuilding);
                }
                else
                {
                    SetZoomLevel(RadarZoomSettings.ZoomLevelOnFoot);
                }
            }
        }

        /// <summary>
        /// Triggered when the player presses a key down.
        /// </summary>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == RadarZoomSettings.OpenMenuKey)
            {
                if (settingsMenu != null)
                {
                    settingsMenu.Show();
                }
                else
                {
                    UI.Notify("Failed to open the settings menu.");
                }
            }
        }
        #endregion Event Listeners
    }
}