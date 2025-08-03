﻿using BusinessLogic.Services;

namespace BusinessLogic.Services
{
    public static class ArmarMail
    {
        public static string DireccionCorreo { get; set; }
        public static string Asunto { get; set; }
        public static string NuevaContraseña { get; set; }

        static ArmarMail()
        {
            DireccionCorreo = string.Empty;
            Asunto = string.Empty;
            NuevaContraseña = string.Empty;
        }

        public static void Preparar()
        {
            string body = @"<style>
                            h1{color:dodgerblue;}
                            h2{color:darkorange;}
                            </style>
                            <h1> Contraseña de ingreso: </h1></br>
                            <h2> " + NuevaContraseña + "</h2>";
            EnviarMail.sendMail(DireccionCorreo, Asunto, body);
        }
    }
}
