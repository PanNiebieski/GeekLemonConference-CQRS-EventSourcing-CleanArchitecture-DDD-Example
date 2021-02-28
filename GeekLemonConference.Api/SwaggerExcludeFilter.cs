//using GeekLemonConference.Application.Common;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using Swashbuckle.Swagger;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace GeekLemonConference.Api
//{
//    public class SwaggerExcludeFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
//    {
//        #region ISchemaFilter Members



//        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
//        {
//            if (schema?.Properties == null || context == null)
//                return;

//            var excludedProperties = context.ParameterInfo
//                .GetCustomAttribute<SwaggerExcludeAttribute>();

//            foreach (var excludedProperty in excludedProperties)
//            {
//                if (schema.Properties.ContainsKey(excludedProperty.Name))
//                    schema.Properties.Remove(excludedProperty.Name);
//            }
//        }

//        #endregion
//    }
//}
