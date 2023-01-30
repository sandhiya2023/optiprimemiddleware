using aravindMiddleware.Data;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace aravindMiddleware.API
{
    public class SampleRequestSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(UserLogin))
            {
                schema.Example = new OpenApiObject()
                {
                    ["orgid"] = new OpenApiString("1"),
                    ["locationid"] = new OpenApiString("1"),
                    ["username"] = new OpenApiString("sakthi"),
                    ["password"] = new OpenApiString("123"),
                };
            }

            //if (context.Type == typeof(ProductMasterInput))
            //{
            //    schema.Example = new OpenApiObject()
            //    {

            //        ["orgid"] = new OpenApiString("1"),
            //        ["zoneid"] = new OpenApiString(""),
            //        ["locationid"] = new OpenApiString(""),
            //        ["barcode"] = new OpenApiString(""),
            //        ["category"] = new OpenApiString("FRAME"),
            //        ["categoryId"] = new OpenApiString("1"),
            //        ["externalid"] = new OpenApiString(""),
            //    };
            //}


        }
    }
}
