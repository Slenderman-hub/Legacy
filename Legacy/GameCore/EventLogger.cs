
namespace Legacy
{

    public class EventLogger
    {
        private readonly Queue<string> _displayedMessages = new Queue<string>();
        private readonly Queue<ConsoleColor> _displayedColors = new Queue<ConsoleColor>();
        private int _maxDisplayLines = 0;

        // Координаты ободка логгера
        private int _startX = 0;
        private int _startY = StatUI.HEIGHT + 1;
        private int _endX = GameSession.CenterIndent - 6;
        private int _endY = GameSession.MAP_HEIGHT;

        //Данные для экрана логгера
        private int _width;
        private int _height;

        private const ConsoleColor _borderColor = ConsoleColor.DarkMagenta;
        private const ConsoleColor _defaultTextColor = ConsoleColor.Gray;
        private const ConsoleColor _timestampColor = ConsoleColor.DarkYellow;
        public EventLogger()
        {
            _width = _endX;
            _height = _endY - _startY;
            _maxDisplayLines = _height - 2;
        }
        public void DrawLoggerUI()
        {
            DrawBorder();
            RefreshDisplay();
        }

        private void DrawBorder()
        {
            Console.ForegroundColor = _borderColor;

            for (int x = _startX; x < _endX; x++)
            {
                Console.SetCursorPosition(x, _startY);
                Console.Write('─');
                Console.SetCursorPosition(x, _endY - 1);
                Console.Write('─');
            }
            for (int y = _startY; y < _endY; y++)
            {
                Console.SetCursorPosition(_startX, y);
                Console.Write('│');
                Console.SetCursorPosition(_endX - 1, y);
                Console.Write('│');
            }
            Console.SetCursorPosition(_startX, _startY);
            Console.Write('┌');
            Console.SetCursorPosition(_endX - 1, _startY);
            Console.Write('┐');
            Console.SetCursorPosition(_startX, _endY - 1);
            Console.Write('└');
            Console.SetCursorPosition(_endX - 1, _endY - 1);
            Console.Write('┘');
            Console.ResetColor();
        }
        public void Log(string message, ConsoleColor messageColor = _defaultTextColor)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string fullMessage = $"[{timestamp}] {message}";


            int maxLineWidth = _width - 4; 
            List<string> lines = InventoryInterface.SplitMessage(fullMessage, maxLineWidth);

            
            foreach (string line in lines)
            {
                _displayedMessages.Enqueue(line);
                _displayedColors.Enqueue(messageColor);
            }

            while (_displayedMessages.Count > _maxDisplayLines)
            {
                _displayedMessages.Dequeue();
                _displayedColors.Dequeue();
            }
            RefreshDisplay();
        }

        

        private void RefreshDisplay()
        {
            for (int y = _startY + 1; y < _endY - 1; y++)
            {
                Console.SetCursorPosition(_startX + 1, y);
                Console.Write(new string(' ', _width - 2));
            }

            string[] messagesToDisplay = _displayedMessages.ToArray();
            ConsoleColor[] colorsToDisplay = _displayedColors.ToArray();

            int displayY = _endY - 2; 

            for (int i = messagesToDisplay.Length - 1; i >= 0; i--)
            {
                if (displayY <= _startY) break;

                Console.SetCursorPosition(_startX + 2, displayY);

                string message = messagesToDisplay[i];
                ConsoleColor messageColor = colorsToDisplay[i];
                int timestampEnd = message.IndexOf(']') + 1;

                if (timestampEnd > 0)
                {
                    Console.ForegroundColor = _timestampColor;
                    Console.Write(message.Substring(0, timestampEnd));

                    Console.ForegroundColor = messageColor;
                    Console.Write(message.Substring(timestampEnd));
                }
                else
                {
                    Console.ForegroundColor = messageColor;
                    Console.Write(message);
                }

                displayY--;
            }

            Console.ResetColor();
        }

        public void Clear()
        {
            _displayedMessages.Clear();
            RefreshDisplay();
        }

    }
}
