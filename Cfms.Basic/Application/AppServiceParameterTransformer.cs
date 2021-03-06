﻿using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Cfms.Basic.Application
{
    /// <summary>
    /// 领域服务约定参数转换程序
    /// </summary>
    public class AppServiceParameterTransformer : IOutboundParameterTransformer
    {
        /// <summary>
        /// 出站变换
        /// </summary>
        /// <param name="value">内部实际路由地址</param>
        /// <returns></returns>
        public string TransformOutbound(object value)
        {
            if (value == null) { return null; }

            // Slugify value
            return Regex.Replace(value.ToString(), "([a-z])AppService", "$1").ToLower();
        }
    }
}
