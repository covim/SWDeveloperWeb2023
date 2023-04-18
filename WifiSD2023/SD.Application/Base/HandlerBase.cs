using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Base
{
    public abstract class HandlerBase
    {
        protected void MapEntityProperties<TSource, TTarget>(TSource source, TTarget target, IList<string> excludeProperties)
        {

            var sourceType = source.GetType();
            var targetType = target.GetType();

            if (sourceType.BaseType.FullName != targetType.BaseType.FullName)
            {
                throw new ApplicationException("Base types are not matching");
            }

            List<PropertyInfo> targetPropertyInfos = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            targetPropertyInfos.ForEach(p =>
            {
                // try to find matching property in source
                if (p.CanWrite && !(excludeProperties ?? new List<string>()).Contains(p.Name))
                {
                    var sourceProperty = sourceType.GetProperty(p.Name, BindingFlags.Instance | BindingFlags.Public);
                    if (sourceProperty != null)
                    {
                        // read property value from source
                        var sourcePropertyValue = sourceProperty.GetValue(source, null);
                        // write property value to target
                        p.SetValue(target, sourcePropertyValue);
                    }
                }

            });


        }
    }
}
