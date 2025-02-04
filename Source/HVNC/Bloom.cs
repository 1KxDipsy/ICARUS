using System.Drawing;

namespace ICARUS.HVNC
{
    public class Bloom
    {
        public string _Name;

        private Color _Value;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public Color Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public Bloom()
        {
        }

        public Bloom(string name, Color value)
        {
            _Name = name;
            _Value = value;
        }
    }
}