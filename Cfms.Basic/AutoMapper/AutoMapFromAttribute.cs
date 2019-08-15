//using System;
//using System.Collections.Generic;
//using System.Text;
//using AutoMapper;

//namespace Cfms.Basic.AutoMapper
//{
//    /// <summary>
//    /// 将注解类型映射自参数类型
//    /// </summary>
//    public class AutoMapFromAttribute : AutoMapAttributeBase
//    {
//        public MemberList MemberList { get; set; } = MemberList.Destination;
//        public AutoMapFromAttribute(Type targetTypes) 
//            : base(targetTypes)
//        {
//        }
//        public AutoMapFromAttribute(MemberList memberList, Type targetTypes)
//           : this(targetTypes)
//        {
//            MemberList = memberList;
//        }

//        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
//        {
//            if (TargetType == null)
//            {
//                return;
//            }
//            configuration.CreateMap(TargetType, type, MemberList);
//        }
//    }
//}
