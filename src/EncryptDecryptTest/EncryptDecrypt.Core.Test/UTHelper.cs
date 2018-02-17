using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiT21.EncryptDecrypt.Core.Test
{

    public class Helper
    {
        const string text = "012345678";
        public static byte[] GetByteArray()
        {
            return text.Select(c => Convert.ToByte(c)).ToArray();
        }
    }

    public class Trace
    {
        public static void WriteLine(string msg = null, [CallerMemberName] string func = "<Empty>")
        {
            System.Diagnostics.Debug.WriteLine($"| {func} | {msg}");
        }
    }
    public class SimpleObject
    {
        public int number { get; set; }
        public string name { get; set; }
        public DateTime dateTime { get; set; }

        public static SimpleObject GetObject()
        {
            var o = new SimpleObject();

            o.name = Guid.NewGuid().ToString();
            o.number = Convert.ToInt32(Regex.Replace(o.name, @"[^0-9]+", string.Empty).Substring(0, 5));
            o.dateTime = DateTime.Now.AddMinutes(o.number);

            return o;
        }
        public static List<SimpleObject> GetObjectList()
        {
            var list = new List<SimpleObject>();

            list.Add(GetObject());
            list.Add(GetObject());
            list.Add(GetObject());
            list.Add(GetObject());
            list.Add(GetObject());

            return list;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            SimpleObject so = obj as SimpleObject;
            bool ret =
                this.name == so.name &&
                this.number == so.number &&
                this.dateTime == so.dateTime;

            return ret;
            //return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            //throw new NotImplementedException();
            return base.GetHashCode();
        }

    }
}

