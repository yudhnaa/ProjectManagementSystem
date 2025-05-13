using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.AppContext
{
    public class FormContext
    {
        private static readonly FormContext _instance = new FormContext();
        public static FormContext Instance => _instance;

        private Form _loginForm;

        private Form _homeForm;

        private FormContext() { }

        public Form LoginForm => _loginForm ?? throw new InvalidOperationException("Login Form is not set.");
        public Form HomeForm => _homeForm ?? throw new InvalidOperationException("Home Form is not set.");

        public void SetLoginForms(Form loginForm)
        {
            if (_loginForm is IDisposable disposableLogin)
            {
                disposableLogin.Dispose();
            }

            _loginForm = loginForm;
        }

        public void SetHomeForms(Form homeForm)
        {
         
            if (_homeForm is IDisposable disposableHome)
            {
                disposableHome.Dispose();
            }

            _homeForm = homeForm;
        }

        public void Logout()
        {
            if (_homeForm is IDisposable disposableHome)
            {
                disposableHome.Dispose();
            }

            UserSession.Instance.Logout();

            _loginForm.Show();
        }

        public bool HasLoginForms => _loginForm != null;
        public bool HasHomeForms => _homeForm != null;
    }

}
