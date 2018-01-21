using NetCommunitySolution.Domain.BankCards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Framework.Htmls
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj,
              bool markCurrentAsSelected = true, int[] valuesToExclude = null)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("参数必须为枚举类型", "enumObj");

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                         select new
                         {
                             ID = Convert.ToInt32(enumValue),
                             //Name = enumValue.GetDescripion(),
                         };
            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);
            return new SelectList(values, "ID", "Name", selectedValue);
        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("参数必须为枚举类型", "enumObj");

            var values = from Enum enumValue in Enum.GetValues(typeof(TEnum))
                         select new
                         {
                             ID = Convert.ToInt32(enumValue),
                             Name = enumValue.GetDescription()//.GetDescription(),
                         };
            object selectedValue = null;
            return new SelectList(values, "ID", "Name", selectedValue);
        }


        public static SelectList EnumToDictionary<TEnum>(this TEnum enumObj, Func<Enum, String> getText, bool isSelected = true) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "enumType");
            }
            Dictionary<Int32, String> enumDic = new Dictionary<int, string>();

            var values = Enum.GetValues(typeof(TEnum));

            foreach (Enum enumValue in values)
            {
                Int32 key = Convert.ToInt32(enumValue);
                String value = getText(enumValue);
                enumDic.Add(key, value);
            }

            var i = enumDic.Select(x => new
            {
                ID = x.Key,
                Name = x.Value
            });

            if (isSelected)
                return new SelectList(i, "Id", "Name", enumObj);
            else
                return new SelectList(i, "Id", "Name", null);
        }

        public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }

        public static List<SelectListItem> ToSelectListItem<TEnum>(this TEnum enumObj) where TEnum : struct
        {
            var enumType = typeof(TEnum);
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var enumValue in Enum.GetValues(enumType))
            {
                selectList.Add(new SelectListItem
                {
                    Text = Enum.GetName(enumType, enumValue),  //GetDescription((enumType)enumValue),
                    Value = enumValue.ToString()
                });
            }
            return selectList;
        }


        /// <summary>
        /// 键值对转换为页面选择框
        /// </summary>
        /// <param name="banksCode"></param>
        /// <param name="addDescription"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListItem(this Dictionary<string, string> banksCode, bool addDescription = true,string code = "")
        {
            var codeItems = banksCode.Select(e => new SelectListItem
            {
                Value = e.Key,
                Text = e.Value,
                Selected = code == e.Key
            }).ToList();

            if (addDescription)
                codeItems.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "请选择银行",
                    Selected = code==""
                });

            return codeItems;
        }

        public static List<SelectListItem> AddressToSelectListItem(this HtmlHelper helper, bool addDescription = true)
        {
            var address = AccountOpenCode.GetAddressCode();
            var items = address.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            }).ToList();

            if (addDescription)
                items.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "请选择省份"
                });

            return items;
        }

        public static List<SelectListItem> CityToSelectListItem(this HtmlHelper helper, string province, bool addDescription = true)
        {
            var address = AccountOpenCode.GetAddressCode();
            var cities = address.FirstOrDefault(e => e.Name.Contains(province)).Cities;
            var items = cities.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            }).ToList();

            if (addDescription)
                items.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "请选择城市"
                });

            return items;
        }

        public static List<SelectListItem> CityToSelectListItem(this HtmlHelper helper, bool addDescription = true)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem {
                Text ="请选择城市",
                Value=""
            });
            return items;

        }


        public static List<SelectListItem> GetBankNameList(this HtmlHelper helper,string name = "")
        {
            var bankNames = new BankName().GetList();
            var items = bankNames.Select(b => new SelectListItem {
                Selected = name == b.Name,
                Text = b.Name,
                Value = b.Name
            }).ToList();

            items.Insert(0, new SelectListItem
            {
                Selected = name == "",
                Text = "请选择银行",
                Value = ""
            });

            return items;
        }
    }
}