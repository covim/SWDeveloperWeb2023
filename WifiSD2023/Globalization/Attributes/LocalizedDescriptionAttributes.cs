using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace Globalization.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string resourceKey;
        private static ResourceManager _ResourceManager { get; set; }
        public LocalizedDescriptionAttribute(string resourceKey)
        {
            this.resourceKey = resourceKey;
        }

        public override string Description
        {
            get => this.resourceKey != null ? _ResourceManager.GetString(this.resourceKey) : null;
        }

        public static void Setup(ResourceManager resourceManager)
        {
            _ResourceManager = resourceManager;
        }
    }
}
