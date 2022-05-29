using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Helpers;
using Microsoft.AspNetCore.Authentication;
using KO.Recursos;

namespace Web.Models.Account
{
    public class LoginViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = nameof(Global.ValidacionCampoUsuarioRequerido), ErrorMessageResourceType = typeof(Global))]
        public string Usuario { get; set; }

        private string encriptPasword;
       
        [Required(ErrorMessageResourceName = nameof(Global.ValidacionCampoPasswordRequerido), ErrorMessageResourceType = typeof(Global))]
        public string Password
        {
            get
            {
                return encriptPasword;
            }
            set
            { 
                if (value != null) encriptPasword = EncryptionHelper.Encrypt(value);//Para que no salte error en Ecryption..          
            }
        }
    }
}
