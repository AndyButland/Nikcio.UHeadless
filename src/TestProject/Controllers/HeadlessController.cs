﻿using Microsoft.AspNetCore.Mvc;
using Nikcio.Umbraco.Headless.Core.Commands.Sites.Pages;
using Nikcio.Umbraco.Headless.Core.Commands.Sites.Pages.PageData;
using Nikcio.Umbraco.Headless.Core.Mappers.Sites.Pages;
using Nikcio.Umbraco.Headless.Core.Mappers.Sites.Pages.PageData;
using Nikcio.Umbraco.Headless.Core.Models.SiteModels.PageModels.PropertyModels;
using Nikcio.Umbraco.Headless.Core.Models.SiteModels.SiteData;
using Nikcio.Umbraco.Headless.Core.Services.Headless;
using System;
using TestProject.umbraco.models;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace TestProject.Controllers
{
    public class HeadlessController : UmbracoApiController
    {
        private readonly IHeadlessService headlessService;

        public HeadlessController(IHeadlessService headlessService)
        {
            this.headlessService = headlessService;
        }

        public virtual IActionResult GetData(string route)
        {
            return new OkObjectResult(headlessService.GetData(route));
        }
    }

    public class TestComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            PropertyMapper.AddMapping(Umbraco.Cms.Core.Constants.PropertyEditors.Aliases.MediaPicker3, x => new NewTestClass(x));
            PageMapper.AddMapping(Test.ModelTypeAlias, x => new NewTestClass2(x));
        }
    }

    public class NewTestClass : PropertyModel
    {
        public string CustomValue { get; set; }
        public string CustomValue2 { get; set; }

        public NewTestClass(ICreatePropertyCommandBase createPropertyCommandBase) : base(createPropertyCommandBase)
        {
            CustomValue = DateTime.Now.ToLongDateString();
            var b = (Image)createPropertyCommandBase.GetProperty();
            //var a = (Image)(((PublishedContentWrapped)createPropertyCommandBase.Property.GetValue()).Unwrap());
            //var b = a.GetProperty(nameof(Image.UmbracoFile)).GetValue();
            //var property = (MediaWithCrops)createPropertyCommandBase.Property.GetValue();
            //CustomValue2 = $"{property.UmbracoBytes} - {property.UmbracoExtension} - {property.UmbracoFile} - {property.UmbracoHeight}";
        }
    }

    public class NewTestClass2 : BasePageModel
    {
        public string CustomValue { get; set; }
        public string CustomValue2 { get; set; }

        public NewTestClass2(ICreatePageCommandBase createPageCommandBase) : base(createPageCommandBase)
        {
            CustomValue = DateTime.Now.ToLongDateString();
            var content = (Test)this.Unwrap();
            //var b = a.GetProperty(nameof(Image.UmbracoFile)).GetValue();
            //var property = (MediaWithCrops)createPropertyCommandBase.Property.GetValue();
            //CustomValue2 = $"{property.UmbracoBytes} - {property.UmbracoExtension} - {property.UmbracoFile} - {property.UmbracoHeight}";
        }

        public NewTestClass2(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }
    }

    public static class PropertyExtentions
    {
        public static IPublishedContent GetProperty(this ICreatePropertyCommandBase createPropertyCommandBase)
        {
            return ((PublishedContentWrapped)createPropertyCommandBase.Property?.GetValue())?.Unwrap();
        }
    }
}