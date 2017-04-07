using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace JellyJam.Ui {
  public class MusicLibrary {
    public static string HEROIC_DEMISE = "heroic_demise";

    private Dictionary<string, Song> music;

    public MusicLibrary(ContentManager content) {
      music = new Dictionary<string, Song>();

      music[HEROIC_DEMISE] = content.Load<Song>("music/heroic_demise");
    }

    public Song this[string key] {
      get { return music[key]; }
      set { music[key] = value; }
    }

    public void play(string key) {
      MediaPlayer.Play(music[key]);
      MediaPlayer.IsRepeating = true;
    }

    public void stop() {
      MediaPlayer.Stop();
    }

    public void pause() {
      MediaPlayer.Pause();
    }
  }
}
