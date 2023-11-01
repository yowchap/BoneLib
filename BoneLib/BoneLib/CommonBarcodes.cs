using System.Collections.Generic;
using SLZ.Marrow.SceneStreaming;

namespace BoneLib
{
    /// <summary>
    /// Holds common barcodes for BONELAB crates.
    /// </summary>
    public static class CommonBarcodes
    {  
        /// <summary>
        /// All avatar barcodes
        /// </summary>
        public static class Avatars
        {
            public static readonly List<string> All = new()
            {
                Heavy,
                Fast,
                Short,
                Tall,
                Strong,
                Light,
                Jimmy,
                FordBW,
                FordBL,
                PeasantFemaleA,
                PeasantFemaleB,
                PeasantFemaleC,
                PeasantMaleA,
                PeasantMaleB,
                PeasantMaleC,
                Nullbody,
                Skeleton,
                SecurityGuard,
                DuckSeasonDog,
                PolyBlank,
                PolyDebugger
            };
            public const string Heavy = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Heavy";
            public const string Fast = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Fast";
            public const string Short = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.CharFurv4GB";
            public const string Tall = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.CharTallv4";
            public const string Strong = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong";
            public const string Light = "SLZ.BONELAB.Content.Avatar.Anime";
            public const string Jimmy = "SLZ.BONELAB.Content.Avatar.CharJimmy";
            public const string FordBW = "SLZ.BONELAB.Content.Avatar.FordBW";
            public const string FordBL = "SLZ.BONELAB.Content.Avatar.CharFord";
            public const string PeasantFemaleA = "SLZ.BONELAB.Core.Avatar.PeasantFemaleA";
            public const string PeasantFemaleB = "c3534c5a-10bf-48e9-beca-4ca850656173";
            public const string PeasantFemaleC = "c3534c5a-2236-4ce5-9385-34a850656173";
            public const string PeasantMaleA = "c3534c5a-87a3-48b2-87cd-f0a850656173";
            public const string PeasantMaleB = "c3534c5a-f12c-44ef-b953-b8a850656173";
            public const string PeasantMaleC = "c3534c5a-3763-4ddf-bd86-6ca850656173";
            public const string Nullbody = "SLZ.BONELAB.Content.Avatar.Nullbody";
            public const string Skeleton = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Charskeleton";
            public const string SecurityGuard = "c3534c5a-d388-4945-b4ff-9c7a53656375";
            public const string DuckSeasonDog = "SLZ.BONELAB.Content.Avatar.DogDuckSeason";
            public const string PolyBlank = "c3534c5a-94b2-40a4-912a-24a8506f6c79";
            public const string PolyDebugger = "SLZ.BONELAB.NoBuild.Avatar.PolyDebugger";
        }
        
        /// <summary>
        /// All map barcodes
        /// </summary>
        public static class Maps
        {
            public static readonly List<string> All = new()
            {
                MainMenu,
                Descent,
                BLHub,
                LongRun,
                MineDive,
                BigAnomaly,
                StreetPuncher,
                SprintBridge,
                MagmaGate,
                Moonbase,
                MonogonMotorway,
                PillarClimb,
                BigAnomaly2,
                Ascent,
                Home,
                VoidG114,
                Baseline,
                Tuscany,
                MuseumBasement,
                HalfwayPark,
                GunRange,
                Holochamber,
                BigBoneBowling,
                Mirror,
                NeonTrial,
                DropPit,
                TunnelTipper,
                FantasyArena,
                ContainerYard,
                DungeonWarrior,
                Rooftops,
                NeonParkour
            };
            // Story
            public const string MainMenu = "c2534c5a-80e1-4a29-93ca-f3254d656e75";
            public const string Descent = "c2534c5a-4197-4879-8cd3-4a695363656e";
            public const string BLHub = "c2534c5a-6b79-40ec-8e98-e58c5363656e";
            public const string LongRun = "c2534c5a-56a6-40ab-a8ce-23074c657665";
            public const string MineDive = "c2534c5a-54df-470b-baaf-741f4c657665";
            public const string BigAnomaly = "c2534c5a-7601-4443-bdfe-7f235363656e";
            public const string StreetPuncher = "SLZ.BONELAB.Content.Level.LevelStreetPunch";
            public const string SprintBridge = "SLZ.BONELAB.Content.Level.SprintBridge04";
            public const string MagmaGate = "SLZ.BONELAB.Content.Level.SceneMagmaGate";
            public const string Moonbase = "SLZ.BONELAB.Content.Level.MoonBase";
            public const string MonogonMotorway = "SLZ.BONELAB.Content.Level.LevelKartRace";
            public const string PillarClimb = "c2534c5a-c056-4883-ac79-e051426f6964";
            public const string BigAnomaly2 = "SLZ.BONELAB.Content.Level.LevelBigAnomalyB";
            public const string Ascent = "c2534c5a-db71-49cf-b694-24584c657665";
            public const string Home = "SLZ.BONELAB.Content.Level.LevelOutro";
            public const string VoidG114 = "fa534c5a868247138f50c62e424c4144.Level.VoidG114";
            // Sandbox
            public const string Baseline = "c2534c5a-61b3-4f97-9059-79155363656e";
            public const string Tuscany = "c2534c5a-2c4c-4b44-b076-203b5363656e";
            public const string MuseumBasement = "fa534c5a83ee4ec6bd641fec424c4142.Level.LevelMuseumBasement";
            public const string HalfwayPark = "fa534c5a83ee4ec6bd641fec424c4142.Level.LevelHalfwayPark";
            public const string GunRange = "fa534c5a83ee4ec6bd641fec424c4142.Level.LevelGunRange";
            public const string Holochamber = "fa534c5a83ee4ec6bd641fec424c4142.Level.LevelHoloChamber";
            // Experimental
            public const string BigBoneBowling = "fa534c5a83ee4ec6bd641fec424c4142.Level.LevelKartBowling";
            public const string Mirror = "SLZ.BONELAB.Content.Level.LevelMirror";
            // Tac Trial
            public const string NeonTrial = "c2534c5a-4f3b-480e-ad2f-69175363656e";
            public const string DropPit = "c2534c5a-de61-4df9-8f6c-416954726547";
            // Arena
            public const string TunnelTipper = "c2534c5a-c180-40e0-b2b7-325c5363656e";
            public const string FantasyArena = "fa534c5a868247138f50c62e424c4144.Level.LevelArenaMin";
            public const string ContainerYard = "c2534c5a-162f-4661-a04d-975d5363656e";
            // Parkour
            public const string DungeonWarrior = "c2534c5a-5c2f-4eef-a851-66214c657665";
            public const string Rooftops = "c2534c5a-c6ac-48b4-9c5f-b5cd5363656e";
            public const string NeonParkour = "fa534c5a83ee4ec6bd641fec424c4142.Level.SceneparkourDistrictLogic";
            // Load levels
            public const string LoadDefault = "fa534c5a83ee4ec6bd641fec424c4142.Level.DefaultLoad";
            public const string LoadMod = "SLZ.BONELAB.CORE.Level.LevelModLevelLoad";
        }
        
        /// <summary>
        /// All NPC barcodes
        /// </summary>
        public static class NPCs
        {
            public static readonly List<string> All = new()
            {
                Crablet,
                CrabletPlus,
                Cultist,
                EarlyExitZombie,
                Ford,
                FordVRJunkie,
                Nullbody,
                NullbodyAgent,
                NullbodyCorrupted,
                Nullrat,
                OmniProjectorHazmat,
                OmniTurret,
                PeasantFemaleA,
                PeasantFemaleB,
                PeasantFemaleC,
                PeasantMaleA,
                PeasantMaleB,
                PeasantMaleC,
                PeasantNull,
                SecurityGuard,
                Skeleton,
                SkeletonFireMage,
                SkeletonSteel,
                VoidTurret
            };
            public const string Crablet = "c1534c5a-4583-48b5-ac3f-eb9543726162";
            public const string CrabletPlus = "c1534c5a-af28-46cb-84c1-012343726162";
            public const string Cultist = "SLZ.BONELAB.Content.Spawnable.NPCCultist";
            public const string EarlyExitZombie = "c1534c5a-2ab7-46fe-b0d6-7495466f7264";
            public const string Ford = "c1534c5a-3fd8-4d50-9eaf-0695466f7264";
            public const string FordVRJunkie = "c1534c5a-481a-45d8-8bc1-d810466f7264";
            public const string Nullbody = "c1534c5a-d82d-4f65-89fd-a4954e756c6c";
            public const string NullbodyAgent = "c1534c5a-0e54-4d5b-bdb8-31754e756c6c";
            public const string NullbodyCorrupted = "c1534c5a-2775-4009-9447-22d94e756c6c";
            public const string Nullrat = "c1534c5a-ef15-44c0-88ae-aebc4e756c6c";
            public const string OmniProjectorHazmat = "c1534c5a-7c6d-4f53-b61c-e4024f6d6e69";
            public const string OmniTurret = "c1534c5a-0df5-495d-8421-75834f6d6e69";
            public const string PeasantFemaleA = "SLZ.BONELAB.Content.Spawnable.NPCPeasantFemL";
            public const string PeasantFemaleB = "SLZ.BONELAB.Content.Spawnable.NPCPeasantFemM";
            public const string PeasantFemaleC = "SLZ.BONELAB.Content.Spawnable.NPCPeasantFemS";
            public const string PeasantMaleA = "SLZ.BONELAB.Content.Spawnable.NPCPeasantMaleL";
            public const string PeasantMaleB = "SLZ.BONELAB.Content.Spawnable.NPCPeasantMaleM";
            public const string PeasantMaleC = "SLZ.BONELAB.Content.Spawnable.NPCPeasantMaleS";
            public const string PeasantNull = "SLZ.BONELAB.Content.Spawnable.NPCPeasantNull";
            public const string SecurityGuard = "SLZ.BONELAB.Content.Spawnable.NPCSecurityGuard";
            public const string Skeleton = "c1534c5a-de57-4aa0-9021-5832536b656c";
            public const string SkeletonFireMage = "c1534c5a-bd53-469d-97f1-165e4e504353";
            public const string SkeletonSteel = "c1534c5a-a750-44ca-9730-b487536b656c";
            public const string VoidTurret = "c1534c5a-290e-4d56-9b8e-ad95566f6964";
        }
        
        /// <summary>
        /// All gun barcodes
        /// </summary>
        public static class Guns
        {
            public static readonly List<string> All = new()
            {
                M1911,
                Eder22,
                RedEder22,
                eHGBlaster,
                Gruber,
                M9,
                P350,
                PT8Alaris,
                Stapler,
                AKM,
                Garand,
                M16ACOG,
                M16Holosight,
                M16IronSights,
                M16LaserForegrip,
                MK18HoloForegrip,
                MK18Holosight,
                MK18IronSights,
                MK18LaserForegrip,
                MK18Naked,
                MK18Sabrelake,
                FAB,
                ShotgunWithHolosight,
                M4,
                DuckSeasonShotgun,
                MP5,
                MP5KFlashlight,
                MP5KHolosight,
                MP5KIronsights,
                MP5KLaser,
                MP5KSabrelake,
                PDRC,
                UMP,
                UZI,
                Vector
            };

            // Pistols
            public const string M1911 = "c1534c5a-fcfc-4f43-8fb0-d29531393131";
            public const string Eder22 = "c1534c5a-2a4f-481f-8542-cc9545646572";
            public const string RedEder22 = "SLZ.BONELAB.Content.Spawnable.HandgunEder22training";
            public const string eHGBlaster = "SLZ.BONELAB.CORE.Spawnable.GunEHG";
            public const string Gruber = "c1534c5a-9f55-4c56-ae23-d33b47727562";
            public const string M9 = "c1534c5a-aade-4fa1-8f4b-d4c547756e4d";
            public const string P350 = "c1534c5a-bcb7-4f02-a4f5-da9550333530";
            public const string PT8Alaris = "c1534c5a-50cf-4500-83d5-c0b447756e50";
            public const string Stapler = "fa534c5a868247138f50c62e424c4144.Spawnable.Stapler";
            // Rifles
            public const string AKM = "c1534c5a-a6b5-4177-beb8-04d947756e41";
            public const string Garand = "SLZ.BONELAB.Content.Spawnable.RifleM1Garand";
            public const string M16ACOG = "c1534c5a-ea97-495d-b0bf-ac955269666c";
            public const string M16Holosight = "c1534c5a-cc53-4aac-b842-46955269666c";
            public const string M16IronSights = "c1534c5a-9112-49e5-b022-9c955269666c";
            public const string M16LaserForegrip = "c1534c5a-4e5b-4fb7-be33-08955269666c";
            public const string MK18HoloForegrip = "SLZ.BONELAB.Content.Spawnable.RifleMK18HoloForegrip";
            public const string MK18Holosight = "c1534c5a-c061-4c5c-a5e2-3d955269666c";
            public const string MK18IronSights = "c1534c5a-f3b6-4161-a525-a8955269666c";
            public const string MK18LaserForegrip = "c1534c5a-ec8e-418a-a545-cf955269666c";
            public const string MK18Naked = "c1534c5a-5c2b-4cb4-ae31-e7955269666c";
            public const string MK18Sabrelake = "c1534c5a-4b3e-4288-849c-ce955269666c";
            // Shotguns
            public const string FAB = "c1534c5a-2774-48db-84fd-778447756e46";
            public const string ShotgunWithHolosight = "c1534c5a-7f05-402f-9320-609647756e35";
            public const string M4 = "c1534c5a-e0b5-4d4b-9df3-567147756e4d";
            public const string DuckSeasonShotgun = "c1534c5a-571f-43dc-8bc6-8e9553686f74";
            // SMGs
            public const string MP5 = "c1534c5a-d00c-4aa8-adfd-3495534d474d";
            public const string MP5KFlashlight = "c1534c5a-3e35-4aeb-b1ec-4a95534d474d";
            public const string MP5KHolosight = "fa534c5a83ee4ec6bd641fec424c4142.Spawnable.MP5KRedDotSight";
            public const string MP5KIronsights = "c1534c5a-9f54-4f32-b8b9-f295534d474d";
            public const string MP5KLaser = "c1534c5a-ccfa-4d99-af97-5e95534d474d";
            public const string MP5KSabrelake = "c1534c5a-6670-4ac2-a82a-a595534d474d";
            public const string PDRC = "c1534c5a-04d7-41a0-b7b8-5a95534d4750";
            public const string UMP = "c1534c5a-40e5-40e0-8139-194347756e55";
            public const string UZI = "c1534c5a-8d03-42de-93c7-f595534d4755";
            public const string Vector = "c1534c5a-4c47-428d-b5a5-b05747756e56";
        }
        
        /// <summary>
        /// All melee weapon barcodes
        /// </summary>
        public static class Melee
        {
            public static readonly List<string> All = new()
            {
                BarbedBat,
                BaseballBat,
                Baseball,
                Baton,
                Crowbar,
                ElectricGuitar,
                FryingPan,
                GolfClub,
                Hammer,
                HandHammer,
                LeadPipe,
                MorningStar,
                Shovel,
                Sledgehammer,
                SpikedClub,
                TrashcanLid,
                VikingShield,
                Warhammer,
                Wrench,
                AxeDouble,
                AxeFirefighter,
                AxeHorror,
                BastardSword,
                ChefKnife,
                Cleaver,
                CombatKnife,
                Dagger,
                HalfSword,
                Hatchet,
                IceAxe,
                Katana,
                Katar,
                Kunai,
                Machete,
                NorseAxe,
                Pickaxe,
                Spear,
                SwordClaymore
            };
            // Blunt
            public const string BarbedBat = "c1534c5a-e962-46dd-b1ef-f39542617262";
            public const string BaseballBat = "c1534c5a-6441-40aa-a070-909542617365";
            public const string Baseball = "c1534c5a-837c-43ca-b4b5-33d842617365";
            public const string Baton = "fa534c5a868247138f50c62e424c4144.Spawnable.Baton";
            public const string Crowbar = "c1534c5a-0c8a-4b82-9f8b-7a9543726f77";
            public const string ElectricGuitar = "SLZ.BONELAB.Content.Spawnable.ElectricGuitar";
            public const string FryingPan = "c1534c5a-d0e9-4d53-9218-e76446727969";
            public const string GolfClub = "c1534c5a-8597-4ffe-892e-b995476f6c66";
            public const string Hammer = "c1534c5a-11d0-4632-b36e-fa9548616d6d";
            public const string HandHammer = "c1534c5a-dfa6-466d-9ab7-bf9548616e64";
            public const string LeadPipe = "c1534c5a-f6f9-4c96-b88e-91d74c656164";
            public const string MorningStar = "c1534c5a-3d5c-4f9f-92fa-c24c4d656c65";
            public const string Shovel = "c1534c5a-5d31-488d-b5b3-aa1c53686f76";
            public const string Sledgehammer = "c1534c5a-1f5a-4993-bbc1-03be4d656c65";
            public const string SpikedClub = "c1534c5a-f5a3-4204-a199-a1e14d656c65";
            public const string TrashcanLid = "c1534c5a-d30c-4c18-9f5f-7cfe54726173";
            public const string VikingShield = "c1534c5a-6d15-47c7-9ad4-b04156696b69";
            public const string Warhammer = "c1534c5a-f6f3-46e2-aa51-67214d656c65";
            public const string Wrench = "c1534c5a-02e7-43cf-bc8d-26955772656e";
            // Blade
            public const string AxeDouble = "c1534c5a-6d6b-4414-a9f2-af034d656c65";
            public const string AxeFirefighter = "c1534c5a-4774-460f-a814-149541786546";
            public const string AxeHorror = "c1534c5a-0ba6-4876-be9c-216741786548";
            public const string BastardSword = "c1534c5a-d086-4e27-918d-ee9542617374";
            public const string ChefKnife = "c1534c5a-8036-440a-8830-b99543686566";
            public const string Cleaver = "c1534c5a-3481-4025-9d28-2e95436c6561";
            public const string CombatKnife = "c1534c5a-1fb8-477c-afbe-2a95436f6d62";
            public const string Dagger = "c1534c5a-d3fc-4987-a93d-d79544616767";
            public const string HalfSword = "c1534c5a-53ae-487e-956f-707148616c66";
            public const string Hatchet = "c1534c5a-d605-4f85-870d-f68848617463";
            public const string IceAxe = "SLZ.BONELAB.Content.Spawnable.MeleeIceAxe";
            public const string Katana = "c1534c5a-282b-4430-b009-58954b617461";
            public const string Katar = "c1534c5a-e606-4a82-878c-652f4b617461";
            public const string Kunai = "c1534c5a-f0d1-40b6-9f9b-c19544616767";
            public const string Machete = "c1534c5a-a767-4a58-b3ef-26064d616368";
            public const string NorseAxe = "c1534c5a-e75f-4ded-aa5a-a27b4178655f";
            public const string Pickaxe = "c1534c5a-f943-42a8-a994-6e955069636b";
            public const string Spear = "c1534c5a-a97f-4bff-b512-e44d53706561";
            public const string SwordClaymore = "c1534c5a-b59c-4790-9b09-499553776f72";
        }
        
        /// <summary>
        /// Contains extra possibly useful barcodes that wouldn't get their own category.
        /// </summary>
        public static class Misc
        {
            public static readonly List<string> All = new()
            {
                GoKart,
                Spawngun,
                Nimbusgun,
                Constrainer
            };
            public const string GoKart = "fa534c5a83ee4ec6bd641fec424c4142.Spawnable.VehicleGokart";
            public const string Spawngun = "c1534c5a-5747-42a2-bd08-ab3b47616467";
            public const string Nimbusgun = "c1534c5a-6b38-438a-a324-d7e147616467";
            public const string Constrainer = "c1534c5a-3813-49d6-a98c-f595436f6e73";
        }
    }
}