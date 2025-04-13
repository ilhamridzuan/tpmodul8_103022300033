using System;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        CovidConfig config;
        if (File.Exists("covid_config.json"))
        {
            string json = File.ReadAllText("covid_config.json");
            config = JsonConvert.DeserializeObject<CovidConfig>(json);
        }
        else
        {
            config = new CovidConfig();
        }

        Console.Write($"Berapa suhu badan anda saat ini? (dalam {config.satuan_suhu}): ");
        double suhu = Convert.ToDouble(Console.ReadLine());

        Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
        int hari = Convert.ToInt32(Console.ReadLine());

        bool suhuOK = false;

        if (config.satuan_suhu == "celcius")
            suhuOK = suhu >= 36.5 && suhu <= 37.5;
        else
            suhuOK = suhu >= 97.7 && suhu <= 99.5;

        if (suhuOK && hari < config.batas_hari_deman)
        {
            Console.WriteLine(config.pesan_diterima);
        }
        else
        {
            Console.WriteLine(config.pesan_ditolak);
        }

        config.UbahSatuan();

        string updatedConfig = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText("covid_config.json", updatedConfig);
    }
}
