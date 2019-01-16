using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PokemonXpCalculator.Models;
using SQLite;

namespace PokemonXpCalculator.Data
{
    public class PokemonDao
    {
        readonly SQLiteConnection conexao;

        public PokemonDao(SQLiteConnection conexao)
        {
            try
            {
                this.conexao = conexao;
                this.conexao.CreateTable<Pokemon>();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
            }
        }
    }
}