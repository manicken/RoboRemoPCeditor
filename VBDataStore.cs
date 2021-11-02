/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-06
 * Time: 13:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsan;

namespace RoboRemoPC
{
    public class TypeValuePair
    {
        public string Type;
        public string Value;

        public TypeValuePair(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
    /// <summary>
    /// Description of VBDataStore.
    /// </summary>
    public class VBDataStore
    {
        public List<string> keys;
        public List<string> types;
        public List<string> values;
        
        public DataTable dt;
            
        public VBDataStore()
        {
            keys = new List<string>();
            types = new List<string>();
            values = new List<string>();
        }

        public VBDataStore(string fromString)
        {
            keys = new List<string>();
            types = new List<string>();
            values = new List<string>();

            FromString(fromString);
        }

        public void FromString(string st)
        {
            keys.Clear();
            types.Clear();
            values.Clear();

            while (true)
            {
                string[] q = splitEntry(st);
                if (q == null)
                {
                    GenerateDataTable();
                    return;
                }
                keys.Add(q[0]);
                q = splitEntry(q[1]);
                types.Add(q[0]);
                q = splitEntry(q[1]);
                values.Add(q[0]);
                st = q[1];
            }
        }

        public void RemoveItems(int startIndex, int count)
        {
            keys.RemoveRange(startIndex, count);
            types.RemoveRange(startIndex, count);
            values.RemoveRange(startIndex, count);
        }
        
        public void UpdateValueFromDataTableToStore(int itemIndex)
        {
            object value = dt.Rows[itemIndex][2];
            if (value.GetType() == typeof(DBNull)) value = "";
            values[itemIndex] = (string)value;
        }
        
        public void GenerateDataTable()
        {
            string dtName = this.GetHashCode().ToString("X4");
            dt = new DataTable(dtName);
            
            dt.Columns.Add("key", typeof(string));
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("value", typeof(string));
            for (int i = 0;  i < keys.Count; i++)
            {
                if (types[i].StartsWith("Ui"))
                    dt.Rows.Add(keys[i], types[i], "(click to show)"); // don't show value of Ui Items here
                else
                    dt.Rows.Add(keys[i], types[i], values[i]);
            }
        }
                
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int size = keys.Count;
            for (int i = 0; i < size; i++) {
                sb.Append(keys[i].Length + " ");
                sb.Append(keys[i] + " ");
                sb.Append(types[i].Length + " ");
                sb.Append(types[i] + " ");
                sb.Append(values[i].Length + " ");
                sb.Append(values[i] + " ");
            }
            return sb.ToString();
        }
        
        public static string[] splitEntry(string st)
        {
            int a, len;

            if (st.NullOrEmpty())
                return null;

            if (!st.TryGetIndexOf(' ', out a))
                return null;
            
            if (!int.TryParse(st.Substring(0, a), out len))
                return null;
            
            String[] res = new String[2];
            res[0] = st.Substring(a + 1,  len);
            res[1] = st.Substring(((a + 1) + len) + 1);
            return res;
        }

        public void putBoolean(string key, bool value)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
            {
                keys.Add(key);
                types.Add("boolean");
                values.Add(/*BuildConfig.VERSION_NAME+*/ value.ToString());
            }
            else
            {
                types[ii] = "boolean";
                values[ii] = /*BuildConfig.VERSION_NAME+*/ value.ToString();
               
                dt.Rows[ii][1] = types[ii];
                dt.Rows[ii][2] = values[ii];
            }
        }
    
        public bool getBoolean(string key, bool defValue)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
                return defValue;
            else
                return bool.Parse(values[ii]);
        }
    
        public void putFloat(string key, float value)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
            {
                keys.Add(key);
                types.Add("float");
                values.Add(value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                types[ii] = "float";
                values[ii] = value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                
                dt.Rows[ii][1] = types[ii];
                dt.Rows[ii][2] = values[ii];
            }
        }
    
        public float getFloat(string key, float defValue)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
                return defValue;
            else
                return float.Parse(values[ii], System.Globalization.CultureInfo.InvariantCulture);
        }
    
        public void putInt(string key, int value)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
            {
                keys.Add(key);
                types.Add("int");
                values.Add(value.ToString());
            }
            else
            {
                types[ii] = "int";
                values[ii] = value.ToString();
               
                dt.Rows[ii][1] = types[ii];
                dt.Rows[ii][2] = values[ii];
            }
        }
    
        public int getInt(String key, int defValue)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
                return defValue;
            else
                return int.Parse(values[ii]);
        }
    
        public void putLong(string key, long value)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
            {
                keys.Add(key);
                types.Add("long");
                values.Add(value.ToString());
            }
            else
            {
                types[ii] = "long";
                values[ii] = value.ToString();
               
                dt.Rows[ii][1] = types[ii];
                dt.Rows[ii][2] = values[ii];
            }
        }
    
        public long getLong(String key, long defValue)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
                return defValue;
            else
                return long.Parse(values[ii]);
        }
    
        public void putString(string key, string value)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
            {
                keys.Add(key);
                types.Add("String");
                values.Add(value);
            }
            else
            {
                types[ii] = "String";
                values[ii] = value;
               
                dt.Rows[ii][1] = types[ii];
                dt.Rows[ii][2] = values[ii];
            }
        }
        public void putUIitem(string key,string type, string value)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
            {
                keys.Add(key);
                types.Add(type);
                values.Add(value);
                dt.Rows.Add(key, type, "(click to show)");
            }
            else
            {
                types[ii] = type;
                values[ii] = value;

                dt.Rows[ii][1] = types[ii];
                dt.Rows[ii][2] = "(click to show)";
            }
        }

        public String getString(string key, string defValue)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
                return defValue;
            else
                return values[ii];
        }

        public TypeValuePair getUiItem(string key)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
                return new TypeValuePair("", "");
            else
                return new TypeValuePair(types[ii], values[ii]);
        }

        public string getType(string key)
        {
            int ii = keys.IndexOf(key);
            if (ii == -1)
                return "";
            else
                return types[ii];
        }

        public void RemoveUiItem(string key)
        {
            int ii = keys.IndexOf(key);
            keys.RemoveAt(ii);
            types.RemoveAt(ii);
            values.RemoveAt(ii);
            int startIndex = int.Parse(key);

            for (int i = ii; i < keys.Count; i++)
            {
                keys[i] = startIndex.ToString();
                startIndex++;
            }
        }
    }
}
