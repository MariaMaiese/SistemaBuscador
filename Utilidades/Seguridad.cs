using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Utilidades
{
    public class Seguridad : ISeguridad
    {
        public string Encriptar(string password) //encriptación
        {
            SHA256 sha256 = SHA256Managed.Create(); //estamos creando un nnuevo objeto del algoritmo sha256
            ASCIIEncoding encoding = new ASCIIEncoding();   // variable que almacenará la password codificado en base64
            byte[] stream = null; // tenemos que pasarlo al arreglo de bytes, lo declaramos vacío
            StringBuilder sb = new StringBuilder(); //nos permite crear una cadena de texto a partir de un arreglo de bytes (u otras cosas). Toma un arreglo de bytes y los convierte en string
            stream = sha256.ComputeHash(encoding.GetBytes(password)); //ejecucion de la enciptacion (cumputeHash). Estamos generando la encriptacion de la contraseña transformada en bytes

            // por cada byte que tenga nuestra contraseña, generará un caracter que representará la contraseña encriptada
            for (int i = 0; i < stream.Length; i++)
            {
                //{0:x2} es el formato
                sb.AppendFormat("{0:x2}", stream[i]); // recorro el arreglo de bytes y por cada byte se agrega un caracter a nuestro string de cadena encriptada
            }
            return sb.ToString();
        }
    }
}
