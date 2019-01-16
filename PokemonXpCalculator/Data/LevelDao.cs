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
using PokemonXpCalculator.Controller;
using PokemonXpCalculator.Models;
using SQLite;

namespace PokemonXpCalculator.Data
{
    public class LevelDao
    {
        readonly SQLiteConnection conexao;

        public LevelDao(SQLiteConnection conexao)
        {
            try
            {
                this.conexao = conexao;
                this.conexao.CreateTable<Level>();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
            }
        }

        public void InserirDadosIniciais()
        {
            try
            {
                if (conexao.Find<Level>(1) == null)
                {
                    var dados = new DadosLevel();
                    dados.SetaDdos();
                    foreach (var level in dados.DicLvl)
                    {
                        var objLevel = new Level
                        {
                            Id = level.Key,
                            Experiencia = level.Value
                        };
                        conexao.Insert(objLevel);
                    } 
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
            }
        }

        public Level GetLevel(int levelId)
        {
            try
            {
                return conexao.Find<Level>(levelId);
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return null;
            }
        }

        public int GetMaxLevel()
        {
            try
            {
                return conexao.Table<Level>().Max().Id;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return 0;
            }
        }
    }
}