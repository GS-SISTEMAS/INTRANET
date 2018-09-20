using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace GS.SISGEGS.Web.Helpers
{
    public class PivotTable
    {
        private DataTable _Table_Source = null;
        private DataTable _Table_Target = null;
        private List<string> _Row_Field = new List<string>();
        private string _Col_Field = "";
        private string _Data_Field = "";
        private object TOT = 0;
        private Hashtable HT = null;

        public PivotTable()
        {
        }

        public DataTable Generate(DataTable Table_Source, List<string> Row_Field, string Col_Field, string Data_Field,
            bool totales = false)
        {

            try
            {
                _Row_Field = Row_Field;
                _Col_Field = Col_Field;
                _Data_Field = Data_Field;
                _Table_Source = Table_Source;

                switch (totales)
                {
                    case true:
                        CreateTableTotales();
                        foreach (DataRow Dr in _Table_Source.Rows)
                        {
                            AddRowTotales(Dr);
                        }

                        FindTotal();
                        break;
                    case false:
                        CreateTable();
                        foreach (DataRow Dr in _Table_Source.Rows)
                        {
                            AddRow(Dr);
                        }

                        break;
                }

                _Table_Target.AcceptChanges();
                HT = null;

                _Table_Target.Columns[8].SetOrdinal(_Table_Target.Columns.Count - 1);
                // UBICA COLUMNA TOTAL AL FINAL DE TODAS LAS COLUMNAS
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
            foreach (var item in _Row_Field)
            {
                _Table_Target.Columns.Add(item, typeof(System.String));
            }
            _Table_Target.Columns.Add("VARIACION", typeof(System.Decimal));
            
            HT = new Hashtable();
        }

        private void CreateTableTotales()
        {
            _Table_Target = new DataTable();
            //_Table_Target.Columns.Add(_Row_Field, typeof (System.String));
            _Table_Target.Columns.Add("TOTAL", typeof (System.Decimal));
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
            {
                bColumnExists = true;
            }
            else
            {
                HT.Add(sData, 0);
                bColumnExists = false;
            }
            if (!bColumnExists)
            {
                _Table_Target.Columns.Add(sData, typeof (System.String));
            }
            DataRow Dr_Target = null;
            //foreach (var row in _Row_Field)
            //{
            foreach (DataRow Dr_Temp in _Table_Target.Select(_Row_Field[0] + "='" + Dr[_Row_Field[0]].ToString() + "'"))
                {
                    Dr_Target = Dr_Temp;
                }


                if (Dr_Target == null)
                {
                    Dr_Target = _Table_Target.NewRow();
                    foreach (var item in _Row_Field)
                    {
                        Dr_Target[item] = Dr[item].ToString();
                    }

                    Dr_Target[sData] = Dr[_Data_Field].ToString();
                    Dr_Target["VARIACION"] = Math.Round(Convert.ToDecimal(Dr[_Data_Field].ToString()),2);
                    _Table_Target.Rows.Add(Dr_Target);
                }
                else
                {
                    Dr_Target[_Row_Field[0]] = Dr[_Row_Field[0]].ToString();
                    for (var i = 2; i < _Row_Field.Count; i++)
                    {
                        Dr_Target[_Row_Field[i]] = Math.Round(Convert.ToDecimal(Dr_Target[_Row_Field[i]].ToString()),2) + Math.Round(Convert.ToDecimal(Dr[_Row_Field[i]].ToString()),2);
                    }
                    if (!string.IsNullOrEmpty(Dr_Target[sData].ToString()))
                    {
                        iData1 = Math.Round(Convert.ToDecimal(Dr_Target[sData].ToString()),2);
                    }

                    if (!string.IsNullOrEmpty(Dr[_Data_Field].ToString()))
                    {
                        iData2 = Math.Round(Convert.ToDecimal(Dr[_Data_Field].ToString()),2);
                    }

                    iData = iData1 + iData2;
                    Dr_Target[sData] = iData;
                    Dr_Target["VARIACION"] = Math.Round(Convert.ToDecimal(Dr_Target["VARIACION"]),2) + iData2;
                }
            //}
        }

        private void AddRowTotales(DataRow Dr)
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
            {
                bColumnExists = true;
            }
            else
            {
                HT.Add(sData, 0);
                bColumnExists = false;
            }
            //if (!bColumnExists)
            //{
            //    _Table_Target.Columns.Add(sData, typeof (System.Decimal));
            //}
            //DataRow Dr_Target = null;
            //foreach (DataRow Dr_Temp in _Table_Target.Select(_Row_Field + "='" + Dr[_Row_Field].ToString() + "'"))
            //{
            //    Dr_Target = Dr_Temp;
            //}
            //if (Dr_Target == null)
            //{
            //    Dr_Target = _Table_Target.NewRow();
            //    Dr_Target[_Row_Field] = Dr[_Row_Field].ToString();
            //    Dr_Target[sData] = Convert.ToInt32(Dr[_Data_Field].ToString()); //PSF
            //    Dr_Target["TOTAL"] = Convert.ToInt32(Dr[_Data_Field].ToString()); //PSF
            //    _Table_Target.Rows.Add(Dr_Target);
            //}
            //else
            //{
            //    Dr_Target[_Row_Field] = Dr[_Row_Field].ToString();
            //    if (!string.IsNullOrEmpty(Dr_Target[sData].ToString()))
            //    {
            //        iData1 = Convert.ToDecimal(Convert.ToInt32(Dr_Target[sData].ToString()));
            //    }

            //    if (!string.IsNullOrEmpty(Dr[_Data_Field].ToString()))
            //    {
            //        iData2 = Convert.ToDecimal(Convert.ToInt32(Dr[_Data_Field].ToString()));
            //    }

            //    iData = iData1 + iData2;
            //    Dr_Target[sData] = iData;
            //    Dr_Target["TOTAL"] = Convert.ToDecimal(Convert.ToInt32(Dr_Target["TOTAL"])) + iData2;

            //}
        }

        private void FindTotal()
        {
            decimal xTOT = 0;
            string sColName = "";
            DataRow Dr = null;
            if (HT.Count > 0)
            {
                Dr = _Table_Target.NewRow();
                IDictionaryEnumerator enumerator = HT.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    sColName = (string) enumerator.Key;
                    TOT = _Table_Target.Compute("SUM([" + sColName + "])", "1=1");
                    Dr[sColName] = TOT;
                    //xTOT += TOT;
                }
                //TOT = _Table_Target.Compute("SUM(TOTAL)", "1=1")
                //Dr["TOTAL"] = xTOT;
                //Dr[_Row_Field] = "TOTAL";
                _Table_Target.Rows.Add(Dr);
            }
        }
    }
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
