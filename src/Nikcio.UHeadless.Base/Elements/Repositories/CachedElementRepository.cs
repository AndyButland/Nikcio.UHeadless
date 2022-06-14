﻿using System.Linq.Expressions;
using Nikcio.UHeadless.Base.Elements.Factories;
using Nikcio.UHeadless.Base.Elements.Models;
using Nikcio.UHeadless.Base.Properties.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Web;

namespace Nikcio.UHeadless.Base.Elements.Repositories {
    /// <summary>
    /// A repository for elements
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public abstract class CachedElementRepository<TElement, TProperty> : ElementRepository<TElement, TProperty>
        where TElement : IElement<TProperty>
        where TProperty : IProperty {
        /// <summary>
        /// An accessor to the published shapshot
        /// </summary>
        protected readonly IPublishedSnapshotAccessor publishedSnapshotAccessor;

        /// <inheritdoc/>
        protected CachedElementRepository(IPublishedSnapshotAccessor publishedSnapshotAccessor, IUmbracoContextFactory umbracoContextFactory, IElementFactory<TElement, TProperty> elementFactory) : base(umbracoContextFactory, elementFactory) {
            umbracoContextFactory.EnsureUmbracoContext();
            this.publishedSnapshotAccessor = publishedSnapshotAccessor;
        }

        /// <summary>
        /// Gets an element
        /// </summary>
        /// <param name="fetch"></param>
        /// <param name="culture"></param>
        /// <param name="cacheSelector"></param>
        /// <returns></returns>
        protected virtual TElement? GetElement<TPublishedCache>(Func<TPublishedCache?, IPublishedContent?> fetch, string? culture, Expression<Func<IPublishedSnapshot, IPublishedCache>> cacheSelector)
            where TPublishedCache : IPublishedCache {
            var publishedCache = GetPublishedCache(cacheSelector);
            if (publishedCache is not null and TPublishedCache typedPublishedCache) {
                return base.GetElement(fetch(typedPublishedCache), culture);
            }
            return default;
        }

        /// <summary>
        /// Gets a list of elements
        /// </summary>
        /// <param name="fetch"></param>
        /// <param name="culture"></param>
        /// <param name="cacheSelector"></param>
        /// <returns></returns>
        protected virtual IEnumerable<TElement?> GetElementList<TPublishedCache>(Func<TPublishedCache?, IEnumerable<IPublishedContent>?> fetch, string? culture, Expression<Func<IPublishedSnapshot, IPublishedCache>> cacheSelector)
            where TPublishedCache : IPublishedCache {
            var publishedCache = GetPublishedCache(cacheSelector);
            if (publishedCache is not null and TPublishedCache typedPublishedCache) {
                return base.GetElementList(fetch(typedPublishedCache), culture);
            }
            return Enumerable.Empty<TElement>();
        }

        /// <summary>
        /// Gets a publish cache
        /// </summary>
        /// <param name="cacheSelector"></param>
        /// <returns></returns>
        protected virtual IPublishedCache? GetPublishedCache(Expression<Func<IPublishedSnapshot, IPublishedCache>> cacheSelector) {
            if (publishedSnapshotAccessor.TryGetPublishedSnapshot(out var publishedSnapshot)) {
                var compiledCacheSelector = cacheSelector.Compile();
                return compiledCacheSelector(publishedSnapshot);
            }
            return null;
        }
    }
}
