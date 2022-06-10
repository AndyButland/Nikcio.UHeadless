﻿using Nikcio.UHeadless.Content.Models;
using Nikcio.UHeadless.Elements.Factories;
using Nikcio.UHeadless.Properties.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Nikcio.UHeadless.Content.Factories {
    /// <summary>
    /// A factory for creating content
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public interface IContentFactory<TContent, TProperty> : IElementFactory<TContent, TProperty>
            where TContent : IContent<TProperty>
            where TProperty : IProperty {
        /// <summary>
        /// Creates a content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        TContent? CreateContent(IPublishedContent? content, string? culture);
    }
}
