using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace OA.GeneralClass
{
    public class ValidateUtil
    {
        /// <summary>
        /// 年度
        /// </summary>
        /// <returns></returns>
        public static ArrayList ALLYEARS()
        {
            ArrayList alyear = new ArrayList();
            int i;
            for (i = 1990; i <= 2020; i++)
                alyear.Add(i);
            return alyear;
        }

        /// <summary>
        /// Blank
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool isBlank(string Input)
        {
            if (Input == null || Input.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Email
        /// </summary>
        /// <param name="txtemail"></param>
        /// <returns></returns>
        public static bool isEmail(string Email)
        {
            Regex r = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", RegexOptions.CultureInvariant);
            return r.IsMatch(Email);
        }

        /// <summary>
        /// Phone
        /// </summary>
        /// <param name="txtphone"></param>
        /// <returns></returns>
        public static bool isPhone(string Phone)
        {
            Regex r = new Regex("((\\(\\d{3}\\))|(\\d{3}\\-))?(\\(0\\d{2,3}\\)|0\\d{2,3}-)?[1-9]\\d{6,7}", RegexOptions.CultureInvariant);
            return r.IsMatch(Phone);
        }

        /// <summary>
        /// Fax
        /// </summary>
        /// <param name="txtphone"></param>
        /// <returns></returns>
        public static bool isFax(string Fax)
        {
            Regex r = new Regex("((\\(\\d{3}\\))|(\\d{3}\\-))?(\\(0\\d{2,3}\\)|0\\d{2,3}-)?[1-9]\\d{6,7}", RegexOptions.CultureInvariant);
            return r.IsMatch(Fax);
        }

        /// <summary>
        /// Mobile
        /// </summary>
        /// <param name="txtmobile"></param>
        /// <returns></returns>
        public static bool isMobile(string Mobile)
        {
            Regex r = new Regex("((\\(\\d{3}\\))|(\\d{3}\\-))?13\\d{9}", RegexOptions.CultureInvariant);
            return r.IsMatch(Mobile);
        }

        /// <summary>
        /// URL
        /// </summary>
        /// <param name="txtURL"></param>
        /// <returns></returns>
        public static bool isURL(string URL)
        {
            Regex r = new Regex("http:\\/\\/[A-Za-z0-9]+\\.[A-Za-z0-9]+[\\/=\\?%\\-&_~`@[\\]\':+!]*([^<>\"\"])*", RegexOptions.CultureInvariant);
            return r.IsMatch(URL);
        }

        /// <summary>
        /// IDCard
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public static bool isIDCard(string IDCard)
        {
            Regex r = new Regex("\\d{15}(\\d{2}[A-Za-z0-9])?", RegexOptions.CultureInvariant);
            return r.IsMatch(IDCard);
        }

        /// <summary>
        /// Currency
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        public static bool isCurrency(string Currency)
        {
            Regex r = new Regex("\\d+(\\.\\d+)?", RegexOptions.CultureInvariant);
            return r.IsMatch(Currency);
        }

        /// <summary>
        /// Number
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static bool isNumber(string Number)
        {
            Regex r = new Regex("\\d+", RegexOptions.CultureInvariant);
            return r.IsMatch(Number);
        }

        /// <summary>
        /// Zip
        /// </summary>
        /// <param name="Zip"></param>
        /// <returns></returns>
        public static bool isZip(string Zip)
        {
            Regex r = new Regex("[1-9]\\d{5}", RegexOptions.CultureInvariant);
            return r.IsMatch(Zip);
        }

        /// <summary>
        /// QQ
        /// </summary>
        /// <param name="QQ"></param>
        /// <returns></returns>
        public static bool isQQ(string QQ)
        {
            Regex r = new Regex("[1-9]\\d{4,8}", RegexOptions.CultureInvariant);
            return r.IsMatch(QQ);
        }

        /// <summary>
        /// Integer
        /// </summary>
        /// <param name="Integer"></param>
        /// <returns></returns>
        public static bool isInteger(string Integer)
        {
            Regex r = new Regex("[-\\+]?\\d+", RegexOptions.CultureInvariant);
            return r.IsMatch(Integer);
        }

        /// <summary>
        ///  Double
        /// </summary>
        /// <param name="Double"></param>
        /// <returns></returns>
        public static bool isDouble(string Double)
        {
            Regex r = new Regex("[-\\+]?\\d+(\\.\\d+)?", RegexOptions.CultureInvariant);
            return r.IsMatch(Double);
        }

        /// <summary>
        /// English
        /// </summary>
        /// <param name="English"></param>
        /// <returns></returns>
        public static bool isEnglish(string English)
        {
            Regex r = new Regex("[A-Za-z]+", RegexOptions.CultureInvariant);
            return r.IsMatch(English);
        }

        /// <summary>
        /// Chinese
        /// </summary>
        /// <param name="Chinese"></param>
        /// <returns></returns>
        public static bool isChinese(string Chinese)
        {
            Regex r = new Regex("[\\u0391-\\uFFE5]+", RegexOptions.CultureInvariant);
            return r.IsMatch(Chinese);
        }
    }
}
