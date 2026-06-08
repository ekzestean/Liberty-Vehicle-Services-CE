

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GTA;
using GTA.Native;

namespace AutoWorkshopCE
{


    internal class AutoWorkshopCEModule
    {
        private GTA.Player Player { get { return Game.LocalPlayer; } }

        private bool enabled = true;
        private bool detectVanillaPayNSpray = false;
        private bool restorePreviousColoursAfterVanillaPayNSpray = true;
        private bool openMenuAfterVanillaPayNSprayFallback = false;
        private bool openMenuBeforeVanillaPayNSpray = false;
        private bool triggerPreMenuOnGarageInteraction = false;
        private bool allowManualOpenInVehicle = false;
        private bool freezeVehicleDuringMenu = true;
        private bool blockPlayerControlDuringMenu = true;
        private bool probeAvailableExtras = true;
        private bool preserveDamageDuringNonRepairWork = false;
        private bool requireRepairedVehicleForExtras = true;
        private bool showDebugMessages = false;
        private bool logToFile = true;
        private bool scriptOwnedCustomService = true;
        private bool abortVanillaGarageOnCancel = false;
        private bool simulateScriptedServiceFade = true;
        private bool clearWantedOnScriptedService = true;
        private bool requireNoWantedForWorkshop = true;
        private bool useScriptGarage = true;
        private bool requireLostControlForScriptGarageEntry = false;
        private int scriptGarageEntryDelayTicks = 0;
        private bool useSeparateGarageInterior = true;
        private bool scriptedQuickRespray = true;
        private bool teleportOutOnCancel = true;
        private float scriptGarageX = 877.740f;
        private float scriptGarageY = -115.060f;
        private float scriptGarageZ = 5.520f;
        private float scriptGarageHeading = 88.770f;
        private float exitBackOffset = 8.0f;
        private float garageEntryNudge = 3.0f;
        private bool useCoordinateWorkshopTriggers = true;
        private bool enableChasePointWorkshop = true;
        private bool alignInPlaceWorkshopVehicle = true;
        private bool allowVanillaGarageFallbackInScriptGarage = false;
        private float payNSprayTriggerRadius = 4.25f;
        private float payNSprayTriggerExitRadius = 9.0f;
        private int vanillaAbortBurstTicks = 0;
        private int vanillaAbortBurstRemaining = 0;
        private bool logPositionHotkeyEnabled = true;
        private Keys keyLogPosition = Keys.F7;
        private string workshopEpisodeKey = "iv";
        private bool requireStoppedForScriptGarageEntry = true;
        private float scriptGarageEntryMaxSpeed = 0.35f;
        private int scriptGarageSettleMs = 2500;
        private bool useLoadingScreenForGarageTeleport = false;
        private bool showEntryPrompt = true;
        private bool scriptGarageTransitioning = false;
        private bool useNativeWorkshopMenu = true;
        private bool showColourPalette = true;
        private int colourPaletteColumns = 16;
        private float colourPaletteCellWidth = 0.018f;
        private float colourPaletteCellHeight = 0.014f;
        private float colourPaletteGap = 0.002f;
        private bool vehicleColourPaletteLoaded = false;
        private int[] vehicleColourR = new int[256];
        private int[] vehicleColourG = new int[256];
        private int[] vehicleColourB = new int[256];
        private bool enableWorkshopBlip = true;
        private int workshopBlipSprite = 30;
        private float workshopBlipScale = 1.0f;
        private bool workshopBlipOnlyWhenNear = true;
        private string workshopBlipName = "Executive Lifestyle Autos";
        private Dictionary<string, Blip> workshopBlips = new Dictionary<string, Blip>();
        private Dictionary<string, int> workshopBlipRetryTicksByName = new Dictionary<string, int>();
        private const float WorkshopIvBrokerDukesUnlockProgress = 8.0f;
        private const float WorkshopIvBohanUnlockProgress = 15.0f;
        private const float WorkshopIvAlgonquinUnlockProgress = 32.0f;
        private const float WorkshopIvAlderneyUnlockProgress = 50.0f;
        private float workshopStoryProgressPercent = 0.0f;
        private bool workshopStoryProgressReliable = false;
        private int workshopStoryProgressRefreshTicks = 0;
        private bool workshopStoryProgressUnavailableLogged = false;
        private bool useFixedWorkshopExit = true;
        private float workshopExitX = 860.344f;
        private float workshopExitY = -114.575f;
        private float workshopExitZ = 5.551f;
        private float workshopExitHeading = 359.533f;


        private bool enableChasePointMechanicNpc = false;
        private string chasePointMechanicModels = "M_Y_MECHANIC_02,M_Y_MECHANIC_01";
        private float chasePointMechanicX = 708.652f;
        private float chasePointMechanicY = 1506.029f;
        private float chasePointMechanicZ = 14.395f;
        private float chasePointMechanicHeading = 253.889f;
        private float chasePointMechanicSpawnDistance = 90.0f;
        private bool chasePointMechanicUseScenario = false;
        private string chasePointMechanicScenario = "WORLD_HUMAN_VEHICLE_MECHANIC";
        private bool placeChasePointMechanicOnGround = true;
        private float chasePointMechanicZOffset = -1.0f;
        private Ped chasePointMechanicPed = null;
        private int chasePointMechanicRetryTicks = 0;
        private int chasePointMechanicRequestTicks = 0;
        private int chasePointMechanicModelIndex = 0;
        private int chasePointMechanicRequestedHash = 0;
        private bool chasePointMechanicModelRequested = false;


        private bool enableChasePointRepairScene = false;
        private string chasePointRepairCarModel = "esperanto";
        private float chasePointRepairCarX = 703.083f;
        private float chasePointRepairCarY = 1502.878f;
        private float chasePointRepairCarZ = 14.398f;
        private float chasePointRepairCarHeading = 175.157f;
        private float chasePointRepairSceneSpawnDistance = 90.0f;
        private int chasePointRepairCarHealth = 520;
        private int chasePointRepairCarEngineHealth = 80;
        private bool chasePointRepairCarOpenBonnet = false;
        private int chasePointRepairCarBonnetDoorIndex = 4;
        private int chasePointRepairCarBonnetRetryTicks = 0;
        private int chasePointRepairCarBonnetRetryMaxTicks = 180;
        private bool chasePointRepairCarUseControlDoor = true;
        private float chasePointRepairCarBonnetOpenAngle = 1.0f;
        private bool chasePointRepairCarUseAggressiveOpenDoor = false;
        private bool chasePointRepairCarFreeze = true;
        private string chasePointRepairCarModels = "";
        private bool randomiseChasePointRepairCarModel = false;
        private Vehicle chasePointRepairCar = null;
        private string activeChasePointRepairCarModel = "";
        private int chasePointRepairCarRetryTicks = 0;
        private int chasePointRepairCarRequestTicks = 0;
        private int chasePointRepairCarRequestedHash = 0;
        private bool chasePointRepairCarModelRequested = false;


        private bool chasePointMechanicAnchorToRepairCar = true;
        private float chasePointMechanicRepairCarOffsetX = 0.45f;
        private float chasePointMechanicRepairCarOffsetY = 2.75f;
        private float chasePointMechanicRepairCarOffsetZ = -0.35f;
        private bool chasePointMechanicFaceRepairCar = true;

        private int minColourID = 0;
        private int maxColourID = 133;
        private int colourStep = 1;
        private int minExtraID = 1;
        private int maxExtraID = 9;

        private int priceRepaintBase = 150;
        private int priceRepaintSpecular = 100;
        private int priceRepaintFeature1 = 100;
        private int priceRepaintFeature2 = 100;
        private int priceToggleExtra = 75;
        private int priceVanillaService = 100;
        private int priceRepair = 100;
        private bool useDynamicRepairPrice = true;
        private float repairMinimumFraction = 0.25f;
        private float repairEngineWeight = 0.45f;
        private float repairBodyWeight = 0.45f;
        private float repairTankWeight = 0.10f;
        private bool useSwanknessPriceModifiers = true;
        private float swankness0Multiplier = 1.00f;
        private float swankness1Multiplier = 1.00f;
        private float swankness2Multiplier = 2.00f;
        private float swankness3Multiplier = 4.00f;
        private float swankness4Multiplier = 6.00f;
        private float swankness5PlusMultiplier = 8.00f;
        private float swanknessFallbackMultiplier = 1.00f;
        private Dictionary<int, int> swanknessByModelHash = new Dictionary<int, int>();
        private Dictionary<string, int> swanknessByModelName = new Dictionary<string, int>();
        private Dictionary<int, int> swanknessOverrideByModelHash = new Dictionary<int, int>();
        private Dictionary<string, int> swanknessOverrideByModelName = new Dictionary<string, int>();
        private bool vehicleSwanknessLoaded = false;
        private int lastLoggedSwanknessHash = 0;
        private int scriptedServiceTimeSkipHours = 0;
        private bool allowScriptedServiceTimeSkip = false;
        private int scriptGarageFadeOutMs = 1200;
        private int scriptGarageFadeInMs = 1200;
        private int scriptGarageFadeHoldMs = 450;
        private Keys keyOpenMenu = Keys.E;
        private Keys keyUp = Keys.Up;
        private Keys keyDown = Keys.Down;
        private Keys keyLeft = Keys.Left;
        private Keys keyRight = Keys.Right;
        private Keys keySelect = Keys.Enter;
        private Keys keyBack = Keys.Back;
        private Keys keyExit = Keys.Escape;
        private int pauseMenuReleaseTicks = 0;
        private const int pauseMenuReleaseDelayTicks = 8;
        private bool pauseMenuDisabledByLvsMenu = false;
        private bool pauseMenuDisableUnavailableLogged = false;

        private Vehicle trackedVehicle = null;
        private int lastResprayStat = -1;
        private bool haveResprayStat = false;
        private bool wasInteractingWithGarage = false;
        private bool preMenuOpenedForCurrentGarageInteraction = false;
        private int preMenuCooldownTicks = 0;
        private int preMenuInteractionTicks = 0;
        private int preMenuOpenDelayTicks = 8;
        private bool suppressPreMenuUntilGarageExit = false;
        private bool suppressPostServiceFallbackOnce = false;
        private bool allowNextVanillaColour = false;
        private int pendingLostGarageTicks = 0;
        private int quickResprayLostGarageTicks = 0;
        private int pendingCancelAfterTicks = 20;
        private ColorIndex trackedBaseColour;
        private ColorIndex trackedSpecularColour;
        private ColorIndex trackedFeatureColour1;
        private ColorIndex trackedFeatureColour2;
        private float trackedHealth = 0.0f;
        private float trackedEngineHealth = 0.0f;
        private bool haveTrackedColours = false;

        private Vehicle serviceVehicle = null;
        private ColorIndex serviceOriginalBaseColour;
        private ColorIndex serviceOriginalSpecularColour;
        private ColorIndex serviceOriginalFeatureColour1;
        private ColorIndex serviceOriginalFeatureColour2;
        private ColorIndex serviceBaseColour;
        private ColorIndex serviceSpecularColour;
        private ColorIndex serviceFeatureColour1;
        private ColorIndex serviceFeatureColour2;
        private bool serviceOpen = false;
        private bool serviceBeforeVanilla = false;
        private bool serviceHadPaidWork = false;
        private bool serviceHadWantedAtEntry = false;
        private int menuLevel = 0;
        private int selectedIndex = 0;
        private int currentColourID = 0;
        private int previewPrice = 0;
        private int messageTimer = 0;
        private string messageText = "";
        private List<int> availableExtras = new List<int>();
        private Dictionary<int, bool> serviceOriginalExtras = new Dictionary<int, bool>();
        private Dictionary<int, bool> serviceCommittedExtras = new Dictionary<int, bool>();
        private int pendingPreviewExtra = -1;
        private int currentVehicleSwankness = -1;
        private float currentVehiclePriceMultiplier = 1.0f;
        private string currentVehiclePriceCategory = "standard";

        private bool pendingService = false;
        private ColorIndex pendingOriginalBaseColour;
        private ColorIndex pendingOriginalSpecularColour;
        private ColorIndex pendingOriginalFeatureColour1;
        private ColorIndex pendingOriginalFeatureColour2;
        private ColorIndex pendingBaseColour;
        private ColorIndex pendingSpecularColour;
        private ColorIndex pendingFeatureColour1;
        private ColorIndex pendingFeatureColour2;
        private Dictionary<int, bool> pendingExtras = new Dictionary<int, bool>();
        private int pendingExtraCharge = 0;
        private Vehicle vanillaWaitVehicle = null;
        private int vanillaWaitVehicleHandle = 0;
        private int vanillaWaitStartResprayStat = -1;
        private bool vanillaWaitHavePosition = false;
        private float vanillaWaitStartX = 0.0f;
        private float vanillaWaitStartY = 0.0f;
        private float vanillaWaitStartZ = 0.0f;
        private float vanillaWaitCancelDistance = 35.0f;

        private bool scriptGarageActive = false;
        private bool serviceInScriptGarage = false;
        private bool activeScriptGarageUsesSeparateInterior = true;
        private int scriptGarageEntryTicks = 0;
        private bool haveLastSafeExitTransform = false;
        private float lastSafeExitX = 0.0f;
        private float lastSafeExitY = 0.0f;
        private float lastSafeExitZ = 0.0f;
        private float lastSafeExitHeading = 0.0f;
        private bool haveScriptGarageExitTransform = false;
        private float scriptGarageExitX = 0.0f;
        private float scriptGarageExitY = 0.0f;
        private float scriptGarageExitZ = 0.0f;
        private float scriptGarageExitHeading = 0.0f;
        private Random random = new Random();

        private bool suspendWorkshopDuringMissions = true;
        private bool despawnWorkshopDecorationDuringMissions = true;
        private bool workshopMissionGuardWasActive = false;
        private bool cachedMissionActive = false;
        private int missionCheckCooldownTicks = 0;
        private int missionGuardPromptCooldownTicks = 0;
        private int missionCheckIntervalTicks = 90;
        private int missionPositiveRefreshes = 0;
        private int missionNegativeRefreshes = 0;
        private int missionRequiredPositiveRefreshes = 2;
        private bool missionFlagUnavailableLogged = false;
        private bool missionPropertyFailureLogged = false;
        private bool missionStateSourceLogged = false;
        private bool useNativeMissionFlagGuard = true;
        private bool missionNativeFlagSourceLogged = false;

        private bool chasePointMechanicUseWorkCycle = true;
        private int chasePointMechanicWorkTicks = 900;
        private int chasePointMechanicGarageTicks = 260;
        private int chasePointMechanicWalkTicks = 360;
        private float chasePointMechanicGarageX = 706.600f;
        private float chasePointMechanicGarageY = 1510.900f;
        private float chasePointMechanicGarageZ = 14.395f;
        private float chasePointMechanicGarageHeading = 180.000f;
        private int chasePointMechanicCycleState = 0;
        private int chasePointMechanicCycleTicks = 0;
        private bool chasePointMechanicCycleTaskIssued = false;

        private bool enableChasePointExtraStaff = false;
        private string chasePointExtraStaffModels = "M_Y_MECHANIC_01,M_Y_MECHANIC_02,M_Y_GARAGE";
        private string chasePointExtraStaffSpawns = "-3.20,3.40,-0.35,95;-6.20,-1.90,-0.35,15;3.80,-3.20,-0.35,260";
        private float chasePointExtraStaffSpawnDistance = 90.0f;
        private int chasePointExtraStaffRetryTicks = 0;
        private List<Ped> chasePointExtraStaffPeds = new List<Ped>();

        private class WorkshopTrigger
        {
            public string Name;
            public float X;
            public float Y;
            public float Z;
            public float Radius;
            public float Heading;
            public bool UseSeparateInterior;

            public WorkshopTrigger(string name, float x, float y, float z, float radius, float heading)
            {
                Name = name;
                X = x;
                Y = y;
                Z = z;
                Radius = radius;
                Heading = heading;
                UseSeparateInterior = true;
            }

            public WorkshopTrigger(string name, float x, float y, float z, float radius, float heading, bool useSeparateInterior)
            {
                Name = name;
                X = x;
                Y = y;
                Z = z;
                Radius = radius;
                Heading = heading;
                UseSeparateInterior = useSeparateInterior;
            }
        }

        private List<WorkshopTrigger> payNSprayTriggers = new List<WorkshopTrigger>();
        private WorkshopTrigger activeTrigger = null;

        private string iniPath = "scripts\\LibertyVehicleServicesCE.ini";

        public AutoWorkshopCEModule()
        {


            enabled = false;
            logToFile = false;
        }

        internal AutoWorkshopCEModule(bool embedded)
        {
            LoadIni();
            NormaliseMenuKeys();
            workshopEpisodeKey = DetectWorkshopEpisodeKey();
            RefreshWorkshopStoryProgress(true);
            LoadVehicleColourPaletteFromCarcols();
            LoadVehicleSwanknessData();
            EnsureWorkshopTriggers();
            Log("Embedded workshop module initialised.");
        }

        public void HostTick()
        {
            OnTick(this, EventArgs.Empty);
        }

        public void HostKeyDown(GTA.KeyEventArgs e)
        {
            OnKeyDown(this, e);
        }

        public bool HostHasOpenMenu()
        {
            return serviceOpen;
        }

        public void HostCloseMenuForMissionGuard()
        {
            if (serviceOpen) CloseMenu(false);
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!enabled) return;

            try
            {
                RefreshWorkshopStoryProgress(false);


                if (preMenuCooldownTicks > 0) preMenuCooldownTicks--;
                if (missionGuardPromptCooldownTicks > 0) missionGuardPromptCooldownTicks--;
                UpdatePauseMenuGuard(serviceOpen);
                if (vanillaAbortBurstRemaining > 0)
                {


                    vanillaAbortBurstRemaining--;
                }

                if (serviceOpen)
                {


                    int statWhileMenu = SafeGetResprayStat();
                    if (!useScriptGarage && serviceBeforeVanilla && haveResprayStat && statWhileMenu > lastResprayStat)
                    {
                        RestoreColours(serviceVehicle, serviceOriginalBaseColour, serviceOriginalSpecularColour, serviceOriginalFeatureColour1, serviceOriginalFeatureColour2);
                        serviceBeforeVanilla = false;
                        Log("Vanilla workshop completed while pre-menu was open. Converted to post-service menu. Current=" + statWhileMenu.ToString() + ".");
                        lastResprayStat = statWhileMenu;
                    }
                    DrawMenu();
                    return;
                }

                UpdateWorkshopBlip();
                UpdateChasePointRepairScene();
                UpdateChasePointMechanicNpc();
                UpdateChasePointExtraStaffPeds();

                if (!Player.Character.isSittingInVehicle())
                {
                    if (pendingService || allowNextVanillaColour)
                    {
                        CancelPendingService("player left the vehicle before vanilla service completed");
                    }
                    haveTrackedColours = false;
                    trackedVehicle = null;
                    haveResprayStat = false;
                    wasInteractingWithGarage = false;
                    preMenuInteractionTicks = 0;
                    preMenuOpenedForCurrentGarageInteraction = true;
                    suppressPreMenuUntilGarageExit = false;
                    suppressPostServiceFallbackOnce = false;
                    pendingLostGarageTicks = 0;
                    quickResprayLostGarageTicks = 0;
                    scriptGarageEntryTicks = 0;
                    haveLastSafeExitTransform = false;
                    scriptGarageActive = false;
                    serviceInScriptGarage = false;
                    activeTrigger = null;
                    return;
                }

                Vehicle v = Player.Character.CurrentVehicle;
                if (!IsValidVehicle(v))
                {
                    if (pendingService || allowNextVanillaColour)
                    {
                        CancelPendingService("current vehicle became invalid before vanilla service completed");
                    }
                    return;
                }

                bool candidateWorkshopUse = serviceOpen || pendingService || scriptGarageActive || serviceInScriptGarage || scriptGarageTransitioning || activeTrigger != null;
                if (!candidateWorkshopUse && useCoordinateWorkshopTriggers)
                {
                    try { candidateWorkshopUse = GetActiveWorkshopTrigger(v) != null; } catch { candidateWorkshopUse = false; }
                }

                if (suspendWorkshopDuringMissions && candidateWorkshopUse && IsMissionActiveSafe())
                {
                    if (!workshopMissionGuardWasActive)
                    {
                        workshopMissionGuardWasActive = true;
                        if (showDebugMessages) Log("MISSION_GUARD active: workshop service interaction suspended.");
                        if (despawnWorkshopDecorationDuringMissions) CleanupChasePointDecoration("mission guard");
                    }
                    if (serviceOpen) CloseMenu(false);
                    if (showEntryPrompt) DrawEntryPrompt("Garage unavailable during missions.");
                    if (missionGuardPromptCooldownTicks <= 0)
                    {
                        Notify("Garage unavailable during missions.");
                        missionGuardPromptCooldownTicks = 120;
                    }
                    return;
                }
                if (workshopMissionGuardWasActive && (!candidateWorkshopUse || !IsMissionActiveSafe()))
                {
                    workshopMissionGuardWasActive = false;
                    if (showDebugMessages) Log("MISSION_GUARD released: workshop interaction resumed.");
                }

                int currentResprayStat = SafeGetResprayStat();
                bool interactingWithGarage = false;

                if (useScriptGarage)
                {
                    OnTickScriptGarageMode(v, currentResprayStat, false);
                    return;
                }

                interactingWithGarage = SafePlayerIsInteractingWithGarage();

                GuardVanillaWaitVehicle(v, currentResprayStat);

                if (interactingWithGarage) preMenuInteractionTicks++;
                else preMenuInteractionTicks = 0;

                if (interactingWithGarage)
                {
                    pendingLostGarageTicks = 0;
                    quickResprayLostGarageTicks = 0;
                }
                else
                {
                    if (pendingService)
                    {
                        pendingLostGarageTicks++;
                        if (pendingLostGarageTicks >= pendingCancelAfterTicks)
                        {
                            CancelPendingService("garage interaction ended before vanilla service completed");
                        }
                    }
                    else if (allowNextVanillaColour)
                    {
                        quickResprayLostGarageTicks++;
                        if (quickResprayLostGarageTicks >= pendingCancelAfterTicks)
                        {
                            allowNextVanillaColour = false;
                            ClearVanillaWait();
                            suppressPreMenuUntilGarageExit = false;
                            suppressPostServiceFallbackOnce = false;
                            Log("Quick respray cancelled: garage interaction ended before vanilla service completed.");
                        }
                    }
                    else
                    {
                        suppressPreMenuUntilGarageExit = false;
                        suppressPostServiceFallbackOnce = false;
                    }
                }

                if (!haveTrackedColours || trackedVehicle == null || !trackedVehicle.Exists())
                {
                    TrackVehicle(v);
                    lastResprayStat = currentResprayStat;
                    haveResprayStat = true;
                    wasInteractingWithGarage = interactingWithGarage;
                    Debug("Tracking vehicle. Respray stat " + currentResprayStat.ToString() + ".");
                    return;
                }

                if (!interactingWithGarage)
                {
                    preMenuOpenedForCurrentGarageInteraction = false;
                }


                if (openMenuBeforeVanillaPayNSpray && triggerPreMenuOnGarageInteraction && interactingWithGarage && preMenuInteractionTicks >= preMenuOpenDelayTicks && !suppressPreMenuUntilGarageExit && !pendingService && !preMenuOpenedForCurrentGarageInteraction && preMenuCooldownTicks <= 0)
                {
                    preMenuOpenedForCurrentGarageInteraction = true;
                    OpenServiceMenu(v, true);
                    Log("Opened pre-service menu on garage interaction. Respray stat=" + currentResprayStat.ToString() + ".");
                    return;
                }


                if (detectVanillaPayNSpray && haveResprayStat && currentResprayStat > lastResprayStat)
                {
                    ColorIndex oldBase = trackedBaseColour;
                    ColorIndex oldSpecular = trackedSpecularColour;
                    ColorIndex oldFeature1 = trackedFeatureColour1;
                    ColorIndex oldFeature2 = trackedFeatureColour2;

                    if (pendingService)
                    {
                        if (IsVanillaWaitVehicle(v))
                        {
                            ApplyPendingService(v);
                            Notify("workshop service complete.");
                            suppressPreMenuUntilGarageExit = true;
                            suppressPostServiceFallbackOnce = true;
                            Log("Detected workshop via stat 282 and applied pending service. Current=" + currentResprayStat.ToString() + ".");
                        }
                        else
                        {
                            CancelPendingService("stat 282 changed in a different vehicle");
                            Log("Detected workshop via stat 282, but pending service was not applied because the vehicle did not match. Current=" + currentResprayStat.ToString() + ".");
                        }
                    }
                    else
                    {
                        bool quickForThisVehicle = allowNextVanillaColour && IsVanillaWaitVehicle(v);
                        if (allowNextVanillaColour && !quickForThisVehicle)
                        {
                            allowNextVanillaColour = false;
                            ClearVanillaWait();
                            Log("Quick respray wait cleared because stat 282 changed in a different vehicle. Current=" + currentResprayStat.ToString() + ".");
                        }

                        if (restorePreviousColoursAfterVanillaPayNSpray && !quickForThisVehicle)
                        {
                            RestoreColours(v, oldBase, oldSpecular, oldFeature1, oldFeature2);
                            Notify("workshop complete. Colours restored.");
                        }
                        else
                        {
                            Notify("Quick respray complete.");
                        }

                        Log("Detected workshop via stat 282. Current=" + currentResprayStat.ToString() + ", Quick=" + quickForThisVehicle.ToString() + ".");

                        if (openMenuAfterVanillaPayNSprayFallback && !suppressPostServiceFallbackOnce)
                        {
                            OpenServiceMenu(v, false);
                        }

                        allowNextVanillaColour = false;
                        ClearVanillaWait();
                        suppressPostServiceFallbackOnce = false;
                        suppressPreMenuUntilGarageExit = true;
                    }

                    TrackVehicle(v);
                    lastResprayStat = currentResprayStat;
                    haveResprayStat = true;
                    wasInteractingWithGarage = interactingWithGarage;
                    preMenuOpenedForCurrentGarageInteraction = true;
                    return;
                }


                if (interactingWithGarage || !Player.CanControlCharacter)
                {
                    wasInteractingWithGarage = interactingWithGarage;
                    return;
                }


                TrackVehicle(v);
                lastResprayStat = currentResprayStat;
                haveResprayStat = true;
                wasInteractingWithGarage = false;
            }
            catch (Exception ex)
            {
                Notify("Workshop module error: " + ex.GetType().Name);
                Log("Tick error: " + ex.ToString());
            }
        }

        private void OnKeyDown(object sender, GTA.KeyEventArgs e)
        {
            if (!enabled) return;

            if (logPositionHotkeyEnabled && e.Key == keyLogPosition)
            {
                LogCurrentVehiclePositionForConfig();
                return;
            }

            if (serviceOpen)
            {
                ConsumeKeyEvent(e);
                HandleMenuKey(e.Key);
                return;
            }

            if (e.Key == keyOpenMenu && allowManualOpenInVehicle)
            {
                if (suspendWorkshopDuringMissions && IsMissionActiveSafe())
                {
                    Notify("Workshop unavailable during missions.");
                    return;
                }
                if (Player.Character.isSittingInVehicle())
                {
                    Vehicle v = Player.Character.CurrentVehicle;
                    if (IsValidVehicle(v)) OpenServiceMenu(v, true);
                }
            }
        }

        private void ConsumeKeyEvent(GTA.KeyEventArgs e)
        {
            if (e == null) return;
            try
            {
                PropertyInfo p = e.GetType().GetProperty("Handled");
                if (p != null && p.CanWrite) p.SetValue(e, true, null);
            }
            catch { }
        }

        private void UpdatePauseMenuGuard(bool menuActive)
        {
            if (menuActive)
            {
                pauseMenuReleaseTicks = pauseMenuReleaseDelayTicks;
                SetPauseMenuDisabledForLvs(true);
                return;
            }
            if (pauseMenuReleaseTicks > 0)
            {
                pauseMenuReleaseTicks--;
                SetPauseMenuDisabledForLvs(true);
                return;
            }
            if (pauseMenuDisabledByLvsMenu) SetPauseMenuDisabledForLvs(false);
        }

        private void HoldPauseMenuDisabledBriefly()
        {
            if (pauseMenuReleaseTicks < pauseMenuReleaseDelayTicks) pauseMenuReleaseTicks = pauseMenuReleaseDelayTicks;
            SetPauseMenuDisabledForLvs(true);
        }

        private void SetPauseMenuDisabledForLvs(bool disabled)
        {
            try
            {
                Function.Call("DISABLE_PAUSE_MENU", disabled);
                pauseMenuDisabledByLvsMenu = disabled;
            }
            catch (Exception ex)
            {
                if (!pauseMenuDisableUnavailableLogged)
                {
                    pauseMenuDisableUnavailableLogged = true;
                    Log("Pause-menu suppression unavailable: " + ex.GetType().Name + ".");
                }
            }
        }

        private void LogCurrentVehiclePositionForConfig()
        {
            try
            {
                if (!Player.Character.isSittingInVehicle())
                {
                    Notify("Position log: not in vehicle.");
                    return;
                }
                Vehicle v = Player.Character.CurrentVehicle;
                float x;
                float y;
                float z;
                if (!TryGetVehiclePosition(v, out x, out y, out z))
                {
                    Notify("Position log failed.");
                    return;
                }
                float h = SafeGetVehicleHeading(v, 0.0f);
                string line = "POSITION X=" + x.ToString("0.000") + ", Y=" + y.ToString("0.000") + ", Z=" + z.ToString("0.000") + ", H=" + h.ToString("0.000") + ". INI trigger token: WorkshopTrigger=Name," + x.ToString("0.000") + "," + y.ToString("0.000") + "," + z.ToString("0.000") + ",5.0," + h.ToString("0.000") + ". Exit token: WorkshopExit=" + x.ToString("0.000") + "," + y.ToString("0.000") + "," + z.ToString("0.000") + "," + h.ToString("0.000") + ".";
                Notify("Position written to LibertyVehicleServicesCE.log.");
                Log(line);
            }
            catch (Exception ex)
            {
                Log("Position log error: " + ex.GetType().Name + ".");
            }
        }

        private void HandleMenuKey(Keys key)
        {
            if (key == Keys.Escape || (keyExit != keyBack && key == keyExit))
            {
                HoldPauseMenuDisabledBriefly();
                if (menuLevel != 0)
                {
                    CancelPreviewIfNeeded();
                    menuLevel = 0;
                    selectedIndex = 0;
                    return;
                }
                if (serviceInScriptGarage || scriptGarageActive)
                {
                    ExitScriptGarageFromMenu();
                }
                else
                {
                    CloseMenu(false);
                }
                return;
            }

            if (key == keyBack)
            {
                return;
            }

            if (key == keyUp)
            {
                if (IsColourMenu() && showColourPalette)
                {
                    ShiftColourPreview(-Math.Max(4, colourPaletteColumns));
                    return;
                }

                int count = GetCurrentMenuCount();
                if (count > 0)
                {
                    selectedIndex--;
                    if (selectedIndex < 0) selectedIndex = count - 1;
                    OnSelectionChanged();
                }
                return;
            }

            if (key == keyDown)
            {
                if (IsColourMenu() && showColourPalette)
                {
                    ShiftColourPreview(Math.Max(4, colourPaletteColumns));
                    return;
                }

                int count = GetCurrentMenuCount();
                if (count > 0)
                {
                    selectedIndex++;
                    if (selectedIndex >= count) selectedIndex = 0;
                    OnSelectionChanged();
                }
                return;
            }

            if (key == keyLeft)
            {
                if (IsColourMenu())
                {
                    ShiftColourPreview(-colourStep);
                }
                else if (menuLevel == 5)
                {
                    PreviewSelectedExtra(false);
                }
                return;
            }

            if (key == keyRight)
            {
                if (IsColourMenu())
                {
                    ShiftColourPreview(colourStep);
                }
                else if (menuLevel == 5)
                {
                    PreviewSelectedExtra(true);
                }
                return;
            }

            if (key == keySelect)
            {
                SelectCurrentItem();
                return;
            }
        }

        private void ShiftColourPreview(int delta)
        {
            if (!IsColourMenu()) return;
            int count = Math.Max(1, maxColourID - minColourID + 1);
            int offset = currentColourID - minColourID;
            offset += delta;
            while (offset < 0) offset += count;
            while (offset >= count) offset -= count;
            currentColourID = minColourID + offset;
            ApplyPreviewColour();
        }

        private void SelectCurrentItem()
        {
            if (!IsValidVehicle(serviceVehicle))
            {
                CloseMenu(false);
                return;
            }

            if (serviceInScriptGarage || scriptGarageActive)
            {
                SelectCurrentWorkshopItem();
                return;
            }

            if (menuLevel == 0)
            {
                if (serviceBeforeVanilla)
                {

                    if (selectedIndex == 0) { QuickRespray(); return; }
                    if (selectedIndex == 1)
                    {
                        RestoreOriginalServiceState();
                        Notify("Repair-only selected. Confirm service to proceed.");
                        return;
                    }
                    if (selectedIndex == 2) { BeginColourMenu(1, priceRepaintBase, serviceVehicle.Color); return; }
                    if (selectedIndex == 3) { BeginColourMenu(2, priceRepaintSpecular, serviceVehicle.SpecularColor); return; }
                    if (selectedIndex == 4) { BeginColourMenu(3, priceRepaintFeature1, serviceVehicle.FeatureColor1); return; }
                    if (selectedIndex == 5) { BeginColourMenu(4, priceRepaintFeature2, serviceVehicle.FeatureColor2); return; }
                    if (selectedIndex == 6) { BeginExtrasMenu(); return; }
                    if (selectedIndex == 7) { ConfirmPreService(); return; }
                    if (selectedIndex == 8) { CloseMenu(false); suppressPreMenuUntilGarageExit = true; suppressPostServiceFallbackOnce = true; return; }
                }
                else
                {

                    if (selectedIndex == 0)
                    {
                        RepairVehicle(serviceVehicle);
                        RestoreColours(serviceVehicle, serviceBaseColour, serviceSpecularColour, serviceFeatureColour1, serviceFeatureColour2);
                        Notify("Vehicle repaired. Colours kept.");
                        return;
                    }
                    if (selectedIndex == 1) { BeginColourMenu(1, priceRepaintBase, serviceVehicle.Color); return; }
                    if (selectedIndex == 2) { BeginColourMenu(2, priceRepaintSpecular, serviceVehicle.SpecularColor); return; }
                    if (selectedIndex == 3) { BeginColourMenu(3, priceRepaintFeature1, serviceVehicle.FeatureColor1); return; }
                    if (selectedIndex == 4) { BeginColourMenu(4, priceRepaintFeature2, serviceVehicle.FeatureColor2); return; }
                    if (selectedIndex == 5) { BeginExtrasMenu(); return; }
                    if (selectedIndex == 6) { CloseMenu(true); return; }
                }
            }

            if (IsColourMenu())
            {
                if (serviceBeforeVanilla)
                {
                    CommitPreviewColour();
                    menuLevel = 0;
                    selectedIndex = 0;
                    Notify("Colour selected. Confirm service to apply.");
                    return;
                }
                else
                {
                    if (TryCharge(previewPrice))
                    {
                        CommitPreviewColour();
                        menuLevel = 0;
                        selectedIndex = 0;
                        Notify("Colour changed.");
                    }
                    return;
                }
            }

            if (menuLevel == 5)
            {
                ToggleSelectedExtra();
                return;
            }
        }

        private void SelectCurrentWorkshopItem()
        {
            if (menuLevel == 0)
            {
                if (selectedIndex == 0) { RepairVehicleFromWorkshop(); return; }
                if (selectedIndex == 1) { BeginColourMenu(1, GetWorkshopPrice(priceRepaintBase), serviceVehicle.Color); return; }
                if (selectedIndex == 2) { BeginColourMenu(2, GetWorkshopPrice(priceRepaintSpecular), serviceVehicle.SpecularColor); return; }
                if (selectedIndex == 3) { BeginColourMenu(3, GetWorkshopPrice(priceRepaintFeature1), serviceVehicle.FeatureColor1); return; }
                if (selectedIndex == 4) { BeginColourMenu(4, GetWorkshopPrice(priceRepaintFeature2), serviceVehicle.FeatureColor2); return; }
                if (selectedIndex == 5) { BeginExtrasMenu(); return; }
                if (selectedIndex == 6) { ExitScriptGarageFromMenu(); return; }
                return;
            }

            if (IsColourMenu())
            {
                ApplyWorkshopColourSelection();
                return;
            }

            if (menuLevel == 5)
            {
                PurchaseSelectedExtraPreview();
                return;
            }
        }

        private void RepairVehicleFromWorkshop()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            int cost = GetRepairPrice(serviceVehicle);
            if (!VehicleNeedsRepair(serviceVehicle))
            {
                Notify("Vehicle does not need repairs.");
                return;
            }
            if (!TryCharge(cost)) return;
            RepairVehicle(serviceVehicle);
            serviceHadPaidWork = true;
            Notify("Vehicle repaired. Charged $" + cost.ToString() + ".");
            Log("Workshop repair applied. Base=$" + priceRepair.ToString() + ", Cost=$" + cost.ToString() + ", Damage=" + GetRepairDamageSeverity(serviceVehicle).ToString("0.00") + ", Class=" + currentVehiclePriceCategory + " x" + currentVehiclePriceMultiplier.ToString("0.00") + ".");
        }

        private void ApplyWorkshopColourSelection()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            if (!IsCurrentPreviewDifferentFromCommitted())
            {
                CancelPreviewIfNeeded();
                menuLevel = 0;
                selectedIndex = 0;
                Notify("No colour change selected.");
                return;
            }
            if (!TryCharge(previewPrice))
            {
                CancelPreviewIfNeeded();
                return;
            }
            CommitPreviewColour();
            serviceHadPaidWork = true;
            menuLevel = 0;
            selectedIndex = 0;
            Notify("Colour applied. Charged $" + previewPrice.ToString() + ".");
            Log("Workshop colour applied. Cost=$" + previewPrice.ToString() + ", Class=" + currentVehiclePriceCategory + " x" + currentVehiclePriceMultiplier.ToString("0.00") + ".");
        }

        private bool IsCurrentPreviewDifferentFromCommitted()
        {
            if (!IsValidVehicle(serviceVehicle)) return false;
            if (menuLevel == 1) return serviceVehicle.Color.Index != serviceBaseColour.Index;
            if (menuLevel == 2) return serviceVehicle.SpecularColor.Index != serviceSpecularColour.Index;
            if (menuLevel == 3) return serviceVehicle.FeatureColor1.Index != serviceFeatureColour1.Index;
            if (menuLevel == 4) return serviceVehicle.FeatureColor2.Index != serviceFeatureColour2.Index;
            return false;
        }

        private void ExitScriptGarageFromMenu()
        {
            CancelPreviewIfNeeded();
            if (!IsValidVehicle(serviceVehicle))
            {
                serviceOpen = false;
                serviceBeforeVanilla = false;
                Player.CanControlCharacter = true;
                return;
            }

            Vehicle v = serviceVehicle;
            bool fadeStarted = false;
            try
            {
                if (simulateScriptedServiceFade)
                {
                    SafeFadeScreenOut(scriptGarageFadeOutMs, true);
                    fadeStarted = true;
                    SafeWait(scriptGarageFadeHoldMs);
                }
                if (serviceHadPaidWork)
                {
                    if (clearWantedOnScriptedService) SafeClearWantedLevel();
                    SafeSkipServiceTime(scriptedServiceTimeSkipHours);
                    Log("Workshop exit after paid work. WantedAtEntry=" + serviceHadWantedAtEntry.ToString() + ".");
                }
                TeleportOutOfScriptGarage(v);
            }
            catch (Exception ex)
            {
                Notify("Workshop exit failed: " + ex.GetType().Name);
                Log("Workshop exit error: " + ex.ToString());
            }
            finally
            {
                serviceOpen = false;
                serviceBeforeVanilla = false;
                HoldPauseMenuDisabledBriefly();
                menuLevel = 0;
                selectedIndex = 0;
                FreezeServiceVehicle(false);
                Player.CanControlCharacter = true;
                if (fadeStarted) SafeFadeScreenIn(scriptGarageFadeInMs, false);
                if (IsValidVehicle(v)) TrackVehicle(v);
                serviceHadPaidWork = false;
            }
        }

        private void BeginColourMenu(int level, int price, ColorIndex current)
        {
            menuLevel = level;
            selectedIndex = 0;
            previewPrice = price;
            currentColourID = ClampColourID(current.Index);
            ApplyPreviewColour();
        }

        private void BeginExtrasMenu()
        {
            if (!CanUseExtras(serviceVehicle))
            {
                menuLevel = 0;
                selectedIndex = 0;
                Notify("Repair vehicle before installing extras.");
                Log("Extras menu blocked: vehicle is not fully repaired.");
                return;
            }

            menuLevel = 5;
            selectedIndex = 0;
            pendingPreviewExtra = -1;
            BuildAvailableExtras();
            if (availableExtras.Count == 0)
            {
                Notify("No available extras found for this vehicle.");
            }
        }

        private void OnSelectionChanged()
        {
            if (IsColourMenu()) ApplyPreviewColour();


        }

        private void OpenServiceMenu(Vehicle v, bool beforeVanilla)
        {
            if (!IsValidVehicle(v)) return;
            serviceVehicle = v;
            ResolveCurrentVehiclePriceClass(v);
            serviceBeforeVanilla = beforeVanilla;
            serviceOriginalBaseColour = v.Color;
            serviceOriginalSpecularColour = v.SpecularColor;
            serviceOriginalFeatureColour1 = v.FeatureColor1;
            serviceOriginalFeatureColour2 = v.FeatureColor2;
            serviceBaseColour = v.Color;
            serviceSpecularColour = v.SpecularColor;
            serviceFeatureColour1 = v.FeatureColor1;
            serviceFeatureColour2 = v.FeatureColor2;
            serviceOpen = true;
            serviceHadPaidWork = false;
            serviceHadWantedAtEntry = SafeGetWantedLevel() > 0;
            if (!scriptGarageActive) serviceInScriptGarage = false;
            menuLevel = 0;
            selectedIndex = 0;
            availableExtras.Clear();
            serviceOriginalExtras.Clear();
            serviceCommittedExtras.Clear();
            pendingExtras.Clear();
            if (beforeVanilla && scriptOwnedCustomService && !scriptGarageActive && !serviceInScriptGarage)
            {
                SafeAbortGarageActivity("legacy pre-service menu opened");
            }
            if (blockPlayerControlDuringMenu) Player.CanControlCharacter = false;
            if (beforeVanilla && freezeVehicleDuringMenu) FreezeServiceVehicle(true);
            if (!(scriptGarageActive || serviceInScriptGarage)) Notify(beforeVanilla ? "Workshop services opened." : "Workshop fallback services opened.");
        }

        private void CloseMenu(bool keepCurrent)
        {
            bool wasBeforeVanilla = serviceBeforeVanilla;
            bool wasScriptGarage = serviceInScriptGarage || scriptGarageActive;
            Vehicle closeVehicle = serviceVehicle;
            if (!keepCurrent && IsValidVehicle(serviceVehicle))
            {
                RestoreOriginalServiceState();
            }
            if (wasBeforeVanilla) preMenuCooldownTicks = 300;
            if (wasBeforeVanilla && !wasScriptGarage && !keepCurrent && scriptOwnedCustomService && abortVanillaGarageOnCancel)
            {
                SafeAbortGarageActivity("pre-service menu closed without scripted service");
                suppressPreMenuUntilGarageExit = true;
                suppressPostServiceFallbackOnce = true;
                preMenuOpenedForCurrentGarageInteraction = true;
            }
            serviceOpen = false;
            serviceBeforeVanilla = false;
            menuLevel = 0;
            selectedIndex = 0;
            FreezeServiceVehicle(false);
            Player.CanControlCharacter = true;
            if (wasScriptGarage && !keepCurrent && teleportOutOnCancel)
            {
                FadeTeleportOutOfScriptGarage(closeVehicle);
            }
            else if (wasScriptGarage)
            {
                scriptGarageActive = false;
                serviceInScriptGarage = false;
            }
            if (IsValidVehicle(serviceVehicle)) TrackVehicle(serviceVehicle);
        }

        private void ConfirmScriptOwnedService()
        {
            if (!IsValidVehicle(serviceVehicle))
            {
                CloseMenu(false);
                return;
            }

            Vehicle v = serviceVehicle;
            int extraCost = ComputePreServiceExtraCost();
            int serviceFee = Math.Max(0, priceVanillaService);
            int totalCost = serviceFee + extraCost;
            if (totalCost > 0 && Player.Money < totalCost)
            {
                Notify("Not enough money. Need $" + totalCost.ToString() + ".");
                return;
            }

            ColorIndex chosenBaseColour = serviceBaseColour;
            ColorIndex chosenSpecularColour = serviceSpecularColour;
            ColorIndex chosenFeatureColour1 = serviceFeatureColour1;
            ColorIndex chosenFeatureColour2 = serviceFeatureColour2;
            Dictionary<int, bool> chosenExtras = new Dictionary<int, bool>();
            foreach (KeyValuePair<int, bool> pair in serviceOriginalExtras)
            {
                chosenExtras[pair.Key] = IsExtraOn(v, pair.Key);
            }

            bool fadeStarted = false;
            bool charged = false;
            try
            {
                SafeAbortGarageActivity("script-owned service confirmed");
                suppressPreMenuUntilGarageExit = true;
                suppressPostServiceFallbackOnce = true;
                preMenuOpenedForCurrentGarageInteraction = true;
                preMenuCooldownTicks = 300;

                if (simulateScriptedServiceFade)
                {
                    SafeFadeScreenOut(scriptGarageFadeOutMs, true);
                    fadeStarted = true;
                    SafeWait(scriptGarageFadeHoldMs);
                }

                if (totalCost > 0)
                {
                    if (!TryCharge(totalCost)) return;
                    charged = true;
                }

                RepairVehicle(v);
                RestoreColours(v, chosenBaseColour, chosenSpecularColour, chosenFeatureColour1, chosenFeatureColour2);
                foreach (KeyValuePair<int, bool> pair in chosenExtras)
                {
                    SetExtraState(v, pair.Key, pair.Value);
                }
                if (clearWantedOnScriptedService) SafeClearWantedLevel();
                SafeSkipServiceTime(scriptedServiceTimeSkipHours);

                pendingExtras.Clear();
                pendingService = false;
                pendingExtraCharge = 0;
                pendingLostGarageTicks = 0;
                allowNextVanillaColour = false;
                quickResprayLostGarageTicks = 0;
                ClearVanillaWait();

                Notify("Service complete. Charged $" + totalCost.ToString() + ".");
                Log("Script-owned service complete. Base fee=$" + serviceFee.ToString() + ", Extra=$" + extraCost.ToString() + ", Total=$" + totalCost.ToString() + ", Charged=" + charged.ToString() + ".");
            }
            catch (Exception ex)
            {
                Notify("Script-owned service failed: " + ex.GetType().Name);
                Log("Script-owned service error: " + ex.ToString());
            }
            finally
            {
                bool wasScriptGarage = serviceInScriptGarage || scriptGarageActive;
                if (wasScriptGarage)
                {
                    TeleportOutOfScriptGarage(v);
                }
                if (fadeStarted) SafeFadeScreenIn(scriptGarageFadeInMs, false);
                serviceOpen = false;
                serviceBeforeVanilla = false;
                menuLevel = 0;
                selectedIndex = 0;
                FreezeServiceVehicle(false);
                Player.CanControlCharacter = true;
                if (IsValidVehicle(v)) TrackVehicle(v);
                lastResprayStat = SafeGetResprayStat();
                haveResprayStat = true;
            }
        }

        private void ConfirmPreService()
        {
            if (!IsValidVehicle(serviceVehicle))
            {
                CloseMenu(false);
                return;
            }

            if (scriptOwnedCustomService)
            {
                ConfirmScriptOwnedService();
                return;
            }

            int extraCost = ComputePreServiceExtraCost();
            if (extraCost > 0 && Player.Money < extraCost)
            {
                Notify("Not enough money. Need $" + extraCost.ToString() + " plus the vanilla workshop fee.");
                return;
            }

            pendingOriginalBaseColour = serviceOriginalBaseColour;
            pendingOriginalSpecularColour = serviceOriginalSpecularColour;
            pendingOriginalFeatureColour1 = serviceOriginalFeatureColour1;
            pendingOriginalFeatureColour2 = serviceOriginalFeatureColour2;
            pendingBaseColour = serviceBaseColour;
            pendingSpecularColour = serviceSpecularColour;
            pendingFeatureColour1 = serviceFeatureColour1;
            pendingFeatureColour2 = serviceFeatureColour2;
            pendingExtras.Clear();
            foreach (KeyValuePair<int, bool> pair in serviceOriginalExtras)
            {
                int extra = pair.Key;
                bool nowOn = IsExtraOn(serviceVehicle, extra);
                if (nowOn != pair.Value) pendingExtras[extra] = nowOn;
            }
            pendingExtraCharge = extraCost;
            pendingLostGarageTicks = 0;
            pendingService = true;
            BeginVanillaWait(serviceVehicle, SafeGetResprayStat());
            suppressPreMenuUntilGarageExit = true;
            suppressPostServiceFallbackOnce = true;


            RestoreOriginalServiceState();
            serviceOpen = false;
            serviceBeforeVanilla = false;
            menuLevel = 0;
            selectedIndex = 0;
            FreezeServiceVehicle(false);
            Player.CanControlCharacter = true;
            Notify("Service confirmed. Let workshop finish the job.");
            Log("Pre-service confirmed. Extra charge=$" + extraCost.ToString() + " reserved. Waiting for stat 282.");
        }

        private void QuickRespray()
        {
            if (!IsValidVehicle(serviceVehicle))
            {
                CloseMenu(false);
                return;
            }
            if (useScriptGarage || scriptedQuickRespray)
            {
                ConfirmScriptedQuickRespray();
                return;
            }
            RestoreOriginalServiceState();
            allowNextVanillaColour = true;
            BeginVanillaWait(serviceVehicle, SafeGetResprayStat());
            suppressPreMenuUntilGarageExit = true;
            suppressPostServiceFallbackOnce = true;
            preMenuOpenedForCurrentGarageInteraction = true;
            quickResprayLostGarageTicks = 0;
            serviceOpen = false;
            serviceBeforeVanilla = false;
            menuLevel = 0;
            selectedIndex = 0;
            FreezeServiceVehicle(false);
            Player.CanControlCharacter = true;
            Notify("Quick respray selected. Let workshop finish.");
            Log("Quick respray selected. Waiting for vanilla stat 282.");
        }

        private void CancelPendingService(string reason)
        {
            pendingExtras.Clear();
            pendingService = false;
            pendingExtraCharge = 0;
            pendingLostGarageTicks = 0;
            allowNextVanillaColour = false;
            quickResprayLostGarageTicks = 0;
            ClearVanillaWait();
            suppressPreMenuUntilGarageExit = false;
            suppressPostServiceFallbackOnce = false;
            Notify("workshop service cancelled.");
            Log("Pending service cancelled: " + reason + ".");
        }

        private int ComputePreServiceExtraCost()
        {
            int cost = 0;
            if (serviceBaseColour.Index != serviceOriginalBaseColour.Index) cost += priceRepaintBase;
            if (serviceSpecularColour.Index != serviceOriginalSpecularColour.Index) cost += priceRepaintSpecular;
            if (serviceFeatureColour1.Index != serviceOriginalFeatureColour1.Index) cost += priceRepaintFeature1;
            if (serviceFeatureColour2.Index != serviceOriginalFeatureColour2.Index) cost += priceRepaintFeature2;

            foreach (KeyValuePair<int, bool> pair in serviceOriginalExtras)
            {
                int extra = pair.Key;
                bool nowOn = IsExtraOn(serviceVehicle, extra);

                if (nowOn && !pair.Value) cost += priceToggleExtra;
            }
            return cost;
        }

        private void ApplyPendingService(Vehicle v)
        {
            if (!IsValidVehicle(v)) return;
            if (pendingExtraCharge > 0 && !TryCharge(pendingExtraCharge))
            {
                RestoreColours(v, pendingOriginalBaseColour, pendingOriginalSpecularColour, pendingOriginalFeatureColour1, pendingOriginalFeatureColour2);
                pendingExtras.Clear();
                pendingService = false;
                pendingExtraCharge = 0;
                pendingLostGarageTicks = 0;
                ClearVanillaWait();
                Log("Pending service could not be charged after vanilla workshop. Restored original colours.");
                return;
            }

            RestoreColours(v, pendingBaseColour, pendingSpecularColour, pendingFeatureColour1, pendingFeatureColour2);
            foreach (KeyValuePair<int, bool> pair in pendingExtras)
            {
                SetExtraState(v, pair.Key, pair.Value);
            }
            pendingExtras.Clear();
            pendingService = false;
            pendingExtraCharge = 0;
            pendingLostGarageTicks = 0;
            ClearVanillaWait();
        }

        private void CancelPreviewIfNeeded()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            if (menuLevel == 1) serviceVehicle.Color = serviceBaseColour;
            if (menuLevel == 2) serviceVehicle.SpecularColor = serviceSpecularColour;
            if (menuLevel == 3) serviceVehicle.FeatureColor1 = serviceFeatureColour1;
            if (menuLevel == 4) serviceVehicle.FeatureColor2 = serviceFeatureColour2;
            if (menuLevel == 5) RestoreCommittedExtras();
        }

        private void RestoreCommittedExtras()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            foreach (KeyValuePair<int, bool> pair in serviceCommittedExtras)
            {
                if (IsExtraOn(serviceVehicle, pair.Key) != pair.Value) SetExtraState(serviceVehicle, pair.Key, pair.Value);
            }
            pendingPreviewExtra = -1;
        }

        private void RestoreUncommittedExtraPreviews()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            bool restoredAny = false;
            foreach (KeyValuePair<int, bool> pair in serviceCommittedExtras)
            {
                if (IsExtraOn(serviceVehicle, pair.Key) != pair.Value)
                {
                    SetExtraState(serviceVehicle, pair.Key, pair.Value);
                    restoredAny = true;
                }
            }
            if (restoredAny) pendingPreviewExtra = -1;
        }

        private void CommitPreviewColour()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            serviceBaseColour = serviceVehicle.Color;
            serviceSpecularColour = serviceVehicle.SpecularColor;
            serviceFeatureColour1 = serviceVehicle.FeatureColor1;
            serviceFeatureColour2 = serviceVehicle.FeatureColor2;
        }
        private void ApplyPreviewColour()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            if (menuLevel == 1)
            {
                serviceVehicle.Color = new ColorIndex(currentColourID);
            }
            else if (menuLevel == 2)
            {
                serviceVehicle.SpecularColor = new ColorIndex(currentColourID);
            }
            else if (menuLevel == 3)
            {
                serviceVehicle.FeatureColor1 = new ColorIndex(currentColourID);
            }
            else if (menuLevel == 4)
            {
                serviceVehicle.FeatureColor2 = new ColorIndex(currentColourID);
            }
        }

        private bool IsColourMenu()
        {
            return menuLevel >= 1 && menuLevel <= 4;
        }

        private int GetCurrentMenuCount()
        {
            if (menuLevel == 0)
            {
                if (serviceInScriptGarage || scriptGarageActive) return 7;
                return serviceBeforeVanilla ? 9 : 7;
            }
            if (IsColourMenu()) return 1;
            if (menuLevel == 5) return Math.Max(1, availableExtras.Count);
            return 0;
        }

        private void DrawMenu()
        {
            if (!IsValidVehicle(serviceVehicle))
            {
                CloseMenu(false);
                return;
            }

            if (messageTimer > 0) messageTimer--;
            if (scriptGarageTransitioning) return;

            bool workshop = serviceInScriptGarage || scriptGarageActive;
            if (workshop && useNativeWorkshopMenu)
            {
                DrawNativeWorkshopMenu();
                return;
            }

            string text = workshop ? "=== AUTO WORKSHOP CE ===\nWorkshop\n" : (serviceBeforeVanilla ? "=== AUTO WORKSHOP CE ===\nPre-service menu\n" : "=== AUTO WORKSHOP CE ===\nPost-service fallback\n");
            text += "Cash: $" + Player.Money.ToString() + "\n------------------------\n";

            if (menuLevel == 0)
            {
                if (workshop)
                {
                    string[] items = new string[] {
                        "Repair vehicle                 " + GetRepairPriceLabel(serviceVehicle),
                        "Repaint body colour            $" + GetWorkshopPrice(priceRepaintBase).ToString(),
                        "Repaint specular colour        $" + GetWorkshopPrice(priceRepaintSpecular).ToString(),
                        "Repaint feature colour 1       $" + GetWorkshopPrice(priceRepaintFeature1).ToString(),
                        "Repaint feature colour 2       $" + GetWorkshopPrice(priceRepaintFeature2).ToString(),
                        (requireRepairedVehicleForExtras && VehicleNeedsRepair(serviceVehicle)) ? "Extras                         repair first" : "Extras                         add $" + GetWorkshopPrice(priceToggleExtra).ToString(),
                        "Exit"
                    };
                    text += BuildList(items);
                    text += serviceHadPaidWork ? "\n" + GetPaidWorkExitLine() + "\n" + KeyLabel(keySelect) + ": select | " + KeyLabel(keyExit) + ": exit" : "\n" + KeyLabel(keySelect) + ": select | " + KeyLabel(keyExit) + ": exit";
                }
                else if (serviceBeforeVanilla)
                {
                    int customCost = ComputePreServiceExtraCost();
                    int serviceFee = scriptOwnedCustomService ? Math.Max(0, priceVanillaService) : 0;
                    int pendingCost = customCost + serviceFee;
                    string confirmText = scriptOwnedCustomService ? "Confirm service              $" + pendingCost.ToString() : "Confirm custom service     +$" + customCost.ToString();
                    string[] items = new string[] {
                        "Quick respray / wanted clear",
                        "Repair only / keep colours",
                        "Repaint body colour        $" + priceRepaintBase,
                        "Repaint specular colour    $" + priceRepaintSpecular,
                        "Repaint feature colour 1   $" + priceRepaintFeature1,
                        "Repaint feature colour 2   $" + priceRepaintFeature2,
                        "Extras                     $" + priceToggleExtra + " each",
                        confirmText,
                        "Cancel / drive away"
                    };
                    text += BuildList(items);
                    text += scriptOwnedCustomService ? "\nQuick respray uses vanilla random colour.\nCustom service is scripted: repair, wanted clear, time skip.\n" + KeyLabel(keySelect) + ": select | " + KeyLabel(keyExit) + ": cancel" : "\nQuick respray uses vanilla random colour.\n" + KeyLabel(keySelect) + ": select | " + KeyLabel(keyExit) + ": cancel";
                }
                else
                {
                    string[] items = new string[] {
                        "Repair only / keep colours",
                        "Repaint body colour        $" + priceRepaintBase,
                        "Repaint specular colour    $" + priceRepaintSpecular,
                        "Repaint feature colour 1   $" + priceRepaintFeature1,
                        "Repaint feature colour 2   $" + priceRepaintFeature2,
                        "Extras                     $" + priceToggleExtra + " each",
                        "Exit"
                    };
                    text += BuildList(items);
                    text += "\n" + KeyLabel(keySelect) + ": select | " + KeyLabel(keyExit) + ": exit";
                }
            }
            else if (IsColourMenu())
            {
                text += GetColourMenuTitle() + "\n";
                text += "Colour ID: " + currentColourID.ToString() + "\n";
                text += "Price: $" + previewPrice.ToString() + (workshop ? " on " + KeyLabel(keySelect) : (serviceBeforeVanilla ? " on confirm" : "")) + "\n";
                text += workshop ? KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + "/" + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + ": preview | " + KeyLabel(keySelect) + ": apply | " + KeyLabel(keyExit) + ": cancel" : KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + ": change | " + KeyLabel(keySelect) + ": select | " + KeyLabel(keyExit) + ": cancel";
            }
            else if (menuLevel == 5)
            {
                text += "EXTRAS\n";
                if (availableExtras.Count == 0)
                {
                    text += "> No available extras\n";
                    text += KeyLabel(keyExit) + ": back/exit";
                }
                else
                {
                    for (int i = 0; i < availableExtras.Count; i++)
                    {
                        int extra = availableExtras[i];
                        string state = IsExtraOn(serviceVehicle, extra) ? "ON" : "OFF";
                        bool committed = GetCommittedExtraState(extra, IsExtraOn(serviceVehicle, extra));
                        bool changed = IsExtraOn(serviceVehicle, extra) != committed;
                        string costText = "";
                        if (changed) costText = IsExtraOn(serviceVehicle, extra) && !committed ? "  add $" + GetWorkshopPrice(priceToggleExtra).ToString() : "  remove free";
                        else costText = IsExtraOn(serviceVehicle, extra) ? "  installed" : "  not installed";
                        text += (i == selectedIndex ? "> " : "  ") + "Extra " + extra.ToString() + ": " + state + (changed ? " *" : "") + costText + (workshop ? " on " + KeyLabel(keySelect) : (serviceBeforeVanilla ? " on confirm" : "")) + "\n";
                    }
                    text += workshop ? KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + ": choose | " + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + ": preview | " + KeyLabel(keySelect) + ": buy | " + KeyLabel(keyExit) + ": back/exit" : KeyLabel(keySelect) + "/" + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + ": toggle | " + KeyLabel(keyExit) + ": exit";
                }
            }

            if (messageTimer > 0 && messageText.Length > 0)
            {
                text += "\n" + messageText;
            }

            try { Game.DisplayText(text); } catch { }
        }

        private string GetPaidWorkExitLine()
        {
            string line = "Paid work: ";
            if (clearWantedOnScriptedService) line += "wanted clear";
            else line += "wanted unchanged";
            if (scriptedServiceTimeSkipHours > 0) line += " +" + scriptedServiceTimeSkipHours.ToString() + "h on exit";
            else line += " on exit";
            return line;
        }

        private bool IsChasePointWorkshopTriggerActive()
        {
            try
            {
                return activeTrigger != null && activeTrigger.Name != null && activeTrigger.Name.ToLowerInvariant().IndexOf("chase") >= 0;
            }
            catch { return false; }
        }

        private string GetWorkshopMenuBrand()
        {
            if (IsChasePointWorkshopTriggerActive()) return "CHASE POINT";
            return "BRUCIE'S";
        }

        private string GetWorkshopMenuSubtitle()
        {
            if (IsChasePointWorkshopTriggerActive()) return "AUTO WORKSHOP";
            return "EXECUTIVE LIFESTYLE AUTOS";
        }


        private string KeyLabel(Keys key)
        {
            if (key == Keys.Back) return "Backspace";
            if (key == Keys.Enter || key == Keys.Return) return "Enter";
            if (key == Keys.Escape) return "Esc";
            if (key == Keys.Up) return "Up";
            if (key == Keys.Down) return "Down";
            if (key == Keys.Left) return "Left";
            if (key == Keys.Right) return "Right";
            if (key == Keys.Delete) return "Delete";
            if (key == Keys.Space) return "Space";
            return key.ToString();
        }

        private string WorkshopSelectExitHint()
        {
            return KeyLabel(keySelect) + " select   " + KeyLabel(keyExit) + " exit";
        }

        private string WorkshopBackHint(string action)
        {
            return KeyLabel(keyExit) + " " + action;
        }

        private string WorkshopColourHint()
        {
            return KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + "/" + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + " preview   " + KeyLabel(keySelect) + " apply   " + KeyLabel(keyExit) + " back";
        }

        private string WorkshopExtrasHint()
        {
            return KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + " choose   " + KeyLabel(keyLeft) + " off   " + KeyLabel(keyRight) + " on   " + KeyLabel(keySelect) + " apply preview   " + KeyLabel(keyExit) + " back";
        }

        private void DrawNativeWorkshopMenu()
        {
            try
            {
                List<string> lines = new List<string>();
                List<bool> selectable = new List<bool>();
                string brand = GetWorkshopMenuBrand();
                string subtitle = GetWorkshopMenuSubtitle();
                string hint = "";

                if (menuLevel == 0)
                {
                    string[] items = new string[] {
                        "Repair vehicle                 " + GetRepairPriceLabel(serviceVehicle),
                        "Repaint body colour            $" + GetWorkshopPrice(priceRepaintBase).ToString(),
                        "Repaint specular colour        $" + GetWorkshopPrice(priceRepaintSpecular).ToString(),
                        "Repaint feature colour 1       $" + GetWorkshopPrice(priceRepaintFeature1).ToString(),
                        "Repaint feature colour 2       $" + GetWorkshopPrice(priceRepaintFeature2).ToString(),
                        (requireRepairedVehicleForExtras && VehicleNeedsRepair(serviceVehicle)) ? "Extras                         repair first" : "Extras                         add $" + GetWorkshopPrice(priceToggleExtra).ToString(),
                        "Exit"
                    };
                    for (int i = 0; i < items.Length; i++) { lines.Add(items[i]); selectable.Add(true); }
                    lines.Add("Price class: " + currentVehiclePriceCategory + " x" + currentVehiclePriceMultiplier.ToString("0.00")); selectable.Add(false);
                    if (serviceHadPaidWork)
                    {
                        lines.Add(GetPaidWorkExitLine()); selectable.Add(false);
                    }
                    hint = WorkshopSelectExitHint();
                }
                else if (IsColourMenu())
                {
                    subtitle = GetColourMenuTitle();
                    lines.Add("Colour ID: " + currentColourID.ToString()); selectable.Add(false);
                    lines.Add("Price: $" + previewPrice.ToString()); selectable.Add(false);
                    lines.Add(KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + "/" + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + " preview palette"); selectable.Add(false);
                    lines.Add(KeyLabel(keySelect) + " apply   " + KeyLabel(keyExit) + " back"); selectable.Add(false);
                    hint = "Preview free. " + KeyLabel(keySelect) + " applies.";
                }
                else if (menuLevel == 5)
                {
                    subtitle = "EXTRAS";
                    if (availableExtras.Count == 0)
                    {
                        lines.Add("No available extras"); selectable.Add(false);
                        hint = WorkshopBackHint("return");
                    }
                    else
                    {
                        for (int i = 0; i < availableExtras.Count; i++)
                        {
                            int extra = availableExtras[i];
                            bool currentOn = IsExtraOn(serviceVehicle, extra);
                            bool committedOn = GetCommittedExtraState(extra, currentOn);
                            string state = currentOn ? "ON" : "OFF";
                            bool changed = currentOn != committedOn;
                            string marker = changed ? " *" : "";
                            string costText = changed ? (currentOn && !committedOn ? "add $" + GetWorkshopPrice(priceToggleExtra).ToString() : "remove free") : (currentOn ? "installed" : "not installed");
                            lines.Add("Extra " + extra.ToString() + ": " + state + marker + "     " + costText);
                            selectable.Add(true);
                        }
                        hint = WorkshopExtrasHint();
                    }
                }

                float x = 0.055f;
                float y = 0.135f;
                float w = 0.365f;
                float headerH = 0.066f;
                float rowH = 0.034f;
                bool colourMenuActive = IsColourMenu();
                float paletteH = 0.0f;
                if (colourMenuActive && showColourPalette)
                {
                    int colourCount = Math.Max(1, maxColourID - minColourID + 1);
                    int columns = Math.Max(4, colourPaletteColumns);
                    int rows = (colourCount + columns - 1) / columns;
                    paletteH = 0.024f + rows * (Math.Max(0.006f, colourPaletteCellHeight) + Math.Max(0.0f, colourPaletteGap));
                }
                float bodyH = 0.080f + (Math.Max(1, lines.Count) * rowH) + 0.055f + paletteH;
                if (bodyH < 0.260f) bodyH = 0.260f;
                if (bodyH > 0.720f) bodyH = 0.720f;

                DrawScreenRect(x + w * 0.5f, y + headerH * 0.5f, w, headerH, 0, 0, 0, 225);
                DrawScreenRect(x + w * 0.5f, y + headerH + bodyH * 0.5f, w, bodyH, 0, 0, 0, 170);
                DrawScreenRect(x + w * 0.5f, y + headerH + 0.014f, w, 0.003f, 255, 255, 255, 120);

                DrawScreenText(x + 0.018f, y + 0.007f, brand, 0.340f, 255, 255, 255, 255, false);
                DrawScreenText(x + 0.019f, y + 0.040f, subtitle, 0.205f, 210, 210, 210, 230, false);
                DrawScreenText(x + w - 0.105f, y + 0.040f, "$" + Player.Money.ToString(), 0.220f, 210, 210, 210, 230, false);

                float rowY = y + headerH + 0.026f;
                for (int i = 0; i < lines.Count; i++)
                {
                    bool selected = selectable[i] && i == selectedIndex;
                    if (selected)
                    {
                        DrawScreenRect(x + w * 0.5f, rowY + 0.014f, w - 0.020f, rowH * 0.82f, 255, 255, 255, 70);
                    }
                    string prefix = selected ? "> " : "  ";
                    int r = selected ? 255 : 215;
                    int g = selected ? 255 : 215;
                    int b = selected ? 255 : 215;
                    DrawScreenText(x + 0.018f, rowY, prefix + lines[i], 0.225f, r, g, b, 245, false);
                    rowY += rowH;
                }

                if (colourMenuActive && showColourPalette)
                {
                    DrawColourPalette(x + 0.026f, rowY + 0.012f);
                }

                if (hint.Length > 0)
                {
                    DrawScreenText(x + 0.018f, y + headerH + bodyH - 0.045f, hint, 0.190f, 175, 175, 175, 220, false);
                }
                if (messageTimer > 0 && messageText.Length > 0)
                {
                    DrawScreenText(x + 0.018f, y + headerH + bodyH - 0.022f, messageText, 0.190f, 235, 235, 235, 230, false);
                }
            }
            catch (Exception ex)
            {
                useNativeWorkshopMenu = false;
                Log("Native workshop menu disabled after draw failure: " + ex.GetType().Name + ".");
                try { Game.DisplayText("AUTO WORKSHOP CE\nMenu draw fallback active."); } catch { }
            }
        }

        private void DrawScreenRect(float x, float y, float width, float height, int r, int g, int b, int a)
        {
            try { Function.Call("DRAW_RECT", x, y, width, height, r, g, b, a); } catch { }
        }

        private void DrawScreenText(float x, float y, string text, float scale, int r, int g, int b, int a, bool centre)
        {


            DrawScreenTextStyled(x, y, text, scale * 0.74f, scale * 1.10f, r, g, b, a, centre);
        }

        private void DrawScreenTextStyled(float x, float y, string text, float scaleX, float scaleY, int r, int g, int b, int a, bool centre)
        {
            if (text == null) text = "";
            if (text.Length > 120) text = text.Substring(0, 120);
            try { Function.Call("SET_TEXT_FONT", 0); } catch { }
            try { Function.Call("SET_TEXT_PROPORTIONAL", true); } catch { }
            try { Function.Call("SET_TEXT_SCALE", scaleX, scaleY); } catch { }
            try { Function.Call("SET_TEXT_COLOUR", r, g, b, a); } catch { }
            try { Function.Call("SET_TEXT_DROPSHADOW", 1, 0, 0, 0, 190); } catch { }
            try { Function.Call("SET_TEXT_EDGE", 1, 0, 0, 0, 150); } catch { }
            try { Function.Call("SET_TEXT_CENTRE", centre); } catch { }
            try { Function.Call("DISPLAY_TEXT_WITH_LITERAL_STRING", x, y, "STRING", text); } catch { }
        }

        private void DrawColourPalette(float startX, float startY)
        {
            int columns = Math.Max(4, colourPaletteColumns);
            float cellW = Math.Max(0.006f, colourPaletteCellWidth);
            float cellH = Math.Max(0.006f, colourPaletteCellHeight);
            float gap = Math.Max(0.0f, colourPaletteGap);
            int count = Math.Max(1, maxColourID - minColourID + 1);

            DrawScreenText(startX, startY - 0.022f, "COLOUR PALETTE", 0.190f, 175, 175, 175, 220, false);

            for (int offset = 0; offset < count; offset++)
            {
                int id = minColourID + offset;
                int col = offset % columns;
                int row = offset / columns;
                float cx = startX + col * (cellW + gap) + cellW * 0.5f;
                float cy = startY + row * (cellH + gap) + cellH * 0.5f;
                int r, g, b;
                GetApproximateVehicleColour(id, out r, out g, out b);

                if (id == currentColourID)
                {
                    DrawScreenRect(cx, cy, cellW + 0.006f, cellH + 0.006f, 255, 255, 255, 230);
                    DrawScreenRect(cx, cy, cellW + 0.003f, cellH + 0.003f, 0, 0, 0, 210);
                }
                DrawScreenRect(cx, cy, cellW, cellH, r, g, b, 235);
            }
        }

        private void GetApproximateVehicleColour(int id, out int r, out int g, out int b)
        {
            if (vehicleColourPaletteLoaded && id >= 0 && id < vehicleColourR.Length)
            {
                r = vehicleColourR[id];
                g = vehicleColourG[id];
                b = vehicleColourB[id];
                return;
            }


            int shade = 55 + (Math.Abs(id) % 10) * 18;
            r = ClampByte(shade);
            g = ClampByte(shade);
            b = ClampByte(shade);
        }

        private int ClampByte(int value)
        {
            if (value < 0) return 0;
            if (value > 255) return 255;
            return value;
        }

        private string BuildList(string[] items)
        {
            string s = "";
            for (int i = 0; i < items.Length; i++)
            {
                s += (i == selectedIndex ? "> " : "  ") + items[i] + "\n";
            }
            return s;
        }

        private string GetColourMenuTitle()
        {
            if (menuLevel == 1) return "BODY COLOUR";
            if (menuLevel == 2) return "SPECULAR COLOUR";
            if (menuLevel == 3) return "FEATURE COLOUR 1";
            if (menuLevel == 4) return "FEATURE COLOUR 2";
            return "COLOUR";
        }

        private void OnTickScriptGarageMode(Vehicle v, int currentResprayStat, bool interactingWithGarage)
        {
            if (!IsValidVehicle(v)) return;

            bool playerControl = true;
            try { playerControl = Player.CanControlCharacter; } catch { playerControl = true; }

            if (useCoordinateWorkshopTriggers)
            {
                WorkshopTrigger trigger = GetActiveWorkshopTrigger(v);
                if (trigger != null)
                {
                    if (scriptGarageActive || serviceInScriptGarage || scriptGarageTransitioning) return;

                    if (requireNoWantedForWorkshop && SafeGetWantedLevel() > 0)
                    {
                        if (showEntryPrompt) DrawEntryPrompt("Lose the cops before using this workshop.");
                        return;
                    }

                    float speed = SafeVehicleSpeed(v);
                    bool stoppedEnough = !requireStoppedForScriptGarageEntry || speed <= scriptGarageEntryMaxSpeed;

                    if (stoppedEnough)
                    {
                        if (showEntryPrompt) DrawEntryPrompt(GetWorkshopEntryPrompt(trigger));

                        if (IsHornInputPressed(v) && activeTrigger == null && preMenuCooldownTicks <= 0)
                        {


                            if (suspendWorkshopDuringMissions && IsMissionActiveFreshSafe())
                            {
                                if (showEntryPrompt) DrawEntryPrompt("Garage unavailable during missions.");
                                Notify("Garage unavailable during missions.");
                                Log("MISSION_GUARD blocked workshop horn entry: " + trigger.Name + ".");
                                return;
                            }

                            activeTrigger = trigger;
                            CaptureScriptGarageExitTransformFromTrigger(v, trigger);
                            preMenuOpenedForCurrentGarageInteraction = true;
                            EnterScriptGarage(v, currentResprayStat);
                            Log("Coordinate workshop trigger accepted by horn: " + trigger.Name + ", Speed=" + speed.ToString("0.000") + ".");
                            return;
                        }
                    }

                    if (!interactingWithGarage && playerControl)
                    {
                        RememberLastSafeExitTransform(v);
                        TrackVehicle(v);
                        lastResprayStat = currentResprayStat;
                        haveResprayStat = true;
                    }
                    return;
                }
                else
                {
                    if (activeTrigger != null) activeTrigger = null;
                    if (!interactingWithGarage && playerControl)
                    {
                        RememberLastSafeExitTransform(v);
                        scriptGarageEntryTicks = 0;
                        preMenuOpenedForCurrentGarageInteraction = false;
                        suppressPreMenuUntilGarageExit = false;
                        suppressPostServiceFallbackOnce = false;
                        pendingService = false;
                        allowNextVanillaColour = false;
                        pendingExtras.Clear();
                        pendingExtraCharge = 0;
                        ClearVanillaWait();
                        TrackVehicle(v);
                        lastResprayStat = currentResprayStat;
                        haveResprayStat = true;
                    }
                    if (!allowVanillaGarageFallbackInScriptGarage) return;
                }
            }

            if (!interactingWithGarage && playerControl)
            {
                RememberLastSafeExitTransform(v);
                scriptGarageEntryTicks = 0;
                preMenuOpenedForCurrentGarageInteraction = false;
                suppressPreMenuUntilGarageExit = false;
                suppressPostServiceFallbackOnce = false;
                pendingService = false;
                allowNextVanillaColour = false;
                pendingExtras.Clear();
                pendingExtraCharge = 0;
                ClearVanillaWait();
                TrackVehicle(v);
                lastResprayStat = currentResprayStat;
                haveResprayStat = true;
                return;
            }


            if (!haveTrackedColours || trackedVehicle == null || !trackedVehicle.Exists())
            {
                TrackVehicle(v);
                lastResprayStat = currentResprayStat;
                haveResprayStat = true;
            }
        }

        private string GetWorkshopEntryPrompt(WorkshopTrigger trigger)
        {
            if (trigger != null && !trigger.UseSeparateInterior) return "Sound horn to use the auto workshop.";
            return "Sound horn to enter Executive Lifestyle Autos.";
        }

        private WorkshopTrigger GetActiveWorkshopTrigger(Vehicle v)
        {
            if (!IsValidVehicle(v)) return null;
            float x;
            float y;
            float z;
            if (!TryGetVehiclePosition(v, out x, out y, out z)) return null;
            for (int i = 0; i < payNSprayTriggers.Count; i++)
            {
                WorkshopTrigger t = payNSprayTriggers[i];
                if (t == null) continue;
                float dx = x - t.X;
                float dy = y - t.Y;
                float dz = z - t.Z;
                float radius = Math.Max(1.0f, t.Radius <= 0.0f ? payNSprayTriggerRadius : t.Radius);
                float verticalTolerance = Math.Max(3.0f, radius);
                if ((dx * dx + dy * dy) <= (radius * radius) && Math.Abs(dz) <= verticalTolerance)
                {
                    return t;
                }
            }
            return null;
        }

        private void CaptureScriptGarageExitTransformFromTrigger(Vehicle v, WorkshopTrigger trigger)
        {
            if (trigger != null && !trigger.UseSeparateInterior)
            {
                haveScriptGarageExitTransform = false;
                return;
            }
            if (useFixedWorkshopExit)
            {
                scriptGarageExitX = workshopExitX;
                scriptGarageExitY = workshopExitY;
                scriptGarageExitZ = workshopExitZ;
                scriptGarageExitHeading = workshopExitHeading;
                haveScriptGarageExitTransform = true;
                return;
            }
            if (trigger == null)
            {
                CaptureScriptGarageExitTransform(v);
                return;
            }
            float heading = SafeGetVehicleHeading(v, trigger.Heading);
            float backX;
            float backY;
            OffsetFromHeading(heading, -exitBackOffset, out backX, out backY);
            scriptGarageExitX = trigger.X + backX;
            scriptGarageExitY = trigger.Y + backY;
            scriptGarageExitZ = trigger.Z + 0.35f;
            scriptGarageExitHeading = heading;
            haveScriptGarageExitTransform = true;
        }

        private void RememberLastSafeExitTransform(Vehicle v)
        {
            if (!IsValidVehicle(v)) return;
            float x;
            float y;
            float z;
            if (!TryGetVehiclePosition(v, out x, out y, out z)) return;
            lastSafeExitX = x;
            lastSafeExitY = y;
            lastSafeExitZ = z;
            lastSafeExitHeading = SafeGetVehicleHeading(v, lastSafeExitHeading);
            haveLastSafeExitTransform = true;
        }

        private void EnterScriptGarage(Vehicle v, int currentResprayStat)
        {
            if (!IsValidVehicle(v)) return;
            if (scriptGarageTransitioning || scriptGarageActive || serviceInScriptGarage) return;
            if (suspendWorkshopDuringMissions && IsMissionActiveFreshSafe())
            {
                Notify("Garage unavailable during missions.");
                Log("MISSION_GUARD blocked script garage entry at final gate.");
                return;
            }
            scriptGarageTransitioning = true;

            if (!haveScriptGarageExitTransform) CaptureScriptGarageExitTransform(v);
            pendingService = false;
            allowNextVanillaColour = false;
            pendingExtras.Clear();
            pendingExtraCharge = 0;
            ClearVanillaWait();

            bool fadeStarted = false;
            bool useSeparateForThisEntry = useSeparateGarageInterior && HasScriptGarageInteriorCoordinates();
            if (activeTrigger != null && !activeTrigger.UseSeparateInterior) useSeparateForThisEntry = false;
            activeScriptGarageUsesSeparateInterior = useSeparateForThisEntry;

            try
            {
                if (simulateScriptedServiceFade)
                {
                    SafeFadeScreenOut(scriptGarageFadeOutMs, true);
                    fadeStarted = true;
                    SafeWait(scriptGarageFadeHoldMs);
                }

                if (useLoadingScreenForGarageTeleport) SafeForceLoadingScreen(true);

                if (useSeparateForThisEntry)
                {
                    SafeTeleportVehicle(v, scriptGarageX, scriptGarageY, scriptGarageZ, scriptGarageHeading);
                    SettleVehicleOnGround(v, scriptGarageSettleMs);
                }
                else
                {
                    if (activeTrigger != null && !activeTrigger.UseSeparateInterior)
                    {
                        if (alignInPlaceWorkshopVehicle)
                        {
                            AlignVehicleForInPlaceWorkshop(v, activeTrigger);
                        }
                        else
                        {
                            Log("In-place workshop entry. Vehicle remains in current bay: " + activeTrigger.Name + ".");
                        }
                    }
                    else
                    {
                        if (useSeparateGarageInterior) Log("Separate garage interior is enabled, but coordinates are still zero/invalid. Falling back to current bay nudge.");
                        NudgeVehicleIntoCurrentGarage(v);
                        SettleVehicleOnGround(v, Math.Min(scriptGarageSettleMs, 750));
                    }
                }

                if (useLoadingScreenForGarageTeleport) SafeForceLoadingScreen(false);

                scriptGarageActive = true;
                serviceInScriptGarage = true;
                OpenServiceMenu(v, true);
                Log("Script garage opened. Stat=" + currentResprayStat.ToString() + ", SeparateInterior=" + useSeparateForThisEntry.ToString() + ".");
            }
            catch (Exception ex)
            {
                scriptGarageActive = false;
                serviceInScriptGarage = false;
                Player.CanControlCharacter = true;
                SafeForceLoadingScreen(false);
                Notify("Script garage entry failed: " + ex.GetType().Name);
                Log("Script garage entry error: " + ex.ToString());
            }
            finally
            {
                SafeForceLoadingScreen(false);
                if (fadeStarted) SafeFadeScreenIn(scriptGarageFadeInMs, false);
                scriptGarageTransitioning = false;
            }
        }

        private void CaptureScriptGarageExitTransform(Vehicle v)
        {
            if (haveLastSafeExitTransform)
            {
                scriptGarageExitX = lastSafeExitX;
                scriptGarageExitY = lastSafeExitY;
                scriptGarageExitZ = lastSafeExitZ;
                scriptGarageExitHeading = lastSafeExitHeading;
                haveScriptGarageExitTransform = true;
                return;
            }

            float x;
            float y;
            float z;
            if (TryGetVehiclePosition(v, out x, out y, out z))
            {
                float heading = SafeGetVehicleHeading(v, 0.0f);
                float backX;
                float backY;
                OffsetFromHeading(heading, -exitBackOffset, out backX, out backY);
                scriptGarageExitX = x + backX;
                scriptGarageExitY = y + backY;
                scriptGarageExitZ = z + 0.25f;
                scriptGarageExitHeading = heading;
                haveScriptGarageExitTransform = true;
            }
            else
            {
                haveScriptGarageExitTransform = false;
            }
        }

        private bool HasScriptGarageInteriorCoordinates()
        {
            return Math.Abs(scriptGarageX) > 0.01f || Math.Abs(scriptGarageY) > 0.01f || Math.Abs(scriptGarageZ) > 0.01f;
        }

        private void NudgeVehicleIntoCurrentGarage(Vehicle v)
        {
            float x;
            float y;
            float z;
            if (!TryGetVehiclePosition(v, out x, out y, out z)) return;
            float heading = SafeGetVehicleHeading(v, 0.0f);
            float dx;
            float dy;
            OffsetFromHeading(heading, garageEntryNudge, out dx, out dy);
            SafeTeleportVehicle(v, x + dx, y + dy, z + 0.05f, heading);
        }

        private void AlignVehicleForInPlaceWorkshop(Vehicle v, WorkshopTrigger trigger)
        {
            if (!IsValidVehicle(v) || trigger == null) return;
            try
            {


                SafeTeleportVehicle(v, trigger.X, trigger.Y, trigger.Z + 0.05f, trigger.Heading);
                try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
                try { Function.Call("WARP_CHAR_INTO_CAR", Player.Character, v); } catch { }
                Log("In-place workshop vehicle aligned without ground settle. Workshop=" + trigger.Name + ", X=" + trigger.X.ToString("0.000") + ", Y=" + trigger.Y.ToString("0.000") + ", Z=" + trigger.Z.ToString("0.000") + ", H=" + trigger.Heading.ToString("0.000") + ".");
            }
            catch (Exception ex)
            {
                Log("In-place workshop alignment failed: " + ex.GetType().Name + ".");
            }
        }

        private void OffsetFromHeading(float heading, float distance, out float dx, out float dy)
        {
            double radians = heading * Math.PI / 180.0;
            dx = (float)(Math.Sin(radians) * distance);
            dy = (float)(Math.Cos(radians) * distance);
        }

        private void FadeTeleportOutOfScriptGarage(Vehicle v)
        {
            bool fadeStarted = false;
            try
            {
                if (simulateScriptedServiceFade)
                {
                    SafeFadeScreenOut(scriptGarageFadeOutMs, true);
                    fadeStarted = true;
                    SafeWait(scriptGarageFadeHoldMs);
                }
                TeleportOutOfScriptGarage(v);
            }
            finally
            {
                if (fadeStarted) SafeFadeScreenIn(scriptGarageFadeInMs, false);
            }
        }

        private void TeleportOutOfScriptGarage(Vehicle v)
        {
            if (!scriptGarageActive && !serviceInScriptGarage) return;
            if (activeScriptGarageUsesSeparateInterior && IsValidVehicle(v) && haveScriptGarageExitTransform)
            {
                SafeTeleportVehicle(v, scriptGarageExitX, scriptGarageExitY, scriptGarageExitZ, scriptGarageExitHeading);
                SettleVehicleOnGround(v, Math.Min(scriptGarageSettleMs, 750));
            }
            else if (!activeScriptGarageUsesSeparateInterior)
            {
                Log("In-place workshop exit. Vehicle was not teleported out.");
            }
            scriptGarageActive = false;
            serviceInScriptGarage = false;
            haveScriptGarageExitTransform = false;
            activeTrigger = null;
            activeScriptGarageUsesSeparateInterior = true;
            scriptGarageEntryTicks = 0;
            preMenuCooldownTicks = 300;
            suppressPreMenuUntilGarageExit = true;
            suppressPostServiceFallbackOnce = true;
        }

        private void ConfirmScriptedQuickRespray()
        {
            if (!IsValidVehicle(serviceVehicle))
            {
                CloseMenu(false);
                return;
            }

            Vehicle v = serviceVehicle;
            int totalCost = Math.Max(0, priceVanillaService);
            if (totalCost > 0 && Player.Money < totalCost)
            {
                Notify("Not enough money. Need $" + totalCost.ToString() + ".");
                return;
            }

            bool fadeStarted = false;
            try
            {
                if (simulateScriptedServiceFade)
                {
                    SafeFadeScreenOut(scriptGarageFadeOutMs, true);
                    fadeStarted = true;
                    SafeWait(scriptGarageFadeHoldMs);
                }

                if (totalCost > 0)
                {
                    if (!TryCharge(totalCost)) return;
                }

                RepairVehicle(v);
                int colour = GetRandomColourID();
                v.Color = new ColorIndex(colour);
                v.SpecularColor = new ColorIndex(colour);
                serviceBaseColour = v.Color;
                serviceSpecularColour = v.SpecularColor;
                serviceFeatureColour1 = v.FeatureColor1;
                serviceFeatureColour2 = v.FeatureColor2;
                if (clearWantedOnScriptedService) SafeClearWantedLevel();
                SafeSkipServiceTime(scriptedServiceTimeSkipHours);
                TeleportOutOfScriptGarage(v);
                Notify("Quick respray complete. Charged $" + totalCost.ToString() + ".");
                Log("Scripted quick respray complete. Colour=" + colour.ToString() + ", Total=$" + totalCost.ToString() + ".");
            }
            catch (Exception ex)
            {
                Notify("Quick respray failed: " + ex.GetType().Name);
                Log("Scripted quick respray error: " + ex.ToString());
            }
            finally
            {
                if (fadeStarted) SafeFadeScreenIn(scriptGarageFadeInMs, false);
                serviceOpen = false;
                serviceBeforeVanilla = false;
                menuLevel = 0;
                selectedIndex = 0;
                FreezeServiceVehicle(false);
                Player.CanControlCharacter = true;
                if (IsValidVehicle(v)) TrackVehicle(v);
                lastResprayStat = SafeGetResprayStat();
                haveResprayStat = true;
                serviceHadPaidWork = false;
            }
        }

        private int GetRandomColourID()
        {
            int min = Math.Min(minColourID, maxColourID);
            int max = Math.Max(minColourID, maxColourID);
            try { return random.Next(min, max + 1); }
            catch { return min; }
        }

        private void SafeTeleportVehicle(Vehicle v, float x, float y, float z, float heading)
        {
            if (!IsValidVehicle(v)) return;
            try { Function.Call("FREEZE_CAR_POSITION", v, false); } catch { }
            try { Function.Call("REQUEST_COLLISION_AT_POSN", x, y, z); } catch { }
            try { Function.Call("LOAD_SCENE", x, y, z); } catch { }
            try { Function.Call("SET_CAR_COORDINATES", v, x, y, z); } catch (Exception ex) { Log("SET_CAR_COORDINATES failed: " + ex.GetType().Name + "."); }
            try { Function.Call("SET_CAR_HEADING", v, heading); } catch (Exception ex) { Log("SET_CAR_HEADING failed: " + ex.GetType().Name + "."); }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            try { Function.Call("WARP_CHAR_INTO_CAR", Player.Character, v); } catch { }
        }

        private void SettleVehicleOnGround(Vehicle v, int settleMs)
        {
            if (!IsValidVehicle(v)) return;
            try { Function.Call("FREEZE_CAR_POSITION", v, false); } catch { }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            int firstWait = Math.Max(0, settleMs / 2);
            int secondWait = Math.Max(0, settleMs - firstWait);
            SafeWait(firstWait);
            try { Function.Call("SET_CAR_ON_GROUND_PROPERLY", v); } catch (Exception ex) { Log("SET_CAR_ON_GROUND_PROPERLY failed: " + ex.GetType().Name + "."); }

            try { Function.Call("WARP_CHAR_INTO_CAR", Player.Character, v); } catch { }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            SafeWait(secondWait);
            try { Function.Call("SET_CAR_ON_GROUND_PROPERLY", v); } catch { }

            try { Function.Call("WARP_CHAR_INTO_CAR", Player.Character, v); } catch { }
            Log("Vehicle settled on ground. WaitMs=" + settleMs.ToString() + ".");
        }

        private void SafeForceLoadingScreen(bool enabled)
        {
            try { Function.Call("FORCE_LOADING_SCREEN", enabled); } catch { }
        }

        private float SafeVehicleSpeed(Vehicle v)
        {
            if (!IsValidVehicle(v)) return 999.0f;
            try
            {
                object value = v.GetType().GetProperty("Speed").GetValue(v, null);
                if (value != null) return Math.Abs(Convert.ToSingle(value));
            }
            catch { }
            try { return Math.Abs(Function.Call<float>("GET_CAR_SPEED", v)); }
            catch { return 999.0f; }
        }

        private bool IsHornInputPressed(Vehicle v)
        {


            try { return Game.isGameKeyPressed(GameKey.SoundHorn); } catch { return false; }
        }

        private Blip TryCreateCoordinateBlip(Vector3 position, string purpose)
        {

            try
            {
                MethodInfo[] methods = typeof(Blip).GetMethods(BindingFlags.Public | BindingFlags.Static);
                for (int i = 0; i < methods.Length; i++)
                {
                    MethodInfo method = methods[i];
                    if (method == null || method.Name != "AddBlip") continue;

                    ParameterInfo[] parameters = method.GetParameters();
                    object result = null;

                    if (parameters.Length == 1 && parameters[0].ParameterType == typeof(Vector3))
                    {
                        result = method.Invoke(null, new object[] { position });
                    }
                    else if (parameters.Length == 3)
                    {
                        result = method.Invoke(null, new object[] { position.X, position.Y, position.Z });
                    }
                    else
                    {
                        continue;
                    }

                    Blip blip = result as Blip;
                    if (blip != null)
                    {
                        Log("Created " + purpose + " through reflected Blip.AddBlip coordinate path.");
                        return blip;
                    }
                }
            }
            catch (TargetInvocationException ex)
            {
                string detail = ex.InnerException == null ? ex.GetType().Name : ex.InnerException.GetType().Name;
                Log("Coordinate blip AddBlip invocation failed for " + purpose + ": " + detail + ".");
            }
            catch (Exception ex)
            {
                Log("Coordinate blip AddBlip reflection failed for " + purpose + ": " + ex.GetType().Name + ".");
            }


            return null;
        }

        private void ConfigureBlip(Blip blip, int icon, BlipDisplay display, BlipColor colour, float scale, string name, string purpose)
        {
            if (blip == null) return;
            try { blip.Icon = (BlipIcon)icon; } catch (Exception ex) { Log("Could not set " + purpose + " icon: " + ex.GetType().Name + "."); }
            try { blip.Display = display; } catch (Exception ex) { Log("Could not set " + purpose + " display: " + ex.GetType().Name + "."); }
            try { blip.Color = colour; } catch (Exception ex) { Log("Could not set " + purpose + " colour: " + ex.GetType().Name + "."); }
            try { blip.Scale = Math.Max(0.01f, scale); } catch (Exception ex) { Log("Could not set " + purpose + " scale: " + ex.GetType().Name + "."); }
            try { blip.ShowOnlyWhenNear = workshopBlipOnlyWhenNear; } catch { }
            try { blip.Name = name; } catch (Exception ex) { Log("Could not set " + purpose + " name: " + ex.GetType().Name + "."); }

        }


        private void DrawEntryPrompt(string text)
        {


            try
            {
                string line = String.IsNullOrEmpty(text) ? "Sound horn to enter garage." : text;
                float x = 0.050f;
                float y = 0.052f;
                float w = 0.305f;
                float h = 0.045f;

                DrawScreenRect(x + w * 0.5f, y + h * 0.5f, w, h, 0, 0, 0, 178);
                DrawScreenTextStyled(x + 0.009f, y + 0.010f, line, 0.185f, 0.330f, 245, 245, 245, 245, false);
            }
            catch
            {
                try { Game.DisplayText(text); } catch { }
            }
        }

        private float SafeGetVehicleHeading(Vehicle v, float fallback)
        {
            if (!IsValidVehicle(v)) return fallback;
            try
            {
                object value = v.GetType().GetProperty("Heading").GetValue(v, null);
                if (value != null) return Convert.ToSingle(value);
            }
            catch { }
            try { return Function.Call<float>("GET_CAR_HEADING", v); }
            catch { return fallback; }
        }

        private void TrackVehicle(Vehicle v)
        {
            if (!IsValidVehicle(v)) return;
            trackedVehicle = v;
            trackedBaseColour = v.Color;
            trackedSpecularColour = v.SpecularColor;
            trackedFeatureColour1 = v.FeatureColor1;
            trackedFeatureColour2 = v.FeatureColor2;
            trackedHealth = SafeVehicleHealth(v);
            trackedEngineHealth = SafeEngineHealth(v);
            haveTrackedColours = true;
        }

        private bool ColoursDiffer(ColorIndex a, ColorIndex b)
        {
            return a.Index != b.Index;
        }

        private void RestoreColours(Vehicle v, ColorIndex baseColour, ColorIndex specular, ColorIndex feature1, ColorIndex feature2)
        {
            if (!IsValidVehicle(v)) return;
            v.Color = baseColour;
            v.SpecularColor = specular;
            v.FeatureColor1 = feature1;
            v.FeatureColor2 = feature2;
        }

        private void RestoreOriginalServiceState()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            RestoreColours(serviceVehicle, serviceOriginalBaseColour, serviceOriginalSpecularColour, serviceOriginalFeatureColour1, serviceOriginalFeatureColour2);
            foreach (KeyValuePair<int, bool> pair in serviceOriginalExtras)
            {
                SetExtraState(serviceVehicle, pair.Key, pair.Value);
            }
            serviceBaseColour = serviceOriginalBaseColour;
            serviceSpecularColour = serviceOriginalSpecularColour;
            serviceFeatureColour1 = serviceOriginalFeatureColour1;
            serviceFeatureColour2 = serviceOriginalFeatureColour2;
        }

        private void RepairVehicle(Vehicle v)
        {
            if (!IsValidVehicle(v)) return;
            v.Repair();
            v.Health = 1000;
            v.EngineHealth = 1000;
            v.PetrolTankHealth = 1000;
        }

        private bool CanUseExtras(Vehicle v)
        {
            if (!IsValidVehicle(v)) return false;
            if (!requireRepairedVehicleForExtras) return true;
            return !VehicleNeedsRepair(v);
        }

        private void CaptureVehicleMechanicalDamage(Vehicle v, out int health, out int engineHealth, out int petrolTankHealth)
        {
            health = 1000;
            engineHealth = 1000;
            petrolTankHealth = 1000;
            if (!preserveDamageDuringNonRepairWork || !IsValidVehicle(v)) return;
            try { health = v.Health; } catch { }
            try { engineHealth = (int)v.EngineHealth; } catch { }
            try { petrolTankHealth = (int)v.PetrolTankHealth; } catch { }
        }

        private void RestoreVehicleMechanicalDamage(Vehicle v, int health, int engineHealth, int petrolTankHealth)
        {
            if (!preserveDamageDuringNonRepairWork || !IsValidVehicle(v)) return;
            try { v.Health = health; } catch { }
            try { v.EngineHealth = engineHealth; } catch { }
            try { v.PetrolTankHealth = petrolTankHealth; } catch { }
        }

        private void BuildAvailableExtras()
        {
            availableExtras.Clear();
            if (!IsValidVehicle(serviceVehicle)) return;

            for (int i = minExtraID; i <= maxExtraID; i++)
            {
                bool exists = true;
                if (probeAvailableExtras)
                {
                    exists = ProbeExtra(serviceVehicle, i);
                }
                if (exists)
                {
                    availableExtras.Add(i);
                    bool currentOn = IsExtraOn(serviceVehicle, i);
                    if (!serviceOriginalExtras.ContainsKey(i)) serviceOriginalExtras[i] = currentOn;
                    if (!serviceCommittedExtras.ContainsKey(i)) serviceCommittedExtras[i] = currentOn;
                }
            }
        }

        private bool ProbeExtra(Vehicle v, int extra)
        {
            try
            {
                bool originalOn = IsExtraOn(v, extra);
                Function.Call("TURN_OFF_VEHICLE_EXTRA", v, extra, false);
                bool canBeOn = IsExtraOn(v, extra);
                Function.Call("TURN_OFF_VEHICLE_EXTRA", v, extra, !originalOn);
                return originalOn || canBeOn;
            }
            catch
            {
                return false;
            }
        }

        private bool IsExtraOn(Vehicle v, int extra)
        {
            try
            {
                return Function.Call<bool>("IS_VEHICLE_EXTRA_TURNED_ON", v, extra);
            }
            catch
            {
                return false;
            }
        }
        private void SetExtraState(Vehicle v, int extra, bool on)
        {
            if (!IsValidVehicle(v)) return;
            try
            {
                if (IsExtraOn(v, extra) == on) return;
                Function.Call("TURN_OFF_VEHICLE_EXTRA", v, extra, !on);
            }
            catch { }
        }

        private bool GetCommittedExtraState(int extra, bool fallback)
        {
            bool committed;
            if (serviceCommittedExtras.TryGetValue(extra, out committed)) return committed;
            if (serviceOriginalExtras.TryGetValue(extra, out committed)) return committed;
            return fallback;
        }

        private void PreviewSelectedExtra(bool on)
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            if (availableExtras.Count == 0) return;
            if (!CanUseExtras(serviceVehicle))
            {
                RestoreCommittedExtras();
                menuLevel = 0;
                selectedIndex = 0;
                Notify("Repair vehicle before installing extras.");
                Log("Extra preview blocked: vehicle is not fully repaired.");
                return;
            }
            if (selectedIndex < 0 || selectedIndex >= availableExtras.Count) selectedIndex = 0;
            int extra = availableExtras[selectedIndex];

            bool current = IsExtraOn(serviceVehicle, extra);
            if (current != on)
            {
                SetExtraState(serviceVehicle, extra, on);
                pendingPreviewExtra = extra;
            }
            messageText = "Extra " + extra.ToString() + " preview: " + (on ? "ON" : "OFF") + ".";
            messageTimer = 45;
        }

        private void PurchaseSelectedExtraPreview()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            if (availableExtras.Count == 0) return;
            if (!CanUseExtras(serviceVehicle))
            {
                RestoreCommittedExtras();
                menuLevel = 0;
                selectedIndex = 0;
                Notify("Repair vehicle before installing extras.");
                Log("Extra purchase blocked: vehicle is not fully repaired.");
                return;
            }
            if (selectedIndex < 0 || selectedIndex >= availableExtras.Count) selectedIndex = 0;

            int extra = availableExtras[selectedIndex];
            bool currentOn = IsExtraOn(serviceVehicle, extra);
            bool committedOn = GetCommittedExtraState(extra, currentOn);

            if (currentOn == committedOn)
            {
                Notify("No extra change selected.");
                return;
            }

            bool workshop = serviceInScriptGarage || scriptGarageActive;
            int charge = 0;


            if (currentOn && !committedOn)
            {
                charge = GetWorkshopPrice(priceToggleExtra);
            }

            if ((workshop || !serviceBeforeVanilla) && charge > 0)
            {
                if (!TryCharge(charge))
                {
                    SetExtraState(serviceVehicle, extra, committedOn);
                    return;
                }
            }

            serviceCommittedExtras[extra] = currentOn;
            serviceOriginalExtras[extra] = currentOn;
            pendingPreviewExtra = -1;
            if (workshop) serviceHadPaidWork = true;

            if (charge > 0)
            {
                Notify("Extra " + extra.ToString() + " installed. Charged $" + charge.ToString() + ".");
            }
            else
            {
                Notify("Extra " + extra.ToString() + " removed free of charge.");
            }
            Log("Workshop extra applied. Extra=" + extra.ToString() + ", NewState=" + currentOn.ToString() + ", Cost=$" + charge.ToString() + ", Class=" + currentVehiclePriceCategory + " x" + currentVehiclePriceMultiplier.ToString("0.00") + ".");
        }

        private void ToggleSelectedExtra()
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            if (availableExtras.Count == 0) return;
            if (!CanUseExtras(serviceVehicle))
            {
                RestoreCommittedExtras();
                menuLevel = 0;
                selectedIndex = 0;
                Notify("Repair vehicle before installing extras.");
                Log("Extra toggle blocked: vehicle is not fully repaired.");
                return;
            }
            if (selectedIndex < 0 || selectedIndex >= availableExtras.Count) selectedIndex = 0;

            int extra = availableExtras[selectedIndex];
            bool on = IsExtraOn(serviceVehicle, extra);

            bool workshop = serviceInScriptGarage || scriptGarageActive;
            if (workshop || !serviceBeforeVanilla)
            {
                if (!TryCharge(GetWorkshopPrice(priceToggleExtra))) return;
            }

            try
            {
                Function.Call("TURN_OFF_VEHICLE_EXTRA", serviceVehicle, extra, on);
                bool newState = !on;
                serviceCommittedExtras[extra] = newState;
                serviceOriginalExtras[extra] = newState;
                if (workshop) serviceHadPaidWork = true;
                Notify("Extra " + extra.ToString() + (on ? " disabled." : " enabled.") + (workshop ? " Charged $" + GetWorkshopPrice(priceToggleExtra).ToString() + "." : ""));
                Log("Workshop extra toggled. Extra=" + extra.ToString() + ", NewState=" + newState.ToString() + ", Cost=$" + (workshop ? GetWorkshopPrice(priceToggleExtra) : 0).ToString() + ".");
            }
            catch
            {
                Notify("Extra toggle failed.");
            }
        }

        private bool TryCharge(int price)
        {
            if (price <= 0) return true;
            if (Player.Money < price)
            {
                Notify("Not enough money. Need $" + price.ToString() + ".");
                return false;
            }
            Player.Money -= price;
            return true;
        }

        private void FreezeServiceVehicle(bool frozen)
        {
            if (!IsValidVehicle(serviceVehicle)) return;
            try { Function.Call("FREEZE_CAR_POSITION", serviceVehicle, frozen); } catch { }
            try { Function.Call("LOCK_CAR_DOORS", serviceVehicle, frozen ? 4 : 0); } catch { }
        }

        private void BeginVanillaWait(Vehicle v, int currentResprayStat)
        {
            vanillaWaitVehicle = v;
            vanillaWaitVehicleHandle = TryGetVehicleHandle(v);
            vanillaWaitStartResprayStat = currentResprayStat;
            vanillaWaitHavePosition = TryGetVehiclePosition(v, out vanillaWaitStartX, out vanillaWaitStartY, out vanillaWaitStartZ);
            Log("Vanilla wait armed. VehicleHandle=" + vanillaWaitVehicleHandle.ToString() + ", Stat=" + vanillaWaitStartResprayStat.ToString() + ", HasPosition=" + vanillaWaitHavePosition.ToString() + ".");
        }

        private void ClearVanillaWait()
        {
            vanillaWaitVehicle = null;
            vanillaWaitVehicleHandle = 0;
            vanillaWaitStartResprayStat = -1;
            vanillaWaitHavePosition = false;
            vanillaWaitStartX = 0.0f;
            vanillaWaitStartY = 0.0f;
            vanillaWaitStartZ = 0.0f;
        }

        private void GuardVanillaWaitVehicle(Vehicle currentVehicle, int currentResprayStat)
        {
            if (!pendingService && !allowNextVanillaColour) return;
            if (currentResprayStat > vanillaWaitStartResprayStat) return;

            if (!IsValidVehicle(vanillaWaitVehicle))
            {
                CancelPendingService("waited vehicle no longer exists before vanilla service completed");
                return;
            }

            if (IsValidVehicle(currentVehicle) && !IsVanillaWaitVehicle(currentVehicle))
            {
                CancelPendingService("player changed vehicle before vanilla service completed");
                return;
            }

            if (vanillaWaitHavePosition)
            {
                float x;
                float y;
                float z;
                if (TryGetVehiclePosition(vanillaWaitVehicle, out x, out y, out z))
                {
                    float dx = x - vanillaWaitStartX;
                    float dy = y - vanillaWaitStartY;
                    float dz = z - vanillaWaitStartZ;
                    float distSq = dx * dx + dy * dy + dz * dz;
                    float maxDistSq = vanillaWaitCancelDistance * vanillaWaitCancelDistance;
                    if (distSq > maxDistSq)
                    {
                        CancelPendingService("vehicle moved away from the armed workshop before vanilla service completed");
                        return;
                    }
                }
            }
        }

        private bool IsVanillaWaitVehicle(Vehicle v)
        {
            if (!IsValidVehicle(v) || !IsValidVehicle(vanillaWaitVehicle)) return false;
            if (System.Object.ReferenceEquals(v, vanillaWaitVehicle)) return true;
            int h = TryGetVehicleHandle(v);
            if (h != 0 && vanillaWaitVehicleHandle != 0) return h == vanillaWaitVehicleHandle;

            float ax;
            float ay;
            float az;
            float bx;
            float by;
            float bz;
            if (TryGetVehiclePosition(v, out ax, out ay, out az) && TryGetVehiclePosition(vanillaWaitVehicle, out bx, out by, out bz))
            {
                float dx = ax - bx;
                float dy = ay - by;
                float dz = az - bz;
                return (dx * dx + dy * dy + dz * dz) < 4.0f;
            }


            return true;
        }

        private int TryGetVehicleHandle(Vehicle v)
        {
            try
            {
                object value = v.GetType().GetProperty("Handle").GetValue(v, null);
                if (value is int) return (int)value;
            }
            catch { }
            return 0;
        }

        private bool TryGetVehiclePosition(Vehicle v, out float x, out float y, out float z)
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
            try
            {
                object pos = v.GetType().GetProperty("Position").GetValue(v, null);
                if (pos == null) return false;
                Type t = pos.GetType();
                object ox = null;
                object oy = null;
                object oz = null;
                try { ox = t.GetProperty("X").GetValue(pos, null); } catch { }
                try { oy = t.GetProperty("Y").GetValue(pos, null); } catch { }
                try { oz = t.GetProperty("Z").GetValue(pos, null); } catch { }
                if (ox == null) { try { ox = t.GetField("X").GetValue(pos); } catch { } }
                if (oy == null) { try { oy = t.GetField("Y").GetValue(pos); } catch { } }
                if (oz == null) { try { oz = t.GetField("Z").GetValue(pos); } catch { } }
                if (ox == null || oy == null || oz == null) return false;
                x = Convert.ToSingle(ox);
                y = Convert.ToSingle(oy);
                z = Convert.ToSingle(oz);
                return true;
            }
            catch { return false; }
        }

        private void SafeAbortGarageActivity(string reason)
        {
            try
            {
                Garage.AbortAllGarageActivity();
                Log("Garage activity aborted: " + reason + ".");
            }
            catch (Exception ex)
            {
                Log("Garage activity abort failed: " + ex.GetType().Name + ".");
            }
        }


        private int SafeGetWantedLevel()
        {
            try { return Player.WantedLevel; }
            catch { return 0; }
        }

        private void SafeWait(int ms)
        {
            if (ms <= 0) return;
            try { System.Threading.Thread.Sleep(ms); }
            catch { }
        }

        private bool VehicleNeedsRepair(Vehicle v)
        {
            if (!IsValidVehicle(v)) return false;
            try
            {
                if (v.Health < 995) return true;
                if (v.EngineHealth < 995) return true;
                if (v.PetrolTankHealth < 995) return true;
            }
            catch { }
            return false;
        }

        private void SafeClearWantedLevel()
        {
            try
            {
                Player.WantedLevel = 0;
                Log("Wanted level cleared by scripted service.");
            }
            catch (Exception ex)
            {
                Log("Wanted clear failed: " + ex.GetType().Name + ".");
            }
        }

        private void SafeSkipServiceTime(int hours)
        {
            if (!allowScriptedServiceTimeSkip) return;
            if (hours <= 0) return;
            try
            {
                int h = Function.Call<int>("GET_HOURS_OF_DAY");
                int m = Function.Call<int>("GET_MINUTES_OF_DAY");
                if (h < 0 || h > 23) h = 0;
                if (m < 0 || m > 59) m = 0;
                int newHour = (h + hours) % 24;
                Function.Call("SET_TIME_OF_DAY", newHour, m);
                Log("Service time skipped by " + hours.ToString() + " hour(s): " + h.ToString("00") + ":" + m.ToString("00") + " -> " + newHour.ToString("00") + ":" + m.ToString("00") + ".");
            }
            catch (Exception ex)
            {
                Log("Service time skip failed: " + ex.GetType().Name + ".");
            }
        }

        private void SafeFadeScreenOut(int ms, bool waitUntilDone)
        {
            try { Game.FadeScreenOut(ms, waitUntilDone); }
            catch (Exception ex) { Log("Fade out failed: " + ex.GetType().Name + "."); }
        }

        private void SafeFadeScreenIn(int ms, bool waitUntilDone)
        {
            try { Game.FadeScreenIn(ms, waitUntilDone); }
            catch (Exception ex) { Log("Fade in failed: " + ex.GetType().Name + "."); }
        }

        private bool IsValidVehicle(Vehicle v)
        {
            return v != null && v.Exists();
        }

        private int SafeGetResprayStat()
        {
            try { return Function.Call<int>("GET_INT_STAT", 282); }
            catch { return lastResprayStat < 0 ? 0 : lastResprayStat; }
        }

        private bool SafePlayerIsInteractingWithGarage()
        {
            try { return Function.Call<bool>("PLAYER_IS_INTERACTING_WITH_GARAGE"); }
            catch { return false; }
        }

        private float SafeVehicleHealth(Vehicle v)
        {
            try { return (float)v.Health; } catch { return 0.0f; }
        }

        private float SafeEngineHealth(Vehicle v)
        {
            try { return (float)v.EngineHealth; } catch { return 0.0f; }
        }

        private int ClampColourID(int id)
        {
            if (id < minColourID) return minColourID;
            if (id > maxColourID) return maxColourID;
            return id;
        }

        private void Notify(string text)
        {
            messageText = text;
            messageTimer = 250;
            if (showDebugMessages)
            {
                try { Game.DisplayText(text); } catch { }
            }
        }

        private void Debug(string text)
        {
            if (!showDebugMessages) return;
            Notify(text);
            Log("DEBUG: " + text);
        }

        private void Log(string text)
        {
            if (!logToFile) return;
            try
            {
                File.AppendAllText("scripts\\LibertyVehicleServicesCE.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - [Workshop] " + text + Environment.NewLine);
            }
            catch { }
        }


        private string GetRepairPriceLabel(Vehicle v)
        {
            if (!IsValidVehicle(v)) return "$" + GetWorkshopPrice(priceRepair).ToString();
            if (!VehicleNeedsRepair(v)) return "$0";
            return "$" + GetRepairPrice(v).ToString();
        }

        private int GetRepairPrice(Vehicle v)
        {
            int fullPrice = GetWorkshopPrice(priceRepair);
            if (!useDynamicRepairPrice) return fullPrice;
            if (!IsValidVehicle(v)) return fullPrice;
            if (!VehicleNeedsRepair(v)) return 0;

            float severity = GetRepairDamageSeverity(v);
            if (severity < 0.0f) severity = 0.0f;
            if (severity > 1.0f) severity = 1.0f;

            float minFraction = repairMinimumFraction;
            if (minFraction < 0.0f) minFraction = 0.0f;
            if (minFraction > 1.0f) minFraction = 1.0f;

            float fraction = minFraction + ((1.0f - minFraction) * severity);
            int value = (int)Math.Round(fullPrice * fraction);
            if (value < 1 && fullPrice > 0) value = 1;
            return value;
        }

        private float GetRepairDamageSeverity(Vehicle v)
        {
            if (!IsValidVehicle(v)) return 0.0f;
            float body = 0.0f;
            float engine = 0.0f;
            float tank = 0.0f;
            try { body = (1000.0f - (float)v.Health) / 1000.0f; } catch { }
            try { engine = (1000.0f - (float)v.EngineHealth) / 1000.0f; } catch { }
            try { tank = (1000.0f - (float)v.PetrolTankHealth) / 1000.0f; } catch { }

            body = Clamp01(body);
            engine = Clamp01(engine);
            tank = Clamp01(tank);

            float weighted = (body * repairBodyWeight) + (engine * repairEngineWeight) + (tank * repairTankWeight);
            float worst = Math.Max(body, Math.Max(engine, tank));
            float severity = Math.Max(weighted, worst * 0.65f);
            return Clamp01(severity);
        }

        private float Clamp01(float value)
        {
            if (value < 0.0f) return 0.0f;
            if (value > 1.0f) return 1.0f;
            return value;
        }

        private int GetWorkshopPrice(int basePrice)
        {
            int cleanBase = Math.Max(0, basePrice);
            if (!useSwanknessPriceModifiers || cleanBase == 0) return cleanBase;
            float multiplier = currentVehiclePriceMultiplier;
            if (multiplier <= 0.0f) multiplier = swanknessFallbackMultiplier;
            int value = (int)Math.Round(cleanBase * multiplier);
            if (value < 0) value = 0;
            return value;
        }

        private int GetEffectivePriceForVehicle(Vehicle v, int basePrice)
        {
            int cleanBase = Math.Max(0, basePrice);
            if (!useSwanknessPriceModifiers || cleanBase == 0) return cleanBase;
            float multiplier = GetVehiclePriceMultiplier(v);
            int value = (int)Math.Round(cleanBase * multiplier);
            if (value < 0) value = 0;
            return value;
        }

        private void ResolveCurrentVehiclePriceClass(Vehicle v)
        {
            currentVehicleSwankness = GetVehicleSwankness(v);
            if (!useSwanknessPriceModifiers || currentVehicleSwankness < 0)
            {
                currentVehiclePriceMultiplier = useSwanknessPriceModifiers ? swanknessFallbackMultiplier : 1.0f;
                currentVehiclePriceCategory = currentVehicleSwankness < 0 ? "standard" : GetSwanknessCategoryName(currentVehicleSwankness);
            }
            else
            {
                currentVehiclePriceMultiplier = GetMultiplierForSwankness(currentVehicleSwankness);
                currentVehiclePriceCategory = GetSwanknessCategoryName(currentVehicleSwankness);
            }
            Log("Workshop price class: swankness=" + currentVehicleSwankness.ToString() + ", category=" + currentVehiclePriceCategory + ", multiplier=" + currentVehiclePriceMultiplier.ToString("0.00") + ". Effective prices: repair=" + GetRepairPriceLabel(v) + ", body=$" + GetWorkshopPrice(priceRepaintBase).ToString() + ", extra add=$" + GetWorkshopPrice(priceToggleExtra).ToString() + ".");
        }

        private float GetVehiclePriceMultiplier(Vehicle v)
        {
            int swankness = GetVehicleSwankness(v);
            if (swankness < 0) return swanknessFallbackMultiplier;
            return GetMultiplierForSwankness(swankness);
        }

        private int GetVehicleSwankness(Vehicle v)
        {
            if (!IsValidVehicle(v)) return -1;
            int hash = TryGetVehicleModelHash(v);
            if (hash != 0)
            {
                int value;
                if (swanknessOverrideByModelHash.TryGetValue(hash, out value))
                {
                    if (lastLoggedSwanknessHash != hash)
                    {
                        lastLoggedSwanknessHash = hash;
                        Log("Vehicle swankness override resolved: hash=" + hash.ToString() + ", swankness=" + value.ToString() + ", price multiplier=" + GetMultiplierForSwankness(value).ToString("0.00") + ".");
                    }
                    return value;
                }
                if (swanknessByModelHash.TryGetValue(hash, out value))
                {
                    if (lastLoggedSwanknessHash != hash)
                    {
                        lastLoggedSwanknessHash = hash;
                        Log("Vehicle swankness resolved: hash=" + hash.ToString() + ", swankness=" + value.ToString() + ", price multiplier=" + GetMultiplierForSwankness(value).ToString("0.00") + ".");
                    }
                    return value;
                }
            }
            return -1;
        }

        private float GetMultiplierForSwankness(int swankness)
        {
            if (swankness <= 0) return swankness0Multiplier;
            if (swankness == 1) return swankness1Multiplier;
            if (swankness == 2) return swankness2Multiplier;
            if (swankness == 3) return swankness3Multiplier;
            if (swankness == 4) return swankness4Multiplier;
            return swankness5PlusMultiplier;
        }

        private string GetSwanknessCategoryName(int swankness)
        {
            if (swankness < 0) return "fallback";
            if (swankness >= 5) return "5+";
            return swankness.ToString();
        }

        private int TryGetVehicleModelHash(Vehicle v)
        {
            if (!IsValidVehicle(v)) return 0;
            try
            {
                PropertyInfo modelProperty = v.GetType().GetProperty("Model");
                if (modelProperty == null) return 0;
                object modelObj = modelProperty.GetValue(v, null);
                return ExtractVehicleModelHash(modelObj);
            }
            catch { return 0; }
        }

        private int ExtractVehicleModelHash(object modelObj)
        {
            if (modelObj == null) return 0;
            try
            {
                if (modelObj is int) return Convert.ToInt32(modelObj);
                string[] names = new string[] { "Hash", "Value", "ID", "Id", "ModelHash" };
                for (int i = 0; i < names.Length; i++)
                {
                    PropertyInfo p = modelObj.GetType().GetProperty(names[i]);
                    if (p == null) continue;
                    object value = p.GetValue(modelObj, null);
                    if (value != null)
                    {
                        int hash = Convert.ToInt32(value);
                        if (hash != 0) return hash;
                    }
                }
                for (int i = 0; i < names.Length; i++)
                {
                    FieldInfo f = modelObj.GetType().GetField(names[i]);
                    if (f == null) continue;
                    object value = f.GetValue(modelObj);
                    if (value != null)
                    {
                        int hash = Convert.ToInt32(value);
                        if (hash != 0) return hash;
                    }
                }
            }
            catch { }
            return 0;
        }

        private void LoadVehicleSwanknessData()
        {
            try
            {
                swanknessByModelHash.Clear();
                swanknessByModelName.Clear();
                string[] paths = new string[] {
                    "update\\common\\data\\vehicles.ide",
                    "common\\data\\vehicles.ide"
                };
                for (int i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];
                    if (!File.Exists(path)) continue;
                    int loaded = LoadVehicleSwanknessDataFromFile(path);
                    if (loaded > 0)
                    {
                        vehicleSwanknessLoaded = true;
                        Log("Loaded vehicle swankness table from " + path + " (" + loaded.ToString() + " entries). Price multipliers: 0=" + swankness0Multiplier.ToString("0.00") + ", 1=" + swankness1Multiplier.ToString("0.00") + ", 2=" + swankness2Multiplier.ToString("0.00") + ", 3=" + swankness3Multiplier.ToString("0.00") + ", 4=" + swankness4Multiplier.ToString("0.00") + ", 5+=" + swankness5PlusMultiplier.ToString("0.00") + ".");
                        return;
                    }
                }
                Log("Vehicle swankness table not found. Prices use fallback multiplier " + swanknessFallbackMultiplier.ToString("0.00") + ".");
            }
            catch (Exception ex)
            {
                Log("Vehicle swankness load failed: " + ex.GetType().Name + ". Prices use fallback multiplier.");
            }
        }

        private int LoadVehicleSwanknessDataFromFile(string path)
        {
            int count = 0;
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = StripDataComment(lines[i]).Trim();
                if (line.Length == 0) continue;
                string lower = line.ToLowerInvariant();
                if (lower == "cars" || lower == "boats" || lower == "bikes" || lower == "planes" || lower == "heli" || lower == "trains") continue;
                string[] parts = line.Split(',');
                if (parts.Length < 13) continue;
                string modelName = parts[0].Trim().ToLowerInvariant();
                string gameName = parts.Length > 4 ? parts[4].Trim().ToLowerInvariant() : modelName;
                int swankness;
                if (!int.TryParse(parts[12].Trim(), out swankness)) continue;
                RegisterVehicleSwankness(modelName, swankness);
                RegisterVehicleSwankness(gameName, swankness);
                count++;
            }
            return count;
        }

        private void RegisterVehicleSwankness(string name, int swankness)
        {
            if (name == null) return;
            name = name.Trim().ToLowerInvariant();
            if (name.Length == 0) return;
            swanknessByModelName[name] = swankness;
            try
            {
                int hash = Function.Call<int>("GET_HASH_KEY", name);
                if (hash != 0) swanknessByModelHash[hash] = swankness;
            }
            catch { }
        }

        private void RegisterVehicleSwanknessOverride(string name, int swankness)
        {
            if (name == null) return;
            name = name.Trim().ToLowerInvariant();
            if (name.Length == 0) return;
            if (swankness < 0) swankness = 0;
            swanknessOverrideByModelName[name] = swankness;
            try
            {
                int hash = Function.Call<int>("GET_HASH_KEY", name);
                if (hash != 0) swanknessOverrideByModelHash[hash] = swankness;
            }
            catch { }
        }

        private void LoadVehicleColourPaletteFromCarcols()
        {
            try
            {
                string[] paths = new string[] {
                    "update\\common\\data\\carcols.dat",
                    "common\\data\\carcols.dat"
                };
                for (int i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];
                    if (!File.Exists(path)) continue;
                    int loaded = LoadVehicleColourPaletteFromFile(path);
                    if (loaded > 0)
                    {
                        vehicleColourPaletteLoaded = true;
                        Log("Loaded vehicle colour palette from " + path + " (" + loaded.ToString() + " colours). Palette swatches now use carcols.dat RGB values.");
                        return;
                    }
                }
                Log("carcols.dat was not found or could not be parsed. Colour palette uses neutral ID cells; vehicle preview remains authoritative.");
            }
            catch (Exception ex)
            {
                Log("Vehicle colour palette load failed: " + ex.GetType().Name + ". Colour palette uses neutral ID cells.");
            }
        }

        private int LoadVehicleColourPaletteFromFile(string path)
        {
            int count = 0;
            string[] lines = File.ReadAllLines(path);
            int sequentialId = 0;
            bool sawCarcolsHeader = false;
            bool inCarcolsSection = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = StripDataComment(lines[i]).Trim();
                if (line.Length == 0) continue;

                string lower = line.ToLowerInvariant();
                if (lower == "carcols" || lower.StartsWith("carcols ") || lower.StartsWith("carcols,"))
                {
                    sawCarcolsHeader = true;
                    inCarcolsSection = true;
                    continue;
                }

                if (sawCarcolsHeader)
                {
                    if (lower == "end")
                    {
                        if (inCarcolsSection) break;
                        continue;
                    }
                    if (!inCarcolsSection) continue;
                }
                else
                {


                    if (lower == "end") break;
                    if (lower == "car4" || lower == "car3" || lower == "car2" || lower.StartsWith("car") && lower.Length <= 6) continue;
                }

                string[] raw = line.Replace("\t", " ").Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (raw.Length < 3) continue;

                int[] nums = new int[Math.Min(raw.Length, 5)];
                int numeric = 0;
                for (int n = 0; n < raw.Length && numeric < nums.Length; n++)
                {
                    int value;
                    if (int.TryParse(raw[n], out value)) nums[numeric++] = value;
                }
                if (numeric < 3) continue;

                int id;
                int r;
                int g;
                int b;


                if (numeric >= 4 && nums[0] >= 0 && nums[0] < 256 && nums[1] >= 0 && nums[1] <= 255 && nums[2] >= 0 && nums[2] <= 255 && nums[3] >= 0 && nums[3] <= 255)
                {
                    id = nums[0];
                    r = nums[1];
                    g = nums[2];
                    b = nums[3];
                }
                else
                {
                    id = sequentialId;
                    r = nums[0];
                    g = nums[1];
                    b = nums[2];
                }

                if (id < 0 || id >= 256) continue;
                vehicleColourR[id] = ClampByte(r);
                vehicleColourG[id] = ClampByte(g);
                vehicleColourB[id] = ClampByte(b);
                sequentialId = Math.Max(sequentialId + 1, id + 1);
                count++;
            }
            return count;
        }

        private string StripDataComment(string line)
        {
            if (line == null) return "";
            int hash = line.IndexOf('#');
            if (hash >= 0) line = line.Substring(0, hash);
            int semicolon = line.IndexOf(';');
            if (semicolon >= 0) line = line.Substring(0, semicolon);
            int slash = line.IndexOf("//");
            if (slash >= 0) line = line.Substring(0, slash);
            return line;
        }


        private string DetectWorkshopEpisodeKey()
        {
            try
            {
                int episode = Function.Call<int>("GET_CURRENT_EPISODE");
                return WorkshopEpisodeKeyFromIndex(episode);
            }
            catch { return "iv"; }
        }

        private string GetWorkshopEpisodeKey()
        {
            if (workshopEpisodeKey == null || workshopEpisodeKey.Length == 0)
            {
                workshopEpisodeKey = DetectWorkshopEpisodeKey();
            }
            if (workshopEpisodeKey == null || workshopEpisodeKey.Length == 0) return "iv";
            return workshopEpisodeKey;
        }

        private string WorkshopEpisodeKeyFromIndex(int episode)
        {
            if (episode == 0) return "iv";
            if (episode == 1) return "tlad";
            if (episode == 2) return "tbogt";
            return "episode_" + episode.ToString(CultureInfo.InvariantCulture);
        }

        private void RefreshWorkshopStoryProgress(bool force)
        {
            if (!force && workshopStoryProgressRefreshTicks > 0)
            {
                workshopStoryProgressRefreshTicks--;
                return;
            }
            workshopStoryProgressRefreshTicks = 300;
            float progress;
            if (TryReadWorkshopStoryProgress(out progress))
            {
                workshopStoryProgressPercent = progress;
                workshopStoryProgressReliable = true;
            }
            else
            {
                workshopStoryProgressReliable = false;
                if (!workshopStoryProgressUnavailableLogged)
                {
                    workshopStoryProgressUnavailableLogged = true;
                    Log("Workshop story-progress stat unavailable; workshop blips fall back to always available.");
                }
            }
        }

        private bool TryReadWorkshopStoryProgress(out float progress)
        {
            progress = 0.0f;
            int statId = GetWorkshopStoryProgressStatId();
            if (statId < 0) return false;
            try
            {
                progress = Function.Call<float>("GET_FLOAT_STAT", statId);
                if (progress < 0.0f) progress = 0.0f;
                if (progress > 100.0f) progress = 100.0f;
                return true;
            }
            catch { return false; }
        }

        private int GetWorkshopStoryProgressStatId()
        {
            string ep = GetWorkshopEpisodeKey();
            if (ep == "tlad") return 133;
            if (ep == "tbogt") return 187;
            return 0;
        }

        private bool IsWorkshopTriggerUnlocked(WorkshopTrigger trigger)
        {
            if (trigger == null) return false;
            string episode = GetWorkshopEpisodeKey();
            if (episode == "tlad" || episode == "tbogt") return true;
            if (!workshopStoryProgressReliable) return true;
            string area = GetWorkshopUnlockArea(trigger);
            if (area == "broker_dukes") return workshopStoryProgressPercent >= WorkshopIvBrokerDukesUnlockProgress;
            if (area == "bohan") return workshopStoryProgressPercent >= WorkshopIvBohanUnlockProgress;
            if (area == "algonquin") return workshopStoryProgressPercent >= WorkshopIvAlgonquinUnlockProgress;
            if (area == "alderney") return workshopStoryProgressPercent >= WorkshopIvAlderneyUnlockProgress;
            return true;
        }

        private string GetWorkshopUnlockArea(WorkshopTrigger trigger)
        {
            if (trigger == null) return "broker_dukes";
            string name = trigger.Name == null ? "" : trigger.Name.ToLowerInvariant();
            if (name.IndexOf("chase point") >= 0) return "bohan";
            if (trigger.X < -650.0f) return "alderney";
            return "broker_dukes";
        }

        private bool ShouldShowWorkshopBlip(WorkshopTrigger trigger, int index)
        {
            if (!enableWorkshopBlip) return false;
            if (trigger == null) return false;
            return IsWorkshopTriggerUnlocked(trigger);
        }

        private void UpdateWorkshopBlip()
        {
            if (!enableWorkshopBlip) return;
            if (payNSprayTriggers.Count == 0) return;

            for (int i = 0; i < payNSprayTriggers.Count; i++)
            {
                WorkshopTrigger trigger = payNSprayTriggers[i];
                if (trigger == null) continue;
                if (!ShouldShowWorkshopBlip(trigger, i)) continue;
                UpdateWorkshopBlipForTrigger(trigger, i);
            }
        }

        private void UpdateWorkshopBlipForTrigger(WorkshopTrigger trigger, int index)
        {
            string key = GetWorkshopBlipKey(trigger, index);
            Blip existing;
            if (workshopBlips.TryGetValue(key, out existing) && existing != null) return;

            int retry;
            if (workshopBlipRetryTicksByName.TryGetValue(key, out retry) && retry > 0)
            {
                workshopBlipRetryTicksByName[key] = retry - 1;
                return;
            }

            string name = GetWorkshopBlipName(trigger, index);
            try
            {
                Blip created = TryCreateCoordinateBlip(new Vector3(trigger.X, trigger.Y, trigger.Z), "workshop map blip " + name);
                if (created != null)
                {
                    ConfigureBlip(created, workshopBlipSprite, BlipDisplay.MapOnly, BlipColor.White, Math.Max(0.2f, workshopBlipScale), name, "workshop map blip");
                    workshopBlips[key] = created;
                    if (showDebugMessages) Log("Workshop map blip created. Trigger=" + trigger.Name + ", Icon=" + workshopBlipSprite.ToString() + ", Name=" + name + ".");
                }
                else
                {
                    workshopBlipRetryTicksByName[key] = 120;
                    Log("Workshop map coordinate blip was not created. Trigger=" + trigger.Name + ".");
                }
            }
            catch (Exception ex)
            {
                workshopBlipRetryTicksByName[key] = 120;
                Log("Workshop map coordinate blip creation failed for " + trigger.Name + ": " + ex.GetType().Name + ".");
            }
        }

        private string GetWorkshopBlipKey(WorkshopTrigger trigger, int index)
        {
            if (trigger == null || String.IsNullOrEmpty(trigger.Name)) return "workshop_" + index.ToString();
            return "workshop_" + index.ToString() + "_" + trigger.Name;
        }

        private int GetWorkshopTriggerIndex(WorkshopTrigger trigger)
        {
            if (trigger == null) return -1;
            for (int i = 0; i < payNSprayTriggers.Count; i++)
            {
                if (System.Object.ReferenceEquals(payNSprayTriggers[i], trigger)) return i;
            }
            return -1;
        }

        private string GetWorkshopDiscoveryKey(WorkshopTrigger trigger, int index)
        {
            if (index < 0) index = GetWorkshopTriggerIndex(trigger);
            string name = trigger == null ? "" : trigger.Name;
            string clean = SanitiseDiscoveryKey(name);
            if (clean.Length == 0) clean = "workshop";
            return "workshop_" + Math.Max(0, index).ToString() + "_" + clean;
        }

        private string SanitiseDiscoveryKey(string text)
        {
            if (text == null) return "";
            text = text.Trim().ToLowerInvariant();
            List<char> chars = new List<char>();
            bool lastUnderscore = false;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                bool ok = (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9');
                if (ok)
                {
                    chars.Add(c);
                    lastUnderscore = false;
                }
                else if (!lastUnderscore && chars.Count > 0)
                {
                    chars.Add('_');
                    lastUnderscore = true;
                }
            }
            while (chars.Count > 0 && chars[chars.Count - 1] == '_') chars.RemoveAt(chars.Count - 1);
            return new string(chars.ToArray());
        }

        private string GetWorkshopBlipName(WorkshopTrigger trigger, int index)
        {
            if (index == 0 && !String.IsNullOrEmpty(workshopBlipName)) return workshopBlipName;
            if (trigger != null && !String.IsNullOrEmpty(trigger.Name)) return trigger.Name;
            return "Auto workshop";
        }

        private Vector3 GetChasePointMechanicSpawnPosition()
        {
            if (chasePointMechanicAnchorToRepairCar)
            {
                return GetOffsetPosition(chasePointRepairCarX, chasePointRepairCarY, chasePointRepairCarZ, chasePointRepairCarHeading,
                    chasePointMechanicRepairCarOffsetX,
                    chasePointMechanicRepairCarOffsetY,
                    chasePointMechanicRepairCarOffsetZ);
            }
            return new Vector3(chasePointMechanicX, chasePointMechanicY, chasePointMechanicZ + chasePointMechanicZOffset);
        }

        private float GetChasePointMechanicHeading()
        {
            if (chasePointMechanicAnchorToRepairCar && chasePointMechanicFaceRepairCar)
            {
                Vector3 pos = GetChasePointMechanicSpawnPosition();
                return GetHeadingFromTo(pos.X, pos.Y, chasePointRepairCarX, chasePointRepairCarY);
            }
            return chasePointMechanicHeading;
        }

        private Vector3 GetOffsetPosition(float baseX, float baseY, float baseZ, float heading, float localX, float localY, float localZ)
        {
            double r = heading * Math.PI / 180.0;

            float forwardX = (float)Math.Sin(r);
            float forwardY = (float)Math.Cos(r);
            float rightX = (float)Math.Cos(r);
            float rightY = -(float)Math.Sin(r);
            return new Vector3(
                baseX + rightX * localX + forwardX * localY,
                baseY + rightY * localX + forwardY * localY,
                baseZ + localZ);
        }

        private float GetHeadingFromTo(float fromX, float fromY, float toX, float toY)
        {
            double dx = toX - fromX;
            double dy = toY - fromY;
            double angle = Math.Atan2(dx, dy) * 180.0 / Math.PI;
            if (angle < 0.0) angle += 360.0;
            return (float)angle;
        }

        private void UpdateChasePointRepairScene()
        {
            if (!enableChasePointRepairScene || !enableChasePointWorkshop) return;
            if (serviceOpen || scriptGarageTransitioning) return;
            try { if (Function.Call<bool>("IS_PAUSE_MENU_ACTIVE")) return; } catch { }

            if (IsValidVehicle(chasePointRepairCar))
            {
                UpdateRepairCarBonnet();
                return;
            }
            if (chasePointRepairCarRetryTicks > 0) { chasePointRepairCarRetryTicks--; return; }

            try
            {
                if (Player.Character == null || !Player.Character.Exists()) return;
                Vector3 p = Player.Character.Position;
                float dx = p.X - chasePointRepairCarX;
                float dy = p.Y - chasePointRepairCarY;
                float dz = p.Z - chasePointRepairCarZ;
                float max = Math.Max(20.0f, chasePointRepairSceneSpawnDistance);
                if ((dx * dx + dy * dy) > (max * max) || Math.Abs(dz) > 35.0f) return;
            }
            catch { return; }

            string repairModel = GetRepairCarModelCandidate();
            if (!PrepareRepairCarModel(repairModel)) return;

            chasePointRepairCar = TryCreateVehicleByName(repairModel, new Vector3(chasePointRepairCarX, chasePointRepairCarY, chasePointRepairCarZ), chasePointRepairCarHeading);
            if (IsValidVehicle(chasePointRepairCar))
            {
                activeChasePointRepairCarModel = repairModel;
                ConfigureRepairSceneCar(chasePointRepairCar);
                Log("Chase Point decorative repair car created. Model=" + repairModel + ".");
                return;
            }

            Log("Chase Point decorative repair car creation failed after model load. Model=" + repairModel + ".");
            chasePointRepairCarRetryTicks = 900;
            chasePointRepairCarRequestedHash = 0;
            chasePointRepairCarModelRequested = false;
            chasePointRepairCarRequestTicks = 0;
        }

        private string GetRepairCarModelCandidate()
        {
            string configured = chasePointRepairCarModels;
            if (configured == null || configured.Trim().Length == 0) configured = chasePointRepairCarModel;
            string[] models = SplitCsv(configured);
            if (models.Length == 0) return chasePointRepairCarModel;
            if (!randomiseChasePointRepairCarModel) return models[0];
            try
            {
                int seed = Environment.TickCount ^ (int)(chasePointRepairCarX * 10.0f) ^ (int)(chasePointRepairCarY * 10.0f);
                Random rng = new Random(seed);
                return models[Math.Abs(rng.Next()) % models.Length];
            }
            catch { return models[0]; }
        }

        private bool PrepareRepairCarModel(string modelName)
        {
            if (modelName == null || modelName.Trim().Length == 0)
            {
                chasePointRepairCarRetryTicks = 900;
                return false;
            }

            int hash = 0;
            try { hash = Function.Call<int>("GET_HASH_KEY", modelName.Trim()); } catch { hash = 0; }
            if (hash == 0)
            {
                Log("Chase Point repair car model has zero hash: " + modelName + ".");
                chasePointRepairCarRetryTicks = 900;
                return false;
            }

            bool loaded = false;
            try { loaded = Function.Call<bool>("HAS_MODEL_LOADED", hash); } catch { loaded = false; }
            if (loaded) return true;

            if (!chasePointRepairCarModelRequested || chasePointRepairCarRequestedHash != hash)
            {
                chasePointRepairCarModelRequested = true;
                chasePointRepairCarRequestedHash = hash;
                chasePointRepairCarRequestTicks = 0;
                try { Function.Call("REQUEST_MODEL", hash); } catch (Exception ex) { Log("REQUEST_MODEL failed for repair car " + modelName + ": " + ex.GetType().Name + "."); }
                Log("Requested Chase Point repair car model: " + modelName + ".");
                return false;
            }

            chasePointRepairCarRequestTicks++;
            if (chasePointRepairCarRequestTicks > 300)
            {
                Log("Chase Point repair car model load timed out: " + modelName + ".");
                chasePointRepairCarRetryTicks = 900;
                chasePointRepairCarRequestedHash = 0;
                chasePointRepairCarModelRequested = false;
                chasePointRepairCarRequestTicks = 0;
            }
            return false;
        }

        private void UpdateChasePointMechanicNpc()
        {
            if (!enableChasePointMechanicNpc || !enableChasePointWorkshop) return;
            if (serviceOpen || scriptGarageTransitioning) return;
            try { if (Function.Call<bool>("IS_PAUSE_MENU_ACTIVE")) return; } catch { }

            if (IsValidPed(chasePointMechanicPed))
            {
                UpdateChasePointMechanicWorkCycle();
                return;
            }
            if (chasePointMechanicRetryTicks > 0) { chasePointMechanicRetryTicks--; return; }

            try
            {
                if (Player.Character == null || !Player.Character.Exists()) return;
                Vector3 p = Player.Character.Position;
                float dx = p.X - chasePointMechanicX;
                float dy = p.Y - chasePointMechanicY;
                float dz = p.Z - chasePointMechanicZ;
                float max = Math.Max(20.0f, chasePointMechanicSpawnDistance);
                if ((dx * dx + dy * dy) > (max * max) || Math.Abs(dz) > 35.0f) return;
            }
            catch { return; }

            string modelName = GetMechanicModelCandidate();
            if (String.IsNullOrEmpty(modelName))
            {
                chasePointMechanicRetryTicks = 900;
                Log("Chase Point mechanic NPC creation skipped: no model candidates configured.");
                return;
            }

            if (!PrepareMechanicModel(modelName)) return;

            Vector3 mechanicSpawn = GetChasePointMechanicSpawnPosition();
            chasePointMechanicPed = TryCreatePedByName(modelName, mechanicSpawn, GetChasePointMechanicHeading());
            if (IsValidPed(chasePointMechanicPed))
            {
                ConfigureMechanicPed(chasePointMechanicPed);
                Log("Chase Point mechanic NPC created. Model=" + modelName + ".");
                return;
            }

            Log("Chase Point mechanic NPC creation failed after model load. Model=" + modelName + ".");
            AdvanceMechanicModelCandidate();
            chasePointMechanicRetryTicks = 300;
        }

        private bool PrepareMechanicModel(string modelName)
        {
            int hash = 0;
            try { hash = Function.Call<int>("GET_HASH_KEY", modelName); } catch { hash = 0; }
            if (hash == 0)
            {
                Log("Chase Point mechanic model has zero hash: " + modelName + ".");
                AdvanceMechanicModelCandidate();
                chasePointMechanicRetryTicks = 300;
                return false;
            }

            bool loaded = false;
            try { loaded = Function.Call<bool>("HAS_MODEL_LOADED", hash); } catch { loaded = false; }
            if (loaded) return true;

            if (!chasePointMechanicModelRequested || chasePointMechanicRequestedHash != hash)
            {
                chasePointMechanicModelRequested = true;
                chasePointMechanicRequestedHash = hash;
                chasePointMechanicRequestTicks = 0;
                try { Function.Call("REQUEST_MODEL", hash); } catch (Exception ex) { Log("REQUEST_MODEL failed for mechanic " + modelName + ": " + ex.GetType().Name + "."); }
                Log("Requested Chase Point mechanic model: " + modelName + ".");
                return false;
            }

            chasePointMechanicRequestTicks++;
            if (chasePointMechanicRequestTicks > 300)
            {
                Log("Chase Point mechanic model load timed out: " + modelName + ". Trying next candidate.");
                AdvanceMechanicModelCandidate();
                chasePointMechanicRetryTicks = 120;
            }
            return false;
        }

        private string GetMechanicModelCandidate()
        {
            string[] models = SplitCsv(chasePointMechanicModels);
            if (models.Length == 0) return "";
            if (chasePointMechanicModelIndex < 0) chasePointMechanicModelIndex = 0;
            if (chasePointMechanicModelIndex >= models.Length) chasePointMechanicModelIndex = 0;
            return models[chasePointMechanicModelIndex];
        }

        private void AdvanceMechanicModelCandidate()
        {
            string[] models = SplitCsv(chasePointMechanicModels);
            if (models.Length == 0)
            {
                chasePointMechanicModelIndex = 0;
            }
            else
            {
                chasePointMechanicModelIndex++;
                if (chasePointMechanicModelIndex >= models.Length) chasePointMechanicModelIndex = 0;
            }
            chasePointMechanicRequestedHash = 0;
            chasePointMechanicModelRequested = false;
            chasePointMechanicRequestTicks = 0;
        }

        private string[] SplitCsv(string value)
        {
            if (value == null) return new string[0];
            string[] raw = value.Split(new char[] { ',' });
            List<string> cleaned = new List<string>();
            for (int i = 0; i < raw.Length; i++)
            {
                string item = raw[i] == null ? "" : raw[i].Trim();
                if (item.Length > 0) cleaned.Add(item);
            }
            return cleaned.ToArray();
        }

        private bool IsValidPed(Ped ped)
        {
            if (ped == null) return false;
            try { return ped.Exists(); } catch { return false; }
        }

        private Ped TryCreatePedByName(string modelName, Vector3 position, float heading)
        {
            if (modelName == null || modelName.Trim().Length == 0) return null;
            modelName = modelName.Trim();

            try
            {
                MethodInfo[] methods = typeof(World).GetMethods(BindingFlags.Public | BindingFlags.Static);
                for (int i = 0; i < methods.Length; i++)
                {
                    MethodInfo method = methods[i];
                    if (method.Name != "CreatePed") continue;
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length < 2) continue;

                    object first = null;
                    if (parameters[0].ParameterType == typeof(string)) first = modelName;
                    else if (parameters[0].ParameterType.Name == "Model")
                    {
                        try { first = Activator.CreateInstance(parameters[0].ParameterType, new object[] { modelName }); }
                        catch
                        {
                            try
                            {
                                int hash = Function.Call<int>("GET_HASH_KEY", modelName);
                                first = Activator.CreateInstance(parameters[0].ParameterType, new object[] { hash });
                            }
                            catch { first = null; }
                        }
                    }
                    else if (parameters[0].ParameterType == typeof(int))
                    {
                        try { first = Function.Call<int>("GET_HASH_KEY", modelName); } catch { first = 0; }
                    }
                    if (first == null) continue;

                    object[] args = null;
                    if (parameters.Length == 2 && parameters[1].ParameterType == typeof(Vector3)) args = new object[] { first, position };
                    else if (parameters.Length == 4 && parameters[1].ParameterType == typeof(float)) args = new object[] { first, position.X, position.Y, position.Z };
                    else continue;

                    object result = method.Invoke(null, args);
                    Ped ped = result as Ped;
                    if (IsValidPed(ped))
                    {
                        try { Function.Call("SET_CHAR_COORDINATES", ped, position.X, position.Y, position.Z); } catch { }
                        try { Function.Call("SET_CHAR_HEADING", ped, heading); } catch { }
                        return ped;
                    }
                }
            }
            catch (Exception ex)
            {
                Log("World.CreatePed reflection failed for " + modelName + ": " + ex.GetType().Name + ".");
            }
            return null;
        }

        private Vehicle TryCreateVehicleByName(string modelName, Vector3 position, float heading)
        {
            if (modelName == null || modelName.Trim().Length == 0) return null;
            modelName = modelName.Trim().ToLowerInvariant();

            try
            {
                MethodInfo[] methods = typeof(World).GetMethods(BindingFlags.Public | BindingFlags.Static);
                for (int i = 0; i < methods.Length; i++)
                {
                    MethodInfo method = methods[i];
                    if (method.Name != "CreateVehicle") continue;
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length < 2) continue;

                    object first = null;
                    if (parameters[0].ParameterType == typeof(string)) first = modelName;
                    else if (parameters[0].ParameterType.Name == "Model")
                    {
                        try { first = Activator.CreateInstance(parameters[0].ParameterType, new object[] { modelName }); }
                        catch
                        {
                            try
                            {
                                int hash = Function.Call<int>("GET_HASH_KEY", modelName);
                                first = Activator.CreateInstance(parameters[0].ParameterType, new object[] { hash });
                            }
                            catch { first = null; }
                        }
                    }
                    else if (parameters[0].ParameterType == typeof(int))
                    {
                        try { first = Function.Call<int>("GET_HASH_KEY", modelName); } catch { first = 0; }
                    }
                    if (first == null) continue;

                    object[] args = null;
                    if (parameters.Length == 2 && parameters[1].ParameterType == typeof(Vector3)) args = new object[] { first, position };
                    else if (parameters.Length == 4 && parameters[1].ParameterType == typeof(float)) args = new object[] { first, position.X, position.Y, position.Z };
                    else continue;

                    object result = method.Invoke(null, args);
                    Vehicle v = result as Vehicle;
                    if (IsValidVehicle(v))
                    {
                        try { Function.Call("SET_CAR_COORDINATES", v, position.X, position.Y, position.Z); } catch { }
                        try { Function.Call("SET_CAR_HEADING", v, heading); } catch { }
                        return v;
                    }
                }
            }
            catch (Exception ex)
            {
                Log("World.CreateVehicle reflection failed for repair scene " + modelName + ": " + ex.GetType().Name + ".");
            }
            return null;
        }

        private void UpdateRepairCarBonnet()
        {
            if (!chasePointRepairCarOpenBonnet) return;
            if (!IsValidVehicle(chasePointRepairCar)) return;
            if (chasePointRepairCarBonnetRetryTicks <= 0) return;
            chasePointRepairCarBonnetRetryTicks--;


            TryOpenRepairCarDoor(chasePointRepairCarBonnetDoorIndex);
        }

        private void TryOpenRepairCarDoor(int doorIndex)
        {
            if (!IsValidVehicle(chasePointRepairCar)) return;


            if (chasePointRepairCarUseControlDoor)
            {
                try { Function.Call("CONTROL_CAR_DOOR", chasePointRepairCar, doorIndex, 1, chasePointRepairCarBonnetOpenAngle); } catch { }
                try { Function.Call("CONTROL_CAR_DOOR", chasePointRepairCar, (uint)doorIndex, (uint)1, chasePointRepairCarBonnetOpenAngle); } catch { }
            }
            try { Function.Call("SET_CAR_DOOR_OPEN", chasePointRepairCar, doorIndex, false, false); } catch { }
            try { Function.Call("SET_CAR_DOOR_OPEN", chasePointRepairCar, doorIndex); } catch { }
            try { Function.Call("SET_VEHICLE_DOOR_OPEN", chasePointRepairCar, doorIndex, false, false); } catch { }
            if (chasePointRepairCarUseAggressiveOpenDoor)
            {
                try { Function.Call("OPEN_CAR_DOOR", chasePointRepairCar, doorIndex); } catch { }
            }
        }

        private void ConfigureRepairSceneCar(Vehicle v)
        {
            if (!IsValidVehicle(v)) return;
            try { Function.Call("SET_CAR_AS_MISSION_CAR", v); } catch { }
            try { Function.Call("SET_CAR_COORDINATES", v, chasePointRepairCarX, chasePointRepairCarY, chasePointRepairCarZ); } catch { }
            try { Function.Call("SET_CAR_HEADING", v, chasePointRepairCarHeading); } catch { }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            try { Function.Call("SET_CAR_ON_GROUND_PROPERLY", v); } catch { }
            try { v.Health = chasePointRepairCarHealth; } catch { }
            try { v.EngineHealth = chasePointRepairCarEngineHealth; } catch { }
            try { v.PetrolTankHealth = 1000; } catch { }
            try { Function.Call("SET_ENGINE_HEALTH", v, chasePointRepairCarEngineHealth); } catch { }
            try { Function.Call("SET_CAR_ENGINE_ON", v, false, true); } catch { }
            try { Function.Call("LOCK_CAR_DOORS", v, 2); } catch { }
            if (chasePointRepairCarOpenBonnet)
            {
                chasePointRepairCarBonnetRetryTicks = Math.Max(30, chasePointRepairCarBonnetRetryMaxTicks);
                UpdateRepairCarBonnet();
            }
            if (chasePointRepairCarFreeze)
            {
                try { Function.Call("FREEZE_CAR_POSITION", v, true); } catch { }
            }
        }

        private void ConfigureMechanicPed(Ped ped)
        {
            if (!IsValidPed(ped)) return;
            try { Function.Call("SET_CHAR_AS_MISSION_CHAR", ped); } catch { }
            Vector3 spawn = GetChasePointMechanicSpawnPosition();
            try { Function.Call("SET_CHAR_COORDINATES", ped, spawn.X, spawn.Y, spawn.Z); } catch { }
            try { Function.Call("SET_CHAR_HEADING", ped, GetChasePointMechanicHeading()); } catch { }
            try { Function.Call("SET_BLOCKING_OF_NON_TEMPORARY_EVENTS", ped, true); } catch { }
            try { Function.Call("SET_CHAR_NEVER_TARGETTED", ped, true); } catch { }
            try { Function.Call("SET_CHAR_PROOFS", ped, true, true, true, true, true); } catch { }
            try { Function.Call("FREEZE_CHAR_POSITION", ped, false); } catch { }
            chasePointMechanicCycleState = 0;
            chasePointMechanicCycleTicks = Math.Max(30, chasePointMechanicWorkTicks);
            chasePointMechanicCycleTaskIssued = false;
            StartMechanicWorkTask(ped);
        }

        private bool IsMissionActiveSafe()
        {
            return IsMissionActiveSafe(false);
        }

        private bool IsMissionActiveFreshSafe()
        {
            return IsMissionActiveSafe(true);
        }

        private bool IsMissionActiveSafe(bool forceRefresh)
        {
            if (!forceRefresh && missionCheckCooldownTicks > 0)
            {
                missionCheckCooldownTicks--;
                return cachedMissionActive;
            }

            missionCheckCooldownTicks = Math.Max(15, missionCheckIntervalTicks);
            bool rawMissionActive = false;

            if (useNativeMissionFlagGuard && !missionFlagUnavailableLogged)
            {
                try
                {
                    rawMissionActive = Function.Call<bool>("GET_MISSION_FLAG");
                    if (!missionNativeFlagSourceLogged)
                    {
                        missionNativeFlagSourceLogged = true;
                        Log("MISSION_GUARD: using native GET_MISSION_FLAG.");
                    }
                }
                catch (Exception ex)
                {
                    missionFlagUnavailableLogged = true;
                    Log("MISSION_GUARD: native GET_MISSION_FLAG unavailable (" + ex.GetType().Name + "). Falling back to Player.isOnMission.");
                }
            }

            if (!rawMissionActive)
            {
                try
                {
                    rawMissionActive = Player.isOnMission;
                    if (!missionStateSourceLogged)
                    {
                        missionStateSourceLogged = true;
                        Log("MISSION_GUARD: using direct Player.isOnMission.");
                    }
                }
                catch (Exception ex)
                {
                    if (!missionPropertyFailureLogged)
                    {
                        missionPropertyFailureLogged = true;
                        Log("MISSION_GUARD: direct Player.isOnMission failed (" + ex.GetType().Name + ").");
                    }
                    rawMissionActive = false;
                }
            }


            if (rawMissionActive)
            {
                missionPositiveRefreshes++;
                missionNegativeRefreshes = 0;
                if (cachedMissionActive || missionPositiveRefreshes >= Math.Max(1, missionRequiredPositiveRefreshes)) cachedMissionActive = true;
            }
            else
            {
                missionNegativeRefreshes++;
                missionPositiveRefreshes = 0;
                if (missionNegativeRefreshes >= 1) cachedMissionActive = false;
            }

            return cachedMissionActive;
        }


        private void CleanupChasePointDecoration(string reason)
        {
            if (IsValidPed(chasePointMechanicPed)) DeletePedSafe(chasePointMechanicPed, "Chase Point mechanic " + reason);
            chasePointMechanicPed = null;
            for (int i = 0; i < chasePointExtraStaffPeds.Count; i++)
            {
                if (IsValidPed(chasePointExtraStaffPeds[i])) DeletePedSafe(chasePointExtraStaffPeds[i], "Chase Point staff " + reason);
            }
            chasePointExtraStaffPeds.Clear();
            if (IsValidVehicle(chasePointRepairCar)) DeleteVehicleSafe(chasePointRepairCar, "Chase Point repair car " + reason);
            chasePointRepairCar = null;
            chasePointMechanicCycleState = 0;
            chasePointMechanicCycleTicks = 0;
            chasePointMechanicCycleTaskIssued = false;
        }

        private void DeletePedSafe(Ped ped, string purpose)
        {
            if (!IsValidPed(ped)) return;
            try { Function.Call("DELETE_CHAR", ped); } catch { }
            try
            {
                MethodInfo m = ped.GetType().GetMethod("Delete", new Type[0]);
                if (m != null) m.Invoke(ped, null);
            }
            catch { }
            if (logToFile && !String.IsNullOrEmpty(purpose)) Log("Deleted " + purpose + ".");
        }

        private void DeleteVehicleSafe(Vehicle v, string purpose)
        {
            if (!IsValidVehicle(v)) return;
            try { Function.Call("FREEZE_CAR_POSITION", v, false); } catch { }
            try { Function.Call("DELETE_CAR", v); } catch { }
            try
            {
                MethodInfo m = v.GetType().GetMethod("Delete", new Type[0]);
                if (m != null) m.Invoke(v, null);
            }
            catch { }
            if (logToFile && !String.IsNullOrEmpty(purpose)) Log("Deleted " + purpose + ".");
        }

        private void StartMechanicWorkTask(Ped ped)
        {
            if (!IsValidPed(ped)) return;
            Vector3 pos = GetChasePointMechanicSpawnPosition();
            try { Function.Call("SET_CHAR_COORDINATES", ped, pos.X, pos.Y, pos.Z); } catch { }
            try { Function.Call("SET_CHAR_HEADING", ped, GetChasePointMechanicHeading()); } catch { }
            try { Function.Call("TASK_TURN_CHAR_TO_FACE_COORD", ped, chasePointRepairCarX, chasePointRepairCarY, chasePointRepairCarZ); } catch { }
            try { Function.Call("TASK_LOOK_AT_COORD", ped, chasePointRepairCarX, chasePointRepairCarY, chasePointRepairCarZ + 0.4f, -1, 0); } catch { }
            if (chasePointMechanicUseScenario && !String.IsNullOrEmpty(chasePointMechanicScenario))
            {
                try { Function.Call("TASK_START_SCENARIO_IN_PLACE", ped, chasePointMechanicScenario, 0, true); } catch { }
            }
            else
            {
                try { Function.Call("TASK_STAND_STILL", ped, -1); } catch { }
            }
            chasePointMechanicCycleTaskIssued = true;
        }

        private void StartMechanicWalkToGarage(Ped ped)
        {
            if (!IsValidPed(ped)) return;
            try { Function.Call("CLEAR_CHAR_TASKS", ped); } catch { }
            try { Function.Call("TASK_GO_STRAIGHT_TO_COORD", ped, chasePointMechanicGarageX, chasePointMechanicGarageY, chasePointMechanicGarageZ, 1.0f, Math.Max(1000, chasePointMechanicWalkTicks * 16)); } catch { }
            try { Function.Call("TASK_FOLLOW_NAV_MESH_TO_COORD", ped, chasePointMechanicGarageX, chasePointMechanicGarageY, chasePointMechanicGarageZ, 1.0f, Math.Max(1000, chasePointMechanicWalkTicks * 16), 0.25f); } catch { }
            chasePointMechanicCycleTaskIssued = true;
        }

        private void StartMechanicReturnToCar(Ped ped)
        {
            if (!IsValidPed(ped)) return;
            Vector3 pos = GetChasePointMechanicSpawnPosition();
            try { Function.Call("CLEAR_CHAR_TASKS", ped); } catch { }
            try { Function.Call("TASK_GO_STRAIGHT_TO_COORD", ped, pos.X, pos.Y, pos.Z, 1.0f, Math.Max(1000, chasePointMechanicWalkTicks * 16)); } catch { }
            try { Function.Call("TASK_FOLLOW_NAV_MESH_TO_COORD", ped, pos.X, pos.Y, pos.Z, 1.0f, Math.Max(1000, chasePointMechanicWalkTicks * 16), 0.25f); } catch { }
            chasePointMechanicCycleTaskIssued = true;
        }

        private void UpdateChasePointMechanicWorkCycle()
        {
            if (!chasePointMechanicUseWorkCycle || !IsValidPed(chasePointMechanicPed)) return;
            if (serviceOpen || scriptGarageTransitioning) return;

            if (chasePointMechanicCycleTicks > 0)
            {
                chasePointMechanicCycleTicks--;
                if (!chasePointMechanicCycleTaskIssued)
                {
                    if (chasePointMechanicCycleState == 0) StartMechanicWorkTask(chasePointMechanicPed);
                    else if (chasePointMechanicCycleState == 1) StartMechanicWalkToGarage(chasePointMechanicPed);
                    else if (chasePointMechanicCycleState == 3) StartMechanicReturnToCar(chasePointMechanicPed);
                }
                return;
            }

            chasePointMechanicCycleTaskIssued = false;
            if (chasePointMechanicCycleState == 0)
            {
                chasePointMechanicCycleState = 1;
                chasePointMechanicCycleTicks = Math.Max(30, chasePointMechanicWalkTicks);
                StartMechanicWalkToGarage(chasePointMechanicPed);
            }
            else if (chasePointMechanicCycleState == 1)
            {
                chasePointMechanicCycleState = 2;
                chasePointMechanicCycleTicks = Math.Max(30, chasePointMechanicGarageTicks);
                try { Function.Call("CLEAR_CHAR_TASKS", chasePointMechanicPed); } catch { }
                try { Function.Call("SET_CHAR_COORDINATES", chasePointMechanicPed, chasePointMechanicGarageX, chasePointMechanicGarageY, chasePointMechanicGarageZ); } catch { }
                try { Function.Call("SET_CHAR_HEADING", chasePointMechanicPed, chasePointMechanicGarageHeading); } catch { }
                try { Function.Call("TASK_STAND_STILL", chasePointMechanicPed, -1); } catch { }
            }
            else if (chasePointMechanicCycleState == 2)
            {
                chasePointMechanicCycleState = 3;
                chasePointMechanicCycleTicks = Math.Max(30, chasePointMechanicWalkTicks);
                StartMechanicReturnToCar(chasePointMechanicPed);
            }
            else
            {
                chasePointMechanicCycleState = 0;
                chasePointMechanicCycleTicks = Math.Max(30, chasePointMechanicWorkTicks);
                StartMechanicWorkTask(chasePointMechanicPed);
            }
        }

        private void UpdateChasePointExtraStaffPeds()
        {
            if (!enableChasePointExtraStaff || !enableChasePointWorkshop) return;
            if (serviceOpen || scriptGarageTransitioning) return;
            if (chasePointExtraStaffRetryTicks > 0) { chasePointExtraStaffRetryTicks--; return; }

            StaffSpawn[] spawns = ParseStaffSpawns(chasePointExtraStaffSpawns);
            if (spawns.Length == 0) return;
            string[] models = SplitCsv(chasePointExtraStaffModels);
            if (models.Length == 0) return;

            try
            {
                if (Player.Character == null || !Player.Character.Exists()) return;
                Vector3 p = Player.Character.Position;
                float max = Math.Max(20.0f, chasePointExtraStaffSpawnDistance);
                float dx = p.X - chasePointRepairCarX;
                float dy = p.Y - chasePointRepairCarY;
                float dz = p.Z - chasePointRepairCarZ;
                if ((dx * dx + dy * dy) > (max * max) || Math.Abs(dz) > 35.0f) return;
            }
            catch { return; }

            while (chasePointExtraStaffPeds.Count < spawns.Length) chasePointExtraStaffPeds.Add(null);
            bool requestedAny = false;
            for (int i = 0; i < spawns.Length; i++)
            {
                if (IsValidPed(chasePointExtraStaffPeds[i])) continue;
                string model = models[i % models.Length];
                if (!PreparePedModelForStaff(model)) { requestedAny = true; continue; }
                StaffSpawn ss = spawns[i];
                Vector3 pos = GetOffsetPosition(chasePointRepairCarX, chasePointRepairCarY, chasePointRepairCarZ, chasePointRepairCarHeading, ss.OffsetX, ss.OffsetY, ss.OffsetZ);
                Ped ped = TryCreatePedByName(model, pos, ss.Heading);
                if (IsValidPed(ped))
                {
                    ConfigureExtraStaffPed(ped, pos, ss.Heading);
                    chasePointExtraStaffPeds[i] = ped;
                    Log("Chase Point extra staff NPC created. Model=" + model + ", Slot=" + i.ToString() + ".");
                }
            }
            if (requestedAny) chasePointExtraStaffRetryTicks = 30;
        }

        private bool PreparePedModelForStaff(string modelName)
        {
            int hash = 0;
            try { hash = Function.Call<int>("GET_HASH_KEY", modelName); } catch { hash = 0; }
            if (hash == 0) return false;
            try { if (Function.Call<bool>("HAS_MODEL_LOADED", hash)) return true; } catch { }
            try { Function.Call("REQUEST_MODEL", hash); } catch { }
            return false;
        }

        private void ConfigureExtraStaffPed(Ped ped, Vector3 pos, float heading)
        {
            if (!IsValidPed(ped)) return;
            try { Function.Call("SET_CHAR_AS_MISSION_CHAR", ped); } catch { }
            try { Function.Call("SET_CHAR_COORDINATES", ped, pos.X, pos.Y, pos.Z); } catch { }
            try { Function.Call("SET_CHAR_HEADING", ped, heading); } catch { }
            try { Function.Call("SET_BLOCKING_OF_NON_TEMPORARY_EVENTS", ped, true); } catch { }
            try { Function.Call("SET_CHAR_NEVER_TARGETTED", ped, true); } catch { }
            try { Function.Call("SET_CHAR_PROOFS", ped, true, true, true, true, true); } catch { }
            try { Function.Call("TASK_STAND_STILL", ped, -1); } catch { }
        }

        private class StaffSpawn
        {
            public float OffsetX;
            public float OffsetY;
            public float OffsetZ;
            public float Heading;
        }

        private StaffSpawn[] ParseStaffSpawns(string value)
        {
            List<StaffSpawn> result = new List<StaffSpawn>();
            if (value == null) return result.ToArray();
            string[] rows = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < rows.Length; i++)
            {
                string[] p = rows[i].Split(new char[] { ',' });
                if (p.Length < 4) continue;
                StaffSpawn ss = new StaffSpawn();
                ss.OffsetX = ReadFloat(p[0], 0.0f);
                ss.OffsetY = ReadFloat(p[1], 0.0f);
                ss.OffsetZ = ReadFloat(p[2], -0.35f);
                ss.Heading = ReadFloat(p[3], 0.0f);
                result.Add(ss);
            }
            return result.ToArray();
        }


        private void EnsureWorkshopTriggers()
        {

            if (useCoordinateWorkshopTriggers && payNSprayTriggers.Count == 0)
            {
                payNSprayTriggers.Add(new WorkshopTrigger("Brucie's executive lifestyle autos", 864.271f, -130.017f, 5.519f, 5.0f, 303.840f));
                Log("Built-in Brucie workshop trigger active.");
                if (enableChasePointWorkshop)
                {
                    payNSprayTriggers.Add(new WorkshopTrigger("Chase Point auto workshop", 711.333f, 1507.065f, 14.398f, 5.0f, 178.564f, false));
                    Log("Built-in Chase Point in-place workshop trigger active.");
                }
            }

            if (!HasScriptGarageInteriorCoordinates())
            {
                scriptGarageX = 877.740f;
                scriptGarageY = -115.060f;
                scriptGarageZ = 5.520f;
                scriptGarageHeading = 88.770f;
                Log("No script garage interior coordinates found in INI. Added built-in Brucie garage interior position.");
            }

            if (useScriptGarage && useCoordinateWorkshopTriggers && HasScriptGarageInteriorCoordinates())
            {
                useSeparateGarageInterior = true;
            }

            Log("Coordinate workshop triggers loaded: " + payNSprayTriggers.Count.ToString() + ". SeparateInterior=" + useSeparateGarageInterior.ToString() + ", Interior=" + scriptGarageX.ToString("0.000") + "," + scriptGarageY.ToString("0.000") + "," + scriptGarageZ.ToString("0.000") + "," + scriptGarageHeading.ToString("0.000") + ", HornEntry=True, StopSpeed=" + scriptGarageEntryMaxSpeed.ToString("0.00") + ".");
            if (useCoordinateWorkshopTriggers && payNSprayTriggers.Count == 0)
            {
                Log("No WorkshopTrigger entries available. Vanilla fallback is " + allowVanillaGarageFallbackInScriptGarage.ToString() + ". Press F7 in a vehicle to dump coordinates for configuration.");
            }
        }


        private void NormaliseMenuKeys()
        {
            if (keyExit == keyBack) keyExit = Keys.Escape;
        }

        private void LoadIni()
        {
            try
            {
                if (!File.Exists(iniPath)) return;

                string section = "";
                string[] lines = File.ReadAllLines(iniPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#")) continue;
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        section = line.Substring(1, line.Length - 2).Trim().ToLowerInvariant();
                        continue;
                    }
                    int eq = line.IndexOf('=');
                    if (eq <= 0) continue;
                    string key = line.Substring(0, eq).Trim();
                    string value = line.Substring(eq + 1).Trim();
                    ApplyIniValue(section, key, value);
                }
            }
            catch
            {

            }
        }

        private void ApplyIniValue(string section, string key, string value)
        {


            string sec = section == null ? "" : section.Trim().ToLowerInvariant();
            string k = key == null ? "" : key.Trim().ToLowerInvariant();

            if (sec == "workshopprices")
            {
                if (k == "repaintbase") priceRepaintBase = Math.Max(0, ReadInt(value, priceRepaintBase));
                else if (k == "repaintspecular") priceRepaintSpecular = Math.Max(0, ReadInt(value, priceRepaintSpecular));
                else if (k == "repaintfeature1") priceRepaintFeature1 = Math.Max(0, ReadInt(value, priceRepaintFeature1));
                else if (k == "repaintfeature2") priceRepaintFeature2 = Math.Max(0, ReadInt(value, priceRepaintFeature2));
                else if (k == "toggleextra") priceToggleExtra = Math.Max(0, ReadInt(value, priceToggleExtra));
                else if (k == "repair") priceRepair = Math.Max(0, ReadInt(value, priceRepair));
                else if (k == "usedynamicrepairprice") useDynamicRepairPrice = ReadBool(value, useDynamicRepairPrice);
                else if (k == "repairminimumfraction") repairMinimumFraction = Math.Max(0.0f, Math.Min(1.0f, ReadFloat(value, repairMinimumFraction)));
                else if (k == "repairbodyweight") repairBodyWeight = Math.Max(0.0f, ReadFloat(value, repairBodyWeight));
                else if (k == "repairengineweight") repairEngineWeight = Math.Max(0.0f, ReadFloat(value, repairEngineWeight));
                else if (k == "repairtankweight") repairTankWeight = Math.Max(0.0f, ReadFloat(value, repairTankWeight));
                return;
            }

            if (sec == "workshoppricemodifiers")
            {
                if (k == "useswanknesspricemodifiers") useSwanknessPriceModifiers = ReadBool(value, useSwanknessPriceModifiers);
                else if (k == "swankness0multiplier") swankness0Multiplier = Math.Max(0.0f, ReadFloat(value, swankness0Multiplier));
                else if (k == "swankness1multiplier") swankness1Multiplier = Math.Max(0.0f, ReadFloat(value, swankness1Multiplier));
                else if (k == "swankness2multiplier") swankness2Multiplier = Math.Max(0.0f, ReadFloat(value, swankness2Multiplier));
                else if (k == "swankness3multiplier") swankness3Multiplier = Math.Max(0.0f, ReadFloat(value, swankness3Multiplier));
                else if (k == "swankness4multiplier") swankness4Multiplier = Math.Max(0.0f, ReadFloat(value, swankness4Multiplier));
                else if (k == "swankness5plusmultiplier") swankness5PlusMultiplier = Math.Max(0.0f, ReadFloat(value, swankness5PlusMultiplier));
                else if (k == "cheapmultiplier") { float v = Math.Max(0.0f, ReadFloat(value, swankness1Multiplier)); swankness0Multiplier = v; swankness1Multiplier = v; }
                else if (k == "standardmultiplier") swankness2Multiplier = Math.Max(0.0f, ReadFloat(value, swankness2Multiplier));
                else if (k == "premiummultiplier") { float v = Math.Max(0.0f, ReadFloat(value, swankness3Multiplier)); swankness3Multiplier = v; swankness4Multiplier = v; }
                else if (k == "luxurymultiplier") swankness5PlusMultiplier = Math.Max(0.0f, ReadFloat(value, swankness5PlusMultiplier));
                else if (k == "fallbackmultiplier") swanknessFallbackMultiplier = Math.Max(0.0f, ReadFloat(value, swanknessFallbackMultiplier));
                return;
            }

            if (sec == "swanknessoverrides")
            {
                RegisterVehicleSwanknessOverride(k, ReadInt(value, 2));
                return;
            }

            if (sec == "controls")
            {
                if (k == "menuup") keyUp = ReadKey(value, keyUp);
                else if (k == "menudown") keyDown = ReadKey(value, keyDown);
                else if (k == "menuleft") keyLeft = ReadKey(value, keyLeft);
                else if (k == "menuright") keyRight = ReadKey(value, keyRight);
                else if (k == "select") keySelect = ReadKey(value, keySelect);
                else if (k == "back") keyBack = ReadKey(value, keyBack);
                else if (k == "exit") keyExit = ReadKey(value, keyExit);
                return;
            }

            if (sec == "debug")
            {
                if (k == "showdebugmessages") showDebugMessages = ReadBool(value, showDebugMessages);
                else if (k == "logtofile") logToFile = ReadBool(value, logToFile);
                return;
            }
        }

        private bool ReadBool(string value, bool fallback)
        {
            string v = value.Trim().ToLowerInvariant();
            if (v == "1" || v == "true" || v == "yes" || v == "on") return true;
            if (v == "0" || v == "false" || v == "no" || v == "off") return false;
            return fallback;
        }

        private int ReadInt(string value, int fallback)
        {
            try { return Int32.Parse(value); } catch { return fallback; }
        }

        private float ReadFloat(string value, float fallback)
        {
            try { return Single.Parse(value, System.Globalization.CultureInfo.InvariantCulture); } catch { return fallback; }
        }

        private Keys ReadKey(string value, Keys fallback)
        {
            try { return (Keys)Enum.Parse(typeof(Keys), value.Trim(), true); }
            catch { return fallback; }
        }
    }
}


namespace LibertyVehicleServicesCE
{


    public class LibertyVehicleServicesCEScript : Script
    {
        [DllImport("plugins\\000_LVSCE_LCP_Bridge.asi", CallingConvention = CallingConvention.Cdecl)]
        private static extern int LVSCE_LCP_Install();

        [DllImport("plugins\\000_LVSCE_LCP_Bridge.asi", CallingConvention = CallingConvention.Cdecl)]
        private static extern int LVSCE_LCP_GetStatus();

        [DllImport("plugins\\000_LVSCE_LCP_Bridge.asi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int LVSCE_LCP_SetPlate(int vehicleHandle, string plate);

        [DllImport("plugins\\000_LVSCE_LCP_Bridge.asi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int LVSCE_LCP_SetPlateEx(int vehicleHandle, int plateTxd, int lettersTxd, float scaleX, float scaleY, float offsetX, float offsetY, float tintR, float tintG, float tintB, float tintA, string plate);

        [DllImport("plugins\\000_LVSCE_LCP_Bridge.asi", CallingConvention = CallingConvention.Cdecl)]
        private static extern int LVSCE_LCP_GetBridgeVersion();

        private const int CustomPlateAbsoluteMaxLength = 8;
        private const int WorkshopServiceBlipSpriteForNames = 30;
        private const float StevieRecoveryX = 733.774f;
        private const float StevieRecoveryY = 1402.436f;
        private const float StevieRecoveryZ = 13.707f;
        private const float StevieRecoveryHeading = 271.343f;
        private static readonly float RecoverySpawnClearRadius = 3.0f;

        private bool enabled = true;
        private bool dealersEnabled = true;
        private bool streetRegistrationEnabled = true;
        private bool ownershipEnabled = true;
        private bool fuelModuleEnabled = true;
        private bool metroStationsEnabled = true;
        private bool metroStationBlipsEnabled = true;
        private bool metroEntranceBlipsEnabled = true;
        private bool workshopModuleExternal = false;
        private bool workshopEnabled = true;
        private AutoWorkshopCE.AutoWorkshopCEModule workshopModule = null;

        private bool showDebugMessages = false;
        private bool logToFile = true;
        private bool logPositionHotkeyEnabled = true;
        private bool blockPlayerControlDuringMenu = true;
        private bool freezePreviewVehicles = true;
        private bool markOwnedVehiclesAsMissionCars = true;
        private bool spawnOwnedVehiclesOnLoad = true;
        private bool updateOwnedStateWhileDriving = true;
        private const float OwnedVehicleAdoptionRadius = 5.0f;
        private const float OwnedVehicleAdoptionMaxZDelta = 2.5f;
        private bool allowServiceVehicleRegistration = false;
        private bool allowEmergencyRegistration = false;
        private bool allowTaxiRegistration = false;
        private bool allowIndustrialRegistration = false;
        private bool requireNoWantedForStreetRegistration = true;
        private bool requireNoWantedForDealers = true;
        private bool requireVehicleStoppedForRegistration = true;
        private bool showWorldPrompts = true;
        private bool createDealerPreviewVehicles = true;
        private bool deletePreviewOnCancel = true;
        private bool keepUnknownDealerModels = false;
        private bool blockWhenPlayerControlDisabled = true;
        private bool suspendServicesDuringMissions = true;
        private bool missionGuardWasActive = false;
        private bool cachedMissionActive = false;
        private int missionCheckCooldownTicks = 0;
        private int missionCheckIntervalTicks = 90;
        private int missionPositiveRefreshes = 0;
        private int missionNegativeRefreshes = 0;
        private int missionRequiredPositiveRefreshes = 2;
        private bool missionFlagUnavailableLogged = false;
        private bool missionPropertyFailureLogged = false;
        private bool missionStateSourceLogged = false;
        private bool useNativeMissionFlagGuard = true;
        private bool missionNativeFlagSourceLogged = false;
        private bool enableDealerBlips = true;
        private bool enableOwnedVehicleBlips = true;
        private bool ownedVehicleBlipsOnlyWhenSpawned = false;
        private const float OwnedVehicleRestoreRadius = 95.0f;
        private const int OwnedVehicleRestoreIntervalTicks = 60;
        private int dealerBlipSprite = 79;
        private int fuelStationBlipSprite = 93;
        private const int MetroStationBlipSprite = 58;
        private int ownedVehicleBlipSprite = 1;
        private float dealerBlipScale = 1.0f;
        private float ownedVehicleBlipScale = 0.75f;
        private float fuelStationBlipScale = 0.75f;
        private const float MetroStationBlipScale = 1.0f;


        private const float MetroEntranceActivationRadius = 260.0f;
        private const int MetroEntranceMaxActiveBlips = 12;
        private const float MetroEntranceBlipScale = 0.5f;
        private int mapBlipRetryTicks = 0;
        private bool logBlipDiagnostics = false;
        private bool pauseMenuLastActiveForBlips = false;
        private bool pauseWorldGuardWasActive = false;
        private int blipDiagnosticCooldownTicks = 0;
        private bool refreshOwnedMissionFlagEachTick = false;
        private bool forceDealerSaleSlotVehicles = true;
        private bool randomiseDealerSaleSlotModels = true;
        private const float dealerSaleSlotClearRadius = 2.25f;
        private const float dealerSaleSlotActivationRadius = 90.0f;
        private int dealerSaleSlotRespawnTicks = 240;
        private int dealerSaleSlotMaintenanceIntervalTicks = 120;
        private int dealerSaleSlotMaxCreatesPerUpdate = 4;
        private int adoptedDealerScanIntervalTicks = 150;
        private int adoptedDealerMaxRetryAttempts = 8;
        private float dealerSaleMinimumHealth = 650.0f;
        private float dealerSaleMinimumEngineHealth = 650.0f;
        private float dealerSaleDamagedPriceFloorFactor = 0.65f;
        private int dealerWorldWarmupTicks = 450;
        private int dealerDeliveryReservationTicks = 900;
        private float dealerVehicleInteractRadius = 3.25f;
        private bool safelyGroundCreatedVehicles = true;
        private int dealerForcedSlotIndex = -1;
        private int missionGuardNoticeCooldownTicks = 0;
        private float dealerPriceVariancePercent = 5.0f;
        private Random dealerRandom = new Random();
        private bool focusCameraOnDealerSelection = false;
        private bool probeDealerExtras = true;
        private bool logDealerExtraDiagnostics = false;
        private bool libertyCityPlatesEnabled = true;
        private bool libertyCityPlatesRequired = false;
        private bool useLibertyCityPlatesBridge = true;
        private bool generatePlateForOwnedVehicles = true;
        private bool useLcpPlateDefinitions = true;
        private bool reapplyPlateOnOwnedVehicleSpawn = true;
        private bool logPlateDiagnostics = false;
        private List<LcpPlateDefinition> lcpPlateDefinitions = new List<LcpPlateDefinition>();
        private Dictionary<int, LcpPlateDefinition> lcpPlateById = new Dictionary<int, LcpPlateDefinition>();
        private Dictionary<string, int> lcpUsedPlateByModel = new Dictionary<string, int>();
        private int generatedPlateLength = 7;
        private int plateApplyRetryTicks = 60;
        private int plateApplyMaxAttempts = 6;
        private int plateApplyStartDelayTicks = 150;
        private bool lcpDetectionDone = false;
        private bool lcpDetected = false;
        private bool lcpMissingLogged = false;
        private bool lcpBridgeChecked = false;
        private bool lcpBridgeAvailable = false;
        private bool lcpBridgeMissingLogged = false;
        private bool lcpBridgeStatusLogged = false;
        private int lcpBridgeLastStatus = Int32.MinValue;
        private int lcpBridgeLastVersion = 0;
        private Random plateRandom = new Random();
        private bool dealerExtrasCacheValid = false;
        private int dealerExtrasCacheHandle = 0;
        private string dealerExtrasCacheModel = "";

        private int minColourID = 0;
        private int maxColourID = 133;
        private int minExtraID = 1;
        private int maxExtraID = 9;
        private bool captureAllExtraSlots = true;
        private bool saveVehicleDirtLevel = true;
        private int stateSaveIntervalTicks = 300;
        private int ownedSpawnDelayTicks = 240;
        private int previewRespawnCooldownTicks = 30;
        private int messageTimer = 0;
        private string messageText = "";
        private int tickCounter = 0;
        private int ownedVehicleRestoreCooldownTicks = 0;
        private bool attemptedInitialSpawn = false;
        private int nextOwnedId = 1;
        private int previewCooldown = 0;
        private bool delayedWorldInitialisationDone = false;
        private int worldReadyStableTicks = 0;
        private const int WorldReadyRequiredStableTicks = 45;
        private bool worldReadyLogWritten = false;
        private bool worldNotReadyLogWritten = false;

        private float registrationStreetMultiplier = 0.30f;
        private float registrationDealerMultiplier = 0.00f;
        private float insuranceDealerMultiplier = 0.12f;
        private float insuranceStreetMultiplier = 0.20f;
        private float recoveryInsuredFeeMultiplier = 0.00f;
        private float recoveryUninsuredFeeMultiplier = 0.35f;
        private int maxOwnedCarsPerEpisode = 5;
        private int customPlatePrice = 500;
        private int stevieDeliveryFee = 300;
        private int customPlateMaxLength = 8;


        private float fuelPricePerGallon = 3.30f;
        private float fuelConsumptionMultiplier = 1.0f;
        private float fuelMissionConsumptionMultiplier = 0.75f;
        private float fuelMissionMinimumPercent = 60.0f;
        private float fuelStartingMinPercent = 5.0f;
        private float fuelStartingMaxPercent = 85.0f;
        private float fuelDealerPurchasePercent = 85.0f;
        private float fuelRefuelRateGallonsPerSecond = 1.25f;
        private float fuelLowPromptPercent = 7.0f;
        private float fuelSputterPercent = 5.0f;
        private float fuelCriticalSputterPercent = 2.0f;
        private float fuelStoppedSpeedThreshold = 0.45f;
        private bool fuelTestDisplayEnabled = false;
        private bool fuelHideHudDuringCutscenes = true;
        private const int FuelUpdateIntervalMs = 500;
        private const int FuelMissionEntryProbeWindowMs = 30000;
        private const int FuelMissionEntryProbeIntervalMs = 2000;
        private const float FuelEpsilonGallons = 0.01f;
        private int fuelLastUpdateMs = 0;
        private string fuelPromptText = "";
        private int fuelPromptUntilMs = 0;
        private string fuelStatusText = "";
        private int fuelStatusUntilMs = 0;
        private int fuelCurrentVehicleHandle = 0;
        private FuelVehicleState fuelCurrentState = null;
        private bool fuelRefuelling = false;
        private int fuelSputterRestoreVehicleHandle = 0;
        private int fuelSputterRestoreAtMs = 0;
        private FuelStation fuelRefuellingStation = null;
        private Vehicle fuelRefuellingVehicle = null;
        private float fuelRefuelCostCarry = 0.0f;
        private int fuelRefuelStartedAtMs = 0;
        private Random fuelRandom = new Random();
        private Dictionary<int, FuelVehicleState> fuelStates = new Dictionary<int, FuelVehicleState>();
        private List<FuelStation> fuelStations = new List<FuelStation>();
        private List<MetroStation> metroStations = new List<MetroStation>();
        private List<MetroEntrance> metroEntrances = new List<MetroEntrance>();
        private float dealerDefaultMarkup = 1.00f;
        private float extraPurchaseFraction = 0.025f;
        private int dealerExtraBasePrice = 75;
        private float dealerExtraSwankness0Multiplier = 1.00f;
        private float dealerExtraSwankness1Multiplier = 1.00f;
        private float dealerExtraSwankness2Multiplier = 2.00f;
        private float dealerExtraSwankness3Multiplier = 4.00f;
        private float dealerExtraSwankness4Multiplier = 6.00f;
        private float dealerExtraSwankness5PlusMultiplier = 8.00f;
        private int defaultVehicleValue = 15000;
        private int minimumDealerPrice = 1000;
        private int minimumStreetRegistrationPrice = 500;
        private int minimumInsurancePrice = 250;

        private Keys keyOpen = Keys.E;
        private Keys keyUp = Keys.Up;
        private Keys keyDown = Keys.Down;
        private Keys keyLeft = Keys.Left;
        private Keys keyRight = Keys.Right;
        private Keys keySelect = Keys.Enter;
        private Keys keyBack = Keys.Back;
        private Keys keyExit = Keys.Escape;
        private string currentEpisodeKey = "";
        private bool currentEpisodeKeyReliable = false;
        private bool episodeNativeUnavailableLogged = false;
        private bool episodeDetectionLogged = false;
        private int currentEpisodeRefreshTicks = 0;
        private int activeOwnedEpisodeMigratedCount = 0;
        private int pauseMenuReleaseTicks = 0;
        private const int pauseMenuReleaseDelayTicks = 8;
        private bool pauseMenuDisabledByLvsMenu = false;
        private bool pauseMenuDisableUnavailableLogged = false;
        private Keys keyLogPosition = Keys.F7;

        private string iniPath = "scripts\\LibertyVehicleServicesCE.ini";
        private string ownedPath = "scripts\\LibertyVehicleServicesCE.owned.ini";
        private string logPath = "scripts\\LibertyVehicleServicesCE.log";

        private Dictionary<int, VehicleDefinition> vehicleByHash = new Dictionary<int, VehicleDefinition>();
        private Dictionary<string, VehicleDefinition> vehicleByModel = new Dictionary<string, VehicleDefinition>();
        private Dictionary<string, string> vehicleModelAliases = new Dictionary<string, string>();
        private Dictionary<string, int> monetaryByHandling = new Dictionary<string, int>();
        private Dictionary<string, int> priceOverrides = new Dictionary<string, int>();
        private Dictionary<string, int> swanknessOverrides = new Dictionary<string, int>();
        private List<DealerDefinition> dealers = new List<DealerDefinition>();
        private List<RegistrationPoint> registrationPoints = new List<RegistrationPoint>();
        private List<OwnedVehicleRecord> ownedVehicles = new List<OwnedVehicleRecord>();
        private bool ownedVehiclesDirty = false;
        private bool ownedVehicleStorageHealthy = true;
        private bool ownedVehicleSaveSuppressedLogged = false;
        private Dictionary<string, float> ownedLastStoryProgressByEpisode = new Dictionary<string, float>();
        private float storyProgressPercent = 0.0f;
        private bool storyProgressReliable = false;
        private int storyProgressRefreshTicks = 0;
        private bool storyProgressUnavailableLogged = false;
        private bool storyProgressUnlockStateLogged = false;
        private const float IvBrokerDukesUnlockProgress = 8.0f;
        private const float IvBohanUnlockProgress = 15.0f;
        private const float IvAlgonquinUnlockProgress = 32.0f;
        private const float IvAlderneyUnlockProgress = 50.0f;
        private Dictionary<string, Blip> dealerBlips = new Dictionary<string, Blip>();
        private Dictionary<string, Blip> fuelStationBlips = new Dictionary<string, Blip>();
        private Dictionary<string, Blip> metroStationBlips = new Dictionary<string, Blip>();
        private Dictionary<string, Blip> metroEntranceBlips = new Dictionary<string, Blip>();

        private Vehicle lastPlayerVehicle = null;
        private int lastPlayerVehicleHandle = 0;

        private bool menuOpen = false;
        private string menuMode = "";
        private DealerDefinition activeDealer = null;
        private RegistrationPoint activeRegistrationPoint = null;
        private Vehicle activeRegistrationVehicle = null;
        private string activeRegistrationModelName = "";
        private OwnedVehicleRecord activeRegistrationOwnedRecord = null;
        private bool registrationInsuranceOnlyMode = false;
        private int selectedIndex = 0;
        private int dealerModelIndex = 0;
        private int dealerColourID = 0;
        private bool dealerInsuranceSelected = true;
        private Vehicle dealerPreviewVehicle = null;
        private string dealerPreviewModel = "";
        private int dealerPreviewHandle = 0;
        private bool dealerPreviewIsAmbient = false;
        private Dictionary<string, Vehicle> dealerSlotVehicles = new Dictionary<string, Vehicle>();
        private Dictionary<string, string> dealerSlotModels = new Dictionary<string, string>();
        private Dictionary<string, bool> dealerSlotAmbient = new Dictionary<string, bool>();
        private Dictionary<string, int> dealerSlotColours = new Dictionary<string, int>();
        private Dictionary<string, float> dealerSlotPriceFactors = new Dictionary<string, float>();
        private Dictionary<string, int> dealerSlotRespawnCooldown = new Dictionary<string, int>();
        private Dictionary<string, DealerHeldEmptySlot> dealerHeldEmptySlots = new Dictionary<string, DealerHeldEmptySlot>();
        private Dictionary<string, int> dealerMaintenanceCooldown = new Dictionary<string, int>();
        private Dictionary<string, int> dealerAdoptedLogCounts = new Dictionary<string, int>();
        private Dictionary<string, bool> dealerAdoptionLockedThisVisit = new Dictionary<string, bool>();
        private Dictionary<string, int> dealerNativeAdoptionAttempts = new Dictionary<string, int>();
        private Dictionary<string, int> dealerDeliveryReservations = new Dictionary<string, int>();
        private int dealerPromptCandidateCooldown = 0;
        private const int dealerPromptCandidateIntervalTicks = 8;
        private DealerDefinition dealerPromptCandidateDealer = null;
        private int dealerPromptCandidateSlotIndex = -1;
        private Vehicle dealerPromptCandidateVehicle = null;
        private bool dealerPromptCandidateExists = false;
        private bool dealerPromptCandidateSaleable = false;
        private bool dealerColourPaletteMode = false;
        private bool showDealerColourPalette = true;
        private int dealerColourPaletteColumns = 16;
        private float dealerColourPaletteCellWidth = 0.018f;
        private float dealerColourPaletteCellHeight = 0.014f;
        private float dealerColourPaletteGap = 0.002f;
        private bool dealerVehicleColourPaletteLoaded = false;
        private int[] dealerVehicleColourR = new int[256];
        private int[] dealerVehicleColourG = new int[256];
        private int[] dealerVehicleColourB = new int[256];
        private List<int> dealerAvailableExtras = new List<int>();
        private Dictionary<int, bool> dealerExtraStates = new Dictionary<int, bool>();
        private bool streetInsuranceSelected = true;
        private OwnedVehicleRecord customPlateTarget = null;
        private string customPlateInput = "";
        private List<LcpPlateDefinition> customPlateChoices = new List<LcpPlateDefinition>();
        private int customPlateDefinitionIndex = 0;
        private int customPlateFieldIndex = 1;

        private class VehicleDefinition
        {
            public string ModelName;
            public string HandlingName;
            public string GameName;
            public string Type;
            public string Flags;
            public int Hash;
            public int Swankness;
            public int MonetaryValue;
            public bool IsEmergency;
            public bool IsTaxi;
            public bool IsIndustrial;
            public bool IsBike;
            public bool IsSports;
            public string Section;
        }

        private class DealerDefinition
        {
            public string Id;
            public string Name;
            public bool Enabled;
            public float X;
            public float Y;
            public float Z;
            public float Radius;
            public float DisplayX;
            public float DisplayY;
            public float DisplayZ;
            public float DisplayHeading;
            public bool HasPurchaseSpawn;
            public float PurchaseX;
            public float PurchaseY;
            public float PurchaseZ;
            public float PurchaseHeading;
            public float Markup;
            public int BlipSprite;
            public float DisplayScanRadius;
            public bool ForceSaleSlotVehicles;
            public bool PersistentSaleSlotVehicles;
            public bool UseCarSpecificInteraction;
            public bool ShowroomDelivery;
            public bool AdoptNativeVehicles;
            public bool FallbackSpawnWhenNoNativeVehicles;
            public float CleanupX;
            public float CleanupY;
            public float CleanupZ;
            public float CleanupRadius;
            public bool HasCleanupBounds;
            public float CleanupMinX;
            public float CleanupMaxX;
            public float CleanupMinY;
            public float CleanupMaxY;
            public string PreferredModel;
            public float PreferredModelChance;
            public List<string> Models = new List<string>();
            public List<DealerDisplaySlot> DisplaySlots = new List<DealerDisplaySlot>();
            public List<DealerDeliverySlot> DeliverySlots = new List<DealerDeliverySlot>();
        }

        private class DealerDisplaySlot
        {
            public float X;
            public float Y;
            public float Z;
            public float Heading;
        }

        private class DealerDeliverySlot
        {
            public float X;
            public float Y;
            public float Z;
            public float Heading;
        }

        private class DealerDeliveryChoice
        {
            public DealerDeliverySlot Slot;
            public string Key;
        }

        private class DealerHeldEmptySlot
        {
            public string DealerId;
            public float X;
            public float Y;
            public float Z;
        }

        private class RegistrationPoint
        {
            public string Id;
            public string Name;
            public bool Enabled;
            public float X;
            public float Y;
            public float Z;
            public float Radius;
            public float VehicleRadius;
            public float PriceMultiplier;
            public int BlipSprite;
            public bool AcceptPremium;
            public bool AcceptIndustrial;
        }

        private class LcpPlateDefinition
        {
            public int Id;
            public int ParentId;
            public int Probability;
            public int PlateTxd;
            public int LettersTxd;
            public float ScaleX;
            public float ScaleY;
            public float OffsetX;
            public float OffsetY;
            public float TintR;
            public float TintG;
            public float TintB;
            public float TintA;
            public string Format;
            public string Name;
            public bool IsFallback;
        }

        private class LcpPlatePreview
        {
            public string Canonical;
            public string Display;
            public string RawInput;
            public bool Complete;
            public int RequiredLetters;
            public int RequiredDigits;
            public int RequiredAny;
            public int TypedLetters;
            public int TypedDigits;
            public int TypedAny;
            public string MissingText;
        }

        private class OwnedVehicleRecord
        {
            public string Id;
            public string ModelName;
            public int ModelHash;
            public string Source;
            public string Episode;
            public bool Registered;
            public bool Insured;
            public bool Destroyed;
            public int WorldHandle;
            public Vehicle VehicleRef;
            public Blip BlipRef;
            public bool BlipIsFallback;
            public int Colour1;
            public int Colour2;
            public int Colour3;
            public int Colour4;
            public string Extras;
            public int Health;
            public int EngineHealth;
            public int PetrolTankHealth;
            public float DirtLevel;
            public float X;
            public float Y;
            public float Z;
            public float Heading;
            public int PurchasePrice;
            public int InsurancePrice;
            public string PlateText;
            public int LcpPlateId = -1;
            public string LcpPlateFormat;
            public string LcpPlateName;
            public string LcpRawInput;
            public bool PlateGeneratedThisSession;
            public bool PlateApplied;
            public bool PlateApplySucceeded;
            public int PlateApplyAttempts;
            public int PlateApplyCooldown;
            public bool FuelKnown;
            public float FuelGallons;
            public float FuelCapacityGallons;
            public int DirtCaptureNextMs;
        }

        private class FuelVehicleState
        {
            public int Handle;
            public string ModelName;
            public float CapacityGallons;
            public float FuelGallons;
            public bool MissionBoostApplied;
            public bool MissionConsumptionReduced;
            public int MissionProbeUntilMs;
            public int MissionProbeNextMs;
            public bool WasOutOfFuel;
            public bool EngineForcedOff;
            public int OutOfFuelEnforceNextMs;
            public int LowPromptCooldownMs;
            public int SputterCooldownMs;
            public OwnedVehicleRecord OwnedRecord;
        }

        private class FuelStation
        {
            public string Id;
            public string Name;
            public string Kind;
            public float X;
            public float Y;
            public float Z;
            public float Radius;
            public float DamagedSeconds;
        }

        private class MetroStation
        {
            public string Id;
            public string Name;
            public string Line;
            public float X;
            public float Y;
            public float Z;
            public float Radius;
        }

        private class MetroEntrance
        {
            public string Id;
            public string StationId;
            public string LineCode;
            public string Label;
            public float X;
            public float Y;
            public float Z;
            public float Radius;
        }

        public LibertyVehicleServicesCEScript()
        {
            Interval = 0;
            currentEpisodeKey = "iv";
            LoadIni();
            ResetLogFile();
            Log("Bootstrap started. Native/world-bound initialisation is deferred until the game world is ready.");
            NormaliseMenuKeys();
            LoadVehicleDatabase();
            LoadLcpPlateDefinitions();
            LoadDealerVehicleColourPaletteFromCarcols();
            EnsureDefaultDealersAndRegistrationPoints();
            try { EnsureDefaultFuelStations(); } catch (Exception ex) { Log("Fuel station setup failed: " + ex.GetType().Name + ". Fuel module disabled."); fuelModuleEnabled = false; }
            try { EnsureDefaultMetroStations(); } catch (Exception ex) { Log("Metro station setup failed: " + ex.GetType().Name + ". Metro module disabled."); metroStationsEnabled = false; }
            FilterDealerModelsAgainstVehicleDatabase();
            LoadOwnedVehicles();
            this.Tick += new EventHandler(OnTick);
            this.KeyDown += new GTA.KeyEventHandler(OnKeyDown);
            Notify("Liberty Vehicle Services CE loaded");
            Log("Core loaded. Episode detection, story-progress checks, LCP bridge detection, workshop initialisation, blip creation and owned-car spawning will start after the save has fully entered the world. VehicleDefs=" + vehicleByModel.Count.ToString() + ", Dealers=" + dealers.Count.ToString() + ", RegistrationPoints=" + registrationPoints.Count.ToString() + ", Owned=" + ownedVehicles.Count.ToString() + ". Modules: dealers=" + dealersEnabled.ToString() + ", registration=" + streetRegistrationEnabled.ToString() + ", ownership=" + ownershipEnabled.ToString() + ", workshop=" + workshopEnabled.ToString() + ", workshopExternal=" + workshopModuleExternal.ToString() + ", fuel=" + fuelModuleEnabled.ToString() + ", metro=" + metroStationsEnabled.ToString() + ".");
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!enabled) return;
            try
            {
                tickCounter++;
                if (messageTimer > 0) messageTimer--;
                if (previewCooldown > 0) previewCooldown--;
                if (missionGuardNoticeCooldownTicks > 0) missionGuardNoticeCooldownTicks--;
                DecrementTimedDealerDictionaries();
                if (dealerWorldWarmupTicks > 0) dealerWorldWarmupTicks--;

                if (!IsGameWorldReadyForLvs())
                {
                    worldReadyStableTicks = 0;
                    if (!worldNotReadyLogWritten)
                    {
                        worldNotReadyLogWritten = true;
                        Log("Waiting for the loaded game world before running LVS world updates.");
                    }
                    return;
                }

                if (!delayedWorldInitialisationDone)
                {
                    if (!TryCompleteDelayedWorldInitialisation()) return;
                }

                RefreshCurrentEpisodeKey(false);
                UpdatePauseMenuGuard(menuOpen || WorkshopMenuOpenSafe());


                if (IsPauseMenuActiveSafe())
                {
                    if (!pauseWorldGuardWasActive)
                    {
                        pauseWorldGuardWasActive = true;
                        if (showDebugMessages) Log("PAUSE_GUARD active: skipping LVS world/entity/blip updates while pause menu/map is open.");
                    }
                    return;
                }
                if (pauseWorldGuardWasActive)
                {
                    pauseWorldGuardWasActive = false;
                    if (showDebugMessages) Log("PAUSE_GUARD released: LVS updates resumed.");
                }

                bool serviceAreaRelevant = IsServiceInteractionRelevantForMissionCheck();
                bool missionGuardActive = suspendServicesDuringMissions && serviceAreaRelevant && IsMissionActiveSafe();
                if (missionGuardActive)
                {
                    if (!missionGuardWasActive)
                    {
                        missionGuardWasActive = true;
                        if (showDebugMessages) Log("MISSION_GUARD active: nearby dealer/registration/workshop service interaction is suspended. Owned vehicles remain active.");
                    }
                    if (menuOpen) CloseMenu(false);
                    DrawSmallPrompt("Service unavailable during missions.");
                    if (missionGuardNoticeCooldownTicks <= 0)
                    {
                        Notify("Service unavailable during missions.");
                        missionGuardNoticeCooldownTicks = 120;
                    }
                }
                else if (missionGuardWasActive)
                {
                    missionGuardWasActive = false;
                    if (showDebugMessages) Log("MISSION_GUARD released: LVS service interactions resumed.");
                }


                if (!missionGuardActive)
                {
                    if (menuOpen)
                    {
                        DrawMenu();
                        return;
                    }
                    if (WorkshopMenuOpenSafe())
                    {
                        TickWorkshopModuleSafe("menu");
                        return;
                    }
                }

                TrackLastPlayerVehicle();

                if (fuelModuleEnabled && IsPlayerValid()) UpdateFuelSystem();

                if (ownershipEnabled)
                {
                    if (!attemptedInitialSpawn)
                    {
                        if (ownedSpawnDelayTicks <= 0)
                        {
                            attemptedInitialSpawn = true;
                            if (spawnOwnedVehiclesOnLoad) SpawnOwnedVehiclesFromSave();
                        }
                        else
                        {
                            ownedSpawnDelayTicks--;
                        }
                    }
                    UpdateOwnedVehicles();
                    UpdateOwnedVehiclePlateApplications();
                    if (enableOwnedVehicleBlips) UpdateOwnedVehicleBlips();
                    if (stateSaveIntervalTicks > 0 && tickCounter % stateSaveIntervalTicks == 0 && ownedVehiclesDirty) SaveOwnedVehicles();
                }

                if (missionGuardActive) return;

                if (workshopEnabled && !workshopModuleExternal && workshopModule != null) TickWorkshopModuleSafe("normal");

                RefreshStoryProgressCache(false);
                UpdateMapBlips();
                if (dealersEnabled) UpdateDealerSaleSlotWorld();

                if (dealersEnabled) UpdateDealerWorldPrompts();
                if (streetRegistrationEnabled) UpdateRegistrationWorldPrompts();
            }
            catch (System.Threading.ThreadAbortException)
            {

                return;
            }
            catch (Exception ex)
            {
                Notify("LVS error: " + ex.GetType().Name);
                Log("Tick error: " + ex.ToString());
            }
        }

        private bool IsGameWorldReadyForLvs()
        {
            if (!IsPlayerValid()) return false;
            try
            {
                Vector3 p = Player.Character.Position;
                if (Single.IsNaN(p.X) || Single.IsNaN(p.Y) || Single.IsNaN(p.Z)) return false;
                if (Single.IsInfinity(p.X) || Single.IsInfinity(p.Y) || Single.IsInfinity(p.Z)) return false;
            }
            catch { return false; }

            try { if (Function.Call<bool>("IS_SCREEN_FADING_IN")) return false; } catch { }
            try { if (Function.Call<bool>("IS_SCREEN_FADING_OUT")) return false; } catch { }
            try { if (Function.Call<bool>("IS_SCREEN_FADED_OUT")) return false; } catch { }
            return true;
        }

        private bool TryCompleteDelayedWorldInitialisation()
        {
            if (delayedWorldInitialisationDone) return true;
            worldReadyStableTicks++;
            if (worldReadyStableTicks < WorldReadyRequiredStableTicks) return false;

            try
            {
                string detectedEpisode = DetectCurrentEpisodeKey();
                if (detectedEpisode != null && detectedEpisode.Length > 0) currentEpisodeKey = detectedEpisode;
                RefreshStoryProgressCache(true);
                ResetOwnedVehiclesForNewGameIfNeeded();
                SeedOwnedStoryProgressMetaAfterLoad();

                if (libertyCityPlatesEnabled)
                {
                    DetectLibertyCityPlates(false);
                }

                if (workshopEnabled && !workshopModuleExternal && workshopModule == null)
                {
                    try
                    {
                        workshopModule = new AutoWorkshopCE.AutoWorkshopCEModule(true);
                        Log("Embedded workshop module initialised after world-ready guard.");
                    }
                    catch (Exception ex)
                    {
                        workshopModule = null;
                        Log("Embedded workshop module failed to initialise after world-ready guard: " + ex.GetType().Name + ".");
                    }
                }

                delayedWorldInitialisationDone = true;
                if (!worldReadyLogWritten)
                {
                    worldReadyLogWritten = true;
                    Log("World-ready initialisation complete. Episode=" + GetCurrentEpisodeKey() + ", ActiveOwned=" + CountCurrentEpisodeOwnedVehicles().ToString() + ", workshop=" + (workshopModule != null).ToString() + ".");
                }
                return true;
            }
            catch (Exception ex)
            {
                worldReadyStableTicks = 0;
                Log("World-ready initialisation failed: " + ex.GetType().Name + ". Will retry.");
                return false;
            }
        }

        private void DecrementTimedDealerDictionaries()
        {
            DecrementIntDictionary(dealerDeliveryReservations);
            DecrementIntDictionary(dealerMaintenanceCooldown);
        }

        private void DecrementIntDictionary(Dictionary<string, int> values)
        {
            if (values == null || values.Count == 0) return;
            List<string> keys = new List<string>(values.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                int value = 0;
                if (!values.TryGetValue(key, out value)) continue;
                value--;
                if (value <= 0) values.Remove(key);
                else values[key] = value;
            }
        }

        private bool WorkshopMenuOpenSafe()
        {
            if (!workshopEnabled || workshopModuleExternal || workshopModule == null) return false;
            try { return workshopModule.HostHasOpenMenu(); }
            catch { return false; }
        }

        private void TickWorkshopModuleSafe(string context)
        {
            if (!workshopEnabled || workshopModuleExternal || workshopModule == null) return;
            try { workshopModule.HostTick(); }
            catch (Exception wex) { Log("Workshop module tick error (" + context + "): " + wex.GetType().Name + "."); }
        }

        private void CloseWorkshopMenuForMissionGuardSafe()
        {
            if (!workshopEnabled || workshopModuleExternal || workshopModule == null) return;
            try { workshopModule.HostCloseMenuForMissionGuard(); }
            catch (Exception wex) { Log("Workshop module close error (mission guard): " + wex.GetType().Name + "."); }
        }

        private void OnKeyDown(object sender, GTA.KeyEventArgs e)
        {
            if (!enabled) return;
            try
            {
                if (logPositionHotkeyEnabled && e.Key == keyLogPosition)
                {
                    LogCurrentPositionForConfig();
                    return;
                }

                if (workshopEnabled && !workshopModuleExternal && workshopModule != null)
                {
                    try { workshopModule.HostKeyDown(e); }
                    catch (Exception wex) { Log("Workshop module key error: " + wex.GetType().Name + "."); }
                    if (WorkshopMenuOpenSafe()) return;
                }

                if (menuOpen)
                {
                    ConsumeKeyEvent(e);
                    HandleMenuKey(e.Key);
                    return;
                }

                if (e.Key == keyOpen && fuelModuleEnabled && TryHandleFuelRefuelKey()) return;

                if (e.Key == keyOpen)
                {
                    if (dealersEnabled && IsPlayerOnFoot())
                    {
                        DealerDefinition vehicleDealer;
                        int saleSlotIndex;
                        Vehicle saleVehicle;


                        if (TryGetNearestDealerVehicleCandidate(out vehicleDealer, out saleSlotIndex, out saleVehicle))
                        {
                            string reason;
                            if (!CanOpenDealerInteraction(out reason)) { Notify(reason); return; }
                            if (!IsDealerVehicleHealthyEnoughForSale(saleVehicle))
                            {
                                Notify("This vehicle needs repairs and is not for sale.");
                                return;
                            }
                            OpenDealerSaleVehicleMenu(vehicleDealer, saleSlotIndex, saleVehicle);
                            return;
                        }

                        DealerDefinition dealer = GetNearestDealer();
                        if (dealer != null && dealer.DisplaySlots.Count == 0)
                        {
                            string reason;
                            if (!CanOpenDealerInteraction(out reason)) { Notify(reason); return; }
                            OpenDealerMenu(dealer);
                            return;
                        }
                    }
                    if (streetRegistrationEnabled)
                    {
                        RegistrationPoint point = GetNearestRegistrationPoint();
                        if (point != null && IsPlayerOnFoot())
                        {
                            string reason;
                            if (!CanOpenBaseInteraction(out reason)) { Notify(reason); return; }
                            OpenRegistrationMenu(point);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notify("LVS key error: " + ex.GetType().Name);
                Log("Key error: " + ex.ToString());
            }
        }

        private void ConsumeKeyEvent(GTA.KeyEventArgs e)
        {
            if (e == null) return;
            try
            {
                PropertyInfo p = e.GetType().GetProperty("Handled");
                if (p != null && p.CanWrite) p.SetValue(e, true, null);
            }
            catch { }
        }

        private void UpdatePauseMenuGuard(bool menuActive)
        {
            if (menuActive)
            {
                pauseMenuReleaseTicks = pauseMenuReleaseDelayTicks;
                SetPauseMenuDisabledForLvs(true);
                return;
            }
            if (pauseMenuReleaseTicks > 0)
            {
                pauseMenuReleaseTicks--;
                SetPauseMenuDisabledForLvs(true);
                return;
            }
            if (pauseMenuDisabledByLvsMenu) SetPauseMenuDisabledForLvs(false);
        }

        private void HoldPauseMenuDisabledBriefly()
        {
            if (pauseMenuReleaseTicks < pauseMenuReleaseDelayTicks) pauseMenuReleaseTicks = pauseMenuReleaseDelayTicks;
            SetPauseMenuDisabledForLvs(true);
        }

        private void SetPauseMenuDisabledForLvs(bool disabled)
        {
            try
            {
                Function.Call("DISABLE_PAUSE_MENU", disabled);
                pauseMenuDisabledByLvsMenu = disabled;
            }
            catch (Exception ex)
            {
                if (!pauseMenuDisableUnavailableLogged)
                {
                    pauseMenuDisableUnavailableLogged = true;
                    Log("Pause-menu suppression unavailable: " + ex.GetType().Name + ".");
                }
            }
        }

        private void RefreshCurrentEpisodeKey(bool force)
        {
            if (!force && currentEpisodeRefreshTicks > 0)
            {
                currentEpisodeRefreshTicks--;
                return;
            }
            currentEpisodeRefreshTicks = 300;
            string detected = DetectCurrentEpisodeKey();
            if (detected.Length == 0) return;
            if (currentEpisodeKey == null || currentEpisodeKey.Length == 0)
            {
                currentEpisodeKey = detected;
                FinaliseOwnedVehicleEpisodesAfterLoad();
                return;
            }
            if (!currentEpisodeKey.Equals(detected, StringComparison.OrdinalIgnoreCase))
            {
                Log("Episode changed from " + currentEpisodeKey + " to " + detected + "; owned vehicles will be filtered by the new episode.");
                currentEpisodeKey = detected;
                attemptedInitialSpawn = false;
                ownedSpawnDelayTicks = 60;
            }
        }

        private string GetCurrentEpisodeKey()
        {
            if (currentEpisodeKey == null || currentEpisodeKey.Length == 0)
            {
                string detected = DetectCurrentEpisodeKey();
                if (detected.Length > 0) currentEpisodeKey = detected;
            }
            if (currentEpisodeKey == null || currentEpisodeKey.Length == 0) return "iv";
            return currentEpisodeKey;
        }

        private string DetectCurrentEpisodeKey()
        {
            try
            {
                int episode = Function.Call<int>("GET_CURRENT_EPISODE");
                string key = EpisodeKeyFromIndex(episode);
                currentEpisodeKeyReliable = true;
                if (!episodeDetectionLogged)
                {
                    episodeDetectionLogged = true;
                    Log("Episode detected through GET_CURRENT_EPISODE: " + episode.ToString() + " => " + key + ".");
                }
                return key;
            }
            catch (Exception ex)
            {
                if (!episodeNativeUnavailableLogged)
                {
                    episodeNativeUnavailableLogged = true;
                    Log("Episode native GET_CURRENT_EPISODE unavailable (" + ex.GetType().Name + "). Temporarily using iv compatibility fallback until detection succeeds.");
                }
            }
            currentEpisodeKeyReliable = false;
            return "";
        }

        private string EpisodeKeyFromIndex(int episode)
        {
            if (episode == 0) return "iv";
            if (episode == 1) return "tlad";
            if (episode == 2) return "tbogt";
            return "episode_" + episode.ToString(CultureInfo.InvariantCulture);
        }

        private string NormaliseEpisodeKey(string value)
        {
            if (value == null) return "";
            string v = value.Trim().ToLowerInvariant();
            if (v.Length == 0) return "";
            if (v == "0" || v == "iv" || v == "gtaiv" || v == "gta_iv" || v == "niko") return "iv";
            if (v == "1" || v == "tlad" || v == "lost" || v == "johnny") return "tlad";
            if (v == "2" || v == "tbogt" || v == "bogt" || v == "gaytony" || v == "luis") return "tbogt";
            return v;
        }

        private bool OwnedRecordBelongsToCurrentEpisode(OwnedVehicleRecord rec)
        {
            if (rec == null) return false;
            string current = GetCurrentEpisodeKey();
            string episode = NormaliseEpisodeKey(rec.Episode);
            if (episode.Length == 0)
            {
                if (!currentEpisodeKeyReliable && (currentEpisodeKey == null || currentEpisodeKey.Length == 0)) return true;
                rec.Episode = current;
                activeOwnedEpisodeMigratedCount++;
                return true;
            }
            return episode.Equals(current, StringComparison.OrdinalIgnoreCase);
        }

        private bool OwnedRecordBelongsToEpisode(OwnedVehicleRecord rec, string targetEpisode)
        {
            if (rec == null) return false;
            string target = NormaliseEpisodeKey(targetEpisode);
            if (target.Length == 0) target = GetCurrentEpisodeKey();
            string episode = NormaliseEpisodeKey(rec.Episode);
            if (episode.Length == 0) return target.Equals(GetCurrentEpisodeKey(), StringComparison.OrdinalIgnoreCase);
            return episode.Equals(target, StringComparison.OrdinalIgnoreCase);
        }

        private int CountCurrentEpisodeOwnedVehicles()
        {
            int count = 0;
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                if (ownedVehicles[i] != null && OwnedRecordBelongsToCurrentEpisode(ownedVehicles[i])) count++;
            }
            return count;
        }

        private int FinaliseOwnedVehicleEpisodesAfterLoad()
        {
            activeOwnedEpisodeMigratedCount = 0;
            int count = CountCurrentEpisodeOwnedVehicles();
            if (activeOwnedEpisodeMigratedCount > 0)
            {
                Log("Owned vehicle legacy migration: assigned " + activeOwnedEpisodeMigratedCount.ToString() + " record(s) to episode " + GetCurrentEpisodeKey() + ".");
                if (delayedWorldInitialisationDone) SaveOwnedVehicles();
                else ownedVehiclesDirty = true;
            }
            return count;
        }

        private bool IsMissionActiveSafe()
        {
            return IsMissionActiveSafe(false);
        }

        private bool IsMissionActiveFreshSafe()
        {
            return IsMissionActiveSafe(true);
        }

        private bool IsMissionActiveSafe(bool forceRefresh)
        {
            if (!forceRefresh && missionCheckCooldownTicks > 0)
            {
                missionCheckCooldownTicks--;
                return cachedMissionActive;
            }

            missionCheckCooldownTicks = Math.Max(15, missionCheckIntervalTicks);
            cachedMissionActive = false;

            if (useNativeMissionFlagGuard && !missionFlagUnavailableLogged)
            {
                try
                {
                    cachedMissionActive = Function.Call<bool>("GET_MISSION_FLAG");
                    if (!missionNativeFlagSourceLogged)
                    {
                        missionNativeFlagSourceLogged = true;
                        Log("MISSION_GUARD: using native GET_MISSION_FLAG.");
                    }
                }
                catch (Exception ex)
                {
                    missionFlagUnavailableLogged = true;
                    Log("MISSION_GUARD: native GET_MISSION_FLAG unavailable (" + ex.GetType().Name + "). Falling back to Player.isOnMission.");
                }
            }

            if (!cachedMissionActive)
            {
                try
                {
                    cachedMissionActive = Player.isOnMission;
                    if (!missionStateSourceLogged)
                    {
                        missionStateSourceLogged = true;
                        Log("MISSION_GUARD: using direct Player.isOnMission.");
                    }
                }
                catch (Exception ex)
                {
                    if (!missionPropertyFailureLogged)
                    {
                        missionPropertyFailureLogged = true;
                        Log("MISSION_GUARD: direct Player.isOnMission failed (" + ex.GetType().Name + ").");
                    }
                    cachedMissionActive = false;
                }
            }


            return cachedMissionActive;
        }


        private bool IsServiceInteractionRelevantForMissionCheck()
        {
            if (menuOpen) return true;
            try
            {
                if (IsPlayerOnFoot())
                {


                    if (GetNearestDealer() != null) return true;
                    if (GetNearestRegistrationPoint() != null) return true;
                }
            }
            catch { }
            return false;
        }

        private void TrackLastPlayerVehicle()
        {
            try
            {
                if (Player.Character != null && Player.Character.Exists() && Player.Character.isSittingInVehicle())
                {
                    Vehicle v = Player.Character.CurrentVehicle;
                    if (IsValidVehicle(v))
                    {
                        lastPlayerVehicle = v;
                        lastPlayerVehicleHandle = TryGetVehicleHandle(v);
                    }
                }
            }
            catch { }
        }

        private bool IsScreenTransitioningForService()
        {
            try { if (Function.Call<bool>("IS_SCREEN_FADED_OUT")) return true; } catch { }
            try { if (Function.Call<bool>("IS_SCREEN_FADING")) return true; } catch { }
            try { if (Function.Call<bool>("IS_SCREEN_FADING_OUT")) return true; } catch { }
            try { if (Function.Call<bool>("IS_SCREEN_FADING_IN")) return true; } catch { }
            return false;
        }

        private bool CanOpenBaseInteraction(out string reason)
        {
            reason = "";
            if (!IsPlayerValid())
            {
                reason = "Service unavailable right now.";
                return false;
            }
            if (suspendServicesDuringMissions && IsMissionActiveFreshSafe())
            {
                reason = "Service unavailable during missions.";
                return false;
            }
            if (blockWhenPlayerControlDisabled)
            {
                try
                {
                    if (!Player.CanControlCharacter && IsScreenTransitioningForService())
                    {
                        reason = "Service unavailable right now.";
                        return false;
                    }
                }
                catch { }
            }
            return true;
        }

        private bool CanOpenDealerInteraction(out string reason)
        {
            if (!CanOpenBaseInteraction(out reason)) return false;
            if (requireNoWantedForDealers && Player.WantedLevel > 0)
            {
                reason = "Lose the wanted level first.";
                return false;
            }
            return true;
        }

        private bool CanShowBaseInteractionPrompt(out string reason)
        {
            reason = "";
            if (!IsPlayerValid())
            {
                reason = "Service unavailable right now.";
                return false;
            }
            if (suspendServicesDuringMissions && IsMissionActiveSafe(false))
            {
                reason = "Service unavailable during missions.";
                return false;
            }
            if (blockWhenPlayerControlDisabled)
            {
                try
                {
                    if (!Player.CanControlCharacter && IsScreenTransitioningForService())
                    {
                        reason = "Service unavailable right now.";
                        return false;
                    }
                }
                catch { }
            }
            return true;
        }

        private bool CanShowDealerInteractionPrompt(out string reason)
        {
            if (!CanShowBaseInteractionPrompt(out reason)) return false;
            if (requireNoWantedForDealers && Player.WantedLevel > 0)
            {
                reason = "Lose the wanted level first.";
                return false;
            }
            return true;
        }

        private void UpdateDealerWorldPrompts()
        {
            if (!IsPlayerOnFoot()) return;
            if (!showWorldPrompts) return;

            RefreshDealerPromptCandidateCache(false);
            if (dealerPromptCandidateExists && IsValidVehicle(dealerPromptCandidateVehicle))
            {
                string reason;
                if (!CanShowDealerInteractionPrompt(out reason)) DrawSmallPrompt(reason);
                else if (!dealerPromptCandidateSaleable) DrawSmallPrompt("This vehicle needs repairs and is not for sale.");
                else
                {
                    string model = GetDealerModelForSaleSlot(dealerPromptCandidateDealer, dealerPromptCandidateSlotIndex, dealerPromptCandidateVehicle);
                    DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " to buy " + GetVehicleDisplayName(model) + ".");
                }
                return;
            }

            DealerDefinition d = GetNearestDealer();
            if (d == null) return;
            if (d.DisplaySlots.Count > 0)
            {
                string reason;
                if (!CanShowDealerInteractionPrompt(out reason)) DrawSmallPrompt(reason);
                else DrawSmallPrompt("Walk up to a vehicle to buy it.");
                return;
            }

            string baseReason;
            if (!CanShowDealerInteractionPrompt(out baseReason)) DrawSmallPrompt(baseReason);
            else DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " to browse " + d.Name + ".");
        }

        private void UpdateRegistrationWorldPrompts()
        {
            if (GetNearestDealer() != null) return;
            RegistrationPoint p = GetNearestRegistrationPoint();
            if (p == null) return;
            if (!IsPlayerOnFoot()) return;
            Vehicle v = FindVehicleForRegistration(p);
            if (showWorldPrompts)
            {
                string reason;
                if (!CanShowBaseInteractionPrompt(out reason)) DrawSmallPrompt(reason);
                else if (IsValidVehicle(v))
                {
                    OwnedVehicleRecord owned = FindOwnedRecordForVehicle(v);
                    if (owned != null)
                    {
                        if (owned.Registered && !owned.Insured) DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " for insurance/registration services.");
                        else DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " for registration services.");
                    }
                    else
                    {
                        string registrationReason;
                        if (!CanRegisterVehicle(v, p, out registrationReason))
                        {
                            if (IsServiceVehicleForRegistration(v, p)) DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " for registration services.");
                            else if (CountCurrentEpisodeOwnedVehicles() > 0 && registrationReason.IndexOf("Registration limit", StringComparison.OrdinalIgnoreCase) >= 0) DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " to manage registrations.");
                            else DrawSmallPrompt(registrationReason);
                        }
                        else DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " to register the nearby vehicle.");
                    }
                }
                else
                {
                    if (IsStevieRegistrationPoint(p) && CountRecoverableOwnedVehicles() > 0) DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " to recover an insured vehicle.");
                    else if (CountCurrentEpisodeOwnedVehicles() > 0) DrawSmallPrompt("Press " + KeyLabel(keyOpen) + " for registration services.");
                    else DrawSmallPrompt("Bring a vehicle here for registration.");
                }
            }
        }

        private void OpenDealerMenu(DealerDefinition dealer)
        {
            string interactionReason;
            if (!CanOpenDealerInteraction(out interactionReason))
            {
                Notify(interactionReason);
                return;
            }
            if (dealer == null || dealer.Models.Count == 0)
            {
                Notify("No vehicles are available at this dealer.");
                return;
            }
            activeDealer = dealer;
            activeRegistrationPoint = null;
            activeRegistrationVehicle = null;
            activeRegistrationOwnedRecord = null;
            registrationInsuranceOnlyMode = false;
            menuMode = "dealer";
            menuOpen = true;
            HoldPauseMenuDisabledBriefly();
            dealerForcedSlotIndex = -1;
            selectedIndex = 0;
            dealerModelIndex = ClampIndex(dealerModelIndex, dealer.DisplaySlots.Count > 0 ? dealer.DisplaySlots.Count : dealer.Models.Count);
            dealerColourID = minColourID;
            dealerInsuranceSelected = true;
            ClearDealerPreview(true);
            ResetDealerExtrasCache();
            CreateOrRefreshDealerPreview();
            FocusCameraOnDealerPreview();
            if (blockPlayerControlDuringMenu) Player.CanControlCharacter = false;
            Log("Dealer menu opened: " + dealer.Id + ".");
        }

        private void OpenDealerSaleVehicleMenu(DealerDefinition dealer, int slotIndex, Vehicle saleVehicle)
        {
            string interactionReason;
            if (!CanOpenDealerInteraction(out interactionReason))
            {
                Notify(interactionReason);
                return;
            }
            if (dealer == null || dealer.Models.Count == 0)
            {
                Notify("No vehicles are available at this dealer.");
                return;
            }
            if (!IsValidVehicle(saleVehicle))
            {
                Notify("This vehicle is not available.");
                return;
            }
            if (!IsDealerVehicleHealthyEnoughForSale(saleVehicle))
            {
                Notify("This vehicle needs repairs and is not for sale.");
                return;
            }
            activeDealer = dealer;
            activeRegistrationPoint = null;
            activeRegistrationVehicle = null;
            activeRegistrationModelName = "";
            activeRegistrationOwnedRecord = null;
            registrationInsuranceOnlyMode = false;
            menuMode = "dealer";
            menuOpen = true;
            HoldPauseMenuDisabledBriefly();
            selectedIndex = 1;
            dealerModelIndex = slotIndex < 0 ? 0 : slotIndex;
            dealerForcedSlotIndex = dealerModelIndex;
            string slotKey = GetDealerSlotKey(dealer, dealerModelIndex);
            if (IsValidVehicle(saleVehicle))
            {
                dealerColourID = GetDealerPrimaryColour(saleVehicle, GetOrCreateDealerSlotColour(slotKey));
                if (slotKey.Length > 0) dealerSlotColours[slotKey] = dealerColourID;
            }
            else dealerColourID = GetOrCreateDealerSlotColour(slotKey);
            GetOrCreateDealerSlotPriceFactor(slotKey);
            dealerInsuranceSelected = true;
            ResetDealerExtrasCache();
            dealerPreviewVehicle = saleVehicle;
            dealerPreviewModel = GetDealerModelForSaleSlot(dealer, dealerModelIndex, saleVehicle);
            dealerPreviewHandle = TryGetVehicleHandle(saleVehicle);
            bool forcedAmbient = false;
            dealerSlotAmbient.TryGetValue(slotKey, out forcedAmbient);
            dealerPreviewIsAmbient = forcedAmbient;
            BuildDealerExtrasFromPreview();
            if (blockPlayerControlDuringMenu) Player.CanControlCharacter = false;
            Log("Dealer vehicle menu opened. Dealer=" + dealer.Id + ", slot=" + (dealerModelIndex + 1).ToString() + ", model=" + dealerPreviewModel + ".");
        }

        private void OpenRegistrationMenu(RegistrationPoint point)
        {
            string interactionReason;
            if (!CanOpenBaseInteraction(out interactionReason))
            {
                Notify(interactionReason);
                return;
            }
            activeRegistrationPoint = point;
            activeDealer = null;
            activeRegistrationVehicle = FindVehicleForRegistration(point);
            activeRegistrationModelName = "";
            activeRegistrationOwnedRecord = null;
            registrationInsuranceOnlyMode = false;

            if (IsValidVehicle(activeRegistrationVehicle))
            {
                activeRegistrationModelName = GetModelNameForVehicle(activeRegistrationVehicle);
                activeRegistrationOwnedRecord = FindOwnedRecordForVehicle(activeRegistrationVehicle);
                if (activeRegistrationOwnedRecord != null && activeRegistrationOwnedRecord.Registered && !activeRegistrationOwnedRecord.Insured)
                {
                    registrationInsuranceOnlyMode = true;
                }
            }

            menuMode = "registration";
            menuOpen = true;
            HoldPauseMenuDisabledBriefly();
            selectedIndex = 0;
            streetInsuranceSelected = true;
            customPlateTarget = null;
            customPlateInput = "";
            customPlateFieldIndex = 1;
            if (blockPlayerControlDuringMenu) Player.CanControlCharacter = false;

            if (IsValidVehicle(activeRegistrationVehicle))
            {
                try { Function.Call("SET_CAR_FORWARD_SPEED", activeRegistrationVehicle, 0.0f); } catch { }
            }
            Log("Registration menu opened: " + (point == null ? "unknown" : point.Id) + ", Vehicle=" + (IsValidVehicle(activeRegistrationVehicle) ? activeRegistrationModelName : "none") + ".");
        }

        private void HandleMenuKey(Keys key)
        {
            if (key == Keys.Escape || (keyExit != keyBack && key == keyExit))
            {
                HoldPauseMenuDisabledBriefly();
                BackOutOfCurrentMenu();
                return;
            }

            if (key == keyBack)
            {
                if (menuMode == "plate")
                {
                    HandleCustomPlateInputKey(key);
                    return;
                }
                return;
            }
            if (menuMode == "dealer") HandleDealerMenuKey(key);
            else if (menuMode == "registration") HandleRegistrationMenuKey(key);
            else if (menuMode == "deregister") HandleDeregisterMenuKey(key);
            else if (menuMode == "deliver") HandleDeliverMenuKey(key);
            else if (menuMode == "plate") HandleCustomPlateInputKey(key);
        }

        private void BackOutOfCurrentMenu()
        {
            if (menuMode == "dealer" && dealerColourPaletteMode)
            {
                dealerColourPaletteMode = false;
                selectedIndex = 1;
                return;
            }
            if (menuMode == "deregister" || menuMode == "deliver" || menuMode == "plate")
            {
                menuMode = "registration";
                selectedIndex = 0;
                customPlateFieldIndex = 1;
                return;
            }
            CloseMenu(false);
        }

        private void HandleDealerMenuKey(Keys key)
        {
            if (dealerColourPaletteMode)
            {
                if (key == keyUp) { ChangeDealerColour(-Math.Max(4, dealerColourPaletteColumns)); return; }
                if (key == keyDown) { ChangeDealerColour(Math.Max(4, dealerColourPaletteColumns)); return; }
                if (key == keyLeft) { ChangeDealerColour(-1); return; }
                if (key == keyRight) { ChangeDealerColour(1); return; }
                if (key == keySelect) { dealerColourPaletteMode = false; return; }
            }

            int count = GetDealerMenuCount();
            if (key == keyUp)
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = count - 1;
                return;
            }
            if (key == keyDown)
            {
                selectedIndex++;
                if (selectedIndex >= count) selectedIndex = 0;
                return;
            }
            if (key == keyLeft || key == keyRight)
            {
                int delta = key == keyRight ? 1 : -1;
                if (selectedIndex == 0)
                {
                    if (dealerForcedSlotIndex < 0) ChangeDealerModel(delta);
                    return;
                }
                if (selectedIndex == 1)
                {
                    ChangeDealerColour(delta);
                    return;
                }
                if (selectedIndex == 2)
                {
                    dealerInsuranceSelected = !dealerInsuranceSelected;
                    return;
                }
                int extraStart = 3;
                int extraIndex = selectedIndex - extraStart;
                if (extraIndex >= 0 && extraIndex < dealerAvailableExtras.Count)
                {
                    int extra = dealerAvailableExtras[extraIndex];
                    bool current = false;
                    dealerExtraStates.TryGetValue(extra, out current);
                    dealerExtraStates[extra] = !current;
                    ApplyDealerExtraStates();
                    return;
                }
            }
            if (key == keySelect)
            {
                if (selectedIndex == 1 && showDealerColourPalette)
                {
                    dealerColourPaletteMode = true;
                    selectedIndex = 1;
                    return;
                }
                int buyIndex = GetDealerBuyIndex();
                if (selectedIndex == buyIndex)
                {
                    ConfirmDealerPurchase();
                    return;
                }
                if (selectedIndex == buyIndex + 1)
                {
                    CloseMenu(false);
                    return;
                }
            }
        }

        private void HandleRegistrationMenuKey(Keys key)
        {
            List<string> actions = BuildRegistrationActions();
            int count = Math.Max(1, actions.Count);
            if (selectedIndex >= count) selectedIndex = count - 1;
            if (selectedIndex < 0) selectedIndex = 0;

            if (key == keyUp)
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = count - 1;
                return;
            }
            if (key == keyDown)
            {
                selectedIndex++;
                if (selectedIndex >= count) selectedIndex = 0;
                return;
            }

            string action = selectedIndex >= 0 && selectedIndex < actions.Count ? actions[selectedIndex] : "close";
            if ((key == keyLeft || key == keyRight) && action == "toggle_insurance")
            {
                streetInsuranceSelected = !streetInsuranceSelected;
                return;
            }
            if (key == keySelect)
            {
                if (action == "toggle_insurance")
                {
                    streetInsuranceSelected = !streetInsuranceSelected;
                    return;
                }
                if (action == "register" || action == "insure")
                {
                    ConfirmStreetRegistration();
                    return;
                }
                if (action == "recover")
                {
                    TryRecoverDestroyedVehicle();
                    return;
                }
                if (action == "deliver")
                {
                    OpenDeliverMenu();
                    return;
                }
                if (action == "plate_current")
                {
                    OwnedVehicleRecord target = activeRegistrationOwnedRecord == null ? FindOwnedRecordForVehicle(activeRegistrationVehicle) : activeRegistrationOwnedRecord;
                    OpenCustomPlateInput(target);
                    return;
                }
                if (action == "plate_unavailable")
                {
                    Notify("Custom plates require LibertyCityPlates and the LVSCE LCP bridge.");
                    return;
                }
                if (action == "deregister")
                {
                    OpenDeregisterMenu();
                    return;
                }
                if (action == "close")
                {
                    CloseMenu(false);
                    return;
                }
            }
        }

        private void HandleDeregisterMenuKey(Keys key)
        {
            List<OwnedVehicleRecord> list = GetCurrentEpisodeOwnedVehicleList(true);
            int count = list.Count + 1;
            if (selectedIndex >= count) selectedIndex = count - 1;
            if (selectedIndex < 0) selectedIndex = 0;

            if (key == keyUp)
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = count - 1;
                return;
            }
            if (key == keyDown)
            {
                selectedIndex++;
                if (selectedIndex >= count) selectedIndex = 0;
                return;
            }
            if (key == keySelect)
            {
                if (selectedIndex >= 0 && selectedIndex < list.Count)
                {
                    CancelOwnedVehicleRegistration(list[selectedIndex]);
                    menuMode = "registration";
                    selectedIndex = 0;
                    return;
                }
                menuMode = "registration";
                selectedIndex = 0;
                return;
            }
        }

        private void HandleDeliverMenuKey(Keys key)
        {
            List<OwnedVehicleRecord> list = GetStevieDeliverableOwnedVehicleList();
            int count = list.Count + 1;
            if (selectedIndex >= count) selectedIndex = count - 1;
            if (selectedIndex < 0) selectedIndex = 0;

            if (key == keyUp)
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = count - 1;
                return;
            }
            if (key == keyDown)
            {
                selectedIndex++;
                if (selectedIndex >= count) selectedIndex = 0;
                return;
            }
            if (key == keySelect)
            {
                if (selectedIndex >= 0 && selectedIndex < list.Count)
                {
                    TryDeliverOwnedVehicleAtStevie(list[selectedIndex]);
                    menuMode = "registration";
                    selectedIndex = 0;
                    return;
                }
                menuMode = "registration";
                selectedIndex = 0;
                return;
            }
        }

        private void HandleCustomPlateInputKey(Keys key)
        {
            if (key == keyUp)
            {
                customPlateFieldIndex--;
                if (customPlateFieldIndex < 0) customPlateFieldIndex = 1;
                return;
            }
            if (key == keyDown)
            {
                customPlateFieldIndex++;
                if (customPlateFieldIndex > 1) customPlateFieldIndex = 0;
                return;
            }
            if (key == keyLeft)
            {
                if (customPlateFieldIndex == 0) CycleCustomPlateDefinition(-1);
                return;
            }
            if (key == keyRight)
            {
                if (customPlateFieldIndex == 0) CycleCustomPlateDefinition(1);
                return;
            }
            if (key == keySelect)
            {
                if (customPlateFieldIndex == 0)
                {
                    customPlateFieldIndex = 1;
                    return;
                }
                ConfirmCustomPlateOrder();
                return;
            }
            if (key == keyBack)
            {
                if (customPlateInput != null && customPlateInput.Length > 0)
                {
                    customPlateInput = customPlateInput.Substring(0, customPlateInput.Length - 1);
                }
                customPlateFieldIndex = 1;
                return;
            }
            if (key == Keys.Delete)
            {
                customPlateInput = "";
                customPlateFieldIndex = 1;
                return;
            }

            char c = PlateCharFromKey(key);
            if (c != '\0')
            {
                customPlateFieldIndex = 1;
                int maxLen = GetCustomPlateMaxLength();
                LcpPlateDefinition def = GetSelectedCustomPlateDefinition();
                if (def != null) maxLen = Math.Max(1, Math.Min(CustomPlateAbsoluteMaxLength, CountInputSlotsForFormat(def.Format)));
                if (customPlateInput == null) customPlateInput = "";
                if (customPlateInput.Length < maxLen) customPlateInput += c;
                return;
            }
        }

        private char PlateCharFromKey(Keys key)
        {
            if (key >= Keys.A && key <= Keys.Z) return (char)('A' + (int)(key - Keys.A));
            if (key >= Keys.D0 && key <= Keys.D9) return (char)('0' + (int)(key - Keys.D0));
            if (key >= Keys.NumPad0 && key <= Keys.NumPad9) return (char)('0' + (int)(key - Keys.NumPad0));
            return '\0';
        }

        private List<string> BuildRegistrationActions()
        {
            List<string> actions = new List<string>();
            OwnedVehicleRecord owned = activeRegistrationOwnedRecord;
            if (owned == null && IsValidVehicle(activeRegistrationVehicle)) owned = FindOwnedRecordForVehicle(activeRegistrationVehicle);

            if (IsValidVehicle(activeRegistrationVehicle))
            {
                if (owned == null)
                {
                    if (IsServiceVehicleForRegistration(activeRegistrationVehicle, activeRegistrationPoint))
                    {
                        actions.Add("service_blocked");
                    }
                    else
                    {
                        actions.Add("toggle_insurance");
                        actions.Add("register");
                    }
                }
                else
                {
                    if (owned.Registered && !owned.Insured) actions.Add("insure");
                    if (owned.Registered) actions.Add(IsCustomPlateServiceProbablyAvailable() ? "plate_current" : "plate_unavailable");
                }
            }

            if (IsStevieRegistrationPoint(activeRegistrationPoint))
            {
                actions.Add("recover");
                if (CountStevieDeliverableOwnedVehicles() > 0) actions.Add("deliver");
            }
            if (CountCurrentEpisodeOwnedVehicles() > 0) actions.Add("deregister");
            actions.Add("close");
            return actions;
        }

        private bool IsStevieRegistrationPoint(RegistrationPoint point)
        {
            if (point == null || point.Id == null) return false;
            string id = point.Id.Trim().ToLowerInvariant();
            return id == "sm_auto_sales" || id == "stevie" || id == "stevie_garage" || id == "s_m_auto_sales";
        }

        private bool CanAddOwnedVehicle(out string reason)
        {
            reason = "";
            if (!ownershipEnabled)
            {
                reason = "Vehicle ownership is disabled.";
                return false;
            }
            if (maxOwnedCarsPerEpisode <= 0) return true;
            int count = CountCurrentEpisodeOwnedVehicles();
            if (count >= maxOwnedCarsPerEpisode)
            {
                reason = "Registration limit reached for this episode (" + maxOwnedCarsPerEpisode.ToString() + "). Cancel an old registration first.";
                return false;
            }
            return true;
        }

        private List<OwnedVehicleRecord> GetCurrentEpisodeOwnedVehicleList(bool includeDestroyed)
        {
            List<OwnedVehicleRecord> list = new List<OwnedVehicleRecord>();
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null) continue;
                if (!OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (!includeDestroyed && rec.Destroyed) continue;
                list.Add(rec);
            }
            return list;
        }

        private OwnedVehicleRecord FindFirstRecoverableOwnedVehicle()
        {
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null) continue;
                if (!OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (rec.Registered && rec.Insured && rec.Destroyed) return rec;
            }
            return null;
        }

        private int CountRecoverableOwnedVehicles()
        {
            int count = 0;
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null) continue;
                if (!OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (rec.Registered && rec.Insured && rec.Destroyed) count++;
            }
            return count;
        }

        private List<OwnedVehicleRecord> GetStevieDeliverableOwnedVehicleList()
        {
            List<OwnedVehicleRecord> list = new List<OwnedVehicleRecord>();
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null) continue;
                if (!OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (!rec.Registered) continue;
                if (rec.Destroyed) continue;
                list.Add(rec);
            }
            return list;
        }

        private int CountStevieDeliverableOwnedVehicles()
        {
            int count = 0;
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null) continue;
                if (!OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (!rec.Registered) continue;
                if (rec.Destroyed) continue;
                count++;
            }
            return count;
        }

        private void OpenDeliverMenu()
        {
            menuMode = "deliver";
            selectedIndex = 0;
        }

        private void OpenDeregisterMenu()
        {
            menuMode = "deregister";
            selectedIndex = 0;
        }

        private void CancelOwnedVehicleRegistration(OwnedVehicleRecord rec)
        {
            if (rec == null)
            {
                Notify("No registered vehicle selected.");
                return;
            }
            string name = GetVehicleDisplayName(rec.ModelName);
            if (rec.BlipRef != null)
            {
                RemoveBlipSafe(rec.BlipRef, "owned vehicle blip " + rec.Id);
                rec.BlipRef = null;
                rec.BlipIsFallback = false;
            }
            if (activeRegistrationOwnedRecord == rec) activeRegistrationOwnedRecord = null;
            if (customPlateTarget == rec) customPlateTarget = null;
            ownedVehicles.Remove(rec);
            ownedVehiclesDirty = true;
            SaveOwnedVehicles();
            Notify(name + " registration cancelled.");
            Log("Registration cancelled. Vehicle=" + rec.Id + ", Model=" + rec.ModelName + ", Episode=" + GetCurrentEpisodeKey() + ".");
        }

        private int GetCustomPlateMaxLength()
        {
            return Math.Max(1, Math.Min(CustomPlateAbsoluteMaxLength, customPlateMaxLength));
        }

        private void OpenCustomPlateInput(OwnedVehicleRecord rec)
        {
            if (rec == null || !rec.Registered || !OwnedRecordBelongsToCurrentEpisode(rec))
            {
                Notify("No registered vehicle selected.");
                return;
            }
            string reason;
            if (!CanUseCustomPlateService(out reason))
            {
                Notify(reason);
                return;
            }
            EnsureLcpPlateDefinitionsLoaded();
            customPlateTarget = rec;
            customPlateChoices = GetAllowedLcpPlateDefinitionsForModel(rec.ModelName);
            customPlateDefinitionIndex = 0;
            customPlateFieldIndex = 1;
            if (customPlateChoices.Count > 0 && rec.LcpPlateId >= 0)
            {
                for (int i = 0; i < customPlateChoices.Count; i++)
                {
                    if (customPlateChoices[i] != null && customPlateChoices[i].Id == rec.LcpPlateId)
                    {
                        customPlateDefinitionIndex = i;
                        break;
                    }
                }
            }
            customPlateInput = CleanPlateRawInput(rec.LcpRawInput);
            if (customPlateInput.Length == 0) customPlateInput = CleanPlateRawInput(rec.PlateText);
            LcpPlateDefinition selected = GetSelectedCustomPlateDefinition();
            int maxLen = selected == null ? GetCustomPlateMaxLength() : CountInputSlotsForFormat(selected.Format);
            maxLen = Math.Max(1, Math.Min(CustomPlateAbsoluteMaxLength, maxLen));
            if (customPlateInput.Length > maxLen) customPlateInput = customPlateInput.Substring(0, maxLen);
            menuMode = "plate";
            selectedIndex = 0;
        }

        private bool IsCustomPlateServiceProbablyAvailable()
        {
            if (!libertyCityPlatesEnabled || !useLibertyCityPlatesBridge) return false;
            try
            {
                bool lcpFile = File.Exists("plugins\\LibertyCityPlates.asi") || File.Exists("LibertyCityPlates.asi") || File.Exists("update\\pc\\textures\\libertycityplates.wtd");
                if (!lcpFile) return false;
                if (!File.Exists("plugins\\000_LVSCE_LCP_Bridge.asi")) return false;
            }
            catch { return false; }
            return true;
        }

        private bool CanUseCustomPlateService(out string reason)
        {
            reason = "";
            if (!libertyCityPlatesEnabled)
            {
                reason = "Custom plates require LibertyCityPlates.";
                return false;
            }
            if (!useLibertyCityPlatesBridge)
            {
                reason = "Custom plates require the LVSCE LCP bridge.";
                return false;
            }
            if (!DetectLibertyCityPlates(true))
            {
                reason = "LibertyCityPlates is not detected.";
                return false;
            }
            if (!DetectLibertyCityPlatesBridge(true))
            {
                reason = "LCP bridge is not available.";
                return false;
            }
            return true;
        }

        private int GetCustomPlatePrice(OwnedVehicleRecord rec)
        {
            if (customPlatePrice < 0) return 0;
            return customPlatePrice;
        }

        private void ConfirmCustomPlateOrder()
        {
            OwnedVehicleRecord rec = customPlateTarget;
            if (rec == null || !rec.Registered || !OwnedRecordBelongsToCurrentEpisode(rec))
            {
                Notify("No registered vehicle selected.");
                menuMode = "registration";
                selectedIndex = 0;
                return;
            }
            string reason;
            if (!CanUseCustomPlateService(out reason))
            {
                Notify(reason);
                return;
            }
            LcpPlateDefinition def = GetSelectedCustomPlateDefinition();
            if (def == null)
            {
                Notify("No compatible LCP plate type is available for this vehicle.");
                return;
            }
            LcpPlatePreview preview = BuildLcpPlatePreview(def, customPlateInput, false);
            if (preview == null || !preview.Complete)
            {
                string missing = preview == null ? "more compatible characters" : preview.MissingText;
                Notify("Plate does not match " + def.Name + ": " + missing + ".");
                return;
            }
            if (preview.Canonical == null || preview.Canonical.Length < 2)
            {
                Notify("Plate must use at least two letters or numbers.");
                return;
            }
            if (IsPlateUsedByAnotherOwnedVehicle(preview.Canonical, def.Id, rec))
            {
                Notify("This plate is already assigned to another registered vehicle.");
                return;
            }
            if (CleanPlateText(rec.PlateText) == preview.Canonical && rec.LcpPlateId == def.Id)
            {
                Notify("This vehicle already has that plate.");
                menuMode = "registration";
                selectedIndex = 0;
                return;
            }
            int price = GetCustomPlatePrice(rec);
            if (!TryCharge(price)) return;
            rec.PlateText = preview.Canonical;
            rec.LcpPlateId = def.Id;
            rec.LcpPlateFormat = def.Format;
            rec.LcpPlateName = def.Name;
            rec.LcpRawInput = preview.RawInput;
            rec.PlateGeneratedThisSession = false;
            ScheduleOwnedVehiclePlateApplication(rec, false);
            if (IsValidVehicle(rec.VehicleRef)) TryApplyOwnedVehiclePlate(rec, rec.VehicleRef, false);
            SaveOwnedVehicles();
            Notify("Custom plate " + preview.Display + " ordered. Charged $" + price.ToString() + ".");
            Log("Custom plate ordered. Vehicle=" + rec.Id + ", Model=" + rec.ModelName + ", PlateType=" + def.Id.ToString() + ", Plate=" + preview.Canonical + ", Charged=$" + price.ToString() + ".");
            menuMode = "registration";
            selectedIndex = 0;
        }

        private bool IsPlateUsedByAnotherOwnedVehicle(string plate, OwnedVehicleRecord self)
        {
            return IsPlateUsedByAnotherOwnedVehicle(plate, -1, self);
        }

        private bool IsPlateUsedByAnotherOwnedVehicle(string plate, int lcpPlateId, OwnedVehicleRecord self)
        {
            string clean = CleanPlateText(plate);
            if (clean.Length == 0) return false;
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null || rec == self) continue;
                if (!OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (CleanPlateText(rec.PlateText) != clean) continue;
                if (lcpPlateId < 0 || rec.LcpPlateId == lcpPlateId) return true;
            }
            return false;
        }

        private int GetDealerMenuCount()
        {
            return 3 + dealerAvailableExtras.Count + 2;
        }

        private int GetDealerBuyIndex()
        {
            return 3 + dealerAvailableExtras.Count;
        }

        private void ChangeDealerModel(int delta)
        {
            if (activeDealer == null) return;
            if (dealerForcedSlotIndex >= 0) return;
            int count = activeDealer.DisplaySlots.Count > 0 ? activeDealer.DisplaySlots.Count : activeDealer.Models.Count;
            if (count == 0) return;
            dealerModelIndex += delta;
            while (dealerModelIndex < 0) dealerModelIndex += count;
            while (dealerModelIndex >= count) dealerModelIndex -= count;
            dealerColourID = minColourID;
            if (activeDealer.DisplaySlots.Count > 0)
            {
                dealerPreviewVehicle = null;
                dealerPreviewModel = "";
                dealerPreviewHandle = 0;
                dealerPreviewIsAmbient = false;
                dealerAvailableExtras.Clear();
                dealerExtraStates.Clear();
                ResetDealerExtrasCache();
                CreateOrRefreshDealerPreview();
                FocusCameraOnDealerPreview();
                return;
            }
            ClearDealerPreview(true);
            CreateOrRefreshDealerPreview();
        }

        private void ChangeDealerColour(int delta)
        {
            int count = Math.Max(1, maxColourID - minColourID + 1);
            int offset = dealerColourID - minColourID;
            offset += delta;
            while (offset < 0) offset += count;
            while (offset >= count) offset -= count;
            dealerColourID = minColourID + offset;
            if (activeDealer != null && dealerForcedSlotIndex >= 0)
            {
                string slotKey = GetDealerSlotKey(activeDealer, dealerForcedSlotIndex);
                if (slotKey.Length > 0) dealerSlotColours[slotKey] = dealerColourID;
            }
            if (IsValidVehicle(dealerPreviewVehicle))
            {
                try
                {
                    dealerPreviewVehicle.Color = new ColorIndex(dealerColourID);
                    dealerPreviewVehicle.SpecularColor = new ColorIndex(dealerColourID);
                }
                catch { }
            }
        }

        private void ConfirmDealerPurchase()
        {
            if (activeDealer == null) return;
            if (!IsValidVehicle(dealerPreviewVehicle))
            {
                Notify("Selected vehicle is not available.");
                CreateOrRefreshDealerPreview();
                return;
            }
            string model = GetCurrentDealerModel();
            string limitReason;
            if (!CanAddOwnedVehicle(out limitReason))
            {
                Notify(limitReason);
                return;
            }
            int vehiclePrice = GetDealerVehiclePrice(model, activeDealer);
            int insurancePrice = dealerInsuranceSelected ? GetInsurancePrice(model, true) : 0;
            int total = vehiclePrice + insurancePrice;

            DealerDeliveryChoice deliveryChoice = null;
            DealerDeliverySlot deliverySlot = null;
            Vehicle deliveredVehicle = null;
            bool showroomDelivery = activeDealer.ShowroomDelivery && activeDealer.DisplaySlots.Count > 0;
            string purchasedSlotKey = activeDealer.DisplaySlots.Count > 0 ? GetDealerSlotKey(activeDealer, dealerForcedSlotIndex >= 0 ? dealerForcedSlotIndex : dealerModelIndex) : "";
            if (showroomDelivery)
            {
                deliveryChoice = FindFreeDealerDeliverySlot(activeDealer);
                deliverySlot = deliveryChoice == null ? null : deliveryChoice.Slot;
                if (deliverySlot == null)
                {
                    Notify("Delivery area is blocked. Clear the delivery point first.");
                    Log("Dealer purchase blocked: no free delivery slot. Dealer=" + activeDealer.Id + ", Model=" + model + ".");
                    return;
                }

                ApplyDealerExtraStates();
                deliveredVehicle = CreateDealerPurchaseSpawnVehicle(model, activeDealer, deliverySlot);
                if (!IsValidVehicle(deliveredVehicle))
                {
                    Notify("Could not deliver this vehicle.");
                    Log("Dealer purchase delivery spawn failed. Dealer=" + activeDealer.Id + ", Model=" + model + ".");
                    return;
                }
            }

            if (!TryCharge(total))
            {
                if (IsValidVehicle(deliveredVehicle)) DeleteVehicleSafe(deliveredVehicle, "unpaid dealer delivery");
                return;
            }
            if (showroomDelivery && deliveryChoice != null) ReserveDealerDeliverySlot(deliveryChoice.Key);

            Vehicle purchaseVehicle = showroomDelivery ? deliveredVehicle : dealerPreviewVehicle;
            if (!showroomDelivery) ApplyDealerExtraStates();

            try { Function.Call("FREEZE_CAR_POSITION", purchaseVehicle, false); } catch { }
            try { Function.Call("LOCK_CAR_DOORS", purchaseVehicle, 0); } catch { }

            OwnedVehicleRecord rec = RegisterOwnedVehicle(purchaseVehicle, model, activeDealer.Id, true, dealerInsuranceSelected, total, insurancePrice);
            if (rec != null)
            {
                Notify(GetVehicleDisplayName(model) + " purchased and registered. Charged $" + total.ToString() + ".");
                Log("Dealer purchase complete. Dealer=" + activeDealer.Id + ", Model=" + model + ", Total=$" + total.ToString() + ", Insured=" + dealerInsuranceSelected.ToString() + ", ShowroomDelivery=" + showroomDelivery.ToString() + ".");

                if (purchasedSlotKey.Length > 0)
                {
                    if (showroomDelivery)
                    {
                        HoldDealerSaleSlotEmptyUntilAreaExit(purchasedSlotKey, activeDealer, dealerPreviewVehicle);
                        RemoveDealerSaleSlotByKey(purchasedSlotKey, false);
                    }
                    else
                    {
                        RemoveDealerSaleSlotByKey(purchasedSlotKey, true);
                        dealerSlotRespawnCooldown[purchasedSlotKey] = Math.Max(1800, dealerSaleSlotRespawnTicks);
                    }
                }
                else RemoveDealerSaleSlotReferenceForVehicle(dealerPreviewVehicle);

                if (showroomDelivery && IsValidVehicle(dealerPreviewVehicle))
                {


                    MarkVehicleAsMission(dealerPreviewVehicle);
                    DeleteVehicleSafe(dealerPreviewVehicle, "purchased showroom display car");
                }
                dealerPreviewVehicle = null;
                CloseMenu(true);
            }
            else
            {
                if (IsValidVehicle(deliveredVehicle)) DeleteVehicleSafe(deliveredVehicle, "unregistered dealer delivery");
            }
        }

        private void ConfirmStreetRegistration()
        {
            if (!IsValidVehicle(activeRegistrationVehicle) || activeRegistrationPoint == null)
            {
                Notify("Registration vehicle is not available.");
                return;
            }
            string model = activeRegistrationModelName.Length > 0 ? activeRegistrationModelName : GetModelNameForVehicle(activeRegistrationVehicle);
            if (registrationInsuranceOnlyMode)
            {
                OwnedVehicleRecord existing = activeRegistrationOwnedRecord == null ? FindOwnedRecordForVehicle(activeRegistrationVehicle) : activeRegistrationOwnedRecord;
                if (existing == null || !existing.Registered)
                {
                    Notify("Registered vehicle record was not found.");
                    return;
                }
                if (existing.Insured)
                {
                    Notify("This vehicle is already insured.");
                    CloseMenu(false);
                    return;
                }
                int insuranceOnlyPrice = GetInsurancePrice(existing.ModelName == null || existing.ModelName.Length == 0 ? model : existing.ModelName, false);
                if (!TryCharge(insuranceOnlyPrice)) return;
                existing.Insured = true;
                existing.InsurancePrice = insuranceOnlyPrice;
                CaptureVehicleState(activeRegistrationVehicle, existing);
                SaveOwnedVehicles();
                Notify(GetVehicleDisplayName(existing.ModelName) + " insured. Charged $" + insuranceOnlyPrice.ToString() + ".");
                Log("Street insurance complete. Point=" + activeRegistrationPoint.Id + ", Vehicle=" + existing.Id + ", Model=" + existing.ModelName + ", Charged=$" + insuranceOnlyPrice.ToString() + ".");
                CloseMenu(true);
                return;
            }
            string reason;
            if (!CanRegisterVehicle(activeRegistrationVehicle, activeRegistrationPoint, out reason))
            {
                Notify(reason);
                return;
            }
            string limitReason;
            if (!CanAddOwnedVehicle(out limitReason))
            {
                Notify(limitReason);
                return;
            }
            int regPrice = GetStreetRegistrationPrice(activeRegistrationVehicle, activeRegistrationPoint);
            int insurancePrice = streetInsuranceSelected ? GetInsurancePrice(model, false) : 0;
            int total = regPrice + insurancePrice;
            if (!TryCharge(total)) return;

            OwnedVehicleRecord rec = RegisterOwnedVehicle(activeRegistrationVehicle, model, activeRegistrationPoint.Id, true, streetInsuranceSelected, total, insurancePrice);
            if (rec != null)
            {
                Notify(GetVehicleDisplayName(model) + " registered. Charged $" + total.ToString() + ".");
                Log("Street registration complete. Point=" + activeRegistrationPoint.Id + ", Model=" + model + ", Total=$" + total.ToString() + ", Insured=" + streetInsuranceSelected.ToString() + ".");
                CloseMenu(true);
            }
        }

        private void TryRecoverDestroyedVehicle()
        {
            if (!IsStevieRegistrationPoint(activeRegistrationPoint))
            {
                Notify("Insured recovery is handled by Stevie only.");
                return;
            }
            OwnedVehicleRecord rec = FindFirstRecoverableOwnedVehicle();
            if (rec == null)
            {
                Notify("No destroyed insured vehicle found.");
                return;
            }
            int fee = GetRecoveryFee(rec);
            if (!TryCharge(fee)) return;

            if (IsValidVehicle(rec.VehicleRef))
            {
                DeleteVehicleSafe(rec.VehicleRef, "recovered destroyed owned vehicle");
                rec.VehicleRef = null;
                rec.WorldHandle = 0;
            }
            if (rec.BlipRef != null)
            {
                RemoveBlipSafe(rec.BlipRef, "owned vehicle blip " + rec.Id);
                rec.BlipRef = null;
                rec.BlipIsFallback = false;
            }
            rec.X = StevieRecoveryX;
            rec.Y = StevieRecoveryY;
            rec.Z = StevieRecoveryZ;
            rec.Heading = StevieRecoveryHeading;
            rec.Health = 1000;
            rec.EngineHealth = 1000;
            rec.PetrolTankHealth = 1000;
            rec.Destroyed = false;

            TryClearRecoverySpawnPoint(rec);
            SpawnOwnedVehicle(rec, false);
            SaveOwnedVehicles();
            Notify("Recovered " + GetVehicleDisplayName(rec.ModelName) + " at Stevie's yard. Charged $" + fee.ToString() + ".");
            Log("Recovered insured vehicle at Stevie. Vehicle=" + rec.Id + ", Model=" + rec.ModelName + ", Fee=$" + fee.ToString() + ", Spawn=" + StevieRecoveryX.ToString("0.000", CultureInfo.InvariantCulture) + "," + StevieRecoveryY.ToString("0.000", CultureInfo.InvariantCulture) + "," + StevieRecoveryZ.ToString("0.000", CultureInfo.InvariantCulture) + "," + StevieRecoveryHeading.ToString("0.000", CultureInfo.InvariantCulture) + ".");
        }

        private void TryDeliverOwnedVehicleAtStevie(OwnedVehicleRecord rec)
        {
            if (!IsStevieRegistrationPoint(activeRegistrationPoint))
            {
                Notify("Vehicle delivery is handled by Stevie only.");
                return;
            }
            if (rec == null || !OwnedRecordBelongsToCurrentEpisode(rec) || !rec.Registered)
            {
                Notify("No registered vehicle selected.");
                return;
            }
            if (rec.Destroyed)
            {
                Notify("Destroyed vehicles require insured recovery.");
                return;
            }
            int fee = GetStevieDeliveryFee(rec);
            if (!TryCharge(fee)) return;

            TryClearRecoverySpawnPoint(rec);
            Vehicle v = rec.VehicleRef;
            bool spawned = false;
            if (!IsValidVehicle(v))
            {
                rec.X = StevieRecoveryX;
                rec.Y = StevieRecoveryY;
                rec.Z = StevieRecoveryZ;
                rec.Heading = StevieRecoveryHeading;
                SpawnOwnedVehicle(rec, false);
                v = rec.VehicleRef;
                spawned = true;
            }
            if (IsValidVehicle(v))
            {
                try { Function.Call("SET_CAR_COORDINATES", v, StevieRecoveryX, StevieRecoveryY, StevieRecoveryZ + 0.75f); } catch { }
                try { Function.Call("SET_CAR_HEADING", v, StevieRecoveryHeading); } catch { }
                PlaceVehicleOnGroundSafe(v, StevieRecoveryX, StevieRecoveryY, StevieRecoveryZ, StevieRecoveryHeading);
                rec.X = StevieRecoveryX;
                rec.Y = StevieRecoveryY;
                rec.Z = StevieRecoveryZ;
                rec.Heading = StevieRecoveryHeading;
                rec.VehicleRef = v;
                rec.WorldHandle = TryGetVehicleHandle(v);
                CaptureVehicleState(v, rec);
                if (rec.BlipRef != null)
                {
                    RemoveBlipSafe(rec.BlipRef, "owned vehicle blip " + rec.Id);
                    rec.BlipRef = null;
                    rec.BlipIsFallback = false;
                }
                FillOwnedVehicleFuel(rec, v);
                if (reapplyPlateOnOwnedVehicleSpawn && spawned) ScheduleOwnedVehiclePlateApplication(rec, true);
            }
            else
            {

                rec.X = StevieRecoveryX;
                rec.Y = StevieRecoveryY;
                rec.Z = StevieRecoveryZ;
                rec.Heading = StevieRecoveryHeading;
                FillOwnedVehicleFuel(rec, null);
            }

            rec.Destroyed = false;
            SaveOwnedVehicles();
            Notify("Delivered " + GetVehicleDisplayName(rec.ModelName) + " to Stevie's yard. Charged $" + fee.ToString() + ".");
            Log("Stevie delivery complete. Vehicle=" + rec.Id + ", Model=" + rec.ModelName + ", Fee=$" + fee.ToString() + ", Fuel=full.");
        }

        private int GetStevieDeliveryFee(OwnedVehicleRecord rec)
        {
            return Math.Max(0, stevieDeliveryFee);
        }

        private void FillOwnedVehicleFuel(OwnedVehicleRecord rec, Vehicle v)
        {
            if (rec == null) return;
            float cap = GetFuelCapacityGallonsForOwnedRecord(rec, v);
            if (cap <= 0.0f) cap = 15.0f;
            rec.FuelGallons = cap;
            rec.FuelKnown = true;
            if (fuelModuleEnabled)
            {
                FuelVehicleState state = null;
                if (IsValidVehicle(v)) state = GetOrCreateFuelState(v, false);
                if (state != null)
                {
                    state.OwnedRecord = rec;
                    state.CapacityGallons = cap;
                    state.FuelGallons = cap;
                    state.WasOutOfFuel = false;
                    state.EngineForcedOff = false;
                    state.OutOfFuelEnforceNextMs = 0;
                }
            }
        }

        private float GetFuelCapacityGallonsForOwnedRecord(OwnedVehicleRecord rec, Vehicle v)
        {
            VehicleDefinition def = null;
            if (IsValidVehicle(v)) def = GetVehicleDefinitionByHashOnly(v);
            if (def == null && rec != null && rec.ModelName != null)
            {
                vehicleByModel.TryGetValue(rec.ModelName.Trim().ToLowerInvariant(), out def);
            }
            return GetFuelCapacityGallons(v, def);
        }

        private void TryClearRecoverySpawnPoint(OwnedVehicleRecord recovering)
        {
            if (RecoverySpawnClearRadius <= 0.0f) return;
            try
            {
                MethodInfo method = typeof(World).GetMethod("GetAllVehicles", BindingFlags.Public | BindingFlags.Static, null, new Type[0], null);
                if (method == null) return;
                object result = method.Invoke(null, null);
                Vehicle[] all = result as Vehicle[];
                if (all == null) return;
                float r2 = RecoverySpawnClearRadius * RecoverySpawnClearRadius;
                for (int i = 0; i < all.Length; i++)
                {
                    Vehicle v = all[i];
                    if (!IsValidVehicle(v)) continue;
                    OwnedVehicleRecord owned = FindOwnedRecordForVehicle(v);
                    if (owned != null && owned != recovering) continue;
                    if (IsPlayerVehicle(v)) continue;
                    if (IsValidVehicle(lastPlayerVehicle) && IsSameVehicle(v, lastPlayerVehicle)) continue;
                    float x; float y; float z;
                    if (!TryGetVehiclePosition(v, out x, out y, out z)) continue;
                    float dx = x - StevieRecoveryX;
                    float dy = y - StevieRecoveryY;
                    if (dx * dx + dy * dy <= r2) DeleteVehicleSafe(v, "recovery spawn point blocker");
                }
            }
            catch { }
        }

        private void CloseMenu(bool keepPreviewOrVehicle)
        {
            if (menuMode == "dealer")
            {
                if (activeDealer != null && (activeDealer.DisplaySlots.Count > 0 || dealerForcedSlotIndex >= 0 || activeDealer.AdoptNativeVehicles))
                {

                    dealerPreviewVehicle = null;
                    dealerPreviewModel = "";
                    dealerPreviewHandle = 0;
                    dealerPreviewIsAmbient = false;
                    dealerAvailableExtras.Clear();
                    dealerExtraStates.Clear();
                    ResetDealerExtrasCache();
                }
                else
                {
                    if (!keepPreviewOrVehicle && deletePreviewOnCancel) ClearDealerPreview(true);
                    else ClearDealerPreview(false);
                }
            }
            if (menuMode == "registration" || menuMode == "plate" || menuMode == "deregister" || menuMode == "deliver")
            {
                if (IsValidVehicle(activeRegistrationVehicle))
                {
                    try { Function.Call("FREEZE_CAR_POSITION", activeRegistrationVehicle, false); } catch { }
                }
            }
            menuOpen = false;
            HoldPauseMenuDisabledBriefly();
            menuMode = "";
            dealerColourPaletteMode = false;
            dealerForcedSlotIndex = -1;
            activeDealer = null;
            activeRegistrationPoint = null;
            activeRegistrationVehicle = null;
            activeRegistrationModelName = "";
            activeRegistrationOwnedRecord = null;
            registrationInsuranceOnlyMode = false;
            customPlateTarget = null;
            customPlateInput = "";
            customPlateFieldIndex = 1;
            selectedIndex = 0;
            Player.CanControlCharacter = true;
        }

        private void CreateOrRefreshDealerPreview()
        {
            if (!createDealerPreviewVehicles || activeDealer == null || previewCooldown > 0) return;
            if (dealerForcedSlotIndex >= 0 && IsValidVehicle(dealerPreviewVehicle)) return;
            string model = GetCurrentDealerModel();
            if (model.Length == 0) return;
            if (activeDealer.DisplaySlots.Count > 0)
            {
                EnsureSpawnedDealerSaleSlotVehicles(activeDealer);
                AssignCurrentDealerSaleSlotPreview();
                FocusCameraOnDealerPreview();
                return;
            }
            if (IsValidVehicle(dealerPreviewVehicle) && model.Equals(dealerPreviewModel, StringComparison.OrdinalIgnoreCase)) return;

            ClearDealerPreview(true);
            DealerDisplaySlot slot = GetCurrentDealerSlot();
            Vector3 pos = slot == null ? new Vector3(activeDealer.DisplayX, activeDealer.DisplayY, activeDealer.DisplayZ) : new Vector3(slot.X, slot.Y, slot.Z);
            float heading = slot == null ? activeDealer.DisplayHeading : slot.Heading;
            Vehicle v = TryCreateVehicleByName(model, pos, heading);
            if (!IsValidVehicle(v))
            {
                Notify("Could not create preview vehicle: " + model + ".");
                Log("Preview creation failed for " + model + ".");
                previewCooldown = previewRespawnCooldownTicks;
                return;
            }
            dealerPreviewVehicle = v;
            dealerPreviewModel = model;
            dealerPreviewHandle = TryGetVehicleHandle(v);
            dealerPreviewIsAmbient = false;
            try { v.Color = new ColorIndex(dealerColourID); } catch { }
            try { v.SpecularColor = new ColorIndex(dealerColourID); } catch { }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            try { Function.Call("SET_CAR_ON_GROUND_PROPERLY", v); } catch { }
            try { Function.Call("LOCK_CAR_DOORS", v, 4); } catch { }
            if (freezePreviewVehicles) { try { Function.Call("FREEZE_CAR_POSITION", v, true); } catch { } }
            BuildDealerExtrasFromPreview();
            Log("Preview vehicle created. Model=" + model + ", Handle=" + dealerPreviewHandle.ToString() + ".");
        }

        private void ClearDealerPreview(bool deleteVehicle)
        {
            if (IsValidVehicle(dealerPreviewVehicle))
            {
                try { Function.Call("FREEZE_CAR_POSITION", dealerPreviewVehicle, false); } catch { }
                try { Function.Call("LOCK_CAR_DOORS", dealerPreviewVehicle, 0); } catch { }
                if (deleteVehicle && !dealerPreviewIsAmbient)
                {
                    try { dealerPreviewVehicle.Delete(); } catch { }
                    try { Function.Call("DELETE_CAR", dealerPreviewVehicle); } catch { }
                }
            }
            dealerPreviewVehicle = null;
            dealerPreviewModel = "";
            dealerPreviewHandle = 0;
            dealerPreviewIsAmbient = false;
            dealerAvailableExtras.Clear();
            dealerExtraStates.Clear();
            ResetDealerExtrasCache();
        }

        private void BuildDealerExtrasFromPreview()
        {
            if (!IsValidVehicle(dealerPreviewVehicle))
            {
                dealerAvailableExtras.Clear();
                dealerExtraStates.Clear();
                ResetDealerExtrasCache();
                return;
            }

            int handle = TryGetVehicleHandle(dealerPreviewVehicle);
            string model = dealerPreviewModel == null ? "" : dealerPreviewModel.ToLowerInvariant();
            if (dealerExtrasCacheValid && dealerExtrasCacheHandle == handle && dealerExtrasCacheModel == model) return;

            dealerAvailableExtras.Clear();
            dealerExtraStates.Clear();

            for (int i = minExtraID; i <= maxExtraID; i++)
            {
                bool exists = false;
                if (probeDealerExtras) exists = ProbeExtra(dealerPreviewVehicle, i);
                else exists = ExtraExistsNonDestructive(dealerPreviewVehicle, i);

                if (exists)
                {
                    dealerAvailableExtras.Add(i);
                    dealerExtraStates[i] = IsExtraOn(dealerPreviewVehicle, i);
                }
            }

            dealerExtrasCacheValid = true;
            dealerExtrasCacheHandle = handle;
            dealerExtrasCacheModel = model;
            if (logDealerExtraDiagnostics)
            {
                Log("Dealer extras built. Model=" + model + ", Handle=" + handle.ToString() + ", Extras=" + JoinIntList(dealerAvailableExtras, ",") + ".");
            }
        }

        private void ResetDealerExtrasCache()
        {
            dealerExtrasCacheValid = false;
            dealerExtrasCacheHandle = 0;
            dealerExtrasCacheModel = "";
        }

        private string JoinIntList(List<int> values, string sep)
        {
            if (values == null || values.Count == 0) return "none";
            List<string> parts = new List<string>();
            for (int i = 0; i < values.Count; i++) parts.Add(values[i].ToString());
            return Join(parts, sep);
        }

        private void ApplyDealerExtraStates()
        {
            ApplyDealerExtraStatesToVehicle(dealerPreviewVehicle);
        }

        private void ApplyDealerExtraStatesToVehicle(Vehicle vehicle)
        {
            if (!IsValidVehicle(vehicle)) return;
            foreach (KeyValuePair<int, bool> pair in dealerExtraStates)
            {
                SetExtraState(vehicle, pair.Key, pair.Value);
            }
        }

        private int GetDealerPrimaryColour(Vehicle vehicle, int fallback)
        {
            if (!IsValidVehicle(vehicle)) return fallback;
            try { return ClampColourID(vehicle.Color.Index); } catch { }
            return fallback;
        }

        private void CopyDealerVehicleAppearance(Vehicle source, Vehicle target)
        {
            if (!IsValidVehicle(source) || !IsValidVehicle(target)) return;
            try { target.Color = source.Color; } catch { }
            try { target.SpecularColor = source.SpecularColor; } catch { }
            try { target.FeatureColor1 = source.FeatureColor1; } catch { }
            try { target.FeatureColor2 = source.FeatureColor2; } catch { }
        }

        private Vehicle CreateDealerPurchaseSpawnVehicle(string model, DealerDefinition dealer)
        {
            if (dealer == null || !dealer.HasPurchaseSpawn || model == null || model.Trim().Length == 0) return null;
            DealerDeliverySlot slot = new DealerDeliverySlot() { X = dealer.PurchaseX, Y = dealer.PurchaseY, Z = dealer.PurchaseZ, Heading = dealer.PurchaseHeading };
            return CreateDealerPurchaseSpawnVehicle(model, dealer, slot);
        }

        private Vehicle CreateDealerPurchaseSpawnVehicle(string model, DealerDefinition dealer, DealerDeliverySlot slot)
        {
            if (dealer == null || slot == null || model == null || model.Trim().Length == 0) return null;
            Vehicle v = TryCreateVehicleByName(model, new Vector3(slot.X, slot.Y, slot.Z), slot.Heading);
            if (!IsValidVehicle(v)) return null;
            if (IsValidVehicle(dealerPreviewVehicle)) CopyDealerVehicleAppearance(dealerPreviewVehicle, v);
            else
            {
                try { v.Color = new ColorIndex(dealerColourID); } catch { }
                try { v.SpecularColor = new ColorIndex(dealerColourID); } catch { }
            }
            ApplyDealerExtraStatesToVehicle(v);
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            PlaceVehicleOnGroundSafe(v, slot.X, slot.Y, slot.Z, slot.Heading);
            try { Function.Call("LOCK_CAR_DOORS", v, 0); } catch { }
            try { Function.Call("FREEZE_CAR_POSITION", v, false); } catch { }
            try { Function.Call("SET_CAR_AS_MISSION_CAR", v); } catch { }
            Log("Dealer purchase vehicle spawned. Dealer=" + dealer.Id + ", Model=" + model + ", X=" + slot.X.ToString("0.000", CultureInfo.InvariantCulture) + ", Y=" + slot.Y.ToString("0.000", CultureInfo.InvariantCulture) + ", Z=" + slot.Z.ToString("0.000", CultureInfo.InvariantCulture) + ".");
            return v;
        }

        private DealerDeliveryChoice FindFreeDealerDeliverySlot(DealerDefinition dealer)
        {
            if (dealer == null) return null;
            if (dealer.DeliverySlots != null && dealer.DeliverySlots.Count > 0)
            {
                for (int i = 0; i < dealer.DeliverySlots.Count; i++)
                {
                    DealerDeliverySlot slot = dealer.DeliverySlots[i];
                    string key = GetDealerDeliverySlotKey(dealer, i);
                    if (IsDealerDeliverySlotClear(slot, key)) return new DealerDeliveryChoice() { Slot = slot, Key = key };
                }
                return null;
            }
            if (dealer.HasPurchaseSpawn)
            {
                DealerDeliverySlot fallback = new DealerDeliverySlot() { X = dealer.PurchaseX, Y = dealer.PurchaseY, Z = dealer.PurchaseZ, Heading = dealer.PurchaseHeading };
                string key = GetDealerDeliverySlotKey(dealer, 0);
                if (IsDealerDeliverySlotClear(fallback, key)) return new DealerDeliveryChoice() { Slot = fallback, Key = key };
            }
            return null;
        }

        private string GetDealerDeliverySlotKey(DealerDefinition dealer, int index)
        {
            if (dealer == null || dealer.Id == null) return "delivery:" + index.ToString();
            return dealer.Id + ":delivery:" + index.ToString();
        }

        private void ReserveDealerDeliverySlot(string key)
        {
            if (key == null || key.Length == 0) return;
            dealerDeliveryReservations[key] = Math.Max(30, dealerDeliveryReservationTicks);
        }

        private bool IsDealerDeliverySlotClear(DealerDeliverySlot slot, string key)
        {
            if (slot == null) return false;
            int reservation = 0;
            if (key != null && key.Length > 0 && dealerDeliveryReservations.TryGetValue(key, out reservation) && reservation > 0) return false;
            try
            {
                MethodInfo method = typeof(World).GetMethod("GetAllVehicles", BindingFlags.Public | BindingFlags.Static, null, new Type[0], null);
                if (method == null) return true;
                object result = method.Invoke(null, null);
                Vehicle[] all = result as Vehicle[];
                if (all == null) return true;
                float r = 4.25f;
                float r2 = r * r;
                for (int i = 0; i < all.Length; i++)
                {
                    Vehicle v = all[i];
                    if (!IsValidVehicle(v)) continue;
                    float x; float y; float z;
                    if (!TryGetVehiclePosition(v, out x, out y, out z)) continue;
                    float dx = x - slot.X;
                    float dy = y - slot.Y;
                    float dz = z - slot.Z;
                    if ((dx * dx + dy * dy) <= r2 && Math.Abs(dz) <= 3.0f) return false;
                }
            }
            catch (Exception ex)
            {
                Log("Dealer delivery-slot occupancy check failed: " + ex.GetType().Name + ". Assuming free.");
            }
            return true;
        }

        private string GetCurrentDealerModel()
        {
            if (activeDealer == null) return "";
            if (activeDealer.DisplaySlots.Count > 0 || activeDealer.AdoptNativeVehicles)
            {
                if (activeDealer.DisplaySlots.Count > 0 && dealerForcedSlotIndex < 0) dealerModelIndex = ClampIndex(dealerModelIndex, activeDealer.DisplaySlots.Count);
                if (IsValidVehicle(dealerPreviewVehicle))
                {
                    string current = GetModelNameForVehicle(dealerPreviewVehicle);
                    if (current != null && current.Length > 0 && current != "unknown") return current.ToLowerInvariant();
                }
                return GetDealerModelForSlot(activeDealer, dealerModelIndex);
            }
            if (activeDealer.Models.Count == 0) return "";
            dealerModelIndex = ClampIndex(dealerModelIndex, activeDealer.Models.Count);
            return activeDealer.Models[dealerModelIndex].Trim().ToLowerInvariant();
        }

        private string GetDealerModelForSlot(DealerDefinition dealer, int slotIndex)
        {
            if (dealer == null || dealer.Models == null || dealer.Models.Count == 0) return "";

            string key = GetDealerSlotKey(dealer, slotIndex);
            string existing = "";
            if (key.Length > 0 && dealerSlotModels.TryGetValue(key, out existing) && existing != null && existing.Trim().Length > 0)
            {
                return existing.Trim().ToLowerInvariant();
            }

            if (randomiseDealerSaleSlotModels && dealer.DisplaySlots != null && dealer.DisplaySlots.Count > 0)
            {
                string selected = SelectDealerSaleSlotModel(dealer, slotIndex);
                if (selected.Length > 0)
                {
                    dealerSlotModels[key] = selected;
                    return selected;
                }
            }

            int modelIndex = slotIndex;
            while (modelIndex < 0) modelIndex += dealer.Models.Count;
            while (modelIndex >= dealer.Models.Count) modelIndex -= dealer.Models.Count;
            string model = dealer.Models[modelIndex] == null ? "" : dealer.Models[modelIndex].Trim().ToLowerInvariant();
            return model;
        }

        private string SelectDealerSaleSlotModel(DealerDefinition dealer, int slotIndex)
        {
            if (dealer == null || dealer.Models == null || dealer.Models.Count == 0) return "";

            string preferred = dealer.PreferredModel == null ? "" : dealer.PreferredModel.Trim().ToLowerInvariant();
            bool preferredAvailable = false;
            if (preferred.Length > 0)
            {
                for (int i = 0; i < dealer.Models.Count; i++)
                {
                    string model = dealer.Models[i] == null ? "" : dealer.Models[i].Trim().ToLowerInvariant();
                    if (model == preferred) { preferredAvailable = true; break; }
                }
            }

            if (preferredAvailable && dealer.PreferredModelChance > 0.0f)
            {
                double roll = 1.0;
                try { roll = dealerRandom.NextDouble(); } catch { }
                if (roll < Math.Max(0.0f, Math.Min(1.0f, dealer.PreferredModelChance)))
                {
                    Log("Dealer sale slot model selected. Dealer=" + dealer.Id + ", slot=" + (slotIndex + 1).ToString() + ", model=" + preferred + " (preferred).");
                    return preferred;
                }
            }

            List<string> candidates = new List<string>();
            for (int i = 0; i < dealer.Models.Count; i++)
            {
                string model = dealer.Models[i] == null ? "" : dealer.Models[i].Trim().ToLowerInvariant();
                if (model.Length == 0) continue;
                if (dealer.Id == "westdyke" && model == "nrg900") continue;
                if (preferredAvailable && model == preferred) continue;

                bool usedByOtherSlot = false;
                if (dealer.DisplaySlots != null && dealer.DisplaySlots.Count > 1)
                {
                    for (int slot = 0; slot < dealer.DisplaySlots.Count; slot++)
                    {
                        if (slot == slotIndex) continue;
                        string otherKey = GetDealerSlotKey(dealer, slot);
                        string otherModel = "";
                        if (dealerSlotModels.TryGetValue(otherKey, out otherModel) && otherModel != null && otherModel.Trim().ToLowerInvariant() == model)
                        {
                            usedByOtherSlot = true;
                            break;
                        }
                    }
                }

                if (!usedByOtherSlot) candidates.Add(model);
            }

            if (candidates.Count == 0)
            {
                for (int i = 0; i < dealer.Models.Count; i++)
                {
                    string model = dealer.Models[i] == null ? "" : dealer.Models[i].Trim().ToLowerInvariant();
                    if (model.Length == 0) continue;
                    if (dealer.Id == "westdyke" && model == "nrg900") continue;
                    if (preferredAvailable && model == preferred && dealer.Models.Count > 1) continue;
                    candidates.Add(model);
                }
            }

            if (candidates.Count == 0 && preferredAvailable) candidates.Add(preferred);
            if (candidates.Count == 0) return "";

            int index = 0;
            try { index = dealerRandom.Next(0, candidates.Count); }
            catch
            {
                long seed = DateTime.Now.Ticks + (long)tickCounter * 7919L + (long)slotIndex * 104729L;
                try { if (dealer.Id != null) seed += (long)dealer.Id.GetHashCode(); } catch { }
                if (seed < 0) seed = -seed;
                index = (int)(seed % (long)candidates.Count);
            }
            string chosen = candidates[index];
            Log("Dealer sale slot model selected. Dealer=" + dealer.Id + ", slot=" + (slotIndex + 1).ToString() + ", model=" + chosen + ".");
            return chosen;
        }

        private string GetDealerSlotKey(DealerDefinition dealer, int slotIndex)
        {
            if (dealer == null || dealer.Id == null) return "";
            return dealer.Id + ":" + slotIndex.ToString();
        }

        private void FocusCameraOnDealerPreview()
        {
            if (!focusCameraOnDealerSelection || !IsValidVehicle(dealerPreviewVehicle)) return;
            try
            {
                float vx; float vy; float vz;
                float px; float py; float pz;
                if (!TryGetVehiclePosition(dealerPreviewVehicle, out vx, out vy, out vz)) return;
                if (!TryGetPlayerPosition(out px, out py, out pz)) return;
                float angle = (float)(Math.Atan2(vx - px, vy - py) * 180.0 / Math.PI);
                try { Function.Call("SET_GAME_CAM_HEADING", angle); } catch { }
                try { Function.Call("SET_GAME_CAM_PITCH", -8.0f); } catch { }
            }
            catch { }
        }

        private void UpdateDealerSaleSlotWorld()
        {
            if (!createDealerPreviewVehicles) return;
            if (menuOpen) return;
            if (dealerWorldWarmupTicks > 0) return;

            for (int i = 0; i < dealers.Count; i++)
            {
                DealerDefinition dealer = dealers[i];
                if (dealer == null || !dealer.Enabled || !dealer.UseCarSpecificInteraction) continue;
                if (dealer.Models == null || dealer.Models.Count == 0) continue;

                string key = dealer.Id == null ? "dealer" + i.ToString() : dealer.Id;
                if (!IsPlayerNearDealerArea(dealer))
                {
                    dealerMaintenanceCooldown.Remove(key);
                    dealerAdoptionLockedThisVisit.Remove(key);
                    dealerNativeAdoptionAttempts.Remove(key);
                    dealerAdoptedLogCounts.Remove(key);
                    ClearHeldDealerSlotsForDealer(dealer);
                    if (dealer.AdoptNativeVehicles) RemoveAmbientDealerSaleReferences(dealer);
                    continue;
                }

                bool lockedThisVisit = false;
                dealerAdoptionLockedThisVisit.TryGetValue(key, out lockedThisVisit);

                if (dealer.AdoptNativeVehicles)
                {


                    if (lockedThisVisit) continue;

                    int cd = 0;
                    if (dealerMaintenanceCooldown.TryGetValue(key, out cd) && cd > 0) continue;
                    dealerMaintenanceCooldown[key] = Math.Max(90, adoptedDealerScanIntervalTicks);

                    int attempts = 0;
                    dealerNativeAdoptionAttempts.TryGetValue(key, out attempts);
                    attempts++;
                    dealerNativeAdoptionAttempts[key] = attempts;

                    int adopted = AdoptNativeDealerVehicles(dealer);
                    if (dealer.ForceSaleSlotVehicles) EnsureSpawnedDealerSaleSlotVehicles(dealer);

                    if (adopted > 0)
                    {


                        if (!dealer.ForceSaleSlotVehicles || attempts >= 2) dealerAdoptionLockedThisVisit[key] = true;
                    }
                    else if (dealer.FallbackSpawnWhenNoNativeVehicles)
                    {
                        EnsureSpawnedDealerSaleSlotVehicles(dealer);
                        if (CountDealerSaleVehicles(dealer) > 0) dealerAdoptionLockedThisVisit[key] = true;
                    }
                    else if (dealer.ForceSaleSlotVehicles)
                    {


                        if (attempts >= Math.Max(1, adoptedDealerMaxRetryAttempts))
                        {
                            dealerAdoptionLockedThisVisit[key] = true;
                            Log("Dealer native adoption locked after empty retries. Dealer=" + dealer.Id + ", Attempts=" + attempts.ToString() + ".");
                        }
                    }
                    continue;
                }

                int spawnedCd = 0;
                if (dealerMaintenanceCooldown.TryGetValue(key, out spawnedCd) && spawnedCd > 0) continue;
                dealerMaintenanceCooldown[key] = Math.Max(30, dealerSaleSlotMaintenanceIntervalTicks);

                if (dealer.ForceSaleSlotVehicles) EnsureSpawnedDealerSaleSlotVehicles(dealer);
            }
        }

        private bool IsPlayerNearDealerArea(DealerDefinition dealer)
        {
            if (dealer == null) return false;
            float px; float py; float pz;
            if (!TryGetPlayerPosition(out px, out py, out pz)) return false;

            if (dealer.HasCleanupBounds)
            {
                float minX = Math.Min(dealer.CleanupMinX, dealer.CleanupMaxX) - 35.0f;
                float maxX = Math.Max(dealer.CleanupMinX, dealer.CleanupMaxX) + 35.0f;
                float minY = Math.Min(dealer.CleanupMinY, dealer.CleanupMaxY) - 35.0f;
                float maxY = Math.Max(dealer.CleanupMinY, dealer.CleanupMaxY) + 35.0f;
                if (px >= minX && px <= maxX && py >= minY && py <= maxY && Math.Abs(pz - dealer.Z) <= 45.0f) return true;
            }

            float maxDist = dealer.AdoptNativeVehicles ? 85.0f : Math.Max(30.0f, dealerSaleSlotActivationRadius);
            float dx = px - dealer.X;
            float dy = py - dealer.Y;
            float dz = pz - dealer.Z;
            return (dx * dx + dy * dy) <= (maxDist * maxDist) && Math.Abs(dz) <= 45.0f;
        }

        private bool IsPlayerNearDealerSaleSlots(DealerDefinition dealer)
        {
            if (dealer == null || dealer.DisplaySlots == null || dealer.DisplaySlots.Count == 0) return false;
            float px; float py; float pz;
            if (!TryGetPlayerPosition(out px, out py, out pz)) return false;
            float maxDist = Math.Max(30.0f, dealerSaleSlotActivationRadius);
            float maxDist2 = maxDist * maxDist;
            for (int i = 0; i < dealer.DisplaySlots.Count; i++)
            {
                DealerDisplaySlot slot = dealer.DisplaySlots[i];
                float dx = px - slot.X;
                float dy = py - slot.Y;
                float dz = pz - slot.Z;
                if ((dx * dx + dy * dy) <= maxDist2 && Math.Abs(dz) <= 35.0f) return true;
            }
            return false;
        }

        private void EnsureSpawnedDealerSaleSlotVehicles(DealerDefinition dealer)
        {
            if (dealer == null || dealer.DisplaySlots == null || dealer.DisplaySlots.Count == 0) return;

            int createdThisUpdate = 0;
            int maxCreates = Math.Max(1, dealerSaleSlotMaxCreatesPerUpdate);
            for (int i = 0; i < dealer.DisplaySlots.Count; i++)
            {
                if (createdThisUpdate >= maxCreates) break;
                string key = GetDealerSlotKey(dealer, i);
                if (IsDealerSlotHeldEmpty(key)) continue;
                int cd = 0;
                if (dealerSlotRespawnCooldown.TryGetValue(key, out cd) && cd > 0)
                {
                    dealerSlotRespawnCooldown[key] = cd - 1;
                    continue;
                }

                Vehicle existing = null;
                if (dealerSlotVehicles.TryGetValue(key, out existing) && IsValidVehicle(existing))
                {
                    if (IsVehicleDead(existing) || IsVehicleAlreadyOwned(existing))
                    {
                        RemoveDealerSaleSlotByKey(key, true);
                        dealerSlotRespawnCooldown[key] = Math.Max(300, dealerSaleSlotRespawnTicks);
                    }
                    continue;
                }

                DealerDisplaySlot slot = dealer.DisplaySlots[i];
                if (IsDealerSaleSlotBlocked(dealer, slot, key))
                {
                    dealerSlotRespawnCooldown[key] = Math.Max(60, dealerSaleSlotMaintenanceIntervalTicks);
                    continue;
                }

                string model = GetDealerModelForSlot(dealer, i);
                if (model.Length == 0) continue;

                Vehicle v = TryCreateVehicleByName(model, new Vector3(slot.X, slot.Y, slot.Z), slot.Heading);
                if (!IsValidVehicle(v))
                {
                    Log("Sale slot vehicle creation failed. Dealer=" + dealer.Id + ", slot=" + (i + 1).ToString() + ", model=" + model + ".");
                    dealerSlotRespawnCooldown[key] = Math.Max(1, dealerSaleSlotRespawnTicks);
                    continue;
                }

                int colour = GetOrCreateDealerSlotColour(key);
                try { v.Color = new ColorIndex(colour); } catch { }
                try { v.SpecularColor = new ColorIndex(colour); } catch { }
                PrepareSpawnedDealerSaleVehicle(v, slot);
                dealerSlotVehicles[key] = v;
                dealerSlotModels[key] = model;
                dealerSlotAmbient[key] = false;
                createdThisUpdate++;
                Log("Sale slot mod vehicle created. Dealer=" + dealer.Id + ", slot=" + (i + 1).ToString() + ", Model=" + model + ", Colour=" + colour.ToString() + ", Handle=" + TryGetVehicleHandle(v).ToString() + ".");
            }
        }

        private int AdoptNativeDealerVehicles(DealerDefinition dealer)
        {
            if (dealer == null || dealer.Id == null) return 0;

            try
            {
                MethodInfo method = typeof(World).GetMethod("GetAllVehicles", BindingFlags.Public | BindingFlags.Static, null, new Type[0], null);
                if (method == null) return 0;
                object result = method.Invoke(null, null);
                Vehicle[] all = result as Vehicle[];
                if (all == null) return 0;


                RemoveAmbientDealerSaleReferences(dealer);

                int adopted = 0;
                int inspected = 0;
                for (int i = 0; i < all.Length && adopted < 48 && inspected < 180; i++)
                {
                    Vehicle v = all[i];
                    if (!IsValidVehicle(v)) continue;

                    float x; float y; float z;
                    if (!TryGetVehiclePosition(v, out x, out y, out z)) continue;
                    if (!IsInsideDealerAdoptionArea(dealer, x, y, z)) continue;
                    inspected++;

                    if (!CanAdoptNativeDealerVehicle(dealer, v)) continue;
                    string model = GetModelNameForVehicleFast(v).ToLowerInvariant();
                    if (model.Length == 0 || model == "unknown") continue;

                    int slotIndex = 100 + adopted;
                    string key = GetDealerSlotKey(dealer, slotIndex);
                    dealerSlotVehicles[key] = v;
                    dealerSlotModels[key] = model;
                    dealerSlotAmbient[key] = true;
                    GetOrCreateDealerSlotPriceFactor(key);
                    adopted++;
                }

                int previous = -1;
                dealerAdoptedLogCounts.TryGetValue(dealer.Id, out previous);
                if (adopted != previous)
                {
                    dealerAdoptedLogCounts[dealer.Id] = adopted;
                    Log("Dealer adopted native vehicle snapshot. Dealer=" + dealer.Id + ", Count=" + adopted.ToString() + ".");
                }
                return adopted;
            }
            catch (Exception ex)
            {
                Log("Native dealer adoption failed. Dealer=" + dealer.Id + ", Error=" + ex.GetType().Name + ".");
                return 0;
            }
        }

        private bool CanAdoptNativeDealerVehicle(DealerDefinition dealer, Vehicle v)
        {
            if (dealer == null || !IsValidVehicle(v)) return false;
            if (IsBlockingScanProtectedVehicle(v)) return false;
            if (IsVehicleAlreadyOwned(v)) return false;
            if (IsAnyDealerSaleSlotVehicle(v)) return false;
            if (IsVehicleInsideHeldDealerSlot(dealer, v)) return false;
            if (IsVehicleDead(v)) return false;
            if (SafeVehicleSpeed(v) > 0.35f) return false;


            VehicleDefinition def = GetVehicleDefinitionByHashFast(v);
            if (def == null && !keepUnknownDealerModels) return false;
            if (def != null)
            {
                if (def.IsEmergency) return false;
                if (def.IsTaxi) return false;
                if (def.IsIndustrial) return false;
            }


            return true;
        }

        private bool IsInsideDealerAdoptionArea(DealerDefinition dealer, float x, float y, float z)
        {
            if (dealer == null) return false;
            if (dealer.HasCleanupBounds)
            {
                float minX = Math.Min(dealer.CleanupMinX, dealer.CleanupMaxX);
                float maxX = Math.Max(dealer.CleanupMinX, dealer.CleanupMaxX);
                float minY = Math.Min(dealer.CleanupMinY, dealer.CleanupMaxY);
                float maxY = Math.Max(dealer.CleanupMinY, dealer.CleanupMaxY);
                if (x < minX || x > maxX || y < minY || y > maxY) return false;
                return Math.Abs(z - dealer.Z) <= 12.0f;
            }

            float cx = dealer.CleanupX;
            float cy = dealer.CleanupY;
            float cz = dealer.CleanupZ;
            float r = dealer.CleanupRadius;
            if (r <= 0.0f)
            {
                cx = dealer.X; cy = dealer.Y; cz = dealer.Z; r = Math.Max(dealer.Radius, 12.0f);
            }
            float dx = x - cx;
            float dy = y - cy;
            return (dx * dx + dy * dy) <= (r * r) && Math.Abs(z - cz) <= 12.0f;
        }

        private bool IsDealerVehicleHealthyEnoughForSale(Vehicle v)
        {
            if (!IsValidVehicle(v)) return false;
            if (IsVehicleDead(v)) return false;
            if (SafeVehicleHealth(v) < dealerSaleMinimumHealth) return false;
            if (SafeEngineHealth(v) < dealerSaleMinimumEngineHealth) return false;
            return true;
        }

        private void RemoveAmbientDealerSaleReferences(DealerDefinition dealer)
        {
            if (dealer == null || dealer.Id == null) return;
            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, Vehicle> pair in dealerSlotVehicles)
            {
                if (pair.Key == null || !pair.Key.StartsWith(dealer.Id + ":")) continue;
                bool isAmbient = false;
                dealerSlotAmbient.TryGetValue(pair.Key, out isAmbient);
                if (isAmbient) keys.Add(pair.Key);
            }
            for (int i = 0; i < keys.Count; i++) RemoveDealerSaleSlotByKey(keys[i], false);
        }

        private void RemoveInvalidDealerSaleReferences(DealerDefinition dealer)
        {
            if (dealer == null || dealer.Id == null) return;
            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, Vehicle> pair in dealerSlotVehicles)
            {
                if (pair.Key == null || !pair.Key.StartsWith(dealer.Id + ":")) continue;
                Vehicle v = pair.Value;
                if (!IsValidVehicle(v) || IsVehicleDead(v) || IsVehicleAlreadyOwned(v)) keys.Add(pair.Key);
                else
                {
                    bool isAmbient = false;
                    dealerSlotAmbient.TryGetValue(pair.Key, out isAmbient);
                    if (isAmbient)
                    {
                        float x; float y; float z;
                        if (!TryGetVehiclePosition(v, out x, out y, out z) || !IsInsideDealerAdoptionArea(dealer, x, y, z)) keys.Add(pair.Key);
                    }
                }
            }
            for (int i = 0; i < keys.Count; i++) RemoveDealerSaleSlotByKey(keys[i], false);
        }

        private int CountDealerSaleVehicles(DealerDefinition dealer)
        {
            if (dealer == null || dealer.Id == null) return 0;
            int count = 0;
            foreach (KeyValuePair<string, Vehicle> pair in dealerSlotVehicles)
            {
                if (pair.Key == null || !pair.Key.StartsWith(dealer.Id + ":")) continue;
                if (IsValidVehicle(pair.Value) && !IsVehicleAlreadyOwned(pair.Value) && !IsVehicleDead(pair.Value)) count++;
            }
            return count;
        }

        private void PrepareSpawnedDealerSaleVehicle(Vehicle v, DealerDisplaySlot slot)
        {
            if (!IsValidVehicle(v)) return;
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            if (slot != null) PlaceVehicleOnGroundSafe(v, slot.X, slot.Y, slot.Z, slot.Heading);
            try { Function.Call("SET_CAR_ENGINE_ON", v, false, true); } catch { }
            try { Function.Call("LOCK_CAR_DOORS", v, 0); } catch { }
            try { Function.Call("FREEZE_CAR_POSITION", v, false); } catch { }
            try { Function.Call("SET_CAR_AS_MISSION_CAR", v); } catch { }
        }

        private bool IsVehicleParkedAtSlot(Vehicle v, DealerDisplaySlot slot)
        {
            if (!IsValidVehicle(v) || slot == null) return false;
            float x; float y; float z;
            if (!TryGetVehiclePosition(v, out x, out y, out z)) return false;
            float dx = x - slot.X;
            float dy = y - slot.Y;
            float dz = z - slot.Z;
            return (dx * dx + dy * dy) <= 1.25f && Math.Abs(dz) <= 1.5f;
        }

        private int GetOrCreateDealerSlotColour(string key)
        {
            int colour = 0;
            if (dealerSlotColours.TryGetValue(key, out colour)) return colour;
            colour = RandomDealerColourId();
            dealerSlotColours[key] = colour;
            return colour;
        }

        private int RandomDealerColourId()
        {
            int min = Math.Min(minColourID, maxColourID);
            int max = Math.Max(minColourID, maxColourID);
            try { return dealerRandom.Next(min, max + 1); }
            catch { return min; }
        }

        private float GetOrCreateDealerSlotPriceFactor(string key)
        {
            float factor = 1.0f;
            if (dealerSlotPriceFactors.TryGetValue(key, out factor)) return factor;
            float range = Math.Max(0.0f, dealerPriceVariancePercent) / 100.0f;
            if (range <= 0.0f) factor = 1.0f;
            else
            {
                double roll = 0.0;
                try { roll = dealerRandom.NextDouble(); } catch { roll = 0.5; }
                factor = 1.0f + (float)((roll * 2.0 - 1.0) * range);
            }
            dealerSlotPriceFactors[key] = factor;
            return factor;
        }

        private bool IsBlockingScanProtectedVehicle(Vehicle v)
        {
            if (!IsValidVehicle(v)) return false;
            try
            {
                if (Player.Character != null && Player.Character.Exists() && Player.Character.isSittingInVehicle())
                {
                    if (IsSameVehicle(v, Player.Character.CurrentVehicle)) return true;
                }
            }
            catch { }
            if (lastPlayerVehicle != null && IsSameVehicle(v, lastPlayerVehicle)) return true;
            return false;
        }

        private bool IsAnyDealerSaleSlotVehicle(Vehicle v)
        {
            foreach (KeyValuePair<string, Vehicle> pair in dealerSlotVehicles)
            {
                if (IsSameVehicle(v, pair.Value)) return true;
            }
            return false;
        }

        private void DeleteVehicleSafe(Vehicle v, string purpose)
        {
            if (!IsValidVehicle(v)) return;


            try { v.Delete(); return; } catch { }
            try { Function.Call("DELETE_CAR", v); } catch { }
        }

        private bool IsDealerSaleSlotBlocked(DealerDefinition dealer, DealerDisplaySlot slot, string ownKey)
        {
            if (slot == null) return true;
            try
            {
                MethodInfo method = typeof(World).GetMethod("GetAllVehicles", BindingFlags.Public | BindingFlags.Static, null, new Type[0], null);
                if (method == null) return false;
                object result = method.Invoke(null, null);
                Vehicle[] all = result as Vehicle[];
                if (all == null) return false;
                float r = Math.Max(0.5f, dealerSaleSlotClearRadius);
                float r2 = r * r;
                for (int i = 0; i < all.Length; i++)
                {
                    Vehicle v = all[i];
                    if (!IsValidVehicle(v)) continue;
                    if (IsAnyDealerSaleSlotVehicle(v)) continue;
                    float x; float y; float z;
                    if (!TryGetVehiclePosition(v, out x, out y, out z)) continue;
                    float dx = x - slot.X;
                    float dy = y - slot.Y;
                    float dz = z - slot.Z;
                    if ((dx * dx + dy * dy) <= r2 && Math.Abs(dz) <= 3.0f) return true;
                }
            }
            catch (Exception ex)
            {
                Log("Dealer sale-slot occupancy check failed: " + ex.GetType().Name + ". Assuming blocked.");
                return true;
            }
            return false;
        }

        private bool IsDealerSlotHeldEmpty(string key)
        {
            return key != null && key.Length > 0 && dealerHeldEmptySlots.ContainsKey(key);
        }

        private void HoldDealerSaleSlotEmptyUntilAreaExit(string key, DealerDefinition dealer, Vehicle soldVehicle)
        {
            if (key == null || key.Length == 0 || dealer == null) return;
            DealerHeldEmptySlot hold = new DealerHeldEmptySlot();
            hold.DealerId = dealer.Id == null ? "" : dealer.Id;
            float x; float y; float z;
            if (TryGetVehiclePosition(soldVehicle, out x, out y, out z))
            {
                hold.X = x;
                hold.Y = y;
                hold.Z = z;
            }
            else
            {
                int slotIndex = ParseDealerSlotIndex(key);
                DealerDisplaySlot slot = dealer.DisplaySlots != null && slotIndex >= 0 && slotIndex < dealer.DisplaySlots.Count ? dealer.DisplaySlots[slotIndex] : null;
                if (slot != null)
                {
                    hold.X = slot.X;
                    hold.Y = slot.Y;
                    hold.Z = slot.Z;
                }
                else
                {
                    hold.X = dealer.DisplayX;
                    hold.Y = dealer.DisplayY;
                    hold.Z = dealer.DisplayZ;
                }
            }
            dealerHeldEmptySlots[key] = hold;
            dealerSlotRespawnCooldown.Remove(key);
            Log("Dealer sale slot held empty until area reload. Dealer=" + hold.DealerId + ", Key=" + key + ".");
        }

        private void ClearHeldDealerSlotsForDealer(DealerDefinition dealer)
        {
            if (dealer == null || dealer.Id == null || dealerHeldEmptySlots.Count == 0) return;
            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, DealerHeldEmptySlot> pair in dealerHeldEmptySlots)
            {
                if (pair.Value != null && pair.Value.DealerId == dealer.Id) keys.Add(pair.Key);
                else if (pair.Key != null && pair.Key.StartsWith(dealer.Id + ":")) keys.Add(pair.Key);
            }
            for (int i = 0; i < keys.Count; i++)
            {
                dealerHeldEmptySlots.Remove(keys[i]);
                dealerSlotRespawnCooldown.Remove(keys[i]);
            }
        }


        private bool IsVehicleInsideHeldDealerSlot(DealerDefinition dealer, Vehicle v)
        {
            if (dealer == null || dealer.Id == null || !IsValidVehicle(v) || dealerHeldEmptySlots.Count == 0) return false;
            float x; float y; float z;
            if (!TryGetVehiclePosition(v, out x, out y, out z)) return false;
            foreach (KeyValuePair<string, DealerHeldEmptySlot> pair in dealerHeldEmptySlots)
            {
                DealerHeldEmptySlot hold = pair.Value;
                if (hold == null) continue;
                if (hold.DealerId != dealer.Id && (pair.Key == null || !pair.Key.StartsWith(dealer.Id + ":"))) continue;
                float dx = x - hold.X;
                float dy = y - hold.Y;
                float dz = z - hold.Z;
                if ((dx * dx + dy * dy) <= 6.25f && Math.Abs(dz) <= 3.0f) return true;
            }
            return false;
        }

        private void RemoveDealerSaleSlotReferenceForVehicle(Vehicle vehicle)
        {
            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, Vehicle> pair in dealerSlotVehicles)
            {
                if (IsSameVehicle(pair.Value, vehicle)) keys.Add(pair.Key);
            }
            for (int i = 0; i < keys.Count; i++) RemoveDealerSaleSlotByKey(keys[i], true);
        }

        private void RemoveDealerSaleSlotByKey(string key, bool holdEmptyUntilRespawn)
        {
            if (key == null || key.Length == 0) return;
            dealerSlotVehicles.Remove(key);
            dealerSlotModels.Remove(key);
            dealerSlotAmbient.Remove(key);
            dealerSlotColours.Remove(key);
            dealerSlotPriceFactors.Remove(key);
            if (holdEmptyUntilRespawn) dealerSlotRespawnCooldown[key] = Math.Max(1, dealerSaleSlotRespawnTicks);
            else dealerSlotRespawnCooldown.Remove(key);
        }

        private void AssignCurrentDealerSaleSlotPreview()
        {
            if (activeDealer == null || activeDealer.DisplaySlots.Count == 0) return;
            if (dealerForcedSlotIndex >= 0) dealerModelIndex = dealerForcedSlotIndex;
            else if (activeDealer.DisplaySlots.Count > 0) dealerModelIndex = ClampIndex(dealerModelIndex, activeDealer.DisplaySlots.Count);
            string key = GetDealerSlotKey(activeDealer, dealerModelIndex);
            Vehicle v = null;
            if (!dealerSlotVehicles.TryGetValue(key, out v) || !IsValidVehicle(v))
            {
                Log("Current sale slot has no valid vehicle. Dealer=" + activeDealer.Id + ", slot=" + (dealerModelIndex + 1).ToString() + ".");
                return;
            }
            string model = "";
            dealerSlotModels.TryGetValue(key, out model);
            if (model == null || model.Length == 0 || model == "unknown") model = GetModelNameForVehicle(v).ToLowerInvariant();
            bool isAmbient = false;
            dealerSlotAmbient.TryGetValue(key, out isAmbient);
            dealerPreviewVehicle = v;
            dealerPreviewModel = model;
            dealerPreviewHandle = TryGetVehicleHandle(v);
            dealerPreviewIsAmbient = isAmbient;
            BuildDealerExtrasFromPreview();
        }

        private void ClearDealerSaleSlotVehicles(bool deleteVehicles, Vehicle keepVehicle)
        {
            if (activeDealer == null || activeDealer.Id == null) return;
            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, Vehicle> pair in dealerSlotVehicles)
            {
                if (pair.Key != null && pair.Key.StartsWith(activeDealer.Id + ":")) keys.Add(pair.Key);
            }
            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                Vehicle v = dealerSlotVehicles[key];
                bool keep = IsSameVehicle(v, keepVehicle);
                bool isAmbient = false;
                dealerSlotAmbient.TryGetValue(key, out isAmbient);
                if (IsValidVehicle(v))
                {
                    try { Function.Call("FREEZE_CAR_POSITION", v, false); } catch { }
                    try { Function.Call("LOCK_CAR_DOORS", v, 0); } catch { }
                    if (deleteVehicles && !keep && !isAmbient)
                    {
                        try { v.Delete(); } catch { }
                        try { Function.Call("DELETE_CAR", v); } catch { }
                    }
                }
                dealerSlotVehicles.Remove(key);
                dealerSlotModels.Remove(key);
                dealerSlotAmbient.Remove(key);
                dealerSlotColours.Remove(key);
                dealerSlotPriceFactors.Remove(key);
            }
        }

        private bool IsSameVehicle(Vehicle a, Vehicle b)
        {
            if (!IsValidVehicle(a) || !IsValidVehicle(b)) return false;
            int ah = TryGetVehicleHandle(a);
            int bh = TryGetVehicleHandle(b);
            if (ah != 0 && bh != 0) return ah == bh;
            return object.ReferenceEquals(a, b);
        }

        private DealerDisplaySlot GetCurrentDealerSlot()
        {
            if (activeDealer == null || activeDealer.DisplaySlots.Count == 0) return null;
            dealerModelIndex = ClampIndex(dealerModelIndex, activeDealer.DisplaySlots.Count);
            return activeDealer.DisplaySlots[dealerModelIndex];
        }

        private int ClampIndex(int idx, int count)
        {
            if (count <= 0) return 0;
            if (idx < 0) return 0;
            if (idx >= count) return count - 1;
            return idx;
        }

        private float ClampFloat(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }


        private int IndexOfModel(List<string> models, string model)
        {
            if (models == null || model == null) return -1;
            string needle = model.Trim().ToLowerInvariant();
            for (int i = 0; i < models.Count; i++)
            {
                string m = models[i] == null ? "" : models[i].Trim().ToLowerInvariant();
                if (m == needle) return i;
            }
            return -1;
        }

        private Vehicle TryCreateVehicleByName(string modelName, Vector3 position, float heading)
        {
            if (modelName == null || modelName.Trim().Length == 0) return null;
            modelName = modelName.Trim().ToLowerInvariant();
            try
            {
                int hash = Function.Call<int>("GET_HASH_KEY", modelName);
                if (hash != 0)
                {
                    try { Function.Call("REQUEST_MODEL", hash); } catch { }
                    try { Function.Call("LOAD_ALL_OBJECTS_NOW"); } catch { }
                }
            }
            catch { }

            try
            {
                MethodInfo[] methods = typeof(World).GetMethods(BindingFlags.Public | BindingFlags.Static);
                for (int i = 0; i < methods.Length; i++)
                {
                    MethodInfo method = methods[i];
                    if (method.Name != "CreateVehicle") continue;
                    ParameterInfo[] p = method.GetParameters();
                    if (p.Length < 2) continue;
                    object first = null;
                    if (p[0].ParameterType == typeof(string))
                    {
                        first = modelName;
                    }
                    else if (p[0].ParameterType.Name == "Model")
                    {
                        try { first = Activator.CreateInstance(p[0].ParameterType, new object[] { modelName }); }
                        catch
                        {
                            try
                            {
                                int hash = Function.Call<int>("GET_HASH_KEY", modelName);
                                first = Activator.CreateInstance(p[0].ParameterType, new object[] { hash });
                            }
                            catch { first = null; }
                        }
                    }
                    else if (p[0].ParameterType == typeof(int))
                    {
                        try { first = Function.Call<int>("GET_HASH_KEY", modelName); } catch { first = 0; }
                    }
                    if (first == null) continue;

                    object[] args = null;
                    if (p.Length == 2 && p[1].ParameterType == typeof(Vector3)) args = new object[] { first, position };
                    else if (p.Length == 4 && p[1].ParameterType == typeof(float)) args = new object[] { first, position.X, position.Y, position.Z };
                    else continue;

                    object result = method.Invoke(null, args);
                    Vehicle v = result as Vehicle;
                    if (IsValidVehicle(v))
                    {
                        PlaceVehicleOnGroundSafe(v, position.X, position.Y, position.Z, heading);
                        return v;
                    }
                }
            }
            catch (Exception ex)
            {
                Log("World.CreateVehicle reflection failed for " + modelName + ": " + ex.GetType().Name + ".");
            }

            try
            {
                int hash = Function.Call<int>("GET_HASH_KEY", modelName);
                Log("No CreateVehicle overload succeeded for " + modelName + " hash=" + hash.ToString() + ".");
            }
            catch { }
            return null;
        }

        private void PlaceVehicleOnGroundSafe(Vehicle v, float x, float y, float z, float heading)
        {
            if (!safelyGroundCreatedVehicles || !IsValidVehicle(v)) return;
            try { Function.Call("FREEZE_CAR_POSITION", v, false); } catch { }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            try { Function.Call("SET_CAR_HEADING", v, heading); } catch { }
            try { Function.Call("SET_CAR_COORDINATES", v, x, y, z + 0.75f); } catch { }
            try { Function.Call("SET_CAR_ON_GROUND_PROPERLY", v); } catch { }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            try { Function.Call("SET_CAR_HEADING", v, heading); } catch { }
        }

        private void SetVehicleOnGroundProperlyOnce(Vehicle v, float heading)
        {
            if (!safelyGroundCreatedVehicles || !IsValidVehicle(v)) return;
            try { Function.Call("FREEZE_CAR_POSITION", v, false); } catch { }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            try { Function.Call("SET_CAR_HEADING", v, heading); } catch { }
            try { Function.Call("SET_CAR_ON_GROUND_PROPERLY", v); } catch { }
            try { Function.Call("SET_CAR_FORWARD_SPEED", v, 0.0f); } catch { }
            try { Function.Call("SET_CAR_HEADING", v, heading); } catch { }
        }

        private OwnedVehicleRecord RegisterOwnedVehicle(Vehicle v, string modelName, string source, bool registered, bool insured, int purchasePrice, int insurancePrice)
        {
            if (!IsValidVehicle(v)) return null;
            if (IsVehicleAlreadyOwned(v))
            {
                Notify("This vehicle is already registered.");
                return null;
            }
            string limitReason;
            if (!CanAddOwnedVehicle(out limitReason))
            {
                Notify(limitReason);
                return null;
            }
            if (modelName == null || modelName.Length == 0 || modelName == "unknown" || modelName.StartsWith("hash_")) modelName = GetModelNameForVehicle(v);
            OwnedVehicleRecord rec = new OwnedVehicleRecord();
            rec.Id = "owned_" + nextOwnedId.ToString("000000");
            nextOwnedId++;
            rec.ModelName = modelName.ToLowerInvariant();
            rec.ModelHash = TryGetVehicleModelHash(v);
            rec.Source = source == null ? "unknown" : source;
            rec.Episode = GetCurrentEpisodeKey();
            rec.Registered = registered;
            rec.Insured = insured;
            rec.Destroyed = false;
            rec.WorldHandle = TryGetVehicleHandle(v);
            rec.VehicleRef = v;
            rec.PurchasePrice = purchasePrice;
            rec.InsurancePrice = insurancePrice;
            rec.DirtLevel = -1.0f;
            EnsurePlateForOwnedRecord(rec);
            CaptureVehicleState(v, rec);
            bool purchasedFromDealer = activeDealer != null && source != null && activeDealer.Id != null && source.Equals(activeDealer.Id, StringComparison.OrdinalIgnoreCase);
            InitialiseOwnedFuelState(v, rec, purchasedFromDealer);
            ScheduleOwnedVehiclePlateApplication(rec, true);
            if (markOwnedVehiclesAsMissionCars) MarkVehicleAsMission(v);
            ownedVehicles.Add(rec);
            ownedVehiclesDirty = true;
            SaveOwnedVehicles();
            return rec;
        }

        private void UpdateOwnedVehicles()
        {
            bool restoreScanNow = ownedVehicleRestoreCooldownTicks <= 0;

            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null) continue;

                if (!OwnedRecordBelongsToCurrentEpisode(rec))
                {
                    ClearOwnedVehicleBlip(rec, "owned vehicle blip " + rec.Id);
                    continue;
                }

                if (rec.Destroyed)
                {
                    if (IsValidVehicle(rec.VehicleRef))
                    {
                        rec.WorldHandle = TryGetVehicleHandle(rec.VehicleRef);
                    }
                    else
                    {
                        rec.VehicleRef = null;
                        rec.WorldHandle = 0;
                    }
                    continue;
                }

                Vehicle v = rec.VehicleRef;
                if (!IsValidVehicle(v) && rec.WorldHandle != 0)
                {
                    Vehicle handleVehicle = VehicleFromHandle(rec.WorldHandle);
                    if (CanReclaimOwnedVehicleFromHandle(rec, handleVehicle))
                    {
                        rec.VehicleRef = handleVehicle;
                        v = handleVehicle;
                    }
                    else
                    {
                        rec.VehicleRef = null;
                        rec.WorldHandle = 0;
                    }
                }

                if (IsValidVehicle(v))
                {
                    if (rec.BlipRef != null && rec.BlipIsFallback) ClearOwnedVehicleBlip(rec, "owned vehicle fallback blip " + rec.Id);

                    if (IsVehicleDead(v))
                    {
                        rec.Destroyed = true;
                        ownedVehiclesDirty = true;
                        rec.WorldHandle = 0;
                        rec.VehicleRef = null;
                        ClearOwnedVehicleBlip(rec, "owned vehicle blip " + rec.Id);
                        Notify("Your registered " + GetVehicleDisplayName(rec.ModelName) + " was destroyed.");
                        Log("Owned vehicle destroyed: " + rec.Id + " " + rec.ModelName + ".");
                        continue;
                    }
                    if (markOwnedVehiclesAsMissionCars && refreshOwnedMissionFlagEachTick) MarkVehicleAsMission(v);
                    if (updateOwnedStateWhileDriving || !IsPlayerVehicle(v)) CaptureVehicleState(v, rec);
                    continue;
                }

                rec.VehicleRef = null;
                rec.WorldHandle = 0;

                if (restoreScanNow && IsPlayerNearOwnedVehicleSavedPosition(rec, OwnedVehicleRestoreRadius))
                {
                    if (!TryAdoptExistingOwnedVehicleFromWorld(rec)) SpawnOwnedVehicle(rec, false);
                }
            }

            if (ownedVehicleRestoreCooldownTicks > 0) ownedVehicleRestoreCooldownTicks--;
            else ownedVehicleRestoreCooldownTicks = OwnedVehicleRestoreIntervalTicks;
        }

        private bool CanReclaimOwnedVehicleFromHandle(OwnedVehicleRecord rec, Vehicle v)
        {
            if (rec == null || !IsValidVehicle(v)) return false;
            if (IsVehicleDead(v)) return false;
            if (!VehicleModelMatchesOwnedRecord(rec, v)) return false;
            if (!VehicleColoursMatchOwnedRecord(rec, v)) return false;
            return true;
        }

        private bool IsPlayerNearOwnedVehicleSavedPosition(OwnedVehicleRecord rec, float radius)
        {
            if (rec == null || !IsPlayerValid()) return false;
            try
            {
                Vector3 playerPos = Player.Character.Position;
                float dx = playerPos.X - rec.X;
                float dy = playerPos.Y - rec.Y;
                float dz = playerPos.Z - rec.Z;
                float r2 = radius * radius;
                return (dx * dx + dy * dy + dz * dz) <= r2;
            }
            catch { }
            return false;
        }

        private void ClearOwnedVehicleBlip(OwnedVehicleRecord rec, string purpose)
        {
            if (rec == null) return;
            if (rec.BlipRef != null)
            {
                RemoveBlipSafe(rec.BlipRef, purpose);
                rec.BlipRef = null;
            }
            rec.BlipIsFallback = false;
        }

        private void SpawnOwnedVehiclesFromSave()
        {
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (!OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (rec.Destroyed) continue;
                if (IsValidVehicle(rec.VehicleRef)) continue;
                if (TryAdoptExistingOwnedVehicleFromWorld(rec)) continue;
                SpawnOwnedVehicle(rec, false);
            }
        }

        private bool TryAdoptExistingOwnedVehicleFromWorld(OwnedVehicleRecord rec)
        {
            if (rec == null || rec.ModelName == null || rec.ModelName.Length == 0) return false;
            if (!OwnedRecordBelongsToCurrentEpisode(rec)) return false;
            try
            {
                MethodInfo method = typeof(World).GetMethod("GetAllVehicles", BindingFlags.Public | BindingFlags.Static, null, new Type[0], null);
                if (method == null) return false;
                object result = method.Invoke(null, null);
                Vehicle[] all = result as Vehicle[];
                if (all == null) return false;
                float r2 = OwnedVehicleAdoptionRadius * OwnedVehicleAdoptionRadius;
                Vehicle best = null;
                float bestD2 = r2;
                for (int i = 0; i < all.Length; i++)
                {
                    Vehicle v = all[i];
                    if (!CanAdoptExistingOwnedVehicleCandidate(rec, v)) continue;
                    float x; float y; float z;
                    if (!TryGetVehiclePosition(v, out x, out y, out z)) continue;
                    float dx = x - rec.X;
                    float dy = y - rec.Y;
                    float dz = z - rec.Z;
                    if (Math.Abs(dz) > OwnedVehicleAdoptionMaxZDelta) continue;
                    float d2 = dx * dx + dy * dy;
                    if (d2 > r2 || d2 > bestD2) continue;
                    best = v;
                    bestD2 = d2;
                }
                if (!IsValidVehicle(best)) return false;
                ClearOwnedVehicleBlip(rec, "owned vehicle fallback blip " + rec.Id);
                ApplyVehicleState(best, rec);
                SetVehicleOnGroundProperlyOnce(best, rec.Heading);
                CaptureVehicleState(best, rec);
                if (markOwnedVehiclesAsMissionCars) MarkVehicleAsMission(best);
                rec.VehicleRef = best;
                rec.WorldHandle = TryGetVehicleHandle(best);
                rec.BlipRef = null;
                rec.BlipIsFallback = false;
                rec.Destroyed = false;
                if (reapplyPlateOnOwnedVehicleSpawn) ScheduleOwnedVehiclePlateApplication(rec, true);
                Log("Adopted existing owned vehicle " + rec.Id + " model=" + rec.ModelName + ".");
                return true;
            }
            catch (Exception ex)
            {
                Log("Owned vehicle adoption scan failed: " + ex.GetType().Name + ".");
                return false;
            }
        }

        private bool CanAdoptExistingOwnedVehicleCandidate(OwnedVehicleRecord rec, Vehicle v)
        {
            if (rec == null || !IsValidVehicle(v)) return false;
            if (IsVehicleDead(v)) return false;
            if (IsPlayerVehicle(v)) return false;
            OwnedVehicleRecord owned = FindOwnedRecordForVehicle(v);
            if (owned != null && owned != rec) return false;
            bool? driverOccupied = IsDriverSeatOccupied(v);
            if (driverOccupied.HasValue && driverOccupied.Value) return false;
            if (!VehicleModelMatchesOwnedRecord(rec, v)) return false;
            if (!VehicleColoursMatchOwnedRecord(rec, v)) return false;
            return true;
        }

        private bool VehicleModelMatchesOwnedRecord(OwnedVehicleRecord rec, Vehicle v)
        {
            if (rec == null || !IsValidVehicle(v)) return false;
            int hash = TryGetVehicleModelHash(v);
            if (rec.ModelHash != 0 && hash != 0) return rec.ModelHash == hash;
            string model = GetModelNameForVehicleFast(v);
            if (model == null) model = "";
            return model.Equals(rec.ModelName == null ? "" : rec.ModelName, StringComparison.OrdinalIgnoreCase);
        }

        private bool VehicleColoursMatchOwnedRecord(OwnedVehicleRecord rec, Vehicle v)
        {
            if (rec == null || !IsValidVehicle(v)) return false;
            int c1; int c2; int c3; int c4;
            if (!TryGetVehicleColours(v, out c1, out c2, out c3, out c4)) return true;
            return c1 == rec.Colour1 && c2 == rec.Colour2 && c3 == rec.Colour3 && c4 == rec.Colour4;
        }

        private bool TryGetVehicleColours(Vehicle v, out int c1, out int c2, out int c3, out int c4)
        {
            c1 = 0; c2 = 0; c3 = 0; c4 = 0;
            if (!IsValidVehicle(v)) return false;
            try
            {
                c1 = v.Color.Index;
                c2 = v.SpecularColor.Index;
                c3 = v.FeatureColor1.Index;
                c4 = v.FeatureColor2.Index;
                return true;
            }
            catch { }
            return false;
        }

        private bool? IsDriverSeatOccupied(Vehicle v)
        {
            if (!IsValidVehicle(v)) return null;
            try
            {
                MethodInfo m = v.GetType().GetMethod("GetPedOnSeat");
                if (m != null)
                {
                    ParameterInfo[] ps = m.GetParameters();
                    if (ps != null && ps.Length == 1)
                    {
                        object driverSeat = null;
                        try { driverSeat = Enum.Parse(ps[0].ParameterType, "Driver"); } catch { }
                        if (driverSeat == null)
                        {
                            try { driverSeat = Enum.ToObject(ps[0].ParameterType, -1); } catch { }
                        }
                        if (driverSeat != null)
                        {
                            object result = m.Invoke(v, new object[] { driverSeat });
                            Ped driver = result as Ped;
                            return driver != null && driver.Exists();
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private void SpawnOwnedVehicle(OwnedVehicleRecord rec, bool forceNearRegistrationPoint)
        {
            if (rec == null || rec.ModelName == null || rec.ModelName.Length == 0) return;
            if (!OwnedRecordBelongsToCurrentEpisode(rec)) return;
            Vector3 pos = new Vector3(rec.X, rec.Y, rec.Z);
            if (forceNearRegistrationPoint && activeRegistrationPoint != null)
            {
                pos = new Vector3(activeRegistrationPoint.X + 4.0f, activeRegistrationPoint.Y, activeRegistrationPoint.Z + 0.2f);
            }
            Vehicle v = TryCreateVehicleByName(rec.ModelName, pos, rec.Heading);
            if (!IsValidVehicle(v))
            {
                Log("Could not spawn owned vehicle " + rec.Id + " model=" + rec.ModelName + ".");
                return;
            }
            ClearOwnedVehicleBlip(rec, "owned vehicle fallback blip " + rec.Id);
            ApplyVehicleState(v, rec);
            PlaceVehicleOnGroundSafe(v, rec.X, rec.Y, rec.Z, rec.Heading);
            CaptureVehicleState(v, rec);
            if (markOwnedVehiclesAsMissionCars) MarkVehicleAsMission(v);
            rec.VehicleRef = v;
            rec.WorldHandle = TryGetVehicleHandle(v);
            rec.BlipRef = null;
            rec.BlipIsFallback = false;
            rec.Destroyed = false;
            if (reapplyPlateOnOwnedVehicleSpawn) ScheduleOwnedVehiclePlateApplication(rec, true);
            Log("Spawned owned vehicle " + rec.Id + " model=" + rec.ModelName + ".");
        }


        private Vehicle VehicleFromHandle(int handle)
        {
            if (handle == 0) return null;
            try
            {
                ConstructorInfo ctor = typeof(Vehicle).GetConstructor(new Type[] { typeof(int) });
                if (ctor != null) return ctor.Invoke(new object[] { handle }) as Vehicle;
            }
            catch { }
            return null;
        }

        private bool IsServiceVehicleForRegistration(Vehicle v, RegistrationPoint point)
        {
            VehicleDefinition def = GetVehicleDefinition(v);
            if (def == null) return false;
            if (allowServiceVehicleRegistration) return false;
            if (def.IsEmergency && !allowEmergencyRegistration) return true;
            if (def.IsTaxi && !allowTaxiRegistration) return true;
            if (def.IsIndustrial && !allowIndustrialRegistration && (point == null || !point.AcceptIndustrial)) return true;
            return false;
        }

        private bool CanRegisterVehicle(Vehicle v, RegistrationPoint point, out string reason)
        {
            reason = "";
            if (!IsValidVehicle(v)) { reason = "No valid vehicle found."; return false; }
            if (IsVehicleAlreadyOwned(v)) { reason = "This vehicle is already registered."; return false; }
            if (requireNoWantedForStreetRegistration && Player.WantedLevel > 0) { reason = "Lose the wanted level first."; return false; }
            if (requireVehicleStoppedForRegistration && SafeVehicleSpeed(v) > 0.75f) { reason = "Stop the vehicle before registration."; return false; }
            if (IsVehicleDead(v)) { reason = "This vehicle cannot be registered."; return false; }
            VehicleDefinition def = GetVehicleDefinition(v);
            if (def != null)
            {
                if (IsServiceVehicleForRegistration(v, point)) { reason = "Service vehicles cannot be registered."; return false; }
                if (def.IsSports && point != null && !point.AcceptPremium) { reason = "This shop will not handle premium vehicles."; return false; }
            }
            string limitReason;
            if (!CanAddOwnedVehicle(out limitReason)) { reason = limitReason; return false; }
            return true;
        }

        private Vehicle FindVehicleForRegistration(RegistrationPoint point)
        {
            if (point == null) return null;
            Vehicle v = null;
            try
            {
                if (Player.Character.isSittingInVehicle()) v = Player.Character.CurrentVehicle;
            }
            catch { }
            if (IsValidVehicle(v)) return v;
            if (IsValidVehicle(lastPlayerVehicle) && Distance(lastPlayerVehicle.Position, new Vector3(point.X, point.Y, point.Z)) <= point.VehicleRadius) return lastPlayerVehicle;
            return null;
        }

        private void ResetDealerPromptCandidateCache()
        {
            dealerPromptCandidateCooldown = 0;
            dealerPromptCandidateDealer = null;
            dealerPromptCandidateSlotIndex = -1;
            dealerPromptCandidateVehicle = null;
            dealerPromptCandidateExists = false;
            dealerPromptCandidateSaleable = false;
        }

        private void RefreshDealerPromptCandidateCache(bool force)
        {
            if (!force && dealerPromptCandidateCooldown > 0 && IsValidVehicle(dealerPromptCandidateVehicle))
            {
                dealerPromptCandidateCooldown--;
                return;
            }

            dealerPromptCandidateCooldown = dealerPromptCandidateIntervalTicks;
            dealerPromptCandidateDealer = null;
            dealerPromptCandidateSlotIndex = -1;
            dealerPromptCandidateVehicle = null;
            dealerPromptCandidateExists = false;
            dealerPromptCandidateSaleable = false;

            DealerDefinition dealer;
            int slotIndex;
            Vehicle vehicle;
            if (!TryGetNearestDealerVehicleCandidate(out dealer, out slotIndex, out vehicle)) return;

            dealerPromptCandidateDealer = dealer;
            dealerPromptCandidateSlotIndex = slotIndex;
            dealerPromptCandidateVehicle = vehicle;
            dealerPromptCandidateExists = true;

            dealerPromptCandidateSaleable = IsDealerVehicleHealthyEnoughForSale(vehicle);
        }

        private bool TryGetNearestDealerVehicleCandidate(out DealerDefinition dealer, out int slotIndex, out Vehicle vehicle)
        {
            dealer = null;
            slotIndex = -1;
            vehicle = null;
            if (!IsPlayerValid()) return false;
            Vector3 p = Player.Character.Position;
            float bestDist = 999999.0f;
            float radius = Math.Max(1.25f, dealerVehicleInteractRadius);

            for (int d = 0; d < dealers.Count; d++)
            {
                DealerDefinition currentDealer = dealers[d];
                if (currentDealer == null || !currentDealer.Enabled || !currentDealer.UseCarSpecificInteraction) continue;
                if (currentDealer.Models == null || currentDealer.Models.Count == 0) continue;
                if (!IsPlayerNearDealerArea(currentDealer)) continue;

                foreach (KeyValuePair<string, Vehicle> pair in dealerSlotVehicles)
                {
                    if (pair.Key == null || !pair.Key.StartsWith(currentDealer.Id + ":")) continue;
                    Vehicle slotVehicle = pair.Value;
                    if (!IsValidVehicle(slotVehicle)) continue;
                    if (IsVehicleAlreadyOwned(slotVehicle)) continue;
                    if (IsVehicleDead(slotVehicle)) continue;
                    float vx; float vy; float vz;
                    if (!TryGetVehiclePosition(slotVehicle, out vx, out vy, out vz)) continue;
                    if (currentDealer.AdoptNativeVehicles && !IsInsideDealerAdoptionArea(currentDealer, vx, vy, vz)) continue;
                    float dx = p.X - vx;
                    float dy = p.Y - vy;
                    float dz = p.Z - vz;
                    float dist = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    if (dist <= radius && dist < bestDist)
                    {
                        dealer = currentDealer;
                        slotIndex = ParseDealerSlotIndex(pair.Key);
                        vehicle = slotVehicle;
                        bestDist = dist;
                    }
                }
            }
            return dealer != null && slotIndex >= 0 && IsValidVehicle(vehicle);
        }

        private DealerDefinition GetDealerBySlotKey(string key)
        {
            if (key == null) return null;
            int idx = key.IndexOf(':');
            if (idx <= 0) return null;
            string id = key.Substring(0, idx);
            for (int i = 0; i < dealers.Count; i++)
            {
                DealerDefinition d = dealers[i];
                if (d != null && d.Id == id) return d;
            }
            return null;
        }

        private int ParseDealerSlotIndex(string key)
        {
            if (key == null) return -1;
            int idx = key.LastIndexOf(':');
            if (idx < 0 || idx + 1 >= key.Length) return -1;
            int value = -1;
            try { value = Convert.ToInt32(key.Substring(idx + 1), CultureInfo.InvariantCulture); } catch { value = -1; }
            return value;
        }

        private string GetDealerModelForSaleSlot(DealerDefinition dealer, int slotIndex, Vehicle vehicle)
        {
            string model = "";
            if (dealer != null)
            {
                string key = GetDealerSlotKey(dealer, slotIndex);
                dealerSlotModels.TryGetValue(key, out model);
            }
            if ((model == null || model.Length == 0 || model == "unknown") && IsValidVehicle(vehicle)) model = GetModelNameForVehicle(vehicle).ToLowerInvariant();
            if (model == null) model = "";
            if (model.Length == 0 && dealer != null) model = GetDealerModelForSlot(dealer, slotIndex);
            return model.Trim().ToLowerInvariant();
        }

        private DealerDefinition GetNearestDealer()
        {
            if (!IsPlayerValid()) return null;
            Vector3 p = Player.Character.Position;
            DealerDefinition best = null;
            float bestDist = 999999.0f;
            for (int i = 0; i < dealers.Count; i++)
            {
                DealerDefinition d = dealers[i];
                if (!d.Enabled) continue;
                if (d.Models.Count == 0) continue;
                float dist = Distance(p, new Vector3(d.X, d.Y, d.Z));
                if (dist <= d.Radius && dist < bestDist)
                {
                    best = d;
                    bestDist = dist;
                }
            }
            return best;
        }

        private RegistrationPoint GetNearestRegistrationPoint()
        {
            if (!IsPlayerValid()) return null;
            Vector3 p = Player.Character.Position;
            RegistrationPoint best = null;
            float bestDist = 999999.0f;
            for (int i = 0; i < registrationPoints.Count; i++)
            {
                RegistrationPoint r = registrationPoints[i];
                if (!r.Enabled) continue;
                float dist = Distance(p, new Vector3(r.X, r.Y, r.Z));
                if (dist <= r.Radius && dist < bestDist)
                {
                    best = r;
                    bestDist = dist;
                }
            }
            return best;
        }

        private string KeyLabel(Keys key)
        {
            if (key == Keys.Back) return "Backspace";
            if (key == Keys.Enter || key == Keys.Return) return "Enter";
            if (key == Keys.Escape) return "Esc";
            if (key == Keys.Up) return "Up";
            if (key == Keys.Down) return "Down";
            if (key == Keys.Left) return "Left";
            if (key == Keys.Right) return "Right";
            if (key == Keys.Delete) return "Delete";
            if (key == Keys.Space) return "Space";
            return key.ToString();
        }

        private string DealerPaletteHint()
        {
            return KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + "/" + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + " choose colour   " + KeyLabel(keySelect) + " apply   " + KeyLabel(keyExit) + " return";
        }

        private string DealerMainHint(bool canChangeVehicle)
        {
            string prefix = canChangeVehicle ? KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + " choose   " + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + " change" : KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + " options";
            return prefix + "   " + KeyLabel(keySelect) + " buy/select   " + KeyLabel(keyExit) + " close";
        }

        private void DrawMenu()
        {
            if (menuMode == "dealer") DrawDealerMenu();
            else if (menuMode == "registration") DrawRegistrationMenu();
            else if (menuMode == "deregister") DrawDeregisterMenu();
            else if (menuMode == "deliver") DrawDeliverMenu();
            else if (menuMode == "plate") DrawCustomPlateInputMenu();
        }

        private void DrawDealerMenu()
        {
            if (activeDealer == null) { CloseMenu(false); return; }
            CreateOrRefreshDealerPreview();
            List<string> lines = new List<string>();
            List<bool> selectable = new List<bool>();
            string model = GetCurrentDealerModel();
            string modelLabel = GetVehicleDisplayName(model);
            if (dealerColourPaletteMode)
            {
                lines.Add("Vehicle: " + modelLabel); selectable.Add(false);
                lines.Add("Colour ID: " + dealerColourID.ToString()); selectable.Add(true);


                for (int pad = 0; pad < 10; pad++) { lines.Add(""); selectable.Add(false); }
                lines.Add("Use " + KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + "/" + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + " to choose a colour."); selectable.Add(false);
                lines.Add(KeyLabel(keySelect) + ": apply selected colour"); selectable.Add(false);
                lines.Add(KeyLabel(keyExit) + ": return"); selectable.Add(false);
                DrawPanel(activeDealer.Name.ToUpperInvariant(), "CAR DEALER", lines, selectable, DealerPaletteHint(), 0, 1);
                if (showDealerColourPalette) DrawDealerColourPalette(0.078f, 0.305f);
                return;
            }
            if (dealerForcedSlotIndex >= 100) lines.Add("Vehicle: " + modelLabel + " (showroom)");
            else if (activeDealer.DisplaySlots.Count > 0) lines.Add("Vehicle: " + modelLabel + " (slot " + (dealerModelIndex + 1).ToString() + "/" + activeDealer.DisplaySlots.Count.ToString() + ")");
            else lines.Add("Model: " + modelLabel);
            selectable.Add(dealerForcedSlotIndex < 0);
            lines.Add("Colour ID: " + dealerColourID.ToString()); selectable.Add(true);
            lines.Add("Insurance: " + (dealerInsuranceSelected ? "yes" : "no")); selectable.Add(true);
            if (dealerAvailableExtras.Count > 0)
            {
                for (int i = 0; i < dealerAvailableExtras.Count; i++)
                {
                    int extra = dealerAvailableExtras[i];
                    bool on = false;
                    dealerExtraStates.TryGetValue(extra, out on);
                    lines.Add("Extra " + extra.ToString() + ": " + (on ? "on" : "off")); selectable.Add(true);
                }
            }
            int vehiclePrice = GetDealerVehiclePrice(model, activeDealer);
            int insurancePrice = dealerInsuranceSelected ? GetInsurancePrice(model, true) : 0;
            int total = vehiclePrice + insurancePrice;
            lines.Add("Buy and register: $" + total.ToString()); selectable.Add(true);
            lines.Add("Cancel"); selectable.Add(true);
            int menuCount = GetDealerMenuCount();
            if (selectedIndex < 0) selectedIndex = 0;
            if (selectedIndex >= menuCount) selectedIndex = menuCount - 1;
            int visualSelected = selectedIndex;
            string hint = dealerForcedSlotIndex >= 0 ? DealerMainHint(false) : DealerMainHint(true);
            DrawPanel(activeDealer.Name.ToUpperInvariant(), "CAR DEALER", lines, selectable, hint, total, visualSelected);
        }

        private void DrawRegistrationMenu()
        {
            if (activeRegistrationPoint == null) { CloseMenu(false); return; }
            List<string> lines = new List<string>();
            List<bool> selectable = new List<bool>();
            List<string> actions = BuildRegistrationActions();
            if (actions.Count > 0)
            {
                if (selectedIndex < 0) selectedIndex = 0;
                if (selectedIndex >= actions.Count) selectedIndex = actions.Count - 1;
            }
            int visualSelected = selectedIndex;
            int total = 0;

            OwnedVehicleRecord owned = activeRegistrationOwnedRecord;
            if (owned == null && IsValidVehicle(activeRegistrationVehicle)) owned = FindOwnedRecordForVehicle(activeRegistrationVehicle);
            string model = activeRegistrationModelName;
            if (model == null || model.Length == 0)
            {
                if (IsValidVehicle(activeRegistrationVehicle)) model = GetModelNameForVehicle(activeRegistrationVehicle);
                else model = "";
            }

            for (int i = 0; i < actions.Count; i++)
            {
                string action = actions[i];
                if (action == "service_blocked")
                {
                    lines.Add("Service vehicles cannot be registered."); selectable.Add(false);
                }
                else if (action == "toggle_insurance")
                {
                    lines.Add("Insurance: " + (streetInsuranceSelected ? "yes" : "no")); selectable.Add(true);
                }
                else if (action == "register")
                {
                    int reg = IsValidVehicle(activeRegistrationVehicle) ? GetStreetRegistrationPrice(activeRegistrationVehicle, activeRegistrationPoint) : 0;
                    int ins = streetInsuranceSelected ? GetInsurancePrice(model, false) : 0;
                    total = reg + ins;
                    lines.Add("Register " + GetVehicleDisplayName(model) + ": $" + total.ToString()); selectable.Add(true);
                }
                else if (action == "insure")
                {
                    string existingModel = owned == null || owned.ModelName == null || owned.ModelName.Length == 0 ? model : owned.ModelName;
                    total = GetInsurancePrice(existingModel, false);
                    lines.Add("Buy insurance for " + GetVehicleDisplayName(existingModel) + ": $" + total.ToString()); selectable.Add(true);
                }
                else if (action == "plate_current")
                {
                    string plate = owned == null ? "" : CleanPlateText(owned.PlateText);
                    if (plate.Length == 0) plate = "not assigned";
                    lines.Add("Order custom plate: " + plate + " ($" + GetCustomPlatePrice(owned).ToString() + ")"); selectable.Add(true);
                }
                else if (action == "plate_unavailable")
                {
                    lines.Add("Custom plate unavailable (requires LibertyCityPlates)"); selectable.Add(false);
                }
                else if (action == "recover")
                {
                    OwnedVehicleRecord recover = FindFirstRecoverableOwnedVehicle();
                    int recoverable = CountRecoverableOwnedVehicles();
                    if (recover == null) lines.Add("Recover insured vehicle: none");
                    else
                    {
                        string label = "Recover " + GetVehicleDisplayName(recover.ModelName) + ": $" + GetRecoveryFee(recover).ToString();
                        if (recoverable > 1) label += " (" + recoverable.ToString() + " waiting)";
                        lines.Add(label);
                    }
                    selectable.Add(true);
                }
                else if (action == "deliver")
                {
                    int deliverable = CountStevieDeliverableOwnedVehicles();
                    lines.Add("Deliver registered vehicle: $" + GetStevieDeliveryFee(null).ToString() + " (" + deliverable.ToString() + " available)"); selectable.Add(true);
                }
                else if (action == "deregister")
                {
                    lines.Add("Cancel registration..."); selectable.Add(true);
                }
                else if (action == "close")
                {
                    lines.Add("Close"); selectable.Add(true);
                }
            }

            if (lines.Count == 0)
            {
                lines.Add("No registration services available."); selectable.Add(false);
                lines.Add("Close"); selectable.Add(true);
                visualSelected = 1;
            }

            string subtitle = activeRegistrationPoint.Name == null ? "REGISTRATION" : activeRegistrationPoint.Name.ToUpperInvariant();
            string hint = KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + " choose   " + KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + " change   " + KeyLabel(keySelect) + " select   " + KeyLabel(keyExit) + " close";
            DrawPanel("LIBERTY VEHICLE SERVICES", subtitle, lines, selectable, hint, total, visualSelected);
        }

        private void DrawDeliverMenu()
        {
            List<OwnedVehicleRecord> list = GetStevieDeliverableOwnedVehicleList();
            List<string> lines = new List<string>();
            List<bool> selectable = new List<bool>();
            for (int i = 0; i < list.Count; i++)
            {
                OwnedVehicleRecord rec = list[i];
                string plate = CleanPlateText(rec.PlateText);
                string text = GetVehicleDisplayName(rec.ModelName);
                if (plate.Length > 0) text += " [" + plate + "]";
                text += " - deliver + full tank ($" + GetStevieDeliveryFee(rec).ToString() + ")";
                lines.Add(text); selectable.Add(true);
            }
            if (list.Count == 0)
            {
                lines.Add("No registered vehicles available."); selectable.Add(false);
            }
            lines.Add("Back"); selectable.Add(true);
            int total = lines.Count;
            if (selectedIndex < 0) selectedIndex = 0;
            if (selectedIndex >= total) selectedIndex = total - 1;
            string hint = KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + " choose   " + KeyLabel(keySelect) + " deliver   " + KeyLabel(keyExit) + " back";
            DrawPanel("LIBERTY VEHICLE SERVICES", "STEVIE'S YARD", lines, selectable, hint, 0, selectedIndex);
        }

        private void DrawDeregisterMenu()
        {
            List<OwnedVehicleRecord> list = GetCurrentEpisodeOwnedVehicleList(true);
            List<string> lines = new List<string>();
            List<bool> selectable = new List<bool>();
            for (int i = 0; i < list.Count; i++)
            {
                OwnedVehicleRecord rec = list[i];
                string plate = CleanPlateText(rec.PlateText);
                string text = GetVehicleDisplayName(rec.ModelName);
                if (plate.Length > 0) text += " [" + plate + "]";
                if (rec.Destroyed) text += " - destroyed";
                else if (rec.Insured) text += " - insured";
                lines.Add(text); selectable.Add(true);
            }
            if (list.Count == 0)
            {
                lines.Add("No registered vehicles in this episode."); selectable.Add(false);
            }
            lines.Add("Back"); selectable.Add(true);
            int visualSelected = selectedIndex;
            if (list.Count == 0) visualSelected = lines.Count - 1;
            string subtitle = activeRegistrationPoint == null || activeRegistrationPoint.Name == null ? "CANCEL REGISTRATION" : activeRegistrationPoint.Name.ToUpperInvariant();
            DrawPanel("CANCEL REGISTRATION", subtitle, lines, selectable, KeyLabel(keySelect) + " cancel selected registration   " + KeyLabel(keyExit) + " back", 0, visualSelected);
        }

        private void DrawCustomPlateInputMenu()
        {
            List<string> lines = new List<string>();
            List<bool> selectable = new List<bool>();
            OwnedVehicleRecord rec = customPlateTarget;
            string current = rec == null ? "" : DisplayLcpPlateText(CleanPlateText(rec.PlateText));
            LcpPlateDefinition def = GetSelectedCustomPlateDefinition();
            LcpPlatePreview preview = def == null ? null : BuildLcpPlatePreview(def, customPlateInput, false);
            string typed = customPlateInput == null || customPlateInput.Length == 0 ? "_" : customPlateInput;
            string previewText = preview == null || preview.Display == null || preview.Display.Length == 0 ? "_" : preview.Display;
            bool lockedType = customPlateChoices == null || customPlateChoices.Count <= 1;

            lines.Add("Current: " + (current.Length == 0 ? "not assigned" : current)); selectable.Add(false);
            lines.Add((lockedType ? "Plate type: " : "Plate type: < ") + (def == null ? "none" : def.Name) + (lockedType ? "" : " >")); selectable.Add(!lockedType);
            lines.Add("Pattern: " + (def == null ? "none" : FormatPlatePatternForUi(def.Format))); selectable.Add(false);
            lines.Add("Input: [" + typed + "]"); selectable.Add(true);
            lines.Add("Preview: " + previewText); selectable.Add(false);
            if (preview != null)
            {
                string req = GetPlateRequirementText(preview);
                if (req.Length > 0) { lines.Add(req); selectable.Add(false); }
                lines.Add(GetPlateMissingText(preview)); selectable.Add(false);
            }
            lines.Add("Price: $" + GetCustomPlatePrice(rec).ToString()); selectable.Add(false);
            lines.Add(KeyLabel(keyBack) + " delete   " + KeyLabel(Keys.Delete) + " clear"); selectable.Add(false);

            int visualSelected = customPlateFieldIndex == 0 && !lockedType ? 1 : 3;
            string footer = KeyLabel(keyUp) + "/" + KeyLabel(keyDown) + " field   ";
            if (!lockedType) footer += KeyLabel(keyLeft) + "/" + KeyLabel(keyRight) + " type   ";
            footer += KeyLabel(keySelect) + " order   " + KeyLabel(keyExit) + " back";
            DrawPanel("CUSTOM PLATE", activeRegistrationPoint == null || activeRegistrationPoint.Name == null ? "REGISTRATION" : activeRegistrationPoint.Name.ToUpperInvariant(), lines, selectable, footer, GetCustomPlatePrice(rec), visualSelected);
        }

        private void DrawPanel(string title, string subtitle, List<string> lines, List<bool> selectable, string hint, int total)
        {
            DrawPanel(title, subtitle, lines, selectable, hint, total, selectedIndex);
        }

        private void DrawPanel(string title, string subtitle, List<string> lines, List<bool> selectable, string hint, int total, int visualSelectedIndex)
        {
            try
            {
                float x = 0.055f;
                float y = 0.135f;
                float w = 0.390f;
                float headerH = 0.066f;
                float rowH = 0.034f;
                float bodyH = 0.090f + Math.Max(1, lines.Count) * rowH + 0.055f;
                if (bodyH < 0.260f) bodyH = 0.260f;
                if (bodyH > 0.720f) bodyH = 0.720f;

                DrawScreenRect(x + w * 0.5f, y + headerH * 0.5f, w, headerH, 0, 0, 0, 225);
                DrawScreenRect(x + w * 0.5f, y + headerH + bodyH * 0.5f, w, bodyH, 0, 0, 0, 170);
                DrawScreenRect(x + w * 0.5f, y + headerH + 0.014f, w, 0.003f, 255, 255, 255, 120);
                DrawScreenText(x + 0.018f, y + 0.007f, title, 0.305f, 255, 255, 255, 255, false);
                DrawScreenText(x + 0.019f, y + 0.040f, subtitle, 0.205f, 210, 210, 210, 230, false);
                DrawScreenText(x + w - 0.115f, y + 0.040f, "$" + Player.Money.ToString(), 0.220f, 210, 210, 210, 230, false);

                float rowY = y + headerH + 0.026f;
                for (int i = 0; i < lines.Count; i++)
                {
                    bool isSelectable = i < selectable.Count && selectable[i];
                    bool selected = isSelectable && i == visualSelectedIndex;
                    if (selected) DrawScreenRect(x + w * 0.5f, rowY + 0.014f, w - 0.020f, rowH * 0.82f, 255, 255, 255, 70);
                    int r = selected ? 255 : (isSelectable ? 215 : 155);
                    int g = selected ? 255 : (isSelectable ? 215 : 155);
                    int b = selected ? 255 : (isSelectable ? 215 : 155);
                    string prefix = selected ? "> " : "  ";
                    DrawScreenText(x + 0.018f, rowY, prefix + lines[i], 0.225f, r, g, b, 245, false);
                    rowY += rowH;
                }
                DrawScreenText(x + 0.018f, y + headerH + bodyH - 0.045f, hint, 0.190f, 175, 175, 175, 220, false);
                if (messageTimer > 0 && messageText.Length > 0) DrawScreenText(x + 0.018f, y + headerH + bodyH - 0.022f, messageText, 0.190f, 235, 235, 235, 230, false);
            }
            catch
            {
                try { Game.DisplayText(title + "\n" + subtitle); } catch { }
            }
        }

        private void DrawSmallPrompt(string text)
        {
            try
            {
                float x = 0.050f;
                float y = 0.052f;
                float w = 0.285f;
                float h = 0.045f;
                DrawScreenRect(x + w * 0.5f, y + h * 0.5f, w, h, 0, 0, 0, 178);
                DrawScreenTextStyled(x + 0.009f, y + 0.010f, text, 0.185f, 0.330f, 245, 245, 245, 245, false);
            }
            catch { try { Game.DisplayText(text); } catch { } }
        }

        private void DrawScreenRect(float x, float y, float width, float height, int r, int g, int b, int a)
        {
            try { Function.Call("DRAW_RECT", x, y, width, height, r, g, b, a); } catch { }
        }

        private void DrawScreenText(float x, float y, string text, float scale, int r, int g, int b, int a, bool centre)
        {
            DrawScreenTextStyled(x, y, text, scale * 0.74f, scale * 1.10f, r, g, b, a, centre);
        }

        private void DrawScreenTextStyled(float x, float y, string text, float scaleX, float scaleY, int r, int g, int b, int a, bool centre)
        {
            if (text == null) text = "";
            if (text.Length > 120) text = text.Substring(0, 120);
            try { Function.Call("SET_TEXT_FONT", 0); } catch { }
            try { Function.Call("SET_TEXT_PROPORTIONAL", true); } catch { }
            try { Function.Call("SET_TEXT_SCALE", scaleX, scaleY); } catch { }
            try { Function.Call("SET_TEXT_COLOUR", r, g, b, a); } catch { }
            try { Function.Call("SET_TEXT_DROPSHADOW", 1, 0, 0, 0, 190); } catch { }
            try { Function.Call("SET_TEXT_EDGE", 1, 0, 0, 0, 150); } catch { }
            try { Function.Call("SET_TEXT_CENTRE", centre); } catch { }
            try { Function.Call("DISPLAY_TEXT_WITH_LITERAL_STRING", x, y, "STRING", text); } catch { }
        }


        private void DrawDealerColourPalette(float startX, float startY)
        {
            try
            {
                int columns = Math.Max(4, dealerColourPaletteColumns);
                float cellW = Math.Max(0.006f, dealerColourPaletteCellWidth);
                float cellH = Math.Max(0.006f, dealerColourPaletteCellHeight);
                float gap = Math.Max(0.0f, dealerColourPaletteGap);
                int count = Math.Max(1, maxColourID - minColourID + 1);

                DrawScreenText(startX, startY - 0.024f, "COLOUR PALETTE", 0.190f, 175, 175, 175, 220, false);
                for (int offset = 0; offset < count; offset++)
                {
                    int id = minColourID + offset;
                    int col = offset % columns;
                    int row = offset / columns;
                    float cx = startX + col * (cellW + gap) + cellW * 0.5f;
                    float cy = startY + row * (cellH + gap) + cellH * 0.5f;
                    int r, g, b;
                    GetDealerApproximateVehicleColour(id, out r, out g, out b);
                    if (id == dealerColourID)
                    {
                        DrawScreenRect(cx, cy, cellW + 0.006f, cellH + 0.006f, 255, 255, 255, 230);
                        DrawScreenRect(cx, cy, cellW + 0.003f, cellH + 0.003f, 0, 0, 0, 210);
                    }
                    DrawScreenRect(cx, cy, cellW, cellH, r, g, b, 235);
                }
            }
            catch { }
        }

        private void GetDealerApproximateVehicleColour(int id, out int r, out int g, out int b)
        {
            if (dealerVehicleColourPaletteLoaded && id >= 0 && id < dealerVehicleColourR.Length)
            {
                r = dealerVehicleColourR[id];
                g = dealerVehicleColourG[id];
                b = dealerVehicleColourB[id];
                return;
            }
            int shade = 55 + (Math.Abs(id) % 10) * 18;
            r = ClampByte(shade);
            g = ClampByte(shade);
            b = ClampByte(shade);
        }

        private int ClampByte(int value)
        {
            if (value < 0) return 0;
            if (value > 255) return 255;
            return value;
        }

        private void LoadDealerVehicleColourPaletteFromCarcols()
        {
            try
            {
                string[] paths = new string[] { "update\\common\\data\\carcols.dat", "common\\data\\carcols.dat" };
                for (int i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];
                    if (!File.Exists(path)) continue;
                    int loaded = LoadDealerVehicleColourPaletteFromFile(path);
                    if (loaded > 0)
                    {
                        dealerVehicleColourPaletteLoaded = true;
                        Log("Loaded dealer vehicle colour palette from " + path + " (" + loaded.ToString() + " colours).");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Log("Dealer vehicle colour palette load failed: " + ex.GetType().Name + ".");
            }
        }

        private int LoadDealerVehicleColourPaletteFromFile(string path)
        {
            int count = 0;
            string[] lines = File.ReadAllLines(path);
            int sequentialId = 0;
            bool sawCarcolsHeader = false;
            bool inCarcolsSection = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = StripDataComment(lines[i]).Trim();
                if (line.Length == 0) continue;
                string lower = line.ToLowerInvariant();
                if (lower == "carcols" || lower.StartsWith("carcols ") || lower.StartsWith("carcols,"))
                {
                    sawCarcolsHeader = true;
                    inCarcolsSection = true;
                    continue;
                }
                if (sawCarcolsHeader)
                {
                    if (lower == "end")
                    {
                        if (inCarcolsSection) break;
                        continue;
                    }
                    if (!inCarcolsSection) continue;
                }
                else
                {
                    if (lower == "end") break;
                    if (lower == "car4" || lower == "car3" || lower == "car2" || (lower.StartsWith("car") && lower.Length <= 6)) continue;
                }
                string[] raw = line.Replace("\t", " ").Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (raw.Length < 3) continue;
                int cursor = 0;
                int explicitId = -1;
                if (raw.Length >= 4)
                {
                    int possibleId;
                    if (Int32.TryParse(raw[0], out possibleId))
                    {
                        int rr, gg, bb;
                        if (Int32.TryParse(raw[1], out rr) && Int32.TryParse(raw[2], out gg) && Int32.TryParse(raw[3], out bb))
                        {
                            explicitId = possibleId;
                            cursor = 1;
                        }
                    }
                }
                int r, g, b;
                if (!Int32.TryParse(raw[cursor], out r)) continue;
                if (!Int32.TryParse(raw[cursor + 1], out g)) continue;
                if (!Int32.TryParse(raw[cursor + 2], out b)) continue;
                int id = explicitId >= 0 ? explicitId : sequentialId;
                sequentialId++;
                if (id < 0 || id >= dealerVehicleColourR.Length) continue;
                dealerVehicleColourR[id] = ClampByte(r);
                dealerVehicleColourG[id] = ClampByte(g);
                dealerVehicleColourB[id] = ClampByte(b);
                count++;
            }
            return count;
        }

        private float SafeGetPedHeadingPropertyOnly(Ped ped, float fallback)
        {
            if (ped == null) return fallback;
            try
            {
                object value = ped.GetType().GetProperty("Heading").GetValue(ped, null);
                if (value != null) return Convert.ToSingle(value);
            }
            catch { }
            return fallback;
        }


        private bool TryGetVehiclePositionPropertyOnly(Vehicle v, out float x, out float y, out float z)
        {
            x = 0.0f; y = 0.0f; z = 0.0f;
            if (!IsValidVehicle(v)) return false;
            try
            {
                Vector3 p = v.Position;
                x = p.X; y = p.Y; z = p.Z;
                return true;
            }
            catch { }
            return false;
        }

        private float SafeGetVehicleHeadingPropertyOnly(Vehicle v, float fallback)
        {
            if (!IsValidVehicle(v)) return fallback;
            try
            {
                object value = v.GetType().GetProperty("Heading").GetValue(v, null);
                if (value != null) return Convert.ToSingle(value);
            }
            catch { }
            return fallback;
        }

        private string GetVehicleDisplayName(string model)
        {
            if (model == null || model.Length == 0) return "Unknown";
            string key = NormaliseVehicleLookupKey(model);
            if (key.Length == 0) return "Unknown";

            VehicleDefinition def = ResolveVehicleDefinitionByLookupKey(key);
            if (def != null)
            {
                string gxt = TryGetGxtLabel(def.GameName);
                if (IsUsableVehicleDisplayText(gxt)) return TitleCaseToken(gxt);

                string native = TryGetNativeVehicleLabel(def.Hash);
                if (IsUsableVehicleDisplayText(native) && !IsBareVehicleTokenForDefinition(native, def)) return TitleCaseToken(native);

                if (def.GameName != null && def.GameName.Length > 0 && def.GameName.ToLowerInvariant() != def.ModelName) return TitleCaseToken(def.GameName);
            }
            if (key.StartsWith("hash_")) return "Unknown";
            return TitleCaseToken(key);
        }

        private string NormaliseVehicleLookupKey(string value)
        {
            if (value == null) return "";
            return value.Trim().ToLowerInvariant();
        }

        private VehicleDefinition ResolveVehicleDefinitionByLookupKey(string key)
        {
            key = NormaliseVehicleLookupKey(key);
            if (key.Length == 0) return null;
            VehicleDefinition def;
            if (vehicleByModel.TryGetValue(key, out def)) return def;
            string modelKey;
            if (vehicleModelAliases.TryGetValue(key, out modelKey))
            {
                if (modelKey != null && modelKey.Length > 0 && vehicleByModel.TryGetValue(modelKey, out def)) return def;
            }
            return null;
        }

        private bool IsBareVehicleTokenForDefinition(string text, VehicleDefinition def)
        {
            if (text == null || def == null) return false;
            string key = NormaliseVehicleLookupKey(text);
            if (key.Length == 0) return false;
            if (key == NormaliseVehicleLookupKey(def.ModelName)) return true;
            if (key == NormaliseVehicleLookupKey(def.GameName)) return true;
            if (key == NormaliseVehicleLookupKey(def.HandlingName)) return true;
            return false;
        }

        private bool IsUsableVehicleDisplayText(string text)
        {
            if (text == null) return false;
            string clean = text.Trim();
            if (clean.Length == 0) return false;
            string lower = clean.ToLowerInvariant();
            return lower != "null" && lower != "unknown";
        }

        private string TryGetGxtLabel(string label)
        {
            if (label == null) return "";
            label = label.Trim();
            if (label.Length == 0) return "";
            try
            {
                string text = Function.Call<string>("GET_STRING_FROM_TEXT_FILE", label);
                if (IsUsableVehicleDisplayText(text)) return text;
            }
            catch { }
            return "";
        }

        private string TryGetNativeVehicleLabel(int hash)
        {
            if (hash == 0) return "";
            try
            {
                string label = Function.Call<string>("GET_DISPLAY_NAME_FROM_VEHICLE_MODEL", hash);
                if (label == null || label.Length == 0) return "";
                string text = TryGetGxtLabel(label);
                if (IsUsableVehicleDisplayText(text)) return text;
                return label;
            }
            catch { return ""; }
        }

        private string TitleCaseToken(string text)
        {
            if (text == null) return "Unknown";
            text = text.Trim();
            if (text.Length == 0) return "Unknown";
            text = text.Replace('_', ' ').Replace('-', ' ');
            string[] parts = text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i].ToLowerInvariant();
                if (part.Length == 1) parts[i] = part.ToUpperInvariant();
                else parts[i] = part.Substring(0, 1).ToUpperInvariant() + part.Substring(1);
            }
            return String.Join(" ", parts);
        }

        private int GetDealerVehiclePrice(string model, DealerDefinition dealer)
        {
            int baseValue = GetVehicleBaseValue(model);
            float markup = dealer == null ? dealerDefaultMarkup : dealer.Markup;
            if (markup <= 0.0f) markup = dealerDefaultMarkup;
            float price = baseValue * markup;
            if (dealer != null && activeDealer == dealer && dealerForcedSlotIndex >= 0)
            {
                string slotKey = GetDealerSlotKey(dealer, dealerForcedSlotIndex);
                if (slotKey.Length > 0) price *= GetOrCreateDealerSlotPriceFactor(slotKey);
                if (IsValidVehicle(dealerPreviewVehicle)) price *= GetDealerSaleConditionPriceFactor(dealerPreviewVehicle);
            }
            price += GetSelectedDealerExtrasCost(model);
            int value = (int)Math.Round(price);
            if (value < minimumDealerPrice) value = minimumDealerPrice;
            return value;
        }


        private float GetDealerSaleConditionPriceFactor(Vehicle v)
        {
            if (!IsValidVehicle(v)) return 1.0f;
            float body = SafeVehicleHealth(v) / 1000.0f;
            float engine = SafeEngineHealth(v) / 1000.0f;
            float condition = body;
            if (engine < condition) condition = engine;
            if (condition > 1.0f) condition = 1.0f;
            if (condition < 0.0f) condition = 0.0f;
            float floor = dealerSaleDamagedPriceFloorFactor;
            if (floor < 0.10f) floor = 0.10f;
            if (floor > 1.0f) floor = 1.0f;
            return floor + (1.0f - floor) * condition;
        }

        private int GetSelectedDealerExtrasCost(string model)
        {
            int count = 0;
            foreach (KeyValuePair<int, bool> pair in dealerExtraStates)
            {
                if (pair.Value) count++;
            }
            if (count <= 0) return 0;
            return GetDealerExtraInstallPrice(model) * count;
        }

        private int GetDealerExtraInstallPrice(string model)
        {
            int swankness = GetModelSwankness(model);
            float multiplier = GetDealerExtraMultiplierForSwankness(swankness);
            int value = (int)Math.Round(dealerExtraBasePrice * multiplier);
            if (value < 0) value = 0;
            return value;
        }

        private int GetModelSwankness(string model)
        {
            if (model == null) return 2;
            string key = NormaliseVehicleLookupKey(model);
            int overrideValue;
            if (swanknessOverrides.TryGetValue(key, out overrideValue)) return overrideValue;
            VehicleDefinition def = ResolveVehicleDefinitionByLookupKey(key);
            if (def != null)
            {
                if (swanknessOverrides.TryGetValue(def.ModelName, out overrideValue)) return overrideValue;
                return def.Swankness;
            }
            return 2;
        }

        private float GetDealerExtraMultiplierForSwankness(int swankness)
        {
            if (swankness <= 0) return dealerExtraSwankness0Multiplier;
            if (swankness == 1) return dealerExtraSwankness1Multiplier;
            if (swankness == 2) return dealerExtraSwankness2Multiplier;
            if (swankness == 3) return dealerExtraSwankness3Multiplier;
            if (swankness == 4) return dealerExtraSwankness4Multiplier;
            return dealerExtraSwankness5PlusMultiplier;
        }

        private int GetStreetRegistrationPrice(Vehicle v, RegistrationPoint point)
        {
            string model = GetModelNameForVehicle(v);
            int baseValue = GetVehicleBaseValue(model);
            float multiplier = point == null || point.PriceMultiplier <= 0.0f ? registrationStreetMultiplier : point.PriceMultiplier;
            int value = (int)Math.Round(baseValue * multiplier);
            if (value < minimumStreetRegistrationPrice) value = minimumStreetRegistrationPrice;
            return value;
        }

        private int GetInsurancePrice(string model, bool dealer)
        {
            int baseValue = GetVehicleBaseValue(model);
            float multiplier = dealer ? insuranceDealerMultiplier : insuranceStreetMultiplier;
            int value = (int)Math.Round(baseValue * multiplier);
            if (value < minimumInsurancePrice) value = minimumInsurancePrice;
            return value;
        }

        private int GetRecoveryFee(OwnedVehicleRecord rec)
        {
            if (rec == null) return 0;
            int value = GetVehicleBaseValue(rec.ModelName);
            float multiplier = rec.Insured ? recoveryInsuredFeeMultiplier : recoveryUninsuredFeeMultiplier;
            int fee = (int)Math.Round(value * multiplier);
            if (fee < 0) fee = 0;
            return fee;
        }

        private int GetVehicleBaseValue(string model)
        {
            if (model == null) return defaultVehicleValue;
            string key = NormaliseVehicleLookupKey(model);
            int overrideValue;
            if (priceOverrides.TryGetValue(key, out overrideValue) && overrideValue > 0) return overrideValue;
            VehicleDefinition def = ResolveVehicleDefinitionByLookupKey(key);
            if (def != null)
            {
                if (priceOverrides.TryGetValue(def.ModelName, out overrideValue) && overrideValue > 0) return overrideValue;
                if (def.MonetaryValue > 0) return def.MonetaryValue;
            }
            return defaultVehicleValue;
        }

        private VehicleDefinition GetVehicleDefinition(Vehicle v)
        {
            int hash = TryGetVehicleModelHash(v);
            VehicleDefinition def;
            if (hash != 0 && vehicleByHash.TryGetValue(hash, out def)) return def;
            string model = GetModelNameForVehicle(v);
            if (model != null)
            {
                def = ResolveVehicleDefinitionByLookupKey(model);
                if (def != null) return def;
            }
            return null;
        }

        private VehicleDefinition GetVehicleDefinitionByHashFast(Vehicle v)
        {
            int hash = TryGetVehicleModelHash(v);
            VehicleDefinition def;
            if (hash != 0 && vehicleByHash.TryGetValue(hash, out def)) return def;
            return null;
        }

        private string GetModelNameForVehicleFast(Vehicle v)
        {
            VehicleDefinition def = GetVehicleDefinitionByHashFast(v);
            if (def != null) return def.ModelName;
            int hash = TryGetVehicleModelHash(v);
            return hash == 0 ? "unknown" : "hash_" + hash.ToString();
        }

        private string GetModelNameForVehicle(Vehicle v)
        {
            VehicleDefinition def = GetVehicleDefinitionByHashOnly(v);
            if (def != null) return def.ModelName;
            int hash = TryGetVehicleModelHash(v);
            return hash == 0 ? "unknown" : "hash_" + hash.ToString();
        }

        private VehicleDefinition GetVehicleDefinitionByHashOnly(Vehicle v)
        {
            int hash = TryGetVehicleModelHash(v);
            VehicleDefinition def;
            if (hash != 0 && vehicleByHash.TryGetValue(hash, out def)) return def;
            return FindVehicleDefinitionByNativeModelCheck(v);
        }

        private VehicleDefinition FindVehicleDefinitionByNativeModelCheck(Vehicle v)
        {
            if (!IsValidVehicle(v) || vehicleByModel.Count == 0) return null;
            try
            {
                foreach (VehicleDefinition def in vehicleByModel.Values)
                {
                    if (def == null || def.Hash == 0) continue;
                    try { if (Function.Call<bool>("IS_CAR_MODEL", v, def.Hash)) return def; } catch { }
                }
            }
            catch { }
            return null;
        }

        private void CaptureVehicleState(Vehicle v, OwnedVehicleRecord rec)
        {
            if (!IsValidVehicle(v) || rec == null) return;
            int oldColour1 = rec.Colour1;
            int oldColour2 = rec.Colour2;
            int oldColour3 = rec.Colour3;
            int oldColour4 = rec.Colour4;
            int oldHealth = rec.Health;
            int oldEngineHealth = rec.EngineHealth;
            int oldPetrolTankHealth = rec.PetrolTankHealth;
            float oldDirt = rec.DirtLevel;
            float oldX = rec.X;
            float oldY = rec.Y;
            float oldZ = rec.Z;
            float oldHeading = rec.Heading;
            string oldExtras = rec.Extras;
            bool oldFuelKnown = rec.FuelKnown;
            float oldFuel = rec.FuelGallons;

            try { rec.Colour1 = v.Color.Index; } catch { }
            try { rec.Colour2 = v.SpecularColor.Index; } catch { }
            try { rec.Colour3 = v.FeatureColor1.Index; } catch { }
            try { rec.Colour4 = v.FeatureColor2.Index; } catch { }
            try { rec.Health = v.Health; } catch { }
            try { rec.EngineHealth = (int)v.EngineHealth; } catch { }
            try { rec.PetrolTankHealth = (int)v.PetrolTankHealth; } catch { }
            if (saveVehicleDirtLevel)
            {
                int now = Environment.TickCount;
                if (rec.DirtCaptureNextMs <= 0 || now >= rec.DirtCaptureNextMs)
                {
                    rec.DirtLevel = SafeGetVehicleDirtLevel(v, rec.DirtLevel);
                    rec.DirtCaptureNextMs = now + 5000;
                }
            }
            float x, y, z;
            if (TryGetVehiclePosition(v, out x, out y, out z))
            {
                rec.X = x; rec.Y = y; rec.Z = z;
            }
            rec.Heading = SafeGetVehicleHeading(v, rec.Heading);
            rec.Extras = CaptureExtras(v);
            rec.WorldHandle = TryGetVehicleHandle(v);
            CaptureOwnedFuelState(v, rec);

            if (oldColour1 != rec.Colour1 || oldColour2 != rec.Colour2 || oldColour3 != rec.Colour3 || oldColour4 != rec.Colour4 ||
                Math.Abs(oldHealth - rec.Health) > 2 || Math.Abs(oldEngineHealth - rec.EngineHealth) > 2 || Math.Abs(oldPetrolTankHealth - rec.PetrolTankHealth) > 2 ||
                Math.Abs(oldX - rec.X) > 0.25f || Math.Abs(oldY - rec.Y) > 0.25f || Math.Abs(oldZ - rec.Z) > 0.25f || Math.Abs(oldHeading - rec.Heading) > 0.5f ||
                String.Compare(oldExtras == null ? "" : oldExtras, rec.Extras == null ? "" : rec.Extras, StringComparison.Ordinal) != 0 ||
                (saveVehicleDirtLevel && Math.Abs(oldDirt - rec.DirtLevel) > 0.05f) || oldFuelKnown != rec.FuelKnown || Math.Abs(oldFuel - rec.FuelGallons) > 0.05f)
            {
                ownedVehiclesDirty = true;
            }
        }

        private void ApplyVehicleState(Vehicle v, OwnedVehicleRecord rec)
        {
            if (!IsValidVehicle(v) || rec == null) return;
            try { v.Color = new ColorIndex(rec.Colour1); } catch { }
            try { v.SpecularColor = new ColorIndex(rec.Colour2); } catch { }
            try { v.FeatureColor1 = new ColorIndex(rec.Colour3); } catch { }
            try { v.FeatureColor2 = new ColorIndex(rec.Colour4); } catch { }
            ApplyExtras(v, rec.Extras);
            try { v.Health = rec.Health <= 0 ? 1000 : rec.Health; } catch { }
            try { v.EngineHealth = rec.EngineHealth <= 0 ? 1000 : rec.EngineHealth; } catch { }
            try { v.PetrolTankHealth = rec.PetrolTankHealth <= 0 ? 1000 : rec.PetrolTankHealth; } catch { }
            if (saveVehicleDirtLevel && rec.DirtLevel >= 0.0f) SafeSetVehicleDirtLevel(v, rec.DirtLevel);
            PlaceVehicleOnGroundSafe(v, rec.X, rec.Y, rec.Z, rec.Heading);
            InitialiseOwnedFuelState(v, rec, false);
        }

        private float SafeGetVehicleDirtLevel(Vehicle v, float fallback)
        {
            if (!IsValidVehicle(v)) return fallback;
            try
            {
                float value = v.Dirtyness;
                if (value < 0.0f || value > 20.0f) return fallback;
                return value;
            }
            catch { return fallback; }
        }

        private void SafeSetVehicleDirtLevel(Vehicle v, float dirt)
        {
            if (!IsValidVehicle(v)) return;
            if (dirt < 0.0f) return;
            try
            {
                if (dirt > 20.0f) dirt = 20.0f;
                v.Dirtyness = dirt;
            }
            catch { }
        }

        private string CaptureExtras(Vehicle v)
        {
            if (!IsValidVehicle(v)) return "";
            List<string> parts = new List<string>();
            for (int i = minExtraID; i <= maxExtraID; i++)
            {


                if (captureAllExtraSlots || ExtraExistsNonDestructive(v, i))
                {
                    parts.Add(i.ToString() + ":" + (IsExtraOn(v, i) ? "1" : "0"));
                }
            }
            return Join(parts, ",");
        }

        private void ApplyExtras(Vehicle v, string extras)
        {
            if (!IsValidVehicle(v) || extras == null || extras.Length == 0) return;
            string[] parts = extras.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                string[] kv = parts[i].Split(':');
                if (kv.Length != 2) continue;
                int id;
                if (!Int32.TryParse(kv[0], out id)) continue;
                string state = kv[1].Trim().ToLowerInvariant();
                bool on = state == "1" || state == "true" || state == "on" || state == "enabled";
                SetExtraState(v, id, on);
            }
        }

        private bool ExtraExistsNonDestructive(Vehicle v, int extra)
        {
            if (!IsValidVehicle(v)) return false;
            try { return Function.Call<bool>("DOES_EXTRA_EXIST", v, extra); } catch { }
            try { return Function.Call<bool>("DOES_VEHICLE_EXTRA_EXIST", v, extra); } catch { }

            try { return IsExtraOn(v, extra); } catch { }
            return false;
        }

        private bool ProbeExtra(Vehicle v, int extra)
        {
            try
            {
                bool originalOn = IsExtraOn(v, extra);
                Function.Call("TURN_OFF_VEHICLE_EXTRA", v, extra, false);
                bool canBeOn = IsExtraOn(v, extra);
                Function.Call("TURN_OFF_VEHICLE_EXTRA", v, extra, !originalOn);
                return originalOn || canBeOn;
            }
            catch { return false; }
        }

        private bool IsExtraOn(Vehicle v, int extra)
        {
            try { return Function.Call<bool>("IS_VEHICLE_EXTRA_TURNED_ON", v, extra); }
            catch { return false; }
        }

        private void SetExtraState(Vehicle v, int extra, bool on)
        {
            if (!IsValidVehicle(v)) return;
            try
            {
                if (IsExtraOn(v, extra) == on) return;
                Function.Call("TURN_OFF_VEHICLE_EXTRA", v, extra, !on);
            }
            catch { }
        }

        private bool IsVehicleAlreadyOwned(Vehicle v)
        {
            if (!IsValidVehicle(v)) return false;
            int handle = TryGetVehicleHandle(v);
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (!OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (rec.WorldHandle != 0 && handle != 0 && rec.WorldHandle == handle && (IsValidVehicle(rec.VehicleRef) || CanReclaimOwnedVehicleFromHandle(rec, v))) return true;
                if (IsValidVehicle(rec.VehicleRef) && rec.VehicleRef == v) return true;
            }
            return false;
        }

        private OwnedVehicleRecord FindOwnedRecordForVehicle(Vehicle v)
        {
            if (!IsValidVehicle(v)) return null;
            int handle = TryGetVehicleHandle(v);
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null || !OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (rec.WorldHandle != 0 && handle != 0 && rec.WorldHandle == handle && (IsValidVehicle(rec.VehicleRef) || CanReclaimOwnedVehicleFromHandle(rec, v))) return rec;
                if (IsValidVehicle(rec.VehicleRef) && rec.VehicleRef == v) return rec;
            }
            return null;
        }

        private OwnedVehicleRecord FindFirstDestroyedOwnedVehicle()
        {
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                if (ownedVehicles[i] != null && OwnedRecordBelongsToCurrentEpisode(ownedVehicles[i]) && ownedVehicles[i].Destroyed) return ownedVehicles[i];
            }
            return null;
        }

        private bool IsPlayerVehicle(Vehicle v)
        {
            try { return Player.Character.isSittingInVehicle() && Player.Character.CurrentVehicle == v; }
            catch { return false; }
        }

        private bool IsVehicleDead(Vehicle v)
        {
            if (!IsValidVehicle(v)) return false;
            try { if (Function.Call<bool>("IS_CAR_DEAD", v)) return true; } catch { }
            try { if (Function.Call<bool>("IS_CAR_ON_FIRE", v)) return true; } catch { }
            try { if (!Function.Call<bool>("IS_CAR_HEALTH_GREATER", v, 1)) return true; } catch { }
            try { if (v.Health <= 0) return true; } catch { }
            try { if (v.EngineHealth <= -900.0f) return true; } catch { }
            return false;
        }

        private float SafeVehicleHealth(Vehicle v)
        {
            try { return (float)v.Health; } catch { return 0.0f; }
        }

        private float SafeEngineHealth(Vehicle v)
        {
            try { return (float)v.EngineHealth; } catch { return 0.0f; }
        }

        private int ClampColourID(int id)
        {
            if (id < minColourID) return minColourID;
            if (id > maxColourID) return maxColourID;
            return id;
        }

        private void MarkVehicleAsMission(Vehicle v)
        {
            if (!IsValidVehicle(v)) return;
            try { Function.Call("SET_CAR_AS_MISSION_CAR", v); } catch { }
            try { Function.Call("SET_HAS_BEEN_OWNED_BY_PLAYER", v, true); } catch { }
        }

        private void LoadVehicleDatabase()
        {
            monetaryByHandling.Clear();
            vehicleByHash.Clear();
            vehicleByModel.Clear();
            vehicleModelAliases.Clear();

            string[] handlingPaths = new string[]
            {
                "update\\common\\data\\handling.dat",
                "update\\TLAD\\common\\data\\handling.dat",
                "update\\TBOGT\\common\\data\\handling.dat",
                "TLAD\\common\\data\\handling.dat",
                "TBOGT\\common\\data\\handling.dat",
                "common\\data\\handling.dat"
            };
            for (int i = 0; i < handlingPaths.Length; i++)
            {
                if (File.Exists(handlingPaths[i])) LoadHandlingFile(handlingPaths[i]);
            }

            string[] vehiclePaths = new string[]
            {
                "update\\common\\data\\vehicles.ide",
                "update\\TLAD\\common\\data\\vehicles.ide",
                "update\\TBOGT\\common\\data\\vehicles.ide",
                "TLAD\\common\\data\\vehicles.ide",
                "TBOGT\\common\\data\\vehicles.ide",
                "common\\data\\vehicles.ide"
            };
            for (int i = 0; i < vehiclePaths.Length; i++)
            {
                if (File.Exists(vehiclePaths[i])) LoadVehiclesIde(vehiclePaths[i]);
            }

            AddBuiltInEpisodeVehicleFallbacks();
            Log("Vehicle database loaded. Definitions=" + vehicleByModel.Count.ToString() + ", HandlingValues=" + monetaryByHandling.Count.ToString() + ".");
        }

        private void LoadHandlingFile(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                int count = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = StripDataComment(lines[i]).Trim();
                    if (line.Length == 0) continue;
                    string[] parts = line.Replace(',', ' ').Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 34) continue;
                    string name = parts[0].Trim().ToUpperInvariant();
                    int value = 0;
                    int[] candidates = new int[] { 33, 34, 32, 35 };
                    for (int c = 0; c < candidates.Length; c++)
                    {
                        int idx = candidates[c];
                        if (idx < 0 || idx >= parts.Length) continue;
                        if (TryParseCleanInt(parts[idx], out value) && value >= 500 && value <= 500000) break;
                        value = 0;
                    }
                    if (value <= 0)
                    {
                        for (int p = parts.Length - 1; p >= 1; p--)
                        {
                            if (TryParseCleanInt(parts[p], out value) && value >= 500 && value <= 500000) break;
                            value = 0;
                        }
                    }
                    if (name.Length > 0 && value > 0)
                    {
                        monetaryByHandling[name] = value;
                        count++;
                    }
                }
                Log("Loaded handling monetary values from " + path + " (" + count.ToString() + ").");
            }
            catch (Exception ex)
            {
                Log("Handling load failed from " + path + ": " + ex.GetType().Name + ".");
            }
        }

        private void LoadVehiclesIde(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                string section = "";
                int count = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = StripDataComment(lines[i]).Trim();
                    if (line.Length == 0) continue;
                    string lower = line.ToLowerInvariant();
                    if (lower == "cars" || lower == "bikes" || lower == "boats" || lower == "heli" || lower == "planes" || lower == "trains") { section = lower; continue; }
                    if (lower == "end") { section = ""; continue; }
                    if (section.Length == 0) continue;
                    string[] parts = line.Split(',');
                    if (parts.Length < 13) continue;
                    VehicleDefinition def = new VehicleDefinition();
                    def.ModelName = parts[0].Trim().ToLowerInvariant();
                    def.Type = parts[2].Trim().ToLowerInvariant();
                    def.Section = section;
                    def.HandlingName = parts[3].Trim().ToUpperInvariant();
                    def.GameName = parts.Length > 4 ? parts[4].Trim().ToUpperInvariant() : def.ModelName.ToUpperInvariant();
                    def.Swankness = ParseSwankness(parts[12]);
                    def.Flags = parts.Length > 15 ? parts[15].Trim().ToLowerInvariant() : parts[parts.Length - 1].Trim().ToLowerInvariant();
                    int money;
                    if (monetaryByHandling.TryGetValue(def.HandlingName, out money)) def.MonetaryValue = money;
                    else def.MonetaryValue = defaultVehicleValue;
                    def.Hash = ComputeGtaHash(def.ModelName);
                    ClassifyVehicle(def, section);
                    if (def.ModelName.Length > 0)
                    {
                        vehicleByModel[def.ModelName] = def;
                        if (def.Hash != 0) vehicleByHash[def.Hash] = def;
                        RegisterVehicleDefinitionAliases(def);
                        count++;
                    }
                }
                Log("Loaded vehicles.ide from " + path + " (" + count.ToString() + ").");
            }
            catch (Exception ex)
            {
                Log("vehicles.ide load failed from " + path + ": " + ex.GetType().Name + ".");
            }
        }

        private void RegisterVehicleDefinitionAliases(VehicleDefinition def)
        {
            if (def == null) return;
            RegisterVehicleModelAlias(def.ModelName, def.ModelName);
            RegisterVehicleModelAlias(def.GameName, def.ModelName);
            RegisterVehicleModelAlias(def.HandlingName, def.ModelName);
        }

        private void RegisterVehicleModelAlias(string alias, string model)
        {
            string a = NormaliseVehicleLookupKey(alias);
            string m = NormaliseVehicleLookupKey(model);
            if (a.Length == 0 || m.Length == 0) return;
            string existing;
            if (vehicleModelAliases.TryGetValue(a, out existing))
            {
                if (existing == m) return;
                if (vehicleByModel.ContainsKey(a)) return;
                vehicleModelAliases[a] = "";
                return;
            }
            vehicleModelAliases[a] = m;
        }

        private void AddBuiltInEpisodeVehicleFallbacks()
        {
            int added = 0;

            added += AddFallbackVehicleDefinition("angel", "bikes", "bike", "ANGEL", 2) ? 1 : 0;
            added += AddFallbackVehicleDefinition("bati", "bikes", "bike", "BATI", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("bati2", "bikes", "bike", "BATI2", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("daemon", "bikes", "bike", "DAEMON", 2) ? 1 : 0;
            added += AddFallbackVehicleDefinition("diabolus", "bikes", "bike", "DIABOLUS", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("double", "bikes", "bike", "DOUBLE", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("hakuchou", "bikes", "bike", "HAKUCHOU", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("hexer", "bikes", "bike", "HEXER", 2) ? 1 : 0;
            added += AddFallbackVehicleDefinition("innovation", "bikes", "bike", "INNOVATION", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("lycan", "bikes", "bike", "LYCAN", 3) ? 1 : 0;
            added += AddFallbackVehicleDefinition("nightblade", "bikes", "bike", "NIGHTBLADE", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("revenant", "bikes", "bike", "REVENANT", 3) ? 1 : 0;
            added += AddFallbackVehicleDefinition("vader", "bikes", "bike", "VADER", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("wayfarer", "bikes", "bike", "WAYFARER", 2) ? 1 : 0;
            added += AddFallbackVehicleDefinition("wolfsbane", "bikes", "bike", "WOLFSBANE", 3) ? 1 : 0;

            added += AddFallbackVehicleDefinition("apc", "cars", "car", "APC", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("buffalo", "cars", "car", "BUFFALO", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("bullet", "cars", "car", "BULLET", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("cavalcade2", "cars", "car", "CAVALCADE", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("f620", "cars", "car", "F620", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("schafter2", "cars", "car", "SCHAFTER2", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("schafter3", "cars", "car", "SCHAFTER3", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("serrano", "cars", "car", "SERRANO", 4) ? 1 : 0;
            added += AddFallbackVehicleDefinition("slamvan", "cars", "car", "SLAMVAN", 2) ? 1 : 0;
            added += AddFallbackVehicleDefinition("superd", "cars", "car", "SUPERD", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("superdrop", "cars", "car", "SUPERDROP", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("towtruck", "cars", "car", "TOWTRUCK", 1) ? 1 : 0;
            added += AddFallbackVehicleDefinition("gburrito", "cars", "car", "GBURRITO", 2) ? 1 : 0;
            added += AddFallbackVehicleDefinition("rhapsody", "cars", "car", "RHAPSODY", 1) ? 1 : 0;
            added += AddFallbackVehicleDefinition("regina", "cars", "car", "REGINA", 1) ? 1 : 0;
            added += AddFallbackVehicleDefinition("tampa", "cars", "car", "TAMPA", 2) ? 1 : 0;
            added += AddFallbackVehicleDefinition("police3", "cars", "car", "POLICE3", 3) ? 1 : 0;
            added += AddFallbackVehicleDefinition("police4", "cars", "car", "POLICE4", 3) ? 1 : 0;

            added += AddFallbackVehicleDefinition("faggio2", "bikes", "bike", "FAGGIO2", 1) ? 1 : 0;

            added += AddFallbackVehicleDefinition("floater", "boats", "boat", "FLOATER", 2) ? 1 : 0;
            added += AddFallbackVehicleDefinition("smuggler", "boats", "boat", "SMUGGLER", 3) ? 1 : 0;

            added += AddFallbackVehicleDefinition("buzzard", "heli", "heli", "BUZZARD", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("swift", "heli", "heli", "SWIFT", 5) ? 1 : 0;
            added += AddFallbackVehicleDefinition("skylift", "heli", "heli", "SKYLIFT", 5) ? 1 : 0;

            if (added > 0) Log("Added built-in EFLC vehicle fallback definitions (" + added.ToString() + ").");
        }

        private bool AddFallbackVehicleDefinition(string model, string section, string type, string handling, int swankness)
        {
            if (model == null) return false;
            string key = model.Trim().ToLowerInvariant();
            if (key.Length == 0 || vehicleByModel.ContainsKey(key)) return false;

            VehicleDefinition def = new VehicleDefinition();
            def.ModelName = key;
            def.Section = section == null ? "cars" : section.Trim().ToLowerInvariant();
            def.Type = type == null ? def.Section : type.Trim().ToLowerInvariant();
            def.HandlingName = handling == null ? key.ToUpperInvariant() : handling.Trim().ToUpperInvariant();
            def.GameName = key.ToUpperInvariant();
            def.Swankness = swankness;
            def.Flags = "";
            int money;
            if (monetaryByHandling.TryGetValue(def.HandlingName, out money)) def.MonetaryValue = money;
            else def.MonetaryValue = defaultVehicleValue;
            def.Hash = ComputeGtaHash(def.ModelName);
            ClassifyVehicle(def, def.Section);
            vehicleByModel[def.ModelName] = def;
            if (def.Hash != 0) vehicleByHash[def.Hash] = def;
            RegisterVehicleDefinitionAliases(def);
            return true;
        }

        private int ComputeGtaHash(string value)
        {
            if (value == null) return 0;
            string text = value.Trim().ToLowerInvariant();
            if (text.Length == 0) return 0;
            unchecked
            {
                uint hash = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    hash += (byte)text[i];
                    hash += (hash << 10);
                    hash ^= (hash >> 6);
                }
                hash += (hash << 3);
                hash ^= (hash >> 11);
                hash += (hash << 15);
                return (int)hash;
            }
        }

        private int ParseSwankness(string value)
        {
            if (value == null) return 2;
            string[] tokens = value.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                int parsed;
                if (Int32.TryParse(tokens[i], out parsed)) return parsed;
            }
            return 2;
        }

        private void ClassifyVehicle(VehicleDefinition def, string section)
        {
            string model = def.ModelName == null ? "" : def.ModelName.ToLowerInvariant();
            string flags = def.Flags == null ? "" : def.Flags.ToLowerInvariant();
            def.IsBike = section == "bikes" || def.Type == "bike" || model == "nrg900" || model == "pcj" || model == "faggio" || model == "sanchez" || model == "bati" || model == "bati2" || model == "hexer" || model == "hakuchou" || model == "double" || model == "innovation" || model == "angel" || model == "daemon" || model == "diabolus" || model == "lycan" || model == "nightblade" || model == "wolfsbane";
            def.IsEmergency = ContainsAny(model, new string[] { "police", "noose", "fbi", "ambulance", "firetruk", "pbus", "polpat" }) || ContainsAny(def.HandlingName.ToLowerInvariant(), new string[] { "police", "noose", "fbi", "ambulance", "firetruk", "pbus", "polpat" });
            def.IsTaxi = model == "taxi" || model == "taxi2" || model == "cabby" || flags.IndexOf("ext_taxi") >= 0;
            def.IsIndustrial = ContainsAny(flags, new string[] { "avoidturns", "big", "truckfirefx", "truckenginefx", "delivery" }) || ContainsAny(model, new string[] { "bus", "mule", "phantom", "packer", "flatbed", "biff", "forklift", "airtug", "ripley", "stockade", "yankee", "benson" });
            def.IsSports = flags.IndexOf("sports") >= 0 || def.Swankness >= 5;
        }

        private bool ContainsAny(string haystack, string[] needles)
        {
            if (haystack == null) return false;
            for (int i = 0; i < needles.Length; i++) if (haystack.IndexOf(needles[i]) >= 0) return true;
            return false;
        }

        private void FilterDealerModelsAgainstVehicleDatabase()
        {
            if (keepUnknownDealerModels || vehicleByModel.Count == 0) return;
            for (int i = 0; i < dealers.Count; i++)
            {
                DealerDefinition d = dealers[i];
                List<string> filtered = new List<string>();
                Dictionary<string, bool> seen = new Dictionary<string, bool>();
                int removed = 0;
                for (int m = 0; m < d.Models.Count; m++)
                {
                    string model = d.Models[m] == null ? "" : d.Models[m].Trim().ToLowerInvariant();
                    if (model.Length == 0) continue;
                    if (seen.ContainsKey(model)) continue;
                    seen[model] = true;
                    if (vehicleByModel.ContainsKey(model)) filtered.Add(model);
                    else removed++;
                }
                if (removed > 0) Log("Dealer " + d.Id + ": skipped " + removed.ToString() + " model(s) missing from active vehicles.ide.");
                d.Models = filtered;
            }
        }

        private void EnsureDefaultDealersAndRegistrationPoints()
        {
            if (dealers.Count == 0)
            {
                DealerDefinition sp = new DealerDefinition();
                sp.Id = "salesparts_chasepoint";
                sp.Name = "Sales & Parts";
                sp.Enabled = true;
                sp.X = 790.683f; sp.Y = 1431.358f; sp.Z = 14.253f; sp.Radius = 4.5f;
                sp.DisplayX = 788.668f; sp.DisplayY = 1437.088f; sp.DisplayZ = 13.793f; sp.DisplayHeading = 179.612f;
                sp.Markup = 0.90f;
                sp.BlipSprite = 79;
                sp.ForceSaleSlotVehicles = true;
                sp.PersistentSaleSlotVehicles = true;
                sp.UseCarSpecificInteraction = true;
                sp.ShowroomDelivery = false;
                sp.AdoptNativeVehicles = true;
                sp.FallbackSpawnWhenNoNativeVehicles = true;
                sp.DisplayScanRadius = 2.25f; sp.CleanupX = 790.8f; sp.CleanupY = 1437.1f; sp.CleanupZ = 13.8f; sp.CleanupRadius = 12.0f;
                sp.DisplaySlots.Add(new DealerDisplaySlot() { X = 788.668f, Y = 1437.088f, Z = 13.793f, Heading = 179.612f });
                sp.DisplaySlots.Add(new DealerDisplaySlot() { X = 793.022f, Y = 1437.172f, Z = 13.759f, Heading = 181.312f });
                AddModels(sp, "primo,manana,marbella,esperanto,blista,feroci,ingot,willard,moonbeam,perennial,minivan,bobcat,burrito,pony,sabre2,fortune,lokus,df8");
                dealers.Add(sp);

                DealerDefinition grotti = new DealerDefinition();
                grotti.Id = "grotti"; grotti.Name = "Grotti"; grotti.Enabled = true;
                grotti.X = 49.138f; grotti.Y = 801.418f; grotti.Z = 14.173f; grotti.Radius = 4.5f;
                grotti.DisplayX = 49.138f; grotti.DisplayY = 801.418f; grotti.DisplayZ = 14.173f; grotti.DisplayHeading = 178.490f;
                grotti.HasPurchaseSpawn = true; grotti.PurchaseX = 49.134f; grotti.PurchaseY = 801.419f; grotti.PurchaseZ = 14.175f; grotti.PurchaseHeading = 178.485f;
                grotti.Markup = 1.00f;
                grotti.BlipSprite = 79;
                grotti.ForceSaleSlotVehicles = false; grotti.PersistentSaleSlotVehicles = false; grotti.UseCarSpecificInteraction = true; grotti.ShowroomDelivery = true; grotti.AdoptNativeVehicles = true; grotti.FallbackSpawnWhenNoNativeVehicles = false; grotti.DisplayScanRadius = 2.25f;
                grotti.CleanupX = 63.0f; grotti.CleanupY = 802.0f; grotti.CleanupZ = 15.0f; grotti.CleanupRadius = 0.0f;
                grotti.HasCleanupBounds = true; grotti.CleanupMinX = 42.0f; grotti.CleanupMaxX = 85.0f; grotti.CleanupMinY = 788.0f; grotti.CleanupMaxY = 828.5f;
                grotti.PreferredModel = "turismo"; grotti.PreferredModelChance = 0.60f;
                grotti.DeliverySlots.Add(new DealerDeliverySlot() { X = 49.134f, Y = 801.419f, Z = 14.175f, Heading = 178.485f });
                grotti.DisplaySlots.Add(new DealerDisplaySlot() { X = 43.681f, Y = 812.965f, Z = 14.096f, Heading = 213.107f });
                grotti.DisplaySlots.Add(new DealerDisplaySlot() { X = 62.315f, Y = 806.462f, Z = 16.364f, Heading = 240.0f });
                grotti.DisplaySlots.Add(new DealerDisplaySlot() { X = 71.452f, Y = 804.453f, Z = 16.366f, Heading = 240.0f });
                AddModels(grotti, "turismo,infernus,supergt,comet,coquette,banshee,bullet,f620,buffalo");
                dealers.Add(grotti);

                DealerDefinition autoeroticar = new DealerDefinition();
                autoeroticar.Id = "autoeroticar"; autoeroticar.Name = "AutoErotiCar"; autoeroticar.Enabled = true;
                autoeroticar.X = -1494.112f; autoeroticar.Y = 1118.967f; autoeroticar.Z = 23.214f; autoeroticar.Radius = 4.5f;
                autoeroticar.DisplayX = -1479.129f; autoeroticar.DisplayY = 1111.788f; autoeroticar.DisplayZ = 24.405f; autoeroticar.DisplayHeading = 155.0f;
                autoeroticar.HasPurchaseSpawn = true; autoeroticar.PurchaseX = -1478.512f; autoeroticar.PurchaseY = 1103.277f; autoeroticar.PurchaseZ = 22.610f; autoeroticar.PurchaseHeading = 181.007f;
                autoeroticar.Markup = 1.00f;
                autoeroticar.BlipSprite = 79;
                autoeroticar.ForceSaleSlotVehicles = false; autoeroticar.PersistentSaleSlotVehicles = false; autoeroticar.UseCarSpecificInteraction = true; autoeroticar.ShowroomDelivery = true; autoeroticar.AdoptNativeVehicles = true; autoeroticar.FallbackSpawnWhenNoNativeVehicles = false; autoeroticar.DisplayScanRadius = 2.25f; autoeroticar.CleanupX = -1487.0f; autoeroticar.CleanupY = 1121.0f; autoeroticar.CleanupZ = 23.5f; autoeroticar.CleanupRadius = 0.0f; autoeroticar.HasCleanupBounds = true; autoeroticar.CleanupMinX = -1501.0f; autoeroticar.CleanupMaxX = -1473.0f; autoeroticar.CleanupMinY = 1098.0f; autoeroticar.CleanupMaxY = 1143.0f;
                autoeroticar.DeliverySlots.Add(new DealerDeliverySlot() { X = -1478.512f, Y = 1103.277f, Z = 22.610f, Heading = 181.007f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1479.129f, Y = 1111.788f, Z = 24.405f, Heading = 155.0f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1484.639f, Y = 1125.456f, Z = 24.478f, Heading = 155.0f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1482.617f, Y = 1132.741f, Z = 24.865f, Heading = 155.0f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1478.675f, Y = 1135.152f, Z = 24.333f, Heading = 155.0f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1481.672f, Y = 1140.469f, Z = 24.405f, Heading = 155.0f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1492.617f, Y = 1137.563f, Z = 24.484f, Heading = 155.0f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1492.887f, Y = 1130.762f, Z = 24.498f, Heading = 155.0f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1498.335f, Y = 1127.256f, Z = 24.564f, Heading = 155.0f });
                autoeroticar.DisplaySlots.Add(new DealerDisplaySlot() { X = -1493.488f, Y = 1124.586f, Z = 24.251f, Heading = 155.0f });
                AddModels(autoeroticar, "feltzer,pres,oracle,sentinel,cognoscenti,supergt,comet,coquette,banshee,f620,superd,schafter2,schafter3,buffalo");
                dealers.Add(autoeroticar);

                DealerDefinition westdyke = new DealerDefinition();
                westdyke.Id = "westdyke"; westdyke.Name = "Westdyke Autos"; westdyke.Enabled = true;
                westdyke.X = -1069.891f; westdyke.Y = 1461.570f; westdyke.Z = 24.792f; westdyke.Radius = 4.5f;
                westdyke.DisplayX = -1070.024f; westdyke.DisplayY = 1466.301f; westdyke.DisplayZ = 24.823f; westdyke.DisplayHeading = 180.0f;
                westdyke.HasPurchaseSpawn = true; westdyke.PurchaseX = -1087.022f; westdyke.PurchaseY = 1454.775f; westdyke.PurchaseZ = 24.033f; westdyke.PurchaseHeading = 285.461f;
                westdyke.Markup = 1.00f;
                westdyke.BlipSprite = 79;
                westdyke.ForceSaleSlotVehicles = true; westdyke.PersistentSaleSlotVehicles = true; westdyke.UseCarSpecificInteraction = true; westdyke.ShowroomDelivery = true; westdyke.AdoptNativeVehicles = true; westdyke.FallbackSpawnWhenNoNativeVehicles = false; westdyke.DisplayScanRadius = 2.25f; westdyke.CleanupX = -1077.0f; westdyke.CleanupY = 1466.0f; westdyke.CleanupZ = 24.5f; westdyke.CleanupRadius = 0.0f; westdyke.HasCleanupBounds = true; westdyke.CleanupMinX = -1093.0f; westdyke.CleanupMaxX = -1061.0f; westdyke.CleanupMinY = 1456.0f; westdyke.CleanupMaxY = 1478.0f;
                westdyke.DeliverySlots.Add(new DealerDeliverySlot() { X = -1087.022f, Y = 1454.775f, Z = 24.033f, Heading = 285.461f });

                westdyke.DisplaySlots.Add(new DealerDisplaySlot() { X = -1089.713f, Y = 1467.573f, Z = 24.792f, Heading = 180.0f });
                westdyke.DisplaySlots.Add(new DealerDisplaySlot() { X = -1073.539f, Y = 1464.711f, Z = 24.792f, Heading = 180.0f });
                westdyke.DisplaySlots.Add(new DealerDisplaySlot() { X = -1067.376f, Y = 1471.424f, Z = 24.792f, Heading = 180.0f });
                westdyke.DisplaySlots.Add(new DealerDisplaySlot() { X = -1078.785f, Y = 1473.724f, Z = 24.792f, Heading = 180.0f });
                AddModels(westdyke, "nrg900,pcj,sanchez,faggio");
                dealers.Add(westdyke);

                DealerDefinition bigpaulie = new DealerDefinition();
                bigpaulie.Id = "bigpaulie"; bigpaulie.Name = "Big Paulie Budget Cars"; bigpaulie.Enabled = true;
                bigpaulie.X = -1676.345f; bigpaulie.Y = -66.232f; bigpaulie.Z = 6.923f; bigpaulie.Radius = 4.5f;
                bigpaulie.DisplayX = -1690.971f; bigpaulie.DisplayY = -65.339f; bigpaulie.DisplayZ = 6.982f; bigpaulie.DisplayHeading = 270.0f;
                bigpaulie.HasPurchaseSpawn = false;
                bigpaulie.Markup = 0.85f;
                bigpaulie.BlipSprite = 79;
                bigpaulie.ForceSaleSlotVehicles = true; bigpaulie.PersistentSaleSlotVehicles = true; bigpaulie.UseCarSpecificInteraction = true; bigpaulie.ShowroomDelivery = false; bigpaulie.AdoptNativeVehicles = false; bigpaulie.FallbackSpawnWhenNoNativeVehicles = false; bigpaulie.DisplayScanRadius = 2.25f; bigpaulie.CleanupX = -1690.8f; bigpaulie.CleanupY = -48.0f; bigpaulie.CleanupZ = 8.2f; bigpaulie.CleanupRadius = 28.0f;
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.971f, Y = -65.339f, Z = 6.982f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.944f, Y = -61.883f, Z = 7.372f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.878f, Y = -58.705f, Z = 7.711f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1691.126f, Y = -55.669f, Z = 7.873f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.984f, Y = -52.297f, Z = 8.065f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.656f, Y = -49.513f, Z = 8.238f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.582f, Y = -46.104f, Z = 8.427f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.449f, Y = -42.991f, Z = 8.604f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.763f, Y = -39.644f, Z = 8.775f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.745f, Y = -36.608f, Z = 8.937f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.658f, Y = -33.285f, Z = 9.120f, Heading = 270.0f });
                bigpaulie.DisplaySlots.Add(new DealerDisplaySlot() { X = -1690.911f, Y = -30.163f, Z = 9.297f, Heading = 270.0f });
                AddModels(bigpaulie, "manana,esperanto,primo,ingot,df8,hakumai,vincent,lokus,willard,merit,buccaneer,faction,marbella,fortune,blista,sabre2");
                dealers.Add(bigpaulie);

                Log("Loaded built-in dealer definitions. User INI contains settings only.");
            }
            if (registrationPoints.Count == 0)
            {
                RegistrationPoint sm = new RegistrationPoint();
                sm.Id = "sm_auto_sales"; sm.Name = "S&M Auto Sales"; sm.Enabled = true;
                sm.X = 724.300f; sm.Y = 1392.884f; sm.Z = 14.262f; sm.Radius = 4.5f; sm.VehicleRadius = 24.0f;
                sm.PriceMultiplier = 0.30f; sm.BlipSprite = 0; sm.AcceptPremium = true; sm.AcceptIndustrial = false;
                registrationPoints.Add(sm);

                Log("Loaded built-in registration point definitions. User INI contains settings only.");
            }
        }

        private void RefreshStoryProgressCache(bool force)
        {
            if (!force && storyProgressRefreshTicks > 0)
            {
                storyProgressRefreshTicks--;
                return;
            }
            storyProgressRefreshTicks = 300;
            float progress;
            if (TryReadStoryProgressPercent(out progress))
            {
                storyProgressPercent = progress;
                storyProgressReliable = true;
                if (!storyProgressUnlockStateLogged)
                {
                    storyProgressUnlockStateLogged = true;
                    Log("Service unlock progress loaded. Episode=" + GetCurrentEpisodeKey() + ", Progress=" + storyProgressPercent.ToString("0.00", CultureInfo.InvariantCulture) + "%.");
                }
            }
            else
            {
                storyProgressReliable = false;
                if (!storyProgressUnavailableLogged)
                {
                    storyProgressUnavailableLogged = true;
                    Log("Story-progress stat unavailable; service blips fall back to always available.");
                }
            }
        }

        private bool TryReadStoryProgressPercent(out float progress)
        {
            progress = 0.0f;
            int statId = GetStoryProgressStatId();
            if (statId < 0) return false;
            try
            {
                progress = Function.Call<float>("GET_FLOAT_STAT", statId);
                if (progress < 0.0f) progress = 0.0f;
                if (progress > 100.0f) progress = 100.0f;
                return true;
            }
            catch { return false; }
        }

        private int GetStoryProgressStatId()
        {
            string ep = GetCurrentEpisodeKey();
            if (ep == "tlad") return 133;
            if (ep == "tbogt") return 187;
            return 0;
        }

        private bool IsUnlockAreaAvailable(string area)
        {
            string ep = GetCurrentEpisodeKey();
            if (ep == "tlad" || ep == "tbogt") return true;
            if (!storyProgressReliable) return true;
            area = NormaliseUnlockArea(area);
            if (area == "broker_dukes") return storyProgressPercent >= IvBrokerDukesUnlockProgress;
            if (area == "bohan") return storyProgressPercent >= IvBohanUnlockProgress;
            if (area == "algonquin") return storyProgressPercent >= IvAlgonquinUnlockProgress;
            if (area == "alderney") return storyProgressPercent >= IvAlderneyUnlockProgress;
            return true;
        }

        private string NormaliseUnlockArea(string area)
        {
            if (area == null) return "broker_dukes";
            area = area.Trim().ToLowerInvariant();
            if (area == "broker" || area == "dukes" || area == "broker_dukes" || area == "broker & dukes") return "broker_dukes";
            if (area == "bohan") return "bohan";
            if (area == "algonquin") return "algonquin";
            if (area == "alderney") return "alderney";
            return "broker_dukes";
        }

        private string InferUnlockArea(string id, string name, float x, float y)
        {
            string key = ((id == null ? "" : id) + " " + (name == null ? "" : name)).ToLowerInvariant();
            if (key.IndexOf("alderney") >= 0 || key.IndexOf("westdyke") >= 0 || key.IndexOf("autoeroticar") >= 0 || key.IndexOf("bigpaulie") >= 0) return "alderney";
            if (key.IndexOf("algonquin") >= 0 || key.IndexOf("grotti") >= 0 || key.IndexOf("castle_garden") >= 0 || key.IndexOf("castle garden") >= 0 || key.IndexOf("easton") >= 0 || key.IndexOf("midtown") >= 0 || key.IndexOf("star") >= 0) return "algonquin";
            if (key.IndexOf("bohan") >= 0 || key.IndexOf("boabo") >= 0 || key.IndexOf("chase_point") >= 0 || key.IndexOf("chase point") >= 0 || key.IndexOf("lcpd") >= 0) return "bohan";
            if (x < -650.0f) return "alderney";
            return "broker_dukes";
        }

        private string GetDealerUnlockArea(DealerDefinition d)
        {
            if (d == null) return "broker_dukes";
            return InferUnlockArea(d.Id, d.Name, d.X, d.Y);
        }

        private string GetRegistrationUnlockArea(RegistrationPoint r)
        {
            if (r == null) return "broker_dukes";
            return InferUnlockArea(r.Id, r.Name, r.X, r.Y);
        }

        private string GetFuelStationUnlockArea(FuelStation station)
        {
            if (station == null) return "broker_dukes";
            return InferUnlockArea(station.Id, station.Name, station.X, station.Y);
        }

        private string GetMetroStationUnlockArea(MetroStation station)
        {
            if (station == null) return "broker_dukes";
            return NormaliseUnlockArea(station.Line);
        }

        private bool ShouldShowDealerBlip(DealerDefinition d)
        {
            if (d == null || d.Id == null || !d.Enabled) return false;
            if (d.Models == null || d.Models.Count == 0) return false;
            return IsUnlockAreaAvailable(GetDealerUnlockArea(d));
        }

        private bool ShouldShowFuelStationBlip(FuelStation station)
        {
            if (station == null || station.Id == null) return false;
            return IsUnlockAreaAvailable(GetFuelStationUnlockArea(station));
        }

        private bool IsMetroStationUnlocked(string stationId)
        {
            MetroStation station = FindMetroStationById(stationId);
            if (station == null) return false;
            return IsUnlockAreaAvailable(GetMetroStationUnlockArea(station));
        }

        private bool ShouldShowMetroStationBlip(MetroStation station)
        {
            if (!metroStationsEnabled || !metroStationBlipsEnabled) return false;
            if (station == null || station.Id == null) return false;
            return IsUnlockAreaAvailable(GetMetroStationUnlockArea(station));
        }

        private bool ShouldShowMetroEntranceBlip(MetroEntrance entrance)
        {
            if (!metroStationsEnabled || !metroEntranceBlipsEnabled) return false;
            if (entrance == null || entrance.Id == null || entrance.StationId == null) return false;
            if (!IsMetroStationUnlocked(entrance.StationId)) return false;
            return IsMetroEntranceNearPlayer(entrance);
        }

        private string GetFuelStationBlipName(FuelStation station)
        {
            if (station == null || station.Kind == null) return "Gas station";
            string kind = station.Kind.Trim().ToLowerInvariant();
            if (kind == "heli" || kind == "helicopter") return "Helipad fuel";
            if (kind == "boat" || kind == "marine") return "Marine fuel";
            return "Gas station";
        }

        private bool IsMetroEntranceNearPlayer(MetroEntrance entrance)
        {
            if (entrance == null) return false;
            try
            {
                Vector3 p = Player.Character.Position;
                float dx = p.X - entrance.X;
                float dy = p.Y - entrance.Y;
                return (dx * dx + dy * dy) <= (MetroEntranceActivationRadius * MetroEntranceActivationRadius);
            }
            catch { return false; }
        }

        private string GetMetroStationBlipName(MetroStation station)
        {
            if (station == null || String.IsNullOrEmpty(station.Name)) return "Subway station";
            return station.Name;
        }

        private string GetMetroEntranceBlipName(MetroEntrance entrance)
        {
            if (entrance == null) return "Subway entrance";
            string label = String.IsNullOrEmpty(entrance.Label) ? "Subway entrance" : entrance.Label;
            return label;
        }

        private int GetMetroEntranceBlipSprite(MetroEntrance entrance)
        {
            string code = entrance == null || entrance.LineCode == null ? "" : entrance.LineCode.Trim().ToUpperInvariant();
            if (code == "3") return 83;
            if (code == "8") return 84;
            if (code == "A") return 85;
            if (code == "B") return 86;
            if (code == "C") return 87;
            if (code == "E") return 88;
            if (code == "J") return 89;
            if (code == "K") return 90;
            return MetroStationBlipSprite;
        }


        private bool IsPauseMenuActiveSafe()
        {
            try { return Function.Call<bool>("IS_PAUSE_MENU_ACTIVE"); }
            catch { return false; }
        }

        private void UpdateMapBlips()
        {
            if (mapBlipRetryTicks > 0) { mapBlipRetryTicks--; return; }
            try
            {
                if (dealersEnabled && enableDealerBlips) UpdateDealerBlips(); else RemoveAllDealerBlips();
                if (fuelModuleEnabled) UpdateFuelStationBlips(); else RemoveAllFuelStationBlips();
                UpdateMetroBlips();
                if (ownershipEnabled && enableOwnedVehicleBlips) UpdateOwnedVehicleBlips();
                UpdateBlipDiagnostics();
            }
            catch (Exception ex)
            {
                mapBlipRetryTicks = 120;
                Log("Map blip update failed: " + ex.GetType().Name + ".");
            }
        }

        private void UpdateDealerBlips()
        {
            PruneDealerBlips();
            for (int i = 0; i < dealers.Count; i++)
            {
                DealerDefinition d = dealers[i];
                if (d == null || !d.Enabled) continue;
                if (!ShouldShowDealerBlip(d)) continue;
                if (dealerBlips.ContainsKey(d.Id) && dealerBlips[d.Id] != null) continue;
                Blip blip = TryCreateCoordinateBlip(new Vector3(d.X, d.Y, d.Z), "dealer blip " + d.Id);
                if (blip != null)
                {
                    int icon = d.BlipSprite > 0 ? d.BlipSprite : dealerBlipSprite;
                    ConfigureBlip(blip, icon, BlipDisplay.MapOnly, BlipColor.White, dealerBlipScale, d.Name, "dealer blip " + d.Id);
                    dealerBlips[d.Id] = blip;
                    if (showDebugMessages) Log("Dealer blip created: " + d.Id + " / " + d.Name + ".");
                }
            }
        }

        private void UpdateFuelStationBlips()
        {
            PruneFuelStationBlips();
            for (int i = 0; i < fuelStations.Count; i++)
            {
                FuelStation s = fuelStations[i];
                if (s == null || s.Id == null) continue;
                if (!ShouldShowFuelStationBlip(s)) continue;
                Blip existing;
                if (fuelStationBlips.TryGetValue(s.Id, out existing) && existing != null) continue;
                Blip blip = TryCreateCoordinateBlip(new Vector3(s.X, s.Y, s.Z), "fuel station blip " + s.Id);
                if (blip != null)
                {
                    ConfigureBlip(blip, fuelStationBlipSprite, BlipDisplay.MapOnly, BlipColor.White, fuelStationBlipScale, GetFuelStationBlipName(s), "fuel station blip " + s.Id);
                    fuelStationBlips[s.Id] = blip;
                    if (showDebugMessages) Log("Fuel station blip created: " + s.Id + " / " + s.Name + ".");
                }
            }
        }

        private void RemoveAllDealerBlips()
        {
            foreach (KeyValuePair<string, Blip> pair in dealerBlips)
            {
                RemoveBlipSafe(pair.Value, "dealer blip " + pair.Key);
            }
            dealerBlips.Clear();
        }

        private void RemoveAllFuelStationBlips()
        {
            foreach (KeyValuePair<string, Blip> pair in fuelStationBlips)
            {
                RemoveBlipSafe(pair.Value, "fuel station blip " + pair.Key);
            }
            fuelStationBlips.Clear();
        }

        private void PruneDealerBlips()
        {
            List<string> remove = new List<string>();
            foreach (KeyValuePair<string, Blip> pair in dealerBlips)
            {
                DealerDefinition dealer = FindDealerById(pair.Key);
                if (dealer == null || !ShouldShowDealerBlip(dealer) || pair.Value == null) remove.Add(pair.Key);
            }
            for (int i = 0; i < remove.Count; i++)
            {
                string id = remove[i];
                Blip blip;
                if (dealerBlips.TryGetValue(id, out blip)) RemoveBlipSafe(blip, "dealer blip " + id);
                dealerBlips.Remove(id);
            }
        }

        private void PruneFuelStationBlips()
        {
            List<string> remove = new List<string>();
            foreach (KeyValuePair<string, Blip> pair in fuelStationBlips)
            {
                FuelStation station = FindFuelStationById(pair.Key);
                if (station == null || !ShouldShowFuelStationBlip(station) || pair.Value == null) remove.Add(pair.Key);
            }
            for (int i = 0; i < remove.Count; i++)
            {
                string id = remove[i];
                Blip blip;
                if (fuelStationBlips.TryGetValue(id, out blip)) RemoveBlipSafe(blip, "fuel station blip " + id);
                fuelStationBlips.Remove(id);
            }
        }

        private DealerDefinition FindDealerById(string id)
        {
            if (id == null) return null;
            for (int i = 0; i < dealers.Count; i++)
            {
                DealerDefinition d = dealers[i];
                if (d != null && String.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase)) return d;
            }
            return null;
        }

        private FuelStation FindFuelStationById(string id)
        {
            if (id == null) return null;
            for (int i = 0; i < fuelStations.Count; i++)
            {
                FuelStation s = fuelStations[i];
                if (s != null && String.Equals(s.Id, id, StringComparison.OrdinalIgnoreCase)) return s;
            }
            return null;
        }

        private void UpdateMetroBlips()
        {
            if (!metroStationsEnabled)
            {
                RemoveAllMetroStationBlips();
                RemoveAllMetroEntranceBlips();
                return;
            }

            UpdateMetroStationBlips();
            UpdateMetroEntranceBlips();
        }

        private void UpdateMetroStationBlips()
        {
            if (!metroStationBlipsEnabled)
            {
                RemoveAllMetroStationBlips();
                return;
            }


            PruneMetroStationBlips();

            for (int i = 0; i < metroStations.Count; i++)
            {
                MetroStation s = metroStations[i];
                if (s == null || s.Id == null) continue;
                if (!ShouldShowMetroStationBlip(s)) continue;

                Blip existing;
                if (metroStationBlips.TryGetValue(s.Id, out existing) && existing != null) continue;

                Blip blip = TryCreateCoordinateBlip(new Vector3(s.X, s.Y, s.Z), "metro station blip " + s.Id);
                if (blip != null)
                {
                    ConfigureBlip(blip, MetroStationBlipSprite, BlipDisplay.MapOnly, BlipColor.White, MetroStationBlipScale, GetMetroStationBlipName(s), "metro station blip " + s.Id);
                    metroStationBlips[s.Id] = blip;
                    if (showDebugMessages) Log("Metro station blip created: " + s.Id + " / " + s.Name + ".");
                }
            }
        }

        private void UpdateMetroEntranceBlips()
        {
            if (!metroEntranceBlipsEnabled)
            {
                RemoveAllMetroEntranceBlips();
                return;
            }

            PruneMetroEntranceBlips();

            if (metroEntranceBlips.Count >= MetroEntranceMaxActiveBlips) return;

            List<MetroEntrance> candidates = GetNearbyMetroEntranceCandidates();
            for (int i = 0; i < candidates.Count; i++)
            {
                if (metroEntranceBlips.Count >= MetroEntranceMaxActiveBlips) break;
                MetroEntrance e = candidates[i];
                if (e == null || e.Id == null) continue;

                Blip existing;
                if (metroEntranceBlips.TryGetValue(e.Id, out existing) && existing != null) continue;

                Blip blip = TryCreateCoordinateBlip(new Vector3(e.X, e.Y, e.Z), "metro entrance blip " + e.Id);
                if (blip != null)
                {
                    ConfigureBlip(blip, GetMetroEntranceBlipSprite(e), BlipDisplay.MapOnly, BlipColor.White, MetroEntranceBlipScale, GetMetroEntranceBlipName(e), "metro entrance blip " + e.Id);
                    metroEntranceBlips[e.Id] = blip;
                    if (showDebugMessages) Log("Metro entrance blip created: " + e.Id + " / " + e.Label + ".");
                }
            }
        }

        private List<MetroEntrance> GetNearbyMetroEntranceCandidates()
        {
            List<MetroEntrance> result = new List<MetroEntrance>();
            Vector3 p;
            try { p = Player.Character.Position; } catch { return result; }

            for (int i = 0; i < metroEntrances.Count; i++)
            {
                MetroEntrance e = metroEntrances[i];
                if (e == null || e.Id == null) continue;
                if (!ShouldShowMetroEntranceBlip(e)) continue;
                result.Add(e);
            }

            result.Sort(delegate(MetroEntrance a, MetroEntrance b)
            {
                float da = Distance2DSq(p.X, p.Y, a.X, a.Y);
                float db = Distance2DSq(p.X, p.Y, b.X, b.Y);
                return da.CompareTo(db);
            });
            return result;
        }

        private float Distance2DSq(float ax, float ay, float bx, float by)
        {
            float dx = ax - bx;
            float dy = ay - by;
            return dx * dx + dy * dy;
        }

        private void PruneMetroStationBlips()
        {
            List<string> remove = new List<string>();
            foreach (KeyValuePair<string, Blip> pair in metroStationBlips)
            {
                MetroStation station = FindMetroStationById(pair.Key);
                if (station == null || !ShouldShowMetroStationBlip(station) || pair.Value == null) remove.Add(pair.Key);
            }
            for (int i = 0; i < remove.Count; i++)
            {
                string id = remove[i];
                Blip blip;
                if (metroStationBlips.TryGetValue(id, out blip)) RemoveBlipSafe(blip, "metro station blip " + id);
                metroStationBlips.Remove(id);
            }
        }

        private void RemoveAllMetroStationBlips()
        {
            foreach (KeyValuePair<string, Blip> pair in metroStationBlips)
            {
                RemoveBlipSafe(pair.Value, "metro station blip " + pair.Key);
            }
            metroStationBlips.Clear();
        }

        private void PruneMetroEntranceBlips()
        {
            List<string> remove = new List<string>();
            foreach (KeyValuePair<string, Blip> pair in metroEntranceBlips)
            {
                MetroEntrance entrance = FindMetroEntranceById(pair.Key);
                if (entrance == null || !ShouldShowMetroEntranceBlip(entrance) || pair.Value == null) remove.Add(pair.Key);
            }
            for (int i = 0; i < remove.Count; i++)
            {
                string id = remove[i];
                Blip blip;
                if (metroEntranceBlips.TryGetValue(id, out blip)) RemoveBlipSafe(blip, "metro entrance blip " + id);
                metroEntranceBlips.Remove(id);
            }
        }

        private void RemoveAllMetroEntranceBlips()
        {
            foreach (KeyValuePair<string, Blip> pair in metroEntranceBlips)
            {
                RemoveBlipSafe(pair.Value, "metro entrance blip " + pair.Key);
            }
            metroEntranceBlips.Clear();
        }

        private MetroStation FindMetroStationById(string id)
        {
            if (id == null) return null;
            for (int i = 0; i < metroStations.Count; i++)
            {
                MetroStation s = metroStations[i];
                if (s != null && String.Equals(s.Id, id, StringComparison.OrdinalIgnoreCase)) return s;
            }
            return null;
        }

        private MetroEntrance FindMetroEntranceById(string id)
        {
            if (id == null) return null;
            for (int i = 0; i < metroEntrances.Count; i++)
            {
                MetroEntrance e = metroEntrances[i];
                if (e != null && String.Equals(e.Id, id, StringComparison.OrdinalIgnoreCase)) return e;
            }
            return null;
        }


        private void UpdateOwnedVehicleBlips()
        {
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null) continue;

                if (!OwnedRecordBelongsToCurrentEpisode(rec) || rec.Destroyed)
                {
                    ClearOwnedVehicleBlip(rec, "owned vehicle blip " + rec.Id);
                    continue;
                }

                bool hasWorldVehicle = IsValidVehicle(rec.VehicleRef);
                if (rec.BlipRef != null)
                {
                    if ((hasWorldVehicle && rec.BlipIsFallback) || (!hasWorldVehicle && !rec.BlipIsFallback))
                    {
                        ClearOwnedVehicleBlip(rec, "owned vehicle blip " + rec.Id);
                    }
                    else continue;
                }

                Blip blip = null;
                bool fallback = false;
                if (hasWorldVehicle)
                {
                    blip = TryCreateVehicleBlip(rec.VehicleRef, "owned vehicle blip " + rec.Id);
                }
                if (blip == null && (!ownedVehicleBlipsOnlyWhenSpawned || !hasWorldVehicle))
                {
                    blip = TryCreateCoordinateBlip(new Vector3(rec.X, rec.Y, rec.Z), "owned vehicle fallback blip " + rec.Id);
                    fallback = blip != null;
                }
                if (blip != null)
                {
                    string blipName = GetVehicleDisplayName(rec.ModelName);
                    ConfigureBlip(blip, ownedVehicleBlipSprite, BlipDisplay.MapOnly, BlipColor.White, ownedVehicleBlipScale, blipName, "owned vehicle blip " + rec.Id);
                    rec.BlipRef = blip;
                    rec.BlipIsFallback = fallback;
                    if (showDebugMessages) Log("Owned vehicle " + (fallback ? "fallback " : "") + "blip created: " + rec.Id + " / " + blipName + ".");
                }
            }
        }

        private void UpdateBlipDiagnostics()
        {
            if (!logBlipDiagnostics) return;
            if (blipDiagnosticCooldownTicks > 0) blipDiagnosticCooldownTicks--;
            bool pauseActive = false;
            try { pauseActive = Function.Call<bool>("IS_PAUSE_MENU_ACTIVE"); } catch { pauseActive = false; }
            if (pauseActive != pauseMenuLastActiveForBlips || (pauseActive && blipDiagnosticCooldownTicks <= 0))
            {
                pauseMenuLastActiveForBlips = pauseActive;
                blipDiagnosticCooldownTicks = 300;
                int ownedBlips = 0;
                for (int i = 0; i < ownedVehicles.Count; i++) if (ownedVehicles[i] != null && OwnedRecordBelongsToCurrentEpisode(ownedVehicles[i]) && ownedVehicles[i].BlipRef != null) ownedBlips++;
                Log("BLIP_DIAG services pauseMenu=" + pauseActive.ToString() + ", dealerBlips=" + dealerBlips.Count.ToString() + ", fuelBlips=" + fuelStationBlips.Count.ToString() + ", metroBlips=" + metroStationBlips.Count.ToString() + ", metroEntrances=" + metroEntranceBlips.Count.ToString() + ", ownedBlips=" + ownedBlips.ToString() + ", retry=" + mapBlipRetryTicks.ToString() + ".");
            }
        }

        private Blip TryCreateCoordinateBlip(Vector3 position, string purpose)
        {

            try
            {
                MethodInfo[] methods = typeof(Blip).GetMethods(BindingFlags.Public | BindingFlags.Static);
                for (int i = 0; i < methods.Length; i++)
                {
                    MethodInfo method = methods[i];
                    if (method == null || method.Name != "AddBlip") continue;
                    ParameterInfo[] parameters = method.GetParameters();
                    object result = null;
                    if (parameters.Length == 1 && parameters[0].ParameterType == typeof(Vector3)) result = method.Invoke(null, new object[] { position });
                    else if (parameters.Length == 3) result = method.Invoke(null, new object[] { position.X, position.Y, position.Z });
                    else continue;
                    Blip blip = result as Blip;
                    if (blip != null)
                    {
                        if (logBlipDiagnostics) Log("BLIP_DIAG created " + purpose + " through AddBlip coordinate path at " + position.X.ToString("0.000") + "," + position.Y.ToString("0.000") + "," + position.Z.ToString("0.000") + ".");
                        return blip;
                    }
                }
            }
            catch (TargetInvocationException ex)
            {
                string detail = ex.InnerException == null ? ex.GetType().Name : ex.InnerException.GetType().Name;
                Log("Coordinate blip AddBlip invocation failed for " + purpose + ": " + detail + ".");
            }
            catch (Exception ex)
            {
                Log("Coordinate blip AddBlip reflection failed for " + purpose + ": " + ex.GetType().Name + ".");
            }


            return null;
        }

        private Blip TryCreateVehicleBlip(Vehicle vehicle, string purpose)
        {
            if (!IsValidVehicle(vehicle)) return null;
            try
            {
                MethodInfo attach = vehicle.GetType().GetMethod("AttachBlip", new Type[0]);
                if (attach != null)
                {
                    object result = attach.Invoke(vehicle, null);
                    Blip blip = result as Blip;
                    if (blip != null) return blip;
                }
            }
            catch (Exception ex)
            {
                Log("Vehicle AttachBlip failed for " + purpose + ": " + ex.GetType().Name + ".");
            }
            try
            {
                MethodInfo[] methods = typeof(Blip).GetMethods(BindingFlags.Public | BindingFlags.Static);
                for (int i = 0; i < methods.Length; i++)
                {
                    MethodInfo method = methods[i];
                    if (method == null || method.Name != "AddBlip") continue;
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length != 1) continue;
                    Type t = parameters[0].ParameterType;
                    if (!t.IsAssignableFrom(vehicle.GetType()) && t.Name.ToLowerInvariant().IndexOf("vehicle") < 0 && t.Name.ToLowerInvariant().IndexOf("entity") < 0) continue;
                    object result = method.Invoke(null, new object[] { vehicle });
                    Blip blip = result as Blip;
                    if (blip != null) return blip;
                }
            }
            catch (Exception ex)
            {
                Log("Vehicle AddBlip reflection failed for " + purpose + ": " + ex.GetType().Name + ".");
            }
            return null;
        }

        private void ConfigureBlip(Blip blip, int icon, BlipDisplay display, BlipColor colour, float scale, string name, string purpose)
        {
            if (blip == null) return;
            try { blip.Icon = (BlipIcon)icon; } catch (Exception ex) { Log("Could not set " + purpose + " icon: " + ex.GetType().Name + "."); }
            try { blip.Display = display; } catch (Exception ex) { Log("Could not set " + purpose + " display: " + ex.GetType().Name + "."); }
            try { blip.Color = colour; } catch (Exception ex) { Log("Could not set " + purpose + " colour: " + ex.GetType().Name + "."); }
            try { blip.Scale = Math.Max(0.01f, scale); } catch (Exception ex) { Log("Could not set " + purpose + " scale: " + ex.GetType().Name + "."); }
            try
            {
                string p = purpose == null ? "" : purpose.ToLowerInvariant();
                bool serviceBlip = p.IndexOf("dealer blip") >= 0 || p.IndexOf("workshop map blip") >= 0 || p.IndexOf("registration blip") >= 0 || p.IndexOf("fuel station blip") >= 0 || p.IndexOf("metro station blip") >= 0 || p.IndexOf("metro entrance blip") >= 0;
                blip.ShowOnlyWhenNear = serviceBlip;
            }
            catch { }
            try { blip.Name = name == null ? "" : name; } catch (Exception ex) { Log("Could not set " + purpose + " name: " + ex.GetType().Name + "."); }


        }


        private void RemoveBlipSafe(Blip blip, string purpose)
        {
            if (blip == null) return;
            try
            {
                MethodInfo m = blip.GetType().GetMethod("Delete", new Type[0]);
                if (m != null) { m.Invoke(blip, null); return; }
            }
            catch { }
            try
            {
                MethodInfo m = blip.GetType().GetMethod("Remove", new Type[0]);
                if (m != null) { m.Invoke(blip, null); return; }
            }
            catch { }
            Log("Could not remove " + purpose + ".");
        }

        private void NormaliseMenuKeys()
        {
            if (keyExit == keyBack) keyExit = Keys.Escape;
        }

        private void LoadIni()
        {
            try
            {
                if (!File.Exists(iniPath)) return;
                string section = "";
                string[] lines = File.ReadAllLines(iniPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string raw = lines[i];
                    string line = raw.Trim();
                    if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#")) continue;
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        section = line.Substring(1, line.Length - 2).Trim();
                        continue;
                    }
                    int eq = line.IndexOf('=');
                    if (eq <= 0) continue;
                    string key = line.Substring(0, eq).Trim();
                    string value = line.Substring(eq + 1).Trim();
                    ApplyIniValue(section, key, value);
                }
            }
            catch (Exception ex)
            {
                Log("INI load failed: " + ex.GetType().Name + ".");
            }
        }

        private void ApplyIniValue(string section, string key, string value)
        {


            string sec = section == null ? "" : section.Trim().ToLowerInvariant();
            string k = key == null ? "" : key.Trim().ToLowerInvariant();

            if (sec == "modules")
            {
                if (k == "enabled") enabled = ReadBool(value, enabled);
                else if (k == "dealers") dealersEnabled = ReadBool(value, dealersEnabled);
                else if (k == "registration") streetRegistrationEnabled = ReadBool(value, streetRegistrationEnabled);
                else if (k == "ownership") ownershipEnabled = ReadBool(value, ownershipEnabled);
                else if (k == "workshop") workshopEnabled = ReadBool(value, workshopEnabled);
                else if (k == "fuel") fuelModuleEnabled = ReadBool(value, fuelModuleEnabled);
                else if (k == "metro") metroStationsEnabled = ReadBool(value, metroStationsEnabled);
                return;
            }

            if (sec == "registration")
            {
                if (k == "allowservicevehicles") allowServiceVehicleRegistration = ReadBool(value, allowServiceVehicleRegistration);
                else if (k == "allowemergencyvehicles") allowEmergencyRegistration = ReadBool(value, allowEmergencyRegistration);
                else if (k == "allowtaxivehicles") allowTaxiRegistration = ReadBool(value, allowTaxiRegistration);
                else if (k == "allowindustrialvehicles") allowIndustrialRegistration = ReadBool(value, allowIndustrialRegistration);
                return;
            }

            if (sec == "prices")
            {
                if (k == "dealerdefaultmarkup") dealerDefaultMarkup = Math.Max(0.01f, ReadFloat(value, dealerDefaultMarkup));
                else if (k == "registrationstreetmultiplier") registrationStreetMultiplier = Math.Max(0.0f, ReadFloat(value, registrationStreetMultiplier));
                else if (k == "insurancedealermultiplier") insuranceDealerMultiplier = Math.Max(0.0f, ReadFloat(value, insuranceDealerMultiplier));
                else if (k == "insurancestreetmultiplier") insuranceStreetMultiplier = Math.Max(0.0f, ReadFloat(value, insuranceStreetMultiplier));
                else if (k == "recoveryinsuredfeemultiplier") recoveryInsuredFeeMultiplier = Math.Max(0.0f, ReadFloat(value, recoveryInsuredFeeMultiplier));
                else if (k == "recoveryuninsuredfeemultiplier") recoveryUninsuredFeeMultiplier = Math.Max(0.0f, ReadFloat(value, recoveryUninsuredFeeMultiplier));
                else if (k == "dealerpricevariancepercent") dealerPriceVariancePercent = Math.Max(0.0f, Math.Min(50.0f, ReadFloat(value, dealerPriceVariancePercent)));
                else if (k == "dealerextrabaseprice") dealerExtraBasePrice = Math.Max(0, ReadInt(value, dealerExtraBasePrice));
                else if (k == "dealerextraswankness0multiplier") dealerExtraSwankness0Multiplier = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness0Multiplier));
                else if (k == "dealerextraswankness1multiplier") dealerExtraSwankness1Multiplier = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness1Multiplier));
                else if (k == "dealerextraswankness2multiplier") dealerExtraSwankness2Multiplier = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness2Multiplier));
                else if (k == "dealerextraswankness3multiplier") dealerExtraSwankness3Multiplier = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness3Multiplier));
                else if (k == "dealerextraswankness4multiplier") dealerExtraSwankness4Multiplier = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness4Multiplier));
                else if (k == "dealerextraswankness5plusmultiplier") dealerExtraSwankness5PlusMultiplier = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness5PlusMultiplier));
                else if (k == "dealerextracheapmultiplier") { float v = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness1Multiplier)); dealerExtraSwankness0Multiplier = v; dealerExtraSwankness1Multiplier = v; }
                else if (k == "dealerextrastandardmultiplier") dealerExtraSwankness2Multiplier = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness2Multiplier));
                else if (k == "dealerextrapremiummultiplier") { float v = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness3Multiplier)); dealerExtraSwankness3Multiplier = v; dealerExtraSwankness4Multiplier = v; }
                else if (k == "dealerextraluxurymultiplier") dealerExtraSwankness5PlusMultiplier = Math.Max(0.0f, ReadFloat(value, dealerExtraSwankness5PlusMultiplier));
                else if (k == "defaultvehiclevalue") defaultVehicleValue = Math.Max(500, ReadInt(value, defaultVehicleValue));
                else if (k == "minimumdealerprice") minimumDealerPrice = Math.Max(0, ReadInt(value, minimumDealerPrice));
                else if (k == "minimumstreetregistrationprice") minimumStreetRegistrationPrice = Math.Max(0, ReadInt(value, minimumStreetRegistrationPrice));
                else if (k == "minimuminsuranceprice") minimumInsurancePrice = Math.Max(0, ReadInt(value, minimumInsurancePrice));
                return;
            }

            if (sec == "priceoverrides")
            {
                int overrideValue = ReadInt(value, -1);
                if (overrideValue > 0) priceOverrides[k] = overrideValue;
                return;
            }

            if (sec == "swanknessoverrides")
            {
                swanknessOverrides[k] = Math.Max(0, ReadInt(value, 2));
                return;
            }

            if (sec == "controls")
            {
                if (k == "open") keyOpen = ReadKey(value, keyOpen);
                else if (k == "menuup") keyUp = ReadKey(value, keyUp);
                else if (k == "menudown") keyDown = ReadKey(value, keyDown);
                else if (k == "menuleft") keyLeft = ReadKey(value, keyLeft);
                else if (k == "menuright") keyRight = ReadKey(value, keyRight);
                else if (k == "select") keySelect = ReadKey(value, keySelect);
                else if (k == "back") keyBack = ReadKey(value, keyBack);
                else if (k == "exit") keyExit = ReadKey(value, keyExit);
                return;
            }

            if (sec == "ownership")
            {
                if (k == "maxownedcarsperepisode") maxOwnedCarsPerEpisode = Math.Max(0, ReadInt(value, maxOwnedCarsPerEpisode));
                else if (k == "customplateprice") customPlatePrice = Math.Max(0, ReadInt(value, customPlatePrice));
                else if (k == "steviedeliveryfee") stevieDeliveryFee = Math.Max(0, ReadInt(value, stevieDeliveryFee));
                else if (k == "customplatemaxlength") customPlateMaxLength = Math.Max(1, Math.Min(CustomPlateAbsoluteMaxLength, ReadInt(value, customPlateMaxLength)));
                return;
            }

            if (sec == "fuel")
            {
                if (k == "enabled") fuelModuleEnabled = ReadBool(value, fuelModuleEnabled);
                else if (k == "fuelpricepergallon") fuelPricePerGallon = Math.Max(0.0f, ReadFloat(value, fuelPricePerGallon));
                else if (k == "consumptionmultiplier") fuelConsumptionMultiplier = Math.Max(0.0f, ReadFloat(value, fuelConsumptionMultiplier));
                else if (k == "missionconsumptionmultiplier") fuelMissionConsumptionMultiplier = Math.Max(0.0f, Math.Min(1.0f, ReadFloat(value, fuelMissionConsumptionMultiplier)));
                else if (k == "missionminimumfuelpercent") fuelMissionMinimumPercent = ClampFloat(ReadFloat(value, fuelMissionMinimumPercent), 0.0f, 100.0f);
                else if (k == "startingfuelminpercent") fuelStartingMinPercent = ClampFloat(ReadFloat(value, fuelStartingMinPercent), 0.0f, 100.0f);
                else if (k == "startingfuelmaxpercent") fuelStartingMaxPercent = ClampFloat(ReadFloat(value, fuelStartingMaxPercent), 0.0f, 100.0f);
                else if (k == "dealerpurchasefuelpercent") fuelDealerPurchasePercent = ClampFloat(ReadFloat(value, fuelDealerPurchasePercent), 0.0f, 100.0f);
                else if (k == "refuelrategallonspersecond") fuelRefuelRateGallonsPerSecond = Math.Max(0.05f, ReadFloat(value, fuelRefuelRateGallonsPerSecond));
                else if (k == "lowfuelpromptpercent") fuelLowPromptPercent = ClampFloat(ReadFloat(value, fuelLowPromptPercent), 0.0f, 100.0f);
                else if (k == "sputterpercent") fuelSputterPercent = ClampFloat(ReadFloat(value, fuelSputterPercent), 0.0f, 100.0f);
                else if (k == "criticalsputterpercent") fuelCriticalSputterPercent = ClampFloat(ReadFloat(value, fuelCriticalSputterPercent), 0.0f, 100.0f);
                else if (k == "showfueltestdisplay" || k == "fueltestdisplay") fuelTestDisplayEnabled = ReadBool(value, fuelTestDisplayEnabled);
                else if (k == "hidehudduringcutscenes" || k == "hidefuelhudduringcutscenes") fuelHideHudDuringCutscenes = ReadBool(value, fuelHideHudDuringCutscenes);
                return;
            }

            if (sec == "metro")
            {
                if (k == "enabled") metroStationsEnabled = ReadBool(value, metroStationsEnabled);
                else if (k == "showstations" || k == "showstationblips") metroStationBlipsEnabled = ReadBool(value, metroStationBlipsEnabled);
                else if (k == "showentrances" || k == "showentranceblips") metroEntranceBlipsEnabled = ReadBool(value, metroEntranceBlipsEnabled);
                return;
            }

            if (sec == "plates" || sec == "libertycityplates")
            {
                if (k == "enabled") libertyCityPlatesEnabled = ReadBool(value, libertyCityPlatesEnabled);
                else if (k == "usebridge" || k == "uselibertycityplatesbridge") useLibertyCityPlatesBridge = ReadBool(value, useLibertyCityPlatesBridge);
                else if (k == "generateownedplates" || k == "generateplatesforownedvehicles") generatePlateForOwnedVehicles = ReadBool(value, generatePlateForOwnedVehicles);
                else if (k == "uselcpdefinitions" || k == "useplatedefinitions" || k == "usenativeformats") useLcpPlateDefinitions = ReadBool(value, useLcpPlateDefinitions);
                else if (k == "required") libertyCityPlatesRequired = ReadBool(value, libertyCityPlatesRequired);
                else if (k == "logdiagnostics") logPlateDiagnostics = ReadBool(value, logPlateDiagnostics);
                return;
            }

            if (sec == "debug")
            {
                if (k == "showdebugmessages") showDebugMessages = ReadBool(value, showDebugMessages);
                else if (k == "logtofile") logToFile = ReadBool(value, logToFile);
                else if (k == "logpositionhotkeyenabled") logPositionHotkeyEnabled = ReadBool(value, logPositionHotkeyEnabled);
                else if (k == "logpositionkey") keyLogPosition = ReadKey(value, keyLogPosition);
                return;
            }
        }


        private void AddModels(DealerDefinition d, string value)
        {
            if (d == null || value == null) return;
            string[] parts = value.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                string m = parts[i].Trim().ToLowerInvariant();
                if (m.Length == 0) continue;
                d.Models.Add(m);
            }
        }

        private bool DetectLibertyCityPlates(bool logWhenMissing)
        {
            if (!libertyCityPlatesEnabled) return false;
            if (lcpDetectionDone)
            {
                if (!lcpDetected && logWhenMissing && !lcpMissingLogged)
                {
                    Log("LCP_NOT_FOUND: LibertyCityPlates.asi was not detected. Stored plate texts will be kept but not visually applied.");
                    lcpMissingLogged = true;
                }
                return lcpDetected;
            }
            lcpDetectionDone = true;
            lcpDetected = false;
            try
            {
                if (File.Exists("plugins\\LibertyCityPlates.asi")) lcpDetected = true;
                else if (File.Exists("LibertyCityPlates.asi")) lcpDetected = true;
                else if (File.Exists("update\\pc\\textures\\libertycityplates.wtd")) lcpDetected = true;
            }
            catch { lcpDetected = false; }
            if (lcpDetected)
            {
                if (logPlateDiagnostics) Log("LCP_DETECTED: LibertyCityPlates files detected. Plate application will be attempted for owned vehicles.");
            }
            else if (logWhenMissing || libertyCityPlatesRequired)
            {
                if (!lcpMissingLogged)
                {
                    Log("LCP_NOT_FOUND: LibertyCityPlates.asi was not detected. Stored plate texts will be kept but not visually applied.");
                    lcpMissingLogged = true;
                }
            }
            return lcpDetected;
        }

        private void EnsureLcpPlateDefinitionsLoaded()
        {
            if (lcpPlateDefinitions == null) lcpPlateDefinitions = new List<LcpPlateDefinition>();
            if (lcpPlateById == null) lcpPlateById = new Dictionary<int, LcpPlateDefinition>();
            if (lcpUsedPlateByModel == null) lcpUsedPlateByModel = new Dictionary<string, int>();
            if (lcpPlateDefinitions.Count == 0) LoadLcpPlateDefinitions();
            if (lcpPlateDefinitions.Count == 0) AddFallbackLcpPlateDefinitions();
        }
        private void LoadLcpPlateDefinitions()
        {
            lcpPlateDefinitions.Clear(); lcpPlateById.Clear(); lcpUsedPlateByModel.Clear();
            string[] candidates = new string[] { "update\\common\\data\\vehicles.ide", "common\\data\\vehicles.ide" };
            bool loaded = false;
            for (int i = 0; i < candidates.Length; i++)
            {
                string path = candidates[i]; if (!File.Exists(path)) continue;
                try { ParseLcpVehicleIde(path); if (lcpPlateDefinitions.Count > 0) { loaded = true; break; } }
                catch (Exception ex) { if (logPlateDiagnostics) Log("LCP_PLATE_DEFINITION_PARSE_FAILED: " + path + " " + ex.GetType().Name + "."); }
            }
            if (!loaded) AddFallbackLcpPlateDefinitions();
            if (logPlateDiagnostics) Log("LCP_PLATE_DEFINITIONS: plates=" + lcpPlateDefinitions.Count.ToString() + ", used_plates=" + lcpUsedPlateByModel.Count.ToString() + ".");
        }
        private void ParseLcpVehicleIde(string path)
        {
            string section = ""; string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i] == null ? "" : lines[i].Trim(); if (line.Length == 0 || line.StartsWith("#")) continue;
                string lower = line.ToLowerInvariant();
                if (lower == "license_plates") { section = "license_plates"; continue; }
                if (lower == "used_plates") { section = "used_plates"; continue; }
                if (lower == "end") { section = ""; continue; }
                if (section == "license_plates") ParseLcpLicensePlateLine(line); else if (section == "used_plates") ParseLcpUsedPlateLine(line);
            }
        }
        private void ParseLcpLicensePlateLine(string line)
        {
            string[] t = SplitIdeTokens(line); if (t.Length < 14) return; int id = ReadInt(t[0], -1); if (id < 0) return;
            LcpPlateDefinition d = new LcpPlateDefinition(); d.Id = id; d.ParentId = t[1].ToLowerInvariant() == "null" ? -1 : ReadInt(t[1], -1); d.Probability = ReadInt(t[2], 0); d.PlateTxd = ReadInt(t[3], 0); d.LettersTxd = ReadInt(t[4], 0); d.ScaleX = ReadFloat(t[5], 1.0f); d.ScaleY = ReadFloat(t[6], 0.43f); d.OffsetX = ReadFloat(t[7], 0.0f); d.OffsetY = ReadFloat(t[8], 0.0f); d.TintR = ReadFloat(t[9], 0.0f); d.TintG = ReadFloat(t[10], 0.0f); d.TintB = ReadFloat(t[11], 0.0f); d.TintA = ReadFloat(t[12], 1.0f); d.Format = t[13]; d.Name = GetLcpPlateDisplayName(id); AddLcpPlateDefinition(d);
        }
        private string[] SplitIdeTokens(string line)
        {
            List<string> tokens = new List<string>(); string current = "";
            for (int i = 0; i < line.Length; i++) { char c = line[i]; if (c == ',' || Char.IsWhiteSpace(c)) { if (current.Length > 0) { tokens.Add(current); current = ""; } } else current += c; }
            if (current.Length > 0) tokens.Add(current); return tokens.ToArray();
        }
        private void ParseLcpUsedPlateLine(string line)
        {
            string[] t = SplitIdeTokens(line); if (t.Length < 2) return; string model = t[0].Trim().ToLowerInvariant(); int id = ReadInt(t[1], -1); if (model.Length > 0 && id >= 0) lcpUsedPlateByModel[model] = id;
        }
        private void AddLcpPlateDefinition(LcpPlateDefinition d)
        {
            if (d == null || d.Format == null || d.Format.Length == 0) return; if (lcpPlateById.ContainsKey(d.Id)) lcpPlateById[d.Id] = d; else lcpPlateById.Add(d.Id, d);
            bool replaced = false; for (int i = 0; i < lcpPlateDefinitions.Count; i++) { if (lcpPlateDefinitions[i] != null && lcpPlateDefinitions[i].Id == d.Id) { lcpPlateDefinitions[i] = d; replaced = true; break; } } if (!replaced) lcpPlateDefinitions.Add(d);
        }
        private void AddFallbackLcpPlateDefinitions()
        {
            if (lcpPlateDefinitions.Count > 0) return;
            AddFallbackLcpPlateDefinition(16, "Taxi", 5, 0, 1.25f, 0.43f, 0.0f, -0.07f, 0.0352f, 0.1647f, 0.3019f, 1.0f, "^&^^("); AddFallbackLcpPlateDefinition(17, "Ambulance", 10, 0, 1.25f, 0.43f, 0.0f, -0.07f, 0.0352f, 0.1647f, 0.3019f, 1.0f, "^^^^EV"); AddFallbackLcpPlateDefinition(18, "Police", 4, 1, 2.0f, 0.8f, 0.0f, 0.0f, 0.0f, 0.0941f, 0.5803f, 1.0f, "^^^^"); AddFallbackLcpPlateDefinition(19, "FIB", 6, 1, 1.4f, 0.5f, -0.043f, -0.05f, 0.0f, 0.094f, 0.580f, 1.0f, "(&&&&&"); AddFallbackLcpPlateDefinition(20, "Commercial", 9, 0, 1.25f, 0.43f, 0.0f, -0.07f, 0.0352f, 0.1647f, 0.3019f, 1.0f, "^^^^^*&&"); AddFallbackLcpPlateDefinition(21, "Official", 8, 0, 1.25f, 0.43f, 0.0f, -0.07f, 0.0352f, 0.1647f, 0.3019f, 1.0f, "&*^^^^^"); AddFallbackLcpPlateDefinition(22, "Limo / T&LC", 7, 0, 1.25f, 0.43f, 0.0f, -0.07f, 0.0352f, 0.1647f, 0.3019f, 1.0f, "T^^^^^^C"); AddFallbackLcpPlateDefinition(23, "Liberty City 2008", 0, 0, 1.25f, 0.43f, 0.0f, -0.07f, 0.0352f, 0.1647f, 0.3019f, 1.0f, "&&&*^^^^"); AddFallbackLcpPlateDefinition(24, "Alderney in LC", 2, 3, 1.4f, 0.48f, 0.0f, -0.02f, 0.0f, 0.0f, 0.0f, 1.0f, "&&&*^^&"); AddFallbackLcpPlateDefinition(25, "LC motorcycle", 1, 0, 1.39f, 0.49f, 0.07f, -0.06f, 0.0352f, 0.1647f, 0.3019f, 1.0f, "^^&&^^"); AddFallbackLcpPlateDefinition(26, "Liberty City 1986", 12, 0, 1.3f, 0.43f, 0.0f, -0.07f, 0.114f, 0.176f, 0.341f, 1.0f, "&&&##^^^"); AddFallbackLcpPlateDefinition(27, "Leonida", 15, 3, 1.42f, 0.48f, 0.0f, -0.07f, 0.024f, 0.369f, 0.302f, 1.0f, "&^^#^&&"); AddFallbackLcpPlateDefinition(28, "Gloriana", 16, 4, 1.4f, 0.46f, 0.0f, -0.016f, 0.02f, 0.02f, 0.02f, 1.0f, "&&&#^^^^"); AddFallbackLcpPlateDefinition(29, "San Andreas", 13, 4, 1.4f, 0.46f, 0.0f, -0.18f, 0.051f, 0.122f, 0.263f, 1.0f, "^&&&^^^"); AddFallbackLcpPlateDefinition(30, "Las Venturas", 3, 1, 1.45f, 0.48f, 0.0f, -0.07f, 0.141f, 0.2f, 0.369f, 1.0f, "&&&*&&&"); AddFallbackLcpPlateDefinition(31, "North Yankton", 14, 4, 1.4f, 0.47f, 0.0f, -0.14f, 0.0f, 0.0f, 0.0f, 1.0f, "&&&#^^^"); AddFallbackLcpPlateDefinition(32, "Liberty 2008 rust", 17, 2, 1.25f, 0.43f, 0.0f, -0.07f, 0.0352f, 0.1647f, 0.3019f, 1.0f, "&&&*^^^^"); AddFallbackLcpPlateDefinition(33, "Liberty 1986 rust", 18, 2, 1.3f, 0.43f, 0.0f, -0.07f, 0.114f, 0.176f, 0.341f, 1.0f, "&&&##^^^"); AddFallbackLcpPlateDefinition(34, "Liberty 1973 rust", 19, 2, 1.4f, 0.475f, 0.0f, 0.1f, 0.016f, 0.09f, 0.349f, 1.0f, "^^^(&&&");
            string[] used = new string[] { "cabby:16", "taxi:16", "taxi2:16", "ambulance:17", "noose:18", "nstockade:18", "police:18", "police2:18", "police3:18", "police4:18", "policeb:18", "polpatriot:18", "pstockade:18", "fbi:19", "limo2:22", "rom:22", "stretch:22", "phantom:20", "stockade:20", "bus:21", "trash:21", "buccaneer:26", "emperor:26", "manana:26", "marbella:26", "virgo:26", "gburrito:32", "sabre2:32", "regina:32", "yankee2:32", "emperor2:33", "towtruck:33", "vigero2:33", "peyote:34", "voodoo:34" }; for (int i = 0; i < used.Length; i++) { string[] parts = used[i].Split(':'); if (parts.Length == 2) lcpUsedPlateByModel[parts[0]] = ReadInt(parts[1], -1); }
        }
        private void AddFallbackLcpPlateDefinition(int id, string name, int plateTxd, int lettersTxd, float sx, float sy, float ox, float oy, float tr, float tg, float tb, float ta, string format)
        { LcpPlateDefinition d = new LcpPlateDefinition(); d.Id = id; d.ParentId = -1; d.Probability = 0; d.PlateTxd = plateTxd; d.LettersTxd = lettersTxd; d.ScaleX = sx; d.ScaleY = sy; d.OffsetX = ox; d.OffsetY = oy; d.TintR = tr; d.TintG = tg; d.TintB = tb; d.TintA = ta; d.Format = format; d.Name = name; d.IsFallback = true; AddLcpPlateDefinition(d); }
        private string GetLcpPlateDisplayName(int id)
        { if (id == 0) return "Alderney"; if (id == 1) return "Alderney motorcycle"; if (id == 2) return "Default Liberty"; if (id == 4) return "Heli"; if (id == 5) return "Plane"; if (id == 16) return "Taxi"; if (id == 17) return "Ambulance"; if (id == 18) return "Police"; if (id == 19) return "FIB"; if (id == 20) return "Commercial"; if (id == 21) return "Official"; if (id == 22) return "Limo / T&LC"; if (id == 23) return "Liberty City 2008"; if (id == 24) return "Alderney in LC"; if (id == 25) return "LC motorcycle"; if (id == 26) return "Liberty City 1986"; if (id == 27) return "Leonida"; if (id == 28) return "Gloriana"; if (id == 29) return "San Andreas"; if (id == 30) return "Las Venturas"; if (id == 31) return "North Yankton"; if (id == 32) return "Liberty 2008 rust"; if (id == 33) return "Liberty 1986 rust"; if (id == 34) return "Liberty 1973 rust"; return "LCP plate " + id.ToString(); }
        private List<LcpPlateDefinition> GetAllowedLcpPlateDefinitionsForModel(string modelName)
        {
            EnsureLcpPlateDefinitionsLoaded(); List<LcpPlateDefinition> result = new List<LcpPlateDefinition>(); string model = modelName == null ? "" : modelName.Trim().ToLowerInvariant(); int fixedId;
            if (model.Length > 0 && lcpUsedPlateByModel.TryGetValue(model, out fixedId)) { LcpPlateDefinition fixedDef; if (lcpPlateById.TryGetValue(fixedId, out fixedDef)) result.Add(fixedDef); return result; }
            VehicleDefinition vehicleDef; if (model.Length > 0 && vehicleByModel.TryGetValue(model, out vehicleDef) && vehicleDef != null && vehicleDef.IsBike) { AddPlateChoiceIfExists(result, 25); if (result.Count > 0) return result; }
            int[] civilian = new int[] { 23, 24, 26, 27, 28, 29, 30, 31 }; for (int i = 0; i < civilian.Length; i++) AddPlateChoiceIfExists(result, civilian[i]);
            if (result.Count == 0) { for (int i = 0; i < lcpPlateDefinitions.Count; i++) { LcpPlateDefinition d = lcpPlateDefinitions[i]; if (d != null && d.Id != 16 && d.Id != 17 && d.Id != 18 && d.Id != 19 && d.Id != 20 && d.Id != 21 && d.Id != 22) result.Add(d); } }
            if (result.Count == 0 && lcpPlateDefinitions.Count > 0) result.Add(lcpPlateDefinitions[0]); return result;
        }
        private void AddPlateChoiceIfExists(List<LcpPlateDefinition> list, int id) { LcpPlateDefinition d; if (!lcpPlateById.TryGetValue(id, out d) || d == null) return; for (int i = 0; i < list.Count; i++) if (list[i] != null && list[i].Id == id) return; list.Add(d); }
        private LcpPlateDefinition GetSelectedCustomPlateDefinition() { if (customPlateChoices == null || customPlateChoices.Count == 0) return customPlateTarget == null ? null : GetDefaultLcpPlateDefinitionForModel(customPlateTarget.ModelName); if (customPlateDefinitionIndex < 0) customPlateDefinitionIndex = 0; if (customPlateDefinitionIndex >= customPlateChoices.Count) customPlateDefinitionIndex = customPlateChoices.Count - 1; return customPlateChoices[customPlateDefinitionIndex]; }
        private void CycleCustomPlateDefinition(int delta) { if (customPlateChoices == null || customPlateChoices.Count <= 1) return; customPlateDefinitionIndex += delta; if (customPlateDefinitionIndex < 0) customPlateDefinitionIndex = customPlateChoices.Count - 1; if (customPlateDefinitionIndex >= customPlateChoices.Count) customPlateDefinitionIndex = 0; LcpPlateDefinition def = GetSelectedCustomPlateDefinition(); int maxLen = def == null ? GetCustomPlateMaxLength() : CountInputSlotsForFormat(def.Format); maxLen = Math.Max(1, Math.Min(CustomPlateAbsoluteMaxLength, maxLen)); if (customPlateInput != null && customPlateInput.Length > maxLen) customPlateInput = customPlateInput.Substring(0, maxLen); }
        private LcpPlateDefinition GetDefaultLcpPlateDefinitionForModel(string modelName) { List<LcpPlateDefinition> list = GetAllowedLcpPlateDefinitionsForModel(modelName); if (list.Count > 0) return list[0]; return null; }
        private LcpPlateDefinition GetStoredLcpPlateDefinition(OwnedVehicleRecord rec) { if (rec == null) return null; EnsureLcpPlateDefinitionsLoaded(); LcpPlateDefinition d; if (rec.LcpPlateId >= 0 && lcpPlateById.TryGetValue(rec.LcpPlateId, out d)) return d; return GetDefaultLcpPlateDefinitionForModel(rec.ModelName); }
        private int CountInputSlotsForFormat(string format) { if (format == null) return GetCustomPlateMaxLength(); int count = 0; for (int i = 0; i < format.Length; i++) { char c = format[i]; if (c == '&' || c == '^' || c == '%' || c == '(') count++; } return count; }
        private LcpPlatePreview BuildLcpPlatePreview(LcpPlateDefinition def, string rawInput, bool fillMissingRandom)
        {
            LcpPlatePreview p = new LcpPlatePreview();
            p.RawInput = CleanPlateRawInput(rawInput);
            List<char> raw = new List<char>();
            int typedDigits = 0;
            for (int i = 0; i < p.RawInput.Length; i++)
            {
                char c = p.RawInput[i];
                raw.Add(c);
                if (c >= '0' && c <= '9') typedDigits++;
            }
            bool[] used = new bool[raw.Count];
            int sequentialIndex = 0;
            List<char> canonical = new List<char>();
            List<char> display = new List<char>();
            bool complete = true;

            if (def == null || def.Format == null)
            {
                p.Canonical = CleanPlateText(rawInput);
                p.Display = DisplayLcpPlateText(p.Canonical);
                p.Complete = p.Canonical.Length > 0;
                p.MissingText = p.Complete ? "" : "no plate format";
                return p;
            }

            for (int i = 0; i < def.Format.Length; i++)
            {
                char f = def.Format[i];
                if (f == '^')
                {
                    p.RequiredDigits++;
                    char c = '\0';
                    for (int j = sequentialIndex; j < raw.Count; j++)
                    {
                        if (!used[j] && raw[j] >= '0' && raw[j] <= '9')
                        {
                            c = raw[j];
                            used[j] = true;
                            sequentialIndex = j + 1;
                            break;
                        }
                    }
                    if (c == '\0' && fillMissingRandom) c = (char)('0' + plateRandom.Next(0, 10));
                    if (c == '\0') { c = '_'; complete = false; }
                    canonical.Add(c); display.Add(c);
                }
                else if (f == '&' || f == '%')
                {
                    p.RequiredAny++;
                    char c = '\0';
                    for (int j = sequentialIndex; j < raw.Count; j++)
                    {
                        if (!used[j])
                        {
                            c = raw[j];
                            used[j] = true;
                            sequentialIndex = j + 1;
                            break;
                        }
                    }
                    if (c == '\0' && fillMissingRandom) c = GenerateRandomPlateAnyChar();
                    if (c == '\0') { c = '_'; complete = false; }
                    canonical.Add(c); display.Add(c);
                }
                else if (f == '(')
                {
                    p.RequiredLetters++;
                    char c = '\0';
                    for (int j = sequentialIndex; j < raw.Count; j++)
                    {
                        if (!used[j] && (raw[j] == 'A' || raw[j] == 'B'))
                        {
                            c = raw[j];
                            used[j] = true;
                            sequentialIndex = j + 1;
                            break;
                        }
                    }
                    if (c == '\0' && fillMissingRandom) c = plateRandom.Next(0, 2) == 0 ? 'A' : 'B';
                    if (c == '\0') { c = '_'; complete = false; }
                    canonical.Add(c); display.Add(c);
                }
                else if (f == '*') { canonical.Add('*'); display.Add('·'); }
                else if (f == '#') { canonical.Add('#'); display.Add(' '); }
                else { char fixedChar = Char.ToUpperInvariant(f); canonical.Add(fixedChar); display.Add(fixedChar); }
            }

            p.TypedAny = p.RawInput.Length;
            p.TypedDigits = typedDigits;
            p.TypedLetters = p.TypedAny - p.TypedDigits;
            p.Canonical = new string(canonical.ToArray());
            p.Display = new string(display.ToArray());
            p.Complete = complete;
            p.MissingText = BuildPlateMissingText(p);
            return p;
        }
        private char GenerateRandomPlateAnyChar() { int n = plateRandom.Next(0, 36); if (n < 10) return (char)('0' + n); return (char)('A' + (n - 10)); }
        private string BuildPlateMissingText(LcpPlatePreview p)
        {
            if (p == null || p.Complete) return "";
            int digitMissing = p.RequiredDigits > p.TypedDigits ? p.RequiredDigits - p.TypedDigits : 0;
            int totalRequired = p.RequiredAny + p.RequiredDigits + p.RequiredLetters;
            int totalTyped = p.TypedAny;
            int anyMissing = totalRequired > totalTyped ? totalRequired - totalTyped : 0;
            if (anyMissing > 0 && digitMissing > 0) return anyMissing.ToString() + " chars incl. " + digitMissing.ToString() + " digit" + (digitMissing == 1 ? "" : "s");
            if (digitMissing > 0) return digitMissing.ToString() + " digit" + (digitMissing == 1 ? "" : "s");
            if (anyMissing > 0) return anyMissing.ToString() + " char" + (anyMissing == 1 ? "" : "s");
            return "compatible chars";
        }
        private string GetPlateRequirementText(LcpPlatePreview p)
        {
            if (p == null) return "";
            List<string> parts = new List<string>();
            if (p.RequiredAny > 0) parts.Add(p.RequiredAny.ToString() + " chars");
            if (p.RequiredDigits > 0) parts.Add(p.RequiredDigits.ToString() + " digits");
            if (p.RequiredLetters > 0) parts.Add(p.RequiredLetters.ToString() + " A/B");
            if (parts.Count == 0) return "";
            return "Requires: " + String.Join(", ", parts.ToArray());
        }
        private string GetPlateMissingText(LcpPlatePreview p)
        {
            if (p == null || p.Complete) return "Ready to order.";
            return "Missing: " + p.MissingText;
        }
        private string FormatPlatePatternForUi(string format) { if (format == null) return ""; string result = ""; for (int i = 0; i < format.Length; i++) { char c = format[i]; if (c == '&') result += "X"; else if (c == '^') result += "0"; else if (c == '%') result += "X"; else if (c == '(') result += "A/B"; else if (c == '*') result += "·"; else if (c == '#') result += " "; else result += Char.ToUpperInvariant(c); } return result; }
        private string DisplayLcpPlateText(string canonical) { if (canonical == null) return ""; string s = CleanPlateText(canonical); s = s.Replace('*', '·'); s = s.Replace('#', ' '); return s; }
        private void EnsurePlateForOwnedRecord(OwnedVehicleRecord rec)
        {
            if (rec == null || !generatePlateForOwnedVehicles) return; EnsureLcpPlateDefinitionsLoaded(); string cleaned = CleanPlateText(rec.PlateText);
            if (cleaned.Length > 0) { rec.PlateText = cleaned; if (useLcpPlateDefinitions) EnsureStoredLcpPlateMetadata(rec); return; }
            if (useLcpPlateDefinitions) { LcpPlateDefinition def = GetDefaultLcpPlateDefinitionForModel(rec.ModelName); if (def != null) { for (int attempt = 0; attempt < 64; attempt++) { string raw = GenerateRawInputForLcpFormat(def.Format); LcpPlatePreview preview = BuildLcpPlatePreview(def, raw, true); if (preview == null || preview.Canonical == null || preview.Canonical.Length == 0) continue; if (!IsPlateUsedByAnotherOwnedVehicle(preview.Canonical, def.Id, rec)) { rec.PlateText = preview.Canonical; rec.LcpPlateId = def.Id; rec.LcpPlateFormat = def.Format; rec.LcpPlateName = def.Name; rec.LcpRawInput = preview.RawInput; rec.PlateGeneratedThisSession = true; rec.PlateApplied = false; rec.PlateApplyAttempts = 0; rec.PlateApplyCooldown = 0; if (logPlateDiagnostics) Log("PLATE_GENERATED_LCP: " + rec.Id + " -> type=" + def.Id.ToString() + " " + rec.PlateText + "."); return; } } } }
            rec.PlateText = GenerateUniquePlateText(); rec.PlateGeneratedThisSession = true; rec.PlateApplied = false; rec.PlateApplyAttempts = 0; rec.PlateApplyCooldown = 0; if (logPlateDiagnostics) Log("PLATE_GENERATED: " + rec.Id + " -> " + rec.PlateText + ".");
        }
        private void EnsureStoredLcpPlateMetadata(OwnedVehicleRecord rec)
        {
            if (rec == null) return;
            LcpPlateDefinition def = null;
            if (rec.LcpPlateId >= 0) lcpPlateById.TryGetValue(rec.LcpPlateId, out def);
            if (def == null) def = GetDefaultLcpPlateDefinitionForModel(rec.ModelName);
            if (def == null) return;
            string raw = CleanPlateRawInput(rec.LcpRawInput);
            if (raw.Length == 0) raw = CleanPlateRawInput(rec.PlateText);
            LcpPlatePreview preview = BuildLcpPlatePreview(def, raw, false);
            if (preview == null || !preview.Complete)
            {
                raw = GenerateRawInputForLcpFormat(def.Format);
                preview = BuildLcpPlatePreview(def, raw, true);
            }
            if (preview != null && preview.Canonical != null && preview.Canonical.Length > 0)
            {
                rec.PlateText = preview.Canonical;
                rec.LcpRawInput = preview.RawInput;
            }
            rec.LcpPlateId = def.Id;
            rec.LcpPlateFormat = def.Format;
            rec.LcpPlateName = def.Name;
        }
        private string GenerateRawInputForLcpFormat(string format) { if (format == null || format.Length == 0) return GeneratePlateText(7); string raw = ""; for (int i = 0; i < format.Length; i++) { char f = format[i]; if (f == '^') raw += (char)('0' + plateRandom.Next(0, 10)); else if (f == '&' || f == '%') raw += GenerateRandomPlateAnyChar(); else if (f == '(') raw += (plateRandom.Next(0, 2) == 0 ? 'A' : 'B'); } return raw; }
        private string GenerateUniquePlateText() { int len = generatedPlateLength; if (len < 1) len = 7; if (len > CustomPlateAbsoluteMaxLength) len = CustomPlateAbsoluteMaxLength; for (int attempt = 0; attempt < 64; attempt++) { string p = GeneratePlateText(len); bool used = false; for (int i = 0; i < ownedVehicles.Count; i++) { if (ownedVehicles[i] != null && CleanPlateText(ownedVehicles[i].PlateText) == p) { used = true; break; } } if (!used) return p; } return GeneratePlateText(len); }
        private string GeneratePlateText(int len) { if (len <= 0) len = 7; if (len > CustomPlateAbsoluteMaxLength) len = CustomPlateAbsoluteMaxLength; char[] chars = new char[len]; for (int i = 0; i < len; i++) { bool letter = i < 3; if (len <= 4) letter = i % 2 == 0; if (letter) chars[i] = (char)('A' + plateRandom.Next(0, 26)); else chars[i] = (char)('0' + plateRandom.Next(0, 10)); } return new string(chars); }
        private string CleanPlateRawInput(string value) { if (value == null) return ""; List<char> chars = new List<char>(); string up = value.ToUpperInvariant(); for (int i = 0; i < up.Length && chars.Count < CustomPlateAbsoluteMaxLength; i++) { char c = up[i]; if ((c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')) chars.Add(c); } return new string(chars.ToArray()); }
        private string CleanPlateText(string value) { if (value == null) return ""; List<char> chars = new List<char>(); string up = value.ToUpperInvariant(); for (int i = 0; i < up.Length && chars.Count < CustomPlateAbsoluteMaxLength; i++) { char c = up[i]; if ((c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')) chars.Add(c); else if (c == '*' || c == '·' || c == '•') chars.Add('*'); else if (c == '#' || c == ' ' || c == '-' || c == '_') chars.Add('#'); } return new string(chars.ToArray()); }

        private void ScheduleOwnedVehiclePlateApplication(OwnedVehicleRecord rec, bool delayed)
        {
            if (rec == null) return;
            EnsurePlateForOwnedRecord(rec);
            rec.PlateApplied = false;
            rec.PlateApplySucceeded = false;
            rec.PlateApplyAttempts = 0;
            rec.PlateApplyCooldown = delayed ? plateApplyStartDelayTicks : 0;
            if (logPlateDiagnostics) Log("LCP_APPLY_SCHEDULED: " + rec.Id + " " + rec.ModelName + " plate=" + rec.PlateText + " delay=" + rec.PlateApplyCooldown.ToString() + ".");
        }

        private void UpdateOwnedVehiclePlateApplications()
        {
            if (!ownershipEnabled || !libertyCityPlatesEnabled) return;
            for (int i = 0; i < ownedVehicles.Count; i++)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (rec == null || rec.Destroyed || !OwnedRecordBelongsToCurrentEpisode(rec)) continue;
                if (!IsValidVehicle(rec.VehicleRef)) continue;

                if (rec.PlateText == null || rec.PlateText.Length == 0)
                {
                    EnsurePlateForOwnedRecord(rec);
                    ScheduleOwnedVehiclePlateApplication(rec, true);
                }


                if (rec.PlateApplySucceeded) continue;
                if (rec.PlateApplyAttempts >= plateApplyMaxAttempts) continue;
                if (rec.PlateApplyCooldown > 0) { rec.PlateApplyCooldown--; continue; }

                bool ok = TryApplyOwnedVehiclePlate(rec, rec.VehicleRef, true);
                rec.PlateApplyCooldown = ok ? 0 : plateApplyRetryTicks;
            }
        }

        private bool TryApplyOwnedVehiclePlate(OwnedVehicleRecord rec, Vehicle v, bool countAttempt)
        {
            if (rec == null || !IsValidVehicle(v)) return false;
            if (!libertyCityPlatesEnabled) return false;
            EnsurePlateForOwnedRecord(rec);
            if (rec.PlateText == null || rec.PlateText.Length == 0) return false;
            if (!DetectLibertyCityPlates(true))
            {
                if (libertyCityPlatesRequired && logPlateDiagnostics) Log("LCP_REQUIRED_BUT_MISSING: cannot apply plate for " + rec.Id + ".");
                return false;
            }
            if (countAttempt) rec.PlateApplyAttempts++;
            bool ok = ApplyLibertyCityPlateToVehicle(v, rec);
            if (ok)
            {
                rec.PlateApplied = true;
                rec.PlateApplySucceeded = true;
                if (rec.PlateGeneratedThisSession)
                {
                    rec.PlateGeneratedThisSession = false;
                    SaveOwnedVehicles();
                }
                if (logPlateDiagnostics) Log("LCP_APPLY_OK: " + rec.Id + " " + rec.ModelName + " plate=" + rec.PlateText + " attempt=" + rec.PlateApplyAttempts.ToString() + ".");
                return true;
            }
            rec.PlateApplied = false;
            if (logPlateDiagnostics) Log("LCP_APPLY_FAILED: " + rec.Id + " " + rec.ModelName + " plate=" + rec.PlateText + " attempt=" + rec.PlateApplyAttempts.ToString() + ".");
            return false;
        }

        private bool ApplyLibertyCityPlateToVehicle(Vehicle v, OwnedVehicleRecord rec)
        {
            if (rec == null) return false;
            string clean = CleanPlateText(rec.PlateText);
            if (!IsValidVehicle(v) || clean.Length == 0) return false;
            int vehicleHandle = ResolveLcpScriptVehicleHandle(v);
            if (vehicleHandle == 0) { if (logPlateDiagnostics) Log("LCP_APPLY_FAILED: could not resolve GTA script vehicle handle for LCP."); return false; }
            if (!useLibertyCityPlatesBridge) return false;
            LcpPlateDefinition def = GetStoredLcpPlateDefinition(rec);
            if (useLcpPlateDefinitions && def != null) return TryApplyLibertyCityPlateViaBridge(vehicleHandle, clean, def);
            return TryApplyLibertyCityPlateViaBridge(vehicleHandle, clean);
        }

        private bool ApplyLibertyCityPlateToVehicle(Vehicle v, string plate)
        {
            string clean = CleanPlateText(plate);
            if (!IsValidVehicle(v) || clean.Length == 0) return false;
            int vehicleHandle = ResolveLcpScriptVehicleHandle(v);
            if (vehicleHandle == 0) return false;
            if (!useLibertyCityPlatesBridge) return false;
            return TryApplyLibertyCityPlateViaBridge(vehicleHandle, clean);
        }

        private bool DetectLibertyCityPlatesBridge(bool logWhenMissing)
        {
            if (!libertyCityPlatesEnabled || !useLibertyCityPlatesBridge) return false;
            if (lcpBridgeChecked)
            {
                if (!lcpBridgeAvailable && logWhenMissing && !lcpBridgeMissingLogged)
                {
                    Log("LCP_BRIDGE_NOT_FOUND: plugins\\000_LVSCE_LCP_Bridge.asi was not detected or could not be loaded. Custom LCP plate ordering will be unavailable; stored vehicle data will still load normally.");
                    lcpBridgeMissingLogged = true;
                }
                return lcpBridgeAvailable;
            }

            lcpBridgeChecked = true;
            lcpBridgeAvailable = false;
            try
            {
                if (!File.Exists("plugins\\000_LVSCE_LCP_Bridge.asi"))
                {
                    if (logWhenMissing && !lcpBridgeMissingLogged)
                    {
                        Log("LCP_BRIDGE_NOT_FOUND: plugins\\000_LVSCE_LCP_Bridge.asi is missing.");
                        lcpBridgeMissingLogged = true;
                    }
                    return false;
                }

                int install = LVSCE_LCP_Install();
                int status = LVSCE_LCP_GetStatus();
                int version = LVSCE_LCP_GetBridgeVersion();
                lcpBridgeLastStatus = status;
                lcpBridgeLastVersion = version;
                lcpBridgeAvailable = install > 0;
                if (logPlateDiagnostics && !lcpBridgeStatusLogged)
                {
                    Log("LCP_BRIDGE_STATUS: install=" + install.ToString() + " status=0x" + status.ToString("X") + " version=" + version.ToString() + ". Full provider command mask is 0x1FF after LCP registers its natives.");
                    lcpBridgeStatusLogged = true;
                }
                return lcpBridgeAvailable;
            }
            catch (DllNotFoundException)
            {
                if (logWhenMissing && !lcpBridgeMissingLogged) { Log("LCP_BRIDGE_NOT_FOUND: bridge DLL could not be loaded."); lcpBridgeMissingLogged = true; }
            }
            catch (EntryPointNotFoundException)
            {
                if (!lcpBridgeMissingLogged) { Log("LCP_BRIDGE_INVALID: bridge exports are missing. Reinstall 000_LVSCE_LCP_Bridge.asi from this patch."); lcpBridgeMissingLogged = true; }
            }
            catch (BadImageFormatException)
            {
                if (!lcpBridgeMissingLogged) { Log("LCP_BRIDGE_INVALID: bridge has wrong bitness or is not a valid Win32 DLL/ASI."); lcpBridgeMissingLogged = true; }
            }
            catch (Exception ex)
            {
                if (!lcpBridgeMissingLogged) { Log("LCP_BRIDGE_ERROR: " + ex.GetType().Name + "."); lcpBridgeMissingLogged = true; }
            }
            return false;
        }

        private bool TryApplyLibertyCityPlateViaBridge(int vehicleHandle, string cleanPlate, LcpPlateDefinition def)
        {
            if (!DetectLibertyCityPlatesBridge(true)) return false;
            if (def == null) return TryApplyLibertyCityPlateViaBridge(vehicleHandle, cleanPlate);
            try
            {
                int result = LVSCE_LCP_SetPlateEx(vehicleHandle, def.PlateTxd, def.LettersTxd, def.ScaleX, def.ScaleY, def.OffsetX, def.OffsetY, def.TintR, def.TintG, def.TintB, def.TintA, cleanPlate);
                int status = 0; try { status = LVSCE_LCP_GetStatus(); lcpBridgeLastStatus = status; } catch { status = lcpBridgeLastStatus; }
                if (result == 1) { if (logPlateDiagnostics) Log("LCP_BRIDGE_APPLY_EX_OK: handle=" + vehicleHandle.ToString() + " plateType=" + def.Id.ToString() + " plate=" + cleanPlate + " status=0x" + status.ToString("X") + "."); return true; }
                if (logPlateDiagnostics) Log("LCP_BRIDGE_APPLY_EX_FAILED: result=" + result.ToString() + " status=0x" + status.ToString("X") + " handle=" + vehicleHandle.ToString() + " plateType=" + def.Id.ToString() + " plate=" + cleanPlate + ". Falling back to text-only apply.");
            }
            catch (EntryPointNotFoundException) { if (logPlateDiagnostics) Log("LCP_BRIDGE_EX_NOT_AVAILABLE: SetPlateEx export missing. Falling back to text-only apply."); }
            catch (Exception ex) { if (logPlateDiagnostics) Log("LCP_BRIDGE_EX_CALL_FAILED: " + ex.GetType().Name + ". Falling back to text-only apply."); }
            return TryApplyLibertyCityPlateViaBridge(vehicleHandle, cleanPlate);
        }

        private bool TryApplyLibertyCityPlateViaBridge(int vehicleHandle, string cleanPlate)
        {
            if (!DetectLibertyCityPlatesBridge(true)) return false;
            try
            {
                int result = LVSCE_LCP_SetPlate(vehicleHandle, cleanPlate);
                int status = 0;
                try { status = LVSCE_LCP_GetStatus(); lcpBridgeLastStatus = status; } catch { status = lcpBridgeLastStatus; }
                if (result == 1)
                {
                    if (logPlateDiagnostics) Log("LCP_BRIDGE_APPLY_OK: handle=" + vehicleHandle.ToString() + " plate=" + cleanPlate + " status=0x" + status.ToString("X") + ".");
                    return true;
                }
                if (logPlateDiagnostics)
                {
                    string reason = "unknown";
                    if (result == -1) reason = "invalid handle or plate text";
                    else if (result == -2) reason = "LCP command pointers not captured; check ASI load order, bridge file name must sort before LibertyCityPlates.asi";
                    else if (result == -3) reason = "LCP native command returned FALSE for this handle";
                    else if (result == -10) reason = "bridge could not resolve kernel32 VirtualProtect";
                    else if (result == -11) reason = "bridge could not find GTA IV register-command call pattern";
                    else if (result == -12) reason = "bridge could not patch register-command call";
                    Log("LCP_BRIDGE_APPLY_FAILED: result=" + result.ToString() + " status=0x" + status.ToString("X") + " handle=" + vehicleHandle.ToString() + " plate=" + cleanPlate + " reason=" + reason + ".");
                }
            }
            catch (Exception ex)
            {
                if (logPlateDiagnostics) Log("LCP_BRIDGE_CALL_FAILED: " + ex.GetType().Name + ".");
            }
            return false;
        }

        private int ResolveLcpScriptVehicleHandle(Vehicle v)
        {
            if (!IsValidVehicle(v)) return 0;
            int h;
            if (TryReadIntMember(v, "Handle", out h) && IsPlausibleScriptHandle(h)) return h;
            if (TryReadIntMember(v, "pHandle", out h) && IsPlausibleScriptHandle(h)) return h;
            if (TryReadIntMember(v, "handle", out h) && IsPlausibleScriptHandle(h)) return h;
            if (TryReadIntMember(v, "m_handle", out h) && IsPlausibleScriptHandle(h)) return h;
            if (TryReadIntMember(v, "NativeValue", out h) && IsPlausibleScriptHandle(h)) return h;
            if (TryReadIntMember(v, "UID", out h) && IsPlausibleScriptHandle(h)) return h;
            return 0;
        }

        private bool IsPlausibleScriptHandle(int h)
        {
            return h > 0 && h < 0x1000000;
        }

        private bool TryReadIntMember(object obj, string name, out int value)
        {
            value = 0;
            if (obj == null) return false;
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            Type t = obj.GetType();
            while (t != null)
            {
                try
                {
                    PropertyInfo p = t.GetProperty(name, flags);
                    if (p != null && p.GetIndexParameters().Length == 0)
                    {
                        object raw = p.GetValue(obj, null);
                        if (TryObjectToInt(raw, out value)) return true;
                    }
                }
                catch { }
                try
                {
                    FieldInfo f = t.GetField(name, flags);
                    if (f != null)
                    {
                        object raw = f.GetValue(obj);
                        if (TryObjectToInt(raw, out value)) return true;
                    }
                }
                catch { }
                t = t.BaseType;
            }
            return false;
        }

        private bool TryObjectToInt(object raw, out int value)
        {
            value = 0;
            if (raw == null) return false;
            try
            {
                if (raw is int) { value = (int)raw; return true; }
                if (raw is uint) { value = unchecked((int)(uint)raw); return true; }
                if (raw is short) { value = (short)raw; return true; }
                if (raw is ushort) { value = (ushort)raw; return true; }
                if (raw is long)
                {
                    long v = (long)raw;
                    if (v > 0 && v < Int32.MaxValue) { value = (int)v; return true; }
                }
                if (raw is ulong)
                {
                    ulong v = (ulong)raw;
                    if (v > 0 && v < Int32.MaxValue) { value = (int)v; return true; }
                }
                value = Convert.ToInt32(raw, CultureInfo.InvariantCulture);
                return true;
            }
            catch { return false; }
        }

        private void LoadOwnedMeta(string[] lines)
        {
            ownedLastStoryProgressByEpisode.Clear();
            if (lines == null) return;
            string section = "";
            for (int i = 0; i < lines.Length; i++)
            {
                string raw = lines[i];
                string line = raw == null ? "" : raw.Trim();
                if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#")) continue;
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    section = line.Substring(1, line.Length - 2).Trim().ToLowerInvariant();
                    continue;
                }
                if (!section.StartsWith("meta.")) continue;
                int eq = line.IndexOf('=');
                if (eq <= 0) continue;
                string key = line.Substring(0, eq).Trim().ToLowerInvariant();
                string value = line.Substring(eq + 1).Trim();
                if (key != "laststoryprogress") continue;
                string episode = NormaliseEpisodeKey(section.Substring(5));
                if (episode.Length == 0) continue;
                ownedLastStoryProgressByEpisode[episode] = ClampFloat(ReadFloat(value, 0.0f), 0.0f, 100.0f);
            }
        }

        private void SeedOwnedStoryProgressMetaAfterLoad()
        {
            float progress;
            if (!TryReadStoryProgressPercent(out progress)) return;
            if (progress <= 0.05f) return;
            string episode = GetCurrentEpisodeKey();
            float previous;
            if (!ownedLastStoryProgressByEpisode.TryGetValue(episode, out previous) || progress > previous + 0.05f)
            {
                ownedLastStoryProgressByEpisode[episode] = progress;
                ownedVehiclesDirty = true;
            }
        }

        private void ResetOwnedVehiclesForNewGameIfNeeded()
        {
            if (!ownershipEnabled || !ownedVehicleStorageHealthy) return;
            string episode = GetCurrentEpisodeKey();
            float currentProgress;
            if (!TryReadStoryProgressPercent(out currentProgress)) return;
            float lastProgress;
            if (!ownedLastStoryProgressByEpisode.TryGetValue(episode, out lastProgress))
            {
                ownedLastStoryProgressByEpisode[episode] = currentProgress;
                return;
            }
            if (currentProgress > 0.05f || lastProgress < 1.0f) return;

            int removed = 0;
            for (int i = ownedVehicles.Count - 1; i >= 0; i--)
            {
                OwnedVehicleRecord rec = ownedVehicles[i];
                if (!OwnedRecordBelongsToEpisode(rec, episode)) continue;
                if (rec != null)
                {
                    if (rec.BlipRef != null) RemoveBlipSafe(rec.BlipRef, "owned vehicle blip " + rec.Id);
                    if (IsValidVehicle(rec.VehicleRef)) DeleteVehicleSafe(rec.VehicleRef, "owned vehicle reset " + rec.Id);
                }
                ownedVehicles.RemoveAt(i);
                removed++;
            }
            ownedLastStoryProgressByEpisode[episode] = currentProgress;
            if (removed > 0)
            {
                ownedVehiclesDirty = true;
                Log("Owned vehicles reset for new game. Episode=" + episode + ", Removed=" + removed.ToString() + ", PreviousProgress=" + lastProgress.ToString("0.00", CultureInfo.InvariantCulture) + "%.");
                SaveOwnedVehicles();
            }
        }

        private void RecordCurrentStoryProgressForOwnedMeta()
        {
            float progress;
            if (!TryReadStoryProgressPercent(out progress)) return;
            ownedLastStoryProgressByEpisode[GetCurrentEpisodeKey()] = progress;
        }

        private void LoadOwnedVehicles()
        {
            bool exists = false;
            try { exists = File.Exists(ownedPath); } catch { exists = false; }
            if (!exists)
            {
                ownedVehicles.Clear();
                ownedVehicleStorageHealthy = true;
                ownedVehicleSaveSuppressedLogged = false;
                return;
            }

            try
            {
                List<OwnedVehicleRecord> loaded = new List<OwnedVehicleRecord>();
                bool fileHadOwnedBlocks = false;
                OwnedVehicleRecord current = null;
                string[] lines = File.ReadAllLines(ownedPath);
                LoadOwnedMeta(lines);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#")) continue;
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        string sec = line.Substring(1, line.Length - 2).Trim();
                        if (sec.ToLowerInvariant().StartsWith("owned."))
                        {
                            fileHadOwnedBlocks = true;
                            current = new OwnedVehicleRecord();
                            current.Id = sec.Substring(6);
                            current.Registered = true;
                            current.Health = 1000;
                            current.EngineHealth = 1000;
                            current.PetrolTankHealth = 1000;
                            current.DirtLevel = -1.0f;
                            current.FuelKnown = false;
                            current.FuelGallons = -1.0f;
                            current.FuelCapacityGallons = 0.0f;
                            loaded.Add(current);
                            int n;
                            string digits = current.Id.Replace("owned_", "");
                            if (Int32.TryParse(digits, out n) && n >= nextOwnedId) nextOwnedId = n + 1;
                        }
                        else current = null;
                        continue;
                    }
                    if (current == null) continue;
                    int eq = line.IndexOf('=');
                    if (eq <= 0) continue;
                    string key = line.Substring(0, eq).Trim().ToLowerInvariant();
                    string value = line.Substring(eq + 1).Trim();
                    ApplyOwnedValue(current, key, value);
                }

                for (int i = loaded.Count - 1; i >= 0; i--)
                {
                    OwnedVehicleRecord rec = loaded[i];
                    if (rec == null || rec.Id == null || rec.Id.Trim().Length == 0 || rec.ModelName == null || rec.ModelName.Trim().Length == 0)
                    {
                        loaded.RemoveAt(i);
                    }
                }

                if (fileHadOwnedBlocks && loaded.Count == 0)
                {
                    ownedVehicleStorageHealthy = false;
                    Log("Owned vehicle load found owned blocks but produced zero valid records. Existing owned vehicle file will not be overwritten this session.");
                    return;
                }

                ownedVehicles = loaded;
                ownedVehicleStorageHealthy = true;
                ownedVehicleSaveSuppressedLogged = false;
                int currentCount = FinaliseOwnedVehicleEpisodesAfterLoad();
                Log("Owned vehicles loaded: " + ownedVehicles.Count.ToString() + ", active for episode " + GetCurrentEpisodeKey() + ": " + currentCount.ToString() + ".");
            }
            catch (Exception ex)
            {
                ownedVehicleStorageHealthy = false;
                Log("Owned vehicle load failed: " + ex.GetType().Name + ". Existing owned vehicle file will not be overwritten this session.");
            }
        }

        private void ApplyOwnedValue(OwnedVehicleRecord rec, string key, string value)
        {
            if (key == "model") rec.ModelName = value.ToLowerInvariant();
            else if (key == "modelhash") rec.ModelHash = ReadInt(value, rec.ModelHash);
            else if (key == "source") rec.Source = value;
            else if (key == "episode") rec.Episode = NormaliseEpisodeKey(value);
            else if (key == "registered") rec.Registered = ReadBool(value, rec.Registered);
            else if (key == "insured") rec.Insured = ReadBool(value, rec.Insured);
            else if (key == "destroyed") rec.Destroyed = ReadBool(value, rec.Destroyed);
            else if (key == "colour1") rec.Colour1 = ReadInt(value, rec.Colour1);
            else if (key == "colour2") rec.Colour2 = ReadInt(value, rec.Colour2);
            else if (key == "colour3") rec.Colour3 = ReadInt(value, rec.Colour3);
            else if (key == "colour4") rec.Colour4 = ReadInt(value, rec.Colour4);
            else if (key == "extras") rec.Extras = value;
            else if (key == "health") rec.Health = ReadInt(value, rec.Health);
            else if (key == "enginehealth") rec.EngineHealth = ReadInt(value, rec.EngineHealth);
            else if (key == "petroltankhealth") rec.PetrolTankHealth = ReadInt(value, rec.PetrolTankHealth);
            else if (key == "fuelgallons" || key == "fuel") { rec.FuelGallons = Math.Max(0.0f, ReadFloat(value, rec.FuelGallons)); rec.FuelKnown = true; }
            else if (key == "fuelcapacitygallons" || key == "fuelcapacity") rec.FuelCapacityGallons = Math.Max(0.0f, ReadFloat(value, rec.FuelCapacityGallons));
            else if (key == "dirtlevel" || key == "dirtylevel" || key == "dirt") rec.DirtLevel = ReadFloat(value, rec.DirtLevel);
            else if (key == "x") rec.X = ReadFloat(value, rec.X);
            else if (key == "y") rec.Y = ReadFloat(value, rec.Y);
            else if (key == "z") rec.Z = ReadFloat(value, rec.Z);
            else if (key == "heading") rec.Heading = ReadFloat(value, rec.Heading);
            else if (key == "purchaseprice") rec.PurchasePrice = ReadInt(value, rec.PurchasePrice);
            else if (key == "insuranceprice") rec.InsurancePrice = ReadInt(value, rec.InsurancePrice);
            else if (key == "platetext") rec.PlateText = CleanPlateText(value);
            else if (key == "lcpplateid") rec.LcpPlateId = ReadInt(value, rec.LcpPlateId);
            else if (key == "lcpplateformat") rec.LcpPlateFormat = value;
            else if (key == "lcpplatename") rec.LcpPlateName = value;
            else if (key == "lcprawinput") rec.LcpRawInput = CleanPlateRawInput(value);
        }


        private void SaveOwnedVehicles()
        {
            if (!ownershipEnabled) return;
            if (!ownedVehicleStorageHealthy)
            {
                if (!ownedVehicleSaveSuppressedLogged)
                {
                    Log("Owned vehicle save suppressed because the owned vehicle file did not load cleanly. Fix or restore LibertyVehicleServicesCE.owned.ini, then reload the script.");
                    ownedVehicleSaveSuppressedLogged = true;
                }
                return;
            }

            try
            {
                RecordCurrentStoryProgressForOwnedMeta();
                List<string> lines = new List<string>();
                lines.Add("; LibertyVehicleServicesCE owned vehicles. Generated file.");
                lines.Add("; Remove a block only if you intentionally want to forget that vehicle.");
                lines.Add("");
                List<string> metaEpisodes = new List<string>(ownedLastStoryProgressByEpisode.Keys);
                metaEpisodes.Sort();
                for (int i = 0; i < metaEpisodes.Count; i++)
                {
                    string episode = NormaliseEpisodeKey(metaEpisodes[i]);
                    if (episode.Length == 0) continue;
                    float progress = ownedLastStoryProgressByEpisode[metaEpisodes[i]];
                    lines.Add("[Meta." + episode + "]");
                    lines.Add("LastStoryProgress=" + progress.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                    lines.Add("");
                }
                for (int i = 0; i < ownedVehicles.Count; i++)
                {
                    OwnedVehicleRecord r = ownedVehicles[i];
                    if (r == null) continue;
                    lines.Add("[Owned." + r.Id + "]");
                    lines.Add("Model=" + SafeString(r.ModelName));
                    lines.Add("ModelHash=" + r.ModelHash.ToString());
                    lines.Add("Source=" + SafeString(r.Source));
                    string episodeToSave = NormaliseEpisodeKey(r.Episode);
                    if (episodeToSave.Length == 0 && (currentEpisodeKeyReliable || (currentEpisodeKey != null && currentEpisodeKey.Length > 0))) episodeToSave = GetCurrentEpisodeKey();
                    if (episodeToSave.Length > 0) lines.Add("Episode=" + SafeString(episodeToSave));
                    lines.Add("Registered=" + r.Registered.ToString());
                    lines.Add("Insured=" + r.Insured.ToString());
                    lines.Add("Destroyed=" + r.Destroyed.ToString());
                    lines.Add("Colour1=" + r.Colour1.ToString());
                    lines.Add("Colour2=" + r.Colour2.ToString());
                    lines.Add("Colour3=" + r.Colour3.ToString());
                    lines.Add("Colour4=" + r.Colour4.ToString());
                    lines.Add("Extras=" + SafeString(r.Extras));
                    lines.Add("Health=" + r.Health.ToString());
                    lines.Add("EngineHealth=" + r.EngineHealth.ToString());
                    lines.Add("PetrolTankHealth=" + r.PetrolTankHealth.ToString());

                    if (r.FuelKnown && r.FuelGallons >= 0.0f) lines.Add("FuelGallons=" + r.FuelGallons.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                    if (saveVehicleDirtLevel && r.DirtLevel >= 0.0f) lines.Add("DirtLevel=" + r.DirtLevel.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
                    lines.Add("X=" + r.X.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
                    lines.Add("Y=" + r.Y.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
                    lines.Add("Z=" + r.Z.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
                    lines.Add("Heading=" + r.Heading.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
                    lines.Add("PurchasePrice=" + r.PurchasePrice.ToString());
                    lines.Add("InsurancePrice=" + r.InsurancePrice.ToString());
                    if (r.PlateText != null && r.PlateText.Length > 0)
                    {
                        lines.Add("PlateText=" + SafeString(CleanPlateText(r.PlateText)));
                        if (r.LcpPlateId >= 0) lines.Add("LcpPlateId=" + r.LcpPlateId.ToString());
                        if (r.LcpPlateFormat != null && r.LcpPlateFormat.Length > 0) lines.Add("LcpPlateFormat=" + SafeString(r.LcpPlateFormat));
                        if (r.LcpPlateName != null && r.LcpPlateName.Length > 0) lines.Add("LcpPlateName=" + SafeString(r.LcpPlateName));
                        if (r.LcpRawInput != null && r.LcpRawInput.Length > 0) lines.Add("LcpRawInput=" + SafeString(CleanPlateRawInput(r.LcpRawInput)));
                    }
                    lines.Add("");
                }
                File.WriteAllLines(ownedPath, lines.ToArray());
                ownedVehiclesDirty = false;
                ownedVehicleSaveSuppressedLogged = false;
            }
            catch (Exception ex)
            {
                Log("Owned vehicle save failed: " + ex.GetType().Name + ".");
            }
        }

        private string SafeString(string s)
        {
            return s == null ? "" : s;
        }

        private void EnsureDefaultFuelStations()
        {
            fuelStations.Clear();

            AddFuelStation("terroil_boabo", "TerrOil", "road", 108.845f, 1136.190f, 14.558f, 7.0f);
            AddFuelStation("ron_westdyke", "RON", "road", -1312.630f, 1735.930f, 27.926f, 7.0f);
            AddFuelStation("globe_oil_alderney", "Globe Oil", "road", -1390.830f, 29.128f, 6.965f, 7.0f);
            AddFuelStation("ron_algonquin_north", "RON", "road", -434.543f, -20.781f, 9.864f, 7.0f);
            AddFuelStation("globe_oil_algonquin", "Globe Oil", "road", -480.111f, -210.643f, 7.749f, 7.0f);
            AddFuelStation("ron_dukes_north", "RON", "road", 1453.580f, 1711.320f, 16.693f, 7.0f);
            AddFuelStation("ron_dukes_east", "RON", "road", 1771.120f, 837.323f, 16.438f, 7.0f);
            AddFuelStation("ron_bohan", "RON", "road", 1126.680f, 330.693f, 29.773f, 7.0f);
            AddFuelStation("ron_broker", "RON", "road", 1129.090f, -358.859f, 19.025f, 7.0f);
            AddFuelStation("ron_chase_point", "RON", "road", 774.382f, 195.533f, 6.182f, 7.0f);
            AddFuelStation("ron_truck_station", "RON Truck Station", "road", 1330.720f, 896.690f, 13.190f, 5.0f);


            AddFuelStation("marine_steinway", "Steinway marine fuel", "boat", 1068.500f, 1073.000f, 0.000f, 20.0f);
            AddFuelStation("marine_east_hook", "East Hook marine fuel", "boat", 697.700f, -77.900f, 0.000f, 10.0f);
            AddFuelStation("marine_algonquin", "Algonquin marine fuel", "boat", 589.300f, 238.400f, 0.000f, 10.0f);
            AddFuelStation("marine_charge_island", "Charge Island marine fuel", "boat", 416.300f, 1092.700f, 0.000f, 20.0f);
            AddFuelStation("marine_castle_garden", "Castle Garden marine fuel", "boat", -394.900f, -616.600f, 0.000f, 10.0f);
            AddFuelStation("marine_alderney", "Alderney marine fuel", "boat", -934.800f, 685.000f, 0.000f, 10.0f);

            AddFuelStation("heli_lcpd", "LCPD helipad fuel", "heli", 66.440f, 1255.800f, 29.890f, 10.0f);
            AddFuelStation("heli_algonquin_south", "Helipad fuel", "heli", -174.450f, -736.920f, 185.340f, 10.0f);
            AddFuelStation("heli_algonquin_easton", "Helipad fuel", "heli", -270.170f, 215.220f, 222.350f, 10.0f);
            AddFuelStation("heli_algonquin_midtown", "Helipad fuel", "heli", -261.560f, -356.190f, 161.760f, 10.0f);
            AddFuelStation("heli_algonquin_star", "Helipad fuel", "heli", -92.320f, 170.860f, 65.010f, 10.0f);
            AddFuelStation("heli_alderney_low", "Helipad fuel", "heli", -707.970f, 358.340f, 3.840f, 10.0f);
            AddFuelStation("heli_airport", "Airport helipad fuel", "heli", 2164.380f, 843.520f, 18.070f, 10.0f);
            AddFuelStation("heli_alderney_west", "Helipad fuel", "heli", -825.930f, 779.830f, 5.880f, 10.0f);
        }

        private void AddFuelStation(string id, string name, string kind, float x, float y, float z, float radius)
        {
            FuelStation station = new FuelStation();
            station.Id = id;
            station.Name = name;
            station.Kind = kind == null ? "road" : kind.Trim().ToLowerInvariant();
            station.X = x;
            station.Y = y;
            station.Z = z;
            station.Radius = Math.Max(2.0f, radius);
            station.DamagedSeconds = 0.0f;
            fuelStations.Add(station);
        }

        private void EnsureDefaultMetroStations()
        {
            metroStations.Clear();
            metroEntrances.Clear();

            AddMetroStation("frankfort_high", "Frankfort High", "Algonquin", -360.916f, 1442.487f, 26.953f, 45.0f);
            AddMetroEntrance("frankfort_high_k", "frankfort_high", "K", "Frankfort High K entrance", -383.466f, 1433.567f, 12.288f);
            AddMetroEntrance("frankfort_high_c", "frankfort_high", "C", "Frankfort High C entrance", -343.295f, 1433.080f, 12.283f);

            AddMetroStation("frankfort_low", "Frankfort Low", "Algonquin", -385.618f, 1392.950f, -4.340f, 45.0f);
            AddMetroEntrance("frankfort_low_k", "frankfort_low", "K", "Frankfort Low K entrance", -331.784f, 1426.434f, 12.635f);
            AddMetroEntrance("frankfort_low_c", "frankfort_low", "C", "Frankfort Low C entrance", -360.143f, 1432.445f, 12.231f);

            AddMetroStation("vespucci_circus", "Vespucci Circus", "Algonquin", -112.708f, 1406.434f, 1.646f, 45.0f);
            AddMetroEntrance("vespucci_circus_a", "vespucci_circus", "A", "Vespucci Circus A entrance", -86.249f, 1427.478f, 20.421f);
            AddMetroEntrance("vespucci_circus_j", "vespucci_circus", "J", "Vespucci Circus J entrance", -67.189f, 1411.637f, 20.421f);

            AddMetroStation("north_park", "North Park", "Algonquin", -145.247f, 1136.780f, -0.485f, 45.0f);
            AddMetroEntrance("north_park_kc", "north_park", "K", "North Park K/C entrance", -135.032f, 1153.501f, 14.774f);

            AddMetroStation("east_park", "East Park", "Algonquin", -71.640f, 664.393f, -1.790f, 45.0f);
            AddMetroEntrance("east_park_k", "east_park", "K", "East Park K entrance", -35.317f, 634.719f, 14.663f);
            AddMetroEntrance("east_park_c", "east_park", "C", "East Park C entrance", -79.773f, 645.708f, 14.662f);

            AddMetroStation("easton", "Easton", "Algonquin", -32.974f, 9.765f, -1.679f, 45.0f);
            AddMetroEntrance("easton_kc", "easton", "K", "Easton K/C main entrance", -112.003f, -22.740f, 14.815f);
            AddMetroEntrance("easton_j", "easton", "J", "Easton J entrance", -35.367f, -18.615f, 14.769f);
            AddMetroEntrance("easton_a", "easton", "A", "Easton A entrance", -68.389f, -36.916f, 14.806f);

            AddMetroStation("manganese_east", "Manganese East", "Algonquin", 113.013f, 569.179f, -3.222f, 45.0f);
            AddMetroEntrance("manganese_east_j", "manganese_east", "J", "Manganese East J entrance", 131.292f, 522.582f, 14.668f);
            AddMetroEntrance("manganese_east_a", "manganese_east", "A", "Manganese East A entrance", 94.743f, 523.032f, 13.244f);

            AddMetroStation("quartz_east", "Quartz East", "Algonquin", 114.389f, 947.252f, -1.805f, 45.0f);
            AddMetroEntrance("quartz_east_j", "quartz_east", "J", "Quartz East J entrance", 134.658f, 911.303f, 14.717f);
            AddMetroEntrance("quartz_east_a", "quartz_east", "A", "Quartz East A entrance", 115.570f, 911.329f, 14.717f);

            AddMetroStation("vauxite", "Vauxite", "Algonquin", -506.110f, 1290.417f, -12.045f, 45.0f);
            AddMetroEntrance("vauxite_a", "vauxite", "A", "Vauxite A entrance", -484.208f, 1332.375f, 17.486f);
            AddMetroEntrance("vauxite_j", "vauxite", "J", "Vauxite J entrance", -484.029f, 1351.544f, 17.478f);

            AddMetroStation("quartz_west", "Quartz West", "Algonquin", -527.464f, 912.001f, -2.730f, 45.0f);
            AddMetroEntrance("quartz_west_a", "quartz_west", "A", "Quartz West A entrance", -544.559f, 926.051f, 9.939f);
            AddMetroEntrance("quartz_west_j", "quartz_west", "J", "Quartz West J entrance", -544.558f, 940.996f, 9.946f);

            AddMetroStation("manganese_west", "Manganese West", "Algonquin", -475.676f, 503.184f, -7.668f, 45.0f);
            AddMetroEntrance("manganese_west_a", "manganese_west", "A", "Manganese West A entrance", -460.973f, 530.123f, 9.858f);
            AddMetroEntrance("manganese_west_j", "manganese_west", "J", "Manganese West J entrance", -508.250f, 530.082f, 6.677f);

            AddMetroStation("west_park", "West Park", "Algonquin", -362.213f, 665.001f, -1.780f, 45.0f);
            AddMetroEntrance("west_park_k", "west_park", "K", "West Park K entrance", -376.992f, 677.020f, 14.680f);
            AddMetroEntrance("west_park_c", "west_park", "C", "West Park C entrance", -394.959f, 707.272f, 14.673f);

            AddMetroStation("hematite", "Hematite", "Algonquin", -522.009f, -31.930f, -7.674f, 45.0f);
            AddMetroEntrance("hematite_j_up", "hematite", "J", "Hematite J entrance", -527.384f, 19.717f, 7.855f);
            AddMetroEntrance("hematite_j_down", "hematite", "J", "Hematite J entrance", -550.510f, 10.119f, 4.667f);
            AddMetroEntrance("hematite_a", "hematite", "A", "Hematite A entrance", -493.760f, 19.768f, 7.855f);

            AddMetroStation("frankfort_avenue", "Frankfort Avenue", "Algonquin", -376.054f, 324.226f, -1.650f, 45.0f);
            AddMetroEntrance("frankfort_avenue_c", "frankfort_avenue", "C", "Frankfort Avenue C entrance", -377.496f, 371.078f, 14.763f);
            AddMetroEntrance("frankfort_avenue_k", "frankfort_avenue", "K", "Frankfort Avenue K entrance", -350.009f, 371.525f, 14.761f);

            AddMetroStation("feldspar", "Feldspar", "Algonquin", -331.294f, -392.136f, -12.561f, 45.0f);
            AddMetroEntrance("feldspar_a", "feldspar", "A", "Feldspar A entrance", -324.532f, -372.956f, 4.890f);
            AddMetroEntrance("feldspar_j_east", "feldspar", "J", "Feldspar J entrance", -349.655f, -335.153f, 4.899f);
            AddMetroEntrance("feldspar_j_west", "feldspar", "J", "Feldspar J entrance", -380.562f, -368.363f, 4.915f);

            AddMetroStation("castle_gardens", "Castle Gardens", "Algonquin", 113.180f, -711.521f, -12.617f, 45.0f);
            AddMetroEntrance("castle_gardens_j", "castle_gardens", "J", "Castle Gardens J entrance", 83.859f, -757.168f, 4.965f);
            AddMetroEntrance("castle_gardens_a", "castle_gardens", "A", "Castle Gardens A entrance", 69.208f, -727.031f, 4.963f);

            AddMetroStation("city_hall", "City Hall", "Algonquin", -72.428f, -485.613f, -1.701f, 45.0f);
            AddMetroEntrance("city_hall_c", "city_hall", "C", "City Hall C entrance", -114.121f, -501.239f, 14.752f);
            AddMetroEntrance("city_hall_k", "city_hall", "K", "City Hall K entrance", -119.666f, -479.584f, 14.763f);

            AddMetroStation("suffolk", "Suffolk", "Algonquin", -278.236f, -171.912f, -1.882f, 45.0f);
            AddMetroEntrance("suffolk_k", "suffolk", "K", "Suffolk K entrance", -303.698f, -144.225f, 14.590f);
            AddMetroEntrance("suffolk_c", "suffolk", "C", "Suffolk C entrance", -252.748f, -171.909f, 14.447f);

            AddMetroStation("emerald", "Emerald", "Algonquin", 93.762f, -267.013f, -3.291f, 45.0f);
            AddMetroEntrance("emerald_a", "emerald", "A", "Emerald A entrance", 115.726f, -318.032f, 14.767f);
            AddMetroEntrance("emerald_j", "emerald", "J", "Emerald J entrance", 115.799f, -302.754f, 14.767f);

            AddMetroStation("san_quentin_avenue", "San Quentin Avenue", "Bohan", 383.067f, 1637.650f, 26.913f, 45.0f);
            AddMetroEntrance("san_quentin_avenue_e", "san_quentin_avenue", "E", "San Quentin Avenue E entrance", 374.972f, 1627.510f, 16.280f);
            AddMetroEntrance("san_quentin_avenue_b", "san_quentin_avenue", "B", "San Quentin Avenue B entrance", 408.429f, 1660.083f, 16.231f);

            AddMetroStation("windmill_street", "Windmill Street", "Bohan", 725.908f, 1495.924f, 27.675f, 45.0f);
            AddMetroEntrance("windmill_street_e", "windmill_street", "E", "Windmill Street E entrance", 731.458f, 1523.582f, 17.349f);
            AddMetroEntrance("windmill_street_b", "windmill_street", "B", "Windmill Street B entrance", 749.645f, 1448.728f, 14.254f);

            AddMetroStation("huntington_street", "Huntington Street", "Dukes", 1425.794f, 387.954f, 40.524f, 45.0f);
            AddMetroEntrance("huntington_street_8", "huntington_street", "8", "Huntington Street 8 entrance", 1386.445f, 368.946f, 23.063f);
            AddMetroEntrance("huntington_street_3", "huntington_street", "3", "Huntington Street 3 entrance", 1386.626f, 404.925f, 22.952f);

            AddMetroStation("francis_international_airport", "Francis International Airport", "Dukes", 2295.260f, 447.766f, 21.830f, 45.0f);
            AddMetroEntrance("francis_international_airport_3", "francis_international_airport", "3", "Francis International Airport 3/8 entrance", 2297.429f, 474.722f, 6.086f);

            AddMetroStation("lynch_street", "Lynch Street", "Broker", 1646.040f, 389.432f, 37.179f, 45.0f);
            AddMetroEntrance("lynch_street_3", "lynch_street", "3", "Lynch Street 3 entrance", 1595.704f, 367.389f, 25.258f);
            AddMetroEntrance("lynch_street_8", "lynch_street", "8", "Lynch Street 8 entrance", 1636.262f, 417.810f, 29.123f);

            AddMetroStation("schottler", "Schottler", "Broker", 1313.216f, -57.765f, 39.063f, 45.0f);
            AddMetroEntrance("schottler_3", "schottler", "3", "Schottler 3 entrance", 1322.707f, -40.451f, 28.213f);

            AddMetroStation("hove_beach", "Hove Beach", "Broker", 1026.349f, -565.300f, 31.809f, 45.0f);
            AddMetroEntrance("hove_beach_3", "hove_beach", "3", "Hove Beach 3/8 entrance", 1005.666f, -543.951f, 14.901f);
        }

        private void AddMetroStation(string id, string name, string line, float x, float y, float z, float radius)
        {
            MetroStation station = new MetroStation();
            station.Id = id;
            station.Name = name;
            station.Line = line == null ? "" : line;
            station.X = x;
            station.Y = y;
            station.Z = z;
            station.Radius = Math.Max(5.0f, radius);
            metroStations.Add(station);
        }

        private void AddMetroEntrance(string id, string stationId, string lineCode, string label, float x, float y, float z)
        {
            MetroEntrance entrance = new MetroEntrance();
            entrance.Id = id;
            entrance.StationId = stationId;
            entrance.LineCode = lineCode == null ? "" : lineCode.Trim().ToUpperInvariant();
            entrance.Label = label == null ? "Subway entrance" : label;
            entrance.X = x;
            entrance.Y = y;
            entrance.Z = z;
            entrance.Radius = 15.0f;
            metroEntrances.Add(entrance);
        }

        private void UpdateFuelSystem()
        {
            DrawFuelPromptIfActive();
            DrawFuelTestDisplayIfActive();
            int now = Environment.TickCount;
            if (fuelLastUpdateMs == 0)
            {
                fuelLastUpdateMs = now;
                return;
            }
            int elapsedMs = now - fuelLastUpdateMs;


            EnforceOutOfFuelVehicleBetweenFuelTicks();
            EnforceRefuellingVehicleBetweenFuelTicks();
            if (elapsedMs < FuelUpdateIntervalMs) return;
            if (elapsedMs > 5000) elapsedMs = FuelUpdateIntervalMs;
            fuelLastUpdateMs = now;
            float dt = Math.Max(0.05f, elapsedMs / 1000.0f);

            UpdateFuelStationCooldowns(dt);

            if (IsFuelGameplaySuspended())
            {
                if (fuelRefuelling) StopFuelRefuelling("");
                return;
            }

            Vehicle v;
            if (!TryGetPlayerDrivenFuelVehicle(out v))
            {
                if (fuelRefuelling) StopFuelRefuelling("Refuelling cancelled.");
                if (fuelCurrentState != null) fuelCurrentState.MissionConsumptionReduced = false;
                fuelCurrentVehicleHandle = 0;
                fuelCurrentState = null;
                return;
            }

            int previousFuelVehicleHandle = fuelCurrentVehicleHandle;
            int currentHandleBeforeState = TryGetVehicleHandle(v);
            bool fuelStateAlreadyKnown = currentHandleBeforeState != 0 && fuelStates.ContainsKey(currentHandleBeforeState);
            bool ownedFuelAlreadyKnown = false;
            OwnedVehicleRecord existingFuelRecord = FindOwnedRecordForVehicle(v);
            if (existingFuelRecord != null && existingFuelRecord.FuelKnown && existingFuelRecord.FuelGallons >= 0.0f) ownedFuelAlreadyKnown = true;
            bool fuelAlreadyKnownBeforeEntry = fuelStateAlreadyKnown || ownedFuelAlreadyKnown;

            FuelVehicleState state = GetOrCreateFuelState(v, false);
            if (state == null) return;
            fuelCurrentVehicleHandle = state.Handle;
            fuelCurrentState = state;
            if (previousFuelVehicleHandle != state.Handle)
            {
                state.MissionConsumptionReduced = false;
                state.MissionProbeUntilMs = Environment.TickCount + FuelMissionEntryProbeWindowMs;
                state.MissionProbeNextMs = 0;
                if (fuelAlreadyKnownBeforeEntry)
                {
                    state.MissionBoostApplied = true;
                    state.MissionProbeUntilMs = 0;
                    UpdateMissionFuelConsumptionState(state);
                }
                else
                {
                    ApplyMissionFuelProtection(v, state, true);
                }
            }
            else
            {
                TryApplyPendingMissionFuelProtection(v, state);
            }
            UpdateMissionFuelConsumptionState(state);
            RestoreFuelSputterEngineIfDue(v, state);

            if (fuelRefuelling) ProcessFuelRefuelling(v, state, dt);
            else ConsumeFuel(v, state, dt);

            ApplyFuelVehicleBehaviour(v, state, dt);
            UpdateOwnedFuelRecordFromState(state);
            UpdateFuelPrompt(v, state);
        }

        private void DrawFuelPromptIfActive()
        {
            if (fuelPromptText == null || fuelPromptText.Length == 0) return;
            if (Environment.TickCount > fuelPromptUntilMs) return;
            if (IsFuelHudCutsceneOverlayActive()) return;
            DrawSmallPrompt(fuelPromptText);
        }

        private void ShowFuelPrompt(string text)
        {
            fuelPromptText = text == null ? "" : text;
            fuelPromptUntilMs = Environment.TickCount + 800;
        }

        private void DrawFuelTestDisplayIfActive()
        {
            if (!fuelTestDisplayEnabled) return;
            if (!ShouldDrawFuelHud()) return;

            FuelVehicleState state = fuelCurrentState;
            if (state == null || state.CapacityGallons <= 0.0f) return;

            try
            {
                string text = FormatFuel(state);
                float h = 0.024f;
                float y = 0.957f;
                float minimapCenterX = 0.067f;
                float charWidth = 0.0054f;
                float leftPad = 0.007f;
                float rightPad = 0.007f;
                float w = Math.Max(0.070f, leftPad + rightPad + (text.Length * charWidth));
                float x = minimapCenterX - (w * 0.5f);
                DrawScreenRect(x + w * 0.5f, y + h * 0.5f, w, h, 0, 0, 0, 145);
                DrawScreenTextStyled(x + leftPad, y + 0.0017f, text, 0.168f, 0.300f, 245, 245, 245, 245, false);

                string status = GetFuelHudStatusText(state);
                if (status.Length > 0)
                {
                    float gap = 0.004f;
                    float statusPad = 0.007f;
                    float statusW = Math.Max(0.050f, (statusPad * 2.0f) + (status.Length * charWidth));
                    float sx = x + w + gap;
                    DrawScreenRect(sx + statusW * 0.5f, y + h * 0.5f, statusW, h, 0, 0, 0, 130);
                    DrawScreenTextStyled(sx + statusPad, y + 0.0017f, status, 0.168f, 0.300f, 245, 220, 180, 245, false);
                }
            }
            catch { }
        }

        private string GetFuelHudStatusText(FuelVehicleState state)
        {
            if (state != null && state.CapacityGallons > 0.0f)
            {
                if (state.FuelGallons <= FuelEpsilonGallons) return "Out of fuel";
                float percent = GetFuelPercent(state);
                if (percent <= fuelLowPromptPercent) return "Low fuel " + ((int)Math.Round(percent)).ToString() + "%";
            }
            if (fuelStatusText != null && fuelStatusText.Length > 0 && Environment.TickCount <= fuelStatusUntilMs) return fuelStatusText;
            return "";
        }

        private bool ShouldDrawFuelHud()
        {
            if (IsFuelGameplaySuspended()) return false;

            try
            {
                if (IsPauseMenuActiveSafe()) return false;
            }
            catch { }

            try
            {
                if (Function.Call<bool>("IS_SCREEN_FADING")) return false;
            }
            catch { }

            try
            {
                if (!Function.Call<bool>("IS_SCREEN_FADED_IN")) return false;
            }
            catch { }

            if (IsFuelHudCutsceneOverlayActive()) return false;

            if (fuelCurrentState == null) return false;
            if (fuelCurrentVehicleHandle == 0) return false;

            Vehicle currentVehicle;
            if (!TryGetPlayerDrivenFuelVehicle(out currentVehicle)) return false;

            int currentHandle = TryGetVehicleHandle(currentVehicle);
            if (currentHandle == 0) return false;
            if (currentHandle != fuelCurrentVehicleHandle) return false;
            if (fuelCurrentState.Handle != 0 && fuelCurrentState.Handle != currentHandle) return false;

            return true;
        }

        private bool IsFuelHudCutsceneOverlayActive()
        {
            if (!fuelHideHudDuringCutscenes) return false;

            bool managedKnown;
            bool nativeKnown;
            bool managedCanControl = ProbeManagedPlayerControlSafe(out managedKnown);
            bool nativeControlOn = ProbeNativePlayerControlSafe(out nativeKnown);

            if (managedKnown && nativeKnown) return !managedCanControl && !nativeControlOn;
            if (managedKnown) return !managedCanControl;
            if (nativeKnown) return !nativeControlOn;
            return false;
        }

        private bool ProbeManagedPlayerControlSafe(out bool known)
        {
            known = false;
            try
            {
                bool value = Player.CanControlCharacter;
                known = true;
                return value;
            }
            catch
            {
                return true;
            }
        }

        private bool ProbeNativePlayerControlSafe(out bool known)
        {
            known = false;
            try
            {
                int playerId = 0;
                try { playerId = Function.Call<int>("GET_PLAYER_ID"); }
                catch { playerId = Function.Call<int>("GET_PLAYER_INDEX"); }
                bool value = Function.Call<bool>("IS_PLAYER_CONTROL_ON", playerId);
                known = true;
                return value;
            }
            catch
            {
                return true;
            }
        }

        private bool IsFuelGameplaySuspended()
        {


            try { if (Function.Call<bool>("IS_SCREEN_FADED_OUT")) return true; } catch { }
            try { if (Function.Call<bool>("IS_SCREEN_FADING")) return true; } catch { }
            try { if (Function.Call<bool>("IS_SCREEN_FADING_OUT")) return true; } catch { }
            try { if (Function.Call<bool>("IS_SCREEN_FADING_IN")) return true; } catch { }


            return false;
        }

        private void ShowFuelStatus(string text, int durationMs)
        {
            fuelStatusText = text == null ? "" : text;
            fuelStatusUntilMs = Environment.TickCount + Math.Max(500, durationMs);
        }

        private void UpdateFuelStationCooldowns(float dt)
        {
            for (int i = 0; i < fuelStations.Count; i++)
            {
                FuelStation s = fuelStations[i];
                if (s == null || s.DamagedSeconds <= 0.0f) continue;
                s.DamagedSeconds -= dt;
                if (s.DamagedSeconds < 0.0f) s.DamagedSeconds = 0.0f;
            }
        }


        private void EnforceRefuellingVehicleBetweenFuelTicks()
        {
            if (!fuelRefuelling) return;
            Vehicle v;
            if (!TryGetPlayerDrivenFuelVehicle(out v))
            {
                StopFuelRefuelling("Refuelling stopped.");
                return;
            }
            if (fuelRefuellingStation == null || GetNearestFuelStationForVehicle(v) != fuelRefuellingStation)
            {
                StopFuelRefuelling("Refuelling stopped.");
                return;
            }
            MaintainFuelRefuellingLock(v);
        }

        private void MaintainFuelRefuellingLock(Vehicle v)
        {
            if (!fuelRefuelling || !IsValidVehicle(v)) return;
            SafeSetVehicleEngine(v, false);
            if (ShouldUseFuelHandbrake(v)) SafeSetVehicleHandbrake(v, true);
        }

        private bool TryHandleFuelRefuelKey()
        {
            Vehicle v;
            if (!TryGetPlayerDrivenFuelVehicle(out v)) return false;
            FuelStation station = GetNearestFuelStationForVehicle(v);
            if (station == null) return false;

            FuelVehicleState state = GetOrCreateFuelState(v, false);
            if (state == null) return true;

            if (fuelRefuelling)
            {
                StopFuelRefuelling("Refuelling cancelled.");
                return true;
            }
            if (station.DamagedSeconds > 0.0f)
            {
                ShowFuelPrompt("Pump damaged. Come back later.");
                return true;
            }
            if (SafeVehicleSpeed(v) > fuelStoppedSpeedThreshold)
            {
                ShowFuelPrompt("Stop the vehicle to refuel.");
                return true;
            }
            if (IsHeliFuelStation(station) && !IsHeliLandedForRefuel(v, station))
            {
                ShowFuelPrompt("Land the helicopter to refuel.");
                return true;
            }
            if (state.FuelGallons >= state.CapacityGallons - FuelEpsilonGallons)
            {
                ShowFuelPrompt("Fuel tank is full.");
                return true;
            }
            fuelRefuelling = true;
            fuelRefuellingStation = station;
            fuelRefuellingVehicle = v;
            fuelRefuelCostCarry = 0.0f;
            fuelRefuelStartedAtMs = Environment.TickCount;
            MaintainFuelRefuellingLock(v);
            ShowFuelPrompt("Refuelling at " + station.Name + ". Press " + KeyLabel(keyOpen) + " to stop.");
            if (showDebugMessages) Log("Fuel refuel started. Station=" + station.Id + ", Vehicle=" + state.ModelName + ", Fuel=" + state.FuelGallons.ToString("0.00", CultureInfo.InvariantCulture) + "/" + state.CapacityGallons.ToString("0.00", CultureInfo.InvariantCulture) + ".");
            return true;
        }

        private void ProcessFuelRefuelling(Vehicle v, FuelVehicleState state, float dt)
        {
            if (!fuelRefuelling || state == null || !IsValidVehicle(v)) return;
            FuelStation station = fuelRefuellingStation;
            if (station == null || GetNearestFuelStationForVehicle(v) != station)
            {
                StopFuelRefuelling("Refuelling stopped.");
                return;
            }
            if (station.DamagedSeconds > 0.0f)
            {
                StopFuelRefuelling("Pump damaged. Refuelling stopped.");
                return;
            }
            MaintainFuelRefuellingLock(v);
            float missing = state.CapacityGallons - state.FuelGallons;
            if (missing <= FuelEpsilonGallons)
            {
                state.FuelGallons = state.CapacityGallons;
                StopFuelRefuelling("Fuel tank is full.");
                return;
            }
            float add = Math.Min(missing, fuelRefuelRateGallonsPerSecond * dt);
            float costFloat = add * fuelPricePerGallon;
            fuelRefuelCostCarry += costFloat;
            int charge = (int)Math.Floor(fuelRefuelCostCarry);
            if (charge > 0)
            {
                if (Player.Money < charge)
                {
                    StopFuelRefuelling("Not enough money to continue refuelling.");
                    return;
                }
                Player.Money -= charge;
                fuelRefuelCostCarry -= charge;
            }
            state.FuelGallons += add;
            if (state.FuelGallons > state.CapacityGallons) state.FuelGallons = state.CapacityGallons;
            state.WasOutOfFuel = false;
            state.EngineForcedOff = false;
            state.OutOfFuelEnforceNextMs = 0;
            ShowFuelPrompt("Refuelling: " + FormatFuel(state) + "  $" + fuelPricePerGallon.ToString("0.00", CultureInfo.InvariantCulture) + "/gal  " + KeyLabel(keyOpen) + ": stop");
            if (state.FuelGallons >= state.CapacityGallons - FuelEpsilonGallons)
            {
                state.FuelGallons = state.CapacityGallons;
                StopFuelRefuelling("Fuel tank is full.");
            }
        }

        private void StopFuelRefuelling(string message)
        {
            bool wasRefuelling = fuelRefuelling;
            Vehicle v = fuelRefuellingVehicle;
            if (!IsValidVehicle(v))
            {
                Vehicle current;
                if (TryGetPlayerDrivenFuelVehicle(out current)) v = current;
            }

            bool hasUsableFuel = true;
            int handle = IsValidVehicle(v) ? TryGetVehicleHandle(v) : 0;
            if (fuelCurrentState != null && handle != 0 && fuelCurrentState.Handle == handle)
                hasUsableFuel = fuelCurrentState.FuelGallons > FuelEpsilonGallons;

            fuelRefuelling = false;
            fuelRefuellingStation = null;
            fuelRefuellingVehicle = null;
            fuelRefuelCostCarry = 0.0f;
            fuelRefuelStartedAtMs = 0;

            ReleaseFuelRefuellingLock(v, hasUsableFuel);
            if (wasRefuelling && message != null && message.Length > 0) ShowFuelPrompt(message);
        }

        private void ReleaseFuelRefuellingLock(Vehicle v, bool allowEngineStart)
        {
            if (!IsValidVehicle(v)) return;
            if (ShouldUseFuelHandbrake(v)) SafeSetVehicleHandbrake(v, false);
            if (allowEngineStart) SafeSetVehicleEngine(v, true);
        }

        private void ConsumeFuel(Vehicle v, FuelVehicleState state, float dt)
        {
            if (state == null || state.FuelGallons <= 0.0f) return;
            float capacity = Math.Max(1.0f, state.CapacityGallons);
            float targetMinutes = GetFuelTargetMinutes(v, state);
            float basePerSecond = capacity / Math.Max(10.0f, targetMinutes) / 60.0f;
            float speed = SafeVehicleSpeed(v);
            float movementFactor = 0.15f;
            if (speed > 0.35f) movementFactor = 0.80f + Math.Min(1.20f, speed / 35.0f);
            float damageFactor = 1.0f;
            try { damageFactor += Math.Max(0.0f, 1000.0f - (float)v.EngineHealth) / 1000.0f * 0.40f; } catch { }
            try { damageFactor += Math.Max(0.0f, 1000.0f - (float)v.PetrolTankHealth) / 1000.0f * 0.60f; } catch { }
            float multiplier = fuelConsumptionMultiplier;
            if (state.MissionConsumptionReduced) multiplier *= fuelMissionConsumptionMultiplier;
            float used = basePerSecond * movementFactor * damageFactor * multiplier * dt;
            if (used <= 0.0f) return;
            state.FuelGallons -= used;
            if (state.FuelGallons < 0.0f) state.FuelGallons = 0.0f;
        }

        private float GetFuelTargetMinutes(Vehicle v, FuelVehicleState state)
        {
            VehicleDefinition def = GetVehicleDefinitionByHashOnly(v);
            if (def != null)
            {
                string section = def.Section == null ? "" : def.Section.ToLowerInvariant();
                if (def.IsBike) return 135.0f;
                if (section == "boats") return 120.0f;
                if (section == "heli" || section == "planes") return 95.0f;
                if (def.IsIndustrial) return 110.0f;
                if (def.IsSports || def.Swankness >= 5) return 120.0f;
                if (def.Swankness <= 1) return 155.0f;
                return 150.0f;
            }
            return 150.0f;
        }

        private void ApplyFuelVehicleBehaviour(Vehicle v, FuelVehicleState state, float dt)
        {
            if (state == null || !IsValidVehicle(v)) return;
            float percent = GetFuelPercent(state);
            if (state.LowPromptCooldownMs > 0) state.LowPromptCooldownMs -= (int)(dt * 1000.0f);
            if (state.SputterCooldownMs > 0) state.SputterCooldownMs -= (int)(dt * 1000.0f);

            if (state.FuelGallons <= FuelEpsilonGallons)
            {
                state.FuelGallons = 0.0f;
                if (!state.WasOutOfFuel)
                {
                    ShowFuelStatus("Out of fuel", 3500);
                    state.WasOutOfFuel = true;
                }
                ForceOutOfFuelVehicleOff(v, state);
                ShowFuelStatus("Out of fuel", 1200);
                return;
            }

            if (state.WasOutOfFuel || state.EngineForcedOff)
            {
                state.WasOutOfFuel = false;
                state.EngineForcedOff = false;
                state.OutOfFuelEnforceNextMs = 0;
                SafeSetVehicleHandbrake(v, false);
                SafeSetVehicleEngine(v, true);
            }

            if (percent <= fuelCriticalSputterPercent && state.SputterCooldownMs <= 0)
            {
                SputterFuelVehicle(v, true);
                state.SputterCooldownMs = 2500;
                ShowFuelStatus("Low fuel " + ((int)Math.Round(percent)).ToString() + "%", 2500);
                return;
            }
            if (percent <= fuelSputterPercent && state.SputterCooldownMs <= 0)
            {
                SputterFuelVehicle(v, false);
                state.SputterCooldownMs = 6000;
                ShowFuelStatus("Low fuel", 2500);
                return;
            }
            if (percent <= fuelLowPromptPercent && state.LowPromptCooldownMs <= 0)
            {
                ShowFuelStatus("Low fuel " + ((int)Math.Round(percent)).ToString() + "%", 3000);
                state.LowPromptCooldownMs = 15000;
            }
        }

        private void EnforceOutOfFuelVehicleBetweenFuelTicks()
        {
            if (fuelRefuelling) return;
            if (fuelCurrentState == null || fuelCurrentState.FuelGallons > FuelEpsilonGallons) return;
            if (IsFuelGameplaySuspended()) return;
            Vehicle v;
            if (!TryGetPlayerDrivenFuelVehicle(out v)) return;
            if (TryGetVehicleHandle(v) != fuelCurrentState.Handle) return;
            ForceOutOfFuelVehicleOff(v, fuelCurrentState);
        }

        private void ForceOutOfFuelVehicleOff(Vehicle v, FuelVehicleState state)
        {
            if (!IsValidVehicle(v) || state == null || fuelRefuelling) return;
            int now = Environment.TickCount;
            if (state.OutOfFuelEnforceNextMs > 0 && now < state.OutOfFuelEnforceNextMs) return;
            SafeSetVehicleEngine(v, false);
            if (ShouldUseFuelHandbrake(v)) SafeSetVehicleHandbrake(v, true);
            state.EngineForcedOff = true;
            state.OutOfFuelEnforceNextMs = now + 100;
        }

        private bool ShouldUseFuelHandbrake(Vehicle v)
        {
            VehicleDefinition def = GetVehicleDefinitionByHashOnly(v);
            if (def == null) return true;
            string section = def.Section == null ? "" : def.Section.ToLowerInvariant();
            if (section == "boats" || section == "heli" || section == "planes") return false;
            return true;
        }

        private void SputterFuelVehicle(Vehicle v, bool critical)
        {
            if (!IsValidVehicle(v) || fuelRefuelling) return;


            SafeSetVehicleEngine(v, false);
            fuelSputterRestoreVehicleHandle = TryGetVehicleHandle(v);
            fuelSputterRestoreAtMs = Environment.TickCount + (critical ? 1400 : 600);
        }

        private void RestoreFuelSputterEngineIfDue(Vehicle v, FuelVehicleState state)
        {
            if (fuelSputterRestoreVehicleHandle == 0) return;
            if (Environment.TickCount < fuelSputterRestoreAtMs) return;
            int handle = TryGetVehicleHandle(v);
            if (handle == fuelSputterRestoreVehicleHandle && state != null && state.FuelGallons > FuelEpsilonGallons && !fuelRefuelling)
            {
                SafeSetVehicleEngine(v, true);
            }
            fuelSputterRestoreVehicleHandle = 0;
            fuelSputterRestoreAtMs = 0;
        }

        private void UpdateFuelPrompt(Vehicle v, FuelVehicleState state)
        {
            if (state == null || !IsValidVehicle(v)) return;
            FuelStation station = GetNearestFuelStationForVehicle(v);
            if (station == null) return;
            if (fuelRefuelling && fuelRefuellingStation == station)
            {
                ShowFuelPrompt("Refuelling: " + FormatFuel(state) + "  $" + fuelPricePerGallon.ToString("0.00", CultureInfo.InvariantCulture) + "/gal  " + KeyLabel(keyOpen) + ": stop");
                return;
            }
            if (station.DamagedSeconds > 0.0f)
            {
                ShowFuelPrompt("Pump damaged. Come back later.");
                return;
            }
            if (state.FuelGallons >= state.CapacityGallons - FuelEpsilonGallons)
            {
                ShowFuelPrompt("Fuel tank full: " + FormatFuel(state));
                return;
            }
            if (SafeVehicleSpeed(v) > fuelStoppedSpeedThreshold)
            {
                ShowFuelPrompt("Stop at " + station.Name + " to refuel.");
                return;
            }
            if (IsHeliFuelStation(station) && !IsHeliLandedForRefuel(v, station))
            {
                ShowFuelPrompt("Land the helicopter to refuel.");
                return;
            }
            ShowFuelPrompt("Press " + KeyLabel(keyOpen) + " to refuel at " + station.Name + ". " + FormatFuel(state));
        }

        private string FormatFuel(FuelVehicleState state)
        {
            if (state == null) return "0.0 gal";
            return state.FuelGallons.ToString("0.0", CultureInfo.InvariantCulture) + "/" + state.CapacityGallons.ToString("0.0", CultureInfo.InvariantCulture) + " gal";
        }

        private float GetFuelPercent(FuelVehicleState state)
        {
            if (state == null || state.CapacityGallons <= 0.0f) return 0.0f;
            return (state.FuelGallons / state.CapacityGallons) * 100.0f;
        }

        private void ApplyMissionFuelProtection(Vehicle v, FuelVehicleState state, bool allowMinimumTopUp)
        {
            if (state == null) return;
            state.MissionConsumptionReduced = false;
            bool missionRelevant = IsMissionActiveFreshSafe();
            if (!missionRelevant) return;
            state.MissionConsumptionReduced = true;
            if (!allowMinimumTopUp)
            {
                state.MissionBoostApplied = true;
                return;
            }
            if (state.MissionBoostApplied) return;
            float minFuel = state.CapacityGallons * ClampFloat(fuelMissionMinimumPercent, 0.0f, 100.0f) / 100.0f;
            if (state.FuelGallons < minFuel)
            {
                state.FuelGallons = minFuel;
                if (showDebugMessages) Log("Fuel mission protection applied on first fuel initialisation. Vehicle=" + state.ModelName + ", Fuel=" + state.FuelGallons.ToString("0.00", CultureInfo.InvariantCulture) + "/" + state.CapacityGallons.ToString("0.00", CultureInfo.InvariantCulture) + ".");
            }
            state.MissionBoostApplied = true;
        }

        private void TryApplyPendingMissionFuelProtection(Vehicle v, FuelVehicleState state)
        {
            if (state == null || state.MissionBoostApplied) return;
            int now = Environment.TickCount;
            if (state.MissionProbeUntilMs <= 0 || now > state.MissionProbeUntilMs) return;
            if (state.MissionProbeNextMs > 0 && now < state.MissionProbeNextMs) return;
            state.MissionProbeNextMs = now + FuelMissionEntryProbeIntervalMs;
            ApplyMissionFuelProtection(v, state, true);
        }

        private void UpdateMissionFuelConsumptionState(FuelVehicleState state)
        {
            if (state == null) return;
            state.MissionConsumptionReduced = IsMissionActiveSafe(false);
        }

        private bool IsVehicleRequiredForMissionSafe(Vehicle v)
        {


            return false;
        }

        private bool TryGetPlayerDrivenFuelVehicle(out Vehicle v)
        {
            v = null;
            if (!IsPlayerValid()) return false;
            try
            {
                if (!Player.Character.isSittingInVehicle()) return false;
                v = Player.Character.CurrentVehicle;
            }
            catch { return false; }
            if (!IsValidVehicle(v)) return false;
            if (!IsPlayerDriverOfVehicle(v)) return false;
            if (!IsFuelEligibleVehicle(v)) return false;
            return true;
        }

        private bool IsPlayerDriverOfVehicle(Vehicle v)
        {
            if (!IsValidVehicle(v) || !IsPlayerValid()) return false;

            try
            {
                MethodInfo m = v.GetType().GetMethod("GetPedOnSeat");
                if (m != null)
                {
                    ParameterInfo[] ps = m.GetParameters();
                    if (ps != null && ps.Length == 1)
                    {
                        object driverSeat = null;
                        try { driverSeat = Enum.Parse(ps[0].ParameterType, "Driver"); } catch { }
                        if (driverSeat == null)
                        {
                            try { driverSeat = Enum.ToObject(ps[0].ParameterType, -1); } catch { }
                        }
                        if (driverSeat != null)
                        {
                            object result = m.Invoke(v, new object[] { driverSeat });
                            Ped driver = result as Ped;
                            if (driver != null && driver.Exists()) return IsSamePed(driver, Player.Character);
                            return false;
                        }
                    }
                }
            }
            catch { }

            try
            {
                Ped driver = Function.Call<Ped>("GET_DRIVER_OF_CAR", v);
                if (driver != null && driver.Exists()) return IsSamePed(driver, Player.Character);
            }
            catch { }

            bool? seatPropertyResult = TryReadPlayerDriverSeatProperty();
            if (seatPropertyResult.HasValue) return seatPropertyResult.Value;

            try { if (Player.Character.isSittingInVehicle() && Player.Character.CurrentVehicle == v) return true; } catch { }
            return false;
        }

        private bool? TryReadPlayerDriverSeatProperty()
        {
            if (!IsPlayerValid()) return null;
            string[] names = new string[] { "CurrentVehicleSeat", "VehicleSeat", "Seat", "SeatIndex", "CurrentSeat" };
            for (int i = 0; i < names.Length; i++)
            {
                try
                {
                    PropertyInfo p = Player.Character.GetType().GetProperty(names[i]);
                    if (p == null) continue;
                    object value = p.GetValue(Player.Character, null);
                    if (value == null) continue;
                    if (value is int)
                    {
                        int seat = (int)value;
                        if (seat == -1) return true;
                        return false;
                    }
                    string s = value.ToString();
                    if (s == null || s.Length == 0) continue;
                    if (s.IndexOf("driver", StringComparison.OrdinalIgnoreCase) >= 0) return true;
                    int parsed;
                    if (int.TryParse(s, out parsed))
                    {
                        if (parsed == -1) return true;
                        return false;
                    }
                }
                catch { }
            }
            return null;
        }

        private bool IsSamePed(Ped a, Ped b)
        {
            if (a == null || b == null) return false;
            try { if (!a.Exists() || !b.Exists()) return false; } catch { return false; }
            int ah = TryGetPedHandle(a);
            int bh = TryGetPedHandle(b);
            if (ah != 0 && bh != 0) return ah == bh;
            return object.ReferenceEquals(a, b);
        }

        private int TryGetPedHandle(Ped p)
        {
            if (p == null) return 0;
            try
            {
                PropertyInfo prop = p.GetType().GetProperty("Handle");
                if (prop != null)
                {
                    object value = prop.GetValue(p, null);
                    if (value != null) return Convert.ToInt32(value);
                }
            }
            catch { }
            try
            {
                PropertyInfo prop = p.GetType().GetProperty("MemoryAddress");
                if (prop != null)
                {
                    object value = prop.GetValue(p, null);
                    if (value != null) return Convert.ToInt32(value);
                }
            }
            catch { }
            return p.GetHashCode();
        }

        private bool IsFuelEligibleVehicle(Vehicle v)
        {
            VehicleDefinition def = GetVehicleDefinitionByHashOnly(v);
            if (def == null) return true;
            if (IsRailVehicleDefinition(def)) return false;
            string section = def.Section == null ? "" : def.Section.ToLowerInvariant();
            if (section == "cars" || section == "bikes" || section == "boats" || section == "heli" || section == "planes") return true;
            return false;
        }

        private bool IsRailVehicleDefinition(VehicleDefinition def)
        {
            if (def == null) return false;
            string section = def.Section == null ? "" : def.Section.ToLowerInvariant();
            string type = def.Type == null ? "" : def.Type.ToLowerInvariant();
            string model = def.ModelName == null ? "" : def.ModelName.ToLowerInvariant();
            string gameName = def.GameName == null ? "" : def.GameName.ToLowerInvariant();
            if (section == "trains") return true;
            if (type.IndexOf("train") >= 0) return true;
            if (ContainsAny(model, new string[] { "subway", "train", "cablecar" })) return true;
            if (ContainsAny(gameName, new string[] { "subway", "train", "cablecar" })) return true;
            return false;
        }

        private FuelVehicleState GetOrCreateFuelState(Vehicle v, bool dealerPurchase)
        {
            if (!IsValidVehicle(v)) return null;
            int handle = TryGetVehicleHandle(v);
            if (handle == 0) return null;
            FuelVehicleState state;
            if (fuelStates.TryGetValue(handle, out state) && state != null) return state;

            VehicleDefinition def = GetVehicleDefinitionByHashOnly(v);
            state = new FuelVehicleState();
            state.Handle = handle;
            state.ModelName = def == null ? GetModelNameForVehicleFast(v) : def.ModelName;
            state.CapacityGallons = GetFuelCapacityGallons(v, def);
            if (state.CapacityGallons <= 0.0f) return null;
            state.OwnedRecord = FindOwnedRecordForVehicle(v);
            if (state.OwnedRecord != null)
            {
                if (state.OwnedRecord.FuelKnown && state.OwnedRecord.FuelGallons >= 0.0f)
                {
                    state.FuelGallons = Math.Min(state.CapacityGallons, state.OwnedRecord.FuelGallons);
                }
                else
                {
                    float pct = dealerPurchase ? fuelDealerPurchasePercent : RandomFuelPercentForVehicle(def);
                    state.FuelGallons = state.CapacityGallons * pct / 100.0f;
                    state.OwnedRecord.FuelGallons = state.FuelGallons;
                    state.OwnedRecord.FuelKnown = true;
                }
            }
            else
            {
                float pct = dealerPurchase ? fuelDealerPurchasePercent : RandomFuelPercentForVehicle(def);
                state.FuelGallons = state.CapacityGallons * pct / 100.0f;
            }
            if (state.FuelGallons < 0.0f) state.FuelGallons = 0.0f;
            if (state.FuelGallons > state.CapacityGallons) state.FuelGallons = state.CapacityGallons;
            fuelStates[handle] = state;
            if (showDebugMessages) Log("Fuel state initialised. Vehicle=" + state.ModelName + ", Fuel=" + state.FuelGallons.ToString("0.00", CultureInfo.InvariantCulture) + "/" + state.CapacityGallons.ToString("0.00", CultureInfo.InvariantCulture) + " gal.");
            return state;
        }

        private float RandomFuelPercentForVehicle(VehicleDefinition def)
        {
            float min = Math.Min(fuelStartingMinPercent, fuelStartingMaxPercent);
            float max = Math.Max(fuelStartingMinPercent, fuelStartingMaxPercent);
            if (def != null)
            {
                string section = def.Section == null ? "" : def.Section.ToLowerInvariant();
                if (def.IsSports || def.Swankness >= 5) min = Math.Max(min, 25.0f);
                if (def.IsIndustrial || section == "boats" || section == "heli" || section == "planes") min = Math.Max(min, 15.0f);
                if (def.Swankness <= 1 && section != "heli" && section != "planes") max = Math.Min(max, 70.0f);
            }
            if (max < min) max = min;
            double roll = 0.5;
            try { roll = fuelRandom.NextDouble(); } catch { }
            return min + (float)roll * (max - min);
        }

        private float GetFuelCapacityGallons(Vehicle v, VehicleDefinition def)
        {
            if (def == null) return 15.0f;
            string model = def.ModelName == null ? "" : def.ModelName.ToLowerInvariant();
            string section = def.Section == null ? "" : def.Section.ToLowerInvariant();
            if (def.IsBike)
            {
                if (model == "faggio") return 2.5f;
                return 4.0f;
            }
            if (section == "boats")
            {
                if (ContainsAny(model, new string[] { "dinghy" })) return 10.0f;
                if (ContainsAny(model, new string[] { "tug" })) return 80.0f;
                return 24.0f;
            }
            if (section == "heli" || section == "planes")
            {
                if (ContainsAny(model, new string[] { "annihilator" })) return 95.0f;
                return 75.0f;
            }
            if (def.IsIndustrial)
            {
                if (ContainsAny(model, new string[] { "phantom", "packer", "flatbed", "biff" })) return 45.0f;
                if (ContainsAny(model, new string[] { "bus", "mule", "yankee", "benson", "stockade" })) return 32.0f;
                return 24.0f;
            }
            if (ContainsAny(model, new string[] { "patriot", "cavalcade", "cavcade", "rancher", "huntley", "rebla", "landstalker" })) return 22.0f;
            if (def.IsSports || def.Swankness >= 5) return 18.0f;
            if (def.Swankness <= 1) return 12.0f;
            if (def.Swankness >= 4) return 17.0f;
            return 15.0f;
        }

        private FuelStation GetNearestFuelStationForVehicle(Vehicle v)
        {
            if (!IsValidVehicle(v) || fuelStations.Count == 0) return null;
            float x; float y; float z;
            if (!TryGetVehiclePosition(v, out x, out y, out z)) return null;
            FuelStation best = null;
            float bestD2 = 99999999.0f;
            for (int i = 0; i < fuelStations.Count; i++)
            {
                FuelStation s = fuelStations[i];
                if (s == null) continue;
                if (!CanFuelStationServeVehicle(s, v)) continue;
                float dz = Math.Abs(z - s.Z);
                float maxDz = GetFuelStationVerticalTolerance(s, v);
                if (dz > maxDz) continue;
                float d2 = Distance2DSquared(x, y, s.X, s.Y);
                float r = Math.Max(2.0f, s.Radius);
                if (d2 <= r * r && d2 < bestD2)
                {
                    best = s;
                    bestD2 = d2;
                }
            }
            return best;
        }

        private float GetFuelStationVerticalTolerance(FuelStation station, Vehicle v)
        {
            string kind = station == null || station.Kind == null ? "road" : station.Kind.ToLowerInvariant();
            if (kind == "boat") return 6.0f;
            if (kind == "heli") return 2.0f;
            return 8.0f;
        }

        private bool CanFuelStationServeVehicle(FuelStation station, Vehicle v)
        {
            if (station == null || !IsValidVehicle(v)) return false;
            VehicleDefinition def = GetVehicleDefinitionByHashOnly(v);
            string kind = station.Kind == null ? "road" : station.Kind.ToLowerInvariant();
            if (def == null)
            {
                return kind == "road";
            }
            string vehicleSection = def.Section == null ? "" : def.Section.ToLowerInvariant();
            if (kind == "boat") return vehicleSection == "boats";
            if (kind == "heli") return vehicleSection == "heli" || vehicleSection == "planes";
            return vehicleSection == "cars" || vehicleSection == "bikes";
        }

        private bool IsHeliFuelStation(FuelStation station)
        {
            return station != null && station.Kind != null && station.Kind.Equals("heli", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsHeliLandedForRefuel(Vehicle v, FuelStation station)
        {
            if (!IsValidVehicle(v) || station == null) return false;
            if (SafeVehicleSpeed(v) > fuelStoppedSpeedThreshold) return false;
            float x; float y; float z;
            if (!TryGetVehiclePosition(v, out x, out y, out z)) return false;
            return Math.Abs(z - station.Z) <= 2.0f;
        }

        private float Distance2D(float ax, float ay, float bx, float by)
        {
            return (float)Math.Sqrt(Distance2DSquared(ax, ay, bx, by));
        }

        private float Distance2DSquared(float ax, float ay, float bx, float by)
        {
            float dx = ax - bx;
            float dy = ay - by;
            return dx * dx + dy * dy;
        }

        private void SafeSetVehicleEngine(Vehicle v, bool on)
        {
            if (!IsValidVehicle(v)) return;
            try { Function.Call("SET_CAR_ENGINE_ON", v, on, true); } catch { }
        }
        private void SafeSetVehicleHandbrake(Vehicle v, bool on)
        {
            if (!IsValidVehicle(v)) return;
            try { Function.Call("SET_CAR_HANDBRAKE", v, on); } catch { }
        }


        private void UpdateOwnedFuelRecordFromState(FuelVehicleState state)
        {
            if (state == null) return;
            OwnedVehicleRecord rec = state.OwnedRecord;
            if (rec == null || !OwnedRecordBelongsToCurrentEpisode(rec)) return;
            if (!rec.FuelKnown || Math.Abs(rec.FuelGallons - state.FuelGallons) > 0.05f) ownedVehiclesDirty = true;
            rec.FuelGallons = state.FuelGallons;
            rec.FuelKnown = true;
        }

        private void CaptureOwnedFuelState(Vehicle v, OwnedVehicleRecord rec)
        {
            if (!fuelModuleEnabled || rec == null || !IsValidVehicle(v)) return;
            FuelVehicleState state = GetOrCreateFuelState(v, false);
            if (state == null) return;
            state.OwnedRecord = rec;
            rec.FuelGallons = state.FuelGallons;
            rec.FuelKnown = true;
        }

        private void InitialiseOwnedFuelState(Vehicle v, OwnedVehicleRecord rec, bool dealerPurchase)
        {
            if (!fuelModuleEnabled || rec == null || !IsValidVehicle(v)) return;
            FuelVehicleState state = GetOrCreateFuelState(v, dealerPurchase);
            if (state == null) return;
            state.OwnedRecord = rec;
            if (dealerPurchase && !rec.FuelKnown)
            {
                state.FuelGallons = state.CapacityGallons * ClampFloat(fuelDealerPurchasePercent, 0.0f, 100.0f) / 100.0f;
            }
            else if (rec.FuelKnown)
            {
                state.FuelGallons = Math.Min(state.CapacityGallons, Math.Max(0.0f, rec.FuelGallons));
            }
            rec.FuelGallons = state.FuelGallons;
            rec.FuelKnown = true;
        }

        private void LogCurrentPositionForConfig()
        {
            try
            {
                if (!IsPlayerValid()) { Notify("Position log failed: no player."); return; }
                Vector3 p = Player.Character.Position;
                float heading = SafeGetPedHeadingPropertyOnly(Player.Character, 0.0f);
                string line = "PLAYER X=" + p.X.ToString("0.000") + ", Y=" + p.Y.ToString("0.000") + ", Z=" + p.Z.ToString("0.000") + ", H=" + heading.ToString("0.000") + ". Marker token: Marker=" + p.X.ToString("0.000") + "," + p.Y.ToString("0.000") + "," + p.Z.ToString("0.000") + ",4.5";
                Log(line);
                if (IsValidVehicle(lastPlayerVehicle))
                {
                    float x, y, z;
                    if (TryGetVehiclePositionPropertyOnly(lastPlayerVehicle, out x, out y, out z))
                    {
                        float h = SafeGetVehicleHeadingPropertyOnly(lastPlayerVehicle, 0.0f);
                        Log("VEHICLE X=" + x.ToString("0.000") + ", Y=" + y.ToString("0.000") + ", Z=" + z.ToString("0.000") + ", H=" + h.ToString("0.000") + ". Display token: Display=" + x.ToString("0.000") + "," + y.ToString("0.000") + "," + z.ToString("0.000") + "," + h.ToString("0.000") + ". Model=" + GetModelNameForVehicleFast(lastPlayerVehicle) + ".");
                    }
                }
                Notify("Position written to LibertyVehicleServicesCE.log.");
            }
            catch (Exception ex)
            {
                Log("Position log error: " + ex.GetType().Name + ".");
            }
        }

        private bool TryCharge(int price)
        {
            if (price <= 0) return true;
            if (Player.Money < price)
            {
                Notify("Not enough money. Need $" + price.ToString() + ".");
                return false;
            }
            Player.Money -= price;
            return true;
        }

        private bool IsPlayerValid()
        {
            try { return Player.Character != null && Player.Character.Exists(); }
            catch { return false; }
        }

        private bool IsPlayerOnFoot()
        {
            try { return IsPlayerValid() && !Player.Character.isSittingInVehicle(); }
            catch { return false; }
        }

        private bool IsValidVehicle(Vehicle v)
        {
            return v != null && v.Exists();
        }

        private int TryGetVehicleHandle(Vehicle v)
        {
            if (!IsValidVehicle(v)) return 0;
            try
            {
                PropertyInfo p = v.GetType().GetProperty("Handle");
                if (p != null)
                {
                    object value = p.GetValue(v, null);
                    if (value != null) return Convert.ToInt32(value);
                }
            }
            catch { }
            try
            {
                PropertyInfo p = v.GetType().GetProperty("MemoryAddress");
                if (p != null)
                {
                    object value = p.GetValue(v, null);
                    if (value != null) return Convert.ToInt32(value);
                }
            }
            catch { }
            return v.GetHashCode();
        }

        private int TryGetVehicleModelHash(Vehicle v)
        {
            if (!IsValidVehicle(v)) return 0;
            try
            {
                object modelObj = v.GetType().GetProperty("Model").GetValue(v, null);
                int hash = ExtractModelHash(modelObj);
                if (hash != 0) return hash;
            }
            catch { }
            return 0;
        }

        private int ExtractModelHash(object modelObj)
        {
            if (modelObj == null) return 0;
            try
            {
                if (modelObj is int) return Convert.ToInt32(modelObj);
                string[] names = new string[] { "Hash", "Value", "ID", "Id", "ModelHash" };
                for (int i = 0; i < names.Length; i++)
                {
                    PropertyInfo p = modelObj.GetType().GetProperty(names[i]);
                    if (p == null) continue;
                    object value = p.GetValue(modelObj, null);
                    if (value != null)
                    {
                        int hash = Convert.ToInt32(value);
                        if (hash != 0) return hash;
                    }
                }
                string text = modelObj.ToString();
                if (text != null && vehicleByModel.ContainsKey(text.Trim().ToLowerInvariant())) return vehicleByModel[text.Trim().ToLowerInvariant()].Hash;
            }
            catch { }
            return 0;
        }

        private bool TryGetPlayerPosition(out float x, out float y, out float z)
        {
            x = 0.0f; y = 0.0f; z = 0.0f;
            try
            {
                if (Player.Character == null || !Player.Character.Exists()) return false;
                Vector3 p = Player.Character.Position;
                x = p.X; y = p.Y; z = p.Z;
                return true;
            }
            catch { }
            return false;
        }

        private bool TryGetVehiclePosition(Vehicle v, out float x, out float y, out float z)
        {
            x = 0.0f; y = 0.0f; z = 0.0f;
            if (!IsValidVehicle(v)) return false;
            try
            {
                Vector3 p = v.Position;
                x = p.X; y = p.Y; z = p.Z;
                return true;
            }
            catch { }
            try
            {
                x = Function.Call<float>("GET_CAR_COORDINATE_X", v);
                y = Function.Call<float>("GET_CAR_COORDINATE_Y", v);
                z = Function.Call<float>("GET_CAR_COORDINATE_Z", v);
                return true;
            }
            catch { }
            return false;
        }

        private float SafeGetVehicleHeading(Vehicle v, float fallback)
        {
            if (!IsValidVehicle(v)) return fallback;
            try
            {
                object value = v.GetType().GetProperty("Heading").GetValue(v, null);
                if (value != null) return Convert.ToSingle(value);
            }
            catch { }
            try { return Function.Call<float>("GET_CAR_HEADING", v); }
            catch { return fallback; }
        }

        private float SafeVehicleSpeed(Vehicle v)
        {
            if (!IsValidVehicle(v)) return 999.0f;
            try
            {
                object value = v.GetType().GetProperty("Speed").GetValue(v, null);
                if (value != null) return Math.Abs(Convert.ToSingle(value));
            }
            catch { }
            try { return Math.Abs(Function.Call<float>("GET_CAR_SPEED", v)); }
            catch { return 999.0f; }
        }

        private float Distance(Vector3 a, Vector3 b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            float dz = a.Z - b.Z;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        private string StripDataComment(string line)
        {
            if (line == null) return "";
            int hash = line.IndexOf('#');
            if (hash >= 0) line = line.Substring(0, hash);
            int semicolon = line.IndexOf(';');
            if (semicolon >= 0) line = line.Substring(0, semicolon);
            int slash = line.IndexOf("//");
            if (slash >= 0) line = line.Substring(0, slash);
            return line;
        }

        private bool TryParseCleanInt(string text, out int value)
        {
            value = 0;
            if (text == null) return false;
            string[] parts = text.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                float f;
                if (Single.TryParse(parts[i], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out f))
                {
                    value = (int)f;
                    return true;
                }
            }
            return false;
        }


        private string Join(List<string> parts, string sep)
        {
            if (parts == null || parts.Count == 0) return "";
            string s = parts[0];
            for (int i = 1; i < parts.Count; i++) s += sep + parts[i];
            return s;
        }

        private bool ReadBool(string value, bool fallback)
        {
            if (value == null) return fallback;
            string v = value.Trim().ToLowerInvariant();
            if (v == "1" || v == "true" || v == "yes" || v == "on") return true;
            if (v == "0" || v == "false" || v == "no" || v == "off") return false;
            return fallback;
        }

        private int ReadInt(string value, int fallback)
        {
            try { return Int32.Parse(value.Trim(), System.Globalization.CultureInfo.InvariantCulture); } catch { return fallback; }
        }

        private float ReadFloat(string value, float fallback)
        {
            try { return Single.Parse(value.Trim(), System.Globalization.CultureInfo.InvariantCulture); } catch { return fallback; }
        }

        private Keys ReadKey(string value, Keys fallback)
        {
            try { return (Keys)Enum.Parse(typeof(Keys), value.Trim(), true); }
            catch { return fallback; }
        }

        private void Notify(string text)
        {
            messageText = text == null ? "" : text;
            messageTimer = 250;
            if (showDebugMessages)
            {
                try { Game.DisplayText(text); } catch { }
            }
        }

        private void ResetLogFile()
        {
            if (!logToFile) return;
            try { File.WriteAllText(logPath, ""); }
            catch { }
        }

        private void Log(string text)
        {
            if (!logToFile) return;
            try { File.AppendAllText(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + text + Environment.NewLine); }
            catch { }
        }
    }
}
