﻿using HotChocolate.Types;
using Nikcio.UHeadless.Basics.ContentTypes.Models;
using Nikcio.UHeadless.Basics.Media.Models;
using Nikcio.UHeadless.Core.GraphQL.Queries;
using Nikcio.UHeadless.Media.Queries;
using Nikcio.UHeadless.Properties.Models;

namespace Nikcio.UHeadless.Basics.Media.Queries {
    /// <summary>
    /// The default implementation of the Media queries
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class BasicMediaQuery : MediaQuery<BasicMedia<BasicProperty, BasicContentType>, BasicProperty> {
    }
}