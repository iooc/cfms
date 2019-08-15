//using System;
//using System.Collections.Generic;
//using System.Text;
//using AutoMapper;

//namespace Cfms.Basic.AutoMapper
//{
//    /// <summary>
//    /// 将类型和给定的类型参数进行双向映射
//    /// </summary>
//    public class AutoMapAttribute : AutoMapAttributeBase
//    {
//        public AutoMapAttribute(Type targetTypes) : base(targetTypes)
//        {
//        }

//        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
//        {
//            if (TargetType==null)
//            {
//                return;
//            }

//            configuration.CreateMap(type, TargetType, MemberList.Source);
//            configuration.CreateMap(TargetType, type, MemberList.Destination);

//            //foreach (var targetType in TargetTypes)
//            //{
//            //    configuration.CreateAutoAttributeMaps(targetType, new[] { type }, MemberList.Destination);
//            //}
//        }
//    }
//}
