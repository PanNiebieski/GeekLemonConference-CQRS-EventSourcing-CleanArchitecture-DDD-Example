﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeekLemonConference.Persistence.Dapper.SQLite {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SqlQueries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SqlQueries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GeekLemonConference.Persistence.Dapper.SQLite.SqlQueries", typeof(SqlQueries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO CallForSpeakes
        ///(
        ///&quot;Number&quot;,
        ///&quot;Status&quot;,
        ///&quot;Speaker_Name_First&quot;,
        ///&quot;Speaker_Name_Last&quot;,
        ///&quot;Speaker_Adress_Country&quot;,
        ///&quot;Speaker_Adress_ZipCode&quot;,
        ///&quot;Speaker_Adress_City&quot;,
        ///&quot;Speaker_Adress_Street&quot;,
        ///&quot;Speaker_Websites_Facebook&quot;,
        ///&quot;Speaker_Websites_Twitter&quot;,
        ///&quot;Speaker_Websites_Instagram&quot;,
        ///&quot;Speaker_Websites_LinkedIn&quot;,
        ///&quot;Speaker_Websites_TikTok&quot;,
        ///&quot;Speaker_Websites_Youtube&quot;,
        ///&quot;Speaker_Websites_FanPageOnFacebook&quot;,
        ///&quot;Speaker_Websites_GitHub&quot;,
        ///&quot;Speaker_Websites_Blog&quot;,
        ///&quot;Speaker_BIO&quot;,
        ///&quot;Speaker_Contact_Email&quot;, [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CallForSpeechInsert {
            get {
                return ResourceManager.GetString("CallForSpeechInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT j.Login,j.Password,j.BirthDate,j.Name_First, j.Name_Last,
        ///                j.Email_ForeConference, j.Email_ForSpeakers, j.Phone_ForConference,
        ///                j.Phone_ForSpekers,j.CategoryId FROM Judges AS j
        ///                INNER JOIN Categories as C ON j.CategoryId = C.Id.
        /// </summary>
        internal static string GetByIdAsyncJudge {
            get {
                return ResourceManager.GetString("GetByIdAsyncJudge", resourceCulture);
            }
        }
    }
}