using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIT.DataAccessLayer.HealthCare.Data
{
    public partial class HealthCareEntities
    {
        static readonly object _state = new object();
        static readonly Dictionary<string, System.Data.Metadata.Edm.MetadataWorkspace> dicMetaData = new Dictionary<string, System.Data.Metadata.Edm.MetadataWorkspace>();
        static public System.Data.Metadata.Edm.MetadataWorkspace GetMetaData(string entityName)
        {
            if (!dicMetaData.ContainsKey(entityName))
            {
                lock (_state)
                {
                    if (!dicMetaData.ContainsKey(entityName))
                    {
                        var temp = string.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", entityName);
                        dicMetaData.Add(
                            entityName,
                            new System.Data.Metadata.Edm.MetadataWorkspace(
                                temp.Split('|'), AppDomain.CurrentDomain.GetAssemblies()));
                    }
                }
            }

            return dicMetaData[entityName];
        }
    }
}
