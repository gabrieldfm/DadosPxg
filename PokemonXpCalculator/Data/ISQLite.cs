using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace PokemonXpCalculator.Data
{
    public interface ISQLite
    {
        SQLiteConnection PegarConexao();
    }
}