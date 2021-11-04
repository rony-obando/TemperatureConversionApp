using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Infraestructure.Temperature
{
    public class ConversionModel
    {
        public Temperaturas[] temperaturas;
        public Temperaturas[] temConvertidas;
        private void Add(Temperaturas t,ref Temperaturas[] temp)
        {
            if (temp == null)
            {
                temp = new Temperaturas[1];
                temp[0] = t;
                return;
            }
            Temperaturas[] tmp = new Temperaturas[temp.Length + 1];
            Array.Copy(temp, tmp, temp.Length);
            tmp[tmp.Length - 1] = t;
            temp = tmp;

        }
        public void Convertir(Temperaturas t,TipoUnidad tC)
        {
            Add(t,ref temperaturas);
            Temperaturas tm = new Temperaturas {
                pos=t.pos,
                tipounidad=tC
            };
            Calculo(t,tm);
        }
        public void Verificar(string cmb1,string cmb2)
        {
            if (string.IsNullOrEmpty(cmb1) || string.IsNullOrEmpty(cmb2))
            {
                throw new ArgumentException("Error, Todos los datos son requeridos");
            }
        }
        private void Calculo(Temperaturas tOrig, Temperaturas tConv)
        {
            double vOrig = tOrig.Dato;
            double vConv = 0;
            double d = 0;
            double b = 0;
            switch (tOrig.tipounidad)
            {
                case TipoUnidad.Celsius:
                    switch (tConv.tipounidad)
                    {
                        case TipoUnidad.Celsius:
                            tConv.Dato = vOrig;
                            Add(tConv, ref temConvertidas);
                            break;
                        case TipoUnidad.Fahrenheit:
                            d = 1.8;
                            b = (vOrig * d);
                            vConv = b+32;
                            tConv.Dato = vConv;
                            Add(tConv, ref temConvertidas);
                            break;
                        case TipoUnidad.Kelvin:
                            vConv = vOrig + 273.15;
                            tConv.Dato = vConv;
                            Add(tConv, ref temConvertidas);
                            break;

                    }
                    break;
                case TipoUnidad.Fahrenheit:
                    switch (tConv.tipounidad)
                    {
                        case TipoUnidad.Celsius:
                             d = 0.55555555555555555555555555555556;
                            b = vOrig - 32;
                            vConv =  b* d;
                            tConv.Dato = vConv;
                            Add(tConv, ref temConvertidas);
                            break;
                        case TipoUnidad.Fahrenheit:
                            tConv.Dato = vOrig;
                            Add(tConv, ref temConvertidas);
                            break;
                        case TipoUnidad.Kelvin:
                            d = 0.55555555555555555555555555555556;
                            b = vOrig - 32;
                            b = b * d;
                            vConv = b+ 273.15;
                            tConv.Dato = vConv;
                            Add(tConv, ref temConvertidas);
                            break;

                    }
                    break;
                case TipoUnidad.Kelvin:
                    switch (tConv.tipounidad)
                    {
                        case TipoUnidad.Celsius:
                            vConv = vOrig - 273.15;
                            tConv.Dato = vConv;
                            Add(tConv, ref temConvertidas);
                            break;
                        case TipoUnidad.Fahrenheit:
                            d = 1.8;
                            b = vOrig - 273.15;
                            vConv = b*d;
                            tConv.Dato = vConv;
                            Add(tConv, ref temConvertidas);
                            break;
                        case TipoUnidad.Kelvin:
                            tConv.Dato = vOrig;
                            Add(tConv, ref temConvertidas);
                            break;

                    }
                    break;
            }
        }
        private int GetIndexByPos(int pos)
        {
            if (pos <= 0)
            {
                throw new ArgumentException("la posición no puede ser negativo o cero.");
            }

            int index = int.MinValue, i = 0;
            if (temperaturas == null)
            {
                return index;
            }

            foreach (Temperaturas t in temperaturas)
            {
                if (t.pos == pos)
                {
                    index = i;
                    break;
                }
                i++;
            }

            return index;
        }
        public void Delete(Temperaturas pos)
        {
            if (temConvertidas.Length == 1 || temperaturas.Length == 1)
            {
                temConvertidas = null;
                temperaturas = null;
            }
            else
            {
                if (pos == null)
                {
                    throw new ArgumentException("Error, es null");
                }
                int f = pos.pos;
                int index = GetIndexByPos(pos.pos);
                if (index < 0)
                {
                    throw new Exception($"No hay una posición: {pos.pos}");
                }

                if (index != temperaturas.Length - 1)
                {
                    temperaturas[index] = temperaturas[temperaturas.Length - 1];
                    temConvertidas[index] = temConvertidas[temConvertidas.Length - 1];
                }

                Temperaturas[] tmp = new Temperaturas[temperaturas.Length - 1];
                Array.Copy(temperaturas, tmp, tmp.Length);
                Array.Sort(tmp, new OrdenByPosicion());
                temperaturas[temperaturas.Length - 1].pos = f;
                temperaturas = tmp;
                Array.Copy(temConvertidas, tmp, tmp.Length);
                Array.Sort(tmp, new OrdenByPosicion());
                temConvertidas[temConvertidas.Length - 1].pos = f;
                temConvertidas = tmp;
            }

        }
        public int Getpos()
        {
            if (temperaturas == null)
            {
                return 1;
            }
            else
            {
                return temperaturas[temperaturas.Length - 1].pos + 1;
            }
        }
        public string Mostrar()
        {
            if (temperaturas == null)
            {
                return "";
            }
            string mostrar = "";
            for (int i =0;i<temperaturas.Length;i++)
            {
                mostrar += $@"{temperaturas[i].pos}. {temperaturas[i].Dato} grados {temperaturas[i].tipounidad} son {temConvertidas[i].Dato} grados {temConvertidas[i].tipounidad}{Environment.NewLine}";
            }
            return mostrar;
        }
        public string MostrarU()
        {
            if (temperaturas == null)
            {
                return "";
            }
            string mostrar = "";
            for (int i = temperaturas.Length-1; i < temperaturas.Length; i++)
            {
                mostrar += $@"{temperaturas[i].Dato} grados {temperaturas[i].tipounidad} son {temConvertidas[i].Dato} grados {temConvertidas[i].tipounidad}{Environment.NewLine}";
            }
            return mostrar;
        }
    }
}
