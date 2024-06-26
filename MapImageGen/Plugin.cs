using System;
using Exiled.API.Features;
using HarmonyLib;
using Server = Exiled.Events.Handlers.Server;

namespace MapImageGen
{
    public class Plugin : Plugin<Config>
    {

        public override string Name => "MapImageGen";
        public override string Author => "Nom";
        public override Version Version => new Version(1, 0, 0);
        public static Plugin Instance { get; private set; }

        public int _patchesCounter;
        private Harmony harmony;

        public override void OnEnabled()
        {
            Server.RoundStarted += EventHandler.OnRoundStarted;
            Server.RoundEnded += EventHandler.OnRoundEnded;

            Instance = this;

            try
            {
                harmony = new Harmony($"com.nom.patch");
                harmony.PatchAll();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            base.OnEnabled();
        }

        public override void OnDisabled()
        {

            Server.RoundStarted -= EventHandler.OnRoundStarted;
            Server.RoundEnded -= EventHandler.OnRoundEnded;

            Instance = null;
            base.OnDisabled();
        }
    }
}
