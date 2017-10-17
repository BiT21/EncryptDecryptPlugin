﻿using Plugin.EncryptDecrypt.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.EncryptDecrypt
{
    public class CrossEncryptDecrypt
    {
        static Lazy<IEncryptDecrypt> Implementation = new Lazy<IEncryptDecrypt>(() => CreateEncryptDecryptService(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IEncryptDecrypt Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IEncryptDecrypt CreateEncryptDecryptService()
        {
#if NETSTANDARD1_0
            return null;
#else
        return new EncryptDecryptImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
