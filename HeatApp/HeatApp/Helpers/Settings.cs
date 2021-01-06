using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace HeatApp.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Variables
        private const string TokenKey = "token_key";
        private static readonly string TokenDefault = string.Empty;

        private const string FullNameKey = "fullname_key";
        private static readonly string FullNameDefault = string.Empty;

        private const string EmailKey = "email_key";
        private static readonly string EmailDefault = string.Empty;

        private const string PhoneKey = "phone_key";
        private static readonly string PhoneDefault = string.Empty;

        private const string SexKey = "sex_key";
        private static readonly string SexDefault = string.Empty;
        #endregion

        #region Propertties
        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault(TokenKey, TokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(TokenKey, value);
            }
        }

        public static string FullName
        {
            get
            {
                return AppSettings.GetValueOrDefault(FullNameKey, FullNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FullNameKey, value);
            }
        }

        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault(EmailKey, EmailDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(EmailKey, value);
            }
        }

        public static string Phone
        {
            get
            {
                return AppSettings.GetValueOrDefault(PhoneKey, PhoneDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PhoneKey, value);
            }
        }

        public static string Sex
        {
            get
            {
                return AppSettings.GetValueOrDefault(SexKey, SexDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SexKey, value);
            }
        }
        #endregion
    }
}