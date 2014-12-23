﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Hi.UrlRewrite.Entities.Rules;
using Sitecore;
using Sitecore.Caching;
using Sitecore.Data;
using Sitecore.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hi.UrlRewrite.Caching
{
    public class RulesCache : CustomCache
    {
        private Database _db;
        private const string inboundRulesKey = "InboundRules";

        public RulesCache(Database db) : 
            base(string.Format("Hi.UrlRewrite[{0}]", db.Name), StringUtil.ParseSizeString(Configuration.CacheSize))
        {
            _db = db;
        }

        public List<InboundRule> GetInboundRules()
        {
            return GetObject(inboundRulesKey) as List<InboundRule>;
        }

        public void SetInboundRules(List<InboundRule> inboundRules)
        {
            long size;

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, inboundRules);
                size = memoryStream.Length;
            }

            SetObject(inboundRulesKey, inboundRules, size);
        }

    }
}