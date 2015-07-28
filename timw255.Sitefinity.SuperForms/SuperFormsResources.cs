using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace timw255.Sitefinity.SuperForms
{
    /// <summary>
    /// Localizable strings for the SuperForms module
    /// </summary>
    /// <remarks>
    /// You can use Sitefinity Thunder to edit this file.
    /// To do this, open the file's context menu and select Edit with Thunder.
    /// 
    /// If you wish to install this as a part of a custom module,
    /// add this to the module's Initialize method:
    /// App.WorkWith()
    ///     .Module(ModuleName)
    ///     .Initialize()
    ///         .Localization<SuperFormsResources>();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/developers-guide/how-to/how-to-import-events-from-facebook/creating-the-resources-class"/>
    [ObjectInfo("SuperFormsResources", ResourceClassId = "SuperFormsResources", Title = "SuperFormsResourcesTitle", TitlePlural = "SuperFormsResourcesTitlePlural", Description = "SuperFormsResourcesDescription")]
    public class SuperFormsResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="SuperFormsResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public SuperFormsResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="SuperFormsResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public SuperFormsResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// SuperForms Resources
        /// </summary>
        [ResourceEntry("SuperFormsResourcesTitle",
            Value = "SuperForms module labels",
            Description = "The title of this class.",
            LastModified = "2015/07/27")]
        public string SuperFormsResourcesTitle
        {
            get
            {
                return this["SuperFormsResourcesTitle"];
            }
        }

        /// <summary>
        /// SuperForms Resources Title plural
        /// </summary>
        [ResourceEntry("SuperFormsResourcesTitlePlural",
            Value = "SuperForms module labels",
            Description = "The title plural of this class.",
            LastModified = "2015/07/27")]
        public string SuperFormsResourcesTitlePlural
        {
            get
            {
                return this["SuperFormsResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// Contains localizable resources for SuperForms module.
        /// </summary>
        [ResourceEntry("SuperFormsResourcesDescription",
            Value = "Contains localizable resources for SuperForms module.",
            Description = "The description of this class.",
            LastModified = "2015/07/27")]
        public string SuperFormsResourcesDescription
        {
            get
            {
                return this["SuperFormsResourcesDescription"];
            }
        }

        /// <summary>
        /// 'Hide' action option text
        /// </summary>
        [ResourceEntry("HideActionOptionText", Value = "Hide", Description = "'Hide' action option text", LastModified = "2014/07/07")]
        public string HideActionOptionText
        {
            get { return this["HideActionOptionText"]; }
        }

        /// <summary>
        /// 'Show' action option text
        /// </summary>
        [ResourceEntry("ShowActionOptionText", Value = "Show", Description = "'Show' action option text", LastModified = "2014/07/07")]
        public string ShowActionOptionText
        {
            get { return this["HideActionOptionText"]; }
        }

        /// <summary>
        /// 'Any' bool option text
        /// </summary>
        [ResourceEntry("AnyBoolOptionText", Value = "Any", Description = "'Any' bool option text", LastModified = "2014/07/07")]
        public string AnyBoolOptionText
        {
            get { return this["AnyBoolOptionText"]; }
        }

        /// <summary>
        /// 'All' bool option text
        /// </summary>
        [ResourceEntry("AllBoolOptionText", Value = "All", Description = "'All' bool option text", LastModified = "2014/07/07")]
        public string AllBoolOptionText
        {
            get { return this["AllBoolOptionText"]; }
        }

        /// <summary>
        /// 'this field if' text
        /// </summary>
        [ResourceEntry("IfLabelText", Value = "this field if", Description = "'this field if' text", LastModified = "2014/07/07")]
        public string IfLabelText
        {
            get { return this["IfLabelText"]; }
        }

        /// <summary>
        /// 'of the following criteria match' text
        /// </summary>
        [ResourceEntry("OfTheFollowingCriteriaMatch", Value = "of the following criteria match.", Description = "'of the following criteria match' text", LastModified = "2014/07/07")]
        public string OfTheFollowingCriteriaMatch
        {
            get { return this["OfTheFollowingCriteriaMatch"]; }
        }

        /// <summary>
        /// Custom Logic view title
        /// </summary>
        [ResourceEntry("ViewTitle", Value = "Logic", Description = "Custom Logic view title", LastModified = "2014/07/07")]
        public string ViewTitle
        {
            get { return this["ViewTitle"]; }
        }

        /// <summary>
        /// 'Use conditional logic' label text
        /// </summary>
        [ResourceEntry("UseConditionalLogicLabel", Value = "Use conditional logic", Description = "'Use conditional logic' label text", LastModified = "2014/07/07")]
        public string UseConditionalLogicLabel
        {
            get { return this["UseConditionalLogicLabel"]; }
        }

        /// <summary>
        /// 'Add' button text
        /// </summary>
        [ResourceEntry("AddButtonText", Value = "Add", Description = "'Add' button text", LastModified = "2014/07/07")]
        public string AddButtonText
        {
            get { return this["AddButtonText"]; }
        }

        /// <summary>
        /// 'Remove' button text
        /// </summary>
        [ResourceEntry("RemoveButtonText", Value = "Remove", Description = "'Remove' button text", LastModified = "2014/07/07")]
        public string RemoveButtonText
        {
            get { return this["RemoveButtonText"]; }
        }
        #endregion
    }
}