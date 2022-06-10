﻿using Nikcio.UHeadless.Basics.Properties.EditorsValues.RichTextEditor.Models;
using Nikcio.UHeadless.Properties.Commands;

namespace Examples.Docs.PropertyValues {
    public class CustomRichText : BasicRichText {

        public string MyCustomProperty { get; set; }

        public CustomRichText(CreatePropertyValue createPropertyValue) : base(createPropertyValue) {
            MyCustomProperty = "Hello here is a property";
        }
    }
}