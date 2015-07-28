using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using timw255.Sitefinity.SuperForms;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("timw255.Sitefinity.SuperForms")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("timw255.Sitefinity.SuperForms")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Registers SuperFormsInstaller.PreApplicationStart() to be executed prior to the application start
[assembly: PreApplicationStartMethod(typeof(SuperFormsInstaller), "PreApplicationStart")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("265f525d-e469-4a59-9a6b-99270ee51aa3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: WebResource("timw255.Sitefinity.SuperForms.Resources.logicalForms.js", "application/x-javascript")]
[assembly: WebResource("timw255.Sitefinity.SuperForms.Resources.logicalForms.min.js", "application/x-javascript")]

[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.min.js", "application/x-javascript")]

[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ConditionalLogicView.min.js", "application/x-javascript")]
[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView.min.js", "application/x-javascript")]
[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView.min.js", "application/x-javascript")]

[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormCheckboxes.min.js", "application/x-javascript")]
[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList.min.js", "application/x-javascript")]
[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText.min.js", "application/x-javascript")]
[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormMultipleChoice.min.js", "application/x-javascript")]
[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormParagraphTextBox.min.js", "application/x-javascript")]
[assembly: WebResource("timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormTextBox.min.js", "application/x-javascript")]