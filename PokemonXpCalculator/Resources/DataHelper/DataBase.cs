using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using PokemonXpCalculator.Models;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using System.IO;
using PokemonXpCalculator.Data;

namespace PokemonXpCalculator.Resources.DataHelper
{
    public class DataBase : ISQLite
    {
        private const string nomeAruivoDB = "PokexGames.db3";

        //string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public SQLiteConnection PegarConexao()
        {
            var caminhoDB = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, nomeAruivoDB);

            return new SQLiteConnection(caminhoDB);
        }
    }
}