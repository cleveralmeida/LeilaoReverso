#pragma checksum "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54fa1f858036ca228f077aa2f2db085a5d0b13a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FornecedorLeilao_Resultado), @"mvc.1.0.view", @"/Views/FornecedorLeilao/Resultado.cshtml")]
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
#line 1 "D:\Copiar\LR - github\LR\Views\_ViewImports.cshtml"
using LR;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Copiar\LR - github\LR\Views\_ViewImports.cshtml"
using LR.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54fa1f858036ca228f077aa2f2db085a5d0b13a4", @"/Views/FornecedorLeilao/Resultado.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bfd7e4aad9a9e246da34990f406f742a95fce54c", @"/Views/_ViewImports.cshtml")]
    public class Views_FornecedorLeilao_Resultado : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LR.Models.FornecedorLeilao>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Questionario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Avaliacao", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
  
    ViewData["Title"] = "Resultado";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<h1>Resultado </h1>\r\n\r\n<p>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "54fa1f858036ca228f077aa2f2db085a5d0b13a44491", async() => {
                WriteLiteral("Após 4 participações, Clique aqui para avaliar o jogo");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</p>\r\n\r\n");
#nullable restore
#line 18 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
  
    if (SignInManager.IsSignedIn(User))
    {
        if (User.IsInRole("Fornecedor"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p style=\"color:red; \" id=\"mensagemInicial\">Aguarde o comprador criar novo LR. Você será convidado a participar...</p>\r\n");
#nullable restore
#line 24 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<p>Prioridade requerida pelo comprador: <b>");
#nullable restore
#line 28 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                      Write(ViewBag.OpcaoComprador);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n<p>Qtd Vencedores indicado pelo comprador: <b>");
#nullable restore
#line 29 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                         Write(ViewBag.QtdVencedores);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n<p>* Se ocorrer empate de lance, o fornecedor que tiver submetido primeiro terá prioridade</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 35 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
           Write(Html.DisplayNameFor(model => model.Classificacao));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 38 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
           Write(Html.DisplayNameFor(model => model.IdFornecedorNavigation.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                Nr. LR\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 44 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
           Write(Html.DisplayNameFor(model => model.Data));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 47 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
           Write(Html.DisplayNameFor(model => model.Lance));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                Vencedor?\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 56 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 60 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
               Write(Html.DisplayFor(modelItem => item.Classificacao));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 63 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
               Write(Html.DisplayFor(modelItem => item.IdFornecedorNavigation.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 66 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
               Write(Html.DisplayFor(modelItem => item.IdLeilao));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 69 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
               Write(Html.DisplayFor(modelItem => item.Data));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n");
#nullable restore
#line 72 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                     if (item.AtributoPrecoPrazo == 9)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 2,50 - 15 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }
                    else if (item.AtributoPrecoPrazo == 8)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 78 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 2,50 - 10 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 78 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }
                    else if (item.AtributoPrecoPrazo == 7)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 82 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 2,50 -  5 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 82 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }
                    else if (item.AtributoPrecoPrazo == 6)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 86 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 2,00 - 20 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 86 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }
                    else if (item.AtributoPrecoPrazo == 5)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 90 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 2,00 - 15 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 90 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }
                    else if (item.AtributoPrecoPrazo == 4)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 94 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 2,00 - 10 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 94 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }
                    else if (item.AtributoPrecoPrazo == 3)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 98 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 1,50 - 30 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 98 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }
                    else if (item.AtributoPrecoPrazo == 2)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 102 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 1,50 - 20 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 102 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }
                    else if (item.AtributoPrecoPrazo == 1)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 106 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("R$ 1,50 - 15 dias p/ entrega"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 106 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                                                 ;
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n                <td>\r\n");
#nullable restore
#line 110 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                     if (item.Vendedor == true)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 112 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("Sim"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 112 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                        
                    }
                    else
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 116 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                   Write(Html.Raw("Não"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 116 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
                                        
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n            </tr>\r\n");
#nullable restore
#line 120 "D:\Copiar\LR - github\LR\Views\FornecedorLeilao\Resultado.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<ApplicationUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<ApplicationUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LR.Models.FornecedorLeilao>> Html { get; private set; }
    }
}
#pragma warning restore 1591
