using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TestKST
{
    class SubClass
    {
        public string RollBackName(string DataName)
        {
            string result = DataName;
            bool __isFirst = true;
            string __NameTemp = DataName;
            int __SpaceIndex = 0;
            var __RollName = "";


            while (__NameTemp.Length > 0)
            {
                if (__isFirst == false)
                {
                    __RollName = " " + __RollName;
                }
                __SpaceIndex = __NameTemp.IndexOf(" ");
                if (__SpaceIndex > 0)
                {
                    __RollName = __NameTemp.Substring(0, __SpaceIndex) + __RollName;
                    __NameTemp = __NameTemp.Substring(__SpaceIndex + 1);
                }
                else
                {
                    __RollName = __NameTemp + __RollName;
                    __NameTemp = "";
                }
                __isFirst = false;
            }

            result = __RollName;
            return result;
        }

        public DataTable FileSort(DataTable File, string ColumnName)
        {
            DataTable result = File.Copy();
            result.Clear();

            File.DefaultView.Sort = ColumnName + " ASC";

            foreach (DataRow __row in File.DefaultView.ToTable().Rows)
            {
                
                result.NewRow();
                result.Rows.Add(RollBackName(__row[ColumnName].ToString()));
            }
            return result;
        }
    }
}
