#pragma checksum "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\BlogPostComments\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5c8feb679d1f245a926caaa321729e5fda3f441a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_BlogPostComments_Details), @"mvc.1.0.view", @"/Areas/Admin/Views/BlogPostComments/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 3 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.CourseCategoriesModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCodee.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.TeachersModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.PlanAndPricingModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.LastNewsAndEventsModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.EventSpeakersModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.EventGalleryModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.ContactPostModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.BlogPostModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.AccountModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.Models.FormModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.Models.Entities.Membership;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.Models.DataContext;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.IxtisasModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.GrupModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.TedrisFennModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.LessonsModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using DiplomLayihe.AppCode.Modules.ExamsModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\_ViewImports.cshtml"
using Resources;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5c8feb679d1f245a926caaa321729e5fda3f441a", @"/Areas/Admin/Views/BlogPostComments/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6357524f0f6ce52bc6c69b61acc99d5b89dbe68c", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_BlogPostComments_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BlogPostComments>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-grd-inverse"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\BlogPostComments\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>Blog Post Comment</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 16 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\BlogPostComments\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 19 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\BlogPostComments\Details.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 22 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\BlogPostComments\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 25 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\BlogPostComments\Details.cshtml"
       Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 28 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\BlogPostComments\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Comment));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 31 "C:\Users\ASUS\source\repos\DiplomLayihe\DiplomLayihe\Areas\Admin\Views\BlogPostComments\Details.cshtml"
       Write(Html.DisplayFor(model => model.Comment));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5c8feb679d1f245a926caaa321729e5fda3f441a10997", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BlogPostComments> Html { get; private set; }
    }
}
#pragma warning restore 1591