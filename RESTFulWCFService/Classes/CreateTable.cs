
using System;
using System.Data;
using System.Collections;

namespace PivotApp
{
    public class PivotTable
    {
        private DataTable _Table_Source = null;
        private DataTable _Table_Target = null;
        private string _Row_Field = "";
        private string _Col_Field = "";
        private string _Data_Field = "";
        private object TOT = 0;
        private Hashtable HT = null;
        public PivotTable()
        {
        }
        public DataTable Generate(DataTable Table_Source, string Row_Field, string Col_Field, string Data_Field)
        {

            try
            {
                _Row_Field = Row_Field;
                _Col_Field = Col_Field;
                _Data_Field = Data_Field;
                _Table_Source = Table_Source;
               CreateTable();
                foreach (DataRow Dr in _Table_Source.Rows)
                {
                    AddRow(Dr);
                }
               // FindTotal();
                _Table_Target.AcceptChanges();
                HT = null;
                return _Table_Target;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void CreateTable()
        {
            _Table_Target = new DataTable();
            _Table_Target.Columns.Add(_Row_Field, typeof(System.String));
           // _Table_Target.Columns.Add("TOTAL", typeof(System.Decimal));
            HT = new Hashtable();
        }
        private void AddRow(DataRow Dr)
        {
            string sData = "";
            Boolean bColumnExists = true;
            decimal iData1 = 0;
            decimal iData2 = 0;
            decimal iData = 0;

            sData = Dr[_Col_Field].ToString();
            sData.Replace(",", "");
            sData.Replace(" ", "_").ToUpper();
            if (HT.ContainsKey(sData))
                bColumnExists = true;
            else
            {
                HT.Add(sData, 0);
                bColumnExists = false;
            }
        //    if (!bColumnExists)
        //        _Table_Target.Columns.Add(sData, typeof(System.Decimal));
        //    DataRow Dr_Target = null;
        //    foreach (DataRow Dr_Temp in _Table_Target.Select(_Row_Field + "='" + Dr[_Row_Field].ToString() + "'"))
        //        Dr_Target = Dr_Temp;
        //    if (Dr_Target == null)
        //    {
        //        Dr_Target = _Table_Target.NewRow();
        //        Dr_Target[_Row_Field] = Dr[_Row_Field].ToString();
        //        Dr_Target[sData] = Dr[_Data_Field].ToString();
        //        Dr_Target["TOTAL"] = Dr[_Data_Field].ToString();
        //        _Table_Target.Rows.Add(Dr_Target);
        //    }
        //    else
        //    {
        //        Dr_Target[_Row_Field] = Dr[_Row_Field].ToString();
        //        if (Dr_Target[sData].ToString() != "")
        //            iData1 = Convert.ToDecimal(Dr_Target[sData].ToString());

        //        if (Dr[_Data_Field].ToString() != "")
        //            iData2 = Convert.ToDecimal(Dr[_Data_Field].ToString());

        //        iData = iData1 + iData2;
        //        Dr_Target[sData] = iData.ToString();
        //        Dr_Target["TOTAL"] = Convert.ToDecimal(Dr_Target["TOTAL"].ToString()) + iData2;
        //    }
        }
        private void FindTotal()
        {
            string sColName = "";
            DataRow Dr = null;
            if (HT.Count > 0)
            {
                Dr = _Table_Target.NewRow();
                IDictionaryEnumerator enumerator = HT.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    sColName = (string)enumerator.Key;
                    TOT = _Table_Target.Compute("SUM([" + sColName + "])", "1=1");
                    Dr[sColName] = TOT;
                }
                TOT = _Table_Target.Compute("SUM(TOTAL)", "1=1");
                Dr["TOTAL"] = TOT;
                Dr[_Row_Field] = "TOTAL";
                _Table_Target.Rows.Add(Dr);
            }
        }
    }
}

