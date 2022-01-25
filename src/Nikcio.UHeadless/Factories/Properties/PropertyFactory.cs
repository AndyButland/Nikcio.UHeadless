﻿using AutoMapper;
using Nikcio.UHeadless.Factories.Properties.PropertyValues;
using Nikcio.UHeadless.Models.Dtos.Content;
using Nikcio.UHeadless.Models.Dtos.Elements;
using Nikcio.UHeadless.Models.Dtos.Propreties;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Nikcio.UHeadless.Factories.Properties
{
    public class PropertyFactory : IPropertyFactory
    {
        private readonly IPropertyValueFactory propertyValueFactory;

        public PropertyFactory(IPropertyValueFactory propertyValueFactory)
        {
            this.propertyValueFactory = propertyValueFactory;
        }

        public void AddProperty(PublishedContentGraphType mappedObject, IPublishedProperty property)
        {
            AddProperty(mappedObject as PublishedElementGraphType, property);
        }

        public void AddProperty(PublishedElementGraphType mappedObject, IPublishedProperty property)
        {
            if (mappedObject.Properties == null)
            {
                mappedObject.Properties = new List<PublishedPropertyGraphType>();
            }
            mappedObject.Properties.Add(GetPropertyGraphType(property));
        }

        private PublishedPropertyGraphType GetPropertyGraphType(IPublishedProperty property)
        {
            var propertyValue = propertyValueFactory.GetPropertyValue(new Commands.Properties.CreatePropertyValue { Property = property });
            return new PublishedPropertyGraphType { Alias = property.Alias, Value = propertyValue };
        }
    }
}