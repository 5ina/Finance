using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Security.YeeDto;
using System;
using System.Linq;

namespace NetCommunitySolution.Customers
{
    /// <summary>
    /// 用户属性扩展类
    /// </summary>
    public static class CustomerAttributeExtensions
    {
        /// <summary>
        /// 获取用户的属性值
        /// </summary>
        /// <typeparam name="TPropType"></typeparam>
        /// <param name="customer"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TPropType GetCustomerAttributeValue<TPropType>(this Customer customer, string key)
        {
            var _customerAttributeService = Abp.Dependency.IocManager.Instance.Resolve<ICustomerAttributeService>();
            
            return GetCustomerAttributeValue<TPropType>(customer, key, _customerAttributeService);
        }

        /// <summary>
        /// 获取用户的属性值
        /// </summary>
        /// <typeparam name="TPropType"></typeparam>
        /// <param name="customer"></param>
        /// <param name="key"></param>
        /// <param name="_customerAttributeService"></param>
        /// <returns></returns>
        public static TPropType GetCustomerAttributeValue<TPropType>(this Customer customer, string key,
            ICustomerAttributeService _customerAttributeService)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var props = _customerAttributeService.GetAttributeByCustomerId(customer.Id);

            if (props == null)
                return default(TPropType);
            if (props.Count == 0)
                return default(TPropType);

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return default(TPropType);

            return CommonHelper.To<TPropType>(prop.Value);
        }

        public static void SaveCustomerAttribute<TPropType>(this Customer customer, string key, TPropType value)
        {
            var _customerAttributeService = Abp.Dependency.IocManager.Instance.Resolve<ICustomerAttributeService>();

            SaveCustomerAttribute<TPropType>(customer, key, value, _customerAttributeService);
        }

        public static void SaveCustomerAttribute<TPropType>(this Customer customer, string key, TPropType value,
            ICustomerAttributeService _customerAttributeService)
        {
            if (customer == null|| customer.Id ==0)
                throw new ArgumentNullException("customer");

            var props = _customerAttributeService.GetAttributeByCustomerId(customer.Id);
            var attribute = new CustomerAttribute();
            if (props != null
                && props.Count > 0
                && props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)) != null)
            {
                attribute = props.FirstOrDefault(ga =>
                 ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));

                if (attribute.Value == value.ToString())
                    return;
                attribute.Value = value.ToString();
            }
            else {
                attribute.CustomerId = customer.Id;
                attribute.Key = key;
                attribute.Value = value.ToString();            
            }
            _customerAttributeService.SaveAttribute(attribute);
        }

        /// <summary>
        /// 获取用户的属性
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="_customerAttributeService"></param>
        /// <returns></returns>
        public static CustomerAttributeDto GetCustomerAttributes(this Customer customer, ICustomerAttributeService _customerAttributeService)
        {
            var model = new CustomerAttributeDto
            {
                Avatar = GetCustomerAttributeValue<string>(customer, CustomerAttributeNames.Avatar),
                Id = customer.Id,
                Reward = GetCustomerAttributeValue<string>(customer, CustomerAttributeNames.Reward),
                Sex = GetCustomerAttributeValue<string>(customer, CustomerAttributeNames.Sex),

            };
            return model;
        }


        public static void SaveYeeInfomation(this Customer customer, ICustomerAttributeService _customerAttributeService, PaymerchantregModel model)
        {
            if (!String.IsNullOrWhiteSpace(model.bind_mobile))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.bind_mobile, model.bind_mobile);
            if (!String.IsNullOrWhiteSpace(model.id_card))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.id_card, model.id_card);
            if (!String.IsNullOrWhiteSpace(model.id_card_back_photo))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.id_card_back_photo, model.id_card_back_photo);
            if (!String.IsNullOrWhiteSpace(model.id_card_photo))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.id_card_photo, model.id_card_photo);
            if (!String.IsNullOrWhiteSpace(model.legal_person))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.legal_person, model.legal_person);
            if (!String.IsNullOrWhiteSpace(model.person_photo))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.person_photo, model.person_photo);
            if (!String.IsNullOrWhiteSpace(model.region_text))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.region_text, model.region_text);
            if (!String.IsNullOrWhiteSpace(model.area_code))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.area_code, model.area_code);
            if (!String.IsNullOrWhiteSpace(model.bank_account_number))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.bank_account_number, model.bank_account_number);
            if (!String.IsNullOrWhiteSpace(model.bank_card_photo))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.bank_card_photo, model.bank_card_photo);
            if (!String.IsNullOrWhiteSpace(model.bank_name))
                SaveCustomerAttribute<string>(customer, CustomerAttributeNames.bank_name, model.bank_name);
        }
        
    }
}
