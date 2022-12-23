using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CCS
{
    //Loglama :  Yapılan operasyonların bir yerde kaydını tutmak.
    //File Logger : Logları bir dosyaya almak demek. Günümüzde sık kullanılan bir log yönetimidir.
    //BU bir ILogger türü mü evet, Çünkü ben dosyaya,veritabanına,uzak sunucuya,mail atabilirim vs
    //Dolayısıyla birbirinin alternatifi olan şeyleri her zaman interface ile implemente etmeliyiz.
    public class FileLogger : ILogger 
    {
        public void Log()
        {
            Console.WriteLine("Dosyaya loglandı.");
        }
    }
}
