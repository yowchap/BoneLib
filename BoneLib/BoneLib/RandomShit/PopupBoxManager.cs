﻿using BoneLib.MonoBehaviours;
using MelonLoader;

using System;
using System.Collections;
using System.Collections.Generic;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

using Il2CppSLZ.Marrow;

namespace BoneLib.RandomShit
{
    public static class PopupBoxManager
    {
        internal static List<string> adMessages { get; private set; } = new List<string>()
        {
            "BONETOME.COM FOR COOL NEW MODS",
            "GET THE CHUNGUS GUN IT DOES CHUNGUS STUFF \n\nhttps://bonetome.com/boneworks/code/87/",
            "MAKE THE GAME HARDER AND POWERED BY PAINKILLERS \n\nhttps://bonetome.com/boneworks/code/147/",
            "FEELING SNACKY? DOWNLOAD THE FOOD FRAMEWORK TODAY! \n\nhttps://bonetome.com/boneworks/code/185/",
            "i've never used this but apparently it's good? \n\nhttps://bonetome.com/boneworks/code/323/",
            "WHAT DOES IT DO? IT MODS. \n\nhttps://bonetome.com/boneworks/code/498/",
            "No memes here, this mod is actually so fun. \n\nhttps://bonetome.com/boneworks/code/661/",
            "DOWNLOAD NOW TO MAKE CHROMIUM VERY ANGRY!!!!1! \n\nhttps://bonetome.com/boneworks/simple/505/",
            "BEST MOD EVER v2 \n\nhttps://bonetome.com/boneworks/simple/719/",
            "GET YOUR GUNS AT BONETOME.COM TODAY",
            "THIS INTERRUPTION TO YOUR GAME IS SPONSORED BY TOMME150",
            "HAHAHA LASER GUN GO BRRRRR \n\nhttps://bonetome.com/boneworks/weapon/823/",
            "I don't get paid enough for this, and neither does TabloidA. \n\nSUPPORT THE BONETOME ON PATREON TODAY! \n\nhttps://www.patreon.com/thebonetome",
            "kinda pog ngl \n\nhttps://bonetome.com/boneworks/weapon/891/ https://bonetome.com/boneworks/weapon/894/",
            "BEST. PISTOL. EVER. \n\nhttps://bonetome.com/boneworks/weapon/798/",
            "THE THERMAL KATANA \nBRIGHTER THAN MY FUTURE. \n\nhttps://bonetome.com/boneworks/weapon/858/",
            "uwu uwu owo uwu uwu \n\nhttps://bonetome.com/boneworks/weapon/771/",
            "OMG GUYS IT'S A SHOTGUN!! LEAK!!1!!!!! \n\nhttps://bonetome.com/boneworks/weapon/413/",
            "THE BEST GUN YOU WILL EVER USE \n\nhttps://bonetome.com/boneworks/weapon/99/",
            "Just a friendly little fellow to keep you company while you play. \n\nhttps://bonetome.com/boneworks/item/114/",
            "guys this thing is really big lol \n\nhttps://bonetome.com/boneworks/weapon/361/",
            "Chap is Gay MINIGUN also tabloid no one likes you \n\nhttps://bonetome.com/boneworks/weapon/107/",
            "my motivation is gone and my life is spiraling into the abyss, but at least i have these honey badgers! download today! \n\nhttps://bonetome.com/boneworks/weapon/515/",
            "the constant stream of void energy being pumped into my brain is killing me, i can feel the names of my family growing fainter in my mind, but at least i have these honey badgers! download today! \n\nhttps://bonetome.com/boneworks/weapon/515/",
            "Ever wanted to have a thermal scope on a dope ass gun with laser beams? No? Well here it is anyways: \n\nhttps://bonetome.com/boneworks/weapon/918/",
            "Have bad aim? Download the SMART PISTOL! \n\nhttps://bonetome.com/boneworks/weapon/447/",
            "Half Life: Alyx pistol, for when you're too poor to afford the actual game. \n\nhttps://bonetome.com/boneworks/weapon/348/",
            "AMONG US PET POGGERS https://bonetome.com/boneworks/npc/464/",
            "Happy friendly NPCs for a good time :) \n\nhttps://bonetome.com/boneworks/npc/222/",
            "BONETOME IS GOOD \n\n\n\nBOTTOM TEXT",
            "there's a big purple thing and it looks kinda cool i guess \n\nhttps://bonetome.com/boneworks/map/520/",
            "void bad, you need to escape it. \n\nhttps://bonetome.com/boneworks/map/893/",
            "parkour but with melons? \nidk i never played it. \n\nhttps://bonetome.com/boneworks/map/899/",
            "rip bonetome.com :(",
            "Solace Station is kinda cool i guess \n\nhttps://boneworks.thunderstore.io/package/TabloidA/Solace_Station/",
            "hotel? chicago.",
            "GUYS IM FROM THE FUTURE! \nPROJECT 4 IS CALLED BONELAB!",
            "You should check out MrPotatoMod by Lucy Cola on thunderstore.io \nThat would be funny I think",
            "Nexus Mods is mid!",
            "Oops!... I corrupted the memory profile again",
            "haha you can't grab this one",
            "which one is your tabloid tabloid \nwhat",
            "Tabloid \"What, you think they're gonna have a mario-kart level? Yeah right\" A",
            "Parzival x Chromium: My Immortal",
            "The Fog is Coming"
        };

        internal static List<string> androidAdMessages { get; private set; } = new List<string>()
        {
            "'Anything is possible with Lava Gang!' but apparently not supporting Quest",
            "SMH. not everone has a pec... Port this to quest.",
            "F*ck you. Quest owners are so supportive and yet motherfrickers like you just ignore a large number of users.",
            "Is therea Qest version?",
            "Upload this for Quest. Build it with the Quest Sdk and uploadpl ease",
            "gib qust suport",
            "waitin gfor the quest version!",
            "I was trying to install this to my qujest 2 but it wasn't. Why?",
            "Will you please work on porting it to quest 2 because I don't have a pc",
            "can u pls port this to quest? :) this would go great on it ^-^",
            "hi I was wondering if you would be able to add this to Quest? thanks.",
            "I was the Quest 2 but then the Mod was not the download when i clic kthe cdownload mod.",
            "can U add it on quest 2?",
            "can you make android copy please, thanks.",
            "can you port this to Quest?",
            "OMG the Pack for Quest button is RIGHT THERE, its NOT that hard to CLICK it and UPLOAD IT.",
            "Does this work on Quest 2?",
            "can you make it compatible withquest?",
            "do questr version? pls.",
            "can u port this to quest 2 pls",
            "port 2 quedt?",
            "can I mod this Into my Quest?",
            "omfg can you please not make mods that are PC only? not everyone can afford one.",
            "There are no QEst but I have. Why are you?",
        };

        private static GameObject basePopup;

        // private const string API_ALL_DOGS = "https://dog.ceo/api/breeds/image/random"; different format
        // may be able to use https://random.dog/woof.json too, but also diff format
        private const string API_SHIBE = "https://dog.ceo/api/breed/shiba/images/random";

        private const string API_CAT = "https://api.sefinek.net/api/v2/random/animal/cat";
        private const string API_BIRD = "https://api.sefinek.net/api/v2/random/animal/bird";

        public static GameObject CreateNewPopupBox()
        {
            string adText = adMessages[Random.Range(0, adMessages.Count)];
            if (HelperMethods.IsAndroid() && Random.Range(1, 11) != 5)
                adText = $"\"{androidAdMessages[Random.Range(0, androidAdMessages.Count)]}\" -fordkiller2014 (you.)";

            return CreateNewPopupBox(adText);
        }

        public static GameObject CreateNewPopupBox(string adText)
        {
            if (basePopup == null)
            {
                ModConsole.Error("PopupBoxManager: Base popup is null");
                return null;
            }

            GameObject newPopup = GameObject.Instantiate(basePopup);
            TextMeshPro Il2CppTMPro = newPopup.GetComponentInChildren<TextMeshPro>();

            Il2CppTMPro.text = adText;
            newPopup.SetActive(true);

            Il2CppTMPro.enableAutoSizing = true;
            Il2CppTMPro.fontSizeMin = 0.5f;
            Il2CppTMPro.fontSizeMax = 4;
            Il2CppTMPro.alignment = TextAlignmentOptions.Center;
            Il2CppTMPro.enableWordWrapping = true;
            Il2CppTMPro.alignment = TextAlignmentOptions.Center; // For some reason this has to be set here as well ¯\_(ツ)_/¯

            if (adText == "haha you can't grab this one")
                newPopup.GetComponentInChildren<BoxGrip>().enabled = false;

            newPopup.AddComponent<PopupBox>();

            // Place the popup in front of the player
            newPopup.transform.position = Player.Head.position + Player.Head.forward * 2;
            newPopup.transform.rotation = Quaternion.LookRotation(newPopup.transform.position - Player.Head.position);

            return newPopup;
        }

        public static GameObject CreateNewImagePopup(byte[] imageBytes)
        {
            // Load the byte array into a texture
            Texture2D texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, imageBytes);

            // Instantiate the ad gameobject and disable the text
            GameObject newPopup = GameObject.Instantiate(basePopup);
            newPopup.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);

            // Assign the picture to the material
            MeshRenderer renderer = newPopup.GetComponentInChildren<MeshRenderer>();
            Material mat = renderer.material;
            //mat.mainTexture = texture;
            mat.SetTexture("_BaseMap", texture);
            mat.color = Color.white;

            // Scale the mesh and grip so the pic isn't stretched
            Vector3 curScale = renderer.transform.localScale;
            Vector3 newScale = new Vector3((float)texture.width / texture.height * curScale.y, curScale.y, curScale.z);
            renderer.transform.localScale = newScale;
            renderer.transform.Rotate(renderer.transform.up, 180);

            foreach (BoxCollider col in newPopup.GetComponentsInChildren<BoxCollider>())
                col.size = newScale;

            newPopup.SetActive(true);

            // Place the popup in front of the player
            newPopup.transform.position = Player.Head.position + Player.Head.forward * 2;
            newPopup.transform.rotation = Quaternion.LookRotation(newPopup.transform.position - Player.Head.position);

            return newPopup;
        }

        /// <summary>
        /// Creates an image popup with an image of a Shibe Inu.
        /// </summary>
        /// <param name="returnCallback">A callback that will be executed when the popup spawns, or <see langword="null"/> if there was an error.</param>
        public static void CreateNewShibePopup(Action<GameObject> returnCallback = null)
        {
            IEnumerator coroutine = SpawnImagePopupFromApi(API_SHIBE, returnCallback);
            MelonCoroutines.Start(coroutine);
        }

        public static void CreateNewCatPopup(Action<GameObject> returnCallback = null)
        {
            IEnumerator coroutine = SpawnImagePopupFromApi(API_CAT, returnCallback);
            MelonCoroutines.Start(coroutine);
        }

        public static void CreateNewBirdPopup(Action<GameObject> returnCallback = null)
        {
            IEnumerator coroutine = SpawnImagePopupFromApi(API_BIRD, returnCallback);
            MelonCoroutines.Start(coroutine);
        }

        // only works with the shibe.online api (or anything else that returns a json array)
        private static IEnumerator SpawnImagePopupFromApi(string apiBase, Action<GameObject> callback)
        {
            UnityWebRequest urlReq = UnityWebRequest.Get(apiBase);
            yield return urlReq.BeginWebRequest();
            while (urlReq.result == UnityWebRequest.Result.InProgress)
            {
                ModConsole.Msg($"Initial API request progress={urlReq.downloadProgress}", LoggingMode.DEBUG);
                // todo: remove
                yield return null;
            }

            switch (urlReq.result)
            {
                case UnityWebRequest.Result.Success:
                    break;

                default:
                    try
                    {
                        callback?.Invoke(null);
                    }
                    finally
                    {
                        ModConsole.Error("Exception whilst invoking image popup callback with null to signify error.", LoggingMode.NORMAL);
                        ModConsole.Msg($"Initial API web request failed, status = {urlReq.result}", LoggingMode.DEBUG);
                    }
                    yield break;
            }

            string jsonUrls = urlReq.downloadHandler.text; // return value is something like ["https://cdn.shibe.online/shibes/8f0792fcac8df87a5d2953031a837a2939fda430.jpg"]
            // Unfortunately the response is not ["link"] anymore, because the domain got sent into ww3 hell and of the other numbers. Both of the new endpoints use JSON and the "message" parameter to link to the image
            string imageUrl = string.Empty;
            try
            {
                var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonUrls);

                if (!deserialized.ContainsKey("message"))
                    throw new KeyNotFoundException("Could not find 'message' property in returned JSON");
                imageUrl = deserialized["message"] as string; // Expecting this to be a string
            }
            catch (Exception ex)
            {
                try
                {
                    callback?.Invoke(null);
                }
                finally
                {
                    ModConsole.Error("Exception whilst invoking image popup callback with null to signify error.", LoggingMode.NORMAL);
                    ModConsole.Msg($"Failed to deserialize the response JSON, exception:\n{ex}", LoggingMode.DEBUG);
                }
            }

            UnityWebRequest imageReq = UnityWebRequest.Get(imageUrl);
            imageReq.BeginWebRequest();
            while (imageReq.result == UnityWebRequest.Result.InProgress)
            {
                ModConsole.Msg($"Image request progress={urlReq.downloadProgress}", LoggingMode.DEBUG);
                // todo: remove
                yield return null;
            }

            switch (imageReq.result)
            {
                case UnityWebRequest.Result.Success:
                    break;

                default:
                    try
                    {
                        callback?.Invoke(null);
                    }
                    finally
                    {
                        ModConsole.Error("Exception whilst invoking image popup callback with null to signify error.", LoggingMode.NORMAL);
                        ModConsole.Msg($"Image web request failed, status = {urlReq.result}", LoggingMode.DEBUG);
                    }
                    yield break;
            }

            // dont inline otherwise createnewimagepopup wont be called
            GameObject createdPopup = CreateNewImagePopup(imageReq.downloadHandler.data);
            callback?.Invoke(createdPopup);
        }

        internal static void StartCoroutines()
        {
            MelonCoroutines.Start(CoSpawnAds());
        }

        internal static IEnumerator CoSpawnAds()
        {
            //if (!Preferences.autoSpawnAds)
            //    yield break;

            //yield return new WaitForSeconds(Random.Range(Preferences.timeBetweenAds.value.x, Preferences.timeBetweenAds.value.y));

            while (basePopup == null)
                yield return new WaitForSeconds(5f);

            GameObject newAd = CreateNewPopupBox();
            newAd.transform.position = Player.Head.position + Player.Head.forward * 2;
            newAd.transform.rotation = Quaternion.LookRotation(newAd.transform.position - Player.Head.position);

            MelonCoroutines.Start(CoSpawnAds());
        }

        /// <summary>
        /// So basically when I was making this I wasn't using any asset bundles yet and I wanted to keep it that way for whatever reason, so instead of just making an asset bundle with the prefab in it like a normal person, I somehow convinced myself that it was a perfectly sane idea to instead manually create the gameobject with a grip component and text and all that fun stuff through code, and that's how this absolute monstrosity of a function came into existence :)
        /// </summary>
        internal static void CreateBaseAd()
        {
            #region Resources

            AudioClip[] clips = Resources.FindObjectsOfTypeAll<AudioClip>();
            List<AudioClip> sounds = new List<AudioClip>();
            foreach (AudioClip clip in clips)
                if (clip.name.Contains("ImpactSoft_SwordBroad"))
                    sounds.Add(clip);

            HandPose sandwichGrip = null;
            HandPose edgeGrip = null;
            HandPose cornerGrip = null;
            HandPose faceGrip = null;
            HandPose[] poses = Resources.FindObjectsOfTypeAll<HandPose>();
            foreach (HandPose p in poses)
            {
                if (p.name == "BoxSandwichGrip")
                    sandwichGrip = p;
                else if (p.name == "BoxEdgeGrip")
                    edgeGrip = p;
                else if (p.name == "BoxCornerGrip")
                    cornerGrip = p;
                else if (p.name == "BoxFaceGrip")
                    faceGrip = p;
            }

            #endregion Resources

            #region Base object

            basePopup = new GameObject($"Ad Base");
            basePopup.SetActive(false);

            Rigidbody rb = basePopup.AddComponent<Rigidbody>();
            rb.mass = 8;
            rb.drag = 0.15f;
            rb.angularDrag = 0.15f;

            ImpactProperties impactProperties = basePopup.AddComponent<ImpactProperties>();
            //impactProperties.material = ImpactPropertiesVariables.Material.PureMetal;
            impactProperties.decalType = ImpactProperties.DecalType.Collider;
            //impactProperties.PenetrationResistance = 0.9f;
            impactProperties.megaPascalModifier = 1;
            //impactProperties.FireResistance = 100;

            ImpactSFX sfx = basePopup.AddComponent<ImpactSFX>();
            sfx.impactSoft = sounds.ToArray();
            sfx.impactHard = sounds.ToArray();
            //sfx.outputMixer = Audio.sfxMixer; // TODO: Audio class
            sfx.pitchMod = 1;
            sfx.bluntDamageMult = 1;
            sfx.minVelocity = 0.4f;
            sfx.velocityClipSplit = 4;
            sfx.jointBreakVolume = 1;

            InteractableHost host = basePopup.AddComponent<InteractableHost>();
            //host.HasRigidbody = true;

            #endregion Base object

            #region Mesh object

            GameObject mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
            mesh.name = "Mesh";
            mesh.transform.parent = basePopup.transform;
            mesh.transform.localPosition = Vector3.zero;
            mesh.transform.localScale = new Vector3(2f, 1f, 0.02f);
            mesh.GetComponent<MeshRenderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit (PBR Workflow)");
            mesh.GetComponent<MeshRenderer>().material.color = new Color(0.1509434f, 0.1509434f, 0.1509434f);

            #endregion Mesh object

            #region Grip object

            GameObject grip = new GameObject("Grip");
            grip.transform.parent = basePopup.transform;
            grip.layer = LayerMask.NameToLayer("Interactable");

            BoxCollider col = grip.AddComponent<BoxCollider>();
            col.size = mesh.transform.localScale;
            mesh.GetComponent<BoxCollider>().enabled = false;

            BoxGrip boxGrip = grip.AddComponent<BoxGrip>();
            boxGrip.isThrowable = true;
            boxGrip.handleAmplifyCurve = new AnimationCurve(new Keyframe[] { new Keyframe(-180, 0), new Keyframe(180, 0) });
            boxGrip.gripOptions = InteractionOptions.MultipleHands;
            boxGrip.priority = 1;
            boxGrip.minBreakForce = float.PositiveInfinity;
            boxGrip.maxBreakForce = float.PositiveInfinity;
            boxGrip.defaultGripDistance = float.PositiveInfinity;
            boxGrip.gripDistanceSqr = float.PositiveInfinity;
            boxGrip.rotationLimit = 180;
            boxGrip.rotationPriorityBuffer = 20;

            boxGrip.sandwichSize = 0.12f;
            boxGrip.edgePadding = 0.1f;
            boxGrip.sandwichHandPose = sandwichGrip;
            boxGrip.canBeSandwichedGrabbed = true;

            boxGrip.edgeHandPose = edgeGrip;
            boxGrip.edgeHandPoseRadius = 0.05f;
            boxGrip.canBeEdgeGrabbed = true;

            boxGrip.cornerHandPose = cornerGrip;
            boxGrip.cornerHandPoseRadius = 0.05f;
            boxGrip.canBeCornerGrabbed = true;

            boxGrip.faceHandPose = faceGrip;
            boxGrip.faceHandPoseRadius = 1;
            boxGrip.canBeFaceGrabbed = true;

            boxGrip.enabledCorners = (BoxGrip.Corners)(-1);
            boxGrip.enabledEdges = (BoxGrip.Edges)(-1);
            boxGrip.enabledFaces = (BoxGrip.Faces)(-1);

            boxGrip._boxCollider = col;

            #endregion Grip object

            #region Destructable object

            ObjectDestructible destructable = basePopup.AddComponent<ObjectDestructible>();
            destructable.damageFromImpact = true;
            //destructable.blasterType = StressLevelZero.Pool.PoolSpawner.BlasterType.Sparks; // TODO: destruction effects
            destructable.blasterScale = Vector3.one * 3;
            destructable.maxHealth = 30;
            destructable.reqHitCount = 1;
            destructable.perBloodied = 0.1f;
            destructable.explosiveForceOnDestruct = 1;
            destructable.damageFromAttack = true;
            destructable.attackMod = 2;
            destructable.modTypeDamage = 2;
            destructable.modImpact = 1;
            destructable.thrImpact = 3;
            destructable.feetDamageMult = 0.1f;
            destructable._impactSfx = sfx;

            #endregion Destructable object

            #region Text object

            GameObject text = new GameObject("Text");
            text.transform.parent = basePopup.transform;

            TextMeshPro tmpro = text.AddComponent<TextMeshPro>();
            tmpro.enableAutoSizing = true;
            tmpro.fontSizeMin = 0.5f;
            tmpro.fontSizeMax = 4;
            tmpro.alignment = TextAlignmentOptions.Center;
            tmpro.enableWordWrapping = true;

            RectTransform rectTransform = text.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(2, 1);
            rectTransform.localPosition = new Vector3(0, 0, -0.015f);

            #endregion Text object
        }
    }
}