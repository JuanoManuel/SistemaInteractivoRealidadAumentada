using JetBrains.Annotations;
using System;
[Serializable]
public class SubtitlesPack
{
    public Subtitles[] pack;

    [Serializable]
    public class Subtitles
    {
        public Subtitle[] subtitles;
        public string name;

    }
}
