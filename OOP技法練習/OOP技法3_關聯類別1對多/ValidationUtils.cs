using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法3_關聯類別1對多
{
    public class ValidationUtils
    {
        public static int shouldBeWithinRange(string name, int num, int min, int max)
        {
            // 檢查 min 是否大於 max
            if (min > max)
            {
                throw new ArgumentException("最小值必須小於或等於最大值", nameof(min));
            }

            // 檢查 num 是否在範圍內
            if (num < min || num > max)
            {
                throw new ArgumentOutOfRangeException(nameof(num), num, $"{name} 必須在 {min} 到 {max} 之間");
            }

            return num;
        }
    }
}
