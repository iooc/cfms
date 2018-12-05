using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Cfms.Basic.AutoMapper
{
    /// <summary>
    /// 将注解类型影射到参数类型
    /// </summary>
    public class AutoMapToAttribute : AutoMapAttributeBase
    {
        public MemberList MemberList { get; set; } = MemberList.Source;
        protected AutoMapToAttribute(Type targetTypes) 
            : base(targetTypes)
        {
        }

        public AutoMapToAttribute(MemberList memberList, Type targetTypes)
            : this(targetTypes)
        {
            MemberList = memberList;
        }

        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            if (TargetType==null)
            {
                return;
            }

            configuration.CreateMap(type, TargetType, MemberList);
        }
    }
}
