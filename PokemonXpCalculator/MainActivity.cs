using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Gms.Ads;
using PokemonXpCalculator.Controller;
using System;
using System.Linq;
using PokemonXpCalculator.Data;
using PokemonXpCalculator.Resources.DataHelper;

namespace PokemonXpCalculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Membros

        public static DadosLevel dados;
        Button btCalc;
        EditText lvlAtual;
        EditText xpAtual;
        EditText lvlFuturo;
        TextView txtXp;
        TextView txtLvl;
        protected AdView adView;
        private LevelDao levelDao;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Teste Banco
            levelDao = new LevelDao(new DataBase().PegarConexao());
            levelDao.InserirDadosIniciais();

            dados = new DadosLevel();
            dados.SetaDdos();

            adView = FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);

            lvlAtual = FindViewById<EditText>(Resource.Id.lvlAtual);
            xpAtual = FindViewById<EditText>(Resource.Id.xpAtual);
            lvlFuturo = FindViewById<EditText>(Resource.Id.lvlFuturo);
            txtXp = FindViewById<TextView>(Resource.Id.txtXp);
            txtLvl = FindViewById<TextView>(Resource.Id.txtLvl);
            btCalc = FindViewById<Button>(Resource.Id.btCalc);

            btCalc.Click += btCalc_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (adView != null)
            {
                adView.Resume();
            }
        }

        void btCalc_Click(Object sender, EventArgs e)
        {
            CalculaPorLvl();
            CalculaPorXp();
        }

        #region Métodos

        private void CalculaPorLvl()
        {
            //var listaDadosLvl = dados.DicLvl;

            if (string.IsNullOrEmpty(lvlAtual.Text)) return;
            if (string.IsNullOrEmpty(lvlFuturo.Text))
            {
                Toast.MakeText(this, "Informe o level futuro", ToastLength.Short).Show();
                return;
            }
            var lvlAtualLocal = Convert.ToInt32(lvlAtual.Text);
            var lvlFuturoLocal = Convert.ToInt32(lvlFuturo.Text);

            //Teste Banco
            var maxLevel = levelDao.GetMaxLevel();

            if (lvlFuturoLocal <= lvlAtualLocal) return;
            if (lvlFuturoLocal <= 2 || lvlAtualLocal <= 1) return;
            //Teste Banco
            if (lvlFuturoLocal > maxLevel || lvlAtualLocal > maxLevel)
            {
                Toast.MakeText(this, lvlFuturoLocal + " não é um level futuro válido", ToastLength.Short).Show();
                return;
            }

            try
            {
                //var dadsoLvlIni = listaDadosLvl.Where(x => x.Key == lvlAtualLocal).ToList();
                //var dadsoLvlFin = listaDadosLvl.Where(x => x.Key == lvlFuturoLocal).ToList();
                //Teste Banco
                var dadsoLvlIniDao = levelDao.GetLevel(lvlAtualLocal);
                var dadsoLvlFinDao = levelDao.GetLevel(lvlFuturoLocal);

                //txtLvl.Text = "Por Level: " + Convert.ToString(dadsoLvlFin.First().Value - dadsoLvlIni.First().Value);
                //Teste Banco
                txtLvl.Text = "Por Level: " + Convert.ToString(dadsoLvlFinDao.Experiencia - dadsoLvlIniDao.Experiencia);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Ocorreu um problema ao processar os dados - " + ex.Message, ToastLength.Short).Show();
            }

        }

        private void CalculaPorXp()
        {
            //var listaDadosLvl = dados.DicLvl;
            if (string.IsNullOrEmpty(xpAtual.Text)) return;
            if (string.IsNullOrEmpty(lvlFuturo.Text))
            {
                Toast.MakeText(this, "Informe o level futuro", ToastLength.Short).Show();
                return;
            } 
            var xpAtualLocal = Convert.ToInt32(xpAtual.Text);
            var lvlFuturoLocal = Convert.ToInt32(lvlFuturo.Text);

            //Teste Banco
            var dadosLevel = levelDao.GetLevel(lvlFuturoLocal);

            //if (listaDadosLvl.FirstOrDefault(x => x.Key == lvlFuturoLocal).Value <= xpAtualLocal) return;
            if (dadosLevel.Experiencia <= xpAtualLocal) return;

            //var dadsoLvlFin = listaDadosLvl.FirstOrDefault(x => x.Key == lvlFuturoLocal).Value;

            //Teste Banco
            txtXp.Text = "Por Xp: " + Convert.ToString(dadosLevel.Experiencia - xpAtualLocal);
        }

        #endregion
    }
}

