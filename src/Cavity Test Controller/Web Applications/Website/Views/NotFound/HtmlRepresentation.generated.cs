﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cavity.Views.NotFound
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.2.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/NotFound/HtmlRepresentation.cshtml")]
    public class HtmlRepresentation : System.Web.Mvc.WebViewPage<dynamic>
    {
        public HtmlRepresentation()
        {
        }
        public override void Execute()
        {

            
            #line 1 "..\..\Views\NotFound\HtmlRepresentation.cshtml"
  
    ViewBag.Title = "404";


            
            #line default
            #line hidden
WriteLiteral("<p id=\"message\">");


            
            #line 4 "..\..\Views\NotFound\HtmlRepresentation.cshtml"
           Write(ViewBag.Message);

            
            #line default
            #line hidden
WriteLiteral("</p>");


        }
    }
}
#pragma warning restore 1591
