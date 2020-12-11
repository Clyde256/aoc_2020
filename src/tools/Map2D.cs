namespace AOC.Tools
{
    public class Map2D
    {
        private char[,] _map = null;

        public Map2D Clone()
        {
            var clone = new Map2D();
            clone._map = new char[this.Rows, this.Cols];

            for(var y = 0; y < this.Rows; y++)
            {
                for(var x = 0; x < this.Cols; x++)
                {
                    clone.SetValue(y, x, this.Value(y, x));
                }
            }

            return clone;
        }

        public void Load(string filePath)
        {
            var reader = FileIO.CreateProjFilePath(filePath);
            var list = reader.ReadAll();
            if (list.Count <= 0) return;

            _map = new char[list.Count, list[0].Length];

            for (var iRow = 0; iRow < list.Count; iRow++) 
            {
                for (var iCol = 0; iCol < list[iRow].Length; iCol++)
                {
                    _map[iRow, iCol] = list[iRow][iCol];
                }
            }
        }

        public void Print() 
        {
            if (_map == null) return;
            
            for (var iRow = 0; iRow < this.Rows; iRow++)
            {
                for (var iCol = 0; iCol < this.Cols; iCol++)
                {
                    System.Console.Write(_map[iRow, iCol]);
                }
                System.Console.Write("\n");
            }
        }

        public int Rows { get { return _map.GetLength(0); } }
        public int Cols { get { return _map.GetLength(1); } }

        public char Value(int y, int x) 
        {
            if (y >= Rows) y = y % Rows;
            if (x >= Cols) x = x % Cols;

            return _map[y, x];
        }

        public bool TryGetValue(int y, int x, ref char value) 
        {
            if (y < 0 || x < 0) return false;
            if (y >= Rows) return false;
            if (x >= Cols) return false;

            value = _map[y, x];

            return true;
        }

        public void SetValue(int y, int x, char value) 
        {
            if (y >= Rows) return;
            if (x >= Cols) return;

            _map[y, x] = value;
        }

        public bool Equale(Map2D map)
        {
            for(var y = 0; y < this.Rows; y++)
            {
                for(var x = 0; x < this.Cols; x++)
                {
                    if (map.Value(y, x) != this.Value(y, x)) return false;
                }
            }

            return true;
        }
    }
}