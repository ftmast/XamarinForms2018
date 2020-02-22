using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servicos.Modelo;
using App01_ConsultarCEP.Servicos;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;

        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if(end != null)
                    {

                        RESULTADO.Text = string.Format("Endereço: {0} - {1} - {2}/{3}", end.logradouro, end.bairro, end.localidade, end.uf);

                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado:" + cep,"OK");
                    }

                }
                catch(Exception e)
                {
                    DisplayAlert("Erro critico", e.Message, "OK");
                }


            }

        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            int NovoCEP = 0;

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }
            
            if(!int.TryParse(cep,out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter apenas numeros.", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
