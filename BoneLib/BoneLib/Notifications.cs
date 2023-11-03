using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using BoneLib.AssetLoader;
using UnityEngine;

namespace BoneLib.Notifications;
    
    // most of this comes from Fusion, with permission to use it here

    public static class NotifAssets
    {
        public static Texture2D Information;
        public static Texture2D Warning;
        public static Texture2D Error;
        public static Texture2D Success;

        public static void SetupBundles()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string androidPath = "BoneLib.Resources.notifications.android.bundle";
            const string windowsPath = "BoneLib.Resources.notifications.bundle";
            if (HelperMethods.IsAndroid())
            {
                var assetBundle = EmbeddedBundle.LoadFromAssembly(assembly, androidPath);
                Information = assetBundle.LoadPersistentAsset<Texture2D>("Assets/information.png");
                Warning = assetBundle.LoadPersistentAsset<Texture2D>("Assets/warning.png");
                Error = assetBundle.LoadPersistentAsset<Texture2D>("Assets/error.png");
                Success = assetBundle.LoadPersistentAsset<Texture2D>("Assets/success.png");
            }
            else
            {
                var assetBundle = EmbeddedBundle.LoadFromAssembly(assembly, windowsPath);
                Information = assetBundle.LoadPersistentAsset<Texture2D>("Assets/information.png");
                Warning = assetBundle.LoadPersistentAsset<Texture2D>("Assets/warning.png");
                Error = assetBundle.LoadPersistentAsset<Texture2D>("Assets/error.png");
                Success = assetBundle.LoadPersistentAsset<Texture2D>("Assets/success.png");
            }
        }
    }

    /// <summary>
    /// The basic types of notifications that can be sent.
    /// </summary>
    public enum NotificationType {
        /// <summary>
        /// Used to inform the user.
        /// </summary>
        Information = 0,

        /// <summary>
        /// Used when the user should be notified of a potential issue.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// Used when the user or program attempts a task and it fails.
        /// </summary>
        Error = 2,

        /// <summary>
        /// Used when the user or program performs a task and it suceeds.
        /// </summary>
        Success = 3,
        
        /// <summary>
        /// Lets the user specify an customIcon
        /// </summary>
        CustomIcon = 4
    }


    /// <summary>
    /// The class used to supply text in a notification.
    /// </summary>
    public struct NotificationText {
        /// <summary>
        /// The text.
        /// </summary>
        public string Text;

        /// <summary>
        /// The color of the text.
        /// </summary>
        public Color Color;

        /// <summary>
        /// Should rich text be allowed?
        /// </summary>
        public bool RichText;

        public NotificationText(string text) : this(text, Color.white) { }

        public NotificationText(string text, Color color, bool richText = false) {
            if (!richText) {
                text = text.RemoveRichText();
            }

            this.Text = text;
            this.Color = color;
            this.RichText = richText;
        }

        public static implicit operator NotificationText(string text) {
            return new NotificationText(text);
        }
    }

    public static class Text
    {
        public static string RemoveRichText(this string str) {
            Regex rich = new(@"<[^>]*>");
            string plainText = str;

            if (rich.IsMatch(plainText)) {
                plainText = rich.Replace(plainText, string.Empty);
            }

            return plainText;
        }
    }


    /// <summary>
    /// The class used for sending notifications to the player. No constructors, provide your own information.
    /// </summary>
    public class Notification
    {
        // Text settings
        /// <summary>
        /// The title of the notification.
        /// </summary>
        public NotificationText Title;

        /// <summary>
        /// The main body of the notification.
        /// </summary>
        public NotificationText Message;

        // Popup settings
        /// <summary>
        /// Should the title be used on the popup? (If false, it shows "New Notification".)
        /// </summary>
        public bool ShowTitleOnPopup = false;

        /// <summary>
        /// Should this notification popup?
        /// </summary>
        public bool IsPopup = true;

        /// <summary>
        /// How long the notification will be up.
        /// </summary>
        public float PopupLength = 2f;

        /// <summary>
        /// The type of notification this is.
        /// </summary>
        public NotificationType Type = NotificationType.Information;
        
        /// <summary>
        /// The customIcon to use. Only used if <see cref="Type"/> is <see cref="NotificationType.CustomIcon"/>.
        /// </summary>
        public Texture2D CustomIcon = null;
    }

    public static class Notifier
    {
        public const float DefaultDuration = 2f;

        private static readonly Queue<Notification> QueuedNotifications = new();
        public static ulong NotificationNumber = 0;

        private static bool _hasEnabledTutorialRig = false;

        
        /// <summary>
        /// Sends a notification to the player.
        /// </summary>
        /// <param name="notification">The notification</param>
        public static void Send(Notification notification) {
            QueueNotification(notification);
        }

        private static void QueueNotification(Notification notification) { 
            QueuedNotifications.Enqueue(notification);
        }

        private static void DequeueNotification() {
            var notification = QueuedNotifications.Dequeue();

            // Show to the player
            var rm = Player.rigManager;

            if (notification.IsPopup && rm != null) {
                var tutorialRig = rm.tutorialRig;
                var headTitles = tutorialRig.headTitles;

                EnableTutorialRig();

                string incomingTitle = "New Notification";

                if (notification.ShowTitleOnPopup)
                    incomingTitle = notification.Title.Text;

                string incomingSubTitle = notification.Message.Text;

                Texture2D incomingTexture = notification.Type switch
                {
                    NotificationType.Warning => NotifAssets.Warning,
                    NotificationType.Error => NotifAssets.Error,
                    NotificationType.Success => NotifAssets.Success,
                    NotificationType.Information => NotifAssets.Information,
                    NotificationType.CustomIcon => notification.CustomIcon,
                    _ => NotifAssets.Information
                };
                Sprite incomingSprite = Sprite.Create(incomingTexture, new Rect(0.0f, 0.0f, incomingTexture.width, incomingTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

                float holdTime = notification.PopupLength;

                headTitles.CUSTOMDISPLAY(incomingTitle, incomingSubTitle, incomingSprite, holdTime);
                headTitles.sr_element.sprite = incomingSprite;
            }
        }

        private static void EnableTutorialRig() {
            var rm = Player.rigManager;

            if (rm != null) {
                var tutorialRig = rm.tutorialRig;
                var headTitles = tutorialRig.headTitles;

                // Make sure the tutorial rig/head titles are enabled
                tutorialRig.gameObject.SetActive(true);
                headTitles.gameObject.SetActive(true);
            }
        }

        private static bool IsPlayingNotification() {
            var rm = Player.rigManager;

            if (rm != null) {
                var tutorialRig = rm.tutorialRig;
                var headTitles = tutorialRig.headTitles;

                return headTitles.headFollower.gameObject.activeInHierarchy;
            }

            return false;
        }

        

        internal static void OnUpdate() {
            // Make sure we aren't loading so we can dequeue existing notifications
            if (QueuedNotifications.Count > 0 && !HelperMethods.IsLoading() && Player.rigManager != null) {
                // Enable the tutorial rig a frame before showing notifs
                if (!_hasEnabledTutorialRig) {
                    EnableTutorialRig();
                    _hasEnabledTutorialRig = true;
                }
                else {
                    // Dequeue notifications
                    if (QueuedNotifications.Count > 0 && !IsPlayingNotification()) {
                        DequeueNotification();
                    }
                }
            }
            else
                _hasEnabledTutorialRig = false;
        }
    }