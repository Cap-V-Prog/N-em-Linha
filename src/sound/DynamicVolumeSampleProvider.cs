using System;
using NAudio.Wave;

namespace NemLinha_Projeto
{
    public class DynamicVolumeSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider _source;
        private float _volume;

        public DynamicVolumeSampleProvider(ISampleProvider source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _volume = 1.0f; // Default volume is 100%
        }

        public float Volume
        {
            get { return _volume; }
            set { _volume = Math.Max(0.0f, Math.Min(1.0f, value)); }
        }

        public WaveFormat WaveFormat => _source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = _source.Read(buffer, offset, count);

            for (int i = 0; i < samplesRead; i++)
            {
                buffer[offset + i] *= _volume;
            }

            return samplesRead;
        }
    }
}
   
