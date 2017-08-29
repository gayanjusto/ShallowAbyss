using System;

namespace Assets.Scripts.Tools
{
    public static class ValueByField
    {
        public static string GetStringFieldValue(this object obj, string fieldName) 
        {
            return obj.GetType().GetField(fieldName).GetValue(obj) as string;
        }
    }
}
