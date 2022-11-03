using DryIoc.FastExpressionCompiler.LightExpression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace StackDeneme
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OkeyOyunu okeyOyunu = new OkeyOyunu();
            okeyOyunu.OkeyTaslariOlusturma();
            okeyOyunu.OkeyTaslariniKarma();
            okeyOyunu.OkeyTaslariniGösterme();
            okeyOyunu.OkeySeçme();
            okeyOyunu.TaslariDagitma();
            okeyOyunu.Puanlama();
        }
    }

    public class Oyuncular
    {
        public string adi { get; set; }
        public int puan { get; set; }
        public Stack oyuncuTaslari { get; set; }
    }
    public class OkeyOyunu
    {
        public int sbt=1;
        Oyuncular o1 = new Oyuncular();
        Oyuncular o2 = new Oyuncular();
        Oyuncular o3 = new Oyuncular();
        Oyuncular o4 = new Oyuncular();
        Oyuncular o;
        public List<string> OkeyTaslari = new List<string>();
        protected string renk = "";
        public void OkeyTaslariOlusturma()
        {
            OkeyTaslari.Add("Sahte Okey");
            OkeyTaslari.Add("Sahte Okey");
            for (int ik= 0; ik < 2; ik++)
            {
                for (int i = 1; i < 14; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        switch (j)
                        {
                            case 1:
                                renk = "mavi";
                                break;
                            case 2:
                                renk = "kırmızı";
                                break;
                            case 3:
                                renk = "yeşil";
                                break;
                            case 4:
                                renk = "siyah";
                                break;
                            default:
                                break;
                        }
                        OkeyTaslari.Add(renk + " " + i);
                    }
                }
            
            }
            Console.WriteLine(OkeyTaslari.Count);
        }
        public void OkeyTaslariniGösterme()
        {
            
            foreach (var taslar in OkeyTaslari)
            {
                Console.WriteLine(taslar);
            }
        } 

        public void OkeyTaslariniKarma()
        {
            Random rnd = new Random();
            int karmaSayisi = rnd.Next(0,106);
            for (int i = 0; i <= karmaSayisi; i++)
            {
                int birinciSayi = rnd.Next(OkeyTaslari.Count);
                int ikinciSayi = rnd.Next(OkeyTaslari.Count);
                string degisecekDeger = OkeyTaslari[birinciSayi];
                OkeyTaslari[birinciSayi] = OkeyTaslari[ikinciSayi];
                OkeyTaslari[ikinciSayi] = degisecekDeger;
            }   
        }
        
        public void OkeySeçme()
        {
            Random random = new Random();
            int okeyIndex = random.Next(OkeyTaslari.Count);
            string okey = OkeyTaslari[okeyIndex];
            OkeyTaslari[okeyIndex] = "*";
            foreach (string tas in OkeyTaslari.ToList())
            {
                if (tas == okey)
                {
                    OkeyTaslari[OkeyTaslari.IndexOf(okey)] = "*";
                }
                else if (tas == "Sahte Okey")
                {
                    OkeyTaslari[OkeyTaslari.IndexOf("Sahte Okey")] = okey;
                }
                
            }
        }

        public void Puanlama()
        {
            int[] puanlar = new int[4];
            for (int k = 0; k < 4; k++)
            {
                switch (k)
                {
                    case 0:
                        o = o1;

                        break;
                    case 1:
                        o = o2;
                        break;
                    case 2:
                        o = o3;
                        break;
                    case 3:
                        o = o4;
                        break;
                    default:
                        break;
                }
                o.adi = $"{k + 1}";
                Console.WriteLine("------------------------------------------------");
                foreach (string tas in o.oyuncuTaslari)
                {
                    Console.WriteLine(tas + " "+ (k+1)+". oyuncunun taşı");
                    if (tas == "*")
                    {
                        
                        o.puan = (o.puan + 20);
                    }
                    else
                    {
                        var ayrilmistas = tas.Split();
                        o.puan = (o.puan +Convert.ToInt32(ayrilmistas[1]));
                    }

                }
                
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"{k+1}. oyuncunun toplam puanı = {o.puan}");
                puanlar[k] = o.puan;

            }
            
            Console.WriteLine(puanlar.Max()+" puanla "+ (Array.IndexOf(puanlar,puanlar.Max())+1)+".oyuncu kazandı.");


        }
        public void TaslariDagitma()
        {
            Random rand = new Random();

            for (int k = 0; k < 4; k++)
            {
                switch (k)
                {
                    case 0:
                        o = o1;

                        break;
                    case 1:
                        o = o2;
                        break;
                    case 2:
                        o = o3;
                        break;
                    case 3:
                        o = o4;
                        break;
                    default:
                        break;
                }
                o.oyuncuTaslari = new Stack();
                for (int i = 0; i < 5; i++)
                {
                    int eklenecekSayi = rand.Next(OkeyTaslari.Count);
                    o.oyuncuTaslari.Push(OkeyTaslari[eklenecekSayi]);
                    OkeyTaslari.RemoveAt(eklenecekSayi);

                }
                for (int i = 0; i < 4; i++)
                {
                    int eklenecekSayi = rand.Next(OkeyTaslari.Count);
                    o.oyuncuTaslari.Push(OkeyTaslari[eklenecekSayi]);
                    OkeyTaslari.RemoveAt(eklenecekSayi);
                }

                for (int i = 0; i < 17; i++)
                {
                    int eklenecekSayi = rand.Next(OkeyTaslari.Count);
                    o.oyuncuTaslari.Push(OkeyTaslari[eklenecekSayi]);
                    OkeyTaslari.RemoveAt(eklenecekSayi);
                }

                if (OkeyTaslari.Count == 2)
                {
                    o1.oyuncuTaslari.Push(OkeyTaslari[0]);
                    OkeyTaslari.RemoveAt(0);

                    o2.oyuncuTaslari.Push(OkeyTaslari[0]);
                    OkeyTaslari.RemoveAt(0);
                }
            }
        }
    }    
}
