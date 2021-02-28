using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class Phone : ValueObject<Phone>
    {
        public PhoneType Type { get; init; }

        public int? AreaCode { get; init; }

        public string Number { get; init; }

        public Phone(PhoneType phonetype, int areaCode,
            string number)
        {
            Type = phonetype;
            AreaCode = areaCode;
            Number = number;
        }

        public Phone(string numberWithAreaCode, PhoneType type)
        {
            AreaCode = PhoneHelper.GetAreaCodeIfExist(numberWithAreaCode);
            Number = PhoneHelper.Remove(numberWithAreaCode, ")");
            Type = type;
        }

        public Phone()
        {

        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return AreaCode;
            yield return Number;
            yield return Type;
        }

        public override string ToString()
        {
            return $"({AreaCode}){Number}";
        }

    }

    public static class PhoneHelper
    {
        public static int GetAreaCodeIfExist(string number)
        {
            string g = Between(number, "(", ")");
            int a;
            var check = int.TryParse(g, out a);
            return a;
        }

        public static string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        public static string Remove(string STR, string FirstString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.Length;
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }
}
